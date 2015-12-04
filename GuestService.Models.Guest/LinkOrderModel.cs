using System;
using System.ComponentModel.DataAnnotations;
namespace GuestService.Models.Guest
{
	public class LinkOrderModel
	{
		[Required]
		public string Name
		{
			get;
			set;
		}
		[Required]
		public int? Claim
		{
			get;
			set;
		}
	}
}
