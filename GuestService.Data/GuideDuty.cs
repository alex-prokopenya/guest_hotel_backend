using System;
using System.Collections.Generic;
namespace GuestService.Data
{
	public class GuideDuty : Guide
	{
		public System.Collections.Generic.List<GuideDutyTime> duties
		{
			get;
			set;
		}
	}
}
