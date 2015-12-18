using Sm.System.Database;
using Sm.System.Profiling;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Linq;
using GuestService.Notifications;
using Sm.System.Mvc.Language;
using System.Threading.Tasks;
namespace GuestService.Data
{
	public static class BookingProvider
	{
		private class BookingFactory
		{
            public ReservationCustomer ReservationCustomer(DataRow row)
            {
                return new ReservationCustomer()
                {
                    address = row.ReadNullableTrimmedString("address"),
                    id = row.ReadNullableInt("inc"),
                    name = row.ReadNullableTrimmedString("name"),
                    phone = row.ReadNullableTrimmedString("phones"),
                    mail = row.ReadNullableTrimmedString("email"),

                    language = row.ReadNullableTrimmedString("language"),
                };
            }

            public ExcursionOrderCalculatePrice ExcursionOrderCalculatePrice(DataRow row)
			{
				return new ExcursionOrderCalculatePrice
				{
					id = row.ReadInt("excursion$inc"),
					name = row.ReadNullableTrimmedString("excursion$name"),
					date = row.ReadUnspecifiedDateTime("date"),
					time = row.IsNull("time$inc") ? null : new ExcursionTime
					{
						id = row.ReadInt("time$inc"),
						name = row.ReadNullableTrimmedString("time$name")
					},
					language = row.IsNull("lang$inc") ? null : new Language
					{
						id = row.ReadInt("lang$inc"),
						name = row.ReadNullableTrimmedString("lang$name"),
						alias = row.ReadNullableTrimmedString("lang$alias")
					},
					group = row.IsNull("group$inc") ? null : new ExcursionGroup
					{
						id = row.ReadInt("group$inc"),
						name = row.ReadNullableTrimmedString("group$name")
					},
					departure = row.IsNull("departure$inc") ? null : new GeoArea
					{
						id = row.ReadInt("departure$inc"),
						name = row.ReadNullableTrimmedString("departure$name"),
						alias = row.ReadNullableTrimmedString("departure$alias")
					},
					pax = new BookingPax
					{
						adult = row.ReadInt("adult", 0),
						child = row.ReadInt("child", 0),
						infant = row.ReadInt("infant", 0)
					},
					contact = new ExcursionContact
					{
						name = row.ReadNullableTrimmedString("contact$name")
					},
					note = row.ReadNullableTrimmedString("note"),
					price = row.IsNull("price") ? null : new OrderPrice
					{
						price = row.ReadDecimal("price"),
						currency = row.ReadNullableTrimmedString("currency$alias")
					},
					closesaletime = row.ReadNullableUnspecifiedDateTime("closesaletime"),
					issaleclosed = row.ReadBoolean("isclosed"),
					isstopsale = row.ReadBoolean("isstopsale")
				};
			}
			public ReservationState ReservationState(DataRow row)
			{
				return new ReservationState
				{
					claimId = row.ReadNullableInt("claim_id"),
					status = row.IsNull("status_id") ? null : new ReservationStatus
					{
						id = row.ReadInt("status_id"),
						description = row.ReadNullableTrimmedString("status_name")
					},
					partner = row.IsNull("partner_id") ? null : new ReservationPartner
					{
						id = row.ReadInt("partner_id"),
						name = row.ReadNullableTrimmedString("partner_name")
					},
					confirmation = row.IsNull("confirm_id") ? null : new ReservationConfirmStatus
					{
						id = row.ReadInt("confirm_id"),
						description = row.ReadNullableTrimmedString("confirm_name")
					},
					price = (row.IsNull("payprice") || row.IsNull("payrest") || row.IsNull("paycurrency")) ? null : new ReservationPrice
					{
						total = row.ReadDecimal("payprice"),
						topay = row.ReadDecimal("payrest"),
						currency = row.ReadNullableTrimmedString("paycurrency")
					},
					action = new ReservationAction
					{
						canshowprice = row.ReadBoolean("showprice", false),
						canprintvoucher = row.ReadBoolean("printvoucher", false),
						canpay = row.ReadBoolean("canpay", false)
					}
				};
			}
			public string ReservationOrderSorting(DataRow row)
			{
				System.DateTime datefrom = row.ReadUnspecifiedDateTime("datefrom");
				datefrom.ToString();
				string text = row.ReadString("type").ToLower();
				string result;
				if (text != null)
				{
					if (text == "hotel")
					{
						result = string.Format("A.{0:s}", datefrom);
						return result;
					}
					if (text == "transport")
					{
						result = string.Format("B.{0:s}", datefrom);
						return result;
					}
					if (text == "transfer")
					{
						result = string.Format("C.{0:s}", datefrom);
						return result;
					}
					if (text == "service")
					{
						result = string.Format("D.{0:s}", datefrom);
						return result;
					}
					if (text == "excursion")
					{
						result = string.Format("E.{0:s}", datefrom);
						return result;
					}
				}
				result = "Z";
				return result;
			}
			public ReservationOrder ReservationOrder(DataRow row)
			{
				ReservationOrder result = new ReservationOrder
				{
					id = (!row.IsNull("orderid")) ? row.ReadNullableTrimmedString("orderid") : row.ReadNullableInt("localid").ToString(),
					localid = row.ReadNullableInt("localid"),
					status = new ReservationStatus
					{
						id = row.ReadInt("actstatusid"),
						description = row.ReadNullableTrimmedString("actstatusname")
					},
					datefrom = row.ReadUnspecifiedDateTime("datefrom"),
					datetill = row.ReadUnspecifiedDateTime("datetill"),
					partner = row.IsNull("partnerid") ? null : new ReservationPartner
					{
						id = row.ReadInt("partnerid"),
						name = row.ReadNullableTrimmedString("partnername")
					},
					pax = new ReservationPax
					{
						adult = row.ReadInt("adult"),
						child = row.ReadInt("child"),
						infant = row.ReadInt("infant"),
						extra = row.ReadInt("addinfant")
					},
					note = row.ReadNullableTrimmedString("note"),
					price = (row.IsNull("saleprice") || row.IsNull("salecurrency")) ? null : new ReservationOrderPrice
					{
						total = row.ReadDecimal("saleprice"),
						currency = row.ReadNullableTrimmedString("salecurrency")
					}
				};
				XElement peopleXml = row.ReadXml("peoples");
				if (peopleXml != null)
				{
					result.peopleids = new System.Collections.Generic.List<string>();
					foreach (XElement peopleNode in peopleXml.DescendantsAndSelf("People"))
					{
						XElement idNode = peopleNode.Element("Id");
						XElement localidNode = peopleNode.Element("LocalId");
						result.peopleids.Add((idNode != null) ? idNode.Value : ((localidNode != null) ? localidNode.Value : ""));
					}
				}
				string orderType = row.ReadString("type").ToLower();
				if (orderType == "hotel")
				{
					result.hotel = new HotelReservationOrder
					{
						id = row.ReadInt("hotelid"),
						name = row.ReadNullableTrimmedString("hotelname"),
						room = row.IsNull("roomid") ? null : new HotelReservationRoom
						{
							id = row.ReadInt("roomid"),
							name = row.ReadNullableTrimmedString("roomname")
						},
						htplace = row.IsNull("htplaceid") ? null : new HotelReservationHtplace
						{
							id = row.ReadInt("htplaceid"),
							name = row.ReadNullableTrimmedString("htplacename")
						},
						meal = row.IsNull("mealid") ? null : new HotelReservationMeal
						{
							id = row.ReadInt("mealid"),
							name = row.ReadNullableTrimmedString("mealname")
						}
					};
				}
				else
				{
					if (orderType == "excursion")
					{
						result.excursion = new ExcursionReservationOrder
						{
							id = row.ReadInt("excursid"),
							name = row.ReadNullableTrimmedString("excursname"),
							time = row.IsNull("extimeid") ? null : new ExcursionReservationTime
							{
								id = row.ReadInt("extimeid"),
								description = row.ReadNullableTrimmedString("extimename")
							},
							grouptype = row.IsNull("exgrouptypeid") ? null : new ExcursionReservationGroup
							{
								id = row.ReadInt("exgrouptypeid"),
								description = row.ReadNullableTrimmedString("exgrouptypename")
							},
							language = row.IsNull("languageid") ? null : new ExcursionReservationLanguage
							{
								id = row.ReadInt("languageid"),
								description = row.ReadNullableTrimmedString("languagename")
							},
							pickuppoint = row.IsNull("geopointfromid") ? null : new PickupPlace
							{
								id = row.ReadInt("geopointfromid"),
								name = row.ReadNullableTrimmedString("geopointfromname")
							},
							pickuphotel = row.IsNull("fromhotelid") ? null : new PickupPlace
							{
								id = row.ReadInt("fromhotelid"),
								name = row.ReadNullableTrimmedString("fromhotelname")
							}
						};
					}
					else
					{
						if (orderType == "transport")
						{
							result.freight = new FreightReservationOrder
							{
								id = row.ReadInt("freightid"),
								name = row.ReadNullableTrimmedString("freightname"),
								bookingclass = row.IsNull("classid") ? null : new FreightReservationBookingclass
								{
									id = row.ReadInt("classid"),
									name = row.ReadNullableTrimmedString("classname")
								},
								place = row.IsNull("frplaceid") ? null : new FreightReservationPlace
								{
									id = row.ReadInt("frplaceid"),
									name = row.ReadNullableTrimmedString("frplacename")
								}
							};
						}
						else
						{
							if (orderType == "transfer")
							{
								result.transfer = new TransferReservationOrder
								{
									id = row.ReadInt("serviceid"),
									name = row.ReadNullableTrimmedString("servicename")
								};
							}
							else
							{
								if (orderType == "service")
								{
									result.service = new ServiceReservationOrder
									{
										id = row.ReadInt("serviceid"),
										name = row.ReadNullableTrimmedString("servicename"),
										servicetype = row.IsNull("servtypeid") ? null : new ServiceReservationServicetype
										{
											id = row.ReadInt("servtypeid"),
											name = row.ReadNullableTrimmedString("servtypename")
										}
									};
								}
							}
						}
					}
				}
				return result;
			}
			public ReservationPeople ReservationPeople(DataRow row)
			{
				return new ReservationPeople
				{
					id = (!row.IsNull("peopleid")) ? row.ReadNullableTrimmedString("peopleid") : row.ReadNullableInt("localid").ToString(),
					localid = row.ReadNullableInt("localid"),
					agetype = row.ReadNullableTrimmedString("agetype"),
					gender = row.ReadNullableTrimmedString("gender"),
					name = row.ReadNullableTrimmedString("name"),
					born = row.ReadNullableUnspecifiedDateTime("born"),
					age = row.ReadNullableInt("age"),
					passport = (row.IsNull("pserie") && row.IsNull("pnumber")) ? null : new ReservationPeoplePassport
					{
						serie = row.ReadNullableTrimmedString("pserie"),
						number = row.ReadNullableTrimmedString("pnumber"),
						issue = row.ReadNullableUnspecifiedDateTime("pissue"),
						valid = row.ReadNullableUnspecifiedDateTime("pvalid")
					},
					address = row.ReadNullableTrimmedString("address"),
					phone = row.ReadNullableTrimmedString("phones"),
					email = row.ReadNullableTrimmedString("email"),
					comment = row.ReadNullableTrimmedString("comment")
				};
			}
			public void FillFreightInfo(FreightReservationOrder freight, DataRow row)
			{
				if (row != null)
				{
					System.TimeSpan? freightDepartureTime = row.ReadNullableUnspecifiedTime("stime");
					FreightPoint freightPoint = new FreightPoint();
					FreightPoint arg_5C_0 = freightPoint;
					System.DateTime dateTime = row.ReadUnspecifiedDateTime("sdate");
					dateTime = dateTime.Date;
					arg_5C_0.date = dateTime.Add(freightDepartureTime ?? System.TimeSpan.FromTicks(0L));
					freightPoint.port = new Airport
					{
						id = row.ReadInt("sport$inc"),
						alias = row.ReadNullableTrimmedString("sport$alias"),
						name = row.ReadNullableTrimmedString("sport$name"),
						town = new Town
						{
							id = row.ReadInt("stown$inc"),
							name = row.ReadNullableString("stown$name")
						}
					};
					freight.departure = freightPoint;
					System.TimeSpan? freightArrivalTime = row.ReadNullableUnspecifiedTime("dtime");
					FreightPoint freightPoint2 = new FreightPoint();
					FreightPoint arg_147_0 = freightPoint2;
					dateTime = freight.departure.date;
					dateTime = dateTime.Date;
					dateTime = dateTime.AddDays((double)row.ReadInt("daysinway"));
					arg_147_0.date = dateTime.Add(freightArrivalTime ?? System.TimeSpan.FromTicks(0L));
					freightPoint2.port = new Airport
					{
						id = row.ReadInt("dport$inc"),
						alias = row.ReadNullableTrimmedString("dport$alias"),
						name = row.ReadNullableTrimmedString("dport$name"),
						town = new Town
						{
							id = row.ReadInt("dtown$inc"),
							name = row.ReadNullableString("dtown$name")
						}
					};
					freight.arrival = freightPoint2;
				}
			}
			public ReservationError ReservationError(DataRow row)
			{
				ReservationError result = new ReservationError
				{
					orderid = row.ReadNullableTrimmedString("orderid"),
					number = row.ReadInt("errnum", 0),
					message = row.ReadNullableTrimmedString("errmessage"),
					usermessage = row.ReadNullableTrimmedString("usermessage"),
					isstop = row.ReadBoolean("stop")
				};
				string errortype = row.ReadNullableTrimmedString("errtype");
				if (errortype != null)
				{
					string text = errortype.ToLower();
					if (text != null)
					{
						if (!(text == "system"))
						{
							if (text == "user")
							{
								result.errortype = ReservationErrorType.user;
							}
						}
						else
						{
							result.errortype = ReservationErrorType.system;
						}
					}
				}
				return result;
			}
			public PaymentMode PaymentMode(DataRow row)
			{
				return new PaymentMode
				{
					id = row.ReadString("id"),
					name = row.ReadNullableTrimmedString("name"),
					processing = row.ReadNullableTrimmedString("processing"),
					comission = row.IsNull("comission") ? null : new ReservationOrderPrice
					{
						total = row.ReadDecimal("comission"),
						currency = row.ReadNullableTrimmedString("currency")
					},
					payrest = row.IsNull("payrest") ? null : new ReservationOrderPrice
					{
						total = row.ReadDecimal("payrest"),
						currency = row.ReadNullableTrimmedString("currency")
					},
					paymentparam = row.ReadNullableTrimmedString("params")
				};
			}
			public PaymentBeforeProcessingResult PaymentBeforeProcessingResult(DataRow row)
			{
				return new PaymentBeforeProcessingResult
				{
					success = row.IsNull("errnum") && row.IsNull("errmessage") && !row.IsNull("invnum"),
					invoiceNumber = row.ReadNullableTrimmedString("invnum")
				};
			}
			public ExcursionVoucher ExcursionVoucher(DataRow row)
			{
				return null;
			}
		}
		private static BookingProvider.BookingFactory factory = new BookingProvider.BookingFactory();
		private static XElement BuildStatusClaimXml(int claimId)
		{
			return new XElement("Booking", new XElement("Reservation", new XAttribute("id", claimId)));
		}
		private static XElement BuildBookingClaimXml(int partner, BookingClaim claim)
		{
			if (claim == null)
			{
				throw new System.ArgumentNullException("claim");
			}
			XElement bookingXml = new XElement("Booking");
			XElement reservationXml = new XElement("Reservation", new object[]
			{
				new XAttribute("partnerid", partner),
				new XElement("Note", claim.note)
			});
			bookingXml.Add(reservationXml);
			XElement personsXml = new XElement("Persons");
			bookingXml.Add(personsXml);
			XElement ordersXml = new XElement("Orders");
			bookingXml.Add(ordersXml);
			if (claim.orders != null)
			{
				foreach (BookingOrder order in claim.orders)
				{
					if (order.excursion != null)
					{
						XElement membersXml = new XElement("Members");
						if (order.excursion.pax != null)
						{
							for (int i = 0; i < order.excursion.pax.adult; i++)
							{
								string personId = System.Guid.NewGuid().ToString();
								if (i == 0 && order.excursion.contact != null)
								{
									personsXml.Add(new XElement("Person", new object[]
									{
										new XAttribute("id", personId),
										new XAttribute("agetype", "adult"),
										(order.excursion.contact.name != null) ? new XAttribute("name", order.excursion.contact.name) : null,
										(order.excursion.contact.name != null) ? new XAttribute("latinname", order.excursion.contact.name) : null,
										(order.excursion.contact.mobile != null) ? new XElement("Contact", new XElement("Phones", new XAttribute("mobile", order.excursion.contact.mobile))) : null
									}));
								}
								else
								{
									personsXml.Add(new XElement("Person", new object[]
									{
										new XAttribute("id", personId),
										new XAttribute("agetype", "adult")
									}));
								}
								membersXml.Add(new XElement("Member", new XAttribute("personid", personId)));
							}
							for (int i = 0; i < order.excursion.pax.child; i++)
							{
								string personId = System.Guid.NewGuid().ToString();
								personsXml.Add(new XElement("Person", new object[]
								{
									new XAttribute("id", personId),
									new XAttribute("agetype", "child")
								}));
								membersXml.Add(new XElement("Member", new XAttribute("personid", personId)));
							}
							for (int i = 0; i < order.excursion.pax.infant; i++)
							{
								string personId = System.Guid.NewGuid().ToString();
								personsXml.Add(new XElement("Person", new object[]
								{
									new XAttribute("id", personId),
									new XAttribute("agetype", "infant")
								}));
								membersXml.Add(new XElement("Member", new XAttribute("personid", personId)));
							}
						}
						ordersXml.Add(new XElement("Order", new object[]
						{
							new XAttribute("type", "excursion"),
							new XAttribute("id", order.orderid ?? ""),
							new XAttribute("datefrom", System.DateTime.SpecifyKind(order.excursion.date.Date, System.DateTimeKind.Unspecified)),
							new XAttribute("datetill", System.DateTime.SpecifyKind(order.excursion.date.Date, System.DateTimeKind.Unspecified)),
							new XElement("Excursion", new object[]
							{
								new XAttribute("excursionid", order.excursion.id),
								(!order.excursion.extime.HasValue) ? null : new XElement("Time", new XAttribute("id", order.excursion.extime)),
								(!order.excursion.grouptype.HasValue) ? null : new XElement("GroupType", new XAttribute("id", order.excursion.grouptype)),
								(!order.excursion.language.HasValue) ? null : new XElement("Language", new XAttribute("id", order.excursion.language)),
								(order.excursion.pickuppoint.HasValue || order.excursion.pickuphotel.HasValue) ? new XElement("From", new object[]
								{
									order.excursion.pickuppoint.HasValue ? new XElement("Geopoint", new XAttribute("id", order.excursion.pickuppoint)) : null,
									order.excursion.pickuphotel.HasValue ? new XElement("Hotel", new XAttribute("id", order.excursion.pickuphotel)) : null
								}) : null
							}),
							new XElement("Note", order.excursion.note),
							membersXml
						}));
					}
				}
			}
			if (claim.customer != null)
			{
				Customer customer = claim.customer;
				bookingXml.Add(new XElement("Customer", new object[]
				{
					new XAttribute("name", customer.name ?? ""),
					(claim.customer.passport == null) ? null : new XElement("Passport", new object[]
					{
						(customer.passport.serie == null) ? null : new XAttribute("serie", customer.passport.serie),
						(customer.passport.number == null) ? null : new XAttribute("number", customer.passport.number),
						(!customer.passport.issuedate.HasValue) ? null : new XAttribute("issuedate", System.DateTime.SpecifyKind(customer.passport.issuedate.Value, System.DateTimeKind.Unspecified)),
						(customer.passport.issueplace == null) ? null : new XAttribute("issueplace", customer.passport.issueplace)
					}),
					new XElement("Contact", new object[]
					{
						(customer.address == null) ? null : new XElement("Address", customer.address),
						(customer.mobile == null) ? null : new XElement("Phones", new XAttribute("mobile", customer.mobile)),
						(customer.email == null) ? null : new XElement("Email", customer.email)
					})
				}));
			}
			return bookingXml;
		}
		public static ReservationState GetReservationState(string language, int claimId)
		{
			XElement xml = BookingProvider.BuildStatusClaimXml(claimId);
			return BookingProvider.BuildBookingProcessResult(language, "status", xml, null);
		}
		public static ReservationState DoCalculation(string language, int partnerId, BookingClaim claim)
		{
			if (claim == null)
			{
				throw new System.ArgumentNullException("claim");
			}
			XElement xml = BookingProvider.BuildBookingClaimXml(partnerId, claim);
			return BookingProvider.BuildBookingProcessResult(language, "calc", xml, null);
		}
		public static ReservationState DoBooking(string language, int partnerId, int partnerPassId, BookingClaim claim)
		{
			if (claim == null)
			{
				throw new System.ArgumentNullException("claim");
			}
			XElement xml = BookingProvider.BuildBookingClaimXml(partnerId, claim);
			return BookingProvider.BuildBookingProcessResult(language, "save", xml, new int?(partnerPassId));
		}
		private static ReservationState BuildBookingProcessResult(string language, string action, XElement xml, int? partnerPassId)
		{
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_BookingProcess", "state,orders,people,errors", new
			{
				action = action,
				xml = xml,
				lang = language,
				partpass = partnerPassId
			});
			ReservationState result = (
				from DataRow row in ds.Tables["state"].Rows
				select BookingProvider.factory.ReservationState(row)).FirstOrDefault<ReservationState>();
			if (result != null)
			{
				result.orders = (
					from DataRow row in ds.Tables["orders"].Rows
					orderby BookingProvider.factory.ReservationOrderSorting(row)
					select BookingProvider.factory.ReservationOrder(row)).ToList<ReservationOrder>();
				foreach (ReservationOrder order in result.orders)
				{
					if (order.freight != null)
					{
						DataSet freightds = DatabaseOperationProvider.QueryProcedure("up_guest_getFreightInfo", "freight", new
						{
							freight = order.freight.id,
							date = order.datefrom,
							language = language
						});
						BookingProvider.factory.FillFreightInfo(order.freight, freightds.Tables["freight"].Rows.Cast<DataRow>().FirstOrDefault<DataRow>());
					}
				}
				result.people = (
					from DataRow row in ds.Tables["people"].Rows
					select BookingProvider.factory.ReservationPeople(row)).ToList<ReservationPeople>();
				result.errors = (
					from DataRow row in ds.Tables["errors"].Rows
					select BookingProvider.factory.ReservationError(row)).ToList<ReservationError>();

                result.customer = GetCustomer(Convert.ToInt32(result.claimId));

                if ((action != "save") && (result.claimId != null))
                {
                    result.agent = GetAgencyName(Convert.ToInt32(result.claimId)); //по id заказа из note забираем alias, по alias получаем login, и agent id
                }
            }
			return result;
		}

