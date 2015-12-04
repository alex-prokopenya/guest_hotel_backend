using GuestService.Code;
using System;
using System.Web.Mvc;
namespace GuestService
{
	[System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class WebSecurityInitializerAttribute : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			WebSecurityInitializer.Initialize();
		}
	}
}
