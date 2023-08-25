using Url_Shortener_API.Data.Enitites;
using Url_Shortener_API.Utils;

namespace Url_Shortener_API.Services.Abstractions
{
    public interface IMongoService
    {
        Task CreateAsync(UrlMapping entity);
        Task<ServiceResult<UrlMapping>> GetMappingByShortUrl(string shortUrl);
        Task<ServiceResult<UrlMapping>> GetMappingByOriginalUrl(string url);
        Task<bool> IsOriginalUrlExistAsync(string url);
        Task<bool> IsShortUrlHashedPortionExistAsync(string shortUrlHashedPortion);
    }
}
