﻿using System.Diagnostics.Metrics;
using Url_Shortener_API.Data.Enitites;
using Url_Shortener_API.Services.Abstractions;
using Url_Shortener_API.Utils;

namespace Url_Shortener_API.Services.Implementations
{
    public class UrlService : IUrlService
    {
        private readonly IMongoService _mongoService;
        private readonly ILogger<UrlService> _logger;

        private const string UrlBase = "https://my.url/";
        public UrlService(IMongoService mongoService, ILogger<UrlService> logger)
        {
            _mongoService = mongoService;
            _logger = logger;
        }

        public async Task<ServiceResult<string>> CreateShortenUrl(string url)
        {
            var hashedPortion = GenerateRandomString(6);

            var isSameUrlExist = await _mongoService.IsOriginalUrlExistAsync(url);
            var isHashedPortionExist = await _mongoService.IsShortUrlHashedPortionExistAsync(hashedPortion);
            if (isSameUrlExist || isHashedPortionExist)
            {
                return new ServiceResult<string>();
            }

            var entity = new UrlMapping
            {
                OriginalUrl = url,
                ShortUrlHash = hashedPortion,
            };

            try
            {
                await _mongoService.CreateAsync(entity);
                return new ServiceResult<string>($"{UrlBase}{hashedPortion}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw; // I do not throw ex to hide stack trace.
            }
        }

        public async Task<ServiceResult<string>> GetOriginalUrl(string shortUrl)
        {
            var hashedPortion = ParseShortUrl(shortUrl);
            if(hashedPortion == null)
            {
                return new ServiceResult<string>();
            }

            var urlMapping = await _mongoService.GetMappingByShortUrl(hashedPortion);
            if (urlMapping.HasValue)
            {
                return new ServiceResult<string>(urlMapping.Value.OriginalUrl);
            }

            return new ServiceResult<string>();
        }
        public async Task<ServiceResult<string>> GetShortUrl(string url)
        {
            var urlMapping = await _mongoService.GetMappingByOriginalUrl(url);
            if(urlMapping.HasValue)
            {
                var fullShortUrl = $"{UrlBase}{urlMapping.Value.ShortUrlHash}";
                return new ServiceResult<string>(fullShortUrl);
            }

            return new ServiceResult<string>();
        }

        public async Task<ServiceResult<string>> PickCustomShortUrl(string url, string shortUrlHashedPortion)
        {
            var isOriginalUrlExist = await _mongoService.IsOriginalUrlExistAsync(url);
            var isShortUrlHashedPortionExist = await _mongoService.IsShortUrlHashedPortionExistAsync(shortUrlHashedPortion);

            if(isOriginalUrlExist || isShortUrlHashedPortionExist)
            {
                return new ServiceResult<string>();
            }

            var entity = new UrlMapping
            {
                ShortUrlHash = shortUrlHashedPortion,
                OriginalUrl = url,
            };

            try
            {
                await _mongoService.CreateAsync(entity);
                return new ServiceResult<string>($"{UrlBase}{shortUrlHashedPortion}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }
        }

        private string GenerateRandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string? ParseShortUrl(string shortUrl)
        {
            Uri uri = new Uri(shortUrl);
            string path = uri.AbsolutePath; 

            if (path.StartsWith('/'))
            {
                string uniqueId = path.Substring(1);
                return uniqueId;
            }

            return null; 
        }
    }
}
