using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class SearchExcursionResult
	{
		[XmlArray("Geographies")]
		public System.Collections.Generic.List<SearchGeography> geography
		{
			get;
			set;
		}
	}
}
