namespace Url_Shortener_API.Data.Enitites
{
    public class UrlMapping : BaseMongoEntity
    {
        public string OriginalUrl { get; set; }
        public string ShortUrlHash { get; set; }
    }
}
