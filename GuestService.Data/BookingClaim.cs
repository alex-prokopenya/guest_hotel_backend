using System;
using System.Collections.Generic;
namespace GuestService.Data
{
	public class BookingClaim
	{
		public System.Collections.Generic.List<BookingOrder> orders
		{
			get;
			set;
		}
		public string note
		{
			get;
			set;
		}
		public Customer customer
		{
			get;
			set;
		}
	}
}
