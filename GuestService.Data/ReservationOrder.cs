using System;
using System.Collections.Generic;
namespace GuestService.Data
{
	public class ReservationOrder
	{
		public string id
		{
			get;
			set;
		}
		public int? localid
		{
			get;
			set;
		}
		public ReservationStatus status
		{
			get;
			set;
		}
		public System.DateTime datefrom
		{
			get;
			set;
		}
		public System.DateTime datetill
		{
			get;
			set;
		}
		public ReservationPartner partner
		{
			get;
			set;
		}
		public ReservationPax pax
		{
			get;
			set;
		}
		public string note
		{
			get;
			set;
		}
		public ReservationOrderPrice price
		{
			get;
			set;
		}
		public System.Collections.Generic.List<string> peopleids
		{
			get;
			set;
		}
		public HotelReservationOrder hotel
		{
			get;
			set;
		}
		public ExcursionReservationOrder excursion
		{
			get;
			set;
		}
		public FreightReservationOrder freight
		{
			get;
			set;
		}
		public TransferReservationOrder transfer
		{
			get;
			set;
		}
		public ServiceReservationOrder service
		{
			get;
			set;
		}
	}
}
