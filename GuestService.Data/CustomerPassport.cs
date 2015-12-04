using System;
namespace GuestService.Data
{
	public class CustomerPassport
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
		public System.DateTime? issuedate
		{
			get;
			set;
		}
		public string issueplace
		{
			get;
			set;
		}
	}
}
