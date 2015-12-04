using System;
namespace GuestService.Data
{
	public class BookingExcursion
	{
		public int id
		{
			get;
			set;
		}
		public System.DateTime date
		{
			get;
			set;
		}
		public int? extime
		{
			get;
			set;
		}
		public int? language
		{
			get;
			set;
		}
		public int? grouptype
		{
			get;
			set;
		}
		public int? pickuppoint
		{
			get;
			set;
		}
		public int? pickuphotel
		{
			get;
			set;
		}
		public BookingPax pax
		{
			get;
			set;
		}
		public ExcursionContact contact
		{
			get;
			set;
		}
		public string note
		{
			get;
			set;
		}
	}
}
