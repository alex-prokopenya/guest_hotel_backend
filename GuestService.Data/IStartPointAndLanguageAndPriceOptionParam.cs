using System;
namespace GuestService.Data
{
	public interface IStartPointAndLanguageAndPriceOptionParam : IStartPointAndLanguageParam
	{
		bool? wp
		{
			get;
			set;
		}
		bool WithoutPrice
		{
			get;
		}
	}
}
