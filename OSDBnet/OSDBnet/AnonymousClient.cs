using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSDBnet {
	public class AnonymousClient : IAnonymousClient, IDisposable {

		private bool disposed = false;
		private const string USERAGENT = "OS Test User Agent";

		protected readonly IOsdb proxy;
		protected string token;

		internal AnonymousClient(IOsdb proxy) {
			this.proxy = proxy;
		}

		internal void Login(string language) {
			LoginResponse response = proxy.Login(string.Empty, string.Empty, language, USERAGENT);
			VerifyResponseCode(response);
			token = response.token;
		}

		public void  Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing) {
			if (!disposed) {
				if (disposing && !string.IsNullOrEmpty(token)) {
					try {
						proxy.Logout(token);
					} catch {
						//soak it. We don't want exception on disposing. It's better to let the session timeout.
					}
					token = null;
				}
				disposed = true;
			}
		}

		~AnonymousClient() {
			Dispose(false);
		}

		protected static void VerifyResponseCode(ResponseBase response) {
			if (null == response) {
				throw new ArgumentNullException("response");
			}
			if (string.IsNullOrEmpty(response.status)) {
				throw new ArgumentException("parameter response.status cannot be null", "response");
			}

			int responseCode = int.Parse(response.status.Substring(0,3));
			if (responseCode >= 400) {
				//TODO: Create Exception type
				throw new Exception(string.Format("Unexpected error response {1}", responseCode, response.status));
			}
		}
	}
}
