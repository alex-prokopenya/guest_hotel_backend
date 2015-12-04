using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace GuestService.Data
{
	[XmlRoot("ExcursionExtendedDescriptionList")]
	public class ExcursionExtendedDescriptionList : System.Collections.Generic.List<ExcursionExtendedDescription>
	{
		public ExcursionExtendedDescriptionList()
		{
		}
		public ExcursionExtendedDescriptionList(System.Collections.Generic.IEnumerable<ExcursionExtendedDescription> collection) : base(collection)
		{
		}
	}
}
