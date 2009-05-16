using System;
using System.Collections.Generic;
using System.Text;

using NativeMediaInfo = Panoply.Library.Native.MediaInfo;

namespace Panoply.Library.MediaInfo
{
    public class Stream
    {
        class ParameterComparer : Comparer<Parameter>
        {
            public override int Compare(Parameter x, Parameter y)
            {
                return x.Name.CompareTo(y.Name);
            }
        }

        const String INFORM_PROPERTY = "Inform";
        MediaInfo _mediaInfo;
        StreamType _type;
        uint _index;
        List<Parameter> _params;
            
        internal Stream(MediaInfo mi, StreamType type, uint index)
        {
            _mediaInfo = mi;
            _type = type;
            _index = index;
        }

        public StreamType Type { get { return _type; } }
        public uint Number { get { return _index; } }

        public IList<Parameter> Parameters
        {
            get
            {
                if (_params == null)
                {
                    List<Parameter> parameters = new List<Parameter>();

                    for (uint idx = 0; idx < _mediaInfo.GetParameterCount(_type, _index); idx++)
                    {
                        //Skip the "Inform" parameter since that's expected as a separate property
                        Parameter param = new Parameter(this, idx);
                        if (param.Name != INFORM_PROPERTY)
                        {
                            parameters.Add(param);
                        }
                    }

                    //Sort the parameters alphabetically
                    parameters.Sort(new ParameterComparer());
                    _params = parameters;
                }

                return _params.AsReadOnly();
            }
        }

        public String Inform()
        {
            return _mediaInfo.GetParameterInfo(_type,
                _index,
                "Inform",
                InfoType.Text);
        }

        internal MediaInfo MediaInfo { get { return _mediaInfo; } }

    }
}
