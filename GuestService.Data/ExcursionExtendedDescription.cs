using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class ExcursionExtendedDescription : ExcursionDescription
	{
		[XmlArray("ExcursionDates")]
		public System.Collections.Generic.List<ExcursionDate> excursiondates
		{
			get;
			set;
		}
		[XmlArray("CategoryGroups")]
		public System.Collections.Generic.List<CatalogFilterCategoryGroup> categorygroups
		{
			get;
			set;
		}
		[XmlElement("Ranking")]
		public CatalogDescriptionExcursionRanking ranking
		{
			get;
			set;
		}
		[XmlArray("SurveyNotes")]
		public System.Collections.Generic.List<ExcursionSurveyNote> surveynotes
		{
			get;
			set;
		}
		public ExcursionExtendedDescription()
		{
		}
		public ExcursionExtendedDescription(ExcursionDescription item)
		{
			if (item == null)
			{
				throw new System.ArgumentNullException("item");
			}
			base.excursion = item.excursion;
			base.pictures = item.pictures;
			base.description = item.description;
		}
	}
}
