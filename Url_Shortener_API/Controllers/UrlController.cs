using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Net;
using Url_Shortener_API.Models.Url.Requests;
using Url_Shortener_API.Models.Url.Responses;
using Url_Shortener_API.Services.Abstractions;

namespace Url_Shortener_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlController : ControllerBase
    {
        private readonly IUrlService _service;
        public UrlController(IUrlService service)
        {
            _service = service;
        }

        [HttpPost("short-url")]
        public async Task<ActionResult<UrlShortenerResponseModel>> UrlShortener(UrlShortenerRequestModel request)
        {
            var serviceResult = await _service.CreateShortenUrl(request.Url);
            if (serviceResult.HasValue)
            {
                return Ok(new UrlShortenerResponseModel { ShortenedUrl = serviceResult.Value });
            }

            return StatusCode(403, "Error encountered while processing your request.");
        }

        [HttpGet("redirect")]
        public async Task<IActionResult> RedirectToOriginalUrl([FromQuery] RedirectToOrigininalUrlRequest request)
        {
            var originalUrl = await _service.GetOriginalUrl(request.ShortUrl);
            if (originalUrl.HasValue)
            {
                return Redirect(originalUrl.Value);
            }

            return NotFound("Url could not found");
        }

        [HttpGet("get-short-url")]
        public async Task<ActionResult<ShortUrlResponse>> GetShortUrl([FromQuery] ShortUrlRequest request)
        {
            var originalUrl = await _service.GetShortUrl(request.Url);
            if (originalUrl.HasValue)
            {
                return Ok(new ShortUrlResponse { ShortUrl = originalUrl.Value });
            }

            return NotFound("Custom short url could not found");
        }

        [HttpPost("pick-custom-short-url")]
        public async Task<ActionResult<PickCustomShortUrlResponse>> PickCustomShortUrl([FromBody] PickCustomShortUrlRequest request)
        {
            var response = await _service.PickCustomShortUrl(request.OriginalUrl, request.CustomShortUrlHashedPortion);
            if (response.HasValue)
            {
                return Ok(new PickCustomShortUrlResponse
                {
                    ShortUrl = response.Value
                });
            }

            return StatusCode(403, "Error encountered while processing your request.");
        }
    }
    
}
