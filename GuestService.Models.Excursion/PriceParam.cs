using GuestService.Data;
using System;
namespace GuestService.Models.Excursion
{
	public class PriceParam : BaseApiParam, IPartnerParam
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
		public int? sp
		{
			get;
			set;
		}
		public int? StartPoint
		{
			get
			{
				return this.sp;
			}
		}
		public string spa
		{
			get;
			set;
		}
		public string StartPointAlias
		{
			get
			{
				return this.spa;
			}
		}
		public int? l
		{
			get;
			set;
		}
		public int? ExcursionLanguage
		{
			get
			{
				return this.l;
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
	}
}
