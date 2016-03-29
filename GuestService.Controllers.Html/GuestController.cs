using GuestService.Code;
using GuestService.Controllers.Api;
using GuestService.Data;
using GuestService.Models.Guest;
using GuestService.Models.Guide;
using Sm.Report;
using Sm.System.Mvc;
using Sm.System.Database;
using Sm.System.Mvc.Language;
using Sm.System.Trace;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using WebMatrix.WebData;
namespace GuestService.Controllers.Html
{
	[HttpPreferences, WebSecurityInitializer, UrlLanguage, Authorize]
	public class GuestController : BaseController
	{
		[ActionName("index"), AllowAnonymous, HttpGet]
		public ActionResult Index(GuestWebParams param)
		{
            if (!WebSecurity.IsAuthenticated)
            {
                string str = base.Url.RouteUrl(base.Request.QueryStringAsRouteValues());
                return base.RedirectToAction("login", "account", new { returnUrl = str });
            }

            GuestContext context = new GuestContext();

            context.GuestPartnerName = GuestService.Controllers.Api.GuestController.GetPartnerName(WebSecurity.CurrentUserId);

            if (WebSecurity.IsAuthenticated)
			{
				System.DateTime currentDate = (param != null && param.TestDate.HasValue) ? param.TestDate.Value.Date : System.DateTime.Now.Date;
				context.GuideDurties = new System.Collections.Generic.List<HotelGuideResult>();
				System.Collections.Generic.List<GuestOrder> horders = GuestProvider.GetActiveHotelOrders(UrlLanguage.CurrentLanguage, WebSecurity.CurrentUserId, currentDate, currentDate.AddDays(1.0));
				if (horders != null && horders.Count > 0)
				{
					GuideController guideController = new GuideController();
					foreach (GuestOrder horder in horders)
					{
						HotelGuideResult durties = guideController.HotelGuide(new HotelGuideParam
						{
							h = horder.hotelid,
							ln = UrlLanguage.CurrentLanguage,
							pb = new System.DateTime?(horder.period.begin.Value),
							pe = new System.DateTime?(horder.period.end.Value)
						});
						context.GuideDurties.Add(durties);
					}
				}
			}
			else
			{
				context.ShowAuthenticationMessage = true;
			}
			return base.View(context);
		}

		[ActionName("order"), HttpGet]
		public ActionResult Order(int? id)
		{
            //GuestController.<>c__DisplayClass4 <>c__DisplayClass = new GuestController.<>c__DisplayClass4();
            //<>c__DisplayClass.id = id;
            //OrderContext context = new OrderContext();
            //bool flag = 1 == 0;
            //int guestId = WebSecurity.CurrentUserId;
            //System.Collections.Generic.List<GuestClaim> claims = GuestProvider.GetLinkedClaims(UrlLanguage.CurrentLanguage, guestId);
            //int? detailedId = null;
            //if (<>c__DisplayClass.id.HasValue)
            //{
            //	if (claims.FirstOrDefault((GuestClaim m) => m.claim == <>c__DisplayClass.id.Value) != null)
            //	{
            //		detailedId = new int?(<>c__DisplayClass.id.Value);
            //	}
            //}

            OrderContext model = new OrderContext();
            int currentUserId = WebSecurity.CurrentUserId;
            List<GuestClaim> linkedClaims = GuestProvider.GetLinkedClaims(UrlLanguage.CurrentLanguage, currentUserId);
            int? detailedId = null;
            if (id.HasValue)
            {
                if (linkedClaims.FirstOrDefault((GuestClaim m) => m.claim == id.Value) != null)
                {
                    detailedId = new int?(id.Value);
                }
            }

          
            if (!(detailedId.HasValue || (linkedClaims.Count <= 0)))
            {
                detailedId = new int?(linkedClaims[0].claim);
            }
            if (detailedId.HasValue)
            {
                ReservationState reservationState = BookingProvider.GetReservationState(UrlLanguage.CurrentLanguage, detailedId.Value);
                if ((reservationState != null) && reservationState.claimId.HasValue)
                {
                    model.Claim = reservationState;
                    model.Claim.agentPaymentAllowed = PartnerProvider.GetPaymentAllowed(currentUserId);
                    model.ExcursionTransfers = GuestProvider.GetExcursionTransferByClaim(UrlLanguage.CurrentLanguage, reservationState.claimId.Value);
                }
            }
            model.ClaimsNotFound = linkedClaims.Count == 0;
            model.ShowOtherClaims = true;
            model.OtherClaims = (
                  from m in linkedClaims

                      //удалить детальн
                      //  where m.claim != detailedId
                  select m).ToList<GuestClaim>();

            return base.View(model);
        }

