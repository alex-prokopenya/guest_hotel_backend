using System;
namespace GuestService.Data
{
	public class Airport
	{
		public int id
		{
			get;
			set;
		}
		public string alias
		{
			get;
			set;
		}
		public string name
		{
			get;
			set;
		}
		public Town town
		{
			get;
			set;
		}
	}
}
