using GuestService.Code;
using GuestService.Controllers.Api;
using GuestService.Data;
using GuestService.Models.Booking;
using GuestService.Resources;
using Newtonsoft.Json;
using Sm.System.Mvc.Language;
using Sm.System.Trace;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using WebMatrix.WebData;
namespace GuestService.Controllers.Html
{
	[HttpPreferences, WebSecurityInitializer, UrlLanguage]
	public class BookingController : BaseController
	{
		[ActionName("index"), HttpGet, OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
		public ActionResult Index(BookingCartWebParam param)
		{
			BookingContext context = new BookingContext();
			CompleteOperation operation = CompleteOperation.CreateFromSession(base.Session);
			ActionResult result;
			if (operation.IsProgress)
			{
				context.BookingOperationId = operation.OperationId;
				result = base.View("_BookingProcessing", context);
			}
			else
			{
				if (operation.HasResult)
				{
					BookingCartResult bookingResult = JsonConvert.DeserializeObject<BookingCartResult>(operation.OperationResultData);
					if (bookingResult != null)
					{
						context.Prepare(bookingResult.Form, bookingResult.Reservation);
					}
					operation.Clear();
					result = base.View(context);
				}
				else
				{
					using (ShoppingCart cart = ShoppingCart.CreateFromSession(base.Session))
					{
						if (param != null && param.PartnerAlias != null)
						{
							cart.PartnerAlias = param.PartnerAlias;
						}
						context.Form = new BookingModel();
						context.Form.PartnerAlias = cart.PartnerAlias;
						if (WebSecurity.IsAuthenticated)
						{
							context.Form.CustomerEmail = WebSecurity.CurrentUserName;
						}
						if (cart.Orders != null)
						{
							cart.Orders.ForEach(delegate(BookingOrder o)
							{
								if (o != null && o.excursion != null && o.excursion.contact != null)
								{
									if (context.Form.CustomerName == null && !string.IsNullOrEmpty(o.excursion.contact.name))
									{
										context.Form.CustomerName = o.excursion.contact.name;
									}
									if (context.Form.CustomerMobile == null && !string.IsNullOrEmpty(o.excursion.contact.mobile))
									{
										context.Form.CustomerMobile = o.excursion.contact.mobile;
									}
								}
							});
						}
						if (cart.Orders != null)
						{
							cart.Orders.ForEach(delegate(BookingOrder m)
							{
								context.Form.Orders.Add(new BookingOrderModel
								{
									BookingOrder = m
								});
							});
						}
					}
					context.Form.Action = "calculate";
					result = this.Index(context.Form);
				}
			}
			return result;
		}
		[ActionName("index"), HttpPost, ValidateAntiForgeryToken]
		public ActionResult Index([Bind(Prefix = "Form")] BookingModel form)
		{
			if (form == null)
			{
				throw new System.ArgumentNullException("form");
			}
			if (form.Action == "remove")
			{
				if (form.RemoveOrderId != null && form.Orders != null)
				{
					form.Orders.RemoveAll((BookingOrderModel m) => m != null && m.BookingOrder != null && m.BookingOrder.orderid == form.RemoveOrderId);
					using (ShoppingCart cart = ShoppingCart.CreateFromSession(base.Session))
					{
						if (cart != null && cart.Orders != null)
						{
							cart.Orders.RemoveAll((BookingOrder m) => m.orderid == form.RemoveOrderId);
						}
					}
				}
			}
			BookingContext context = new BookingContext();
			BookingClaim bookingClaim = new BookingClaim();
			bookingClaim.orders = new System.Collections.Generic.List<BookingOrder>();
			if (form.Orders != null)
			{
				form.Orders.ForEach(delegate(BookingOrderModel m)
				{
					if (m != null && m.BookingOrder != null)
					{
						bookingClaim.orders.Add(m.BookingOrder);
					}
				});
			}
			BookingCartParam bookingCartParam = new BookingCartParam();
			bookingCartParam.ln = UrlLanguage.CurrentLanguage;
			GuestService.Controllers.Api.BookingController controller = new GuestService.Controllers.Api.BookingController();
			ActionResult result;
			if (form.Action == null)
			{
				if (!form.RulesAccepted)
				{
					base.ModelState.AddModelError("Form.RulesAccepted", BookingStrings.RulesAccepted);
				}
				if (base.ModelState.IsValid)
				{
					bookingClaim.note = form.BookingNote;
					bookingClaim.customer = new Customer
					{
						name = form.CustomerName,
						mobile = form.CustomerMobile,
						email = form.CustomerEmail,
						address = form.CustomerAddress
					};
					CompleteOperation operation = CompleteOperation.CreateFromSession(base.Session);
					operation.Start();
					context.BookingOperationId = operation.OperationId;
					int? userId = WebSecurity.IsAuthenticated ? new int?(WebSecurity.CurrentUserId) : null;
					System.Threading.ThreadPool.QueueUserWorkItem(delegate(object o)
					{
						try
						{
							BookingCartResult bookingResult = new BookingCartResult();
							bookingResult.Form = form;
							bookingResult.Reservation = controller.Book(bookingCartParam, bookingClaim);
							if (bookingResult.Reservation != null && bookingResult.Reservation.claimId.HasValue)
							{
								if (userId.HasValue)
								{
									GuestProvider.LinkGuestClaim(userId.Value, bookingResult.Reservation.claimId.Value);
								}
							}
							string data = JsonConvert.SerializeObject(bookingResult);
							CompleteOperationProvider.SetResult(operation.OperationId, "bookingresult", data);
						}
						catch (System.Exception ex)
						{
							Tracing.WebTrace.TraceEvent(TraceEventType.Error, 2, ex.ToString());
							CompleteOperationProvider.SetResult(operation.OperationId, null, null);
						}
					}, null);
					result = base.View("_BookingProcessing", context);
					return result;
				}
			}
			ReservationState reservation = controller.Calculate(bookingCartParam, bookingClaim);
			context.Prepare(form, reservation);
			result = base.View(context);
			return result;
		}
		[ActionName("bookingstatus"), HttpGet]
		public JsonResult BookingStatus(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new System.ArgumentNullException("id");
			}
			int? reservation = null;
			string[] errors = null;
			CompleteOperation operation = CompleteOperation.CreateFromSession(base.Session);
			bool isFinished = operation.IsFinished();
			if (isFinished)
			{
				if (operation.OperationResultType == "bookingresult" && operation.OperationResultData != null)
				{
					BookingCartResult bookingResult = JsonConvert.DeserializeObject<BookingCartResult>(operation.OperationResultData);
					if (bookingResult != null && bookingResult.Reservation != null)
					{
						reservation = bookingResult.Reservation.claimId;
						if (reservation.HasValue)
						{
							base.TempData[string.Format("order.{0}.name", reservation.Value)] = ((bookingResult.Form != null) ? bookingResult.Form.CustomerName : "");
							operation.Clear();
							using (ShoppingCart cart = ShoppingCart.CreateFromSession(base.Session))
							{
								cart.Clear();
							}
						}
						else
						{
							if (bookingResult.Reservation.errors != null)
							{
								errors = (
									from m in bookingResult.Reservation.errors
									select (!string.IsNullOrEmpty(m.usermessage)) ? m.usermessage : m.message).ToArray<string>();
							}
						}
					}
				}
			}
			return base.Json(new
			{
				isfinished = isFinished,
				reservation = reservation,
				errors = errors
			}, JsonRequestBehavior.AllowGet);
		}
		[ActionName("agreement"), HttpGet]
		public ActionResult Agreement()
		{
			BookingAgreementContext context = new BookingAgreementContext();
			AgreementParam agreementParam = new AgreementParam();
			agreementParam.ln = UrlLanguage.CurrentLanguage;
			GuestService.Controllers.Api.BookingController controller = new GuestService.Controllers.Api.BookingController();
			BookingAgreement result = controller.Agreement(agreementParam);
			context.Text = ((result != null) ? result.text : null);
			return this.PartialView("_Agreement", context);
		}
	}
}
