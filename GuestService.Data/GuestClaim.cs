using System;
using System.Collections.Generic;
namespace GuestService.Data
{
    public class GuestClaim
    {
        public int claim
        {
            get;
            set;
        }

        public DateTime bookDate
        {
            get;
            set;
        }

        public int price
        {
            get;
            set;
        }

        public string rate
        {
            get;
            set;
        }

        public string status
        {
            get;
            set;
        }

        public DatePeriod period
        {
            get;
            set;
        }
        public string tourname
        {
            get;
            set;
        }
        public System.Collections.Generic.List<GuestOrder> orders
        {
            get;
            set;
        }
    }
}
