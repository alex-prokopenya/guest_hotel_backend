using GuestService.Data;
using System;
namespace GuestService.Models.Excursion
{
	public class ExcursionPickupHotelsParam : BaseApiParam, IPartnerParam
	{
		public string pa
		{
			get;
			set;
		}
		public string PartnerAlias
		{
			get
			{
				return this.pa;
			}
		}
		public string psid
		{
			get;
			set;
		}
		public string PartnerSessionID
		{
			get
			{
				return this.psid;
			}
		}
		public int ex
		{
			get;
			set;
		}
		public int Excursion
		{
			get
			{
				return this.ex;
			}
		}
		public int et
		{
			get;
			set;
		}
		public int ExcursionTime
		{
			get
			{
				return this.et;
			}
		}
		public System.DateTime? dt
		{
			get;
			set;
		}
		public System.DateTime? Date
		{
			get
			{
				return this.dt;
			}
		}
		public int[] dp
		{
			get;
			set;
		}
		public int[] DeparturePoints
		{
			get
			{
				return this.dp;
			}
		}
	}
}
