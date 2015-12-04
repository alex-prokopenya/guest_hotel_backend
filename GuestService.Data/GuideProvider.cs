using Sm.System.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
namespace GuestService.Data
{
	public static class GuideProvider
	{
		private class GuideFactory
		{
			internal HotelGuide HotelGuide(DataRow row)
			{
				return new HotelGuide
				{
					id = row.ReadInt("guide$inc"),
					name = row.ReadNullableTrimmedString("guide$name"),
					mobile = row.ReadNullableTrimmedString("guide$mobile")
				};
			}
			internal HotelGuideDurty HotelGuideDurty(DataRow row)
			{
				return new HotelGuideDurty
				{
					guideid = row.ReadInt("guide$inc"),
					workday = (row.IsNull("guide$datebeg") && row.IsNull("guide$dateend")) ? null : new DatePeriod
					{
						begin = row.ReadNullableUnspecifiedDateTime("guide$datebeg"),
						end = row.ReadNullableUnspecifiedDateTime("guide$dateend")
					},
					worktime = (row.IsNull("guide$timebeg") && row.IsNull("guide$timeend")) ? null : new TimePeriod
					{
						begin = row.ReadNullableUnspecifiedTime("guide$timebeg"),
						end = row.ReadNullableUnspecifiedTime("guide$timeend")
					}
				};
			}
		}
		private static GuideProvider.GuideFactory factory = new GuideProvider.GuideFactory();
		public static System.Collections.Generic.List<HotelGuide> GetHotelGuides(string lang, int hotelId, System.DateTime begin, System.DateTime end)
		{
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getHotelGuides", "guides,duties", new
			{
				hotel = hotelId,
				periodBegin = begin,
				periodEnd = end
			});
			System.Collections.Generic.List<HotelGuide> result = (
				from DataRow m in ds.Tables["guides"].Rows
				select GuideProvider.factory.HotelGuide(m)).ToList<HotelGuide>();
			foreach (HotelGuide guide in result)
			{
				guide.durties = (
					from DataRow m in ds.Tables["duties"].Rows
					select GuideProvider.factory.HotelGuideDurty(m) into m
					where m.guideid == guide.id
					select m).ToList<HotelGuideDurty>();
			}
			return result;
		}
		public static Image GetGuideImage(int id)
		{
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getGuideImage", "image", new
			{
				id
			});
			DataRow row = ds.Tables["image"].Rows.Cast<DataRow>().FirstOrDefault<DataRow>();
			Image result;
			if (row != null && !row.IsNull("img$picture"))
			{
				result = Image.FromStream(new System.IO.MemoryStream((byte[])row["img$picture"]));
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
