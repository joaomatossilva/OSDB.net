using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OSDBnet.Tests
{
    [TestClass]
    public class QueryTests
    {
        [TestMethod]
        public void CanSearchWithSeasonAndEpisode()
        {
            var client = Osdb.Login("en", ConfigurationManager.AppSettings["TestUserAgent"]);
            var results = client.SearchSubtitlesFromQuery("en", "Arrow", 1, 1);
            Assert.IsTrue(results.Count > 0);
        }

        [TestMethod]
        public void CanSearchOnlyText()
        {
            var client = Osdb.Login("en", ConfigurationManager.AppSettings["TestUserAgent"]);
            var results = client.SearchSubtitlesFromQuery("en", "Arrow");
            Assert.IsTrue(results.Count > 0);
        }
    }
}
