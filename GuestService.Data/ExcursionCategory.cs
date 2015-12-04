using System;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class ExcursionCategory
	{
		[XmlAttribute]
		public int id
		{
			get;
			set;
		}
		[XmlAttribute]
		public string name
		{
			get;
			set;
		}
		[XmlElement("CategoryGroup")]
		public CategoryGroup categorygroup
		{
			get;
			set;
		}
		[XmlIgnore]
		public int? sort
		{
			get;
			set;
		}
	}
}
