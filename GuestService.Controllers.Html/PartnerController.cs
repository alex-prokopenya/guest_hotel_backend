namespace GuestService.Controllers.Html
{
    using GuestService;
    using GuestService.Code;
    using GuestService.Controllers.Api;
    using GuestService.Data;
    using GuestService.Models.Guest;
    using GuestService.Models.Guide;
    using GuestService.Models.Partner;
    using Sm.Report;
    using Sm.System.Database;
    using Sm.System.Mvc;
    using Sm.System.Mvc.Language;
    using Sm.System.Trace;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Web.Mvc;
    using WebMatrix.WebData;


    [HttpPreferences, Authorize, UrlLanguage, WebSecurityInitializer]
    public class PartnerController : BaseController
    {
        [AllowAnonymous, HttpGet, ActionName("brief")]
        public ActionResult Brief(GuestWebParams param)
        {
            BriefContext model = new BriefContext();
            return base.View(model);
        }

        private ActionResult BuildVoucher(int claimId)
        {
            ActionResult result3;
            try
            {
                List<ReportParam> list = new List<ReportParam>();
                ReportParam item = new ReportParam
                {
                    Name = "vClaimList",
                    Value = claimId.ToString()
                };
                list.Add(item);
                string str = ConfigurationManager.AppSettings["report_PrintVoucher"];
                if (string.IsNullOrEmpty(str))
                {
                    throw new Exception("report_PrintVoucher is empty");
                }
                ReportResult result = ReportServer.BuildReport(str, ReportFormat.pdf, list.ToArray());
                if (result == null)
                {
                    throw new Exception("report data is empty");
                }
                MemoryStream fileStream = new MemoryStream(result.Content);
                FileStreamResult result2 = new FileStreamResult(fileStream, "application/pdf")
                {
                    FileDownloadName = string.Format("voucher_{0}.pdf", claimId)
                };
                result3 = result2;
            }
            catch (Exception exception)
            {
                Tracing.ServiceTrace.TraceEvent(TraceEventType.Error, 0, exception.ToString());
                throw;
            }
            return result3;
        }

        private void ClearErrorState(ModelState modelState)
        {
            if (modelState != null)
            {
                modelState.Errors.Clear();
            }
        }

        [HttpGet, AllowAnonymous, ActionName("departure")]
        public ActionResult Departure(GuestWebParams param)
        {
            DepartureContext context;
            DateTime date = ((param != null) && param.TestDate.HasValue) ? param.TestDate.Value.Date : DateTime.Now.Date;
            if (WebSecurity.IsAuthenticated)
            {
                context = new DepartureContext();
                List<GuestClaim> list = GuestProvider.GetActiveClaims(UrlLanguage.CurrentLanguage, WebSecurity.CurrentUserId, date);
                if ((list != null) && (list.Count > 0))
                {
                    context.Hotels = new List<DepartureHotel>();
                    foreach (GuestClaim claim in list)
                    {
                        int? hotel = null;
                        context.Hotels.AddRange(GuestProvider.GetDepartureInfo(UrlLanguage.CurrentLanguage, date, date.AddDays(1.0), hotel, new int?(claim.claim)));
                    }
                }
                return base.View(context);
            }
            if (HttpPreferences.Current.LocationHotel != null)
            {
                context = new DepartureContext
                {
                    Hotel = CatalogProvider.GetHotelDescription(UrlLanguage.CurrentLanguage, HttpPreferences.Current.LocationHotel)
                };
                if (context.Hotel != null)
                {
                    context.Hotels = GuestProvider.GetDepartureInfo(UrlLanguage.CurrentLanguage, date, date.AddDays(1.0), new int?(context.Hotel.id), null);
                }
                return base.View(context);
            }
            string str = base.Url.RouteUrl(base.Request.QueryStringAsRouteValues());
            return base.RedirectToAction("login", "account", new { returnUrl = str });
        }

        [ActionName("findorder"), HttpGet]
        public ActionResult FindOrder()
        {
            FindOrderContext model = new FindOrderContext();
            return base.View(model);
        }

        [ValidateAntiForgeryToken, ActionName("findorder"), HttpPost]
        public ActionResult FindOrderPost(string id, [Bind(Prefix = "Form")] FindOrderModel form)
        {
            FindOrderContext model = new FindOrderContext
            {
                Form = form
            };
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
                int currentUserId = WebSecurity.CurrentUserId;
                if (form.RequestType == "claim")
                {
                    model.Claims = GuestProvider.FindGuestClaims(UrlLanguage.CurrentLanguage, currentUserId, form.ClaimName, new int?(Convert.ToInt32(form.Claim)), null);
                }
                else
                {
                    if (form.RequestType != "passport")
                    {
                        throw new Exception("invalid RequestType");
                    }
                    model.Claims = GuestProvider.FindGuestClaims(UrlLanguage.CurrentLanguage, currentUserId, form.PassportName, null, form.Passport);
                }
                model.NotFound = model.Claims.Count == 0;
            }
            return base.View(model);
        }

        [HttpGet, AllowAnonymous, ActionName("index")]
        public ActionResult Index(GuestWebParams param)
        {
            if (!WebSecurity.IsAuthenticated)
            {
                string str = base.Url.RouteUrl(base.Request.QueryStringAsRouteValues());
                return base.RedirectToAction("login", "account", new { returnUrl = str });
            }

            //получить им€ партнера по userId

            PartnerContext model = new PartnerContext();
            model.UserName = WebSecurity.CurrentUserName;
            model.ProviderName = Api.PartnerExcursionController.GetProviderName(WebSecurity.CurrentUserId);

            DateTime firstDate = ((param != null) && param.TestDate.HasValue) ? param.TestDate.Value.Date : DateTime.Now.Date;
            model.GuideDurties = new List<HotelGuideResult>();
            List<GuestOrder> list = GuestProvider.GetActiveHotelOrders(UrlLanguage.CurrentLanguage, WebSecurity.CurrentUserId, firstDate, firstDate.AddDays(1.0));
            if ((list != null) && (list.Count > 0))
            {
                GuideController controller = new GuideController();
                foreach (GuestOrder order in list)
                {
                    HotelGuideParam param2 = new HotelGuideParam
                    {
                         h = order.hotelid,
                         ln = UrlLanguage.CurrentLanguage,
                         pb = new DateTime?(order.period.begin.Value),
                         pe = new DateTime?(order.period.end.Value)
                     };
                     HotelGuideResult item = controller.HotelGuide(param2);
                     model.GuideDurties.Add(item);
                }
            }
         
            return base.View(model);
        }

        [ValidateAntiForgeryToken, HttpPost, ActionName("linkorder")]
        public ActionResult LinkOrder([Bind(Prefix = "Link")] LinkOrderModel model)
        {
            if ((model != null) && model.Claim.HasValue)
            {
                GuestProvider.LinkGuestClaim(WebSecurity.CurrentUserId, model.Name, model.Claim.Value);
                return base.RedirectToAction("order", new { id = model.Claim });
            }
            return base.RedirectToAction("order");
        }

        [ActionName("balance"), HttpGet]
        public ActionResult Balance(int? id, int? excId, string dateFrom, string dateTo, int? language)
        {
            PartnerBalanceContext model = new PartnerBalanceContext();

            int currentUserId = WebSecurity.CurrentUserId;

            model.PartnerId = Api.PartnerExcursionController.GetProviderId(currentUserId);

            model.PartnerAuth = model.PartnerAuth = GetMd5Hash(model.PartnerId);

            return base.View(model);
        }

        [HttpPost, ActionName("balance")]
        public ActionResult Balance([Bind(Prefix = "OrderFindForm")] PartnerBalanceContext model)
        {
            model = new PartnerBalanceContext();

            int currentUserId = WebSecurity.CurrentUserId;

            model.PartnerId = Api.PartnerExcursionController.GetProviderId(currentUserId);

            model.PartnerAuth = GetMd5Hash(model.PartnerId);

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

        [HttpPost, ActionName("order")]
        public ActionResult Order([Bind(Prefix = "OrderFindForm")] PartnerOrderContext model)
        {
            model = new PartnerOrderContext();
            var test = Request.Params.AllKeys;

            int currentUserId = WebSecurity.CurrentUserId;

            int partnerId = Api.PartnerExcursionController.GetProviderId(currentUserId);

            var filters = Filters(currentUserId);

            model.FilterExcursions = filters.excursions;

            model.FilterLanguages = filters.languages;

            model.FilterSelectedExcursion = Request.Params["excursion_filter"] != null? Convert.ToInt32(Request.Params["excursion_filter"]) : -1;

            model.FilterSelectedLanguage = Request.Params["lang_filter"] != null ? Convert.ToInt32(Request.Params["lang_filter"]) : -1;

            model.FilterDateFrom = Request.Params["date_from"] != null ? Convert.ToDateTime(Request.Params["date_from"]).ToString("yyyy-MM-dd") : DateTime.Today.ToString("yyyy-MM-dd");

            model.FilterDateTo = Request.Params["date_till"] != null ? Convert.ToDateTime(Request.Params["date_till"]).ToString("yyyy-MM-dd") : DateTime.Today.AddDays(2).ToString("yyyy-MM-dd");

            model.PartnerId = partnerId;

            model.PartnerAuth = GetMd5Hash(partnerId);

            if (Request.Params["act"] == "report")
            {
                string getParams =  "?ReportFormat=" + Request.Params["ReportFormat"] +
                                    "&ReportName=" + Request.Params["ReportName"] +
                                    "&date_from=" + model.FilterDateFrom + "&date_till=" + model.FilterDateTo +
                                    "&pr_id=" + partnerId + "&sid=" + GetMd5Hash(partnerId);

                return new RedirectResult(GuestService.Settings.PartnerReportUrl + getParams);
            }
            else
            {
                List<PartnerOrder> linkedClaims = GetPartnerOrders(partnerId, model.FilterDateFrom, model.FilterDateTo, model.FilterSelectedLanguage, model.FilterSelectedExcursion);

                model.Orders = linkedClaims;

                model.ClaimsNotFound = linkedClaims.Count == 0;
            }

            return base.View(model);
        }

        private List<PartnerOrder> GetPartnerOrders(int partnerId, string dateFrom, string dateTo, int language, int excursion)
        {
            var query = "SELECT o.inc, o.claim, o.service, o.datebeg,  gr.lname as gname, lg.lname as lgname, tm.lname as tmname, " +
                                " xs.adult, xs.child, xs.inf, ex.lname as exlname,  ex.name as exname, st.lname as stname, " +
                                " pb.name as cname, pb.address as caddress, xs.picktime, xs.hotel, ht.lname, xs.pickuppoint, gp.lname as gpname, " +
                                " lg.inc as lang, ex.inc as exc, o.partner as provider " +
                                " FROM[income].[dbo].[order] as o " +
                                " left outer join[income].[dbo].exgrouptype as gr on gr.inc = o.exgrouptype " +
                                " left outer join[income].[dbo].language as lg on lg.inc = o.language " +
                                " left outer join[income].[dbo].extime as tm on tm.inc = o.extime " +
                                " left outer join[income].[dbo].exsale as xs on xs.[order] = o.inc " +
                                " left outer join[income].[dbo].service as ex on ex.inc = o.service " +
                                " left outer join[income].[dbo].claim as cl on cl.inc = o.claim " +
                                " left outer join[income].[dbo].status as st on st.inc = cl.status " +
                                " left outer join income.dbo.[physical_buyer] as pb on pb.claim = cl.inc " +
                                " left outer join income.dbo.hotel as ht on ht.inc = xs.hotel " +
                                " left outer join income.dbo.geopoint as gp on gp.inc = xs.pickuppoint " +

                                " where o.datebeg between '" + dateFrom + "' and '" + dateTo + "' " +

                                (language >= 0 ? " and ISNULL(lg.inc, 0) = " + language : "") +

                                (excursion >= 0 ? " and ex.excurs = " + excursion : "") +

                                " and o.partner = " + partnerId;

            //прочитать
            var res = DatabaseOperationProvider.Query(query, "orders", new { });

            Dictionary<int, PartnerOrder> tempResult = new Dictionary<int, PartnerOrder>();

            string orderKeys = "-1";

            foreach (DataRow row in res.Tables["orders"].Rows)
            {
                var tempOrder = new PartnerOrder()
                {
                    orderId = row.ReadInt("inc"),
                    claimId = row.ReadInt("claim").ToString(),
                    beginDate = row.ReadDateTime("datebeg").ToString("yyyy-MM-dd"),
                    title = row.ReadNullableString("exlname")+ ", " + row.ReadNullableString("gname") + ", " + row.ReadNullableString("tmname") ,
                    adults = 0,
                    childs = 0,
                    infs = 0,
                    customerName = row.ReadNullableString("cname"),
                    customerAddress = row.ReadNullableString("caddress"),
                    language = row.ReadNullableString("lgname"),
                    status = row.ReadNullableString("stname"),
                    pickup = row.ReadNullableString("gpname")
                };

                tempResult[tempOrder.orderId] = tempOrder;

                orderKeys += ", " + tempOrder.orderId;
            }

            var selectPeople = " SELECT human, op.[order] from[income].[dbo].opeople as op " +

                                " inner join[income].[dbo].people as p on p.inc = op.people where  op.[order] in ("+ orderKeys + ")";

             res = DatabaseOperationProvider.Query(selectPeople, "orders", new { });
            //прочитать, заполнить данные
            foreach (DataRow row in res.Tables["orders"].Rows)
            {
                var inc = row.ReadInt("order");
                var type = row.ReadString("human");

                if (tempResult.ContainsKey(inc))
                {
                    switch (type)
                    {
                        case "CHD":
                            tempResult[inc].childs++;
                            break;
                        case "INF":
                            tempResult[inc].infs++;
                            break;
                        default:
                            tempResult[inc].adults++;
                        break;
                    }
                }
            }

            return tempResult.Values.ToList();
        }

        [ActionName("order"), HttpGet]
        public ActionResult Order(int? id, int? excId, string dateFrom, string dateTo, int? language)
        {
            PartnerOrderContext model = new PartnerOrderContext();

            int currentUserId = WebSecurity.CurrentUserId;

            int partnerId = Api.PartnerExcursionController.GetProviderId(currentUserId);

            var filters = Filters(currentUserId);

            model.FilterExcursions = filters.excursions;

            model.FilterLanguages = filters.languages;

            model.FilterSelectedExcursion = excId.HasValue ? excId.Value : -1;

            model.FilterSelectedLanguage = language.HasValue ? language.Value : -1;

            model.FilterDateFrom = dateFrom != null ? dateFrom : DateTime.Today.ToString("yyyy-MM-dd");

            model.FilterDateTo = dateTo != null ? dateTo : DateTime.Today.AddDays(2).ToString("yyyy-MM-dd");

            List<PartnerOrder> linkedOrders = GetPartnerOrders(partnerId, model.FilterDateFrom, model.FilterDateTo, model.FilterSelectedLanguage, model.FilterSelectedExcursion);

            model.Orders = linkedOrders;

            model.ClaimsNotFound = linkedOrders.Count == 0;

            return base.View(model);
        }

        public FiltersResult Filters(int userId)
        {
            FiltersResult result = null;

            if (result == null || Settings.IsCacheDisabled)
            {
                result = new FiltersResult();
                System.Collections.Generic.List<CatalogExcursionMinPrice> excursions = ExcursionProvider.FindExcursions(UrlLanguage.CurrentLanguage, 2, null, null, null, null, null, null, null, null, null, null, null, null);

                var filtered = Api.PartnerExcursionController.FilterExcursions(excursions, Api.PartnerExcursionController.GetProviderId(userId));

                var excs = new List<CatalogFilterItem>();

                foreach (CatalogExcursionMinPrice item in filtered)
                    excs.Add( new CatalogFilterItem() { id = item.excursion.id, name = item.excursion.name, count = 0 });

                //фильтруем по поставщику
                result.excursions = excs;
                result.languages = new List<CatalogFilterItem>();
                result.languages.Add(new CatalogFilterItem() { id = 0, name = "no language", count = 0 });
                result.languages.AddRange( ExcursionProvider.BuildFilterLanguages(filtered, null));
               
            }
            return result;
        }

        private Dictionary<int, string> GetPartnerLanguages( int currentUserId )
        {
            var result = new Dictionary<int, string>();

            result.Add(0, "Ѕез €зыка");
            result.Add(1, "item1");
            result.Add(2, "item2");

            return result;
        }

        private Dictionary<int, string> GetPartnerExcursions( int currentUserId )
        {
            var result = new Dictionary<int, string>();

            result.Add(1, "item1");
            result.Add(2, "item2");

            return result;
        }

        [AllowAnonymous, ActionName("payorder")]
        public ActionResult PayOrder(int? id)
        {
          //  WebSecurity.get
            if (WebSecurity.IsAuthenticated)
            {
                int guestId = WebSecurity.CurrentUserId;
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
                            where m.id == "2.EUR"//model.paymentId //ID способа оплаты
                            select m).FirstOrDefault<PaymentMode>();

                    if (paymentMode == null)
                    {
                        throw new System.Exception(string.Format("payment mode id '{0}' not found", "2.EUR"));
                    }

                    PaymentBeforeProcessingResult beforePaymentResult = BookingProvider.BeforePaymentProcessing(UrlLanguage.CurrentLanguage, paymentMode.paymentparam);

                    //approve invoice
                    ConfirmInvoiceResult invoiceResult = BookingProvider.ConfirmInvoice(beforePaymentResult.invoiceNumber.Trim());

                    //update status
                    BookingProvider.AcceptInvoice(id.Value);

                    //redirect to order id  
                    return base.RedirectToAction("order", new { id = id.Value });

                }
            }
            return null;
        }

        [AllowAnonymous, ActionName("printorder")]
        public ActionResult PrintOrder(int? id)
        {
            PrintOrderContext context = new PrintOrderContext();
            context.Form = new PrintOrderModel();
            context.Form.Claim = (id.HasValue ? id.ToString() : "");
            ActionResult result;
            if (WebSecurity.IsAuthenticated)
            {
                int guestId = WebSecurity.CurrentUserId;
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
                    result = this.BuildVoucher(detailedId.Value);
                    return result;
                }
                context.NotFound = true;
            }
            result = base.View(context);
            return result;
        }

        [ActionName("printorder"), HttpPost, AllowAnonymous]
        public ActionResult PrintOrderPost([Bind(Prefix = "Form")] PrintOrderModel model)
        {
            PrintOrderContext context = new PrintOrderContext
            {
                Form = model
            };
            int result = 0;
            if (base.ModelState.IsValid && int.TryParse(model.Claim, out result))
            {
                List<GuestClaim> list = GuestProvider.FindGuestClaims(UrlLanguage.CurrentLanguage, 0, model.Name, new int?(result), null);
                if ((list != null) && (list.Count > 0))
                {
                    return this.BuildVoucher(result);
                }
                context.NotFound = true;
            }
            return base.View(context);
        }

        [HttpGet, ActionName("summary"), AllowAnonymous]
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

        [AllowAnonymous, HttpPost, ActionName("summary")]
        public ActionResult Summary([Bind(Prefix = "OrderFindForm")] OrderModel model)
        {
            SummaryContext context = new SummaryContext
            {
                ShowOrderFindForm = true
            };
            if (base.ModelState.IsValid)
            {
                DateTime? currentDate = model.CurrentDate;
                DateTime dateFrom = currentDate.HasValue ? currentDate.GetValueOrDefault() : DateTime.Now.Date;
                List<GuestClaim> claims = GuestProvider.FindGuestClaims(UrlLanguage.CurrentLanguage, 0, model.ClaimName, new int?(Convert.ToInt32(model.Claim)), null);
                if ((claims != null) && (claims.Count > 0))
                {
                    int? nullable2;
                    ReservationState reservationState = BookingProvider.GetReservationState(UrlLanguage.CurrentLanguage, claims[0].claim);
                    if ((reservationState != null) && (nullable2 = reservationState.claimId).HasValue)
                    {
                        context.Claim = reservationState;
                        context.ShowOrderFindForm = false;
                        context.Hotels = new List<DepartureHotel>();
                        foreach (GuestClaim claim in claims)
                        {
                            nullable2 = null;
                            context.Hotels.AddRange(GuestProvider.GetDepartureInfo(UrlLanguage.CurrentLanguage, dateFrom, dateFrom.AddDays(1.0), nullable2, new int?(claim.claim)));
                        }
                        context.GuideDurties = new List<HotelGuideResult>();
                        List<GuestOrder> list2 = GuestProvider.GetActiveHotelOrders(claims, dateFrom, dateFrom.AddDays(1.0));
                        if ((list2 != null) && (list2.Count > 0))
                        {
                            GuideController controller = new GuideController();
                            foreach (GuestOrder order in list2)
                            {
                                HotelGuideParam param = new HotelGuideParam
                                {
                                    h = order.hotelid,
                                    ln = UrlLanguage.CurrentLanguage,
                                    pb = new DateTime?(order.period.begin.Value),
                                    pe = new DateTime?(order.period.end.Value)
                                };
                                HotelGuideResult item = controller.HotelGuide(param);
                                context.GuideDurties.Add(item);
                            }
                        }
                    }
                }
            }
            context.OrderFindNotFound = context.Claim == null;
            return base.View(context);
        }

        [ActionName("unlinkorder"), ValidateAntiForgeryToken, HttpPost]
        public ActionResult UnlinkOrder([Bind(Prefix = "Unlink")] UnlinkOrderModel model)
        {
            if ((model != null) && model.Claim.HasValue)
            {
                GuestProvider.UnlinkGuestClaim(WebSecurity.CurrentUserId, model.Claim.Value);
            }
            return base.RedirectToAction("order");
        }
    }
}

