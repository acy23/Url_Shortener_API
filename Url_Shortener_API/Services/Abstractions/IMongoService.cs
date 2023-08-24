using Url_Shortener_API.Data.Enitites;
using Url_Shortener_API.Utils;

namespace Url_Shortener_API.Services.Abstractions
{
    public interface IMongoService
    {
        Task CreateAsync(UrlMapping entity);
        Task<ServiceResult<UrlMapping>> GetOriginalUrl(string shortUrl);
        Task<ServiceResult<UrlMapping>> GetShortUrl(string url);
        Task<bool> IsSameUrlExistAsync(string url);
    }
}
