using System;
using System.Collections.Generic;
namespace GuestService.Data.Survey
{
	public class ExcursionRankInfo : ExcursionRank
	{
		public System.Collections.Generic.List<CharacteristicRank> Characteristics
		{
			get;
			set;
		}
	}
}
