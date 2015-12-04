using System;
namespace GuestService.Data
{
	public class ExcursionReservationOrder
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
		public ExcursionReservationTime time
		{
			get;
			set;
		}
		public ExcursionReservationGroup grouptype
		{
			get;
			set;
		}
		public ExcursionReservationLanguage language
		{
			get;
			set;
		}
		public PickupPlace pickuppoint
		{
			get;
			set;
		}
		public PickupPlace pickuphotel
		{
			get;
			set;
		}
	}
}
