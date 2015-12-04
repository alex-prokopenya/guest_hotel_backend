using GuestService.Data;
using System;
namespace GuestService.Code
{
	public static class QuestionnaireExtension
	{
		public static bool ContainsQuestions(this QuestionnaireGroup group)
		{
			bool result;
			foreach (QuestionnaireQuestion question in group.Questions)
			{
				if (question.ContainsIssues() || question.IsNote)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}
		public static bool ContainsIssues(this QuestionnaireQuestion question)
		{
			return question.Issues.Count > 0;
		}
	}
}
