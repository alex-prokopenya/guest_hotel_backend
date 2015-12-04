using System;
namespace GuestService.Data
{
	public class ReservationPeoplePassport
	{
		public string serie
		{
			get;
			set;
		}
		public string number
		{
			get;
			set;
		}
		public System.DateTime? issue
		{
			get;
			set;
		}
		public System.DateTime? valid
		{
			get;
			set;
		}
	}
}
