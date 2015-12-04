using GuestService.Code;
using GuestService.Data;
using GuestService.Data.Survey;
using GuestService.Models.Booking;
using GuestService.Models.Excursion;
using GuestService.Resources;
using Sm.System.Mvc.Language;
using Sm.System.Mvc.Theme;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
namespace GuestService.Controllers.Html
{
	[HttpPreferences, WebSecurityInitializer, UrlLanguage]
	public class ExcursionController : BaseController
	{
		public ActionResult Index(ExcursionIndexWebParam param)
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
								excursion = param.Excursion,
								date = param.Date
							};
						}
					}
				}
			}
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
					int? ordercount = null;
					using (ShoppingCart cart = ShoppingCart.CreateFromSession(base.Session))
					{
						cart.Orders.Add(new BookingOrder
						{
							orderid = System.Guid.NewGuid().ToString(),
							excursion = param.excursion
						});
						ordercount = new int?(cart.Orders.Count);
					}
					result = base.Json(new
					{
						ok = true,
						ordercount = ordercount
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
		[ActionName("addrating"), HttpPost]
		public ActionResult AddRating(int? id)
		{
			ActionResult result;
			if (Settings.IsAddRankingEnabled && id.HasValue)
			{
				System.Collections.Generic.List<ExcursionDescription> excursions = ExcursionProvider.GetDescription(UrlLanguage.CurrentLanguage, new int[]
				{
					id.Value
				});
				if (excursions != null && excursions.Count == 1)
				{
					ExcursionInvitation invitation = SurveyProvider.CreateInvitation(id.Value, excursions[0].excursion.name, UrlLanguage.CurrentLanguage);
					if (invitation != null)
					{
						result = base.RedirectToAction("index", "survey", new
						{
							id = invitation.AccessCode
						});
						return result;
					}
				}
			}
			result = base.RedirectToAction("index");
			return result;
		}
	}
}
