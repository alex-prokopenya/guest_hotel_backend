using GuestService.Data;
using System;
using System.Collections.Generic;
namespace GuestService.Models.Guest
{
	public class FindOrderContext
	{
		public FindOrderModel Form
		{
			get;
			set;
		}
		public LinkOrderModel Link
		{
			get;
			set;
		}
		public System.Collections.Generic.List<GuestClaim> Claims
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
