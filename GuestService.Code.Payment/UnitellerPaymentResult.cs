using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web;
namespace GuestService.Code.Payment
{
	public class UnitellerPaymentResult
	{
		public enum OpeationStatus
		{
			Failed,
			Success
		}
		public string InvoiceNumber
		{
			get;
			private set;
		}
		public UnitellerPaymentResult.OpeationStatus Status
		{
			get;
			private set;
		}
		public static UnitellerPaymentResult Create(HttpRequestBase request)
		{
			if (request == null)
			{
				throw new System.ArgumentNullException("request");
			}
			string invoiceNumber = request["Order_ID"];
			if (string.IsNullOrEmpty(invoiceNumber))
			{
				throw new System.Exception("Order_ID is empty");
			}
			string status = request["Status"];
			if (string.IsNullOrEmpty(status))
			{
				throw new System.Exception("Status is empty");
			}
			string signStr = request["Signature"];
			if (string.IsNullOrEmpty(signStr))
			{
				throw new System.Exception("Signature is empty");
			}
			System.Text.StringBuilder signature = new System.Text.StringBuilder();
			using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
			{
				string signatureString = string.Format("{0}{1}{2}", invoiceNumber, status, ConfigurationManager.AppSettings["unitellerPassword"]);
				byte[] array = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(signatureString));
				for (int i = 0; i < array.Length; i++)
				{
					byte b = array[i];
					signature.AppendFormat("{0:X2}", b);
				}
			}
			if (signature.ToString() != signStr)
			{
				throw new System.Exception(string.Format("invalid signature", new object[0]));
			}
			return new UnitellerPaymentResult
			{
				InvoiceNumber = invoiceNumber,
				Status = (status.ToLower() == "authorized" || status.ToLower() == "paid") ? UnitellerPaymentResult.OpeationStatus.Success : UnitellerPaymentResult.OpeationStatus.Failed
			};
		}
	}
}
