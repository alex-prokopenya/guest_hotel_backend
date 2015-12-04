using System;
namespace GuestService.Models.Booking
{
	public class BookingCartWebParam
	{
		public string pa
		{
			get;
			set;
		}
		public string PartnerAlias
		{
			get
			{
				return this.pa;
			}
		}
	}
}
