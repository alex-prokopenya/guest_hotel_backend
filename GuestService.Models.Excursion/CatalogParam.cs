using GuestService.Data;
using System;
namespace GuestService.Models.Excursion
{
	public class CatalogParam : BaseApiParam, IPartnerParam
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
		public int? sl
		{
			get;
			set;
		}
		public int? SearchLimit
		{
			get
			{
				return this.sl;
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
		public string s
		{
			get;
			set;
		}
		public string SearchText
		{
			get
			{
				return string.IsNullOrWhiteSpace(this.s) ? null : this.s;
			}
		}
		public int[] c
		{
			get;
			set;
		}
		public int[] Categories
		{
			get
			{
				return this.c;
			}
		}
		public int[] dp
		{
			get;
			set;
		}
		public int[] Departures
		{
			get
			{
				return this.dp;
			}
		}
		public int[] d
		{
			get;
			set;
		}
		public int[] Destinations
		{
			get
			{
				return this.d;
			}
		}
		public int[] l
		{
			get;
			set;
		}
		public int[] ExcursionLanguages
		{
			get
			{
				return this.l;
			}
		}
		public System.TimeSpan? nd
		{
			get;
			set;
		}
		public System.TimeSpan? MinDuration
		{
			get
			{
				return this.nd;
			}
		}
		public System.TimeSpan? xd
		{
			get;
			set;
		}
		public System.TimeSpan? MaxDuration
		{
			get
			{
				return this.xd;
			}
		}
		public string so
		{
			get;
			set;
		}
		public string SortOrder
		{
			get
			{
				return this.so;
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
