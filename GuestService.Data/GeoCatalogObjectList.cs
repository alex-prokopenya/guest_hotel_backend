using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace GuestService.Data
{
	[XmlRoot("GeoCatalogObjectList")]
	public class GeoCatalogObjectList : System.Collections.Generic.List<GeoCatalogObject>
	{
		public GeoCatalogObjectList(System.Collections.Generic.IEnumerable<GeoCatalogObject> collection) : base(collection)
		{
		}
	}
}
