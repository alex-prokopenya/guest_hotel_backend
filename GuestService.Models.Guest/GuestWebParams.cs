using System;
namespace GuestService.Models.Guest
{
	public class GuestWebParams
	{
		public System.DateTime? _dt
		{
			get;
			set;
		}
		public System.DateTime? TestDate
		{
			get
			{
				return this._dt;
			}
		}
	}
}
