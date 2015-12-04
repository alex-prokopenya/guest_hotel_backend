using Sm.System.Database;
using Sm.System.Exceptions;
using System;
using System.Data;
using System.Linq;
namespace GuestService.Data
{
	public static class UserToolsProvider
	{
		private class UserToolsFactory
		{
			internal WebPartner WebPartner(DataRow row)
			{
				return new WebPartner
				{
					id = row.ReadInt("partner"),
					passId = row.ReadInt("partpass")
				};
			}
			internal WebPartner OnlinePartner(DataRow row)
			{
				return new WebPartner
				{
					id = row.ReadInt("partner"),
					passId = row.ReadInt("partpass"),
					alias = row.ReadNullableTrimmedString("alias")
				};
			}
		}
		private static UserToolsProvider.UserToolsFactory factory = new UserToolsProvider.UserToolsFactory();
		public static WebPartner FindPartnerForPublicWeb(string alias)
		{
			if (string.IsNullOrEmpty(alias))
			{
				throw new ArgumentNullExceptionWithCode(214, "alias");
			}
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_getPartnerForPublicWeb", "partner", new
			{
				alias
			});
			WebPartner result = (
				from DataRow row in ds.Tables["partner"].Rows
				select UserToolsProvider.factory.WebPartner(row)).FirstOrDefault<WebPartner>();
			if (result != null)
			{
				result.alias = alias;
			}
			return result;
		}
		public static WebPartner FindPartnerByOnlineSID(string sid)
		{
			if (string.IsNullOrEmpty(sid))
			{
				throw new ArgumentNullExceptionWithCode(215, "sid");
			}
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_findPartnerByOnlineSID", "partner", new
			{
				sid
			});
			return (
				from DataRow row in ds.Tables["partner"].Rows
				select UserToolsProvider.factory.OnlinePartner(row)).FirstOrDefault<WebPartner>();
		}
		public static WebPartner GetPartner(IPartnerParam param)
		{
			if (param == null)
			{
				throw new System.ArgumentNullException("param");
			}
			WebPartner result;
			if (!string.IsNullOrEmpty(param.PartnerSessionID))
			{
				WebPartner partner = UserToolsProvider.FindPartnerByOnlineSID(param.PartnerSessionID);
				if (partner == null)
				{
					throw new ExceptionWithCode(213, string.Format("invalid sid '{0}'", param.PartnerSessionID));
				}
				result = partner;
			}
			else
			{
				if (!string.IsNullOrEmpty(param.PartnerAlias))
				{
					WebPartner partner = UserToolsProvider.FindPartnerForPublicWeb(param.PartnerAlias);
					if (partner == null)
					{
						throw new ArgumentExceptionWithCode(212, string.Format("invalid partner alias '{0}'", param.PartnerAlias));
					}
					result = partner;
				}
				else
				{
					string defaultPartnerAlias = Settings.ExcursionDefaultPartnerAlias;
					if (string.IsNullOrEmpty(defaultPartnerAlias))
					{
						throw new ExceptionWithCode(210, "partner authentication required: use 'pa' or 'psid' params");
					}
					WebPartner partner = UserToolsProvider.FindPartnerForPublicWeb(defaultPartnerAlias);
					if (partner == null)
					{
						throw new ArgumentExceptionWithCode(211, string.Format("invalid default partner alias '{0}'", defaultPartnerAlias));
					}
					result = partner;
				}
			}
			return result;
		}
		public static void UmgRaiseMessage(string lang, string title, string address, string template, string data)
		{
			DatabaseOperationProvider.ExecuteProcedure("umg.up_RaiseMessage", new
			{
				type = "MAIL",
				address = address,
				lang = lang,
				title = title,
				template = template,
				addresseedata = data
			});
		}
	}
}
