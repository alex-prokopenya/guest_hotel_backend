using System;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class CatalogExcursionMinPrice
	{
		[XmlElement("Excursion")]
		public CatalogExcursion excursion
		{
			get;
			set;
		}
		[XmlElement("MinPrice")]
		public PriceSummary minPrice
		{
			get;
			set;
		}
		[XmlElement("Rank")]
		public CatalogExcursionRanking ranking
		{
			get;
			set;
		}
	}
}
