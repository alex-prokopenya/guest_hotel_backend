using Sm.System.Mvc.Log;
using System;
using System.Web.Mvc;
namespace GuestService
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
			filters.Add(new LogExceptionAttribute());
		}
	}
}
