using NUnit.Framework;

namespace OSDBnet.Tests
{
    [TestFixture]
    public class QueryTests
    {

        private const string UserAgent = "TemporaryUserAgent";

        [Test]
        public void CanSearchWithSeasonAndEpisode()
        {
            var client = Osdb.Create(UserAgent);
            var results = client.SearchSubtitlesFromQuery("en", "arrow", 1, 1).Result;
            Assert.IsTrue(results.Count > 0);
        }

        [Test]
        public void CanSearchOnlyText()
        {
            var client = Osdb.Create(UserAgent);
            var results = client.SearchSubtitlesFromQuery("en", "Arrow").Result;
            Assert.IsTrue(results.Count > 0);
        }
    }
}
