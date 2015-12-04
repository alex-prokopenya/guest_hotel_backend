using System;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class CatalogDescriptionExcursionRankItem
	{
		[XmlAttribute("name")]
		public string name
		{
			get;
			set;
		}
		[XmlAttribute("rank")]
		public decimal? rank
		{
			get;
			set;
		}
	}
}
