using System;
namespace GuestService.Data
{
	public class GuestOrder
	{
		public DatePeriod period
		{
			get;
			set;
		}
		public string title
		{
			get;
			set;
		}
		public string description
		{
			get;
			set;
		}
		public int? hotelid
		{
			get;
			set;
		}
		public int? serviceid
		{
			get;
			set;
		}
	}
}
