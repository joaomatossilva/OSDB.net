using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CookComputing.XmlRpc;

namespace OSDBnet {
	[XmlRpcUrl("http://api.opensubtitles.org/xml-rpc")]
	public interface IOsdb : IXmlRpcProxy {
		[XmlRpcMethod("ServerInfo")]
		ServerInfo ServerInfo();

		[XmlRpcMethod("LogIn")]
		LoginResponse Login(string username, string password, string language, string useragent);

		[XmlRpcMethod("LogOut")]
		ResponseBase Logout(string token);

	}
}
