using GuestService.Code;
using GuestService.Data;
using GuestService.Models.Booking;
using GuestService.Models.Excursion;
using GuestService.Models.Partner;
using GuestService.Resources;
using Sm.System.Mvc.Language;
using Sm.System.Mvc.Theme;
using System.Web.Mvc;
using WebMatrix.WebData;
using Sm.System.Mvc;
using Sm.System.Database;
using System.Data;
using System;
using System.Net.Http;
namespace GuestService.Controllers.Html
{
    [HttpPreferences, WebSecurityInitializer, UrlLanguage]
    public class PartnerExcursionController : BaseController
    {
        [AllowAnonymous, HttpGet, ActionName("logout")]
        public ActionResult Logout(ExcursionIndexWebParam param)
        {
            base.Session.Abandon();
            WebSecurity.Logout();
            if (param != null && param.visualtheme != null)
            {
                new VisualThemeManager(this).SafeSetThemeName(param.visualtheme);
            }
            ExcursionIndexContext context = new ExcursionIndexContext();
            context.PartnerAlias = ((param.PartnerAlias != null) ? param.PartnerAlias : Settings.ExcursionDefaultPartnerAlias);
            if (string.IsNullOrEmpty(context.PartnerAlias))
            {
                throw new System.ArgumentException("partner alias is not specified");
            }
            context.StartPointAlias = param.StartPointAlias;
            context.ExcursionDate = System.DateTime.Today.Date.AddDays((double)Settings.ExcursionDefaultDate);
            if (param.ShowCommand != null)
            {
                context.NavigateState = new ExcursionIndexNavigateCommand();
                if (param.ShowCommand.ToLower() == "search")
                {
                    if (param.SearchText != null || param.Categories != null || param.Destinations != null || param.ExcursionLanguages != null)
                    {
                        context.NavigateState.Cmd = "search";
                        context.NavigateState.Options = new ExcursionIndexNavigateOptions
                        {
                            text = param.SearchText,
                            categories = param.Categories,
                            destinations = param.Destinations,
                            languages = param.ExcursionLanguages
                        };
                    }
                }
                else
                {
                    if (param.ShowCommand.ToLower() == "description")
                    {
                        if (param.Excursion.HasValue)
                        {
                            context.NavigateState.Cmd = "description";
                            context.NavigateState.Options = new ExcursionIndexNavigateOptions
                            {
                                excursion = param.Excursion
                            };
                        }
                    }
                }
            }
            return base.View(context);
        }

        [AllowAnonymous, HttpGet, ActionName("howtobook")]
        public ActionResult HowToBook(ExcursionIndexWebParam param)
        {
            if (param != null && param.visualtheme != null)
            {
                new VisualThemeManager(this).SafeSetThemeName(param.visualtheme);
            }
            ExcursionIndexContext context = new ExcursionIndexContext();
            context.PartnerAlias = ((param.PartnerAlias != null) ? param.PartnerAlias : Settings.ExcursionDefaultPartnerAlias);
            if (string.IsNullOrEmpty(context.PartnerAlias))
            {
                throw new System.ArgumentException("partner alias is not specified");
            }
            context.StartPointAlias = param.StartPointAlias;
            context.ExcursionDate = System.DateTime.Today.Date.AddDays((double)Settings.ExcursionDefaultDate);
            if (param.ShowCommand != null)
            {
                context.NavigateState = new ExcursionIndexNavigateCommand();
                if (param.ShowCommand.ToLower() == "search")
                {
                    if (param.SearchText != null || param.Categories != null || param.Destinations != null || param.ExcursionLanguages != null)
                    {
                        context.NavigateState.Cmd = "search";
                        context.NavigateState.Options = new ExcursionIndexNavigateOptions
                        {
                            text = param.SearchText,
                            categories = param.Categories,
                            destinations = param.Destinations,
                            languages = param.ExcursionLanguages
                        };
                    }
                }
                else
                {
                    if (param.ShowCommand.ToLower() == "description")
                    {
                        if (param.Excursion.HasValue)
                        {
                            context.NavigateState.Cmd = "description";
                            context.NavigateState.Options = new ExcursionIndexNavigateOptions
                            {
                                excursion = param.Excursion
                            };
                        }
                    }
                }
            }
            return base.View(context);
        }

