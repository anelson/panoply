using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

using Microsoft.Win32;

using DirectShowLib;

using Panoply.Library;

namespace Panoply.Library.Filters
{
    public class Filter : FilterDevice
    {
        [StructLayout(LayoutKind.Sequential)]
        class BinaryData
        {
            [MarshalAs(UnmanagedType.U4)]
            public uint Version;
            [MarshalAs(UnmanagedType.U4)]
            public Merit Merit;
        }

        const String FILTER_DATA_BLOB_VALUE = "FilterData";

        BinaryData _binaryData;
        byte[] _binaryDataBlob;
        String _rawFilePath;
        String _filePath;
        FilterCategory _category;
        Utils.FileVersion _fileVersion;


        internal Filter(FilterCategory category, DsDevice device)
            : base(device)
        {
            _category = category;
        }

        public String FilterRegistryKey { get { return String.Format("CLSID\\{{{0}}}", Clsid); } }
        public String FilterInProcServer32RegistryKey { get { return String.Format("{0}\\InProcServer32", FilterRegistryKey); } }
        public String FilterInstanceRegistryKey { get { return String.Format("CLSID\\{{{0}}}\\Instance\\{{{1}}}", _category.Clsid, Clsid); } }

        /// <summary>
        /// HKEY_CLASSES_ROOT\Filter\[clsid] key, supposedly used to make the filter discoverable for intelligent
        /// connect, according to http://msdn.microsoft.com/en-us/library/ms924596.aspx
        /// </summary>
        public String FilterIntelligentConnectRegistryKey { get { return String.Format("Filter\\{{{0}}}", Clsid); } }

        /// <summary>
        /// All registry keys relevant to this filter.  Useful in trying to unregister it manually
        /// </summary>
        public String[] FilterRegistryKeys
        {
            get
            {
                return new String[] {
                    FilterRegistryKey,
                    FilterInstanceRegistryKey,
                    FilterIntelligentConnectRegistryKey
                };
            }
        }

        public String RawFilePath
        {
            get
            {
                if (Clsid == Guid.Empty)
                {
                    //If we couldn't get the CLSID, then nothing in the registry will be available
                    return null;
                }

                if (_rawFilePath == null)
                {
                    using (RegistryKey reg = Registry.ClassesRoot.OpenSubKey(FilterInProcServer32RegistryKey))
                    {
                        _rawFilePath = (String)reg.GetValue(String.Empty);
                    }
                }

                return _rawFilePath;
            }
        }

        public String FilePath
        {
            get
            {
                if (_filePath == null)
                {
                    _filePath = RawFilePath;
                    if (_filePath != null)
                    {
                        if (_filePath.StartsWith("%"))
                        {
                            //This path includes an environment variable.  Expand the environment variables
                            _filePath = Environment.ExpandEnvironmentVariables(_filePath);
                        }

                        //Many codecs have a file name with no path.  Usually these are system files like quartz.dll, but not always
                        //Expand the raw file path into a usable file system path
                        if (Path.GetDirectoryName(_filePath) == String.Empty)
                        {
                            //This file name has no path, which implies the file is in the system path
                            _filePath = Path.Combine(Environment.SystemDirectory, _filePath);
                        }
                    }
                }

                return _filePath;
            }
        }

        public uint Version
        {
            get {
                if (GetBinaryData() != null)
                {
                    return GetBinaryData().Version;
                }
                else
                {
                    return 0;
                }
            }
        }

        public Utils.FileVersion FileVersion
        {
            get
            {
                if (_fileVersion == null)
                {
                    if (File.Exists(FilePath))
                    {
                        _fileVersion = Utils.FileVersion.GetVersionOfFile(FilePath);
                    }

                    //GetVersionOfFile returns null if the given file contains no version information
                    if (_fileVersion == null) {
                        _fileVersion = Utils.FileVersion.Empty;
                    }
                }

                return _fileVersion;
            }
        }

        public void SetMerit(Merit merit) {
            if (GetBinaryData() == null)
            {
                throw new InvalidOperationException("Can't set merit when no binary data are available");
            }

            Merit oldMerit = GetBinaryData().Merit;

            try
            {
                GetBinaryData().Merit = merit;
                WriteBinaryData();
            }
            catch
            {
                //Continue to report the old merit since the new merit didn't save correctly
                GetBinaryData().Merit = oldMerit;
                throw;
            }
        }

