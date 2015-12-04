using System;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class CategoryWithGroup : Category
	{
		[XmlElement("Group")]
		public CategoryGroup group
		{
			get;
			set;
		}
	}
}
