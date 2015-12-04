using System;
namespace GuestService.Models.Guide
{
	public class HotelGuideParam : BaseApiParam
	{
		public int? h
		{
			get;
			set;
		}
		public int? Hotel
		{
			get
			{
				return this.h;
			}
		}
		public System.DateTime? pb
		{
			get;
			set;
		}
		public System.DateTime? PeriodBegin
		{
			get
			{
				return this.pb;
			}
		}
		public System.DateTime? pe
		{
			get;
			set;
		}
		public System.DateTime? PeriodEnd
		{
			get
			{
				return this.pe;
			}
		}
	}
}
