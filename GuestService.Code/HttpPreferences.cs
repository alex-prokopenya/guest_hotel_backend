using Sm.System.Extension;
using System;
using System.Configuration;
using System.Linq;
namespace GuestService.Code
{
	public class HttpPreferences
	{
		[System.ThreadStatic]
		private static HttpPreferences current;
		private string visualTheme;
		private string locationHotel;
		public static HttpPreferences Current
		{
			get
			{
				if (HttpPreferences.current == null)
				{
					HttpPreferences.current = HttpPreferences.CreateDefault();
				}
				return HttpPreferences.current;
			}
			set
			{
				HttpPreferences.current = value;
			}
		}
		public string VisualTheme
		{
			get
			{
				return this.visualTheme;
			}
			set
			{
				if (!string.IsNullOrEmpty(value) && !HttpPreferences.CheckVisualTheme(value))
				{
					throw new System.ArgumentException(string.Format("invalid visual theme '{0}'", value));
				}
				this.visualTheme = value;
			}
		}
		public string LocationHotel
		{
			get
			{
				return this.locationHotel;
			}
			set
			{
				this.locationHotel = value;
			}
		}
		public static HttpPreferences CreateDefault()
		{
			return new HttpPreferences
			{
				VisualTheme = ConfigurationManager.AppSettings.AsString("visualThemeDefault", "").ToLower(),
				LocationHotel = null
			};
		}
		public static bool CheckVisualTheme(string value)
		{
			bool result;
			if (string.IsNullOrEmpty(value))
			{
				result = false;
			}
			else
			{
				string[] themes = (
					from m in ConfigurationManager.AppSettings.AsString("visualThemeSupported", "").Split(";,".ToCharArray())
					select m.ToLower()).ToArray<string>();
				result = themes.Contains(value);
			}
			return result;
		}
		public static bool CheckLocationHotel(string value)
		{
			return true;
		}
	}
}
