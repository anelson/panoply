using System;
using System.Collections.Generic;
using System.Text;

using DirectShowLib;

namespace Panoply.Library.FilterGraph
{
    public class Pin
    {
        Filter _filter;
        IPin _pin;
        PinInfo _pi;
        bool _havePi = false;

        internal Pin(Filter filter, IPin pin)
        {
            _filter = filter;
            _pin = pin;
        }

        public Filter Filter { get { return _filter; } }

        public int Ordinal { get { return _filter.Pins.IndexOf(this); } }

        public String Name { get { return GetInfo().name; } }
            

        internal IPin IPin { get { return _pin; } }

        public PinDirection GetDirection()
        {
            PinDirection dir;
            int hr = _pin.QueryDirection(out dir);
            DsError.ThrowExceptionForHR(hr);

            return dir;
        }

        public Pin GetConnectedPin()
        {
            IPin connectedToPin;
            int hr = _pin.ConnectedTo(out connectedToPin);
            if (hr == Utils.Com.VFW_E_NOT_CONNECTED)
            {
                //Means pin isn't connected
                return null;
            }
            DsError.ThrowExceptionForHR(hr);

            return _filter.Graph.FindPinForIPin(connectedToPin);
        }

        private PinInfo GetInfo()
        {
            if (!_havePi)
            {
                int hr = _pin.QueryPinInfo(out _pi);
                DsError.ThrowExceptionForHR(hr);
                _havePi = true;
            }

            return _pi;
        }
    }
}
