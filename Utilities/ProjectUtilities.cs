using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
	public static class ProjectUtilities
	{
		public static void CreateCookie(HttpContext httpContext, string key, string value, int expireTime)
		{
			CookieOptions option = new CookieOptions()
			{
				Path = "/", // Default: [null]

				// Domain: Gets or sets the domain to associate the cookie with.
				// نباید تغییر دهیم، خودش به طور
				// خودکار بر اساس دامنه سایت تنظیم می‌شود

				Secure = false, // Default: [false]

				// Secure: Gets or sets a value that indicates whether to transmit
				// the cookie using Secure Sockets Layer (SSL)--that is, over HTTPS only.

				HttpOnly = false, // Default: [false]

				// HttpOnly: Gets or sets a value that indicates
				// whether a cookie is accessible by client-side script.
				// [true] if a cookie must not be accessible by client-side script; otherwise, [false].

				IsEssential = false, // Default: [false]

				// Indicates if this cookie is essential for the application
				// to function correctly. If true then consent policy checks may be bypassed.

				MaxAge = null, // Default: [null]

				// MaxAge: Gets or sets the max-age for the cookie.

				Expires =
						System.DateTimeOffset.UtcNow.AddYears(1), // Default: [null]

				// Expires: Gets or sets the expiration date and time for the cookie.

				//SameSite =
				//		Microsoft.AspNetCore.Http.SameSiteMode.Unspecified, // Default: [Unspecified]

				// SameSite: Gets or sets the value for the SameSite attribute of the cookie.

				// The SameSiteMode representing the enforcement mode of the cookie:

				// Lax			1	Indicates the client should send the cookie with "same-site"
				//					requests, and with "cross-site" top-level navigations.
				// None			0	Indicates the client should disable same-site restrictions.
				// Strict		2	Indicates the client should only send the cookie with
				//					"same-site" requests.
				// Unspecified	-1	No SameSite field will be set, the client should
				//					follow its default cookie policy.
			};
			option.Expires = DateTime.Now.AddMonths(expireTime);
			httpContext.Response.Cookies.Append(key, value, option);
		}

		public static void RemoveCookie(HttpContext httpContext, string key)
		{
			httpContext.Response.Cookies.Delete(key);
		}

		public static string ReadCookie(HttpContext httpContext, string key)
		{
			var value = httpContext.Request.Cookies[key];
			return value != null ? value : string.Empty;
		}

		public static void SetCulture(string? cultureName)
		{
			if (string.IsNullOrWhiteSpace(cultureName) == false)
			{
				var cultureInfo =
					new System.Globalization.CultureInfo(name: cultureName);

				System.Threading.Thread.CurrentThread.CurrentCulture = cultureInfo;
				System.Threading.Thread.CurrentThread.CurrentUICulture = cultureInfo;
			}
		}

	}
}
