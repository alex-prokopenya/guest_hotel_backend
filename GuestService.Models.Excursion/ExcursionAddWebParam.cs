using GuestService.Data;
using System;
namespace GuestService.Models.Excursion
{
	public class ExcursionAddWebParam
	{
		public string partner
		{
			get;
			set;
		}
		public BookingExcursion excursion
		{
			get;
			set;
		}
	}
}
