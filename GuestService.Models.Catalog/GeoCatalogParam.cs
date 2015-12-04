using System;
namespace GuestService.Models.Catalog
{
	public class GeoCatalogParam : BaseApiParam
	{
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
	}
}
