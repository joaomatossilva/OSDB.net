using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CookComputing.XmlRpc;

namespace OSDBnet.Backend {
	[XmlRpcUrl("http://api.opensubtitles.org/xml-rpc")]
	public interface IOsdb : IXmlRpcProxy {
		[XmlRpcMethod("ServerInfo")]
		ServerInfo ServerInfo();

		[XmlRpcMethod("LogIn")]
		LoginResponse Login(string username, string password, string language, string useragent);

		[XmlRpcMethod("LogOut")]
		ResponseBase Logout(string token);

		[XmlRpcMethod("SearchSubtitles")]
		SearchSubtitlesResponse SearchSubtitles(string token, SearchSubtitlesRequest[] request);

		[XmlRpcMethod("CheckSubHash")]
		CheckSubHashResponse CheckSubHash(string token, string[] subhashes);
	}
}
