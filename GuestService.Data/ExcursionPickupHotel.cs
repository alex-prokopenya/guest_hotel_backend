using System;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class ExcursionPickupHotel
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
		public System.DateTime? pickuptime
		{
			get;
			set;
		}
		[XmlAttribute]
		public string pickupplace
		{
			get;
			set;
		}
	}
}
