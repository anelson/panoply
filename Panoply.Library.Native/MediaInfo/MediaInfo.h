// Panoply.Library.Native.h

#pragma once

#include "MediaInfo\MediaInfo.h"

using namespace System;

namespace Panoply {
	namespace Library {
		namespace Native {
			namespace MediaInfo {
				public enum class StreamType 
				{
					General = MediaInfoLib::Stream_General,                 
					Video = MediaInfoLib::Stream_Video,                   
					Audio = MediaInfoLib::Stream_Audio,                   
					Text = MediaInfoLib::Stream_Text,                    
					Chapters = MediaInfoLib::Stream_Chapters,                
					Image = MediaInfoLib::Stream_Image,                   
					Menu = MediaInfoLib::Stream_Menu,                    
					Max = MediaInfoLib::Stream_Max
				};

				public enum class InfoType
				{
					Name = MediaInfoLib::Info_Name,                     
					Text = MediaInfoLib::Info_Text,                     
					Measure = MediaInfoLib::Info_Measure,               
					Options = MediaInfoLib::Info_Options,               
					NameText = MediaInfoLib::Info_Name_Text,            
					MeasureText = MediaInfoLib::Info_Measure_Text,      
					MoreInfo = MediaInfoLib::Info_Info,                 
					HowTo = MediaInfoLib::Info_HowTo,                   
					Domain = MediaInfoLib::Info_Domain,                 
					Max = MediaInfoLib::Info_Max
				};

				public ref class MediaInfo {
				private:
					MediaInfo();

				public:
					virtual ~MediaInfo();
					!MediaInfo();

					property static String^ LibraryVersion { String^ get(); }

					static void Initialize(String^ appName, String^ appVersion);

					static MediaInfo^ AttachToFile(String^ filePath);
					void Open(String^ filePath);
					String^ Inform();

					String^ GetOption(String^ option);
					void SetOption(String^ option, String^ value);

					unsigned int GetStreamCount(StreamType streamKind);
					unsigned int GetParameterCount(StreamType streamKind, unsigned int streamNumber);

					String^ GetParameterInfo(StreamType streamKind,
						unsigned int streamNumber,
						unsigned int parameterNumber,
						InfoType infoKind);

					String^ GetParameterInfo(StreamType streamKind,
						unsigned int streamNumber,
						String^ parameterName,
						InfoType infoKind);

				protected:
					virtual void Cleanup(bool disposing);

				private:
					MediaInfoLib::MediaInfo* _mediaInfo;
					bool _disposed;
					static bool _initialized;
					static String^ _mediaInfoLibVersion;
				};
			}
		}
	}
}

