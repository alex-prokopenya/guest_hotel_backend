using System;
namespace GuestService
{
	public class PaymentPaypalSettings
	{
		public string Username
		{
			get;
			set;
		}
		public string Password
		{
			get;
			set;
		}
		public string Signature
		{
			get;
			set;
		}
		public bool IsSandbox
		{
			get;
			set;
		}
	}
}
