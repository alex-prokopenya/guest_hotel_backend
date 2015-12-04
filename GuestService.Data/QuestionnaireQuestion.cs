using System;
using System.Collections.Generic;
namespace GuestService.Data
{
	public class QuestionnaireQuestion
	{
		public int Id
		{
			get;
			set;
		}
		public string Category
		{
			get;
			set;
		}
		public bool IsMultiple
		{
			get;
			set;
		}
		public string Text
		{
			get;
			set;
		}
		public bool IsNote
		{
			get;
			set;
		}
		public string NoteCaption
		{
			get;
			set;
		}
		public System.Collections.Generic.List<QuestionnaireIssue> Issues
		{
			get;
			private set;
		}
		public QuestionnaireQuestion()
		{
			this.Issues = new System.Collections.Generic.List<QuestionnaireIssue>();
		}
	}
}
