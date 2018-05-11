using System;
using System.Threading.Tasks;
using Spooky.XmlRpc;

namespace OSDBnet
{
    public static class Osdb
    {

        private static XmlRpcHttpClient proxyInstance;
        private static XmlRpcHttpClient Proxy
        {
            get
            {
                if (proxyInstance == null)
                {
                    proxyInstance = new XmlRpcHttpClient(new Uri("http://api.opensubtitles.org/xml-rpc"));
                }
                return proxyInstance;
            }
        }

        public static object ServerInfo()
        {
            var response = Proxy.Invoke<object>("ServerInfo");
            return response;
        }

        public static async Task<IAnonymousClient> Login(string userAgent)
        {
            var systemLanguage = GetSystemLanguage();
            return await Login(systemLanguage, userAgent)
                .ConfigureAwait(false);
        }

        public static async Task<IAnonymousClient> Login(string language, string userAgent)
        {
            var client = new AnonymousClient(Proxy);
            await client.Login(string.Empty, string.Empty, language, userAgent)
                .ConfigureAwait(false);
            return client;
        }

        public static async Task<IAnonymousClient> Login(string username, string password, string language, string userAgent)
        {
            var client = new AnonymousClient(Proxy);
            await client.Login(username, password, language, userAgent)
                .ConfigureAwait(false);
            return client;
        }

        private static string GetSystemLanguage()
        {
            var currentCulture = System.Globalization.CultureInfo.CurrentUICulture;
            return currentCulture.TwoLetterISOLanguageName.ToLower();
        }
    }
}
