using System;
using System.Collections.Generic;
using System.Text;

using DirectShowLib;

using Panoply.Library.Filters;

namespace Panoply.Library.Presentation
{
    public class PropertyExceptionEventArgs : EventArgs
    {
        public readonly String PropertyName;
        public readonly Exception Exception;

        public PropertyExceptionEventArgs(String propertyName, Exception exception)
        {
            this.PropertyName = propertyName;
            this.Exception = exception;
        }
    }

    public delegate void PropertyExceptionEventHandler(object sender, PropertyExceptionEventArgs args);

    /// <summary>
    /// Abstract base class representing a node in the filter tree.  Concrete subclasses
    /// of this class wrap Filter and FilterCategory objects in an on-demand and exception-eating
    /// layer that makes it easier to build some sort of GUI on top without the whole thing 
    /// crashing down if there's an unexpected exception somewhere.
    /// 
    /// Until you touch most of the properties on this and its subclasses, they are not evaluated, and 
    /// when they are any exceptions are eaten and reported with a separate property.
    /// </summary>
    public abstract class FilterDeviceTreeNode
    {
        FilterDevice _device;
        Dictionary<String, Exception> _propertyExceptions = new Dictionary<string, Exception>();

        internal FilterDeviceTreeNode(FilterDevice device)
        {
            _device = device;
        }

        public event PropertyExceptionEventHandler PropertyException;

        public String FriendlyName
        {
            get
            {
                try
                {
                    return _device.FriendlyName;
                }
                catch (Exception e)
                {
                    ReportPropertyException("FriendlyName", e);
                    return null;
                }
            }
        }

        public Exception FriendlyNameException
        {
            get { return GetPropertyException("FriendlyName"); }
        }

        public String DevicePath
        {
            get
            {
                try
                {
                    return _device.DevicePath;
                }
                catch (Exception e)
                {
                    ReportPropertyException("DevicePath", e);
                    return null;
                }
            }
        }

        public Exception DevicePathException
        {
            get { return GetPropertyException("DevicePath"); }
        }

        public Merit Merit
        {
            get
            {
                try
                {
                    return _device.Merit;
                }
                catch (Exception e)
                {
                    ReportPropertyException("Merit", e);
                    return Merit.None;
                }
            }
        }

        public Exception MeritException
        {
            get { return GetPropertyException("Merit"); }
        }

        public Guid Clsid
        {
            get
            {
                try
                {
                    return _device.Clsid;
                }
                catch (Exception e)
                {
                    ReportPropertyException("Clsid", e);
                    return Guid.Empty;
                }
            }
        }

        public Exception ClsidException
        {
            get { return GetPropertyException("Clsid"); }
        }

        internal abstract void WriteToXml(System.Xml.XmlWriter writer);

        protected Exception GetPropertyException(string propertyName)
        {
            Exception e = null;
            _propertyExceptions.TryGetValue(propertyName, out e);
            return e;
        }

        protected void ReportPropertyException(string propertyName, Exception e)
        {
            _propertyExceptions[propertyName] = e;
            OnPropertyException(new PropertyExceptionEventArgs(propertyName, e));
        }

        protected virtual void OnPropertyException(PropertyExceptionEventArgs e)
        {
            if (PropertyException != null)
            {
                PropertyException(this, e);
            }
        }

        protected static void WriteXmlAttributeIfNonNull(System.Xml.XmlWriter writer, String name, object value, Exception exception)
        {
            if (exception != null)
            {
                writer.WriteAttributeString(name + "Exception", exception.ToString());
            } else if (value != null)
            {
                writer.WriteAttributeString(name, value.ToString());
            }
        }
    }
}
