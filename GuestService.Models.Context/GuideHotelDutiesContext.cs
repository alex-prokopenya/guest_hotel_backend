using GuestService.Data;
using System;
using System.Collections.Generic;
namespace GuestService.Models.Context
{
	public class GuideHotelDutiesContext
	{
		public System.Collections.Generic.List<HotelGuideResult> Hotels
		{
			get;
			private set;
		}
		public GuideHotelDutiesContext()
		{
			this.Hotels = new System.Collections.Generic.List<HotelGuideResult>();
		}
	}
}
