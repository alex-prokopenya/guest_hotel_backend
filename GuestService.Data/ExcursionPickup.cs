using System;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class ExcursionPickup : GeoArea
	{
		[XmlElement("PickupTime")]
		public System.DateTime? pickuptime
		{
			get;
			set;
		}
		[XmlAttribute]
		public string note
		{
			get;
			set;
		}
	}
}
