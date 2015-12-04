using System;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class ExcursionPicture
	{
		[XmlAttribute]
		public int ex
		{
			get;
			set;
		}
		[XmlAttribute]
		public int index
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
