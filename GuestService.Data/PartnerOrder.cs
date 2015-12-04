using System;
namespace GuestService.Data
{
	public class PartnerOrder
	{
        public int orderId
        {
            get;
            set;
        }

        public string claimId
        {
            get;
            set;
        }

        public string beginDate
		{
			get;
			set;
		}

		public string title
		{
			get;
			set;
		}

		public string status
		{
			get;
			set;
		}

        public string language
        {
            get;
            set;
        }

        public int adults
        {
            get;
            set;
        }

        public int childs
        {
            get;
            set;
        }

        public int infs
        {
            get;
            set;
        }
        public string pickup
        {
            get;
            set;
        }
        public string customerName
        {
            get;
            set;
        }
        public string customerAddress
        {
            get;
            set;
        }
    }
}
