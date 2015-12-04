using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace GuestService.Models
{
	public class RequestSettings
	{
		private const string cookieName = "viewmode";
		private const string themeKey = "theme";
		private const string hotelKey = "hotel";
		private HttpRequestBase request;
		private HttpResponseBase response;
		private string settingsTheme;
		private string settingsHotel;
		public string Theme
		{
			get
			{
				return this.settingsTheme;
			}
			set
			{
				if (this.settingsTheme != value)
				{
					this.settingsTheme = value;
					this.SaveCookieSettings();
				}
			}
		}
		public string Hotel
		{
			get
			{
				return this.settingsHotel;
			}
			set
			{
				if (this.settingsHotel != value)
				{
					this.settingsHotel = value;
					this.SaveCookieSettings();
				}
			}
		}
		public RequestSettings(HttpRequestBase request) : this(request, null)
		{
		}
		public RequestSettings(HttpRequestBase request, HttpResponseBase response)
		{
			this.request = request;
			if (request == null)
			{
				throw new System.ArgumentNullException("request");
			}
			this.response = response;
			HttpCookie cookie = null;
			if (this.response != null && this.response.Cookies.AllKeys.Contains("viewmode"))
			{
				cookie = this.response.Cookies["viewmode"];
			}
			else
			{
				if (this.request != null)
				{
					cookie = this.request.Cookies["viewmode"];
				}
			}
			this.ReadCookieSettings(cookie);
		}
		public void Clear()
		{
			this.settingsTheme = null;
			this.settingsHotel = null;
			this.SaveCookieSettings();
		}
		private void ReadCookieSettings(HttpCookie cookie)
		{
			if (cookie != null && cookie.HasKeys)
			{
				this.settingsTheme = cookie["theme"];
				this.settingsHotel = cookie["hotel"];
			}
		}
		private void SaveCookieSettings()
		{
			if (this.response == null)
			{
				throw new System.Exception("save settiongs not allowed");
			}
			HttpCookie cookie = new HttpCookie("viewmode");
			cookie.Path = UrlHelper.GenerateContentUrl("~", this.request.RequestContext.HttpContext);
			cookie.Expires = System.DateTime.Now.AddYears(1);
			if (this.settingsTheme != null)
			{
				cookie.Values.Add("theme", this.settingsTheme);
			}
			if (this.settingsHotel != null)
			{
				cookie.Values.Add("hotel", this.settingsHotel);
			}
			this.response.Cookies.Set(cookie);
		}
	}
}
