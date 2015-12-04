using GuestService.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace GuestService.Models.Booking
{
	public class BookingModel
	{
		public string Action
		{
			get;
			set;
		}
		public string PartnerAlias
		{
			get;
			set;
		}
		public System.Collections.Generic.List<BookingOrderModel> Orders
		{
			get;
			private set;
		}
		[Required(ErrorMessageResourceName = "BookingModel_R_CustomerName", ErrorMessageResourceType = typeof(BookingStrings))]
		public string CustomerName
		{
			get;
			set;
		}
		[Required(ErrorMessageResourceName = "BookingModel_R_CustomerMobile", ErrorMessageResourceType = typeof(BookingStrings))]
		public string CustomerMobile
		{
			get;
			set;
		}
		[Required(ErrorMessageResourceName = "BookingModel_R_UserEmail", ErrorMessageResourceType = typeof(BookingStrings))]
		public string CustomerEmail
		{
			get;
			set;
		}
		public string CustomerAddress
		{
			get;
			set;
		}
		public string BookingNote
		{
			get;
			set;
		}
		public bool RulesAccepted
		{
			get;
			set;
		}
		public string RemoveOrderId
		{
			get;
			set;
		}
		public BookingModel()
		{
			this.Orders = new System.Collections.Generic.List<BookingOrderModel>();
		}
	}
}
