using GuestService.Data;
using GuestService.Models.Booking;
using Sm.System.Exceptions;
using Sm.System.Mvc.Language;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebMatrix.WebData;
namespace GuestService.Controllers.Api
{
	[HttpUrlLanguage, Authorize]
	public class BookingController : ApiController
	{
		[ActionName("state"), AllowAnonymous, HttpGet]
		public ReservationState State(int? id, [FromUri] StatusParams param)
		{
			if (!id.HasValue)
			{
				throw new ArgumentNullExceptionWithCode(110, "id");
			}
			if (param == null)
			{
				throw new System.ArgumentNullException("param");
			}
			WebPartner partner = UserToolsProvider.GetPartner(param);
			ReservationState result = BookingProvider.GetReservationState(param.Language, id.Value);
			ReservationState result2;
			if (result != null && result.claimId.HasValue)
			{
				if (param.PartnerSessionID != null && result.partner != null && result.partner.id == partner.id)
				{
					result2 = result;
					return result2;
				}
				if (WebSecurity.CurrentUserId > 0)
				{
					System.Collections.Generic.List<GuestClaim> claims = GuestProvider.GetLinkedClaims(param.Language, WebSecurity.CurrentUserId);
					bool arg_10E_0;
					if (claims != null)
					{
						arg_10E_0 = (claims.FirstOrDefault((GuestClaim m) => m.claim == result.claimId.Value) == null);
					}
					else
					{
						arg_10E_0 = true;
					}
					if (!arg_10E_0)
					{
						result2 = result;
						return result2;
					}
				}
			}
			result2 = null;
			return result2;
		}
		[ActionName("calculate"), AllowAnonymous, HttpPost]
		public ReservationState Calculate([FromUri] BookingCartParam param, [FromBody] BookingClaim claim)
		{
			if (param == null)
			{
				throw new System.ArgumentNullException("param");
			}
			if (claim == null)
			{
				throw new System.ArgumentNullException("claim");
			}
			WebPartner partner = UserToolsProvider.GetPartner(param);
			return BookingProvider.DoCalculation(param.Language, partner.id, claim);
		}
		[ActionName("book"), AllowAnonymous, HttpPost]
		public ReservationState Book([FromUri] BookingCartParam param, [FromBody] BookingClaim claim)
		{
			if (param == null)
			{
				throw new System.ArgumentNullException("param");
			}
			if (claim == null)
			{
				throw new System.ArgumentNullException("claim");
			}
			WebPartner partner = UserToolsProvider.GetPartner(param);
			if (claim.customer != null)
			{
				foreach (BookingOrder order in claim.orders)
				{
					if (order.excursion != null)
					{
						if (order.excursion.contact == null)
						{
							order.excursion.contact = new ExcursionContact();
						}
						if (order.excursion.contact.name == null)
						{
							order.excursion.contact.name = claim.customer.name;
						}
						if (order.excursion.contact.mobile == null)
						{
							order.excursion.contact.mobile = claim.customer.mobile;
						}
					}
				}
			}
			return BookingProvider.DoBooking(param.Language, partner.id, partner.passId, claim);
		}
		[ActionName("agreement"), AllowAnonymous, HttpGet]
		public BookingAgreement Agreement([FromUri] AgreementParam param)
		{
			BookingAgreement result = new BookingAgreement();
			string path = HttpContext.Current.Server.MapPath(CustomizationPath.AgreementsFolder);
			string name = System.IO.Path.Combine(path, string.Format("Booking{0}{1}.txt", ".", UrlLanguage.CurrentLanguage));
			if (System.IO.File.Exists(name))
			{
				result.text = System.IO.File.ReadAllText(name);
			}
			else
			{
				name = System.IO.Path.Combine(path, string.Format("Booking{0}{1}.txt", "", ""));
				if (System.IO.File.Exists(name))
				{
					result.text = System.IO.File.ReadAllText(name);
				}
			}
			return result;
		}
	}
}
