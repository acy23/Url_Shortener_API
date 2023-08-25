using FluentValidation;

namespace Url_Shortener_API.Models.Url.Requests
{
    public class PickCustomShortUrlRequest
    {
        public string CustomShortUrlHashedPortion { get; set; }
        public string OriginalUrl { get; set; }
    }

    public class PickCustomShortUrlRequestValidator : AbstractValidator<PickCustomShortUrlRequest>
    {
        public PickCustomShortUrlRequestValidator()
        {
            RuleFor(model => model.OriginalUrl).MaximumLength(255);
            RuleFor(model => model.OriginalUrl).NotEmpty().NotNull().WithMessage("URL is required.").Must(BeAValidUrl).WithMessage("Invalid URL format.");
            RuleFor(model => model.CustomShortUrlHashedPortion).NotEmpty().NotNull().MaximumLength(6).WithMessage("Invalid custom hash portion.");
        }

        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