        [AllowAnonymous, HttpGet, ActionName("howtopay")]
        public ActionResult HowToPay(ExcursionIndexWebParam param)
        {
            if (param != null && param.visualtheme != null)
            {
                new VisualThemeManager(this).SafeSetThemeName(param.visualtheme);
            }
            ExcursionIndexContext context = new ExcursionIndexContext();
            context.PartnerAlias = ((param.PartnerAlias != null) ? param.PartnerAlias : Settings.ExcursionDefaultPartnerAlias);
            if (string.IsNullOrEmpty(context.PartnerAlias))
            {
                throw new System.ArgumentException("partner alias is not specified");
            }
            context.StartPointAlias = param.StartPointAlias;
            context.ExcursionDate = System.DateTime.Today.Date.AddDays((double)Settings.ExcursionDefaultDate);
            if (param.ShowCommand != null)
            {
                context.NavigateState = new ExcursionIndexNavigateCommand();
                if (param.ShowCommand.ToLower() == "search")
                {
                    if (param.SearchText != null || param.Categories != null || param.Destinations != null || param.ExcursionLanguages != null)
                    {
                        context.NavigateState.Cmd = "search";
                        context.NavigateState.Options = new ExcursionIndexNavigateOptions
                        {
                            text = param.SearchText,
                            categories = param.Categories,
                            destinations = param.Destinations,
                            languages = param.ExcursionLanguages
                        };
                    }
                }
                else
                {
                    if (param.ShowCommand.ToLower() == "description")
                    {
                        if (param.Excursion.HasValue)
                        {
                            context.NavigateState.Cmd = "description";
                            context.NavigateState.Options = new ExcursionIndexNavigateOptions
                            {
                                excursion = param.Excursion
                            };
                        }
                    }
                }
            }
            return base.View(context);
        }

        [ActionName("setstopsale"), HttpPost]
        public JsonResult SetStop(ExcursionAddWebParam param)
        {
            if (param == null)
            {
                throw new System.ArgumentNullException("param");
            }
            System.Collections.Generic.List<string> errors = new System.Collections.Generic.List<string>();
            JsonResult result;
            if (param.excursion != null)
            {
                if (param.excursion.pax != null && param.excursion.pax.adult + param.excursion.pax.child > 0)
                {
                    using (ShoppingCart cart = ShoppingCart.CreateFromSession(base.Session))
                    {
                        cart.Orders.Add(new BookingOrder
                        {
                            orderid = System.Guid.NewGuid().ToString(),
                            excursion = param.excursion
                        });
                    }
                    result = base.Json(new
                    {
                        ok = true
                    });
                    return result;
                }
                errors.Add(ExcursionStrings.Get("ErrorGuestCount"));
            }
            else
            {
                errors.Add(ExcursionStrings.Get("ErrorInvalidParams"));
            }
            result = base.Json(new
            {
                errormessages = errors.ToArray()
            });
            return result;
        }

