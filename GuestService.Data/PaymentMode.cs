using System;
namespace GuestService.Data
{
	public class PaymentMode
	{
		public string id
		{
			get;
			set;
		}
		public string name
		{
			get;
			set;
		}
		public string processing
		{
			get;
			set;
		}
		public ReservationOrderPrice comission
		{
			get;
			set;
		}
		public ReservationOrderPrice payrest
		{
			get;
			set;
		}
		public string paymentparam
		{
			get;
			set;
		}
	}
}