        private static string GetAgencyName(int claimId)
        {
            var res = DatabaseOperationProvider.Query("select c.name from guestservice_UserProfile as b, guestservice_claim as a, partner as c where a.claim = @claimId and b.userId=a.userId and b.partnerId = c.inc", "agent", new { claimId = claimId });

            if (res.Tables["agent"].Rows.Count == 0)
                return "";
            else
                return res.Tables["agent"].Rows[0].ReadNullableTrimmedString("name");
        }
        //добавить язык
        private static ReservationCustomer GetCustomer(int claimId)
        {
            var res = DatabaseOperationProvider.Query("select a.name, a.email, a.phones, a.inc, a.address, b.language from physical_buyer as a, guestservice_claim as b where a.claim = @claimId and b.claim =  @claimId ", "customer", new { claimId = claimId });

            if (res.Tables["customer"].Rows.Count > 0)
                return (from DataRow row in res.Tables["customer"].Rows
                        select BookingProvider.factory.ReservationCustomer(row)).FirstOrDefault<ReservationCustomer>();
            else
            {
                res = DatabaseOperationProvider.Query("select a.name, a.email, a.phones, a.inc, a.address, 'en' as language from physical_buyer as a where a.claim = @claimId ", "customer", new { claimId = claimId });

                return (from DataRow row in res.Tables["customer"].Rows
                        select BookingProvider.factory.ReservationCustomer(row)).FirstOrDefault<ReservationCustomer>();
            }
        }


