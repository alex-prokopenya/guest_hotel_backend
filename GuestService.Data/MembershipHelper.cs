using Sm.System.Database;
using System;
using System.Data;
using System.Linq;
namespace GuestService.Data
{
	public static class MembershipHelper
	{
		private class GuestFactory
		{
			internal GuestClaim GuestClaim(DataRow row)
			{
				return new GuestClaim
				{
					claim = row.ReadInt("claim$id"),
					period = new DatePeriod
					{
						begin = new System.DateTime?(row.ReadDateTime("claim$datebeg")),
						end = new System.DateTime?(row.ReadDateTime("claim$dateend"))
					},
					tourname = row.ReadNullableTrimmedString("tour$name")
				};
			}
		}
		private static MembershipHelper.GuestFactory factory = new MembershipHelper.GuestFactory();
		public static string GetUserConfirmationToken(int userId)
		{
			DataSet ds = DatabaseOperationProvider.Query("select ConfirmationToken from [webpages_Membership] where UserId = @userId", "user", new
			{
				userid = userId
			});
			return (
				from DataRow m in ds.Tables["user"].Rows
				select m.ReadNullableTrimmedString("ConfirmationToken")).FirstOrDefault<string>();
		}
	}
}
