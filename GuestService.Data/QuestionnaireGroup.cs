using System;
using System.Collections.Generic;
namespace GuestService.Data
{
	public class QuestionnaireGroup
	{
		public int Id
		{
			get;
			set;
		}
		public string Caption
		{
			get;
			set;
		}
		public System.Collections.Generic.List<QuestionnaireQuestion> Questions
		{
			get;
			private set;
		}
		public QuestionnaireGroup()
		{
			this.Questions = new System.Collections.Generic.List<QuestionnaireQuestion>();
		}
	}
}
