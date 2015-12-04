using System;
using System.Text;
using System.Web;
namespace GuestService.Code
{
	public static class HttpRequestBaseExtension
	{
		public static string DumpValues(this HttpRequestBase request)
		{
			System.Text.StringBuilder result = new System.Text.StringBuilder();
			if (request != null)
			{
				if (request.QueryString != null && request.QueryString.Keys.Count > 0)
				{
					result.AppendLine("QueryString:");
					foreach (string i in request.QueryString.Keys)
					{
						result.AppendLine(string.Format(" {0} = '{1}'", i, request.QueryString[i]));
					}
				}
				if (request.Params != null && request.Params.Keys.Count > 0)
				{
					result.AppendLine("Params:");
					foreach (string i in request.Params.Keys)
					{
						result.AppendLine(string.Format(" {0} = '{1}'", i, request.Params[i]));
					}
				}
				if (request.Form != null && request.Form.Keys.Count > 0)
				{
					result.AppendLine("Form:");
					foreach (string i in request.Form.Keys)
					{
						result.AppendLine(string.Format(" {0} = '{1}'", i, request.Form[i]));
					}
				}
				if (request.ServerVariables != null && request.ServerVariables.Keys.Count > 0)
				{
					result.AppendLine("ServerVariables:");
					foreach (string i in request.ServerVariables.Keys)
					{
						result.AppendLine(string.Format(" {0} = '{1}'", i, request.ServerVariables[i]));
					}
				}
			}
			return result.ToString();
		}
		public static Uri BaseServerAddress(this HttpRequestBase request)
		{
			string forwardedHost = request.ServerVariables["HTTP_X_FORWARDED_HOST"];
			return new Uri(request.Url.Scheme + "://" + ((!string.IsNullOrEmpty(forwardedHost)) ? forwardedHost : request.Url.Authority));
		}
	}
}
