using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class FilterDetailsResult
	{
		[XmlArray("CategoryGroups")]
		public System.Collections.Generic.List<CatalogFilterCategoryGroup> categorygroups
		{
			get;
			set;
		}
		[XmlArray("Departures")]
		public System.Collections.Generic.List<CatalogFilterLocationItem> departures
		{
			get;
			set;
		}
		[XmlArray("Destinations")]
		public System.Collections.Generic.List<CatalogFilterLocationWithStateItem> destinations
		{
			get;
			set;
		}
		[XmlArray("Languages")]
		public System.Collections.Generic.List<CatalogFilterItem> languages
		{
			get;
			set;
		}
		[XmlElement("Durations")]
		public CatalogFilterDuration durations
		{
			get;
			set;
		}
		[XmlArray("DestinationStates")]
		public System.Collections.Generic.List<GeoArea> destinationstates
		{
			get;
			set;
		}
	}
}
