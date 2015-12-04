using System;
using System.Collections.Generic;
namespace GuestService.Data.Survey
{
	public class SurveyQuestionModel
	{
		public string issue
		{
			get;
			set;
		}
		public System.Collections.Generic.Dictionary<string, string> marks
		{
			get;
			set;
		}
		public string note
		{
			get;
			set;
		}
	}
}
