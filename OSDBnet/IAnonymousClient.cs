using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OSDBnet
{
    public interface IAnonymousClient : IDisposable
    {
        Task<IList<Subtitle>> SearchSubtitlesFromFile(string languages, string filename);
        Task<IList<Subtitle>> SearchSubtitlesFromImdb(string languages, string imdbId);
        Task<IList<Subtitle>> SearchSubtitlesFromQuery(string languages, string query, int? season = null, int? episode = null);
        Task<string> DownloadSubtitleToPath(string path, Subtitle subtitle);
        Task<string> DownloadSubtitleToPath(string path, Subtitle subtitle, string newSubtitleName);
        Task<long> CheckSubHash(string subHash);
        Task<IEnumerable<MovieInfo>> CheckMovieHash(string moviehash);
        /*IEnumerable<Language> GetSubLanguages();
        IEnumerable<Language> GetSubLanguages(string language);
        IEnumerable<Movie> SearchMoviesOnImdb(string query);
        MovieDetails GetImdbMovieDetails(string imdbId);
        //Should this be exposed?
        void NoOperation();
        IEnumerable<UserComment> GetComments(string idSubtitle);
        string DetectLanguge(string data);
        void ReportWrongMovieHash(string idSubMovieFile);*/
    }
}
