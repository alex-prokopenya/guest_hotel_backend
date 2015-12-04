using System;
namespace GuestService.Models.Excursion
{
	public class SearchParam : BaseApiParam
	{
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
				return this.s;
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
	}
}