		[ActionName("order"), HttpPost]
		public ActionResult Order([Bind(Prefix = "OrderFindForm")] OrderModel model)
		{
			OrderContext context = new OrderContext();
			context.ShowOrderFindForm = true;
			if (base.ModelState.IsValid)
			{
				System.Collections.Generic.List<GuestClaim> claims = GuestProvider.FindGuestClaims(UrlLanguage.CurrentLanguage, 0, model.ClaimName, new int?(System.Convert.ToInt32(model.Claim)), null);
				if (claims != null && claims.Count > 0)
				{
					ReservationState claim = BookingProvider.GetReservationState(UrlLanguage.CurrentLanguage, claims[0].claim);
					if (claim != null && claim.claimId.HasValue)
					{
						context.Claim = claim;
                        context.Claim.agentPaymentAllowed = PartnerProvider.GetPaymentAllowed(WebSecurity.CurrentUserId);
                        context.ExcursionTransfers = GuestProvider.GetExcursionTransferByClaim(UrlLanguage.CurrentLanguage, claim.claimId.Value);
						context.ShowOrderFindForm = false;
					}
				}
			}
			context.OrderFindNotFound = (context.Claim == null);
			return base.View(context);
		}

        [ActionName("report"), HttpGet]
        public ActionResult Balance(int? id, int? excId, string dateFrom, string dateTo, int? language)
        {
            var model = new UserBalanceContext();

            int currentUserId = WebSecurity.CurrentUserId;

            model.UserId = currentUserId;

            model.UserAuth = GetMd5Hash(model.UserId);

            return base.View(model);
        }

        [HttpPost, ActionName("report")]
        public ActionResult Balance([Bind(Prefix = "OrderFindForm")] UserBalanceContext model)
        {
            model = new UserBalanceContext();

            int currentUserId = WebSecurity.CurrentUserId;

            model.UserId = currentUserId;

            model.UserAuth = GetMd5Hash(model.UserId);

            return base.View(model);
        }

        private static string GetMd5Hash(int partnerId)
        {
            byte[] keyInBytes = System.Text.UTF8Encoding.UTF8.GetBytes("secret_key");
            byte[] payloadInBytes = System.Text.UTF8Encoding.UTF8.GetBytes("secretkey" + partnerId);

            var md5 = new System.Security.Cryptography.HMACMD5(keyInBytes);
            byte[] hash = md5.ComputeHash(payloadInBytes);

            var result = BitConverter.ToString(hash).Replace("-", string.Empty);
            return result;
        }

