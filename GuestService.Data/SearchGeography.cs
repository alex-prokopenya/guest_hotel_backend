using System;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class SearchGeography
	{
		[XmlAttribute]
		public string name
		{
			get;
			set;
		}
		[XmlAttribute]
		public string geotype
		{
			get;
			set;
		}
		[XmlAttribute]
		public int[] destinations
		{
			get;
			set;
		}
	}
}
