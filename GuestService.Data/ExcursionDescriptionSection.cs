using System;
using System.Collections.Generic;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class ExcursionDescriptionSection
	{
		[XmlAttribute]
		public string title
		{
			get;
			set;
		}
		[XmlArray("Paragraphs"), XmlArrayItem("Paragraph")]
		public System.Collections.Generic.List<string> paragraphs
		{
			get;
			set;
		}
		[XmlArray("Sections")]
		public System.Collections.Generic.List<ExcursionDescriptionSection> sections
		{
			get;
			set;
		}
	}
}
