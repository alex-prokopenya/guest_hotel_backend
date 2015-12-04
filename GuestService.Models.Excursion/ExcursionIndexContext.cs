using System;
namespace GuestService.Models.Excursion
{
	public class ExcursionIndexContext
	{
		public string PartnerAlias
		{
			get;
			set;
		}
		public string StartPointAlias
		{
			get;
			set;
		}
		public System.DateTime ExcursionDate
		{
			get;
			set;
		}
		public ExcursionIndexNavigateCommand NavigateState
		{
			get;
			set;
		}
	}
}
