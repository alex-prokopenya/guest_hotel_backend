using System;
namespace GuestService.Data
{
	public class FreightReservationOrder
	{
		public int id
		{
			get;
			set;
		}
		public string name
		{
			get;
			set;
		}
		public FreightReservationBookingclass bookingclass
		{
			get;
			set;
		}
		public FreightReservationPlace place
		{
			get;
			set;
		}
		public FreightPoint departure
		{
			get;
			set;
		}
		public FreightPoint arrival
		{
			get;
			set;
		}
	}
}
