using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class DeparturesResult
	{
		[XmlArray("Departures")]
		public System.Collections.Generic.List<GeoArea> departures
		{
			get;
			set;
		}
	}
}
