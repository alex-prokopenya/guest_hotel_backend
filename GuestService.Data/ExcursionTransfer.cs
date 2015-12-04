using System;
namespace GuestService.Data
{
	public class ExcursionTransfer
	{
		public int? claim
		{
			get;
			set;
		}
		public int? order
		{
			get;
			set;
		}
		public int? exsale
		{
			get;
			set;
		}
		public string voucher
		{
			get;
			set;
		}
		public string excursion
		{
			get;
			set;
		}
		public string excursiontime
		{
			get;
			set;
		}
		public string transferident
		{
			get;
			set;
		}
		public string transfernote
		{
			get;
			set;
		}
		public System.DateTime date
		{
			get;
			set;
		}
		public System.DateTime? pickup
		{
			get;
			set;
		}
		public string pickupplace
		{
			get;
			set;
		}
		public DepartureWorker guide
		{
			get;
			set;
		}
		public DepartureWorker guide2
		{
			get;
			set;
		}
	}
}
