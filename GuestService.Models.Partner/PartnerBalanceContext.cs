using GuestService.Data;
using System;
using System.Collections.Generic;
namespace GuestService.Models.Partner
{
	public class PartnerBalanceContext
	{
        public int PartnerId
        {
            get;
            set;
        }

        public string PartnerAuth
        {
            get;
            set;
        }

        public string FilterDateFrom
        {
            get;
            set;
        }

        public string FilterDateTo
        {
            get;
            set;
        }
    }
}
