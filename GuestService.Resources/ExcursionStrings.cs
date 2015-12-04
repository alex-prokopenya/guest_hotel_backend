using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

using Sm.System.Mvc.Language;
using System.Collections.Generic;
using System.Collections;


namespace GuestService.Resources
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), System.Diagnostics.DebuggerNonUserCode, System.Runtime.CompilerServices.CompilerGenerated]
	public class ExcursionStrings
	{
		private static System.Resources.ResourceManager resourceMan;
		private static System.Globalization.CultureInfo resourceCulture;
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static System.Resources.ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(ExcursionStrings.resourceMan, null))
				{
					System.Resources.ResourceManager temp = new System.Resources.ResourceManager("GuestService.Resources.ExcursionStrings", typeof(SharedStrings).Assembly);
					ExcursionStrings.resourceMan = temp;
				}
				return ExcursionStrings.resourceMan;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static System.Globalization.CultureInfo Culture
		{
			get
			{
				return ExcursionStrings.resourceCulture;
			}
			set
			{
				ExcursionStrings.resourceCulture = value;
			}
		}

        private static Dictionary<string, Dictionary<string, string>> strings = new Dictionary<string, Dictionary<string, string>>();

        public static string Get(string key)
        {
            var str = UrlLanguage.CurrentLanguage;

            if (!strings.ContainsKey(str))
            {
                strings[str] = new Dictionary<string, string>();

                //загрузить строки из xml
                string fileName = System.IO.Path.Combine(GuestService.Notifications.TemplateParser.GetAssemblyDirectory(), "Resources", "ExcursionStrings." + str + ".resx");

                if (System.IO.File.Exists(fileName))
                {
                    ResXResourceReader rr = new ResXResourceReader(fileName);

                    foreach (DictionaryEntry d in rr)
                        strings[str].Add(d.Key.ToString(), d.Value.ToString());
                }
            }

            if (strings[str].ContainsKey(key))
                return strings[str][key];

            return ExcursionStrings.ResourceManager.GetString(key, ExcursionStrings.resourceCulture);
        }

        public static string BookButton
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("BookButton", ExcursionStrings.resourceCulture);
			}
		}
		public static string BookNowButton
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("BookNowButton", ExcursionStrings.resourceCulture);
			}
		}
		public static string DateTimeFormat
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DateTimeFormat", ExcursionStrings.resourceCulture);
			}
		}
		public static string DateTimeLanguage
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DateTimeLanguage", ExcursionStrings.resourceCulture);
			}
		}
		public static string DeatilGroupSize
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DeatilGroupSize", ExcursionStrings.resourceCulture);
			}
		}
		public static string DepartureLabel
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DepartureLabel", ExcursionStrings.resourceCulture);
			}
		}
		public static string DescriptionInfo_Categories
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DescriptionInfo_Categories", ExcursionStrings.resourceCulture);
			}
		}
		public static string DescriptionInfo_Direction
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DescriptionInfo_Direction", ExcursionStrings.resourceCulture);
			}
		}
		public static string DescriptionInfo_Duration
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DescriptionInfo_Duration", ExcursionStrings.resourceCulture);
			}
		}
		public static string DescriptionInfo_Language
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DescriptionInfo_Language", ExcursionStrings.resourceCulture);
			}
		}
		public static string DescriptionInfo_Partner
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DescriptionInfo_Partner", ExcursionStrings.resourceCulture);
			}
		}
		public static string DetailAdult
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DetailAdult", ExcursionStrings.resourceCulture);
			}
		}
		public static string DetailChild
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DetailChild", ExcursionStrings.resourceCulture);
			}
		}
		public static string DetailDateTitle
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DetailDateTitle", ExcursionStrings.resourceCulture);
			}
		}
		public static string DetailDeparture
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DetailDeparture", ExcursionStrings.resourceCulture);
			}
		}
		public static string DetailDescription
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DetailDescription", ExcursionStrings.resourceCulture);
			}
		}
		public static string DetailExcursionDateTitle
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DetailExcursionDateTitle", ExcursionStrings.resourceCulture);
			}
		}
		public static string DetailInfant
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DetailInfant", ExcursionStrings.resourceCulture);
			}
		}
		public static string DetailMapExpand
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DetailMapExpand", ExcursionStrings.resourceCulture);
			}
		}
		public static string DetailOnsaleTill
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DetailOnsaleTill", ExcursionStrings.resourceCulture);
			}
		}
		public static string DetailPrice
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DetailPrice", ExcursionStrings.resourceCulture);
			}
		}
		public static string DetailPriceTitle
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DetailPriceTitle", ExcursionStrings.resourceCulture);
			}
		}
		public static string DetailReturnToExcursionDate
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DetailReturnToExcursionDate", ExcursionStrings.resourceCulture);
			}
		}
		public static string DetailReturnToExcursionList
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DetailReturnToExcursionList", ExcursionStrings.resourceCulture);
			}
		}
		public static string DetailSelectedDateTitle
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DetailSelectedDateTitle", ExcursionStrings.resourceCulture);
			}
		}
		public static string DetailServicePrice
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DetailServicePrice", ExcursionStrings.resourceCulture);
			}
		}
		public static string DurationAlt
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DurationAlt", ExcursionStrings.resourceCulture);
			}
		}
		public static string DurationDay
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DurationDay", ExcursionStrings.resourceCulture);
			}
		}
		public static string DurationDays
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DurationDays", ExcursionStrings.resourceCulture);
			}
		}
		public static string DurationHour
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DurationHour", ExcursionStrings.resourceCulture);
			}
		}
		public static string DurationHours
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DurationHours", ExcursionStrings.resourceCulture);
			}
		}
		public static string DurationMin
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DurationMin", ExcursionStrings.resourceCulture);
			}
		}
		public static string DurationMins
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("DurationMins", ExcursionStrings.resourceCulture);
			}
		}
		public static string ErrorGuestCount
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("ErrorGuestCount", ExcursionStrings.resourceCulture);
			}
		}
		public static string ErrorInvalidParams
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("ErrorInvalidParams", ExcursionStrings.resourceCulture);
			}
		}
		public static string ErrorSummary
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("ErrorSummary", ExcursionStrings.resourceCulture);
			}
		}
		public static string ExcursionLanguage
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("ExcursionLanguage", ExcursionStrings.resourceCulture);
			}
		}
		public static string ExcursionMapMarkerDeparture
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("ExcursionMapMarkerDeparture", ExcursionStrings.resourceCulture);
			}
		}
		public static string ExcursionMapMarkerLocation
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("ExcursionMapMarkerLocation", ExcursionStrings.resourceCulture);
			}
		}
		public static string ExcursionNotFound
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("ExcursionNotFound", ExcursionStrings.resourceCulture);
			}
		}
		public static string ExcursionRegionTitle
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("ExcursionRegionTitle", ExcursionStrings.resourceCulture);
			}
		}
		public static string ExcursionTitle
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("ExcursionTitle", ExcursionStrings.resourceCulture);
			}
		}
		public static string ExtraFilters
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("ExtraFilters", ExcursionStrings.resourceCulture);
			}
		}
		public static string ExtraFiltersAllDates
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("ExtraFiltersAllDates", ExcursionStrings.resourceCulture);
			}
		}
		public static string ExtraFiltersCategory
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("ExtraFiltersCategory", ExcursionStrings.resourceCulture);
			}
		}
		public static string ExtraFiltersDate
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("ExtraFiltersDate", ExcursionStrings.resourceCulture);
			}
		}
		public static string ExtraFiltersDepartures
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("ExtraFiltersDepartures", ExcursionStrings.resourceCulture);
			}
		}
		public static string ExtraFiltersDirection
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("ExtraFiltersDirection", ExcursionStrings.resourceCulture);
			}
		}
		public static string ExtraFiltersLanguage
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("ExtraFiltersLanguage", ExcursionStrings.resourceCulture);
			}
		}
		public static string GeorgaphyTitle
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("GeorgaphyTitle", ExcursionStrings.resourceCulture);
			}
		}
		public static string GuestServiceTitle
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("GuestServiceTitle", ExcursionStrings.resourceCulture);
			}
		}
		public static string HelpPlease
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("HelpPlease", ExcursionStrings.resourceCulture);
			}
		}
		public static string MapHideLink
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("MapHideLink", ExcursionStrings.resourceCulture);
			}
		}
		public static string MapShowLink
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("MapShowLink", ExcursionStrings.resourceCulture);
			}
		}
		public static string NavigateCategory
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("NavigateCategory", ExcursionStrings.resourceCulture);
			}
		}
		public static string NavigateExcursions
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("NavigateExcursions", ExcursionStrings.resourceCulture);
			}
		}
		public static string OrderAddShopCartButton
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("OrderAddShopCartButton", ExcursionStrings.resourceCulture);
			}
		}
		public static string OrderAddShopCartSuccess
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("OrderAddShopCartSuccess", ExcursionStrings.resourceCulture);
			}
		}
		public static string OrderAdult
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("OrderAdult", ExcursionStrings.resourceCulture);
			}
		}
		public static string OrderChild
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("OrderChild", ExcursionStrings.resourceCulture);
			}
		}
		public static string OrderCloseButton
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("OrderCloseButton", ExcursionStrings.resourceCulture);
			}
		}
		public static string OrderCount
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("OrderCount", ExcursionStrings.resourceCulture);
			}
		}
		public static string OrderFormHelp
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("OrderFormHelp", ExcursionStrings.resourceCulture);
			}
		}
		public static string OrderGoShoppingCart
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("OrderGoShoppingCart", ExcursionStrings.resourceCulture);
			}
		}
		public static string OrderInfant
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("OrderInfant", ExcursionStrings.resourceCulture);
			}
		}
		public static string OrderName
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("OrderName", ExcursionStrings.resourceCulture);
			}
		}
		public static string OrderNote
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("OrderNote", ExcursionStrings.resourceCulture);
			}
		}
		public static string OrderPhone
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("OrderPhone", ExcursionStrings.resourceCulture);
			}
		}
		public static string OrderPickUp
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("OrderPickUp", ExcursionStrings.resourceCulture);
			}
		}
		public static string OrderPriceForPax
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("OrderPriceForPax", ExcursionStrings.resourceCulture);
			}
		}
		public static string OrderPriceForService
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("OrderPriceForService", ExcursionStrings.resourceCulture);
			}
		}
		public static string OrderReturnExcursionList
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("OrderReturnExcursionList", ExcursionStrings.resourceCulture);
			}
		}
		public static string OrderTitle
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("OrderTitle", ExcursionStrings.resourceCulture);
			}
		}
		public static string PartnerAlt
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("PartnerAlt", ExcursionStrings.resourceCulture);
			}
		}
		public static string PriceByPax
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("PriceByPax", ExcursionStrings.resourceCulture);
			}
		}
		public static string PriceByService
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("PriceByService", ExcursionStrings.resourceCulture);
			}
		}
		public static string PriceFrom
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("PriceFrom", ExcursionStrings.resourceCulture);
			}
		}
		public static string PriceNotFound
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("PriceNotFound", ExcursionStrings.resourceCulture);
			}
		}
		public static string SearchDeparture_AnyPoint
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("SearchDeparture_AnyPoint", ExcursionStrings.resourceCulture);
			}
		}
		public static string SearchPlaceholder
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("SearchPlaceholder", ExcursionStrings.resourceCulture);
			}
		}
		public static string SearchTitle
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("SearchTitle", ExcursionStrings.resourceCulture);
			}
		}
		public static string ShowMap
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("ShowMap", ExcursionStrings.resourceCulture);
			}
		}
		public static string SortOrderByName
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("SortOrderByName", ExcursionStrings.resourceCulture);
			}
		}
		public static string SortOrderByPrice
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("SortOrderByPrice", ExcursionStrings.resourceCulture);
			}
		}
		public static string Title
		{
			get
			{
				return ExcursionStrings.ResourceManager.GetString("Title", ExcursionStrings.resourceCulture);
			}
		}
		internal ExcursionStrings()
		{
		}
	}
}
