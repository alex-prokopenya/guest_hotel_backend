using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class CatalogExcursion
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
		public string url
		{
			get;
			set;
		}
		[XmlArray("Destinations")]
		public System.Collections.Generic.List<GeoArea> destinations
		{
			get;
			set;
		}
		[XmlIgnore]
		public System.TimeSpan? duration
		{
			get;
			set;
		}
		[XmlArray("Departures")]
		public System.Collections.Generic.List<GeoArea> departures
		{
			get;
			set;
		}
		[XmlElement("ExcursionPartner")]
		public Partner excursionPartner
		{
			get;
			set;
		}
		[XmlArray("Languages")]
		public System.Collections.Generic.List<Language> languages
		{
			get;
			set;
		}
		[XmlArray("Categories")]
		public System.Collections.Generic.List<ExcursionCategory> categories
		{
			get;
			set;
		}
		[JsonIgnore, XmlAttribute("duration")]
		public string DurationString
		{
			get
			{
				return (!this.duration.HasValue) ? null : XmlConvert.ToString(this.duration.Value);
			}
			set
			{
				this.duration = (string.IsNullOrEmpty(value) ? null : new System.TimeSpan?(XmlConvert.ToTimeSpan(value)));
			}
		}
	}
}
