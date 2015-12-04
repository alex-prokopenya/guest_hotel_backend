using System;
namespace GuestService.Data
{
	public class HotelCatalogObject
	{
		public int id
		{
			get;
			set;
		}
		public string alias
		{
			get;
			set;
		}
		public string name
		{
			get;
			set;
		}
		public HotelStar star
		{
			get;
			set;
		}
		public Town town
		{
			get;
			set;
		}
		public Region region
		{
			get;
			set;
		}
		public string address
		{
			get;
			set;
		}
		public string web
		{
			get;
			set;
		}
		public GeoLocation geoposition
		{
			get;
			set;
		}
	}
}