        [ActionName("findorder"), HttpGet]
		public ActionResult FindOrder()
		{
			FindOrderContext context = new FindOrderContext();
			return base.View(context);
		}
		[ActionName("findorder"), HttpPost, ValidateAntiForgeryToken]
		public ActionResult FindOrderPost(string id, [Bind(Prefix = "Form")] FindOrderModel form)
		{
			FindOrderContext context = new FindOrderContext();
			context.Form = form;
			if (form.RequestType != "claim")
			{
				this.ClearErrorState(base.ModelState["Form.Claim"]);
				this.ClearErrorState(base.ModelState["Form.ClaimName"]);
			}
			if (form.RequestType != "passport")
			{
				this.ClearErrorState(base.ModelState["Form.Passport"]);
				this.ClearErrorState(base.ModelState["Form.PassportName"]);
			}
			if (base.ModelState.IsValid)
			{
				int guestId = WebSecurity.CurrentUserId;
				if (form.RequestType == "claim")
				{
					context.Claims = GuestProvider.FindGuestClaims(UrlLanguage.CurrentLanguage, guestId, form.ClaimName, new int?(System.Convert.ToInt32(form.Claim)), null);
				}
				else
				{
					if (!(form.RequestType == "passport"))
					{
						throw new System.Exception("invalid RequestType");
					}
					context.Claims = GuestProvider.FindGuestClaims(UrlLanguage.CurrentLanguage, guestId, form.PassportName, null, form.Passport);
				}
				context.NotFound = (context.Claims.Count == 0);
			}
			return base.View(context);
		}
		[ActionName("linkorder"), HttpPost, ValidateAntiForgeryToken]
		public ActionResult LinkOrder([Bind(Prefix = "Link")] LinkOrderModel model)
		{
			ActionResult result;
			if (model != null && model.Claim.HasValue)
			{
				int guestId = WebSecurity.CurrentUserId;
				GuestProvider.LinkGuestClaim(guestId, model.Name, model.Claim.Value);
				result = base.RedirectToAction("order", new
				{
					id = model.Claim
				});
			}
			else
			{
				result = base.RedirectToAction("order");
			}
			return result;
		}
		[ActionName("unlinkorder"), HttpPost, ValidateAntiForgeryToken]
		public ActionResult UnlinkOrder([Bind(Prefix = "Unlink")] UnlinkOrderModel model)
		{
			if (model != null && model.Claim.HasValue)
			{
				int guestId = WebSecurity.CurrentUserId;
				GuestProvider.UnlinkGuestClaim(guestId, model.Claim.Value);
			}
			return base.RedirectToAction("order");
		}
		[ActionName("departure"), AllowAnonymous, HttpGet]
		public ActionResult Departure(GuestWebParams param)
		{
			System.DateTime currentDate = (param != null && param.TestDate.HasValue) ? param.TestDate.Value.Date : System.DateTime.Now.Date;
			ActionResult result;
			if (WebSecurity.IsAuthenticated)
			{
				DepartureContext context = new DepartureContext();
				System.Collections.Generic.List<GuestClaim> claims = GuestProvider.GetActiveClaims(UrlLanguage.CurrentLanguage, WebSecurity.CurrentUserId, currentDate);
				if (claims != null && claims.Count > 0)
				{
					context.Hotels = new System.Collections.Generic.List<DepartureHotel>();
					foreach (GuestClaim claim in claims)
					{
						context.Hotels.AddRange(GuestProvider.GetDepartureInfo(UrlLanguage.CurrentLanguage, currentDate, currentDate.AddDays(1.0), null, new int?(claim.claim)));
					}
				}
				result = base.View(context);
			}
			else
			{
				if (HttpPreferences.Current.LocationHotel != null)
				{
					DepartureContext context = new DepartureContext();
					context.Hotel = CatalogProvider.GetHotelDescription(UrlLanguage.CurrentLanguage, HttpPreferences.Current.LocationHotel);
					if (context.Hotel != null)
					{
						context.Hotels = GuestProvider.GetDepartureInfo(UrlLanguage.CurrentLanguage, currentDate, currentDate.AddDays(1.0), new int?(context.Hotel.id), null);
					}
					result = base.View(context);
				}
				else
				{
					string url = base.Url.RouteUrl(base.Request.QueryStringAsRouteValues());
					result = base.RedirectToAction("login", "account", new
					{
						returnUrl = url
					});
				}
			}
			return result;
		}
		[ActionName("brief"), AllowAnonymous, HttpGet]
		public ActionResult Brief(GuestWebParams param)
		{
			BriefContext context = new BriefContext();
			return base.View(context);
		}
		[ActionName("summary"), AllowAnonymous, HttpGet]
		public ActionResult Summary(GuestWebParams param)
		{
            SummaryContext model = new SummaryContext
            {
                ShowOrderFindForm = true,
                OrderFindForm = new OrderModel()
            };
            model.OrderFindForm.Claim = "";
            model.OrderFindForm.ClaimName = "";
            model.OrderFindForm.CurrentDate = new DateTime?(((param != null) && param.TestDate.HasValue) ? param.TestDate.Value.Date : DateTime.Now.Date);
            return base.View(model);
        }
		[ActionName("summary"), AllowAnonymous, HttpPost]
		public ActionResult Summary([Bind(Prefix = "OrderFindForm")] OrderModel model)
		{
			SummaryContext context = new SummaryContext();
			context.ShowOrderFindForm = true;
			if (base.ModelState.IsValid)
			{
				System.DateTime currentDate = model.CurrentDate ?? System.DateTime.Now.Date;
				System.Collections.Generic.List<GuestClaim> claims = GuestProvider.FindGuestClaims(UrlLanguage.CurrentLanguage, 0, model.ClaimName, new int?(System.Convert.ToInt32(model.Claim)), null);
				if (claims != null && claims.Count > 0)
				{
					ReservationState claim = BookingProvider.GetReservationState(UrlLanguage.CurrentLanguage, claims[0].claim);
					if (claim != null && claim.claimId.HasValue)
					{
						context.Claim = claim;
						context.ShowOrderFindForm = false;
						context.Hotels = new System.Collections.Generic.List<DepartureHotel>();
						foreach (GuestClaim c in claims)
						{
							context.Hotels.AddRange(GuestProvider.GetDepartureInfo(UrlLanguage.CurrentLanguage, currentDate, currentDate.AddDays(1.0), null, new int?(c.claim)));
						}
						context.GuideDurties = new System.Collections.Generic.List<HotelGuideResult>();
						System.Collections.Generic.List<GuestOrder> horders = GuestProvider.GetActiveHotelOrders(claims, currentDate, currentDate.AddDays(1.0));
						if (horders != null && horders.Count > 0)
						{
							GuideController guideController = new GuideController();
							foreach (GuestOrder horder in horders)
							{
								HotelGuideResult durties = guideController.HotelGuide(new HotelGuideParam
								{
									h = horder.hotelid,
									ln = UrlLanguage.CurrentLanguage,
									pb = new System.DateTime?(horder.period.begin.Value),
									pe = new System.DateTime?(horder.period.end.Value)
								});
								context.GuideDurties.Add(durties);
							}
						}
					}
				}
			}
			context.OrderFindNotFound = (context.Claim == null);
			return base.View(context);
		}
		[ActionName("printorder"), AllowAnonymous]
		public ActionResult PrintOrder(int? id)
		{
			PrintOrderContext context = new PrintOrderContext();
			context.Form = new PrintOrderModel();
			context.Form.Claim = (id.HasValue ? id.ToString() : "");
			ActionResult result;
			if (WebSecurity.IsAuthenticated)
			{
				int guestId = WebSecurity.CurrentUserId;
				System.Collections.Generic.List<GuestClaim> claims = GuestProvider.GetLinkedClaims(UrlLanguage.CurrentLanguage, guestId);
				int? detailedId = null;
				if (id.HasValue)
				{
					if (claims.FirstOrDefault((GuestClaim m) => m.claim == id.Value) != null)
					{
						detailedId = new int?(id.Value);
					}
				}
				if (detailedId.HasValue)
				{
					result = this.BuildVoucher(detailedId.Value);
					return result;
				}
				context.NotFound = true;
			}
			result = base.View(context);
			return result;
		}
		[ActionName("printorder"), AllowAnonymous, HttpPost]
		public ActionResult PrintOrderPost([Bind(Prefix = "Form")] PrintOrderModel model)
		{
			PrintOrderContext context = new PrintOrderContext();
			context.Form = model;
			int claimId = 0;
			ActionResult result;
			if (base.ModelState.IsValid && int.TryParse(model.Claim, out claimId))
			{
				System.Collections.Generic.List<GuestClaim> claims = GuestProvider.FindGuestClaims(UrlLanguage.CurrentLanguage, 0, model.Name, new int?(claimId), null);
				if (claims != null && claims.Count > 0)
				{
					result = this.BuildVoucher(claimId);
					return result;
				}
				context.NotFound = true;
			}
			result = base.View(context);
			return result;
		}

