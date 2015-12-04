using GuestService.Data.Survey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
namespace GuestService.Data
{
	public class CatalogDescriptionExcursionRanking : CatalogExcursionRanking
	{
		[XmlArray("Ranks")]
		public System.Collections.Generic.List<CatalogDescriptionExcursionRankItem> ranks
		{
			get;
			private set;
		}
		public static CatalogDescriptionExcursionRanking Create(ExcursionRankInfo rank, string language)
		{
			CatalogDescriptionExcursionRanking result2;
			if (rank.SurveyCount > 0 && rank.AverageRank.HasValue)
			{
				CatalogDescriptionExcursionRanking result = new CatalogDescriptionExcursionRanking();
				result.count = rank.SurveyCount;
				result.average = (rank.AverageRank.HasValue ? new decimal?(System.Math.Round(rank.AverageRank.Value, 1, System.MidpointRounding.AwayFromZero)) : null);
				result.BuildAverageTitle(language);
				result.ranks = (
					from m in rank.Characteristics
					select new CatalogDescriptionExcursionRankItem
					{
						name = m.Name,
						rank = m.Rank.HasValue ? new decimal?(System.Math.Round(m.Rank.Value, 1, System.MidpointRounding.AwayFromZero)) : null
					}).ToList<CatalogDescriptionExcursionRankItem>();
				result2 = result;
			}
			else
			{
				result2 = null;
			}
			return result2;
		}
	}
}
