using System;
namespace GuestService.Data
{
	public class PaymentBeforeProcessingResult
	{
		public bool success
		{
			get;
			set;
		}
		public string invoiceNumber
		{
			get;
			set;
		}
	}
}
