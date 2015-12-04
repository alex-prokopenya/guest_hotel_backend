using GuestService.Data;
using System;
using System.Collections.Generic;
namespace GuestService.Models.Payment
{
	public class PaymentContext
	{
		public ReservationState Reservation
		{
			get;
			set;
		}
		public System.Collections.Generic.List<PaymentMode> PaymentModes
		{
			get;
			set;
		}
	}
}
