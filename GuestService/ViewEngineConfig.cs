using System;
using System.Collections.Generic;
using System.Web.Mvc;
namespace GuestService
{
	public static class ViewEngineConfig
	{
		public static void Register(ViewEngineCollection viewEngineCollection)
		{
			foreach (IViewEngine viewEngine in viewEngineCollection)
			{
				RazorViewEngine engine = viewEngine as RazorViewEngine;
				if (engine != null)
				{
					System.Collections.Generic.List<string> locations = new System.Collections.Generic.List<string>
					{
						string.Format("{0}/{{0}}.cshtml", CustomizationPath.ViewsFolder)
					};
					locations.AddRange(engine.ViewLocationFormats);
					engine.PartialViewLocationFormats = locations.ToArray();
				}
			}
		}
	}
}
