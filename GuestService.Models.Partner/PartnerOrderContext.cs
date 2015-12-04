using GuestService.Data;
using System;
using System.Collections.Generic;
namespace GuestService.Models.Partner
{
	public class PartnerOrderContext
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

        public bool ShowOrderFindForm
		{
			get;
			set;
		}
		public PartnerOrderModel OrderFindForm
		{
			get;
			set;
		}
		public bool OrderFindNotFound
		{
			get;
			set;
		}

        public List<PartnerOrder> Orders
        {
            get;
            set;
        }

        public List<ReservationState> Claims
        {
            get;
            set;
        }
        public bool ClaimsNotFound
		{
			get;
			set;
		}
		public bool ShowOtherClaims
		{
			get;
			set;
		}
		public System.Collections.Generic.List<GuestClaim> OtherClaims
		{
			get;
			set;
		}
		public System.Collections.Generic.List<ExcursionTransfer> ExcursionTransfers
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

        public int FilterSelectedLanguage
        {
            get;
            set;
        }

        public int FilterSelectedExcursion
        {
            get;
            set;
        }

        public System.Collections.Generic.List<CatalogFilterItem> FilterExcursions
        {
            get;
            set;
        }

        public System.Collections.Generic.List<CatalogFilterItem> FilterLanguages
        {
            get;
            set;
        }
    }
}
