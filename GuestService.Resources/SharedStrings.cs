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
	public class SharedStrings
	{
		private static System.Resources.ResourceManager resourceMan;
		private static System.Globalization.CultureInfo resourceCulture;
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static System.Resources.ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(SharedStrings.resourceMan, null))
				{
					System.Resources.ResourceManager temp = new System.Resources.ResourceManager("GuestService.Resources.SharedStrings", typeof(SharedStrings).Assembly);
					SharedStrings.resourceMan = temp;
				}
				return SharedStrings.resourceMan;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static System.Globalization.CultureInfo Culture
		{
			get
			{
				return SharedStrings.resourceCulture;
			}
			set
			{
				SharedStrings.resourceCulture = value;
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
                string fileName = System.IO.Path.Combine(GuestService.Notifications.TemplateParser.GetAssemblyDirectory(), "Resources", "SharedStrings." + str + ".resx");

                if (System.IO.File.Exists(fileName))
                {
                    ResXResourceReader rr = new ResXResourceReader(fileName);

                    foreach (DictionaryEntry d in rr)
                        strings[str].Add(d.Key.ToString(), d.Value.ToString());
                }
            }

            if (strings[str].ContainsKey(key))
                return strings[str][key];

            return SharedStrings.ResourceManager.GetString(key, SharedStrings.resourceCulture);
        }


        public static string DepartureHotel_Flight
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("DepartureHotel_Flight", SharedStrings.resourceCulture);
			}
		}
		public static string DepartureHotel_FlightDestination
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("DepartureHotel_FlightDestination", SharedStrings.resourceCulture);
			}
		}
		public static string DepartureHotel_FlightSource
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("DepartureHotel_FlightSource", SharedStrings.resourceCulture);
			}
		}
		public static string DepartureHotel_GuestList
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("DepartureHotel_GuestList", SharedStrings.resourceCulture);
			}
		}
		public static string DepartureHotel_Guide
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("DepartureHotel_Guide", SharedStrings.resourceCulture);
			}
		}
		public static string DepartureHotel_Guide2
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("DepartureHotel_Guide2", SharedStrings.resourceCulture);
			}
		}
		public static string DepartureHotel_GuidePhone
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("DepartureHotel_GuidePhone", SharedStrings.resourceCulture);
			}
		}
		public static string DepartureHotel_Hotel
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("DepartureHotel_Hotel", SharedStrings.resourceCulture);
			}
		}
		public static string DepartureHotel_HotelRegion
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("DepartureHotel_HotelRegion", SharedStrings.resourceCulture);
			}
		}
		public static string DepartureHotel_HotelTown
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("DepartureHotel_HotelTown", SharedStrings.resourceCulture);
			}
		}
		public static string DepartureHotel_TransferBus
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("DepartureHotel_TransferBus", SharedStrings.resourceCulture);
			}
		}
		public static string DepartureHotel_TransferDate
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("DepartureHotel_TransferDate", SharedStrings.resourceCulture);
			}
		}
		public static string DepartureHotel_TransferIndividual
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("DepartureHotel_TransferIndividual", SharedStrings.resourceCulture);
			}
		}
		public static string DepartureHotel_TransferNote
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("DepartureHotel_TransferNote", SharedStrings.resourceCulture);
			}
		}
		public static string DepartureHotel_TransferTime
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("DepartureHotel_TransferTime", SharedStrings.resourceCulture);
			}
		}
		public static string Error_MainPageLink
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("Error_MainPageLink", SharedStrings.resourceCulture);
			}
		}
		public static string Error_Text
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("Error_Text", SharedStrings.resourceCulture);
			}
		}
		public static string Error_Title
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("Error_Title", SharedStrings.resourceCulture);
			}
		}
		public static string Error404_Departure
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("Error404_Departure", SharedStrings.resourceCulture);
			}
		}
		public static string Error404_Excursion
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("Error404_Excursion", SharedStrings.resourceCulture);
			}
		}
		public static string Error404_GuestService
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("Error404_GuestService", SharedStrings.resourceCulture);
			}
		}
		public static string Error404_Reservation
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("Error404_Reservation", SharedStrings.resourceCulture);
			}
		}
		public static string Error404_Text
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("Error404_Text", SharedStrings.resourceCulture);
			}
		}
		public static string Error404_Title
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("Error404_Title", SharedStrings.resourceCulture);
			}
		}
		public static string ErrorSummary_Title
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ErrorSummary_Title", SharedStrings.resourceCulture);
			}
		}
		public static string ExcursionTransfer_Date
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ExcursionTransfer_Date", SharedStrings.resourceCulture);
			}
		}
		public static string ExcursionTransfer_Guide2Name
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ExcursionTransfer_Guide2Name", SharedStrings.resourceCulture);
			}
		}
		public static string ExcursionTransfer_Guide2Phone
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ExcursionTransfer_Guide2Phone", SharedStrings.resourceCulture);
			}
		}
		public static string ExcursionTransfer_GuideName
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ExcursionTransfer_GuideName", SharedStrings.resourceCulture);
			}
		}
		public static string ExcursionTransfer_GuidePhone
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ExcursionTransfer_GuidePhone", SharedStrings.resourceCulture);
			}
		}
		public static string ExcursionTransfer_PickupPlace
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ExcursionTransfer_PickupPlace", SharedStrings.resourceCulture);
			}
		}
		public static string ExcursionTransfer_PickupTime
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ExcursionTransfer_PickupTime", SharedStrings.resourceCulture);
			}
		}
		public static string HotelGuide_HotelDuty
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("HotelGuide_HotelDuty", SharedStrings.resourceCulture);
			}
		}
		public static string HotelGuide_Phone
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("HotelGuide_Phone", SharedStrings.resourceCulture);
			}
		}
		public static string NavigatorLogout
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("NavigatorLogout", SharedStrings.resourceCulture);
			}
		}
		public static string NavigatorMyOrders
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("NavigatorMyOrders", SharedStrings.resourceCulture);
			}
		}
		public static string NavigatorTitle
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("NavigatorTitle", SharedStrings.resourceCulture);
			}
		}
		public static string ReservationOrder_Adult
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ReservationOrder_Adult", SharedStrings.resourceCulture);
			}
		}
		public static string ReservationOrder_Child
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ReservationOrder_Child", SharedStrings.resourceCulture);
			}
		}
		public static string ReservationOrder_ExcursionAlt
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ReservationOrder_ExcursionAlt", SharedStrings.resourceCulture);
			}
		}
		public static string ReservationOrder_ExcursionLang
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ReservationOrder_ExcursionLang", SharedStrings.resourceCulture);
			}
		}
		public static string ReservationOrder_ExcursionTime
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ReservationOrder_ExcursionTime", SharedStrings.resourceCulture);
			}
		}
		public static string ReservationOrder_FreightAlt
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ReservationOrder_FreightAlt", SharedStrings.resourceCulture);
			}
		}
		public static string ReservationOrder_FreightArrival
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ReservationOrder_FreightArrival", SharedStrings.resourceCulture);
			}
		}
		public static string ReservationOrder_FreightClass
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ReservationOrder_FreightClass", SharedStrings.resourceCulture);
			}
		}
		public static string ReservationOrder_FreightDeparture
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ReservationOrder_FreightDeparture", SharedStrings.resourceCulture);
			}
		}
		public static string ReservationOrder_FreightSeat
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ReservationOrder_FreightSeat", SharedStrings.resourceCulture);
			}
		}
		public static string ReservationOrder_HotelAlt
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ReservationOrder_HotelAlt", SharedStrings.resourceCulture);
			}
		}
		public static string ReservationOrder_Infant
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ReservationOrder_Infant", SharedStrings.resourceCulture);
			}
		}
		public static string ReservationOrder_Meal
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ReservationOrder_Meal", SharedStrings.resourceCulture);
			}
		}
		public static string ReservationOrder_Note
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ReservationOrder_Note", SharedStrings.resourceCulture);
			}
		}
		public static string ReservationOrder_Room
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ReservationOrder_Room", SharedStrings.resourceCulture);
			}
		}
		public static string ReservationOrder_ServiceAlt
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ReservationOrder_ServiceAlt", SharedStrings.resourceCulture);
			}
		}
		public static string ReservationOrder_ServiceType
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ReservationOrder_ServiceType", SharedStrings.resourceCulture);
			}
		}
		public static string ReservationOrder_TransferAlt
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ReservationOrder_TransferAlt", SharedStrings.resourceCulture);
			}
		}
		public static string ToolbarGuestOrderLink
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ToolbarGuestOrderLink", SharedStrings.resourceCulture);
			}
		}
		public static string ToolbarSignIn
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ToolbarSignIn", SharedStrings.resourceCulture);
			}
		}
		public static string ValidationSummary_Title
		{
			get
			{
				return SharedStrings.ResourceManager.GetString("ValidationSummary_Title", SharedStrings.resourceCulture);
			}
		}
		internal SharedStrings()
		{
		}
	}
}
