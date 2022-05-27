using Infrastructure.Middlewares;
using Microsoft.Extensions.Options;

namespace Server.Pages
{
    public class ChangeCultureModel : Infrastructure.BasePageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ApplicationSettings? ApplicationSettingOptions { get; }

        public ChangeCultureModel(IHttpContextAccessor httpContextAccessor,
			Microsoft.Extensions.Options.IOptions<ApplicationSettings>? applicationSettingOptions) :base()
		{
            _httpContextAccessor = httpContextAccessor;
            ApplicationSettingOptions = applicationSettingOptions?.Value;
        }


        public IActionResult OnGet(string? cultureName)
        {
            var typedHeaders=HttpContext.Request.GetTypedHeaders();

            var httpReferer = typedHeaders.Referer?.AbsoluteUri;

			if (string.IsNullOrWhiteSpace(httpReferer))
			{
                return RedirectToPage("/Index");
			}

            var defaultCultureName = ApplicationSettingOptions?.CultureSettings?.DefaultCultureName;

            var defaultCultureNames = ApplicationSettingOptions?.CultureSettings?.SupportedCultureNames;

            if (string.IsNullOrEmpty(cultureName))
			{
                cultureName = defaultCultureName;
			}

            if (defaultCultureNames?.Contains(cultureName!)==false)
            {
				cultureName = defaultCultureName;
            }        

			ProjectUtilities.SetCulture(cultureName);

			ProjectUtilities.RemoveCookie(_httpContextAccessor.HttpContext!, cultureName!);

			ProjectUtilities.CreateCookie(_httpContextAccessor.HttpContext!,ProjectUtilities.CookieName, cultureName!, 1);

			return Redirect(httpReferer);
        }
    }
}
