using GuestService.Data;
using GuestService.Models;
using GuestService.Models.Guide;
using GuestService.Resources;
using Sm.System.Exceptions;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
namespace GuestService.Controllers.Api
{
	public class GuideController : ApiController
	{
		[ActionName("hotelguide"), HttpGet]
		public HotelGuideResult HotelGuide([FromUri] HotelGuideParam param)
		{
			if (param == null)
			{
				throw new System.ArgumentNullException("param");
			}
			if (!param.Hotel.HasValue)
			{
				throw new ArgumentNullExceptionWithCode(107, "h");
			}
			if (!param.PeriodBegin.HasValue)
			{
				throw new ArgumentNullExceptionWithCode(108, "pb");
			}
			if (!param.PeriodEnd.HasValue)
			{
				throw new ArgumentNullExceptionWithCode(109, "pe");
			}
			return new HotelGuideResult
			{
				hotel = CatalogProvider.GetHotelDescription(param.Language, param.Hotel.Value),
				guides = GuideProvider.GetHotelGuides(param.Language, param.Hotel.Value, param.PeriodBegin.Value, param.PeriodEnd.Value)
			};
		}
		[ActionName("photo"), HttpGet]
		public HttpResponseMessage Photo(int id, [FromUri] ImageParam param)
		{
			HttpResponseMessage response = new HttpResponseMessage();
			Image image = GuideProvider.GetGuideImage(id);
			ImageFormatter formatter = new ImageFormatter(image, Pictures.GuideNoPhoto);
			param.ApplyFormat(formatter);
			System.IO.Stream stream = formatter.CreateStream();
			if (stream != null)
			{
				response.Content = new StreamContent(stream);
				response.Content.Headers.ContentType = new MediaTypeHeaderValue(formatter.MediaType);
			}
			else
			{
				response.StatusCode = HttpStatusCode.NotFound;
			}
			return response;
		}
	}
}
