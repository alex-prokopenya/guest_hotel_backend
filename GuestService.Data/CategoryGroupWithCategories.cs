using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class CategoryGroupWithCategories
	{
		[XmlElement("Group")]
		public CategoryGroup group
		{
			get;
			set;
		}
		[XmlArray("Categories")]
		public System.Collections.Generic.List<Category> categories
		{
			get;
			set;
		}
	}
}
