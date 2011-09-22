using System;
using CookComputing.XmlRpc;

namespace OSDBnet.Backend {

	public class ResponseBase {
		public string status;
		public double seconds;
	}

	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public class LoginResponse : ResponseBase {
		public string token;
	}

	public class SearchSubtitlesRequest {
		public string sublanguageid;
		public string moviehash;
		public string moviebytesize;
		public string imdbid;
		public string query;
	}

	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public class SearchSubtitlesResponse : ResponseBase {
		public Object data;
	}

	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public class SearchSubtitlesInfo {
		public string MatchedBy;
		public string IDSubMovieFile;
		public string MovieHash;
		public string MovieByteSize;
		public string MovieTimeMS;
		public string IDSubtitleFile;
		public string SubFileName;
		public string SubActualCD;
		public string SubSize;
		public string SubHash;
		public string IDSubtitle;
		public string UserID;
		public string SubLanguageID;
		public string SubFormat;
		public string SubSumCD;
		public string SubAuthorComment;
		public string SubAddDate;
		public string SubBad;
		public string SubRating;
		public string SubDownloadsCnt;
		public string MovieReleaseName;
		public string IDMovie;
		public string IDMovieImdb;
		public string MovieName;
		public string MovieNameEng;
		public string MovieYear;
		public string MovieImdbRating;
		public string SubFeatured;
		public string UserNickName;
		public string ISO639;
		public string LanguageName;
		public string SubComments;
		public string SubHearingImpaired;
		public string UserRank;
		public string SubDownloadLink;
		public string ZipDownloadLink;
		public string SubtitlesLink;
	}

	public struct ServerInfo {
		public string subs_downloads;
		public string movies_aka;
		public int users_loggedin;
		public int users_online_program;
		public double seconds;
		public string users_max_alltime;
		public string xmlrpc_version;
		public string movies_total;
		public string total_subtitles_languages;
		public string application;
		public string contact;
		public LastUpdate last_update_strings;
		public int users_online_total;
		public string xmlrpc_url;
		public string users_registered;
		public string subs_subtitle_files;
		public string website_url;
	}

	public struct LastUpdate {
		public string ar;
		public string bg;
		public string ca;
		public string cs;
		public string da;
		public string de;
		public string el;
		public string en;
		public string es;
		public string et;
		public string eu;
		// ....
	}
}