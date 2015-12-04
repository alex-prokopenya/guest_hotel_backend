using GuestService.Data;
using GuestService.Models.Catalog;
using Sm.System.Exceptions;
using Sm.System.Mvc.Language;
using System;
using System.Collections.Generic;
using System.Web.Http;
namespace GuestService.Controllers.Api
{
	[HttpUrlLanguage]
	public class CatalogController : ApiController
	{
		[ActionName("geopoints"), HttpGet]
		public GeoCatalogObjectList GeoPoints([FromUri] GeoCatalogParam param)
		{
			if (param == null)
			{
				throw new System.ArgumentNullException("param");
			}
			if (string.IsNullOrEmpty(param.SearchText))
			{
				throw new ArgumentNullExceptionWithCode(101, "s");
			}
			System.Collections.Generic.List<GeoCatalogObject> result = CatalogProvider.GetGeoPoints(param.Language, param.SearchText);
			return new GeoCatalogObjectList(result);
		}
		[ActionName("geopointbyalias"), HttpGet]
		public GeoCatalogObject GeoPointByAlias([FromUri] GeoPointByAliasParam param)
		{
			if (param == null)
			{
				throw new System.ArgumentNullException("param");
			}
			if (string.IsNullOrEmpty(param.GeoPointAlias))
			{
				throw new ArgumentNullExceptionWithCode(106, "gpa");
			}
			return CatalogProvider.GetGeoPointByAlias(param.Language, param.GeoPointAlias);
		}
	}
}
