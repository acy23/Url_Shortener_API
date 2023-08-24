using Url_Shortener_API.Utils;

namespace Url_Shortener_API.Services.Abstractions
{
    public interface IUrlService
    {
        Task<ServiceResult<string>> CreateShortenUrl(string url);
        Task<ServiceResult<string>> GetOriginalUrl(string shortUrl);
        Task<ServiceResult<string>> GetShortUrl(string url);

    }
}
