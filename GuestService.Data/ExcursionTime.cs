using System;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class ExcursionTime
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
