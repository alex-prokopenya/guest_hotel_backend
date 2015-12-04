using System;
namespace GuestService.Data
{
	public class ReservationPrice
	{
		public decimal total
		{
			get;
			set;
		}
		public decimal topay
		{
			get;
			set;
		}
		public string currency
		{
			get;
			set;
		}
	}
}
