using System;
namespace GuestService.Models.Payment
{
	public class ProcessingResultModel
	{
		public bool? success
		{
			get;
			set;
		}
		public string invoice
		{
			get;
			set;
		}
		public string token
		{
			get;
			set;
		}
		public string payerID
		{
			get;
			set;
		}
	}
}
