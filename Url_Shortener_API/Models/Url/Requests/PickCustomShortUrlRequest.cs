using FluentValidation;

namespace Url_Shortener_API.Models.Url.Requests
{
    public class PickCustomShortUrlRequest
    {
        public string Url { get; set; }
    }

    public class PickCustomShortUrlRequestValidator : AbstractValidator<PickCustomShortUrlRequest>
    {
        public PickCustomShortUrlRequestValidator()
        {
            RuleFor(model => model.Url).MaximumLength(255);

            RuleFor(model => model.Url).NotEmpty().NotNull().WithMessage("URL is required.").Must(BeAValidUrl).WithMessage("Invalid URL format.");
        }

        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
