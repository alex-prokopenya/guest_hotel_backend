using System;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class ExcursionDate
	{
		[XmlAttribute]
		public System.DateTime date
		{
			get;
			set;
		}
		[XmlAttribute]
		public bool isprice
		{
			get;
			set;
		}
		[XmlAttribute]
		public bool isstopsale
		{
			get;
			set;
		}
		[XmlAttribute]
		public bool allclosed
		{
			get;
			set;
		}
	}
}
