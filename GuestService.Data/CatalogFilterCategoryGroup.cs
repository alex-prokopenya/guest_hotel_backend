using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class CatalogFilterCategoryGroup
	{
		[XmlAttribute]
		public string name
		{
			get;
			set;
		}
		[XmlArray("Items")]
		public System.Collections.Generic.List<CatalogFilterItem> items
		{
			get;
			set;
		}
	}
}
