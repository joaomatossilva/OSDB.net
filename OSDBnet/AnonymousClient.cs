using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Net;
using OSDBnet.Backend;
using Spooky.XmlRpc;
using System.Threading.Tasks;
using System.Net.Http;

namespace OSDBnet
{
    public class AnonymousClient : IAnonymousClient, IDisposable
    {

        private bool disposed = false;

        protected readonly XmlRpcHttpClient proxy;
        protected string token;

        internal AnonymousClient(XmlRpcHttpClient proxy)
        {
            this.proxy = proxy;
        }

        internal async Task Login(string username, string password, string language, string userAgent)
        {
            var response = await proxy.Invoke<object>("Login", username, password, language, userAgent)
                .ConfigureAwait(false);
            //VerifyResponseCode(response);
            //token = response.token;
            token = String.Empty;
        }

        public Task<IList<Subtitle>> SearchSubtitlesFromFile(string languages, string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                throw new ArgumentNullException("filename");
            }
            FileInfo file = new FileInfo(filename);
            if (!file.Exists)
            {
                throw new ArgumentException("File doesn't exist", "filename");
            }
            var request = new SearchSubtitlesRequest { sublanguageid = languages };
            request.moviehash = HashHelper.ToHexadecimal(HashHelper.ComputeMovieHash(filename));
            request.moviebytesize = file.Length.ToString();

            request.imdbid = string.Empty;
            request.query = string.Empty;

            return SearchSubtitlesInternal(request);
        }

        public Task<IList<Subtitle>> SearchSubtitlesFromImdb(string languages, string imdbId)
        {
            if (string.IsNullOrEmpty(imdbId))
            {
                throw new ArgumentNullException("imdbId");
            }
            var request = new SearchSubtitlesRequest
            {
                sublanguageid = languages,
                imdbid = imdbId
            };
            return SearchSubtitlesInternal(request);
        }

