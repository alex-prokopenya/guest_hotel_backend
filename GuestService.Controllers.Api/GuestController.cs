using GuestService.Data;
using GuestService.Models.Guest;
using Sm.System.Mvc.Language;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Sm.System.Database;
namespace GuestService.Controllers.Api
{
	[HttpUrlLanguage, Authorize]
	public class GuestController : ApiController
	{
        public static string GetPartnerName(int userId)
        {
            var res = DatabaseOperationProvider.Query("select b.name from guestservice_UserProfile as a, partner as b where a.userId = @userId and a.partnerId = b.inc ", "services", new { userId = userId });

            if (res.Tables[0].Rows.Count > 0)
                return res.Tables[0].Rows[0].ReadNullableTrimmedString("name").ToString();
            else
                return "";
        }

        [ActionName("departure"), AllowAnonymous, HttpGet]
		public System.Collections.Generic.List<DepartureHotel> Departure([FromUri] DepartureParam param)
		{
			if (param == null)
			{
				throw new System.ArgumentNullException("param");
			}
			if (!param.FirstDate.HasValue)
			{
				throw new System.ArgumentNullException("fd");
			}
			if (!param.LastDate.HasValue)
			{
				throw new System.ArgumentNullException("ld");
			}
			if (!param.Hotel.HasValue && !param.Claim.HasValue)
			{
				throw new System.ArgumentException("'h' or 'c' should be specified");
			}
			return GuestProvider.GetDepartureInfo(param.Language, param.FirstDate.Value, param.LastDate.Value, param.Hotel, param.Claim);
		}
	}
}
