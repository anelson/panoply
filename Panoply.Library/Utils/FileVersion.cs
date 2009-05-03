using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Panoply.Library.Utils
{
    public class FileVersion
    {
        private static class Win32
        {
            [StructLayout(LayoutKind.Sequential)]
            public class VS_FIXEDFILEINFO
            {
                public uint signature;
                public uint structVersion;
                public uint fileVersionMs;
                public uint fileVersionLs;
                public uint productVersionMs;
                public uint productVersionLs;
                public uint fileFlagMask;
                public uint fileFlags;
                public uint fileOs;
                public uint fileType;
                public uint fileSubtype;
                public uint fileDateMs;
                public uint fileDateLs;
            }

            [DllImport("version.dll", SetLastError=true)]
            public static extern bool GetFileVersionInfo(string filename, int handle, int length, IntPtr buffer);

            [DllImport("version.dll", SetLastError = true)]
            public static extern int GetFileVersionInfoSize(string filename, out int handle);

            [DllImport("version.dll", SetLastError = true)]
            public static extern bool VerQueryValue(IntPtr buffer, string subblock, out IntPtr blockbuffer, out int len);
        }

        public static FileVersion Empty = new FileVersion(0, 0, 0, 0);

        public readonly ushort Major;
        public readonly ushort Minor;
        public readonly ushort Revision;
        public readonly ushort Build;

        private FileVersion(ushort major, ushort minor, ushort revision, ushort build)
        {
            Major = major;
            Minor = minor;
            Revision = revision;
            Build = build;
        }

        public override string ToString()
        {
            return String.Format("{0}.{1}.{2}.{3}",
                Major,
                Minor,
                Revision,
                Build);
        }

        public override int GetHashCode()
        {
            return Major.GetHashCode() ^
                Minor.GetHashCode() ^
                Revision.GetHashCode() ^
                Build.GetHashCode();
        }

        public static FileVersion GetVersionOfFile(String path)
        {
            int dontCare = 0;
            int size = Win32.GetFileVersionInfoSize(path, out dontCare);
            byte[] versionInfo = new byte[size];
            GCHandle versionInfoHandle = GCHandle.Alloc(versionInfo, GCHandleType.Pinned);
            try
            {
                Win32.GetFileVersionInfo(path, dontCare, size, versionInfoHandle.AddrOfPinnedObject());

                IntPtr fixedFileInfoPtr;
                if (!Win32.VerQueryValue(versionInfoHandle.AddrOfPinnedObject(), "\\", out fixedFileInfoPtr, out dontCare))
                {
                    return null;
                }

                Win32.VS_FIXEDFILEINFO fixedFileInfo = new Win32.VS_FIXEDFILEINFO();

                Marshal.PtrToStructure(fixedFileInfoPtr, fixedFileInfo);

                return new FileVersion((ushort)(fixedFileInfo.fileVersionMs >> 16),
                    (ushort)(fixedFileInfo.fileVersionMs & 0xffff),
                    (ushort)(fixedFileInfo.fileVersionLs >> 16),
                    (ushort)(fixedFileInfo.fileVersionLs & 0xffff));
            }
            finally
            {
                versionInfoHandle.Free();
            }
        }
    }
}
