using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace GuestService.Data
{
	[XmlRoot("CategoryWithGroupList")]
	public class CategoryWithGroupList : System.Collections.Generic.List<CategoryWithGroup>
	{
		public CategoryWithGroupList(System.Collections.Generic.IEnumerable<CategoryWithGroup> collection) : base(collection)
		{
		}
	}
}
