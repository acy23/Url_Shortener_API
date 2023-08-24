using FluentValidation;

namespace Url_Shortener_API.Models.Url.Requests
{
    public class RedirectToOrigininalUrlRequest
    {
        public string ShortUrl { get; set; }
    }

    public class RedirectToOrigininalUrlRequestValidator : AbstractValidator<RedirectToOrigininalUrlRequest>
    {
        public RedirectToOrigininalUrlRequestValidator()
        {
            RuleFor(model => model.ShortUrl).MaximumLength(255);

            RuleFor(model => model.ShortUrl).NotEmpty().NotNull().WithMessage("URL is required.").Must(BeAValidUrl).WithMessage("Invalid URL format.");
        }

        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
