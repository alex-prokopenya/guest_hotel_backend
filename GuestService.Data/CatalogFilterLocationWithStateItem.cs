using System;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class CatalogFilterLocationWithStateItem : CatalogFilterLocationItem
	{
		[XmlAttribute]
		public string stateid
		{
			get;
			set;
		}
		public CatalogFilterLocationWithStateItem()
		{
		}
		public CatalogFilterLocationWithStateItem(CatalogFilterLocationItem item, string stateId)
		{
			base.id = item.id;
			base.name = item.name;
			base.count = item.count;
			base.location = item.location;
			this.stateid = stateId;
		}
	}
}
