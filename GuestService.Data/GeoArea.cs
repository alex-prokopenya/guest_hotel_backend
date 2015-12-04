using System;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class GeoArea
	{
		[XmlAttribute]
		public int id
		{
			get;
			set;
		}
		[XmlAttribute]
		public string alias
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
		[XmlElement("Location")]
		public GeoLocation location
		{
			get;
			set;
		}
	}
}
