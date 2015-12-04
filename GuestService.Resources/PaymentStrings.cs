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
	public class PaymentStrings
	{
		private static System.Resources.ResourceManager resourceMan;
		private static System.Globalization.CultureInfo resourceCulture;
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static System.Resources.ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(PaymentStrings.resourceMan, null))
				{
					System.Resources.ResourceManager temp = new System.Resources.ResourceManager("GuestService.Resources.PaymentStrings", typeof(PaymentStrings).Assembly);
					PaymentStrings.resourceMan = temp;
				}
				return PaymentStrings.resourceMan;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static System.Globalization.CultureInfo Culture
		{
			get
			{
				return PaymentStrings.resourceCulture;
			}
			set
			{
				PaymentStrings.resourceCulture = value;
			}
		}

        private static Dictionary<string, Dictionary<string, string>> strings = new Dictionary<string, Dictionary<string, string>>();

        public static string Get(string key)
        {
            var str = UrlLanguage.CurrentLanguage;

            if (!strings.ContainsKey(str))
            {
                strings[str] = new Dictionary<string, string>();

                string fileName = System.IO.Path.Combine(GuestService.Notifications.TemplateParser.GetAssemblyDirectory(), "Resources", "PaymentStrings." + str + ".resx");

                if (System.IO.File.Exists(fileName))
                {
                    ResXResourceReader rr = new ResXResourceReader(fileName);

                    foreach (DictionaryEntry d in rr)
                        strings[str].Add(d.Key.ToString(), d.Value.ToString());
                }
            }

            if (strings[str].ContainsKey(key))
                return strings[str][key];

            return PaymentStrings.ResourceManager.GetString(key, PaymentStrings.resourceCulture);
        }

        public static string PaymentAfterConfirmationMessage
		{
			get
			{
				return PaymentStrings.ResourceManager.GetString("PaymentAfterConfirmationMessage", PaymentStrings.resourceCulture);
			}
		}
		public static string PaymentAlreadyPaid
		{
			get
			{
				return PaymentStrings.ResourceManager.GetString("PaymentAlreadyPaid", PaymentStrings.resourceCulture);
			}
		}
		public static string PaymentBankBill
		{
			get
			{
				return PaymentStrings.ResourceManager.GetString("PaymentBankBill", PaymentStrings.resourceCulture);
			}
		}
		public static string PaymentCancelled
		{
			get
			{
				return PaymentStrings.ResourceManager.GetString("PaymentCancelled", PaymentStrings.resourceCulture);
			}
		}
		public static string PaymentCannotPayOrder
		{
			get
			{
				return PaymentStrings.ResourceManager.GetString("PaymentCannotPayOrder", PaymentStrings.resourceCulture);
			}
		}
		public static string PaymentComission
		{
			get
			{
				return PaymentStrings.ResourceManager.GetString("PaymentComission", PaymentStrings.resourceCulture);
			}
		}
		public static string PaymentForOrderFormat
		{
			get
			{
				return PaymentStrings.ResourceManager.GetString("PaymentForOrderFormat", PaymentStrings.resourceCulture);
			}
		}
		public static string PaymentGuestService
		{
			get
			{
				return PaymentStrings.ResourceManager.GetString("PaymentGuestService", PaymentStrings.resourceCulture);
			}
		}
		public static string PaymentListTitle
		{
			get
			{
				return PaymentStrings.ResourceManager.GetString("PaymentListTitle", PaymentStrings.resourceCulture);
			}
		}
		public static string PaymentMainPageLink
		{
			get
			{
				return PaymentStrings.ResourceManager.GetString("PaymentMainPageLink", PaymentStrings.resourceCulture);
			}
		}
		public static string PaymentMethod
		{
			get
			{
				return PaymentStrings.ResourceManager.GetString("PaymentMethod", PaymentStrings.resourceCulture);
			}
		}
		public static string PaymentOrderTitle
		{
			get
			{
				return PaymentStrings.ResourceManager.GetString("PaymentOrderTitle", PaymentStrings.resourceCulture);
			}
		}
		public static string PaymentPayButton
		{
			get
			{
				return PaymentStrings.ResourceManager.GetString("PaymentPayButton", PaymentStrings.resourceCulture);
			}
		}
		public static string PaymentRedirectUniteller
		{
			get
			{
				return PaymentStrings.ResourceManager.GetString("PaymentRedirectUniteller", PaymentStrings.resourceCulture);
			}
		}
		public static string PaymentReservationCannotPay
		{
			get
			{
				return PaymentStrings.ResourceManager.GetString("PaymentReservationCannotPay", PaymentStrings.resourceCulture);
			}
		}
		public static string PaymentReservationNotFound
		{
			get
			{
				return PaymentStrings.ResourceManager.GetString("PaymentReservationNotFound", PaymentStrings.resourceCulture);
			}
		}
		public static string PaymentReservationStatusTitle
		{
			get
			{
				return PaymentStrings.ResourceManager.GetString("PaymentReservationStatusTitle", PaymentStrings.resourceCulture);
			}
		}
		public static string PaymentResultError
		{
			get
			{
				return PaymentStrings.ResourceManager.GetString("PaymentResultError", PaymentStrings.resourceCulture);
			}
		}
		public static string PaymentResultOK
		{
			get
			{
				return PaymentStrings.ResourceManager.GetString("PaymentResultOK", PaymentStrings.resourceCulture);
			}
		}
		public static string PaymentTitle
		{
			get
			{
				return PaymentStrings.ResourceManager.GetString("PaymentTitle", PaymentStrings.resourceCulture);
			}
		}
		public static string PaymentToPay
		{
			get
			{
				return PaymentStrings.ResourceManager.GetString("PaymentToPay", PaymentStrings.resourceCulture);
			}
		}
		public static string PaymentTotal
		{
			get
			{
				return PaymentStrings.ResourceManager.GetString("PaymentTotal", PaymentStrings.resourceCulture);
			}
		}
		internal PaymentStrings()
		{
		}
	}
}
