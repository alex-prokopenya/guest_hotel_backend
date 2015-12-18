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
	public class AccountStrings
	{
		private static System.Resources.ResourceManager resourceMan;
		private static System.Globalization.CultureInfo resourceCulture;
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static System.Resources.ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(AccountStrings.resourceMan, null))
				{
					System.Resources.ResourceManager temp = new System.Resources.ResourceManager("GuestService.Resources.AccountStrings", typeof(AccountStrings).Assembly);
					AccountStrings.resourceMan = temp;
				}
				return AccountStrings.resourceMan;
			}
		}
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static System.Globalization.CultureInfo Culture
		{
			get
			{
				return AccountStrings.resourceCulture;
			}
			set
			{
				AccountStrings.resourceCulture = value;
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
                string fileName = System.IO.Path.Combine(GuestService.Notifications.TemplateParser.GetAssemblyDirectory(), "Resources", "AccountStrings." + str + ".resx");

                if (System.IO.File.Exists(fileName))
                {
                    ResXResourceReader rr = new ResXResourceReader(fileName);

                    foreach (DictionaryEntry d in rr)
                        strings[str].Add(d.Key.ToString(), d.Value.ToString());
                }
            }

            if (strings[str].ContainsKey(key))
                return strings[str][key];

            return AccountStrings.ResourceManager.GetString(key, AccountStrings.resourceCulture);
        }

        public static string AccountLogin_EmailNotConfirmed
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("AccountLogin_EmailNotConfirmed", AccountStrings.resourceCulture);
			}
		}
		public static string AccountLogin_InvalidCredentails
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("AccountLogin_InvalidCredentails", AccountStrings.resourceCulture);
			}
		}
		public static string AccountRecovery_CannotRecovery
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("AccountRecovery_CannotRecovery", AccountStrings.resourceCulture);
			}
		}
		public static string AccountResetPassword_CannotReset
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("AccountResetPassword_CannotReset", AccountStrings.resourceCulture);
			}
		}
		public static string ErrorDuplicateEmail
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("ErrorDuplicateEmail", AccountStrings.resourceCulture);
			}
		}
		public static string ErrorDuplicateUserName
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("ErrorDuplicateUserName", AccountStrings.resourceCulture);
			}
		}
		public static string ErrorInvalidAnswer
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("ErrorInvalidAnswer", AccountStrings.resourceCulture);
			}
		}
		public static string ErrorInvalidEmail
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("ErrorInvalidEmail", AccountStrings.resourceCulture);
			}
		}
		public static string ErrorInvalidPassword
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("ErrorInvalidPassword", AccountStrings.resourceCulture);
			}
		}
		public static string ErrorInvalidQuestion
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("ErrorInvalidQuestion", AccountStrings.resourceCulture);
			}
		}
		public static string ErrorInvalidUserName
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("ErrorInvalidUserName", AccountStrings.resourceCulture);
			}
		}
		public static string ErrorProviderError
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("ErrorProviderError", AccountStrings.resourceCulture);
			}
		}
		public static string ErrorUnknownError
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("ErrorUnknownError", AccountStrings.resourceCulture);
			}
		}
		public static string ErrorUserRejected
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("ErrorUserRejected", AccountStrings.resourceCulture);
			}
		}
		public static string LocalPasswordModel_C_ConfirmPassword
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("LocalPasswordModel_C_ConfirmPassword", AccountStrings.resourceCulture);
			}
		}
		public static string LocalPasswordModel_L_Password
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("LocalPasswordModel_L_Password", AccountStrings.resourceCulture);
			}
		}
		public static string Login_AlreadyUser
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Login_AlreadyUser", AccountStrings.resourceCulture);
			}
		}
		public static string Login_Email
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Login_Email", AccountStrings.resourceCulture);
			}
		}
		public static string Login_Forget_1
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Login_Forget_1", AccountStrings.resourceCulture);
			}
		}
		public static string Login_Forget_2
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Login_Forget_2", AccountStrings.resourceCulture);
			}
		}
		public static string Login_LoginButton
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Login_LoginButton", AccountStrings.resourceCulture);
			}
		}
		public static string Login_LogoutButton
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Login_LogoutButton", AccountStrings.resourceCulture);
			}
		}
		public static string Login_Password
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Login_Password", AccountStrings.resourceCulture);
			}
		}
		public static string Login_Ph_Email
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Login_Ph_Email", AccountStrings.resourceCulture);
			}
		}
		public static string Login_Ph_Password
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Login_Ph_Password", AccountStrings.resourceCulture);
			}
		}
		public static string Login_RememberMe
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Login_RememberMe", AccountStrings.resourceCulture);
			}
		}
		public static string Login_Social
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Login_Social", AccountStrings.resourceCulture);
			}
		}
        public static string LoginModel_L_Date
        {
            get
            {
                return AccountStrings.Get("LoginModel_L_Date");
            }
        }

        public static string LoginModel_L_AboutCompany
        {
            get
            {
                return AccountStrings.Get("LoginModel_L_AboutCompany");
            }
        }

        public static string LoginModel_L_Insurance
        {
            get
            {
                return AccountStrings.Get("LoginModel_L_Insurance");
            }
        }

        public static string LoginModel_L_Contact
        {
            get
            {
                return AccountStrings.Get("LoginModel_L_Contact");
            }
        }

        public static string LoginModel_L_Phone
        {
            get
            {
                return AccountStrings.Get("LoginModel_L_Phone");
            }
        }
        public static string LoginModel_L_Fax
        {
            get
            {
                return AccountStrings.Get("LoginModel_L_Fax");
            }
        }
        public static string LoginModel_L_Website
        {
            get
            {
                return AccountStrings.Get("LoginModel_L_Website");
            }
        }
        public static string LoginModel_C_ConfirmPassword
		{
			get
			{
				return AccountStrings.Get("LoginModel_C_ConfirmPassword");
			}
		}

        public static string LoginModel_L_CompanyName
        {
            get
            {
                return AccountStrings.Get("LoginModel_L_CompanyName");
            }
        }

        public static string LoginModel_L_Licenses
        {
            get
            {
                return AccountStrings.Get("LoginModel_L_Licenses");
            }
        }
        public static string LoginModel_L_Password
        {
            get
            {
                return AccountStrings.Get("LoginModel_L_Password");
            }
        }

        public static string LoginModel_L_CountryRegion
        {
            get
            {
                return AccountStrings.Get("LoginModel_L_CountryRegion");
            }
        }
        public static string LoginModel_R_Mail
        {
            get
            {
                return AccountStrings.Get("LoginModel_R_Mail");
            }
        }
        public static string LoginModel_L_Square
        {
            get
            {
                return AccountStrings.Get("LoginModel_L_Square");
            }
        }
        public static string LoginModel_L_Location
        {
            get
            {
                return AccountStrings.Get("LoginModel_L_Location");
            }
        }


        public static string LoginModel_L_People
        {
			get
			{
				return AccountStrings.Get("LoginModel_L_People");
			}
		}
		public static string LoginModel_R_Password
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("LoginModel_R_Password", AccountStrings.resourceCulture);
			}
		}
		public static string LoginModel_R_UserName
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("LoginModel_R_UserName", AccountStrings.resourceCulture);
			}
		}
		public static string LoginText_1
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("LoginText_1", AccountStrings.resourceCulture);
			}
		}
		public static string LoginText_2
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("LoginText_2", AccountStrings.resourceCulture);
			}
		}
		public static string LoginTitle
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("LoginTitle", AccountStrings.resourceCulture);
			}
		}
		public static string RecoveryModel_R_UserName
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("RecoveryModel_R_UserName", AccountStrings.resourceCulture);
			}
		}
		public static string Register_As_1
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Register_As_1", AccountStrings.resourceCulture);
			}
		}
		public static string Register_As_2
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Register_As_2", AccountStrings.resourceCulture);
			}
		}
		public static string Register_Back
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Register_Back", AccountStrings.resourceCulture);
			}
		}
		public static string Register_ChangePassword
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Register_ChangePassword", AccountStrings.resourceCulture);
			}
		}
		public static string Register_ConfirmEmail
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Register_ConfirmEmail", AccountStrings.resourceCulture);
			}
		}
		public static string Register_ConfirmPassword
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Register_ConfirmPassword", AccountStrings.resourceCulture);
			}
		}
		public static string Register_Email
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Register_Email", AccountStrings.resourceCulture);
			}
		}
		public static string Register_EmailSuccess
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Register_EmailSuccess", AccountStrings.resourceCulture);
			}
		}
		public static string Register_EmailUnsuccess
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Register_EmailUnsuccess", AccountStrings.resourceCulture);
			}
		}
		public static string Register_ErrorSocial
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Register_ErrorSocial", AccountStrings.resourceCulture);
			}
		}
		public static string Register_ErrorSocialS
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Register_ErrorSocialS", AccountStrings.resourceCulture);
			}
		}
		public static string Register_MainFormLink
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Register_MainFormLink", AccountStrings.resourceCulture);
			}
		}
		public static string Register_Password
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Register_Password", AccountStrings.resourceCulture);
			}
		}
		public static string Register_Ph_ConfirmPassword
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Register_Ph_ConfirmPassword", AccountStrings.resourceCulture);
			}
		}
		public static string Register_Ph_Email
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Register_Ph_Email", AccountStrings.resourceCulture);
			}
		}
		public static string Register_Ph_Password
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Register_Ph_Password", AccountStrings.resourceCulture);
			}
		}
		public static string Register_RegisterButton
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Register_RegisterButton", AccountStrings.resourceCulture);
			}
		}
		public static string Register_ToMyAccount
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Register_ToMyAccount", AccountStrings.resourceCulture);
			}
		}
		public static string RegisterEmailNotConfirmed
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("RegisterEmailNotConfirmed", AccountStrings.resourceCulture);
			}
		}
		public static string RegisterEmailNotConfirmedNote
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("RegisterEmailNotConfirmedNote", AccountStrings.resourceCulture);
			}
		}
		public static string RegisterEmailNote
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("RegisterEmailNote", AccountStrings.resourceCulture);
			}
		}
		public static string RegisterText
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("RegisterText", AccountStrings.resourceCulture);
			}
		}
		public static string RegisterTitle
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("RegisterTitle", AccountStrings.resourceCulture);
			}
		}
		public static string ResetPassword_CannotReset
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("ResetPassword_CannotReset", AccountStrings.resourceCulture);
			}
		}
		public static string Restore_Btn
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Restore_Btn", AccountStrings.resourceCulture);
			}
		}
		public static string Restore_ConfirmPassword
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Restore_ConfirmPassword", AccountStrings.resourceCulture);
			}
		}
		public static string Restore_Email_Register
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Restore_Email_Register", AccountStrings.resourceCulture);
			}
		}
		public static string Restore_MaimFormLink
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Restore_MaimFormLink", AccountStrings.resourceCulture);
			}
		}
		public static string Restore_Password
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Restore_Password", AccountStrings.resourceCulture);
			}
		}
		public static string Restore_Ph_ConfirmPassword
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Restore_Ph_ConfirmPassword", AccountStrings.resourceCulture);
			}
		}
		public static string Restore_Ph_Password
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Restore_Ph_Password", AccountStrings.resourceCulture);
			}
		}
		public static string Restore_SetPasswordButton
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("Restore_SetPasswordButton", AccountStrings.resourceCulture);
			}
		}
		public static string RestoreChangedOK
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("RestoreChangedOK", AccountStrings.resourceCulture);
			}
		}
		public static string RestorePasswordChangedError
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("RestorePasswordChangedError", AccountStrings.resourceCulture);
			}
		}
		public static string RestoreText
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("RestoreText", AccountStrings.resourceCulture);
			}
		}
		public static string RestoreTitle
		{
			get
			{
				return AccountStrings.ResourceManager.GetString("RestoreTitle", AccountStrings.resourceCulture);
			}
		}
		internal AccountStrings()
		{
		}
	}
}
