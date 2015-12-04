using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class ExcursionDescription
	{
		[XmlElement("Excursion")]
		public CatalogExcursion excursion
		{
			get;
			set;
		}
		[XmlArray("Pictures")]
		public System.Collections.Generic.List<ExcursionPicture> pictures
		{
			get;
			set;
		}
		[XmlArray("Description")]
		public System.Collections.Generic.List<ExcursionDescriptionSection> description
		{
			get;
			set;
		}
	}
}
