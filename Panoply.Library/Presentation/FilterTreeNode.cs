using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DirectShowLib;

using Panoply.Library.Filters;
using Panoply.Library.Utils;

namespace Panoply.Library.Presentation
{
    public class FilterTreeNode : FilterDeviceTreeNode
    {
        Filter _filter;

        public FilterTreeNode(Filter filter)
            : base(filter)
        {
            _filter = filter;
        }

        public string RawFilePath
        {
            get
            {
                try
                {
                    return _filter.RawFilePath;
                }
                catch (Exception e)
                {
                    ReportPropertyException("RawFilePath", e);
                    return null;
                }
            }
        }

        public Exception RawFilePathException
        {
            get { return GetPropertyException("RawFilePath"); }
        }

        public string FilePath
        {
            get
            {
                try
                {
                    return _filter.FilePath;
                }
                catch (Exception e)
                {
                    ReportPropertyException("FilePath", e);
                    return null;
                }
            }
        }

        public Exception FilePathException
        {
            get { return GetPropertyException("FilePath"); }
        }

        public uint Version
        {
            get
            {
                try
                {
                    return _filter.Version;
                }
                catch (Exception e)
                {
                    ReportPropertyException("Version", e);
                    return uint.MinValue;
                }
            }
        }

        public Exception VersionException
        {
            get { return GetPropertyException("Version"); }
        }

        public FileVersion FileVersion
        {
            get
            {
                try
                {
                    return _filter.FileVersion;
                }
                catch (Exception e)
                {
                    ReportPropertyException("FileVersion", e);
                    return null;
                }
            }
        }

        public Exception FileVersionException
        {
            get { return GetPropertyException("FileVersion"); }
        }

        public void SetMerit(Merit merit)
        {
            _filter.SetMerit(merit);
        }

        public void ShowPropertyPage(IntPtr parentWindowHandle)
        {
            _filter.ShowPropertyPage(parentWindowHandle);
        }

        /// <summary>
        /// Uses COM to unregister this filter DLL.  
        /// </summary>
        public void Unregister()
        {
            _filter.Unregister();
        }

        /// <summary>
        /// Attempts to manually remove this filter from the registry.
        /// Do not attempt unless Unregister has failed, for example because the DLL is no longer installed
        /// </summary>
        public void RemoveFromRegistry()
        {
            _filter.RemoveFromRegistry();
        }

        internal override void WriteToXml(System.Xml.XmlWriter writer)
        {
            writer.WriteStartElement("filter");
            {
                WriteXmlAttributeIfNonNull(writer, "friendlyName", FriendlyName, FriendlyNameException);

                WriteXmlAttributeIfNonNull(writer, "devicePath", DevicePath, DevicePathException);

                WriteXmlAttributeIfNonNull(writer, "merit", Merit, MeritException);

                WriteXmlAttributeIfNonNull(writer, "clsid", Clsid, ClsidException);

                WriteXmlAttributeIfNonNull(writer, "rawFilePath", RawFilePath, RawFilePathException);

                WriteXmlAttributeIfNonNull(writer, "filePath", FilePath, FilePathException);

                WriteXmlAttributeIfNonNull(writer, "version", Version, VersionException);

                WriteXmlAttributeIfNonNull(writer, "fileVersion", FileVersion, FileVersionException);
            }
            writer.WriteEndElement();
        }
    }
}
