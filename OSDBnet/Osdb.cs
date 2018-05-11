namespace OSDBnet
{
    public static class Osdb
    {
        public static IOsdbClient Create(string userAgent)
        {
            var client = new OsdbClient(userAgent);
            return client;
        }

        private static string GetSystemLanguage()
        {
            var currentCulture = System.Globalization.CultureInfo.CurrentUICulture;
            return currentCulture.TwoLetterISOLanguageName.ToLower();
        }
    }
}
