using GuestService.Data;
using System;
using System.Collections.Generic;
namespace GuestService.Models.Guest
{
	public class SummaryContext
	{
		public bool ShowOrderFindForm
		{
			get;
			set;
		}
		public OrderModel OrderFindForm
		{
			get;
			set;
		}
		public bool OrderFindNotFound
		{
			get;
			set;
		}
		public ReservationState Claim
		{
			get;
			set;
		}
		public System.Collections.Generic.List<DepartureHotel> Hotels
		{
			get;
			set;
		}
		public System.Collections.Generic.List<HotelGuideResult> GuideDurties
		{
			get;
			set;
		}
	}
}
