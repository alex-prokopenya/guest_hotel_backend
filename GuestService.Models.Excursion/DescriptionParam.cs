using System;
namespace GuestService.Models.Excursion
{
	public class DescriptionParam : BaseApiParam
	{
		public int[] ex
		{
			get;
			set;
		}
		public int[] Excursions
		{
			get
			{
				return this.ex;
			}
		}
	}
}
