using GuestService.Resources;
using Sm.System.Mvc.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace GuestService.Models
{
	public class RegisterModel
	{
		[Email(ErrorMessageResourceName = "LoginModel_R_Mail", ErrorMessageResourceType = typeof(AccountStrings)), Required]
		public string UserName
		{
			get;
			set;
		}
		[DataType(DataType.Password), Required, StringLength(100, ErrorMessageResourceName = "LoginModel_L_Password", ErrorMessageResourceType = typeof(AccountStrings), MinimumLength = 6)]
		public string Password
		{
			get;
			set;
		}
		[DataType(DataType.Password), System.Web.Mvc.Compare("Password", ErrorMessageResourceName = "LoginModel_C_ConfirmPassword", ErrorMessageResourceType = typeof(AccountStrings))]
		public string ConfirmPassword
		{
			get;
			set;
		}
	}
}
