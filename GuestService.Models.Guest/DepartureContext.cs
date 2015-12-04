using GuestService.Data;
using System;
using System.Collections.Generic;
namespace GuestService.Models.Guest
{
	public class DepartureContext
	{
		public System.Collections.Generic.List<DepartureHotel> Hotels
		{
			get;
			set;
		}
		public HotelCatalogObject Hotel
		{
			get;
			set;
		}
	}
}
