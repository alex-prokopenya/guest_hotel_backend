using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace GuestService.Data
{
	[XmlRoot("ExcursionPriceList")]
	public class ExcursionPriceList : System.Collections.Generic.List<ExcursionPrice>
	{
		public ExcursionPriceList(System.Collections.Generic.IEnumerable<ExcursionPrice> collection) : base(collection)
		{
		}
	}
}
