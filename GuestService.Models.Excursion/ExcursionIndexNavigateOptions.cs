using System;
namespace GuestService.Models.Excursion
{
	public class ExcursionIndexNavigateOptions
	{
		public string text
		{
			get;
			set;
		}
		public int[] categories
		{
			get;
			set;
		}
		public int[] destinations
		{
			get;
			set;
		}
		public int[] languages
		{
			get;
			set;
		}
		public string sortorder
		{
			get;
			set;
		}
		public int? excursion
		{
			get;
			set;
		}
		public System.DateTime? date
		{
			get;
			set;
		}
	}
}
