using System;
using System.Collections.Generic;
namespace GuestService.Data
{
	public class DepartureTransfer
	{
		public int id
		{
			get;
			set;
		}
		public string ident
		{
			get;
			set;
		}
		public bool indiviadual
		{
			get;
			set;
		}
		public string servicename
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
		public DepartureFlight flight
		{
			get;
			set;
		}
		public DepartureDestinationHotel hotel
		{
			get;
			set;
		}
		public string language
		{
			get;
			set;
		}
		public string note
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
		public System.Collections.Generic.List<DepartureMember> people
		{
			get;
			set;
		}
	}
}
