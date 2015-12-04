using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace GuestService.Data
{
	[XmlRoot("ExcursionPickupHotelsList")]
	public class ExcursionPickupHotelsList : System.Collections.Generic.List<ExcursionPickupHotel>
	{
		public ExcursionPickupHotelsList(System.Collections.Generic.IEnumerable<ExcursionPickupHotel> collection) : base(collection)
		{
		}
	}
}
