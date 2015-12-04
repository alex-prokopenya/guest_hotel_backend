using GuestService.Resources;
using Sm.System.Mvc.Validation;
using System;
using System.ComponentModel.DataAnnotations;
namespace GuestService.Models
{
	public class RecoveryModel
	{
		[Email(ErrorMessageResourceName = "LoginModel_R_Mail", ErrorMessageResourceType = typeof(AccountStrings)), Required(ErrorMessageResourceName = "RecoveryModel_R_UserName", ErrorMessageResourceType = typeof(AccountStrings))]
		public string UserName
		{
			get;
			set;
		}
		public string ResetToken
		{
			get;
			set;
		}
	}
}
