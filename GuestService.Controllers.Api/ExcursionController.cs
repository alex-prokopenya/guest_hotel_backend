using GuestService.Code;
using GuestService.Data;
using GuestService.Data.Survey;
using GuestService.Models;
using GuestService.Models.Excursion;
using GuestService.Resources;
using Sm.System.Exceptions;
using Sm.System.Mvc.Language;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Caching;
using System.Web.Http;
namespace GuestService.Controllers.Api
{
	[HttpUrlLanguage]
	public class ExcursionController : ApiController
	{
		[ActionName("search"), HttpGet]
		public SearchExcursionResult Search([FromUri] SearchParam param)
		{
			if (param == null)
			{
				throw new System.ArgumentNullException("param");
			}
			if (string.IsNullOrEmpty(param.SearchText))
			{
				throw new ArgumentNullExceptionWithCode(101, "s");
			}
			if (!param.StartPoint.HasValue && param.StartPointAlias != null)
			{
				param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
			}
			int limit = (param.SearchLimit.HasValue && param.SearchLimit.Value > 0) ? param.SearchLimit.Value : Settings.ExcursionGeographySearchLimit;
			return ExcursionProvider.SearchExcursionObjects(param.Language, param.StartPoint, param.SearchText, limit);
		}
		[ActionName("categories"), HttpGet]
		public CategoryWithGroupList Categories([FromUri] CategoryParam param)
		{
			if (param == null)
			{
				throw new System.ArgumentNullException("param");
			}
			if (!param.StartPoint.HasValue && param.StartPointAlias != null)
			{
				param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
			}
			return new CategoryWithGroupList(ExcursionProvider.GetCategories(param.Language, param.StartPoint));
		}
		[ActionName("categoriesbygroup"), HttpGet]
		public CategoryGroupWithCategoriesList CategoriesByGroup([FromUri] CategoryParam param)
		{
			if (param == null)
			{
				throw new System.ArgumentNullException("param");
			}
			if (!param.StartPoint.HasValue && param.StartPointAlias != null)
			{
				param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
			}
			return new CategoryGroupWithCategoriesList(ExcursionProvider.GetCategoriesByGroup(param.Language, param.StartPoint));
		}
		[ActionName("destinationsandcategorygroups"), HttpGet]
		public DestinationsAndCategoryGroupsResult DestinationsAndCategoryGroups([FromUri] DestinationAndCategoryParam param)
		{
			if (param == null)
			{
				throw new System.ArgumentNullException("param");
			}
			WebPartner partner = UserToolsProvider.GetPartner(param);
			if (!param.StartPoint.HasValue && param.StartPointAlias != null)
			{
				param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
			}
			DestinationsAndCategoryGroupsResult result = new DestinationsAndCategoryGroupsResult();
			FilterDetailsResult filterDetails = ExcursionController.GetCachedFilterDetails(param, partner);
			result.destinationstates = filterDetails.destinationstates;
			result.categorygroups = ExcursionProvider.GetCategoriesByGroup(param.Language, param.StartPoint);
			return result;
		}
		[ActionName("departures"), HttpGet]
		public DeparturesResult Departures([FromUri] DepartureParam param)
		{
			if (param == null)
			{
				throw new System.ArgumentNullException("param");
			}
			WebPartner partner = UserToolsProvider.GetPartner(param);
			if (!param.StartPoint.HasValue && param.StartPointAlias != null)
			{
				param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
			}
			string departureResultCacheKey = string.Format("cache_departuresResult[ln:{0}][p:{1}][sp:{2}][st:{3}][wp:{4}]", new object[]
			{
				param.Language,
				partner.id,
				param.StartPoint.HasValue ? param.StartPoint.ToString() : "-",
				param.DestinationState.HasValue ? param.DestinationState.ToString() : "-",
				param.WithoutPrice
			});
			DeparturesResult result = HttpContext.Current.Cache[departureResultCacheKey] as DeparturesResult;
			if (result == null || Settings.IsCacheDisabled)
			{
				System.Collections.Generic.List<CatalogExcursionMinPrice> excursions = ExcursionProvider.FindExcursions(param.Language, partner.id, null, null, null, param.StartPoint, null, null, null, param.DestinationState.HasValue ? new int[]
				{
					param.DestinationState.Value
				} : null, null, null, null, null, param.WithoutPrice);
				result = new DeparturesResult();
				result.departures = ExcursionProvider.BuildDepartureList(excursions);
				HttpContext.Current.Cache.Add(departureResultCacheKey, result, null, System.DateTime.Now.AddMinutes(10.0), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
			}
			return result;
		}
		[ActionName("categoryimage"), HttpGet]
		public HttpResponseMessage CategoryImage(int id, [FromUri] ImageParam param)
		{
			if (param == null)
			{
				throw new System.ArgumentNullException("param");
			}
			HttpResponseMessage response = new HttpResponseMessage();
			string imageCacheKey = string.Format("categoryImage[id:{0}][w:{1}][h:{2}][m:{3}][d:{4}]", new object[]
			{
				id,
				param.h,
				param.w,
				param.Mode,
				param.ShowDefault ?? true
			});
			ImageCacheItem cacheResult = null;
			if (cacheResult == null || Settings.IsCacheDisabled)
			{
				Image image = ExcursionProvider.GetCategoryImage(id);
				if (image == null && param.ShowDefault == false)
				{
					response.StatusCode = HttpStatusCode.NotFound;
				}
				else
				{
					ImageFormatter formatter = new ImageFormatter(image, Properties.Resources._78777 );
					formatter.Format = ((image != null) ? ImageFormat.Jpeg : ImageFormat.Png);
					param.ApplyFormat(formatter);
					System.IO.Stream stream = formatter.CreateStream();
					if (stream != null)
					{
						cacheResult = ImageCacheItem.Create(stream, formatter.MediaType);
						HttpContext.Current.Cache.Add(imageCacheKey, cacheResult, null, System.DateTime.Now.AddMinutes(10.0), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
						response.Content = new StreamContent(stream);
						response.Content.Headers.ContentType = new MediaTypeHeaderValue(formatter.MediaType);
						response.Headers.CacheControl = new CacheControlHeaderValue();
						response.Headers.CacheControl.Public = true;
						response.Headers.CacheControl.MaxAge = new System.TimeSpan?(System.TimeSpan.FromHours(1.0));
					}
					else
					{
						response.StatusCode = HttpStatusCode.NotFound;
					}
				}
			}
			else
			{
				response.Content = new StreamContent(cacheResult.CraeteStream());
				response.Content.Headers.ContentType = new MediaTypeHeaderValue(cacheResult.MediaType);
			}
			response.Headers.CacheControl = new CacheControlHeaderValue();
			response.Headers.CacheControl.Public = true;
			response.Headers.CacheControl.MaxAge = new System.TimeSpan?(System.TimeSpan.FromHours(1.0));
			return response;
		}
		[ActionName("catalog"), HttpGet]
		public CatalogResult Catalog([FromUri] CatalogParam param)
		{
			if (param == null)
			{
				throw new System.ArgumentNullException("param");
			}
			WebPartner partner = UserToolsProvider.GetPartner(param);
			if (!param.StartPoint.HasValue && param.StartPointAlias != null)
			{
				param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
			}
			ExcursionProvider.ExcursionSorting sorting = (!string.IsNullOrEmpty(param.SortOrder)) ? ((ExcursionProvider.ExcursionSorting)System.Enum.Parse(typeof(ExcursionProvider.ExcursionSorting), param.SortOrder)) : ExcursionProvider.ExcursionSorting.name;
			CatalogResult result = new CatalogResult();
			result.excursions = ExcursionProvider.FindExcursions(param.Language, partner.id, param.FirstDate, param.LastDate, param.SearchLimit, param.StartPoint, param.SearchText, param.Categories, param.Departures, (param.Destinations != null && param.Destinations.Length > 0) ? param.Destinations : (param.DestinationState.HasValue ? new int[]
			{
				param.DestinationState.Value
			} : null), param.ExcursionLanguages, param.MinDuration, param.MaxDuration, new ExcursionProvider.ExcursionSorting?(sorting), param.WithoutPrice);
			System.Collections.Generic.Dictionary<int, ExcursionRank> rankings = SurveyProvider.GetExcursionsRanking((
				from m in result.excursions
				select m.excursion.id).ToList<int>(), param.Language);
			foreach (CatalogExcursionMinPrice excursion in result.excursions)
			{
				ExcursionRank rank = null;
				if (rankings.TryGetValue(excursion.excursion.id, out rank))
				{
					excursion.ranking = CatalogExcursionRanking.Create(rank, param.Language);
				}
			}
			return result;
		}
		[ActionName("filters"), HttpGet]
		public FiltersResult Filters([FromUri] FiltersParam param)
		{
			if (param == null)
			{
				throw new System.ArgumentNullException("param");
			}
			WebPartner partner = UserToolsProvider.GetPartner(param);
			if (!param.StartPoint.HasValue && param.StartPointAlias != null)
			{
				param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
			}
			string filtersResultCacheKey = string.Format("cache_filterResult[ln:{0}][p:{1}][sp:{2}][st:{3}][wp:{4}]", new object[]
			{
				param.Language,
				partner.id,
				param.StartPoint.HasValue ? param.StartPoint.ToString() : "-",
				param.DestinationState.HasValue ? param.DestinationState.ToString() : "-",
				param.WithoutPrice
			});
			FiltersResult result = HttpContext.Current.Cache[filtersResultCacheKey] as FiltersResult;
			if (result == null || Settings.IsCacheDisabled)
			{
				result = new FiltersResult();
				System.Collections.Generic.List<CatalogExcursionMinPrice> excursions = ExcursionProvider.FindExcursions(param.Language, partner.id, null, null, null, param.StartPoint, null, null, null, param.DestinationState.HasValue ? new int[]
				{
					param.DestinationState.Value
				} : null, null, null, null, null, param.WithoutPrice);
				result.categorygroups = ExcursionProvider.BuildFilterCategories(excursions, null);
				result.departures = ExcursionProvider.BuildFilterDepartures(excursions, null);
				result.destinations = ExcursionProvider.BuildFilterDestinations(excursions, null);
				result.languages = ExcursionProvider.BuildFilterLanguages(excursions, null);
				result.durations = ExcursionProvider.BuildFilterDurations(excursions);
				HttpContext.Current.Cache.Add(filtersResultCacheKey, result, null, System.DateTime.Now.AddMinutes(10.0), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
			}
			return result;
		}
		[ActionName("filterdetails"), HttpGet]
		public FilterDetailsResult FilterDetails([FromUri] FiltersParam param)
		{
			if (param == null)
			{
				throw new System.ArgumentNullException("param");
			}
			WebPartner partner = UserToolsProvider.GetPartner(param);
			if (!param.StartPoint.HasValue && param.StartPointAlias != null)
			{
				param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
			}
			return ExcursionController.GetCachedFilterDetails(param, partner);
		}
		[ActionName("catalogimage"), HttpGet]
		public HttpResponseMessage CatalogImage(int id, [FromUri] CatalogImageParam param)
		{
			if (param == null)
			{
				throw new System.ArgumentNullException("param");
			}
			HttpResponseMessage response = new HttpResponseMessage();
			string imageCacheKey = string.Format("catalogImage[id:{0}][w:{1}][h:{2}][i:{3}][m:{4}][d:{5}]", new object[]
			{
				id,
				param.h,
				param.w,
				param.i,
				param.Mode,
				param.ShowDefault ?? true
			});
			ImageCacheItem cacheResult = HttpContext.Current.Cache[imageCacheKey] as ImageCacheItem;
			if (cacheResult == null || Settings.IsCacheDisabled)
			{
				Image image = ExcursionProvider.GetCatalogImage(id, param.Index);
				if (image == null && param.ShowDefault == false)
				{
					response.StatusCode = HttpStatusCode.NotFound;
				}
				else
				{
					ImageFormatter formatter = new ImageFormatter(image, Properties.Resources._78777);
					formatter.Format = ((image != null) ? ImageFormat.Jpeg : ImageFormat.Png);
					param.ApplyFormat(formatter);
					System.IO.Stream stream = formatter.CreateStream();
					if (stream != null)
					{
						cacheResult = ImageCacheItem.Create(stream, formatter.MediaType);
						HttpContext.Current.Cache.Add(imageCacheKey, cacheResult, null, System.DateTime.Now.AddMinutes(10.0), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
						response.Content = new StreamContent(stream);
						response.Content.Headers.ContentType = new MediaTypeHeaderValue(formatter.MediaType);
					}
					else
					{
						response.StatusCode = HttpStatusCode.NotFound;
					}
				}
			}
			else
			{
				response.Content = new StreamContent(cacheResult.CraeteStream());
				response.Content.Headers.ContentType = new MediaTypeHeaderValue(cacheResult.MediaType);
			}
			response.Headers.CacheControl = new CacheControlHeaderValue();
			response.Headers.CacheControl.Public = true;
			response.Headers.CacheControl.MaxAge = new System.TimeSpan?(System.TimeSpan.FromHours(1.0));
			return response;
		}
		[ActionName("price"), HttpGet]
		public ExcursionPriceList Price(int id, [FromUri] PriceParam param)
		{
			if (param == null)
			{
				throw new System.ArgumentNullException("param");
			}
			WebPartner partner = UserToolsProvider.GetPartner(param);
			if (!param.Date.HasValue)
			{
				throw new ArgumentNullExceptionWithCode(202, "date");
			}
			if (!param.StartPoint.HasValue && param.StartPointAlias != null)
			{
				param.sp = new int?(CatalogProvider.GetGeoPointIdByAlias(param.StartPointAlias));
			}
			ExcursionPriceList result;
			if (param.Date.Value.Date < System.DateTime.Today)
			{
				result = new ExcursionPriceList(new System.Collections.Generic.List<ExcursionPrice>());
			}
			else
			{
				System.Collections.Generic.List<ExcursionPrice> prices = ExcursionProvider.GetPrice(param.Language, partner.id, id, param.Date.Value, param.StartPoint);
				result = new ExcursionPriceList((
					from m in prices
					where !m.issaleclosed && !m.isstopsale && m.price != null
					select m).ToList<ExcursionPrice>());
			}
			return result;
		}
		[ActionName("dates"), HttpGet]
		public ExcursionDateList Dates(int id, [FromUri] DatesParam param)
		{
			if (param == null)
			{
				throw new System.ArgumentNullException("param");
			}
			WebPartner partner = UserToolsProvider.GetPartner(param);
			if (!param.FirstDate.HasValue)
			{
				throw new ArgumentNullExceptionWithCode(103, "firstadate");
			}
			if (!param.LastDate.HasValue)
			{
				throw new ArgumentNullExceptionWithCode(104, "lastdate");
			}
			return new ExcursionDateList(ExcursionProvider.GetDates(partner.id, id, param.FirstDate.Value, param.LastDate.Value));
		}
		[ActionName("description"), HttpGet]
		public ExcursionDescriptionList Description([FromUri] DescriptionParam param)
		{
			if (param == null)
			{
				throw new System.ArgumentNullException("param");
			}
			if (param.Excursions == null)
			{
				throw new ArgumentNullExceptionWithCode(105, "ex");
			}
			return new ExcursionDescriptionList(ExcursionProvider.GetDescription(param.Language, param.Excursions));
		}
		[ActionName("exdescription"), HttpGet]
		public ExcursionExtendedDescriptionList ExtendedDescription([FromUri] ExtendedDescriptionParam param)
		{
			if (param == null)
			{
				throw new System.ArgumentNullException("param");
			}
			if (param.Excursions == null)
			{
				throw new ArgumentNullExceptionWithCode(105, "ex");
			}
			WebPartner partner = UserToolsProvider.GetPartner(param);
			if (!param.FirstDate.HasValue)
			{
				param.fd = new System.DateTime?(System.DateTime.Now.Date);
			}
			if (!param.LastDate.HasValue)
			{
				param.ld = new System.DateTime?(param.FirstDate.Value.AddDays((double)Settings.ExcursionCheckAvailabilityDays));
			}
			ExcursionExtendedDescriptionList result = new ExcursionExtendedDescriptionList();
			System.Collections.Generic.List<ExcursionDescription> descriptions = ExcursionProvider.GetDescription(param.Language, param.Excursions);
			foreach (ExcursionDescription description in descriptions)
			{
				ExcursionExtendedDescription resultItem = new ExcursionExtendedDescription(description);
				if (description != null && description.excursion != null)
				{
					resultItem.categorygroups = ExcursionProvider.BuildDescriptionCategories(description.excursion);
					resultItem.excursiondates = ExcursionProvider.GetDates(partner.id, description.excursion.id, param.FirstDate.Value, param.LastDate.Value);
					resultItem.ranking = CatalogDescriptionExcursionRanking.Create(SurveyProvider.GetExcursionRanking(description.excursion.id, param.Language), param.Language);
					resultItem.surveynotes = ExcursionSurveyNote.Create(SurveyProvider.GetExcursionNotes(description.excursion.id));
				}
				result.Add(resultItem);
			}
			return result;
		}
		[ActionName("excursionpickuphotels"), HttpGet]
		public ExcursionPickupHotelsList ExcursionPickupHotels([FromUri] ExcursionPickupHotelsParam param)
		{
			if (param == null)
			{
				throw new System.ArgumentNullException("param");
			}
			System.Collections.Generic.List<ExcursionPickupHotel> result = ExcursionProvider.GetExcursionPickupHotels(param.Language, param.Excursion, new int?(param.ExcursionTime), param.DeparturePoints);
			return new ExcursionPickupHotelsList(result);
		}
		private static FilterDetailsResult GetCachedFilterDetails(IStartPointAndLanguageAndPriceOptionParam param, WebPartner partner)
		{
			string filtersResultCacheKey = string.Format("cache_filterDetails[ln:{0}][p:{1}][sp:{2}][wp:{3}]", new object[]
			{
				param.Language,
				partner.id,
				param.StartPoint.HasValue ? param.StartPoint.ToString() : "-",
				param.WithoutPrice
			});
			FilterDetailsResult result = HttpContext.Current.Cache[filtersResultCacheKey] as FilterDetailsResult;
			if (result == null || Settings.IsCacheDisabled)
			{
				result = new FilterDetailsResult();
				System.Collections.Generic.List<CatalogExcursionMinPrice> excursions = ExcursionProvider.FindExcursions(param.Language, partner.id, null, null, null, param.StartPoint, null, null, null, null, null, null, null, null, param.WithoutPrice);
				result.categorygroups = ExcursionProvider.BuildFilterCategories(excursions, null);
				result.departures = ExcursionProvider.BuildFilterDepartures(excursions, null);
				result.languages = ExcursionProvider.BuildFilterLanguages(excursions, null);
				result.durations = ExcursionProvider.BuildFilterDurations(excursions);
				System.Collections.Generic.List<CatalogFilterLocationItem> destinations = ExcursionProvider.BuildFilterDestinations(excursions, null);
				if (destinations != null)
				{
					result.destinations = new System.Collections.Generic.List<CatalogFilterLocationWithStateItem>();
					if (destinations.Count > 0)
					{
						ExcursionProvider.LoadStatesResult stateResult = ExcursionProvider.LoadStatesForPoints(param.Language, (
							from m in destinations
							select m.id).ToArray<int>());
						foreach (CatalogFilterLocationItem item in destinations)
						{
							int stateId;
							if (stateResult.Links.TryGetValue(item.id, out stateId))
							{
								result.destinations.Add(new CatalogFilterLocationWithStateItem(item, stateId.ToString()));
							}
							else
							{
								result.destinations.Add(new CatalogFilterLocationWithStateItem(item, null));
							}
						}
						result.destinationstates = stateResult.States;
					}
				}
				HttpContext.Current.Cache.Add(filtersResultCacheKey, result, null, System.DateTime.Now.AddMinutes(10.0), Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
			}
			return result;
		}
	}
}
