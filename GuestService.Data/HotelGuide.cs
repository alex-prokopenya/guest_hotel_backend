using System;
using System.Collections.Generic;
namespace GuestService.Data
{
	public class HotelGuide
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
		public string mobile
		{
			get;
			set;
		}
		public System.Collections.Generic.List<HotelGuideDurty> durties
		{
			get;
			set;
		}
	}
}
