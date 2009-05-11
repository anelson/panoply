using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using DirectShowLib;

namespace Panoply.Library.Filters
{
    public class FilterCategory : FilterDevice
    {
        static Guid CLSID_ActiveMovieCategories = new Guid("DA4E3DA0-D07D-11d0-BD50-00A0C911CE86");
        List<Filter> _filters;

        public static List<FilterCategory> EnumerateFilterCategories()
        {
            List<FilterCategory> filterCategories = new List<FilterCategory>();

            DsDevice[] devices = DsDevice.GetDevicesOfCat(CLSID_ActiveMovieCategories);
            foreach (DsDevice device in devices)
            {
                filterCategories.Add(new FilterCategory(device));
            }

            return filterCategories;
        }

        private FilterCategory(DsDevice device) : base(device)
        {
        }

        public IList<Filter> Filters
        {
            get
            {
                if (_filters == null)
                {
                    _filters = EnumerateFilters();
                }

                return _filters.AsReadOnly();
            }
        }

        /// <summary>
        /// Called by a Filter when it has been unregistered
        /// </summary>
        /// <param name="filter"></param>
        internal void FilterUnregistered(Filter filter) {
            _filters.Remove(filter);
        }

        protected override Merit GetMerit()
        {
            try
            {
                Object merit = GetValueFromMoniker("Merit");
                if (merit is int)
                {
                    return (Merit)merit;
                }
                else if (merit is byte[])
                {
                    //At least one filter category, "WDM Stream Decompression Devices" on my x64 Vista Ultimate machine,
                    //returns merit as a four-byte array.  Weak!
                    GCHandle handle = GCHandle.Alloc(merit, GCHandleType.Pinned);
                    try
                    {
                        int meritInt = Marshal.ReadInt32(handle.AddrOfPinnedObject());
                        return (Merit)meritInt;
                    }
                    finally
                    {
                        handle.Free();
                    }
                }
                else
                {
                    return Merit.None;
                }
            }
            catch (COMException)
            {
                return Merit.None;
            }
        }

        private List<Filter> EnumerateFilters()
        {
            List<Filter> filters = new List<Filter>();
            DsDevice[] devices = DsDevice.GetDevicesOfCat(Clsid);
            foreach (DsDevice device in devices)
            {
                //In empirical experience, some filters don't have a friendlyname, and also are not usable for the purposes
                //we're interested in, so ignore
                if (!String.IsNullOrEmpty(device.Name))
                {
                    filters.Add(new Filter(this, device));
                }
            }

            return filters;
        }
    }
}
