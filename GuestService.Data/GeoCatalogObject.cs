using System;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class GeoCatalogObject
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
		public string geotype
		{
			get;
			set;
		}
	}
}
