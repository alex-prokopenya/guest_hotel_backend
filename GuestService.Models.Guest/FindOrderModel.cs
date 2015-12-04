using GuestService.Resources;
using Sm.System.Mvc.Validation;
using System;
using System.ComponentModel.DataAnnotations;
namespace GuestService.Models.Guest
{
	public class FindOrderModel
	{
		public string RequestType
		{
			get;
			set;
		}
		[Display(Name = "FindOrderModel_N_ClaimName", ResourceType = typeof(GuestStrings)), Required(ErrorMessageResourceName = "FindOrderModel_R_ClaimName", ErrorMessageResourceType = typeof(GuestStrings))]
		public string ClaimName
		{
			get;
			set;
		}
		[Digital(ErrorMessageResourceName = "FindOrderModel_D_Claim", ErrorMessageResourceType = typeof(GuestStrings)), Display(Name = "FindOrderModel_N_Claim", ResourceType = typeof(GuestStrings)), Required(ErrorMessageResourceName = "FindOrderModel_R_Claim", ErrorMessageResourceType = typeof(GuestStrings))]
		public string Claim
		{
			get;
			set;
		}
		[Display(Name = "FindOrderModel_N_ClaimName", ResourceType = typeof(GuestStrings)), Required(ErrorMessageResourceName = "FindOrderModel_R_ClaimName", ErrorMessageResourceType = typeof(GuestStrings))]
		public string PassportName
		{
			get;
			set;
		}
		[Display(Name = "FindOrderModel_N_Passport", ResourceType = typeof(GuestStrings)), Required(ErrorMessageResourceName = "FindOrderModel_R_Passport", ErrorMessageResourceType = typeof(GuestStrings))]
		public string Passport
		{
			get;
			set;
		}
	}
}
