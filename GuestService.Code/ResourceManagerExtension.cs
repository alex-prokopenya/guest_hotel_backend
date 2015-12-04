using System;
using System.Resources;
namespace GuestService.Code
{
	public static class ResourceManagerExtension
	{
		public static string Get(this System.Resources.ResourceManager manager, string name)
		{
			return manager.GetString(name);
		}
		public static string Get(this System.Resources.ResourceManager manager, string name, params object[] param)
		{
			return string.Format(manager.GetString(name), param);
		}
	}
}
