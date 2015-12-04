using System;
using System.Web;
using System.Web.Mvc;
namespace GuestService.Code
{
	public class HttpPreferencesManager : System.IDisposable
	{
		private const string cookieName = "preferences";
		private const string visualThemeName = "vt";
		private const string locationHotelName = "lh";
		private Controller controller;
		public HttpPreferencesManager(Controller controller)
		{
			if (controller == null)
			{
				throw new System.ArgumentNullException();
			}
			this.controller = controller;
		}
		public HttpPreferences LoadPreferences()
		{
			HttpCookie cookie = this.controller.HttpContext.Request.Cookies["preferences"];
			HttpPreferences result2;
			if (cookie != null && cookie.Values != null)
			{
				HttpPreferences result = HttpPreferences.CreateDefault();
				string value = cookie.Values["vt"];
				if (value != null && HttpPreferences.CheckVisualTheme(value))
				{
					result.VisualTheme = value;
				}
				value = cookie.Values["lh"];
				if (value != null && HttpPreferences.CheckLocationHotel(value))
				{
					result.LocationHotel = (string.IsNullOrEmpty(value) ? null : value);
				}
				result2 = result;
			}
			else
			{
				result2 = null;
			}
			return result2;
		}
		public void Save(HttpPreferences preferences)
		{
			if (preferences == null)
			{
				throw new System.ArgumentNullException("preferences");
			}
			HttpCookie cookie = new HttpCookie("preferences");
			cookie.Path = this.controller.Url.Content("~");
			cookie.Expires = System.DateTime.Now.AddYears(10);
			cookie.Values["vt"] = preferences.VisualTheme;
			cookie.Values["lh"] = preferences.LocationHotel;
			this.controller.HttpContext.Response.Cookies.Set(cookie);
		}
		public void Dispose()
		{
			this.Save(HttpPreferences.Current);
		}
	}
}
