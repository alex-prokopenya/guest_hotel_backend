using System;
using System.Web.Mvc;
namespace GuestService.Code
{
	[System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Method, AllowMultiple = false)]
	public class HttpPreferencesAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			Controller controller = filterContext.Controller as Controller;
			if (controller != null)
			{
				HttpPreferencesManager manager = new HttpPreferencesManager(controller);
				HttpPreferences preferences = manager.LoadPreferences();
				if (preferences != null)
				{
					HttpPreferences.Current = preferences;
				}
			}
		}
	}
}
