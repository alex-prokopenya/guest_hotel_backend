using GuestService.Data;
using System;
using System.Linq;
namespace GuestService.Models.Booking
{
	public class BookingContext
	{
		public BookingModel Form
		{
			get;
			set;
		}
		public ReservationState Reservation
		{
			get;
			set;
		}
		public bool IsBookingEnabled
		{
			get;
			set;
		}
		public string BookingOperationId
		{
			get;
			set;
		}
		public void Prepare(BookingModel form, ReservationState reservation)
		{
			this.Reservation = reservation;
			this.Form = new BookingModel();
			if (form != null)
			{
				this.Form.PartnerAlias = form.PartnerAlias;
				this.Form.CustomerName = form.CustomerName;
				this.Form.CustomerAddress = form.CustomerAddress;
				this.Form.CustomerEmail = form.CustomerEmail;
				this.Form.CustomerMobile = form.CustomerMobile;
				this.Form.BookingNote = form.BookingNote;
			}
			if (this.Reservation != null)
			{
				if (this.Reservation.orders != null)
				{
					this.Reservation.orders.ForEach(delegate(ReservationOrder m)
					{
						BookingOrderModel order = new BookingOrderModel();
						order.ReservationOrder = m;
						if (form != null && form.Orders != null)
						{
							BookingOrderModel formOrder = form.Orders.FirstOrDefault((BookingOrderModel o) => o != null && o.BookingOrder != null && o.BookingOrder.orderid == m.id);
							if (formOrder != null)
							{
								order.BookingOrder = formOrder.BookingOrder;
							}
						}
						this.Form.Orders.Add(order);
					});
				}
			}
			bool arg_14C_1;
			if (this.Reservation != null)
			{
				arg_14C_1 = (this.Reservation.errors.FirstOrDefault((ReservationError m) => m.isstop) == null);
			}
			else
			{
				arg_14C_1 = false;
			}
			this.IsBookingEnabled = arg_14C_1;
		}
	}
}
