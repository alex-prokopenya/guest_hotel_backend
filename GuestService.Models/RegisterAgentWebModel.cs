using GuestService.Resources;
using Sm.System.Mvc.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web;
namespace GuestService.Models
{
	public class RegisterAgentWebModel
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

        //[DataType(DataType.Text), Required, StringLength(250, ErrorMessageResourceName = "LoginModel_L_CompanyName", ErrorMessageResourceType = typeof(AccountStrings), MinimumLength = 1)]
        //public string CompanyName
        //{
        //    get;
        //    set;
        //}

        [DataType(DataType.Text), Required, StringLength(250, ErrorMessageResourceName = "LoginModel_L_CountryRegion", ErrorMessageResourceType = typeof(AccountStrings), MinimumLength = 1)]
        public string CountryRegion
        {
            get;
            set;
        }

        [DataType(DataType.Date), Required, StringLength(250, ErrorMessageResourceName = "LoginModel_L_Date", ErrorMessageResourceType = typeof(AccountStrings), MinimumLength = 10)]
        public string DateEstablish
        {
            get;
            set;
        }

        //[DataType(DataType.Text), Required, StringLength(250, ErrorMessageResourceName = "LoginModel_L_Licenses", ErrorMessageResourceType = typeof(AccountStrings))]
        //public string Licenses
        //{
        //    get;
        //    set;
        //}

        //public class ValidateFileAttribute : RequiredAttribute
        //{
        //    public override bool IsValid(object value)
        //    {
        //        var file = value as HttpPostedFileBase;
        //        if (file == null)
        //        {
        //            return false;
        //        }

        //        if (file.ContentLength > 3 * 1024 * 1024)
        //        {
        //            return false;
        //        }
                
        //        return true;
        //    }
        //}

        //[ValidateFile(ErrorMessage = "Please select a file smaller than 3MB"), Required]
        //public HttpPostedFileBase ConstitutiveDocs { get; set; }

        //[ValidateFile(ErrorMessage = "Please select a file smaller than 3MB")]
        //public HttpPostedFileBase Insurance { get; set; }
               

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
    }
}
