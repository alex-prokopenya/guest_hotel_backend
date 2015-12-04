using System;
using System.ComponentModel.DataAnnotations;
namespace GuestService.Models.Guest
{
	public class UnlinkOrderModel
	{
		[Required]
		public int? Claim
		{
			get;
			set;
		}
	}
}
