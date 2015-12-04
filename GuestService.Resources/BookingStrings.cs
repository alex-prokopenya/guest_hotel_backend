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
	public class BookingStrings
	{
		private static System.Resources.ResourceManager resourceMan;
		private static System.Globalization.CultureInfo resourceCulture;
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static System.Resources.ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(BookingStrings.resourceMan, null))
				{
					System.Resources.ResourceManager temp = new System.Resources.ResourceManager("GuestService.Resources.BookingStrings", typeof(BookingStrings).Assembly);
					BookingStrings.resourceMan = temp;
				}
				return BookingStrings.resourceMan;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static System.Globalization.CultureInfo Culture
		{
			get
			{
				return BookingStrings.resourceCulture;
			}
			set
			{
				BookingStrings.resourceCulture = value;
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
                string fileName = System.IO.Path.Combine(GuestService.Notifications.TemplateParser.GetAssemblyDirectory(), "Resources", "BookingStrings." + str + ".resx");

                if (System.IO.File.Exists(fileName))
                {
                    ResXResourceReader rr = new ResXResourceReader(fileName);

                    foreach (DictionaryEntry d in rr)
                        strings[str].Add(d.Key.ToString(), d.Value.ToString());
                }
            }

            if (strings[str].ContainsKey(key))
                return strings[str][key];

            return BookingStrings.ResourceManager.GetString(key, BookingStrings.resourceCulture);
        }

        public static string BookingAgreementCancel
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingAgreementCancel", BookingStrings.resourceCulture);
			}
		}
		public static string BookingAgreementConfirm
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingAgreementConfirm", BookingStrings.resourceCulture);
			}
		}
		public static string BookingAgreementConfirmMessage_1
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingAgreementConfirmMessage_1", BookingStrings.resourceCulture);
			}
		}
		public static string BookingAgreementConfirmMessage_2
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingAgreementConfirmMessage_2", BookingStrings.resourceCulture);
			}
		}
		public static string BookingAgreementTitle
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingAgreementTitle", BookingStrings.resourceCulture);
			}
		}
		public static string BookingErrorTitle
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingErrorTitle", BookingStrings.resourceCulture);
			}
		}
		public static string BookingFormAddress
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingFormAddress", BookingStrings.resourceCulture);
			}
		}
		public static string BookingFormMail
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingFormMail", BookingStrings.resourceCulture);
			}
		}
		public static string BookingFormName
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingFormName", BookingStrings.resourceCulture);
			}
		}
		public static string BookingFormNote
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingFormNote", BookingStrings.resourceCulture);
			}
		}
		public static string BookingFormPhone
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingFormPhone", BookingStrings.resourceCulture);
			}
		}
		public static string BookingInProcess
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingInProcess", BookingStrings.resourceCulture);
			}
		}
		public static string BookingMenuTitle
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingMenuTitle", BookingStrings.resourceCulture);
			}
		}
		public static string BookingModel_R_CustomerMobile
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingModel_R_CustomerMobile", BookingStrings.resourceCulture);
			}
		}
		public static string BookingModel_R_CustomerName
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingModel_R_CustomerName", BookingStrings.resourceCulture);
			}
		}
		public static string BookingModel_R_UserEmail
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingModel_R_UserEmail", BookingStrings.resourceCulture);
			}
		}
		public static string BookingPayButton
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingPayButton", BookingStrings.resourceCulture);
			}
		}
		public static string BookingProcessing_OrderTitle_1
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingProcessing_OrderTitle_1", BookingStrings.resourceCulture);
			}
		}
		public static string BookingProcessing_OrderTitle_2
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingProcessing_OrderTitle_2", BookingStrings.resourceCulture);
			}
		}
		public static string BookingProcessing_Title
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingProcessing_Title", BookingStrings.resourceCulture);
			}
		}
		public static string BookingResultText_1
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingResultText_1", BookingStrings.resourceCulture);
			}
		}
		public static string BookingResultText_2
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingResultText_2", BookingStrings.resourceCulture);
			}
		}
		public static string BookingResultTitle
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingResultTitle", BookingStrings.resourceCulture);
			}
		}
		public static string BookingReturnToOrder
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookingReturnToOrder", BookingStrings.resourceCulture);
			}
		}
		public static string BookNowButton
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("BookNowButton", BookingStrings.resourceCulture);
			}
		}
		public static string EmptyCart_1
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("EmptyCart_1", BookingStrings.resourceCulture);
			}
		}
		public static string EmptyCart_2
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("EmptyCart_2", BookingStrings.resourceCulture);
			}
		}
		public static string ErrorMessageTitle
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("ErrorMessageTitle", BookingStrings.resourceCulture);
			}
		}
		public static string GoBack
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("GoBack", BookingStrings.resourceCulture);
			}
		}
		public static string GuestServiceTitle
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("GuestServiceTitle", BookingStrings.resourceCulture);
			}
		}
		public static string OrderTotalLabel
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("OrderTotalLabel", BookingStrings.resourceCulture);
			}
		}
		public static string RemoveOrderCancelButton
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("RemoveOrderCancelButton", BookingStrings.resourceCulture);
			}
		}
		public static string RemoveOrderConfirmButton
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("RemoveOrderConfirmButton", BookingStrings.resourceCulture);
			}
		}
		public static string RemoveOrderConfirmText
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("RemoveOrderConfirmText", BookingStrings.resourceCulture);
			}
		}
		public static string RemoveOrderConfirmTitle
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("RemoveOrderConfirmTitle", BookingStrings.resourceCulture);
			}
		}
		public static string RemoveOrderLink
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("RemoveOrderLink", BookingStrings.resourceCulture);
			}
		}
		public static string RulesAccepted
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("RulesAccepted", BookingStrings.resourceCulture);
			}
		}
		public static string Title
		{
			get
			{
				return BookingStrings.ResourceManager.GetString("Title", BookingStrings.resourceCulture);
			}
		}
		internal BookingStrings()
		{
		}
	}
}
