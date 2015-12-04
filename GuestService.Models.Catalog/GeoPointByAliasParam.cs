using System;
namespace GuestService.Models.Catalog
{
	public class GeoPointByAliasParam : BaseApiParam
	{
		public string gpa
		{
			get;
			set;
		}
		public string GeoPointAlias
		{
			get
			{
				return this.gpa;
			}
		}
	}
}
