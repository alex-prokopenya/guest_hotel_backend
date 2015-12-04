using System;
using System.Data.Entity;
namespace GuestService.Models
{
	public class UsersContext : DbContext
	{
		public DbSet<UserProfile> UserProfiles
		{
			get;
			set;
		}
		public UsersContext() : base("db")
		{
		}
	}
}
