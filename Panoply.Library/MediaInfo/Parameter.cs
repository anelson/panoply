using System;
using System.Collections.Generic;
using System.Text;

namespace Panoply.Library.MediaInfo
{
    public class Parameter
    {
        Stream _stream;
        uint _index;

        internal Parameter(Stream stream, uint index)
        {
            _stream = stream;
            _index = index;
        }

        public uint Number { get { return _index; } }

        public String Name { get { return GetInfo(InfoType.Name); } }
        public String LocalizedName { get { return GetInfo(InfoType.NameText); } }
        
        public String Units { get { return GetInfo(InfoType.Measure); } }
        public String LocalizedUnits { get { return GetInfo(InfoType.MeasureText); } }

        public String Value { get { return GetInfo(InfoType.Text); } }

        public String GetInfo(InfoType type)
        {
            return _stream.MediaInfo.GetParameterInfo(_stream.Type,
                _stream.Number,
                _index,
                type);
        }
    }
}
