using System;
namespace GuestService.Data
{
	public class ExcursionOrder
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
		public System.DateTime date
		{
			get;
			set;
		}
		public ExcursionTime time
		{
			get;
			set;
		}
		public Language language
		{
			get;
			set;
		}
		public ExcursionGroup group
		{
			get;
			set;
		}
		public GeoArea departure
		{
			get;
			set;
		}
		public BookingPax pax
		{
			get;
			set;
		}
		public OrderPrice price
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