        [ActionName("changequota"), HttpPost]
        public JsonResult Changequota(ExcursionAddWebParam param)
        {
            System.Collections.Generic.List<string> errors = new System.Collections.Generic.List<string>();
            JsonResult result;

            if (param == null)
            {
                throw new System.ArgumentNullException("param");
            }
            try
            {
                //param.excursion.date
                string selectLimit = "select  top 1  * " +
                                      " from  " +
                                      "  exlimit  " +
                                      " where excurs = @Excurs  " +
                                      "  and @Date between datebeg and dateend  " +
                                      "  and substring(isnull(days, '1111111'), datepart(weekday, @Date), 1) > '0'  " +
                                      "  and(isnull(extime, 0) = 0 or isnull(extime, 0) = isnull(@ExTime, 0))  " +
                                      "  and(language is null or language = @Language)  " +
                                      "  and(region is null)  ";

                var needDate = param.excursion.date.Date;

                DataSet ds = DatabaseOperationProvider.Query(selectLimit, "limits", new
                {
                    Excurs = param.excursion.id,
                    Language = param.excursion.language,
                    ExTime = param.excursion.extime,
                    date = needDate
                });

                //получить id квоты
                if (ds.Tables[0].Rows.Count > 0)
                {
                    //проверить даты квоты
                    var row = ds.Tables[0].Rows[0];

                    var dateFrom = System.Convert.ToDateTime(row["datebeg"]);
                    var dateTo = System.Convert.ToDateTime(row["dateend"]);

                    //если ровно день, сделать апдейт
                    if (dateFrom == dateTo)
                    {
                        DatabaseOperationProvider.Query("update [exlimit] set capacity = @capacity where inc = @inc ", "limits", new
                        {
                            inc = Convert.ToInt32( row["inc"]),
                            capacity = param.excursion.pax.adult
                        });
                    }
                    else
                    {
                        if (needDate == dateFrom)
                        {
                            DatabaseOperationProvider.Query("update [exlimit] set datebeg = @datebeg where inc = @inc ", "limits", new
                            {
                                inc = Convert.ToInt32( row["inc"]),
                                datebeg = dateFrom.AddDays(1)
                            });
                        }
                        else if (needDate == dateTo)
                        {
                            DatabaseOperationProvider.Query("update [exlimit] set dateend = @dateend where inc = @inc ", "limits", new
                            {
                                inc = Convert.ToInt32(row["inc"]),
                                dateend = dateTo.AddDays(-1)
                            });
                        }
                        else
                        { //если не ровно, разбить период и добавить новую
                            DatabaseOperationProvider.Query("update [exlimit] set dateend = @dateend where inc = @inc ", "limits", new
                            {
                                inc = Convert.ToInt32(row["inc"]),
                                dateend = needDate.AddDays(-1)
                            });

                            DatabaseOperationProvider.Query("insert into [exlimit] ([excurs],[language],[region],[datebeg],[dateend],[capacity],[author], " +
                                                                                   " [editor],[adate],[edate],[extime],[days],[note]) " +
                                                                                   " values(@excurs,@language,@region,@datebeg,@dateend,@capacity, " +
                                                                                           "@author,@editor,@adate,@edate,@extime,@days,@note) ", "limits", new
                                                                                           {
                                                                                               excurs = row.ReadInt("excurs"),
                                                                                               language = row.ReadInt("language"),
                                                                                               region = row.ReadNullableInt("region"),
                                                                                               datebeg = needDate.AddDays(1),
                                                                                               dateend = row.ReadDateTime("dateend"),
                                                                                               capacity = row.ReadInt("capacity"),
                                                                                               author = row.ReadNullableInt("author"),
                                                                                               editor = row.ReadNullableInt("editor"),
                                                                                               adate = row.ReadNullableDateTime("adate"),
                                                                                               edate = row.ReadNullableDateTime("edate"),
                                                                                               extime = row.ReadNullableInt("extime"),
                                                                                               days = row.ReadNullableString("days"),
                                                                                               note = row.ReadNullableString("note")
                                                                                           });
                        }

                        DatabaseOperationProvider.Query("insert into [exlimit] ([excurs],[language],[region],[datebeg],[dateend],[capacity],[author], " +
                                                                                    " [editor],[adate],[edate],[extime],[days],[note]) " +
                                                                                    " values(@excurs,@language,@region,@datebeg,@dateend,@capacity, " +
                                                                                            "@author,@editor,@adate,@edate,@extime,@days,@note) ", "limits", new
                                                                                            {
                                                                                                excurs = row.ReadInt("excurs"),
                                                                                                language = row.ReadInt("language"),
                                                                                                region = row.ReadNullableInt("region"),
                                                                                                datebeg = needDate,
                                                                                                dateend = needDate,
                                                                                                capacity = param.excursion.pax.adult,
                                                                                                author = row.ReadNullableInt("author"),
                                                                                                editor = row.ReadNullableInt("editor"),
                                                                                                adate = row.ReadNullableDateTime("adate"),
                                                                                                edate = row.ReadNullableDateTime("edate"),
                                                                                                extime = row.ReadNullableInt("extime"),
                                                                                                days = row.ReadNullableString("days"),
                                                                                                note = row.ReadNullableString("note")
                                                                                            });
                    }
                }
                else
                {//если нет, просто добавить
                    DatabaseOperationProvider.Query("insert into [exlimit] ([excurs],[language],[datebeg],[dateend],[capacity], [extime]) " +
                                                                                " values(@excurs,@language,@datebeg,@dateend,@capacity, @extime )"
                                                                                        , "limits", new
                                                                                        {
                                                                                            excurs = param.excursion.id,
                                                                                            language = param.excursion.language,
                                                                                            datebeg = needDate,
                                                                                            dateend = needDate,
                                                                                            capacity = param.excursion.pax.adult,
                                                                                            extime = param.excursion.extime
                                                                                        });
                }


                if (param.excursion != null)
                {
                    result = base.Json(new
                    {
                        ok = true
                    });
                    return result;
                }
                else
                {
                    errors.Add(ExcursionStrings.Get("ErrorInvalidParams"));
                }
                result = base.Json(new
                {
                    errormessages = errors.ToArray()
                });

            }
            catch (System.Exception ex)
            {
                errors.Add(ex.Message);
                result = base.Json(new
                {
                    errormessages = errors.ToArray()
                });

            }
            return result;
        }

