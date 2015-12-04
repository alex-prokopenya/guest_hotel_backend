using System;
namespace GuestService.Models.Guest
{
	public class PrintOrderContext
	{
		public PrintOrderModel Form
		{
			get;
			set;
		}
		public bool NotFound
		{
			get;
			set;
		}
	}
}
