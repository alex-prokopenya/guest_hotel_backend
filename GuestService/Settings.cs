using Sm.System.SettingsExtension;
using System;
using System.Configuration;
namespace GuestService
{
	public static class Settings
	{

        public static string PartnerReportUrl
        {
            get
            {
                return ConfigurationManager.AppSettings.AsString("ReportGeneratorUrl", "http://exgo.com:8081/MyReportPerpetuum.aspx");
            }
        }

        public static int ExcursionGeographySearchLimit
		{
			get
			{
				return ConfigurationManager.AppSettings.AsInt("excursionGeographySearchLimit", 50);
			}
		}
		public static string ExcursionDefaultPartnerAlias
		{
			get
			{
				return ConfigurationManager.AppSettings.AsString("excursionDefaultPartnerAlias", null);
			}
		}
		public static bool ExcursionWithPriceOnlyCatalog
		{
			get
			{
				return ConfigurationManager.AppSettings.AsBool("excursionWithPriceOnlyCatalog", false);
			}
		}
		public static int ExcursionDefaultDate
		{
			get
			{
				return ConfigurationManager.AppSettings.AsInt("excursionDefaultDate", 0);
			}
		}
		public static int ExcursionCheckAvailabilityDays
		{
			get
			{
				return ConfigurationManager.AppSettings.AsInt("excursionCheckAvailabilityDays", 100);
			}
		}
		public static PaymentPaypalSettings PaymentPaypal
		{
			get
			{
				PaymentPaypalSettings settings = new PaymentPaypalSettings();
				settings.Username = ConfigurationManager.AppSettings.AsString("paymentPaypalUsername", null);
				settings.Password = ConfigurationManager.AppSettings.AsString("paymentPaypalPassword", null);
				settings.Signature = ConfigurationManager.AppSettings.AsString("paymentPaypalSinature", null);
				settings.IsSandbox = ConfigurationManager.AppSettings.AsBool("paymentPaypalSandbox", false);
				return (!string.IsNullOrEmpty(settings.Username) && !string.IsNullOrEmpty(settings.Password) && !string.IsNullOrEmpty(settings.Signature)) ? settings : null;
			}
		}
		public static string GuestDefaultPage
		{
			get
			{
				return ConfigurationManager.AppSettings.AsString("guestDefaultPage", "index");
			}
		}
		public static bool IsShowBreadCrumb
		{
			get
			{
				return ConfigurationManager.AppSettings.AsBool("isShowBreadCrumb", true);
			}
		}
		public static bool IsShowHotelGuideInfo
		{
			get
			{
				return ConfigurationManager.AppSettings.AsBool("isShowHotelGuideInfo", true);
			}
		}
		public static bool IsHideDeparturePoints
		{
			get
			{
				return ConfigurationManager.AppSettings.AsBool("isHideDeparturePoints", false);
			}
		}
		public static bool IsAddRankingEnabled
		{
			get
			{
				return ConfigurationManager.AppSettings.AsBool("isAddRankingEnabled", false);
			}
		}
		public static bool IsCacheDisabled
		{
			get
			{
				return ConfigurationManager.AppSettings.AsBool("isCacheDisabled", false);
			}
		}
	}
}
