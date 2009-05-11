#
# spec file for package libmatroska for (at least) SuSE 9.0, 9.1
#
# Copyright (c) 2004 SUSE LINUX AG, Nuernberg, Germany.
# This file and all modifications and additions to the pristine
# package are under the same license as the package itself.
#
# Please submit bugfixes or comments via http://www.suse.de/feedback/
#

# neededforbuild  gcc-c++ libebml libstdc++-devel

BuildRequires: aaa_base acl attr bash bind-utils bison bzip2 coreutils cpio cpp cracklib db devs diffutils file filesystem fillup findutils flex gawk glibc glibc-devel glibc-locale gpm grep groff gzip info less libebml libgcc libstdc++ libxcrypt m4 make man mktemp ncurses net-tools netcfg openldap2-client openssl pam pam-modules patch permissions popt readline sed syslogd sysvinit tar timezone util-linux vim zlib zlib-devel autoconf automake binutils gcc gcc-c++ gdbm gettext libebml libstdc++-devel libtool perl rpm

Name:         libmatroska
URL:          http://dl.matroska.org/downloads/libmatroska/
Version: 0.7.8
Release: 1
Summary:      library to deal with matroska files.
License:      LGPL
Group:        Development/Libraries/Other
Source:       %{name}-%{version}.tar.bz2
Patch:        suse-libmatroska-makefile.diff
Summary:      library to deal with matroska files.
BuildRoot:    %{_tmppath}/%{name}-%{version}-build
Prefix:       /usr

%description
libmatroska is a C++ libary to parse Matroska files (.mkv and .mka). It
depends on libebml to work. You only need this package to compile your
own applications.



Authors:
--------
    Steve Lhomme <steve.lhomme@free.fr>
    Moritz Bunkus <moritz@bunkus.org>

%prep
rm -rf $RPM_BUILD_ROOT
%setup
%patch -p1

%build
export CFLAGS="$RPM_OPT_FLAGS"
cd make/linux
make PREFIX=$RPM_BUILD_ROOT/usr LIBDIR=%{_libdir} staticlib

%install
cd make/linux
make install_staticlib install_headers PREFIX=$RPM_BUILD_ROOT/usr LIBDIR=$RPM_BUILD_ROOT/%{_libdir}

%clean
rm -rf $RPM_BUILD_ROOT

%post
%run_ldconfig

%postun
%run_ldconfig

%files
%defattr (-,root,root)
%{_libdir}/libmatroska.a
/usr/include/matroska
# /usr/include/ebml/*

%changelog -n libmatroska
* Sat Apr 16 2005 - moritz@bunkus.org
- updated for the new libmatroska build targets
* Thu Sep 02 2004 - seife@suse.de
- add libebml to neededforbuild
* Wed Sep 01 2004 - seife@suse.de
- initial submission
