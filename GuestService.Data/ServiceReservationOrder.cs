using System;
namespace GuestService.Data
{
	public class ServiceReservationOrder
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
		public ServiceReservationServicetype servicetype
		{
			get;
			set;
		}
	}
}