        public static System.Collections.Generic.List<PaymentMode> GetPaymentModes(string language, int claimId)
		{
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getPaymentModes", "modes", new
			{
				claimid = claimId,
				language = language
			});
			return (
				from DataRow row in ds.Tables["modes"].Rows
				select BookingProvider.factory.PaymentMode(row)).ToList<PaymentMode>();
		}
		public static PaymentBeforeProcessingResult BeforePaymentProcessing(string language, string paymentparam)
		{
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_PayProcessing_Before", "processing", new
			{
				paymentparam,
				language
			});
			return (
				from DataRow row in ds.Tables["processing"].Rows
				select BookingProvider.factory.PaymentBeforeProcessingResult(row)).FirstOrDefault<PaymentBeforeProcessingResult>();
		}
		public static ConfirmInvoiceResult ConfirmInvoice(string invoiceNumber)
		{
			if (string.IsNullOrEmpty(invoiceNumber))
			{
				throw new System.ArgumentNullException("invoiceNumber");
			}
			ConfirmInvoiceResult result = new ConfirmInvoiceResult();
			using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
			{
				using (SqlCommand command = connection.CreateCommand())
				{
					command.CommandType = CommandType.StoredProcedure;
					command.CommandText = "invoicesforbank.dbo.up_ConfirmInvoiceByINumber";
					command.Parameters.Add("INumber", SqlDbType.VarChar).Value = invoiceNumber;
					using (OperationDurationLimit.ShortOperation(command))
					{
						connection.Open();
						using (SqlDataReader reader = command.ExecuteReader())
						{
							if (reader.Read())
							{
								int[] fields = new int[]
								{
									reader.GetOrdinal("ret"),
									reader.GetOrdinal("message")
								};
								result.Error = new int?(reader.ReadInt(fields[0]));
								result.ErrorMessage = reader.ReadNullableTrimmedString(fields[1]);
							}
						}
					}
				}
			}
			return result;
		}

        public static void AcceptInvoice()
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "income.dbo.up_RegPaymentsFromBankInvoices";

                    using (OperationDurationLimit.ShortOperation(command))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                        }
                    }
                }
            }
        }

        public static void AcceptInvoice(int claimId)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "income.dbo.up_RegPaymentsFromBankInvoices";

                    using (OperationDurationLimit.ShortOperation(command))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                        }
                    }
                }
            }

            //отправка сообщений
            {
                var reservation = BookingProvider.GetReservationState(UrlLanguage.CurrentLanguage, claimId);


                Task[] tasks = new Task[]
                                {
                                    Task.Factory.StartNew(() => new SimpleEmailService().SendEmail<ReservationState>(reservation.customer.mail, "payment", reservation.customer.language, reservation, false, null)),
                                    Task.Factory.StartNew(() => new SmsSender().SendMessage<ReservationState>(reservation.customer.phone, "payment", reservation.customer.language, reservation))
                                };

                Task.WaitAll(tasks);
            }
        }
   }
}
