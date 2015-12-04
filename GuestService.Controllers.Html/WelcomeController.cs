using GuestService.Code;
using GuestService.Models.Welcome;
using Sm.System.Mvc;
using Sm.System.Mvc.Language;
using System;
using System.Web.Mvc;
namespace GuestService.Controllers.Html
{
	[HttpPreferences, WebSecurityInitializer, UrlLanguage]
	public class WelcomeController : BaseController
	{
		[ActionName("index"), HttpGet]
		public ActionResult Index(WelcomeParam param)
		{
			if (param != null)
			{
				HttpPreferencesManager manager = new HttpPreferencesManager(this);
				if (param.visual != null)
				{
					if (HttpPreferences.CheckVisualTheme(param.visual))
					{
						HttpPreferences.Current.VisualTheme = param.visual;
					}
					base.Request.RemoveRouteValue("visual");
				}
				if (param.locationhotel != null)
				{
					if (HttpPreferences.CheckLocationHotel(param.locationhotel))
					{
						HttpPreferences.Current.LocationHotel = param.locationhotel;
					}
					base.Request.RemoveRouteValue("locationhotel");
				}
				manager.Save(HttpPreferences.Current);
			}
			string[] pageParams = Settings.GuestDefaultPage.Split(".,;/".ToCharArray());
			return this.RedirectToAction((pageParams.Length > 1) ? pageParams[1] : pageParams[0], (pageParams.Length > 1) ? pageParams[0] : "guest", base.Request.QueryStringAsRouteValues());
		}
		[ActionName("reset"), HttpGet]
		public ActionResult Reset()
		{
			HttpPreferencesManager manager = new HttpPreferencesManager(this);
			manager.Save(HttpPreferences.Current);
			return base.RedirectToAction("index");
		}
		public ActionResult HotelInfo(string id)
		{
			return base.View();
		}
		public ActionResult Error()
		{
			throw new System.Exception("ERROR EXCEPTION");
		}
	}
}
