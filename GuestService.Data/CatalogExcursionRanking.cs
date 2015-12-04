using GuestService.Data.Survey;
using GuestService.Resources;
using System;
using System.Globalization;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class CatalogExcursionRanking
	{
		[XmlAttribute("average")]
		public decimal? average
		{
			get;
			set;
		}
		[XmlElement("count")]
		public int count
		{
			get;
			set;
		}
		[XmlAttribute("title")]
		public string title
		{
			get;
			set;
		}
		public static CatalogExcursionRanking Create(ExcursionRank rank, string language)
		{
			if (rank == null)
			{
				throw new System.ArgumentNullException("rank");
			}
			CatalogExcursionRanking result = new CatalogExcursionRanking();
			result.count = rank.SurveyCount;
			result.average = (rank.AverageRank.HasValue ? new decimal?(System.Math.Round(rank.AverageRank.Value, 1, System.MidpointRounding.AwayFromZero)) : null);
			result.BuildAverageTitle(language);
			return result;
		}
		protected void BuildAverageTitle(string language)
		{
			System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CreateSpecificCulture(language);
			if (this.average >= 9.5m)
			{
				this.title = ExcursionStrings.ResourceManager.GetString("Rank_1", culture);
			}
			else
			{
				if (this.average >= 9.0m)
				{
					this.title = ExcursionStrings.ResourceManager.GetString("Rank_2", culture);
				}
				else
				{
					if (this.average >= 8.0m)
					{
						this.title = ExcursionStrings.ResourceManager.GetString("Rank_3", culture);
					}
					else
					{
						if (this.average >= 7.0m)
						{
							this.title = ExcursionStrings.ResourceManager.GetString("Rank_4", culture);
						}
						else
						{
							this.title = string.Empty;
						}
					}
				}
			}
		}
	}
}
