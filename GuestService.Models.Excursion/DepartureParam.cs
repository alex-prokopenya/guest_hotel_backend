using GuestService.Data;
using System;
namespace GuestService.Models.Excursion
{
	public class DepartureParam : CategoryParam, IPartnerParam
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
		public new bool? wp
		{
			get;
			set;
		}
		public new bool WithoutPrice
		{
			get
			{
				return this.wp.HasValue ? this.wp.Value : (!Settings.ExcursionWithPriceOnlyCatalog);
			}
		}
	}
}
