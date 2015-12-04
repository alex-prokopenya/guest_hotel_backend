using System;
namespace GuestService.Data
{
	public class BookingOrder
	{
		public string orderid
		{
			get;
			set;
		}
		public BookingExcursion excursion
		{
			get;
			set;
		}
	}
}
