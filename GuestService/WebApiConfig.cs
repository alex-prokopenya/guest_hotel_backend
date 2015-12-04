using Sm.System.Mvc.Exceptions;
using System;
using System.Web.Http;
namespace GuestService
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.Filters.Add(new HttpExceptionFilterAttribute());
			config.Formatters.XmlFormatter.UseXmlSerializer = true;
			string name = "Api";
			string routeTemplate = "api/{language}/{controller}/{action}/{id}";
			object defaults = new
			{
				action = "index",
				id = RouteParameter.Optional
			};
			object constraints = new
			{
				language = "\\w\\w(\\-\\w\\w)?"
			};
			config.Routes.MapHttpRoute(name, routeTemplate, defaults, constraints);
			name = "ApiWithoutLanguage";
			routeTemplate = "api/{controller}/{action}/{id}";
			defaults = new
			{
				language = "en",
				action = "index",
				id = RouteParameter.Optional
			};
			config.Routes.MapHttpRoute(name, routeTemplate, defaults);
		}
	}
}
