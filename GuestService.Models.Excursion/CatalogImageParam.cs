using System;
namespace GuestService.Models.Excursion
{
	public class CatalogImageParam : ImageParam
	{
		public int? i
		{
			get;
			set;
		}
		public int Index
		{
			get
			{
				return this.i.HasValue ? this.i.Value : 0;
			}
		}
	}
}
