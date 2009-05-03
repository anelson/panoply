using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;

using DirectShowLib;

namespace Panoply.Library.Filters
{
    public abstract class FilterDevice
    {
        DsDevice _device;

        internal FilterDevice(DsDevice device)
        {
            _device = device;
        }

        public String FriendlyName { get { return _device.Name; } }

        public String DevicePath { get { return _device.DevicePath; } }

        public Merit Merit { get { return GetMerit(); } }

        public Guid Clsid { get { return GetClsid(); } }

        protected DsDevice DirectShowDevice { get { return _device; } }

        protected Object GetValueFromMoniker(String name)
        {
            IPropertyBag bag = null;
            object bagObj = null;
            object val = null;

            try
            {
                Guid bagId = typeof(IPropertyBag).GUID;
                _device.Mon.BindToStorage(null, null, ref bagId, out bagObj);

                bag = (IPropertyBag)bagObj;

                int hr = bag.Read(name, out val, null);
                DsError.ThrowExceptionForHR(hr);

                return val;
            }
            finally
            {
                bag = null;
                if (bagObj != null)
                {
                    Marshal.ReleaseComObject(bagObj);
                    bagObj = null;
                }
            }
        }

        protected abstract Merit GetMerit();

        private Guid GetClsid()
        {
            try
            {
                return new Guid((String)GetValueFromMoniker("CLSID"));
            }
            catch (COMException e)
            {
                if (e.ErrorCode == Utils.Com.E_PROP_ID_UNSUPPORTED)
                {
                    //This shouldn't happen, but at least one codec (WMAudio DMO) on my x64 vista ultimate box
                    //throws this error when you query for CLSID
                    return Guid.Empty;
                }

                throw;
            }
        }
    }
}
