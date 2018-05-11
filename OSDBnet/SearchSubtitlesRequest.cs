namespace OSDBnet
{
    internal class SearchSubtitlesRequest
    {
        public string Sublanguageid { get; set; }
        public string Moviehash { get; set; }
        public long? Moviebytesize { get; set; }
        public string Imdbid { get; set; }
        public string Query { get; set; }
        public int? Season { get; set; }
        public int? Episode { get; set; }
    }
}