using Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Middlewares
{
    public static class ExtentionMethods
    {
        public static IApplicationBuilder UseCultureCookie(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CultureCookieHandlerMiddlewares>();
        }
    }
}
