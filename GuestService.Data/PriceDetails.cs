using System;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class PriceDetails
	{
		public enum PriceType
		{
			perPerson,
			perService
		}
		[XmlAttribute]
		public PriceDetails.PriceType priceType
		{
			get;
			set;
		}
		[XmlAttribute]
		public decimal service
		{
			get;
			set;
		}
		[XmlAttribute]
		public decimal adult
		{
			get;
			set;
		}
		[XmlAttribute]
		public decimal child
		{
			get;
			set;
		}
		[XmlAttribute]
		public decimal infant
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
		[XmlAttribute]
		public int minGroup
		{
			get;
			set;
		}
		[XmlAttribute]
		public int maxGroup
		{
			get;
			set;
		}
	}
}
