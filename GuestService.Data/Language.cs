using System;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class Language
	{
		[XmlAttribute]
		public int id
		{
			get;
			set;
		}
		[XmlAttribute]
		public string alias
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
	}
}
