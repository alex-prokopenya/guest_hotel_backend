using System;
namespace GuestService.Data
{
	public class DepartureFlight
	{
		public int id
		{
			get;
			set;
		}
		public string name
		{
			get;
			set;
		}
		public Airport source
		{
			get;
			set;
		}
		public Airport target
		{
			get;
			set;
		}
		public System.DateTime? takeoff
		{
			get;
			set;
		}
		public System.TimeSpan? landingtime
		{
			get;
			set;
		}
	}
}