        public void ShowPropertyPage(IntPtr parentWindowHandle)
        {
            if (Clsid == Guid.Empty) {
                //We don't have a CLSID for this filter, so there's no way we can create it
                throw new InvalidOperationException("You cannot display the property page for a filter with no CLSID");
            }

            Type filterType = Type.GetTypeFromCLSID(Clsid);

            Object filterObj = null;

            try
            {
                filterObj = System.Activator.CreateInstance(filterType);
            }
            catch (FileNotFoundException e)
            {
                throw new ApplicationException(
                    String.Format("Unable to display property page for this filter because the filter DLL '{0}' was not found",
                    FilePath),
                    e);
            }

            IBaseFilter filter = (IBaseFilter)filterObj;

            ISpecifyPropertyPages propPages;
            try
            {
                propPages = (ISpecifyPropertyPages)filterObj;
            }
            catch (InvalidCastException e)
            {
                throw new NotSupportedException("This filter does not support property pages", e);
            }

            DsCAUUID pages = new DsCAUUID();
            int hr = propPages.GetPages(out pages);
            DsError.ThrowExceptionForHR(hr);

            try
            {
                hr = Utils.Com.OleCreatePropertyFrame(parentWindowHandle,
                    0,
                    0,
                    FriendlyName,
                    1,
                    new object[] { filter },
                    (uint)pages.cElems,
                    pages.ToGuidArray(),
                    0,
                    0,
                    IntPtr.Zero);
                DsError.ThrowExceptionForHR(hr);
            }
            finally
            {
                Marshal.FreeCoTaskMem(pages.pElems);
            }
        }

        /// <summary>
        /// Uses COM to unregister this filter DLL.  
        /// </summary>
        public void Unregister()
        {
            Panoply.Library.Utils.Com.RegisterDll(FilePath, false);
            _category.FilterUnregistered(this);
        }

        /// <summary>
        /// Attempts to manually remove this filter from the registry.
        /// Do not attempt unless Unregister has failed, for example because the DLL is no longer installed
        /// </summary>
        public void RemoveFromRegistry()
        {
            foreach (String key in FilterRegistryKeys)
            {
                try
                {
                    RemoveRegistryKey(key);
                }
                catch (Exception e)
                {
                    throw new ArgumentException(String.Format("Error removing registry key '{0}': {1}", key, e.Message),
                        e);
                }
            }
        }

        protected override Merit GetMerit()
        {
            if (GetBinaryData() != null)
            {
                return GetBinaryData().Merit;
            }
            else
            {
                return Merit.None;
            }
        }

        private BinaryData GetBinaryData()
        {
            if (Clsid == Guid.Empty)
            {
                //If we couldn't get the CLSID, then nothing in the registry will be available
                return _binaryData;
            }

            if (_binaryData == null)
            {
                using (RegistryKey reg = Registry.ClassesRoot.OpenSubKey(FilterInstanceRegistryKey))
                {
                    if (reg != null)
                    {
                        _binaryDataBlob = (byte[])reg.GetValue(FILTER_DATA_BLOB_VALUE);
                        GCHandle handle = GCHandle.Alloc(_binaryDataBlob, GCHandleType.Pinned);
                        try
                        {
                            _binaryData = new BinaryData();
                            Marshal.PtrToStructure(handle.AddrOfPinnedObject(),
                                _binaryData);
                        }
                        finally
                        {
                            handle.Free();
                        }
                    }
                }
            }

            return _binaryData;
        }

        private void WriteBinaryData()
        {
            //Write the updated binary data to the binary data blob, then write the blob to the registry
            GCHandle handle = GCHandle.Alloc(_binaryDataBlob, GCHandleType.Pinned);
            try
            {
                Marshal.StructureToPtr(_binaryData,
                    handle.AddrOfPinnedObject(),
                    true);
            }
            finally
            {
                handle.Free();
            }
            try
            {
                using (RegistryKey reg = Registry.ClassesRoot.OpenSubKey(FilterInstanceRegistryKey, RegistryKeyPermissionCheck.ReadWriteSubTree))
                {
                    if (reg == null)
                    {
                        throw new InvalidOperationException(String.Format("The registry key '{0}' does not exist for this filter", FilterInstanceRegistryKey));
                    }

                    reg.SetValue(FILTER_DATA_BLOB_VALUE, _binaryDataBlob);
                }
            } 
            catch (System.Security.SecurityException e)
            {
                throw new ApplicationException("Access to the specified registry key is denied.  Ensure this application is running with administrator privileges.",
                    e);
            }
        }

        private void RemoveRegistryKey(string key)
        {
            try
            {
                RegistryKey reg = Registry.ClassesRoot.OpenSubKey(key, RegistryKeyPermissionCheck.ReadWriteSubTree);
                if (reg == null) { return; }
                reg.Close();
            }
            catch (System.Security.SecurityException e)
            {
                throw new ApplicationException("Access to the specified registry key is denied.  Ensure this application is running with administrator privileges.",
                    e);
            }

            Registry.ClassesRoot.DeleteSubKeyTree(key);
        }
    }
}
