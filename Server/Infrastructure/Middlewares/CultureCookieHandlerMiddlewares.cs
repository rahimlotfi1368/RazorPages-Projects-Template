using Microsoft.Extensions.Options;
namespace Infrastructure.Middlewares
{
	public class CultureCookieHandlerMiddlewares:Object
	{
		private RequestDelegate Next { get; }
        public Microsoft.AspNetCore.Builder.RequestLocalizationOptions? RequestLocalizationOptions { get; }

        public CultureCookieHandlerMiddlewares(RequestDelegate next,
			Microsoft.Extensions.Options.IOptions<Microsoft.AspNetCore.Builder.RequestLocalizationOptions>? requestLocalizationOptions) :base()
		{
			Next = next;
            RequestLocalizationOptions = requestLocalizationOptions?.Value;
        }
		
		public async Task InvokeAsync(HttpContext httpContext)
		{
			var defaultCultureName = RequestLocalizationOptions?.DefaultRequestCulture.UICulture.Name;

            var defaultCultureNames=RequestLocalizationOptions?.SupportedCultures?.Select(x => x.Name).ToList();

			var cultureName = ProjectUtilities.GetCurrrentCultureName(httpContext, defaultCultureNames);

			if (cultureName==null)
            {
				cultureName = defaultCultureName;
			}

			ProjectUtilities.SetCulture(cultureName);

			await Next(httpContext);
		}
	}
}
