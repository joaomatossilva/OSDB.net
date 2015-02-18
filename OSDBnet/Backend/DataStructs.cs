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
        public string sublanguageid = string.Empty;
        public string moviehash = string.Empty;
        public string moviebytesize = string.Empty;
        public string imdbid = string.Empty;
        public string query = string.Empty;
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

    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public class CheckSubHashResponse : ResponseBase {
        public Object data;
    }

    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public class CheckMovieHashResponse : ResponseBase {
        public Object data;
    }

    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public class CheckMovieHashInfo {
        public string MovieHash;
        public string MovieImdbID;
        public string SeenCount;
        public string MovieYear;
        public string MovieName;
    }

    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public class GetSubLanguagesResponse : ResponseBase {
        public GetSubLanguagesInfo[] data;
    }

    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public class GetSubLanguagesInfo {
        public string SubLanguageID;
        public string LanguageName;
        public string ISO639;
    }

    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public class SearchMoviesOnIMDBResponse : ResponseBase {
        public MoviesOnIMDBInfo[] data;
    }

    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public class MoviesOnIMDBInfo {
        public string id;
        public string title;
    }

    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public class GetIMDBMovieDetailsResponse : ResponseBase {
        public IMDBMovieDetails data;
    }

    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public class IMDBMovieDetails {
        public object cast;
        public object writers;
        public string trivia;
        public string[] genres;
        public string[] country;
        public string[] language;
        public object directors;
        public string duration;
        public string tagline;
        public string rating;
        public string cover;
        public string id;
        public string votes;
        public string title;
        public string[] aka;
        public string year;
    }

    public class ImdbPerson {
        public string id;
        public string name;
    }

    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public class GetCommentsResponse : ResponseBase {
        public object data;
    }

    public class CommentsData {
        public string IDSubtitle;
        public string UserID;
        public string UserNickName;
        public string Comment;
        public string Created;
    }

    [XmlRpcMissingMapping(MappingAction.Ignore)]
    public class DetectLanguageResponse : ResponseBase {
        public object data;
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