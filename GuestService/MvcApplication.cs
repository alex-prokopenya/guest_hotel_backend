using GuestService.Code;
using System;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
namespace GuestService
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			WebApiConfig.Register(GlobalConfiguration.Configuration);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			AuthConfig.RegisterAuth();
			WebSecurityInitializer.Initialize();
		}
		public override string GetVaryByCustomString(HttpContext context, string custom)
		{
			string result;
			if (custom == "language")
			{
				result = System.Threading.Thread.CurrentThread.CurrentUICulture.ToString();
			}
			else
			{
				result = base.GetVaryByCustomString(context, custom);
			}
			return result;
		}
		protected void Application_Error(object sender, System.EventArgs e)
		{
			System.Exception exception = base.Server.GetLastError();
			base.Response.Clear();
		}
	}
}
