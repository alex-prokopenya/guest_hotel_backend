using GuestService.Code;
using GuestService.Code.Payment;
using GuestService.Data;
using Sm.System.Trace;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Web.Mvc;
namespace GuestService.Controllers.Html
{
	[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
	public class PaymentProcessingController : Controller
	{
		[HttpPost, ValidateInput(false)]
		public ActionResult uniteller()
		{
			string action = base.RouteData.GetRequiredString("action");
			ActionResult result;
			try
			{
				Tracing.DataTrace.TraceEvent(TraceEventType.Verbose, 0, "UNITELLER payment data: {0}", new object[]
				{
					base.Request.DumpValues()
				});
				this.CheckServerAddressList();
				UnitellerPaymentResult paymentResult = UnitellerPaymentResult.Create(base.Request);
				if (paymentResult.Status == UnitellerPaymentResult.OpeationStatus.Success)
				{
					ConfirmInvoiceResult invoiceResult = BookingProvider.ConfirmInvoice(paymentResult.InvoiceNumber.Trim());
					Tracing.DataTrace.TraceEvent(TraceEventType.Information, 0, "UNITELLER transaction: invoice: '{0}', status: '{1}', invoice confirmation: '{2}'", new object[]
					{
						paymentResult.InvoiceNumber,
						paymentResult.Status,
						invoiceResult.IsSuccess ? "SUCCESS" : "FAILED"
					});
					if (!invoiceResult.IsSuccess)
					{
						throw new System.Exception(string.Format("invoice confirm error {0}", invoiceResult.ErrorMessage));
					}
				}
				else
				{
					Tracing.DataTrace.TraceEvent(TraceEventType.Information, 0, "UNITELLER transaction: invoice: '{1}', status: '{2}'", new object[]
					{
						paymentResult.InvoiceNumber,
						paymentResult.Status
					});
				}
				result = new EmptyResult();
			}
			catch (System.Exception ex)
			{
				Tracing.DataTrace.TraceEvent(TraceEventType.Error, 0, "UNITELLER payment error: {0}", new object[]
				{
					ex.ToString()
				});
				result = new HttpStatusCodeResult(500);
			}
			return result;
		}
		private void CheckServerAddressList()
		{
			string serverAddressListStr = ConfigurationManager.AppSettings["paymentSystemIP"];
			if (!string.IsNullOrEmpty(serverAddressListStr))
			{
				string[] serverAddressList = serverAddressListStr.Split(";,".ToCharArray());
				string remoteAddress = base.HttpContext.Request.ServerVariables["REMOTE_ADDR"];
				if (string.IsNullOrEmpty(remoteAddress))
				{
					throw new System.Exception("'REMOTE_ADDR' variable not found");
				}
				string[] array = serverAddressList;
				for (int i = 0; i < array.Length; i++)
				{
					string address = array[i];
					if (address == remoteAddress)
					{
						return;
					}
				}
				throw new System.Exception("invalid server address");
			}
		}
	}
}
