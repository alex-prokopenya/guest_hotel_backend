using GuestService.Resources;
using Sm.System.Mvc.Validation;
using System;
using System.ComponentModel.DataAnnotations;
namespace GuestService.Models
{
	public class RegisterExternalLoginModel
	{
		[Email(ErrorMessageResourceName = "LoginModel_R_Mail", ErrorMessageResourceType = typeof(AccountStrings)), Required]
		public string UserName
		{
			get;
			set;
		}
		public string ExternalLoginData
		{
			get;
			set;
		}
	}
}
