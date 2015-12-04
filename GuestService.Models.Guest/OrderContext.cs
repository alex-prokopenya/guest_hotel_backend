using GuestService.Data;
using System;
using System.Collections.Generic;
namespace GuestService.Models.Guest
{
	public class OrderContext
	{
		public bool ShowOrderFindForm
		{
			get;
			set;
		}
		public OrderModel OrderFindForm
		{
			get;
			set;
		}
		public bool OrderFindNotFound
		{
			get;
			set;
		}
		public ReservationState Claim
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
		public UnlinkOrderModel Unlink
		{
			get;
			set;
		}
		public System.Collections.Generic.List<ExcursionTransfer> ExcursionTransfers
		{
			get;
			set;
		}
	}
}
