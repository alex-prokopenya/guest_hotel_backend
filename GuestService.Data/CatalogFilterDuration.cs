using Newtonsoft.Json;
using System;
using System.Xml;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class CatalogFilterDuration
	{
		[XmlIgnore]
		public System.TimeSpan min
		{
			get;
			set;
		}
		[XmlIgnore]
		public System.TimeSpan max
		{
			get;
			set;
		}
		[JsonIgnore, XmlAttribute(AttributeName = "min")]
		public string MinString
		{
			get
			{
				return XmlConvert.ToString(this.min);
			}
			set
			{
				this.min = (string.IsNullOrEmpty(value) ? System.TimeSpan.Zero : XmlConvert.ToTimeSpan(value));
			}
		}
		[JsonIgnore, XmlAttribute(AttributeName = "max")]
		public string MaxString
		{
			get
			{
				return XmlConvert.ToString(this.max);
			}
			set
			{
				this.max = (string.IsNullOrEmpty(value) ? System.TimeSpan.Zero : XmlConvert.ToTimeSpan(value));
			}
		}
	}
}
