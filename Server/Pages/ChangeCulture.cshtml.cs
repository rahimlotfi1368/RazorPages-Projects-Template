using Infrastructure.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Utilities;

namespace Server.Pages
{
    public class ChangeCultureModel : Infrastructure.BasePageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ChangeCultureModel(IHttpContextAccessor httpContextAccessor):base()
		{
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnGet(string? culture)
        {

            var defaultCulture = "fa";
		
            var typedHeaders=HttpContext.Request.GetTypedHeaders();

            var httpReferer = typedHeaders.Referer?.AbsoluteUri;

			if (string.IsNullOrWhiteSpace(httpReferer))
			{
                return RedirectToPage("/Index");
			}

			if (string.IsNullOrEmpty(culture))
			{
                culture = defaultCulture;
			}

            culture= culture.Trim().ToLower();

			switch (culture)
			{
				case "fa":
				case "en":
					{
						break;
					}
				default:
					{
						culture= defaultCulture;
						break;
					}
			}

			ProjectUtilities.SetCulture(culture);

			ProjectUtilities.RemoveCookie(_httpContextAccessor.HttpContext, culture);

			ProjectUtilities.CreateCookie(_httpContextAccessor.HttpContext,CultureCookieHandlerMiddlewares.CookieName, culture, 1);

			return Redirect(httpReferer);
        }
    }
}