        [AllowAnonymous, ActionName("payorder")]
        public ActionResult PayOrder(int? id)
        {
            if (WebSecurity.IsAuthenticated)
            {
                int guestId = WebSecurity.CurrentUserId;

                if (!PartnerProvider.GetPaymentAllowed(guestId))
                    return null;

                List<GuestClaim> claims = GuestProvider.GetLinkedClaims(UrlLanguage.CurrentLanguage, guestId);
                int? detailedId = null;
                if (id.HasValue)
                {
                    if (claims.FirstOrDefault((GuestClaim m) => m.claim == id.Value) != null)
                    {
                        detailedId = new int?(id.Value);
                    }
                }
                if (detailedId.HasValue)
                {
                    Tracing.DataTrace.TraceEvent(TraceEventType.Verbose, 0, "UNITELLER payment data: {0}", new object[]
                    {
                        base.Request.DumpValues()
                    });

                    //create invoice
                    PaymentMode paymentMode = (
                            from m in BookingProvider.GetPaymentModes(UrlLanguage.CurrentLanguage, detailedId.Value)
                            where m.id == "2.EUR" //ID способа оплаты
                            select m).FirstOrDefault<PaymentMode>();

                    if (paymentMode == null)
                    {
                        throw new System.Exception(string.Format("payment mode id '{0}' not found", "2.EUR"));
                    }

                    PaymentBeforeProcessingResult beforePaymentResult = BookingProvider.BeforePaymentProcessing(UrlLanguage.CurrentLanguage, paymentMode.paymentparam);

                    //approve invoice
                    ConfirmInvoiceResult invoiceResult = BookingProvider.ConfirmInvoice(beforePaymentResult.invoiceNumber.Trim());

                    //update status
                    BookingProvider.AcceptInvoice();


                    //узнать accId
                    var selectAccId = "select max(account) as accid from accdetail where claim = " + id.Value;

                    //прочитать
                    var res = DatabaseOperationProvider.Query(selectAccId, "res", new { });

                    var accId = Convert.ToInt32(res.Tables["res"].Rows[0][0]);

                    var userName = GetUserNameAndPartner(guestId);

                    //проставить note в таблицу accdetails
                    var updateDetails = "update accdetail set note = 'hotel " + userName + "' where account = " + accId;

                    DatabaseOperationProvider.Query(updateDetails, "res", new { });

                    //поменять тип платежа в таблице accounts
                    var updatePaymentType = "update account set paymenttype = 2, note = '" + userName + "', partner =" + GetUserPartner(guestId) + " where inc = " + accId;
                    DatabaseOperationProvider.Query(updatePaymentType, "res", new { });

                    //redirect to order id  
                    return base.RedirectToAction("order", new { id = id.Value });
                }
            }
            return null;
        }

