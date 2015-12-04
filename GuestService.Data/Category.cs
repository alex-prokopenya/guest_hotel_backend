using System;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class Category
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
		[XmlAttribute]
		public string description
		{
			get;
			set;
		}
	}
}
