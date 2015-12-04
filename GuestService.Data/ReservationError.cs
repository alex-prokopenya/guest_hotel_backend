using System;
namespace GuestService.Data
{
	public class ReservationError
	{
		public string orderid
		{
			get;
			set;
		}
		public ReservationErrorType errortype
		{
			get;
			set;
		}
		public int number
		{
			get;
			set;
		}
		public string message
		{
			get;
			set;
		}
		public string usermessage
		{
			get;
			set;
		}
		public bool isstop
		{
			get;
			set;
		}
	}
}
