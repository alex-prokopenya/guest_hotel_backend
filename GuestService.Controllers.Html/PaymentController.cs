using GuestService.Code;
using GuestService.Data;
using GuestService.Models.Payment;
using GuestService.Resources;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;
using Sm.Report;
using Sm.System.Mvc.Language;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using GuestService.Models.Guest;
using GuestService.Models;
using GuestService.Notifications;

namespace GuestService.Controllers.Html
{
	[HttpPreferences, WebSecurityInitializer, UrlLanguage]
	public class PaymentController : BaseController
	{

        [HttpPost, ActionName("cancelorder")]
        public JsonResult SendCancellationOrder(CancellationOrderWebParam param)
        {
            try
            {
                ReservationState claim = BookingProvider.GetReservationState(UrlLanguage.CurrentLanguage, param.claimId);

                var customer = claim.customer;

                try
                {
                    new SimpleEmailService().SendEmail<CancellationMessageTemplate>(Settings.EmailForCancellation,
                                                                                    "send_cancellation_order",
                                                                                    "en",
                                                                                    new CancellationMessageTemplate()
                                                                                    {
                                                                                        NumberOrder = param.claimId.ToString(),
                                                                                        ReasonCancellation = param.reason,
                                                                                        Name = customer.name,
                                                                                        Phone = customer.phone,
                                                                                        Email = customer.mail
                                                                                    });
                }
                catch (Exception ex)
                {

                }

                BookingProvider.ChangeClaimStatus(claim.claimId.Value, Settings.AnnulateRequestStatusId, claim.status.id);

                return base.Json(new { ok = true });
            }
            catch (Exception)
            {

            }

            return base.Json(new { ok = false });
        }


