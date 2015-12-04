using System;
using System.Collections.Generic;
namespace GuestService.Data
{
	public class HotelGuideResult
	{
		public HotelCatalogObject hotel
		{
			get;
			set;
		}
		public System.Collections.Generic.List<HotelGuide> guides
		{
			get;
			set;
		}
	}
}
