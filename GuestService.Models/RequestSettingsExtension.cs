using System;
using System.Web;
namespace GuestService.Models
{
	public static class RequestSettingsExtension
	{
		public static RequestSettings RequestSettings(this HttpRequestBase request)
		{
			return new RequestSettings(request, request.RequestContext.HttpContext.Response);
		}
	}
}
