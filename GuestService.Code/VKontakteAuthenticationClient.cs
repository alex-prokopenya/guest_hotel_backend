using DotNetOpenAuth.AspNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
namespace GuestService.Code
{
	public class VKontakteAuthenticationClient : IAuthenticationClient
	{
		private class AccessToken
		{
			public string access_token = null;
			public string user_id = null;
		}
		private class UserData
		{
			public string uid = null;
			public string first_name = null;
			public string last_name = null;
			public string photo_50 = null;
		}
		private class UsersData
		{
			public VKontakteAuthenticationClient.UserData[] response = null;
		}
		public string appId;
		public string appSecret;
		private string redirectUri;
		string IAuthenticationClient.ProviderName
		{
			get
			{
				return "vkontakte";
			}
		}
		public VKontakteAuthenticationClient(string appId, string appSecret)
		{
			this.appId = appId;
			this.appSecret = appSecret;
		}
		void IAuthenticationClient.RequestAuthentication(HttpContextBase context, Uri returnUrl)
		{
			string APP_ID = this.appId;
			this.redirectUri = context.Server.UrlEncode(returnUrl.ToString());
			string address = string.Format("https://oauth.vk.com/authorize?client_id={0}&redirect_uri={1}&response_type=code", APP_ID, this.redirectUri);
			HttpContext.Current.Response.Redirect(address, false);
		}
		AuthenticationResult IAuthenticationClient.VerifyAuthentication(HttpContextBase context)
		{
			AuthenticationResult result;
			try
			{
				string code = context.Request["code"];
				string address = string.Format("https://oauth.vk.com/access_token?client_id={0}&client_secret={1}&code={2}&redirect_uri={3}", new object[]
				{
					this.appId,
					this.appSecret,
					code,
					this.redirectUri
				});
				string response = VKontakteAuthenticationClient.Load(address);
				VKontakteAuthenticationClient.AccessToken accessToken = VKontakteAuthenticationClient.DeserializeJson<VKontakteAuthenticationClient.AccessToken>(response);
				address = string.Format("https://api.vk.com/method/users.get?uids={0}&fields=photo_50", accessToken.user_id);
				response = VKontakteAuthenticationClient.Load(address);
				VKontakteAuthenticationClient.UsersData usersData = VKontakteAuthenticationClient.DeserializeJson<VKontakteAuthenticationClient.UsersData>(response);
				VKontakteAuthenticationClient.UserData userData = usersData.response.First<VKontakteAuthenticationClient.UserData>();
				result = new AuthenticationResult(true, ((IAuthenticationClient)this).ProviderName, accessToken.user_id, userData.first_name + " " + userData.last_name, new System.Collections.Generic.Dictionary<string, string>());
			}
			catch (System.Exception ex)
			{
				result = new AuthenticationResult(ex);
			}
			return result;
		}
		public static string Load(string address)
		{
			HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
			string result;
			using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
			{
				using (System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream()))
				{
					result = reader.ReadToEnd();
				}
			}
			return result;
		}
		public static T DeserializeJson<T>(string input)
		{
			JavaScriptSerializer serializer = new JavaScriptSerializer();
			return serializer.Deserialize<T>(input);
		}
	}
}
