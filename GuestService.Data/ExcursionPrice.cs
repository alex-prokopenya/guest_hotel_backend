using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
namespace GuestService.Data
{
    public class ExcursionPrice
    {
        [XmlAttribute]
        public int id
        {
            get;
            set;
        }
        [XmlAttribute]
        public System.DateTime date
        {
            get;
            set;
        }
        [XmlElement("Language")]
        public Language language
        {
            get;
            set;
        }
        [XmlElement("Group")]
        public ExcursionGroup group
        {
            get;
            set;
        }
        [XmlElement("Time")]
        public ExcursionTime time
        {
            get;
            set;
        }
        [XmlElement("Departures")]
        public System.Collections.Generic.List<GeoArea> departures
        {
            get;
            set;
        }
        [XmlIgnore]
        public System.DateTime? closesaletime
        {
            get;
            set;
        }

        [XmlAttribute]
        public bool issaleclosed
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

        [XmlElement("FreeSeats")]
        public int? freeseats
        {
            get;
            set;
        }

        [XmlElement("TotalSeats")]
        public int? totalseats
        {
            get;
            set;
        }

        [XmlElement("Price")]
        public PriceDetails price
        {
            get;
            set;
        }
        [XmlElement("PickupPoints")]
        public System.Collections.Generic.List<ExcursionPickup> pickuppoints
        {
            get;
            set;
        }
        [JsonIgnore, XmlAttribute("closesaletime")]
        public string ClosesaletimeString
        {
            get
            {
                return (!this.closesaletime.HasValue) ? null : XmlConvert.ToString(this.closesaletime.Value, XmlDateTimeSerializationMode.Unspecified);
            }
            set
            {
                this.closesaletime = (string.IsNullOrEmpty(value) ? null : new System.DateTime?(XmlConvert.ToDateTime(value, XmlDateTimeSerializationMode.Unspecified)));
            }
        }
    }
}
