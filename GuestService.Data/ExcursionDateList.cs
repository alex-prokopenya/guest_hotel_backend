using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace GuestService.Data
{
	[XmlRoot("ExcursionDateList")]
	public class ExcursionDateList : System.Collections.Generic.List<ExcursionDate>
	{
		public ExcursionDateList(System.Collections.Generic.IEnumerable<ExcursionDate> collection) : base(collection)
		{
		}
	}
}
