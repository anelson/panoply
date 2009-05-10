using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using DirectShowLib;

namespace Panoply.Library.Utils
{
    /// <summary>
    /// Wrapper for some native COM shit
    /// </summary>
    public static class Com
    {
        public const int E_FILENOTFOUND = unchecked((int)0x80070002);
        public const int E_PROP_ID_UNSUPPORTED = unchecked((int)0x80070490);
        public const int E_INVALIDARG = unchecked((int)0x80070057);

        [DllImport("OleAut32.dll", PreserveSig = false)]
        public static extern int OleCreatePropertyFrame(IntPtr hwndOwner, 
            uint x, 
            uint y,
            [MarshalAs(UnmanagedType.LPWStr)] string lpszCaption,
            UInt32 cObjects, 
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4, ArraySubType = UnmanagedType.IUnknown)] object[] lplpUnk,
            UInt32 cPages, 
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 6)] Guid[] lpPageClsID,
            uint lcid, 
            UInt32 dwReserved,
            IntPtr lpvReserved);

        [DllImport("OleAut32.dll", PreserveSig = false)]
        public static extern void OleCreatePropertyFrame(IntPtr hwndOwner,
            uint x,
            uint y,
            [MarshalAs(UnmanagedType.LPWStr)] string lpszCaption,
            UInt32 cObjects,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4, ArraySubType = UnmanagedType.IUnknown)] object[] lplpUnk,
            UInt32 cPages,
            IntPtr lpPageClsID,
            uint lcid,
            UInt32 dwReserved,
            IntPtr lpvReserved);
        
        [DllImport("kernel32", SetLastError=true)]
        public static extern IntPtr LoadLibrary(string lpFileName);
        
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr hModule);
        
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
        
        public delegate int DllRegisterServerInvoker();

        public static void RegisterDll(String dllPath, bool register)
        {
            IntPtr handle = IntPtr.Zero;

            try
            {
                handle = LoadLibrary(dllPath);
            }
            catch (Exception e)
            {
                throw new ApplicationException(
                    String.Format("Unable to load DLL '{0}'", dllPath),
                    e);
            }

            //For some reason, despite SetLastError=true in the pinvoke declaration, LoadLibrary
            //calls that fail don't throw. 
            if (handle == IntPtr.Zero)
            {
                throw new ApplicationException(
                    String.Format("Unable to load DLL '{0}'.  It either does not exist or is invalid", dllPath));
            }

            try
            {
                //Find the entry point for DLL register/unregister
                String funcName;
                if (register) {
                    funcName = "DllRegisterServer";
                } else {
                    funcName = "DllUnregisterServer";
                }

                IntPtr fptr;

                try
                {
                    fptr = GetProcAddress(handle, funcName);
                }
                catch (Exception e)
                {
                    throw new NotSupportedException(string.Format("The DLL '{0}' does not support COM registration", dllPath),
                        e);
                }

                DllRegisterServerInvoker drs = (DllRegisterServerInvoker) Marshal.GetDelegateForFunctionPointer( fptr, typeof(DllRegisterServerInvoker) );

                //Now we have a delegate wrapping this function pointer.  Call it.
                int hr = drs();

                try
                {
                    DsError.ThrowExceptionForHR(hr);
                }
                catch (Exception e)
                {
                    throw new ApplicationException(
                        String.Format("There was an error invoking the {0}register function in DLL '{1}': {2}",
                        (register ? "" : "un"),
                        dllPath,
                        e.Message),
                        e);
                }
            }
            finally
            {
                FreeLibrary(handle);
            }
        }
    }
}
