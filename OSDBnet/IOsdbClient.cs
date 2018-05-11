using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSDBnet
{
    public interface IOsdbClient : IDisposable
    {
        Task<IList<Subtitle>> SearchSubtitlesFromFile(string languages, string filename);
        Task<IList<Subtitle>> SearchSubtitlesFromHash(string languages, string fileHash, long moviebytesize);
        Task<IList<Subtitle>> SearchSubtitlesFromImdb(string languages, string imdbId);
        Task<IList<Subtitle>> SearchSubtitlesFromQuery(string languages, string query, int? season = null, int? episode = null);
        Task<string> DownloadSubtitleToPath(string path, Subtitle subtitle);
        Task<string> DownloadSubtitleToPath(string path, Subtitle subtitle, string newSubtitleName);
    }
}