        private int GetUserPartner(int userId)
        {
            var selectPartnerName = "select partnerId from guestservice_UserProfile where userId = " + userId;

            //прочитать
            var res = DatabaseOperationProvider.Query(selectPartnerName, "res", new { });

            if (res.Tables["res"].Rows.Count > 0)
                return Convert.ToInt32(res.Tables["res"].Rows[0][0]);

            return 2;
        }

        private string GetUserNameAndPartner(int userId)
        {
            var selectPartnerName = "select a.name, b.userName  from partner as a, guestservice_UserProfile as b where a.inc = b.partnerId and b.userId = " + userId;

            //прочитать
            var res = DatabaseOperationProvider.Query(selectPartnerName, "res", new { });

            if (res.Tables["res"].Rows.Count > 0)
                return res.Tables["res"].Rows[0][0] + ": " + res.Tables["res"].Rows[0][1];

            return "Unknown";
        }

        private ActionResult BuildVoucher(int claimId)
		{
			ActionResult result;
			try
			{
				System.Collections.Generic.List<ReportParam> reportParams = new System.Collections.Generic.List<ReportParam>();
				reportParams.Add(new ReportParam
				{
					Name = "vClaimList",
					Value = claimId.ToString()
				});
				string reportName = ConfigurationManager.AppSettings["report_PrintVoucher"];
				if (string.IsNullOrEmpty(reportName))
				{
					throw new System.Exception("report_PrintVoucher is empty");
				}
				ReportResult reportData = ReportServer.BuildReport(reportName, ReportFormat.pdf, reportParams.ToArray());
				if (reportData == null)
				{
					throw new System.Exception("report data is empty");
				}
				System.IO.MemoryStream stream = new System.IO.MemoryStream(reportData.Content);
				result = new FileStreamResult(stream, "application/pdf")
				{
					FileDownloadName = string.Format("voucher_{0}.pdf", claimId)
				};
			}
			catch (System.Exception ex)
			{
				Tracing.ServiceTrace.TraceEvent(TraceEventType.Error, 0, ex.ToString());
				throw;
			}
			return result;
		}

		private void ClearErrorState(ModelState modelState)
		{
			if (modelState != null)
			{
				modelState.Errors.Clear();
			}
		}
	}
}
