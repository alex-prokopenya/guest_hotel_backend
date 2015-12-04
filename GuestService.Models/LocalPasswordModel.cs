using GuestService.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace GuestService.Models
{
	public class LocalPasswordModel
	{
		[DataType(DataType.Password), Required]
		public string OldPassword
		{
			get;
			set;
		}
		[DataType(DataType.Password), Required, StringLength(100, ErrorMessageResourceName = "LocalPasswordModel_L_Password", ErrorMessageResourceType = typeof(AccountStrings), MinimumLength = 6)]
		public string NewPassword
		{
			get;
			set;
		}
		[DataType(DataType.Password), System.Web.Mvc.Compare("NewPassword", ErrorMessageResourceName = "LocalPasswordModel_C_ConfirmPassword", ErrorMessageResourceType = typeof(AccountStrings))]
		public string ConfirmPassword
		{
			get;
			set;
		}
	}
}
