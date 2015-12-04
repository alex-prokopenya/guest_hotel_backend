using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace GuestService.Data
{
	[XmlRoot("CategoryGroupWithCategoriesList")]
	public class CategoryGroupWithCategoriesList : System.Collections.Generic.List<CategoryGroupWithCategories>
	{
		public CategoryGroupWithCategoriesList(System.Collections.Generic.IEnumerable<CategoryGroupWithCategories> collection) : base(collection)
		{
		}
	}
}
