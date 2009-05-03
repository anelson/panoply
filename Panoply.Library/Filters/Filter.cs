using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Linq;
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

        BinaryData _binaryData;
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

        public void ShowPropertyPage(IntPtr parentWindowHandle)
        {
            if (Clsid == Guid.Empty) {
                //We don't have a CLSID for this filter, so there's no way we can create it
                throw new InvalidOperationException("You cannot display the property page for a filter with no CLSID");
            }

            Type filterType = Type.GetTypeFromCLSID(Clsid);

            Object filterObj = System.Activator.CreateInstance(filterType);
            IBaseFilter filter = (IBaseFilter)filterObj;
            try
            {
                ISpecifyPropertyPages propPages = (ISpecifyPropertyPages)filterObj;
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
            catch (COMException e)
            {
                throw new NotSupportedException("This filter does not support property pages", e);
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
                        byte[] blob = (byte[])reg.GetValue("FilterData");
                        GCHandle handle = GCHandle.Alloc(blob, GCHandleType.Pinned);
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
    }
}
