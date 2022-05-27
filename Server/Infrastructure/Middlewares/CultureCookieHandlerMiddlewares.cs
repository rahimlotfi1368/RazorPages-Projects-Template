namespace Infrastructure.Middlewares
{
	public class CultureCookieHandlerMiddlewares:Object
	{
		private RequestDelegate Next { get; }
        public ApplicationSettings? ApplicationSettingOptions { get; }

        public CultureCookieHandlerMiddlewares(RequestDelegate next,
			Microsoft.Extensions.Options.IOptions<ApplicationSettings>? applicationSettingOptions) :base()
		{
			Next = next;
            ApplicationSettingOptions = applicationSettingOptions?.Value;
        }
		
		public async Task InvokeAsync(HttpContext httpContext)
		{
			var defaultCultureName = ApplicationSettingOptions?.CultureSettings?.DefaultCultureName;

			var defaultCultureNames = ApplicationSettingOptions?.CultureSettings?.SupportedCultureNames;

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
