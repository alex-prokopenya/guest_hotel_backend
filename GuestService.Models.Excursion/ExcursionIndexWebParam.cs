using System;
namespace GuestService.Models.Excursion
{
	public class ExcursionIndexWebParam
	{
		public string visualtheme
		{
			get;
			set;
		}
		public string sc
		{
			get;
			set;
		}
		public string ShowCommand
		{
			get
			{
				return this.sc;
			}
		}
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
		public int? ex
		{
			get;
			set;
		}
		public int? Excursion
		{
			get
			{
				return this.ex;
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
