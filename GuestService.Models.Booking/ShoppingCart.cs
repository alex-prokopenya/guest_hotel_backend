using GuestService.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web;
namespace GuestService.Models.Booking
{
	public class ShoppingCart : System.IDisposable
	{
		private const string SessionStateName = "shoppingCart";
		private HttpSessionStateBase _session;
		public string PartnerAlias
		{
			get;
			set;
		}
		public System.Collections.Generic.List<BookingOrder> Orders
		{
			get;
			private set;
		}
		public bool IsOrders
		{
			get
			{
				return this.Orders != null && this.Orders.Count > 0;
			}
		}
		public static ShoppingCart CreateFromSession(HttpSessionStateBase session)
		{
			if (session == null)
			{
				throw new System.ArgumentNullException("session");
			}
			ShoppingCart result = null;
			string data = session["shoppingCart"] as string;
			if (!string.IsNullOrEmpty(data))
			{
				result = JsonConvert.DeserializeObject<ShoppingCart>(data);
			}
			if (result == null)
			{
				result = new ShoppingCart();
			}
			result._session = session;
			return result;
		}
		public void Dispose()
		{
			this.Save();
		}
		public void Save()
		{
			if (this._session == null)
			{
				throw new System.Exception("_session is null");
			}
			this._session["shoppingCart"] = JsonConvert.SerializeObject(this);
		}
		public ShoppingCart()
		{
			this.Orders = new System.Collections.Generic.List<BookingOrder>();
		}
		public void Clear()
		{
			this.PartnerAlias = null;
			this.Orders.Clear();
		}
	}
}
