using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class DestinationsAndCategoryGroupsResult
	{
		[XmlArray("DestinationStates")]
		public System.Collections.Generic.List<GeoArea> destinationstates
		{
			get;
			set;
		}
		[XmlArray("CategoryGroups")]
		public System.Collections.Generic.List<CategoryGroupWithCategories> categorygroups
		{
			get;
			set;
		}
	}
}
