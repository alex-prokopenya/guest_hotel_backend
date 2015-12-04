using System;
namespace GuestService.Data
{
	public class ExcursionOrderCalculatePrice : ExcursionOrder
	{
		public System.DateTime? closesaletime
		{
			get;
			set;
		}
		public bool issaleclosed
		{
			get;
			set;
		}
		public bool isstopsale
		{
			get;
			set;
		}
	}
}
