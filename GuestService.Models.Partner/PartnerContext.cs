using GuestService.Data;
using System;
using System.Collections.Generic;
namespace GuestService.Models.Partner
{
    public class PartnerContext
    {
        public string UserName
        {
            get;
            set;
        }
        public string ProviderName
        {
            get;
            set;
        }

        public bool ShowAuthenticationMessage
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