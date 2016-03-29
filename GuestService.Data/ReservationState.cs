using System;
using System.Collections.Generic;
using System.Linq;
namespace GuestService.Data
{
    public class ReservationState
    {
        public bool agentPaymentAllowed
        {
            get;
            set;
        }

        public int? claimId
        {
            get;
            set;
        }

        public DateTime timelimit
        {
            get;
            set;
        }

        public ReservationStatus status
        {
            get;
            set;
        }

        public ReservationCustomer customer
        {
            get;
            set;
        }
        public string agent
        {
            get;
            set;
        }
        public ReservationPartner partner
        {
            get;
            set;
        }
        public ReservationConfirmStatus confirmation
        {
            get;
            set;
        }
        public ReservationPrice price
        {
            get;
            set;
        }
        public ReservationAction action
        {
            get;
            set;
        }
        public System.Collections.Generic.List<ReservationOrder> orders
        {
            get;
            set;
        }
        public System.Collections.Generic.List<ReservationPeople> people
        {
            get;
            set;
        }
        public System.Collections.Generic.List<ReservationError> errors
        {
            get;
            set;
        }
        public System.Collections.Generic.List<ReservationError> GetOrderErrors(ReservationOrder order)
        {
            if (order == null)
            {
                throw new System.ArgumentNullException("order");
            }
            System.Collections.Generic.List<ReservationError> result = new System.Collections.Generic.List<ReservationError>();
            if (this.errors != null && !string.IsNullOrEmpty(order.id))
            {
                result.AddRange(
                    from m in this.errors
                    where m.orderid == order.id
                    select m);
            }
            return result;
        }
        public System.Collections.Generic.List<ReservationError> GetReservationErrors()
        {
            System.Collections.Generic.List<ReservationError> result = new System.Collections.Generic.List<ReservationError>();
            if (this.errors != null)
            {
                result.AddRange(
                    from m in this.errors
                    where string.IsNullOrEmpty(m.orderid)
                    select m);
            }
            return result;
        }
        public System.Collections.Generic.List<ReservationPeople> GetOrderPeople(ReservationOrder order)
        {
            if (order == null)
            {
                throw new System.ArgumentNullException("order");
            }
            System.Collections.Generic.List<ReservationPeople> result = new System.Collections.Generic.List<ReservationPeople>();
            if (this.people != null && order.peopleids != null)
            {
                result.AddRange(
                    from m in this.people
                    where order.peopleids.Contains(m.id)
                    select m);
            }
            return result;
        }
    }
}
