#include "stdafx.h"

#include <vcclr.h>

#include ".\MediaInfo.h"

#define MEDIAINFO_LIB_VERSION		L"0.7.15" //The version of MediaInfoLib this is compiled against

using namespace Panoply::Library::Native::MediaInfo;

static MediaInfoLib::String ClrStringToStlWstring(String^ clrString) {
	if (clrString == nullptr) {
		return L"";
	}

	pin_ptr<const wchar_t> clrStringBuffer = ::PtrToStringChars(clrString);
	
	return MediaInfoLib::String(clrStringBuffer);
}

MediaInfo::MediaInfo() {
	_mediaInfo = new MediaInfoLib::MediaInfo();
}

MediaInfo::~MediaInfo() {
	Cleanup(true);
}

MediaInfo::!MediaInfo() {
	Cleanup(false);
	GC::SuppressFinalize(this);
}

String^ MediaInfo::LibraryVersion::get() {
	if (!_initialized) {
		throw gcnew InvalidOperationException(L"MediaInfo hasn't been initialized yet");
	}

	return _mediaInfoLibVersion;
}

void MediaInfo::Initialize(String^ appName, String^ appVersion) {
	if (!_initialized) {
		String^ versionString = String::Format(L"{0};{1};{2}",
			MEDIAINFO_LIB_VERSION,
			appName,
			appVersion);

		MediaInfoLib::String mediaInfoVersion = MediaInfoLib::MediaInfo::Option_Static(L"Info_Version",
			::ClrStringToStlWstring(versionString));

		if (mediaInfoVersion == L"") {
			throw gcnew ApplicationException(L"This library is not compatible with the version of mediainfolib being used");
		}

		_mediaInfoLibVersion = gcnew String(mediaInfoVersion.c_str());

		_initialized = true;
	}
}

MediaInfo^ MediaInfo::AttachToFile(String^ filePath) {
	if (!_initialized) {
		throw gcnew InvalidOperationException(L"MediaInfo hasn't been initialized yet");
	}

	MediaInfo^ mi = gcnew MediaInfo();

	try {
		mi->Open(filePath);
	} catch (...) {
		delete mi;
		throw;
	}

	return mi;
}

void MediaInfo::Open(String^ filePath) {
	if (!_mediaInfo->Open(::ClrStringToStlWstring(filePath))) {
		throw gcnew ApplicationException(
			String::Format(L"Unable to open file '{0}'", filePath)
			);
	}
}

String^ MediaInfo::Inform() {
	return gcnew String(
		_mediaInfo->Inform().c_str()
		);
}

String^ MediaInfo::GetOption(String^ option) {
	return gcnew String(_mediaInfo->Option(::ClrStringToStlWstring(option)).c_str());
}

void MediaInfo::SetOption(String^ option, String^ value) {
	_mediaInfo->Option(
		::ClrStringToStlWstring(option),
		::ClrStringToStlWstring(value)
		);
}

unsigned int MediaInfo::GetStreamCount(StreamType streamKind) {
	return static_cast<unsigned int>(_mediaInfo->Count_Get(static_cast<MediaInfoLib::stream_t>(streamKind)));
}

unsigned int MediaInfo::GetParameterCount(StreamType streamKind, unsigned int streamNumber) {
	return static_cast<unsigned int>(_mediaInfo->Count_Get(static_cast<MediaInfoLib::stream_t>(streamKind),
		streamNumber));
}

String^ MediaInfo::GetParameterInfo(StreamType streamKind,
	unsigned int streamNumber,
	unsigned int parameterNumber,
	InfoType infoKind) {
	return gcnew String(
		_mediaInfo->Get(
			static_cast<MediaInfoLib::stream_t>(streamKind),
			streamNumber,
			parameterNumber,
			static_cast<MediaInfoLib::info_t>(infoKind)
			).c_str()
		);
}

String^ MediaInfo::GetParameterInfo(StreamType streamKind,
	unsigned int streamNumber,
	String^ parameterName,
	InfoType infoKind) {
	return gcnew String(
		_mediaInfo->Get(
			static_cast<MediaInfoLib::stream_t>(streamKind),
			streamNumber,
			::ClrStringToStlWstring(parameterName),
			static_cast<MediaInfoLib::info_t>(infoKind)
			).c_str()
		);
}


void MediaInfo::Cleanup(bool disposing) {
	disposing;

	if (!_disposed) {
		if (_mediaInfo) {
			delete _mediaInfo;
			_mediaInfo = NULL;
		}

		_disposed = true;
	}
}

