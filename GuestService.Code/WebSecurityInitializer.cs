using GuestService.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WebMatrix.WebData;
namespace GuestService.Code
{
	public static class WebSecurityInitializer
	{
		private static object isInitializedSyncObject = new object();
		private static bool isInitialized = false;
		public static void Initialize()
		{
			if (!WebSecurityInitializer.isInitialized)
			{
				lock (WebSecurityInitializer.isInitializedSyncObject)
				{
					if (!WebSecurityInitializer.isInitialized)
					{
						WebSecurityInitializer.isInitialized = true;
						Database.SetInitializer<UsersContext>(null);
						try
						{
							using (UsersContext context = new UsersContext())
							{
								if (!context.Database.Exists())
								{
									((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
								}
							}
							bool autoCreateTables = true;
							WebSecurity.InitializeDatabaseConnection("db", "guestservice_UserProfile", "userId", "userName", autoCreateTables);
						}
						catch (System.Exception ex)
						{
							throw new System.InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
						}
					}
				}
			}
		}
	}
}
