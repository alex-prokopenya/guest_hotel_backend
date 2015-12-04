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
	public class GuestStrings
	{
		private static System.Resources.ResourceManager resourceMan;
		private static System.Globalization.CultureInfo resourceCulture;
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static System.Resources.ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(GuestStrings.resourceMan, null))
				{
                    var temp = new ResourceManager("GuestService.Resources.GuestStrings", typeof(SharedStrings).Assembly);

                 //   var test= temp.GetString("DepartureNotFound");
                    GuestStrings.resourceMan = temp;
				}
				return GuestStrings.resourceMan;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static System.Globalization.CultureInfo Culture
		{
			get
			{
				return GuestStrings.resourceCulture;
			}
			set
			{
				GuestStrings.resourceCulture = value;
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
                string fileName = System.IO.Path.Combine(GuestService.Notifications.TemplateParser.GetAssemblyDirectory(),"Resources", "GuestStrings." + str + ".resx");

                if (System.IO.File.Exists(fileName))
                {
                    ResXResourceReader rr = new ResXResourceReader(fileName);

                    foreach (DictionaryEntry d in rr)
                        strings[str].Add(d.Key.ToString(), d.Value.ToString());
                }
            }

            if (strings[str].ContainsKey(key))
                return strings[str][key];

            return GuestStrings.ResourceManager.GetString(key, GuestStrings.resourceCulture);
        }


        public static string Authenticate_1
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("Authenticate_1", GuestStrings.resourceCulture);
			}
		}
		public static string Authenticate_2
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("Authenticate_2", GuestStrings.resourceCulture);
			}
		}
		public static string Authenticate_3
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("Authenticate_3", GuestStrings.resourceCulture);
			}
		}
		public static string DepartureBack
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("DepartureBack", GuestStrings.resourceCulture);
			}
		}
		public static string DepartureNotFound
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("DepartureNotFound", GuestStrings.resourceCulture);
			}
		}
		public static string DepartureNotFoundNote_1
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("DepartureNotFoundNote_1", GuestStrings.resourceCulture);
			}
		}
		public static string DepartureNotFoundNote_2
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("DepartureNotFoundNote_2", GuestStrings.resourceCulture);
			}
		}
		public static string DepartureNoTransferFound
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("DepartureNoTransferFound", GuestStrings.resourceCulture);
			}
		}
		public static string DepartureTitle
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("DepartureTitle", GuestStrings.resourceCulture);
			}
		}
		public static string FindOrderChoose_1
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("FindOrderChoose_1", GuestStrings.resourceCulture);
			}
		}
		public static string FindOrderChoose_2
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("FindOrderChoose_2", GuestStrings.resourceCulture);
			}
		}
		public static string FindOrderClaim
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("FindOrderClaim", GuestStrings.resourceCulture);
			}
		}
		public static string FindOrderDeleteButton
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("FindOrderDeleteButton", GuestStrings.resourceCulture);
			}
		}
		public static string FindOrderFindButton
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("FindOrderFindButton", GuestStrings.resourceCulture);
			}
		}
		public static string FindOrderFound
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("FindOrderFound", GuestStrings.resourceCulture);
			}
		}
		public static string FindOrderKnow_1
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("FindOrderKnow_1", GuestStrings.resourceCulture);
			}
		}
		public static string FindOrderKnow_2
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("FindOrderKnow_2", GuestStrings.resourceCulture);
			}
		}
		public static string FindOrderLinkOrderButton
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("FindOrderLinkOrderButton", GuestStrings.resourceCulture);
			}
		}
		public static string FindOrderModel_D_Claim
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("FindOrderModel_D_Claim", GuestStrings.resourceCulture);
			}
		}
		public static string FindOrderModel_N_Claim
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("FindOrderModel_N_Claim", GuestStrings.resourceCulture);
			}
		}
		public static string FindOrderModel_N_ClaimName
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("FindOrderModel_N_ClaimName", GuestStrings.resourceCulture);
			}
		}
		public static string FindOrderModel_N_Passport
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("FindOrderModel_N_Passport", GuestStrings.resourceCulture);
			}
		}
		public static string FindOrderModel_R_Claim
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("FindOrderModel_R_Claim", GuestStrings.resourceCulture);
			}
		}
		public static string FindOrderModel_R_ClaimName
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("FindOrderModel_R_ClaimName", GuestStrings.resourceCulture);
			}
		}
		public static string FindOrderModel_R_Passport
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("FindOrderModel_R_Passport", GuestStrings.resourceCulture);
			}
		}
		public static string FindOrderName
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("FindOrderName", GuestStrings.resourceCulture);
			}
		}
		public static string FindOrderNameSmall
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("FindOrderNameSmall", GuestStrings.resourceCulture);
			}
		}
		public static string FindOrderNoLinkedOrders
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("FindOrderNoLinkedOrders", GuestStrings.resourceCulture);
			}
		}
		public static string FindOrderNotFound
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("FindOrderNotFound", GuestStrings.resourceCulture);
			}
		}
		public static string FindOrderOrderTitle
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("FindOrderOrderTitle", GuestStrings.resourceCulture);
			}
		}
		public static string FindOrderPassSer
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("FindOrderPassSer", GuestStrings.resourceCulture);
			}
		}
		public static string FindOrderTitle
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("FindOrderTitle", GuestStrings.resourceCulture);
			}
		}
		public static string GuestServicesExcursionDepartureTitle
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("GuestServicesExcursionDepartureTitle", GuestStrings.resourceCulture);
			}
		}
		public static string GuestServicesFindOrder_1
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("GuestServicesFindOrder_1", GuestStrings.resourceCulture);
			}
		}
		public static string GuestServicesFindOrder_2
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("GuestServicesFindOrder_2", GuestStrings.resourceCulture);
			}
		}
		public static string GuestServicesFindOrder_3
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("GuestServicesFindOrder_3", GuestStrings.resourceCulture);
			}
		}
		public static string GuestServicesLink
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("GuestServicesLink", GuestStrings.resourceCulture);
			}
		}
		public static string GuestServicesTitle
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("GuestServicesTitle", GuestStrings.resourceCulture);
			}
		}
		public static string GuestServiceTitle
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("GuestServiceTitle", GuestStrings.resourceCulture);
			}
		}
		public static string HotelAlt
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("HotelAlt", GuestStrings.resourceCulture);
			}
		}
		public static string HotelDepartureNoInformation
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("HotelDepartureNoInformation", GuestStrings.resourceCulture);
			}
		}
		public static string HotelDepartureTitle
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("HotelDepartureTitle", GuestStrings.resourceCulture);
			}
		}
		public static string HotelNotFound
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("HotelNotFound", GuestStrings.resourceCulture);
			}
		}
		public static string HotelNotFound_1
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("HotelNotFound_1", GuestStrings.resourceCulture);
			}
		}
		public static string HotelNotFound_Link
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("HotelNotFound_Link", GuestStrings.resourceCulture);
			}
		}
		public static string HotelTitle
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("HotelTitle", GuestStrings.resourceCulture);
			}
		}
		public static string MenuBookingAlt
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("MenuBookingAlt", GuestStrings.resourceCulture);
			}
		}
		public static string MenuBookingText
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("MenuBookingText", GuestStrings.resourceCulture);
			}
		}
		public static string MenuBookingTitle
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("MenuBookingTitle", GuestStrings.resourceCulture);
			}
		}
		public static string MenuDepartureAlt
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("MenuDepartureAlt", GuestStrings.resourceCulture);
			}
		}
		public static string MenuDepartureText
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("MenuDepartureText", GuestStrings.resourceCulture);
			}
		}
		public static string MenuDepartureTitle
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("MenuDepartureTitle", GuestStrings.resourceCulture);
			}
		}
		public static string MenuExcursionAlt
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("MenuExcursionAlt", GuestStrings.resourceCulture);
			}
		}
		public static string MenuExcursionText
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("MenuExcursionText", GuestStrings.resourceCulture);
			}
		}
		public static string MenuExcursionTitle
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("MenuExcursionTitle", GuestStrings.resourceCulture);
			}
		}
		public static string MenuInformationAlt
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("MenuInformationAlt", GuestStrings.resourceCulture);
			}
		}
		public static string MenuInformationText
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("MenuInformationText", GuestStrings.resourceCulture);
			}
		}
		public static string MenuInformationTitle
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("MenuInformationTitle", GuestStrings.resourceCulture);
			}
		}
		public static string MenuOrdersAlt
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("MenuOrdersAlt", GuestStrings.resourceCulture);
			}
		}
		public static string MenuOrdersText
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("MenuOrdersText", GuestStrings.resourceCulture);
			}
		}
		public static string MenuOrdersTitle
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("MenuOrdersTitle", GuestStrings.resourceCulture);
			}
		}
		public static string MoreOrderAlt
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("MoreOrderAlt", GuestStrings.resourceCulture);
			}
		}
		public static string MoreOrdersFindButton
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("MoreOrdersFindButton", GuestStrings.resourceCulture);
			}
		}
		public static string MoreOrdersInstruction
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("MoreOrdersInstruction", GuestStrings.resourceCulture);
			}
		}
		public static string MoreOrdersListOrderAlt
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("MoreOrdersListOrderAlt", GuestStrings.resourceCulture);
			}
		}
		public static string MoreOrderTitle
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("MoreOrderTitle", GuestStrings.resourceCulture);
			}
		}
		public static string OrderDoPaymentButton
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("OrderDoPaymentButton", GuestStrings.resourceCulture);
			}
		}
		public static string OrderInfoTitle
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("OrderInfoTitle", GuestStrings.resourceCulture);
			}
		}
		public static string OrderToPay
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("OrderToPay", GuestStrings.resourceCulture);
			}
		}
		public static string OrderTotal
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("OrderTotal", GuestStrings.resourceCulture);
			}
		}
		public static string PrintOrderBuildVoucherButton
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("PrintOrderBuildVoucherButton", GuestStrings.resourceCulture);
			}
		}
		public static string PrintOrderConfirmCaption
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("PrintOrderConfirmCaption", GuestStrings.resourceCulture);
			}
		}
		public static string PrintOrderModel_Claim
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("PrintOrderModel_Claim", GuestStrings.resourceCulture);
			}
		}
		public static string PrintOrderModel_D_Claim
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("PrintOrderModel_D_Claim", GuestStrings.resourceCulture);
			}
		}
		public static string PrintOrderModel_N_Claim
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("PrintOrderModel_N_Claim", GuestStrings.resourceCulture);
			}
		}
		public static string PrintOrderModel_N_Name
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("PrintOrderModel_N_Name", GuestStrings.resourceCulture);
			}
		}
		public static string PrintOrderModel_Name_1
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("PrintOrderModel_Name_1", GuestStrings.resourceCulture);
			}
		}
		public static string PrintOrderModel_Name_2
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("PrintOrderModel_Name_2", GuestStrings.resourceCulture);
			}
		}
		public static string PrintOrderModel_R_Claim
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("PrintOrderModel_R_Claim", GuestStrings.resourceCulture);
			}
		}
		public static string PrintOrderModel_R_Name
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("PrintOrderModel_R_Name", GuestStrings.resourceCulture);
			}
		}
		public static string PrintOrderNotFound
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("PrintOrderNotFound", GuestStrings.resourceCulture);
			}
		}
		public static string PrintOrderTitle
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("PrintOrderTitle", GuestStrings.resourceCulture);
			}
		}
		public static string PrintVoucherButton
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("PrintVoucherButton", GuestStrings.resourceCulture);
			}
		}
		public static string String1
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("String1", GuestStrings.resourceCulture);
			}
		}
		public static string SummaryCheckAnother
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("SummaryCheckAnother", GuestStrings.resourceCulture);
			}
		}
		public static string SummaryFindButton
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("SummaryFindButton", GuestStrings.resourceCulture);
			}
		}
		public static string SummaryFindReservationTitle
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("SummaryFindReservationTitle", GuestStrings.resourceCulture);
			}
		}
		public static string SummaryNameLabel
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("SummaryNameLabel", GuestStrings.resourceCulture);
			}
		}
		public static string SummaryNameLabel_1
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("SummaryNameLabel_1", GuestStrings.resourceCulture);
			}
		}
		public static string SummaryNotFound
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("SummaryNotFound", GuestStrings.resourceCulture);
			}
		}
		public static string SummaryReservationLabel
		{
			get
			{
				return GuestStrings.ResourceManager.GetString("SummaryReservationLabel", GuestStrings.resourceCulture);
			}
		}
		internal GuestStrings()
		{
		}
	}
}
