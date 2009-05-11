using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

using NativeMediaInfo = Panoply.Library.Native.MediaInfo;

namespace Panoply.Library.MediaInfo
{
	public enum StreamType 
	{
		General = NativeMediaInfo.StreamType.General,
        Video = NativeMediaInfo.StreamType.Video,
        Audio = NativeMediaInfo.StreamType.Audio,
        Text = NativeMediaInfo.StreamType.Text,
        Chapters = NativeMediaInfo.StreamType.Chapters,
        Image = NativeMediaInfo.StreamType.Image,
        Menu = NativeMediaInfo.StreamType.Menu,
        Max = NativeMediaInfo.StreamType.Max
	};

	public enum InfoType
	{
		Name = NativeMediaInfo.InfoType.Name,
        Text = NativeMediaInfo.InfoType.Text,
        Measure = NativeMediaInfo.InfoType.Measure,
        Options = NativeMediaInfo.InfoType.Options,
        NameText = NativeMediaInfo.InfoType.NameText,
        MeasureText = NativeMediaInfo.InfoType.MeasureText,
        MoreInfo = NativeMediaInfo.InfoType.MoreInfo,
        HowTo = NativeMediaInfo.InfoType.HowTo,
        Domain = NativeMediaInfo.InfoType.Domain,
        Max = NativeMediaInfo.InfoType.Max
	};
    public class MediaInfo : IDisposable
    {
        NativeMediaInfo.MediaInfo _mediaInfo;
        Dictionary<StreamType, List<Stream>> _streamsByType = new Dictionary<StreamType, List<Stream>>();

        static MediaInfo()
        {
            NativeMediaInfo.MediaInfo.Initialize(
                Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                Assembly.GetExecutingAssembly().GetName().FullName);
        }

        private MediaInfo(NativeMediaInfo.MediaInfo mediaInfo)
        {
            _mediaInfo = mediaInfo;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (_mediaInfo != null)
            {
                _mediaInfo.Dispose();
                _mediaInfo = null;
            }
        }

        #endregion

        public IList<Stream> GetStreams(StreamType type)
        {
            if (!_streamsByType.ContainsKey(type))
            {
                uint numStreams = _mediaInfo.GetStreamCount((NativeMediaInfo.StreamType)type);

                List<Stream> streams = new List<Stream>((int)numStreams);

                for (uint idx = 0; idx < numStreams; idx++)
                {
                    streams.Add(new Stream(this, type, idx));
                }

                _streamsByType.Add(type, streams);
            }

            return _streamsByType[type].AsReadOnly();
        }

        public static String LibraryVersion { get { return NativeMediaInfo.MediaInfo.LibraryVersion; } }

        public static MediaInfo Open(String filePath)
        {
            MediaInfo mi = new MediaInfo(NativeMediaInfo.MediaInfo.AttachToFile(filePath));

            return mi;
        }

        public String Inform()
        {
            return _mediaInfo.Inform();
        }

        public String GetOption(String option)
        {
            return _mediaInfo.GetOption(option);
        }

        public void SetOption(String option, String value)
        {
            _mediaInfo.SetOption(option, value);
        }

        internal uint GetParameterCount(StreamType streamType,
            uint streamNumber)
        {
            return _mediaInfo.GetParameterCount((NativeMediaInfo.StreamType)streamType,
                streamNumber);
        }

        internal String GetParameterInfo(StreamType streamType, uint streamNumber, uint parameterNumber, InfoType infoType)
        {
            return _mediaInfo.GetParameterInfo((NativeMediaInfo.StreamType)streamType,
                streamNumber,
                parameterNumber,
                (NativeMediaInfo.InfoType)infoType);
        }

        internal String GetParameterInfo(StreamType streamType, uint streamNumber, String parameterName, InfoType infoType)
        {
            return _mediaInfo.GetParameterInfo((NativeMediaInfo.StreamType)streamType,
                streamNumber,
                parameterName,
                (NativeMediaInfo.InfoType)infoType);
        }
    }
}
