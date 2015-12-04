using System;
using System.Collections.Generic;
namespace GuestService.Models.Payment
{
	public class PaymentResultContext
	{
		public bool Success
		{
			get;
			set;
		}
		public System.Collections.Generic.List<string> Errors
		{
			get;
			private set;
		}
		public PaymentResultContext()
		{
			this.Errors = new System.Collections.Generic.List<string>();
		}
	}
}
