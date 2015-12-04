using GuestService.Data;
using System;
namespace GuestService.Models.Excursion
{
	public class CategoryParam : BaseApiParam, IStartPointAndLanguageAndPriceOptionParam, IStartPointAndLanguageParam
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
