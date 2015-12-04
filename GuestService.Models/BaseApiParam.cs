using Sm.System.Mvc.Language;
using System;
namespace GuestService.Models
{
	public class BaseApiParam
	{
		public string ln
		{
			get;
			set;
		}
		public string Language
		{
			get
			{
				return (!string.IsNullOrEmpty(this.ln)) ? this.ln : UrlLanguage.CurrentLanguage;
			}
		}
	}
}
