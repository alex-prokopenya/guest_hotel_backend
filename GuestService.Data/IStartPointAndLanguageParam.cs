using System;
namespace GuestService.Data
{
	public interface IStartPointAndLanguageParam
	{
		int? sp
		{
			get;
			set;
		}
		int? StartPoint
		{
			get;
		}
		string spa
		{
			get;
			set;
		}
		string StartPointAlias
		{
			get;
		}
		string ln
		{
			get;
			set;
		}
		string Language
		{
			get;
		}
	}
}
