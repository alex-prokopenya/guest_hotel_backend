using GuestService.Data;
using System;
namespace GuestService.Models.Excursion
{
	public class ExtendedDescriptionParam : DescriptionParam, IPartnerParam
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
		public System.DateTime? fd
		{
			get;
			set;
		}
		public System.DateTime? FirstDate
		{
			get
			{
				return this.fd;
			}
		}
		public System.DateTime? ld
		{
			get;
			set;
		}
		public System.DateTime? LastDate
		{
			get
			{
				return this.ld;
			}
		}
	}
}
