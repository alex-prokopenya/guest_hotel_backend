using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class CatalogResult
	{
		[XmlArray("Excursions")]
		public System.Collections.Generic.List<CatalogExcursionMinPrice> excursions
		{
			get;
			set;
		}
	}
}
