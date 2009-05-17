using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using DirectShowLib;

namespace Panoply.Library.FilterGraph
{
    public class Filter
    {
        Graph _graph;
        IBaseFilter _filter;
        IFileSourceFilter _fileSourceFilter;
        String _fileSourceFilterFileName;
        List<Pin> _pins;
        FilterInfo _fi;
        bool _haveFi = false;

        internal Filter(Graph graph, IBaseFilter filter)
        {
            _graph = graph;
            _filter = filter;
            _fileSourceFilter = filter as IFileSourceFilter;
        }

        public Graph Graph { get { return _graph; } }

        public IList<Pin> Pins
        {
            get
            {
                if (_pins == null)
                {
                    GetPins();
                }

                return _pins.AsReadOnly();
            }
        }

        /// <summary>
        /// A name suitable for displaying to the user.  For most filters it's the filter name,
        /// but for file source filters which represent the source of a file, it's the file name 
        /// portion of the file path
        /// </summary>
        public String DisplayName
        {
            get
            {
                if (IsFileSourceFilter)
                {
                    return System.IO.Path.GetFileName(FileName);
                }
                else
                {
                    return Name;
                }
            }
        }


        public String Name
        {
            get
            {
                return GetInfo().achName;
            }
        }

        public bool IsFileSourceFilter { get { return _fileSourceFilter != null; } }
        public String FileName
        {
            get
            {
                if (_fileSourceFilterFileName == null)
                {
                    if (_fileSourceFilter != null)
                    {
                        int hr = _fileSourceFilter.GetCurFile(out _fileSourceFilterFileName, null);
                        DsError.ThrowExceptionForHR(hr);
                    }
                }

                return _fileSourceFilterFileName;
            }
        }

        /// <summary>
        /// Finds all pins in a given direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public IList<Pin> GetPins(PinDirection direction)
        {
            List<Pin> pins = new List<Pin>();
            foreach (Pin pin in Pins)
            {
                if (pin.GetDirection() == direction)
                {
                    pins.Add(pin);
                }
            }

            return pins.AsReadOnly();
        }

        private FilterInfo GetInfo()
        {
            if (!_haveFi)
            {
                int hr = _filter.QueryFilterInfo(out _fi);
                DsError.ThrowExceptionForHR(hr);
                _haveFi = true;
            }

            return _fi;
        }

        private void GetPins()
        {
            List<Pin> pins = new List<Pin>();

            IEnumPins enumPins;
            int hr = _filter.EnumPins(out enumPins);
            DsError.ThrowExceptionForHR(hr);

            IPin[] ipins = new IPin[1];
            IntPtr count = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(ulong)));

            try
            {
                while ((hr = enumPins.Next(1, ipins, count)) == 0)
                {
                    IPin pin = ipins[0];

                    pins.Add(new Pin(this, pin));
                }
            }
            finally
            {
                Marshal.FreeCoTaskMem(count);
                count = IntPtr.Zero;
            }

            _pins = pins;
        }
    }
}
