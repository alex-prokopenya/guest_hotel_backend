using System;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class ExcursionGroup
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
	}
}
