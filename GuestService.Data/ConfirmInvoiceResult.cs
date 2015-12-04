using System;
namespace GuestService.Data
{
	public class ConfirmInvoiceResult
	{
		public int? Error
		{
			get;
			set;
		}
		public string ErrorMessage
		{
			get;
			set;
		}
		public bool IsSuccess
		{
			get
			{
				return !this.Error.HasValue;
			}
		}
	}
}
