using GuestService.Data;
using System;
using System.Collections.Generic;
namespace GuestService.Models.Guest
{
	public class GuestContext
	{
		public bool ShowAuthenticationMessage
		{
			get;
			set;
		}
        public string GuestPartnerName
        {
            get;
            set;
        }
        public System.Collections.Generic.List<HotelGuideResult> GuideDurties
		{
			get;
			set;
		}
	}
}
