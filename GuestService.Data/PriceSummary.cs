using System;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class PriceSummary
	{
		public enum PriceType
		{
			perPerson,
			perService
		}
		[XmlAttribute]
		public PriceSummary.PriceType priceType
		{
			get;
			set;
		}
		[XmlAttribute]
		public decimal price
		{
			get;
			set;
		}
		[XmlAttribute]
		public string currency
		{
			get;
			set;
		}
	}
}
