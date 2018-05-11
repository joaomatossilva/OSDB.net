using NUnit.Framework;

namespace OSDBnet.Tests
{
    [TestFixture]
    public class QueryTests
    {
        private const string UserAgent = "a";

        [Test]
        public void CanSearchWithSeasonAndEpisode()
        {
            var client = Osdb.Login("en", UserAgent).Result;
            var results = client.SearchSubtitlesFromQuery("en", "Arrow", 1, 1).Result;
            Assert.IsTrue(results.Count > 0);
        }

        [Test]
        public void CanSearchOnlyText()
        {
            var client = Osdb.Login("en", UserAgent).Result;
            var results = client.SearchSubtitlesFromQuery("en", "Arrow").Result;
            Assert.IsTrue(results.Count > 0);
        }
    }
}
