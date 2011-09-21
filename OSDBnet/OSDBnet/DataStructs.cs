using System;
using CookComputing.XmlRpc;

namespace OSDBnet {

	public class ResponseBase {
		public string status;
		public double seconds;
	}

	[XmlRpcMissingMapping(MappingAction.Ignore)]
	public class LoginResponse : ResponseBase {
		public string token;
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