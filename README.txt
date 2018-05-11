
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



1. Introduction
-----------------------

OSDB.net is a .Net library to wrap the OSDB (Open Subtitles Database) protocol 
described here: https://forum.opensubtitles.org/viewtopic.php?f=8&t=16453&sid=f821df068da21f743a0e89a36827e8a1


2. Building/Installing
------------------------

The fastest and cleaner way to install this library is using the NuGet tool:
PM> Install-Package OSDBnet

If you want to compile it from source, first download/fork/clone directly from 
the repository git://github.com/joaomatossilva/OSDB.net.git and you can open the OSDBnet.sln
file in Visual Studio.


3. OSDB Coverage Status
------------------------

Only the REST api is supported. For the legacy xmlrpc version see the 0.4.0 version
(https://www.nuget.org/packages/OSDBnet/0.4.0)


