using GuestService.Data;
using System;
namespace GuestService.Models.Booking
{
	public class BookingOrderModel
	{
		public BookingOrder BookingOrder
		{
			get;
			set;
		}
		public ReservationOrder ReservationOrder
		{
			get;
			set;
		}
	}
}
