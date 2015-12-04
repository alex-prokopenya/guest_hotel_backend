using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GuestService.Models
{
	[System.ComponentModel.DataAnnotations.Schema.Table("guestservice_UserProfile")]
	public class UserProfile
	{
		[Key, System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
		public int UserId
		{
			get;
			set;
		}
		public string UserName
		{
			get;
			set;
		}
	}
}
