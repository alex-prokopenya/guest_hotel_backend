using System;
using System.Collections.Generic;
namespace GuestService.Data.Survey
{
	public class SurveyResultsModel
	{
		public string accesscode
		{
			get;
			set;
		}
		public SurveyCustomerModel guest
		{
			get;
			set;
		}
		public System.Collections.Generic.Dictionary<string, SurveyQuestionModel> questions
		{
			get;
			private set;
		}
		public SurveyResultsModel()
		{
			this.questions = new System.Collections.Generic.Dictionary<string, SurveyQuestionModel>();
		}
	}
}