        [ActionName("index"), HttpGet]
		public ActionResult Index(int? claim)
		{
			PaymentContext paymentContext = new PaymentContext();
			bool hasValue = claim.HasValue;
			if (hasValue)
			{
				paymentContext.Reservation = BookingProvider.GetReservationState(UrlLanguage.CurrentLanguage, claim.Value);
				paymentContext.PaymentModes = BookingProvider.GetPaymentModes(UrlLanguage.CurrentLanguage, claim.Value);
			}
			return base.View(paymentContext);
		}
		[ActionName("index"), HttpPost, ValidateAntiForgeryToken]
		public ActionResult Index(PaymentModel model)
		{
			bool flag = model == null;
			if (flag)
			{
				throw new ArgumentNullException("model");
			}
			bool flag2 = model.claimId == 0;
			if (flag2)
			{
				throw new ArgumentException("claimid");
			}
			return base.View(new PaymentContext
			{
				Reservation = BookingProvider.GetReservationState(UrlLanguage.CurrentLanguage, model.claimId),
				PaymentModes = BookingProvider.GetPaymentModes(UrlLanguage.CurrentLanguage, model.claimId)
			});
		}
		[ActionName("processing"), HttpPost, ValidateAntiForgeryToken]
		public ActionResult Processing(ProcessingModel model)
		{
			bool flag = model == null;
			if (flag)
			{
				throw new ArgumentNullException("model");
			}
			bool flag2 = model.claimId == 0;
			if (flag2)
			{
				throw new ArgumentException("claimid");
			}
			PaymentMode paymentMode = (
				from m in BookingProvider.GetPaymentModes(UrlLanguage.CurrentLanguage, model.claimId)
				where m.id == model.paymentId
				select m).FirstOrDefault<PaymentMode>();
			bool flag3 = paymentMode == null;
			if (flag3)
			{
				throw new Exception(string.Format("payment mode id '{0}' not found", model.paymentId));
			}
			string text = (paymentMode.processing ?? "").ToLowerInvariant();
			bool flag4 = text != null;
			if (flag4)
			{
				bool flag5 = !(text == "paypal");
				ActionResult result;
				if (flag5)
				{
					bool flag6 = !(text == "uniteller");
					if (flag6)
					{
						bool flag7 = !(text == "bank");
						if (flag7)
						{
							goto IL_149;
						}
						result = this.Processing_Bank(model.claimId, paymentMode);
					}
					else
					{
						result = this.Processing_Uniteller(model.claimId, paymentMode);
					}
				}
				else
				{
					result = this.Processing_PayPal(model.claimId, paymentMode);
				}
				return result;
			}
			IL_149:
			throw new Exception(string.Format("unsupported processing system '{0}'", paymentMode.processing));
		}
		[ActionName("processingresult"), HttpGet]
		public ActionResult ProcessingResult(string id, ProcessingResultModel model)
		{
			string text = (id ?? "").ToLowerInvariant();
			bool flag = text != null;
			if (flag)
			{
				bool flag2 = !(text == "paypal");
				ActionResult result;
				if (flag2)
				{
					bool flag3 = !(text == "uniteller");
					if (flag3)
					{
						goto IL_5E;
					}
					result = this.ProcessingResult_Uniteller(model);
				}
				else
				{
					result = this.ProcessingResult_PayPal(model);
				}
				return result;
			}
			IL_5E:
			throw new Exception(string.Format("unsupported processing system '{0}'", id));
		}
		private ActionResult Processing_PayPal(int claim, PaymentMode payment)
		{
			bool flag = payment == null;
			if (flag)
			{
				throw new ArgumentNullException("payment");
			}
			PaymentBeforeProcessingResult paymentBeforeProcessingResult = BookingProvider.BeforePaymentProcessing(UrlLanguage.CurrentLanguage, payment.paymentparam);
			bool flag2 = paymentBeforeProcessingResult == null;
			if (flag2)
			{
				throw new Exception("cannot get payment details");
			}
			bool flag3 = !paymentBeforeProcessingResult.success;
			if (flag3)
			{
				throw new Exception("payment details fail");
			}
			List<PaymentDetailsType> list = new List<PaymentDetailsType>();
			PaymentDetailsType paymentDetailsType = new PaymentDetailsType();
			paymentDetailsType.AllowedPaymentMethod = new AllowedPaymentMethodType?(AllowedPaymentMethodType.ANYFUNDINGSOURCE);
			CurrencyCodeType value = (CurrencyCodeType)EnumUtils.GetValue(payment.payrest.currency, typeof(CurrencyCodeType));
			PaymentDetailsItemType paymentDetailsItemType = new PaymentDetailsItemType();
			paymentDetailsItemType.Name = string.Format(PaymentStrings.Get("PaymentForOrderFormat"), claim);
			paymentDetailsItemType.Amount = new BasicAmountType(new CurrencyCodeType?(value), payment.payrest.total.ToString("#.00", NumberFormatInfo.InvariantInfo));
			paymentDetailsItemType.Quantity = new int?(1);
			paymentDetailsItemType.ItemCategory = new ItemCategoryType?(ItemCategoryType.PHYSICAL);
			paymentDetailsItemType.Description = string.Format("Booking #{0}", claim);
			paymentDetailsType.PaymentDetailsItem = new List<PaymentDetailsItemType>
			{
				paymentDetailsItemType
			};
			paymentDetailsType.PaymentAction = new PaymentActionCodeType?(PaymentActionCodeType.SALE);
			paymentDetailsType.OrderTotal = new BasicAmountType(paymentDetailsItemType.Amount.currencyID, paymentDetailsItemType.Amount.value);
			list.Add(paymentDetailsType);
			SetExpressCheckoutRequestDetailsType setExpressCheckoutRequestDetailsType = new SetExpressCheckoutRequestDetailsType();
			setExpressCheckoutRequestDetailsType.ReturnURL = new Uri(base.Request.BaseServerAddress(), base.Url.Action("processingresult", new
			{
				id = "paypal",
				success = true
			})).ToString();
			setExpressCheckoutRequestDetailsType.CancelURL = new Uri(base.Request.BaseServerAddress(), base.Url.Action("processingresult", new
			{
				id = "paypal",
				success = false
			})).ToString();
			setExpressCheckoutRequestDetailsType.NoShipping = "1";
			setExpressCheckoutRequestDetailsType.AllowNote = "0";
			setExpressCheckoutRequestDetailsType.SolutionType = new SolutionTypeType?(SolutionTypeType.SOLE);
			setExpressCheckoutRequestDetailsType.SurveyEnable = "0";
			setExpressCheckoutRequestDetailsType.PaymentDetails = list;
			setExpressCheckoutRequestDetailsType.InvoiceID = paymentBeforeProcessingResult.invoiceNumber;
			SetExpressCheckoutRequestType setExpressCheckoutRequestType = new SetExpressCheckoutRequestType();
			setExpressCheckoutRequestType.Version = "104.0";
			setExpressCheckoutRequestType.SetExpressCheckoutRequestDetails = setExpressCheckoutRequestDetailsType;
			SetExpressCheckoutReq setExpressCheckoutReq = new SetExpressCheckoutReq();
			setExpressCheckoutReq.SetExpressCheckoutRequest = setExpressCheckoutRequestType;
			Dictionary<string, string> dictionary = PaymentController.PayPal_CreateConfig();
			PayPalAPIInterfaceServiceService payPalAPIInterfaceServiceService = new PayPalAPIInterfaceServiceService(dictionary);
			SetExpressCheckoutResponseType setExpressCheckoutResponseType = payPalAPIInterfaceServiceService.SetExpressCheckout(setExpressCheckoutReq);
			IEnumerable<KeyValuePair<string, string>> arg_2A1_0 = dictionary;

            System.Collections.Generic.Dictionary<string, string> config = PaymentController.PayPal_CreateConfig();
            PayPalAPIInterfaceServiceService service = new PayPalAPIInterfaceServiceService(config);
        //   SetExpressCheckoutResponseType setECResponse = service.SetExpressCheckout(wrapper);

            System.Collections.Generic.KeyValuePair<string, string> sandboxConfig = config.FirstOrDefault((System.Collections.Generic.KeyValuePair<string, string> m) => m.Key == "mode");

            string sandboxServer = (sandboxConfig.Key != null && sandboxConfig.Value == "sandbox") ? ".sandbox" : "";

            //создать контекст
            //передать во вьюху ссылку для перехода
            //  return new RedirectResult(string.Format("https://www{0}.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token={1}", sandboxServer, base.Server.UrlEncode(setECResponse.Token)));

            return base.View("PaymentSystems\\PayPal", new ProcessingContext
            {
                Reservation = BookingProvider.GetReservationState(UrlLanguage.CurrentLanguage, claim),
                PaymentMode = payment,
                BeforePaymentResult = paymentBeforeProcessingResult,
                RedirectUrl = string.Format("https://www{0}.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token={1}", sandboxServer, base.Server.UrlEncode(setExpressCheckoutResponseType.Token))
            });
        }
		private ActionResult ProcessingResult_PayPal(ProcessingResultModel model)
		{
			bool flag = model == null;
			if (flag)
			{
				throw new ArgumentNullException("model");
			}
			PaymentResultContext context = new PaymentResultContext();
			bool flag2 = model.success == true;
			if (flag2)
			{
				bool flag3 = model.token == null;
				if (flag3)
				{
					throw new ArgumentNullException("token");
				}
				bool flag4 = model.payerID == null;
				if (flag4)
				{
					throw new ArgumentNullException("payerID");
				}
				GetExpressCheckoutDetailsRequestType getExpressCheckoutDetailsRequestType = new GetExpressCheckoutDetailsRequestType();
				getExpressCheckoutDetailsRequestType.Version = "104.0";
				getExpressCheckoutDetailsRequestType.Token = model.token;
				GetExpressCheckoutDetailsReq getExpressCheckoutDetailsReq = new GetExpressCheckoutDetailsReq();
				getExpressCheckoutDetailsReq.GetExpressCheckoutDetailsRequest = getExpressCheckoutDetailsRequestType;
				Dictionary<string, string> config = PaymentController.PayPal_CreateConfig();
				PayPalAPIInterfaceServiceService payPalAPIInterfaceServiceService = new PayPalAPIInterfaceServiceService(config);
				GetExpressCheckoutDetailsResponseType expressCheckoutDetails = payPalAPIInterfaceServiceService.GetExpressCheckoutDetails(getExpressCheckoutDetailsReq);
				bool flag5 = expressCheckoutDetails == null;
				if (flag5)
				{
					throw new Exception("checkout details result is null");
				}
				bool flag6 = expressCheckoutDetails.Errors != null && expressCheckoutDetails.Errors.Count > 0;
				if (flag6)
				{
					expressCheckoutDetails.Errors.ForEach(delegate(ErrorType m)
					{
						context.Errors.Add(m.LongMessage);
					});
				}
				bool flag7 = expressCheckoutDetails.Ack == AckCodeType.SUCCESS || expressCheckoutDetails.Ack == AckCodeType.SUCCESSWITHWARNING;
				if (flag7)
				{
					GetExpressCheckoutDetailsResponseDetailsType getExpressCheckoutDetailsResponseDetails = expressCheckoutDetails.GetExpressCheckoutDetailsResponseDetails;
					bool flag8 = getExpressCheckoutDetailsResponseDetails == null;
					if (flag8)
					{
						throw new Exception("details object is null");
					}
					bool flag9 = string.IsNullOrEmpty(getExpressCheckoutDetailsResponseDetails.InvoiceID);
					if (flag9)
					{
						throw new Exception("invoiceID not found");
					}
					bool flag10 = getExpressCheckoutDetailsResponseDetails.PaymentDetails == null;
					if (flag10)
					{
						throw new Exception("payment details is null");
					}
					List<PaymentDetailsType> list = new List<PaymentDetailsType>();
					foreach (PaymentDetailsType current in getExpressCheckoutDetailsResponseDetails.PaymentDetails)
					{
						list.Add(new PaymentDetailsType
						{
							NotifyURL = null,
							PaymentAction = current.PaymentAction,
							OrderTotal = current.OrderTotal
						});
					}
					DoExpressCheckoutPaymentRequestType doExpressCheckoutPaymentRequestType = new DoExpressCheckoutPaymentRequestType();
					doExpressCheckoutPaymentRequestType.Version = "104.0";
					doExpressCheckoutPaymentRequestType.DoExpressCheckoutPaymentRequestDetails = new DoExpressCheckoutPaymentRequestDetailsType
					{
						PaymentDetails = list,
						Token = model.token,
						PayerID = model.payerID
					};
					DoExpressCheckoutPaymentResponseType doExpressCheckoutPaymentResponseType = payPalAPIInterfaceServiceService.DoExpressCheckoutPayment(new DoExpressCheckoutPaymentReq
					{
						DoExpressCheckoutPaymentRequest = doExpressCheckoutPaymentRequestType
					});
					bool flag11 = doExpressCheckoutPaymentResponseType == null;
					if (flag11)
					{
						throw new Exception("payment result is null");
					}
					bool flag12 = doExpressCheckoutPaymentResponseType.Errors != null && doExpressCheckoutPaymentResponseType.Errors.Count > 0;
					if (flag12)
					{
						doExpressCheckoutPaymentResponseType.Errors.ForEach(delegate(ErrorType m)
						{
							context.Errors.Add(m.LongMessage);
						});
					}
					bool flag13 = doExpressCheckoutPaymentResponseType.Ack == AckCodeType.SUCCESS || doExpressCheckoutPaymentResponseType.Ack == AckCodeType.SUCCESSWITHWARNING;
					if (flag13)
					{
						ConfirmInvoiceResult confirmInvoiceResult = BookingProvider.ConfirmInvoice(getExpressCheckoutDetailsResponseDetails.InvoiceID.Trim());
						bool flag14 = !confirmInvoiceResult.IsSuccess;
						if (flag14)
						{
							context.Errors.Add(string.Format("invoice confirmation error: {0}", confirmInvoiceResult.ErrorMessage));
						}
						else
						{
							context.Success = true;
						}
					}
				}
			}
			else
			{
				context.Errors.Add(PaymentStrings.PaymentCancelled);
			}
			return base.View("_ProcessingResult", context);
		}
		private static Dictionary<string, string> PayPal_CreateConfig()
		{
			PaymentPaypalSettings paymentPaypal = Settings.PaymentPaypal;
			bool flag = paymentPaypal == null;
			if (flag)
			{
				throw new Exception("PayPal settings are not configured");
			}
			return new Dictionary<string, string>
			{
				
				{
					"mode",
					paymentPaypal.IsSandbox ? "sandbox" : "live"
				},
				
				{
					"account1.apiUsername",
					paymentPaypal.Username
				},
				
				{
					"account1.apiPassword",
					paymentPaypal.Password
				},
				
				{
					"account1.apiSignature",
					paymentPaypal.Signature
				}
			};
		}
		private ActionResult Processing_Uniteller(int claim, PaymentMode payment)
		{
			bool flag = payment == null;
			if (flag)
			{
				throw new ArgumentNullException("payment");
			}
			PaymentBeforeProcessingResult paymentBeforeProcessingResult = BookingProvider.BeforePaymentProcessing(UrlLanguage.CurrentLanguage, payment.paymentparam);
			bool flag2 = paymentBeforeProcessingResult == null;
			if (flag2)
			{
				throw new Exception("cannot get payment details");
			}
			bool flag3 = !paymentBeforeProcessingResult.success;
			if (flag3)
			{
				throw new Exception("payment details fail");
			}
			return base.View("PaymentSystems\\Uniteller", new ProcessingContext
			{
				Reservation = BookingProvider.GetReservationState(UrlLanguage.CurrentLanguage, claim),
				PaymentMode = payment,
				BeforePaymentResult = paymentBeforeProcessingResult
			});
		}
		private ActionResult ProcessingResult_Uniteller(ProcessingResultModel model)
		{
			bool flag = model == null;
			if (flag)
			{
				throw new ArgumentNullException("model");
			}
			PaymentResultContext paymentResultContext = new PaymentResultContext();
			bool flag2 = model.success == true;
			if (flag2)
			{
				paymentResultContext.Success = true;
			}
			else
			{
				paymentResultContext.Errors.Add(PaymentStrings.PaymentCancelled);
			}
			return base.View("_ProcessingResult", paymentResultContext);
		}
		private ActionResult Processing_Bank(int claim, PaymentMode payment)
		{
			bool flag = payment == null;
			if (flag)
			{
				throw new ArgumentNullException("payment");
			}
			PaymentBeforeProcessingResult paymentBeforeProcessingResult = BookingProvider.BeforePaymentProcessing(UrlLanguage.CurrentLanguage, payment.paymentparam);
			bool flag2 = paymentBeforeProcessingResult == null;
			if (flag2)
			{
				throw new Exception("cannot get payment details");
			}
			bool flag3 = !paymentBeforeProcessingResult.success;
			if (flag3)
			{
				throw new Exception("payment details fail");
			}
			ActionResult result;
			try
			{
				List<ReportParam> list = new List<ReportParam>();
				list.Add(new ReportParam
				{
					Name = "vClaimList",
					Value = claim.ToString()
				});
				string text = ConfigurationManager.AppSettings["report_PrintInvoice"];
				bool flag4 = string.IsNullOrEmpty(text);
				if (flag4)
				{
					throw new Exception("report_PrintInvoice is empty");
				}
				ReportResult reportResult = ReportServer.BuildReport(text, ReportFormat.pdf, list.ToArray());
				bool flag5 = reportResult == null;
				if (flag5)
				{
					throw new Exception("report data is empty");
				}
				MemoryStream fileStream = new MemoryStream(reportResult.Content);
				result = new FileStreamResult(fileStream, "application/pdf")
				{
					FileDownloadName = string.Format("invoice_{0}.pdf", claim)
				};
			}
			catch (Exception var_13_117)
			{
				throw;
			}
			return result;
		}
	}
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  