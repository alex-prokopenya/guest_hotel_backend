using System;
namespace GuestService.Data
{
	public class HotelReservationOrder
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
		public HotelReservationRoom room
		{
			get;
			set;
		}
		public HotelReservationHtplace htplace
		{
			get;
			set;
		}
		public HotelReservationMeal meal
		{
			get;
			set;
		}
	}
}
