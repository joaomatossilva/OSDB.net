using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CookComputing.XmlRpc;

namespace OSDBnet {
	public class OSDBClient {
		public object ServerInfo() {
			IOsdb proxy = XmlRpcProxyGen.Create<IOsdb>();
			var response = proxy.ServerInfo();
			return response;
		}
	}
}
