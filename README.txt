
 _______  _______  ______   ______     _        _______ _________
(  ___  )(  ____ \(  __  \ (  ___ \   ( (    /|(  ____ \\__   __/
| (   ) || (    \/| (  \  )| (   ) )  |  \  ( || (    \/   ) (   
| |   | || (_____ | |   ) || (__/ /   |   \ | || (__       | |   
| |   | |(_____  )| |   | ||  __ (    | (\ \) ||  __)      | |   
| |   | |      ) || |   ) || (  \ \   | | \   || (         | |   
| (___) |/\____) || (__/  )| )___) )_ | )  \  || (____/\   | |   
(_______)\_______)(______/ |/ \___/(_)|/    )_)(_______/   )_(   


TOC
1. Introduction
2. Building/Installing
3. OSDB Coverage Status
4. RoadMap
5. Version History



1. Introduction
-----------------------

OSDB.net is a .Net library to wrap the OSDB (Open Subtitles Database) protocol 
described here: http://trac.opensubtitles.org/projects/opensubtitles/wiki/OSDb


2. Building/Installing
------------------------

The fastest and cleaner way to install this library is using the NuGet tool:
PM> Install-Package OSDBnet

If you want to compile it from source, first download/fork/clone directly from 
the repository git://github.com/kappy/OSDB.net.git and you can open the OSDB.sln
file in Visual Studio 2010.


3. OSDB Coverage Status
------------------------

- Fully Supported Methods:
ServerInfo
LogOut
SearchSubtitles
CheckSubHash
CheckMovieHash
GetSubLanguages 
SearchMoviesOnIMDB
GetIMDBMovieDetails

- Half Supported Methods:
LogIn ( missing user, only anonymous supported )

- Not yet Supported:
SearchToMail
InsertMovieHash
TryUploadSubtitles
UploadSubtitles
DetectLanguage
DownloadSubtitles ( download is done directly by http)
ReportWrongMovieHash
InsertMovie
SubtitlesVote
GetComments
AddComment
AddRequest
NoOperation

- Will not be implemented (this method are program specific 'subdownloader' or 'oscar'):
GetAvailableTranslations
GetTranslation
AutoUpdate


4. RoadMap
------------------------

v1.0 - Fisrt Realease
tentative release date: N/A

For this version the library should be very well tested and should have a coverage
of the OSDB protocol of 100%

----
v0.6
tentative release date: N/A

Refactoring of internal aplication. This version should the the final official release

----
v0.5
tentative release date: N/A

Added upload and new movies insert coverage

----
v0.4
tentative release date: 2011-10-30

This version should include logged users only available methods

----
v0.3
tentative release date: 2011-10-16

This version should have better coverage of the OSDB protocol still on anonymous
public methods


5. Version History
-----------------------

v0.2 - 2011-09-29
Increased OSDB coverage on methods:
-CheckSubHash
-CheckMovieHash
-GetSubLanguages
-SearchMoviesOnImdb
-GetIMDBMovieDetails


v0.1 - 2011-09-27
First relase.
OSDB Coverage only limited to anonymous login and search subtitles from hash