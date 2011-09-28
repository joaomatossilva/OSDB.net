using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OSDBnet.Backend;
using CookComputing.XmlRpc;

namespace OSDBnet {
	public static class Osdb {

		private static IOsdb proxyInstance;
		private static IOsdb Proxy {
			get {
				if (proxyInstance == null) {
					proxyInstance = XmlRpcProxyGen.Create<IOsdb>();
				}
				return proxyInstance;
			}
		}

		public static object ServerInfo() {
			var response = Proxy.ServerInfo();
			return response;
		}

		public static IAnonymousClient Login(string userAgent) {
			var systemLanguage = GetSystemLanguage();
			return Login(systemLanguage, userAgent);
		}

		public static IAnonymousClient Login(string language, string userAgent) {
			var client = new AnonymousClient(Proxy);
			client.Login(language, userAgent);
			return client;
		}

		private static string GetSystemLanguage() {
			var currentCulture = System.Globalization.CultureInfo.CurrentUICulture;
			return currentCulture.TwoLetterISOLanguageName.ToLower();
		}
	}
}