        [AllowAnonymous, HttpGet, ActionName("addexcursion")]
        public ActionResult AddExcursion(ExcursionIndexWebParam param)
        {
            if (!WebSecurity.IsAuthenticated)
            {
                string str = base.Url.RouteUrl(base.Request.QueryStringAsRouteValues());
                return base.RedirectToAction("login", "account", new { returnUrl = str });
            }

            AddExcursionContext context = new AddExcursionContext();
            context.Regions = PartnerProvider.GetPartnerRegions(Api.PartnerExcursionController.GetProviderId(WebSecurity.CurrentUserId));
            context.Languages = PartnerProvider.GetPartnerLangs();
            return base.View(context);
        }

        public ActionResult Index(ExcursionIndexWebParam param)
        {
            if (!WebSecurity.IsAuthenticated)
            {
                string str = base.Url.RouteUrl(base.Request.QueryStringAsRouteValues());
                return base.RedirectToAction("login", "account", new { returnUrl = str });
            }
            int userId = WebSecurity.CurrentUserId;
            string userName = WebSecurity.CurrentUserName;

            if (param != null && param.visualtheme != null)
            {
                new VisualThemeManager(this).SafeSetThemeName(param.visualtheme);
            }
            ExcursionIndexContext context = new ExcursionIndexContext();
            context.PartnerAlias = ((param.PartnerAlias != null) ? param.PartnerAlias : Settings.ExcursionDefaultPartnerAlias);
            if (string.IsNullOrEmpty(context.PartnerAlias))
            {
                throw new System.ArgumentException("partner alias is not specified");
            }
            context.StartPointAlias = param.StartPointAlias;
            context.ExcursionDate = System.DateTime.Today.Date.AddDays((double)Settings.ExcursionDefaultDate);

            if (param.ShowCommand != null)
            {
                context.NavigateState = new ExcursionIndexNavigateCommand();
                if (param.ShowCommand.ToLower() == "search")
                {
                    if (param.SearchText != null || param.Categories != null || param.Destinations != null || param.ExcursionLanguages != null)
                    {
                        context.NavigateState.Cmd = "search";
                        context.NavigateState.Options = new ExcursionIndexNavigateOptions
                        {
                            text = param.SearchText,
                            categories = param.Categories,
                            destinations = param.Destinations,
                            languages = param.ExcursionLanguages
                        };
                    }
                }
                else
                {
                    if (param.ShowCommand.ToLower() == "description")
                    {
                        if (param.Excursion.HasValue)
                        {
                            context.NavigateState.Cmd = "description";
                            context.NavigateState.Options = new ExcursionIndexNavigateOptions
                            {
                                excursion = param.Excursion
                            };
                        }
                    }
                }
            }
            return base.View(context);
        }

