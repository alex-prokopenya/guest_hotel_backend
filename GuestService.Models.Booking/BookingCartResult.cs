using GuestService.Data;
using System;
namespace GuestService.Models.Booking
{
	public class BookingCartResult
	{
		public BookingModel Form
		{
			get;
			set;
		}
		public ReservationState Reservation
		{
			get;
			set;
		}
	}
}
