using GuestService.Data;
using System;
namespace GuestService.Models.Excursion
{
	public class FiltersParam : BaseApiParam, IPartnerParam, IStartPointAndLanguageAndPriceOptionParam, IStartPointAndLanguageParam
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
		public int? ds
		{
			get;
			set;
		}
		public int? DestinationState
		{
			get
			{
				return this.ds;
			}
		}
		public bool? wp
		{
			get;
			set;
		}
		public bool WithoutPrice
		{
			get
			{
				return this.wp.HasValue ? this.wp.Value : (!Settings.ExcursionWithPriceOnlyCatalog);
			}
		}
	}
}
