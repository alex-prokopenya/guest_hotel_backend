using System;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class CatalogFilterLocationItem : CatalogFilterItem
	{
		[XmlElement("Location")]
		public GeoLocation location
		{
			get;
			set;
		}
	}
}
