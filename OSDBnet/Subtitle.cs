using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSDBnet {
	public class Subtitle {

		public string SubtitleId { get; set; }
		public string SubtitleHash { get; set; }
		public string SubtitleFileName { get; set; }

		public string MovieId { get; set; }
		public string ImdbId { get; set; }
		public string MovieName { get; set; }
		public string OriginalMovieName { get; set; }
		public int MovieYear { get; set; }

		public string LanguageId { get; set; }
		public string LanguageName { get; set; }

		public string Rating { get; set; }
		public string Bad { get; set; }

		public Uri SubTitleDownloadLink { get; set; }
		public Uri SubtitlePageLink { get; set; }
	}
}
