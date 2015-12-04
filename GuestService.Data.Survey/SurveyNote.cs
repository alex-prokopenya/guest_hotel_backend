using System;
using System.Collections.Generic;
namespace GuestService.Data.Survey
{
	public class SurveyNote
	{
		public int Invitation
		{
			get;
			set;
		}
		public System.DateTime CompleteDate
		{
			get;
			set;
		}
		public string Language
		{
			get;
			set;
		}
		public string ParticipantPrefix
		{
			get;
			set;
		}
		public string ParticipantName
		{
			get;
			set;
		}
		public System.Collections.Generic.List<SurveyNoteItem> Notes
		{
			get;
			set;
		}
	}
}
