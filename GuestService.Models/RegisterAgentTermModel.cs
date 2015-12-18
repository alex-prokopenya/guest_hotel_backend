using GuestService.Resources;
using Sm.System.Mvc.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web;
namespace GuestService.Models
{
	public class RegisterAgentTermModel
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

        [DataType(DataType.Text), Required, StringLength(250, ErrorMessageResourceName = "LoginModel_L_CompanyName", ErrorMessageResourceType = typeof(AccountStrings), MinimumLength = 1)]
        public string CompanyName
        {
            get;
            set;
        }

        [DataType(DataType.Text), Required, StringLength(250, ErrorMessageResourceName = "LoginModel_L_CountryRegion", ErrorMessageResourceType = typeof(AccountStrings), MinimumLength = 1)]
        public string CountryRegion
        {
            get;
            set;
        }
     
        [DataType(DataType.MultilineText), Required, StringLength(250, ErrorMessageResourceName = "LoginModel_L_AboutCompany", ErrorMessageResourceType = typeof(AccountStrings))]
        public string AboutCompany
        {
            get;
            set;
        }

        [DataType(DataType.Text), Required, StringLength(250, ErrorMessageResourceName = "LoginModel_L_Contact", ErrorMessageResourceType = typeof(AccountStrings))]
        public string Contact
        {
            get;
            set;
        }
        [DataType(DataType.Text), Required, StringLength(250, ErrorMessageResourceName = "LoginModel_L_Phone", ErrorMessageResourceType = typeof(AccountStrings))]
        public string Phone
        {
            get;
            set;
        }
        [DataType(DataType.Text), Required, StringLength(250, ErrorMessageResourceName = "LoginModel_L_Fax", ErrorMessageResourceType = typeof(AccountStrings))]
        public string Fax
        {
            get;
            set;
        }

        [DataType(DataType.Url), Required, StringLength(250, ErrorMessageResourceName = "LoginModel_L_Website", ErrorMessageResourceType = typeof(AccountStrings))]
        public string Website
        {
            get;
            set;
        }


        [DataType(DataType.Text), Required, StringLength(250, ErrorMessageResourceName = "LoginModel_L_Location", ErrorMessageResourceType = typeof(AccountStrings))]
        public string Location
        {
            get;
            set;
        }


        [DataType(DataType.Text), Required, StringLength(250, ErrorMessageResourceName = "LoginModel_L_Square", ErrorMessageResourceType = typeof(AccountStrings))]
        public string Square
        {
            get;
            set;
        }


        [DataType(DataType.Text), Required, StringLength(250, ErrorMessageResourceName = "LoginModel_L_People", ErrorMessageResourceType = typeof(AccountStrings))]
        public string People
        {
            get;
            set;
        }
    }
}