        public Task<IList<Subtitle>> SearchSubtitlesFromQuery(string languages, string query, int? season = null, int? episode = null)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentNullException("query");
            }
            var request = new SearchSubtitlesRequest
            {
                sublanguageid = languages,
                query = query,
                season = season,
                episode = episode
            };
            return SearchSubtitlesInternal(request);
        }

        private async Task<IList<Subtitle>> SearchSubtitlesInternal(SearchSubtitlesRequest request)
        {
            var response = await proxy.Invoke<SearchSubtitlesResponse>("SearchSubtitles", token, new SearchSubtitlesRequest[] { request })
                .ConfigureAwait(false);
            VerifyResponseCode(response);

            var subtitles = new List<Subtitle>();

            var subtitlesInfo = response.data as object[];
            if (null != subtitlesInfo)
            {
                foreach (var infoObject in subtitlesInfo)
                {
                    //var subInfo = SimpleObjectMapper.MapToObject<SearchSubtitlesInfo>((XmlRpcStruct)infoObject);
                    //subtitles.Add(BuildSubtitleObject(subInfo));
                }
            }
            return subtitles;
        }

        public Task<string> DownloadSubtitleToPath(string path, Subtitle subtitle)
        {
            return DownloadSubtitleToPath(path, subtitle, null);
        }

        public async Task<string> DownloadSubtitleToPath(string path, Subtitle subtitle, string newSubtitleName)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }
            if (null == subtitle)
            {
                throw new ArgumentNullException("subtitle");
            }
            if (!Directory.Exists(path))
            {
                throw new ArgumentException("path should point to a valid location");
            }

            string destinationfile = Path.Combine(path, (string.IsNullOrEmpty(newSubtitleName)) ? subtitle.SubtitleFileName : newSubtitleName);
            var client = new HttpClient();
            var zipStream = await client.GetStreamAsync(subtitle.SubTitleDownloadLink)
                .ConfigureAwait(false);
            await UnZipSubtitleFileToFile(zipStream, destinationfile)
                .ConfigureAwait(false);

            return destinationfile;
        }

        public async Task<long> CheckSubHash(string subHash)
        {
            var response = await proxy.Invoke<CheckSubHashResponse>("CheckSubHash", token, new string[] { subHash });
            VerifyResponseCode(response);

            long idSubtitleFile = 0;
            //var hashInfo = response.data as XmlRpcStruct;
            //if (null != hashInfo && hashInfo.ContainsKey(subHash))
            //{
            //    idSubtitleFile = Convert.ToInt64(hashInfo[subHash]);
            //}

            return idSubtitleFile;
        }

        public async Task<IEnumerable<MovieInfo>> CheckMovieHash(string moviehash)
        {
            var response = await proxy.Invoke<CheckMovieHashResponse>("CheckMovieHash", token, new string[] { moviehash })
                .ConfigureAwait(false);
            VerifyResponseCode(response);

            var movieInfoList = new List<MovieInfo>();

            //var hashInfo = response.data as XmlRpcStruct;
            //if (null != hashInfo && hashInfo.ContainsKey(moviehash))
            //{
            //    var movieInfoArray = hashInfo[moviehash] as object[];
            //    foreach (XmlRpcStruct movieInfoStruct in movieInfoArray)
            //    {
            //        var movieInfo = SimpleObjectMapper.MapToObject<CheckMovieHashInfo>(movieInfoStruct);
            //        movieInfoList.Add(BuildMovieInfoObject(movieInfo));
            //    }
            //}

            return movieInfoList;
        }
        /*
        public IEnumerable<Language> GetSubLanguages()
        {
            //get system language
            return GetSubLanguages("en");
        }
        
        public IEnumerable<Language> GetSubLanguages(string language)
        {
            var response = proxy.GetSubLanguages(language);
            VerifyResponseCode(response);

            IList<Language> languages = new List<Language>();
            foreach (var languageInfo in response.data)
            {
                languages.Add(BuildLanguageObject(languageInfo));
            }
            return languages;
        }

        public IEnumerable<Movie> SearchMoviesOnImdb(string query)
        {
            var response = proxy.SearchMoviesOnIMDB(token, query);
            VerifyResponseCode(response);

            IList<Movie> movies = new List<Movie>();

            if (response.data.Length == 1 && string.IsNullOrEmpty(response.data.First().id))
            {
                // no match found
                return movies;
            }

            foreach (var movieInfo in response.data)
            {
                movies.Add(BuildMovieObject(movieInfo));
            }
            return movies;
        }

        public MovieDetails GetImdbMovieDetails(string imdbId)
        {
            var response = proxy.GetIMDBMovieDetails(token, imdbId);
            VerifyResponseCode(response);

            var movieDetails = BuildMovieDetailsObject(response.data);
            return movieDetails;
        }

        public void NoOperation()
        {
            var response = proxy.NoOperation(token);
            VerifyResponseCode(response);
        }

        public IEnumerable<UserComment> GetComments(string idsubtitle)
        {
            var response = proxy.GetComments(token, new string[] { idsubtitle });
            VerifyResponseCode(response);

            var comments = new List<UserComment>();
            var commentsStruct = response.data as XmlRpcStruct;
            if (commentsStruct == null)
                return comments;

            if (commentsStruct.ContainsKey("_" + idsubtitle))
            {
                object[] commentsList = commentsStruct["_" + idsubtitle] as object[];
                if (commentsList != null)
                {
                    foreach (XmlRpcStruct commentStruct in commentsList)
                    {
                        var comment = SimpleObjectMapper.MapToObject<CommentsData>(commentStruct);
                        comments.Add(BuildUserCommentObject(comment));
                    }
                }
            }

            return comments;
        }

        public string DetectLanguge(string data)
        {
            var bytes = GzipString(data);
            var text = Convert.ToBase64String(bytes);

            var response = proxy.DetectLanguage(token, new string[] { text });
            VerifyResponseCode(response);

            var languagesStruct = response.data as XmlRpcStruct;
            if (languagesStruct == null)
                return null;

            foreach (string key in languagesStruct.Keys)
            {
                return languagesStruct[key].ToString();
            }
            return null;
        }

        public void ReportWrongMovieHash(string idSubMovieFile)
        {
            var response = proxy.ReportWrongMovieHash(token, idSubMovieFile);
            VerifyResponseCode(response);
        }
        */
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing && !string.IsNullOrEmpty(token))
                {
                    try
                    {
                        proxy.Invoke<object>("Logout", token);
                    }
                    catch
                    {
                        //soak it. We don't want exception on disposing. It's better to let the session timeout.
                    }
                    token = null;
                }
                disposed = true;
            }
        }

        ~AnonymousClient()
        {
            Dispose(false);
        }

        protected static string Base64Encode(string str)
        {
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(encbuff);
        }
        protected static string Base64Decode(string str)
        {
            byte[] decbuff = Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(decbuff);
        }

        protected static byte[] GzipString(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    msi.CopyTo(gs);
                }

                return mso.ToArray();
            }
        }

        protected static string GUnzipString(byte[] gzippedString)
        {
            using (var msi = new MemoryStream(gzippedString))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    gs.CopyTo(mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }

        protected static async Task UnZipSubtitleFileToFile(Stream zipStream, string subFileName)
        {
            using (FileStream subFile = File.OpenWrite(subFileName))
            {
                var gzip = new GZipStream(zipStream, CompressionMode.Decompress);
                await gzip.CopyToAsync(subFile)
                    .ConfigureAwait(false);
            }
        }

        protected static Subtitle BuildSubtitleObject(SearchSubtitlesInfo info)
        {
            var sub = new Subtitle
            {
                SubtitleId = info.IDSubtitle,
                SubtitleHash = info.SubHash,
                SubtitleFileName = info.SubFileName,
                SubTitleDownloadLink = new Uri(info.SubDownloadLink),
                SubtitlePageLink = new Uri(info.SubtitlesLink),
                LanguageId = info.SubLanguageID,
                LanguageName = info.LanguageName,
                Rating = info.SubRating,
                Bad = info.SubBad,

                ImdbId = info.IDMovieImdb,
                MovieId = info.IDMovie,
                MovieName = info.MovieName,
                OriginalMovieName = info.MovieNameEng,
                MovieYear = int.Parse(info.MovieYear)
            };
            return sub;
        }

        protected static MovieInfo BuildMovieInfoObject(CheckMovieHashInfo info)
        {
            var movieInfo = new MovieInfo
            {
                MovieHash = info.MovieHash,
                MovieImdbID = info.MovieImdbID,
                MovieYear = info.MovieYear,
                MovieName = info.MovieName,
                SeenCount = info.SeenCount
            };
            return movieInfo;
        }

        protected static Language BuildLanguageObject(GetSubLanguagesInfo info)
        {
            var language = new Language
            {
                LanguageName = info.LanguageName,
                SubLanguageID = info.SubLanguageID,
                ISO639 = info.ISO639
            };
            return language;
        }

        protected static Movie BuildMovieObject(MoviesOnIMDBInfo info)
        {
            var movie = new Movie
            {
                Id = Convert.ToInt64(info.id),
                Title = info.title
            };
            return movie;
        }

        //protected static MovieDetails BuildMovieDetailsObject(IMDBMovieDetails info)
        //{
        //    var movie = new MovieDetails
        //    {
        //        Aka = info.aka,
        //        Cast = SimpleObjectMapper.MapToDictionary(info.cast as XmlRpcStruct),
        //        Cover = info.cover,
        //        Id = info.id,
        //        Rating = info.rating,
        //        Title = info.title,
        //        Votes = info.votes,
        //        Year = info.year,
        //        Country = info.country,
        //        Directors = SimpleObjectMapper.MapToDictionary(info.directors as XmlRpcStruct),
        //        Duration = info.duration,
        //        Genres = info.genres,
        //        Language = info.language,
        //        Tagline = info.tagline,
        //        Trivia = info.trivia,
        //        Writers = SimpleObjectMapper.MapToDictionary(info.writers as XmlRpcStruct)
        //    };
        //    return movie;
        //}

        protected static UserComment BuildUserCommentObject(CommentsData info)
        {
            var comment = new UserComment
            {
                Comment = info.Comment,
                Created = info.Created,
                IDSubtitle = info.IDSubtitle,
                UserID = info.UserID,
                UserNickName = info.UserNickName
            };
            return comment;
        }

        protected static void VerifyResponseCode(ResponseBase response)
        {
            if (null == response)
            {
                throw new ArgumentNullException("response");
            }
            if (string.IsNullOrEmpty(response.status))
            {
                //aperantly there are some methods that dont define 'status'
                return;
            }

            int responseCode = int.Parse(response.status.Substring(0, 3));
            if (responseCode >= 400)
            {
                //TODO: Create Exception type
                throw new Exception(string.Format("Unexpected error response {1}", responseCode, response.status));
            }
        }
    }
}
