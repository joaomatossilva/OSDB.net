using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace OSDBnet
{
    public class OsdbClient : IOsdbClient, IDisposable
    {
        private const string Url = "https://rest.opensubtitles.org/search/";
        private bool disposed = false;

        protected readonly string userAgent;
        protected HttpClient httpClient;

        internal OsdbClient(string userAgent)
        {
            this.userAgent = userAgent;
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
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
            var request = new SearchSubtitlesRequest { Sublanguageid = languages };
            request.Moviehash = HashHelper.ToHexadecimal(HashHelper.ComputeMovieHash(filename));
            request.Moviebytesize = file.Length;

            request.Imdbid = string.Empty;
            request.Query = string.Empty;

            return SearchSubtitlesInternal(request);
        }

        public Task<IList<Subtitle>> SearchSubtitlesFromHash(string languages, string fileHash, long moviebytesize)
        {
            var request = new SearchSubtitlesRequest { Sublanguageid = languages };
            request.Moviehash = fileHash;
            request.Moviebytesize = moviebytesize;

            request.Imdbid = string.Empty;
            request.Query = string.Empty;

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
                Sublanguageid = languages,
                Imdbid = imdbId
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
                Sublanguageid = languages,
                Query = query,
                Season = season,
                Episode = episode
            };
            return SearchSubtitlesInternal(request);
        }

        private async Task<IList<Subtitle>> SearchSubtitlesInternal(SearchSubtitlesRequest request)
        {
            var keys = new List<string>();
            if(request.Episode != null)
            {
                keys.Add($"episode-{request.Episode}");
            }
            if(request.Imdbid != null)
            {
                keys.Add($"imdbid-{request.Imdbid}");
            }
            if(request.Moviebytesize != null)
            {
                keys.Add($"moviebytesize-{request.Moviebytesize}");
            }
            if (!string.IsNullOrEmpty(request.Moviehash))
            {
                keys.Add($"moviehash-{request.Moviehash}");
            }
            if (!string.IsNullOrEmpty(request.Query))
            {
                keys.Add($"query-{WebUtility.UrlEncode(request.Query)}");
            }
            if(request.Season != null)
            {
                keys.Add($"season-{request.Season}");
            }
            if(!string.IsNullOrEmpty(request.Sublanguageid))
            {
                keys.Add($"sublanguageid-{request.Sublanguageid}");
            }

            var response = await Get<List<Subtitle>>(keys)
                .ConfigureAwait(false);
            return response;
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
            string tempZipName = Path.GetTempFileName();

            var stream = await GetStream(subtitle.SubTitleDownloadLink)
                .ConfigureAwait(false);
            await UnZipSubtitleFileToFile(stream, destinationfile)
                .ConfigureAwait(false);

            return destinationfile;
        }

        protected virtual async Task<T> Get<T>(List<string> parameters)
        {
            parameters.Sort();
            var path = String.Join("/", parameters);

            var response = await httpClient.GetStringAsync(new Uri(Url + path))
                .ConfigureAwait(false);

            return JsonConvert.DeserializeObject<T>(response);
        }

        protected virtual Task<Stream> GetStream(Uri uri)
        {
            return httpClient.GetStreamAsync(uri);
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing && httpClient != null)
                {
                    httpClient.Dispose();
                    httpClient = null;
                }
                disposed = true;
            }
        }

        ~OsdbClient()
        {
            Dispose(false);
        }

        //protected static string Base64Encode(string str)
        //{
        //    byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(str);
        //    return Convert.ToBase64String(encbuff);
        //}
        //protected static string Base64Decode(string str)
        //{
        //    byte[] decbuff = Convert.FromBase64String(str);
        //    return System.Text.Encoding.UTF8.GetString(decbuff);
        //}

        //protected static string GUnzipString(byte[] gzippedString)
        //{
        //    using (var msi = new MemoryStream(gzippedString))
        //    using (var mso = new MemoryStream())
        //    {
        //        using (var gs = new GZipStream(msi, CompressionMode.Decompress))
        //        {
        //            gs.CopyTo(mso);
        //        }

        //        return Encoding.UTF8.GetString(mso.ToArray());
        //    }
        //}

        protected static async Task UnZipSubtitleFileToFile(Stream zipFileStream, string subFileName)
        {
            using (FileStream subFile = File.OpenWrite(subFileName))
            {
                var gzip = new GZipStream(zipFileStream, CompressionMode.Decompress);
                await gzip.CopyToAsync(subFile)
                    .ConfigureAwait(false);
            }
        }
    }
}
