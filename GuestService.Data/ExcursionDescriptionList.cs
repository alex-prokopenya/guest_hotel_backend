using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace GuestService.Data
{
	[XmlRoot("ExcursionDescriptionList")]
	public class ExcursionDescriptionList : System.Collections.Generic.List<ExcursionDescription>
	{
		public ExcursionDescriptionList(System.Collections.Generic.IEnumerable<ExcursionDescription> collection) : base(collection)
		{
		}
	}
}
