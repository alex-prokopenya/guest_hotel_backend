using System;
namespace GuestService.Models.Excursion
{
	public class EditImageParam : ImageParam
	{
		public int? i
		{
			get;
			set;
		}
		public int Index
		{
			get
			{
				return this.i.HasValue ? this.i.Value : 0;
			}
		}
	}
}
