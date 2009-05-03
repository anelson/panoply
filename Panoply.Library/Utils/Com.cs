using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

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

        [DllImport("olepro32.dll", PreserveSig = false)]
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

        [DllImport("olepro32.dll", PreserveSig = false)]
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

    }
}
