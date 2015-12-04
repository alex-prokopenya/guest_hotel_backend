using System;
using System.Collections.Generic;
namespace GuestService.Data
{
	public class DepartureHotel
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
		public System.Collections.Generic.List<DepartureTransfer> transfers
		{
			get;
			set;
		}
	}
}
