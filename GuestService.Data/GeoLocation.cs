using System;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class GeoLocation
	{
		[XmlAttribute]
		public decimal latitude
		{
			get;
			set;
		}
		[XmlAttribute]
		public decimal longitude
		{
			get;
			set;
		}
	}
}
