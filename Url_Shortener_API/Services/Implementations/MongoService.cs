using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Url_Shortener_API.Data.Enitites;
using Url_Shortener_API.Services.Abstractions;
using Url_Shortener_API.Utils;

namespace Url_Shortener_API.Services.Implementations
{
    public class MongoService : IMongoService
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<UrlMapping> _collection;
        public MongoService(
            IOptions<Data.MongoDatabaseSettings> mongoDatabaseSettings,
            IConfiguration configuration)
        {
            _configuration = configuration;

            var mongoClient = new MongoClient(
                mongoDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mongoDatabaseSettings.Value.DatabaseName);

            _collection = mongoDatabase.GetCollection<UrlMapping>(nameof(UrlMapping));

        }

        public async Task CreateAsync(UrlMapping entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task<ServiceResult<UrlMapping>> GetOriginalUrl(string shortUrl)
        {
            var urlMapping = await _collection.Find(x => x.ShortUrlHash == shortUrl && !x.IsDeleted).SingleOrDefaultAsync();
            if(urlMapping == null)
            {
                return new ServiceResult<UrlMapping>();
            }

            return new ServiceResult<UrlMapping>(urlMapping);
        }

        public async Task<ServiceResult<UrlMapping>> GetShortUrl(string url)
        {
            var urlMapping = await _collection.Find(x => x.OriginalUrl == url && !x.IsDeleted).SingleOrDefaultAsync();
            if (urlMapping == null)
            {
                return new ServiceResult<UrlMapping>();
            }

            return new ServiceResult<UrlMapping>(urlMapping);
        }

        public async Task<bool> IsSameUrlExistAsync(string url)
        {
            var isUrlMappingExist = await _collection.Find(x => x.OriginalUrl == url && !x.IsDeleted).AnyAsync();
            return isUrlMappingExist;
        }
    }
}
