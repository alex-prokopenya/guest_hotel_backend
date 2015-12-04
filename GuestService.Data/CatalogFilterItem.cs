using System;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class CatalogFilterItem
	{
		[XmlAttribute]
		public int id
		{
			get;
			set;
		}
		[XmlAttribute]
		public string name
		{
			get;
			set;
		}
		[XmlAttribute]
		public int count
		{
			get;
			set;
		}
	}
}
