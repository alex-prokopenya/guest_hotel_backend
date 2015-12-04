using System;
namespace GuestService.Models.Guest
{
	public class DepartureParam : BaseApiParam
	{
		public System.DateTime? fd
		{
			get;
			set;
		}
		public System.DateTime? FirstDate
		{
			get
			{
				return this.fd;
			}
		}
		public System.DateTime? ld
		{
			get;
			set;
		}
		public System.DateTime? LastDate
		{
			get
			{
				return this.ld;
			}
		}
		public int? c
		{
			get;
			set;
		}
		public int? Claim
		{
			get
			{
				return this.c;
			}
		}
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
		public string ha
		{
			get;
			set;
		}
		public string HotelAlias
		{
			get
			{
				return this.ha;
			}
		}
	}
}
