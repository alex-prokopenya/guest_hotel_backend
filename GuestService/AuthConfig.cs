using DotNetOpenAuth.AspNet;
using GuestService.Code;
using Microsoft.Web.WebPages.OAuth;
using Sm.System.SettingsExtension;
using System;
using System.Collections.Generic;
using System.Configuration;
namespace GuestService
{
	public static class AuthConfig
	{
		public static void RegisterAuth()
		{
			string oauthId = ConfigurationManager.AppSettings.AsString("oauthMicrosoftClientId", null);
			string oauthSecret = ConfigurationManager.AppSettings.AsString("oauthMicrosoftClientSecret", null);
			if (!string.IsNullOrEmpty(oauthId) && !string.IsNullOrEmpty(oauthSecret))
			{
				OAuthWebSecurity.RegisterMicrosoftClient(oauthId, oauthSecret);
			}
			oauthId = ConfigurationManager.AppSettings.AsString("oauthTwitterConsumerKey", null);
			oauthSecret = ConfigurationManager.AppSettings.AsString("oauthTwitterConsumerSecret", null);
			if (!string.IsNullOrEmpty(oauthId) && !string.IsNullOrEmpty(oauthSecret))
			{
				OAuthWebSecurity.RegisterTwitterClient(oauthId, oauthSecret);
			}
			oauthId = ConfigurationManager.AppSettings.AsString("oauthFacebookAppId", null);
			oauthSecret = ConfigurationManager.AppSettings.AsString("oauthFacebookAppSecret", null);
			if (!string.IsNullOrEmpty(oauthId) && !string.IsNullOrEmpty(oauthSecret))
			{
				OAuthWebSecurity.RegisterFacebookClient(oauthId, oauthSecret);
			}
			if (ConfigurationManager.AppSettings.AsBool("oauthGoogle", false))
			{
				OAuthWebSecurity.RegisterGoogleClient();
			}
			oauthId = ConfigurationManager.AppSettings.AsString("oauthVKontakteAppId", null);
			oauthSecret = ConfigurationManager.AppSettings.AsString("oauthVKontakteAppSecret", null);
			if (!string.IsNullOrEmpty(oauthId) && !string.IsNullOrEmpty(oauthSecret))
			{
				IAuthenticationClient client = new VKontakteAuthenticationClient(oauthId, oauthSecret);
				string displayName = "VK";
				System.Collections.Generic.IDictionary<string, object> extraData = null;
				OAuthWebSecurity.RegisterClient(client, displayName, extraData);
			}
		}
	}
}
