using System;
using Newtonsoft.Json;

namespace OSDBnet
{
    public class Subtitle
    {
        [JsonProperty("IDSubtitle")]
        public string SubtitleId { get; set; }

        [JsonProperty("SubHash")]
        public string SubtitleHash { get; set; }

        [JsonProperty("SubFileName")]
        public string SubtitleFileName { get; set; }

        [JsonProperty("IDMovie")]
        public string MovieId { get; set; }

        [JsonProperty("IDMovieImdb")]
        public string ImdbId { get; set; }

        [JsonProperty("MovieName")]
        public string MovieName { get; set; }

        [JsonProperty("MovieNameEng")]
        public string OriginalMovieName { get; set; }

        [JsonProperty("MovieYear")]
        public int MovieYear { get; set; }

        [JsonProperty("SubLanguageID")]
        public string LanguageId { get; set; }

        [JsonProperty("LanguageName")]
        public string LanguageName { get; set; }

        [JsonProperty("SubRating")]
        public string Rating { get; set; }

        [JsonProperty("SubBad")]
        public string Bad { get; set; }

        [JsonProperty("SubDownloadLink")]
        public Uri SubTitleDownloadLink { get; set; }

        [JsonProperty("SubtitlesLink")]
        public Uri SubtitlePageLink { get; set; }
    }
}
