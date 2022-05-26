using Infrastructure.Middlewares;
using Microsoft.Extensions.Options;

namespace Server.Pages
{
    public class ChangeCultureModel : Infrastructure.BasePageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private RequestLocalizationOptions? RequestLocalizationOptions { get; }

        public ChangeCultureModel(IHttpContextAccessor httpContextAccessor,
			Microsoft.Extensions.Options.IOptions<Microsoft.AspNetCore.Builder.RequestLocalizationOptions>? requestLocalizationOptions) :base()
		{
            _httpContextAccessor = httpContextAccessor;
            RequestLocalizationOptions = requestLocalizationOptions?.Value;
        }


        public IActionResult OnGet(string? cultureName)
        {
            var typedHeaders=HttpContext.Request.GetTypedHeaders();

            var httpReferer = typedHeaders.Referer?.AbsoluteUri;

			if (string.IsNullOrWhiteSpace(httpReferer))
			{
                return RedirectToPage("/Index");
			}

            var defaultCulture = RequestLocalizationOptions?.DefaultRequestCulture?.UICulture.Name;

            var defaultCultureNames = RequestLocalizationOptions?.SupportedCultures?.Select(x => x.Name).ToList();

            if (string.IsNullOrEmpty(cultureName))
			{
                cultureName = defaultCulture;
			}

            if (defaultCultureNames?.Contains(cultureName!)==false)
            {
				cultureName = defaultCulture;
            }        

			ProjectUtilities.SetCulture(cultureName);

			ProjectUtilities.RemoveCookie(_httpContextAccessor.HttpContext!, cultureName!);

			ProjectUtilities.CreateCookie(_httpContextAccessor.HttpContext!,ProjectUtilities.CookieName, cultureName!, 1);

			return Redirect(httpReferer);
        }
    }
}
