using Utilities;
namespace Infrastructure.Middlewares
{
	public class CultureCookieHandlerMiddlewares:Object
	{
		private RequestDelegate Next { get; }
		public CultureCookieHandlerMiddlewares(RequestDelegate next) :base()
		{
			Next = next;
		}
		public readonly static string CookieName = "Culture.Cookie";
		public async Task InvokeAsync(HttpContext httpContext)
		{
			var cultureName = ProjectUtilities.ReadCookie(httpContext, CookieName);

			if (string.IsNullOrEmpty(cultureName)==false)
            {
				cultureName=cultureName.Substring(0, 2).ToLower();
			}

			switch (cultureName)
			{
				case "fa":
				case "en":
					{
						ProjectUtilities.SetCulture(cultureName);
						break;
					}
				default:
					{
						ProjectUtilities.SetCulture("fa");
						break;
					}
			}
			await Next(httpContext);
		}
	}
}
