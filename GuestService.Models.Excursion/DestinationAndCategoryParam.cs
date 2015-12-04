using GuestService.Data;
using System;
namespace GuestService.Models.Excursion
{
	public class DestinationAndCategoryParam : CategoryParam, IPartnerParam
	{
		public string pa
		{
			get;
			set;
		}
		public string PartnerAlias
		{
			get
			{
				return this.pa;
			}
		}
		public string psid
		{
			get;
			set;
		}
		public string PartnerSessionID
		{
			get
			{
				return this.psid;
			}
		}
	}
}
