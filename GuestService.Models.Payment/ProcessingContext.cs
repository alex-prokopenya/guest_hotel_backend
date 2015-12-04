using GuestService.Data;
using System;
namespace GuestService.Models.Payment
{
	public class ProcessingContext
	{
		public ReservationState Reservation
		{
			get;
			set;
		}
		public PaymentMode PaymentMode
		{
			get;
			set;
		}
		public PaymentBeforeProcessingResult BeforePaymentResult
		{
			get;
			set;
		}

        public string RedirectUrl
        {
            get;
            set;
        }
    }
}
