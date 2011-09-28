using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSDBnet {
	public interface IAnonymousClient : IDisposable {
		IList<Subtitle> SearchSubtitlesFromFile(string languages, string filename);
		IList<Subtitle> SearchSubtitlesFromImdb(string languages, string imdbId);
		IList<Subtitle> SearchSubtitlesFromQuery(string languages, string query);
		string DownloadSubtitleToPath(string path, Subtitle subtitle);
		long CheckSubHash(string subHash);
		IEnumerable<MovieInfo> CheckMovieHash(string moviehash);
	}
}