        [AllowAnonymous, HttpGet, ActionName("editlist")]
        public ActionResult EditList(ExcursionIndexWebParam param)
        {
            if (!WebSecurity.IsAuthenticated)
            {
                string str = base.Url.RouteUrl(base.Request.QueryStringAsRouteValues());
                return base.RedirectToAction("login", "account", new { returnUrl = str });
            }
            int userId = WebSecurity.CurrentUserId;
            string userName = WebSecurity.CurrentUserName;

            if (param != null && param.visualtheme != null)
            {
                new VisualThemeManager(this).SafeSetThemeName(param.visualtheme);
            }
            ExcursionIndexContext context = new ExcursionIndexContext();
            context.PartnerAlias = ((param.PartnerAlias != null) ? param.PartnerAlias : Settings.ExcursionDefaultPartnerAlias);
            if (string.IsNullOrEmpty(context.PartnerAlias))
            {
                throw new System.ArgumentException("partner alias is not specified");
            }
            context.StartPointAlias = param.StartPointAlias;
            context.ExcursionDate = System.DateTime.Today.Date.AddDays((double)Settings.ExcursionDefaultDate);

            if (param.ShowCommand != null)
            {
                context.NavigateState = new ExcursionIndexNavigateCommand();
                if (param.ShowCommand.ToLower() == "search")
                {
                    if (param.SearchText != null || param.Categories != null || param.Destinations != null || param.ExcursionLanguages != null)
                    {
                        context.NavigateState.Cmd = "search";
                        context.NavigateState.Options = new ExcursionIndexNavigateOptions
                        {
                            text = param.SearchText,
                            categories = param.Categories,
                            destinations = param.Destinations,
                            languages = param.ExcursionLanguages
                        };
                    }
                }
                else
                {
                    if (param.ShowCommand.ToLower() == "description")
                    {
                        if (param.Excursion.HasValue)
                        {
                            context.NavigateState.Cmd = "description";
                            context.NavigateState.Options = new ExcursionIndexNavigateOptions
                            {
                                excursion = param.Excursion
                            };
                        }
                    }
                }
            }
            return base.View(context);
        }

        [AllowAnonymous, HttpGet, ActionName("editexcursion")]
        public ActionResult EditExcursion(ExcursionIndexWebParam param, int? id)
        {
            //TODO !! выводить все экскурсии, в том числе без цен и неактивные
            if (!WebSecurity.IsAuthenticated)
            {
                string str = base.Url.RouteUrl(base.Request.QueryStringAsRouteValues());
                return base.RedirectToAction("login", "account", new { returnUrl = str });
            }

            EditExcursionContext context = new EditExcursionContext();

            context.Regions = PartnerProvider.GetPartnerRegions(Api.PartnerExcursionController.GetProviderId(WebSecurity.CurrentUserId));
            context.Languages = PartnerProvider.GetPartnerLangs();

            ExcursionProvider.GetExcursionTexts(id.Value,
                                                out context.Names, 
                                                out context.Route, 
                                                out context.Types,
                                                out context.Region,
                                                out context.Descriptions);

            context.OldPhotos = ExcursionProvider.GetExcursionOldPhotos(id.Value);
            context.OldPrices = ExcursionProvider.GetExcursionOldPrices(id.Value);

            return base.View(context);
        }

        [ActionName("addcart"), HttpPost]
        public JsonResult AddCart(ExcursionAddWebParam param)
        {
            if (param == null)
            {
                throw new System.ArgumentNullException("param");
            }
            System.Collections.Generic.List<string> errors = new System.Collections.Generic.List<string>();
            JsonResult result;
            if (param.excursion != null)
            {
                if (param.excursion.pax != null && param.excursion.pax.adult + param.excursion.pax.child > 0)
                {
                    using (ShoppingCart cart = ShoppingCart.CreateFromSession(base.Session))
                    {
                        cart.Orders.Add(new BookingOrder
                        {
                            orderid = System.Guid.NewGuid().ToString(),
                            excursion = param.excursion
                        });
                    }
                    result = base.Json(new
                    {
                        ok = true
                    });
                    return result;
                }
                errors.Add(ExcursionStrings.Get("ErrorGuestCount"));
            }
            else
            {
                errors.Add(ExcursionStrings.Get("ErrorInvalidParams"));
            }
            result = base.Json(new
            {
                errormessages = errors.ToArray()
            });
            return result;
        }
    }
}
