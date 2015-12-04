using GuestService.Data.Survey;
using System;
using System.Collections.Generic;
using System.Linq;
namespace GuestService.Data
{
	public class ExcursionSurveyNote
	{
		public string guestnameprefix
		{
			get;
			set;
		}
		public string guestname
		{
			get;
			set;
		}
		public string language
		{
			get;
			set;
		}
		public string completedate
		{
			get;
			set;
		}
		public string positivenote
		{
			get;
			set;
		}
		public string negativenote
		{
			get;
			set;
		}
		public static System.Collections.Generic.List<ExcursionSurveyNote> Create(System.Collections.Generic.List<SurveyNote> list)
		{
			System.Collections.Generic.List<ExcursionSurveyNote> result = null;
			if (list != null && list.Count > 0)
			{
				result = new System.Collections.Generic.List<ExcursionSurveyNote>();
				foreach (SurveyNote item in list)
				{
					if (item.Notes != null && item.Notes.Count > 0)
					{
						ExcursionSurveyNote note = new ExcursionSurveyNote();
						if (!string.IsNullOrEmpty(item.ParticipantName))
						{
							note.guestnameprefix = item.ParticipantPrefix;
							note.guestname = item.ParticipantName;
						}
						note.language = item.Language;
						note.completedate = item.CompleteDate.ToString("dd.MM.yyyy");
						note.positivenote = string.Join("; ", (
							from m in item.Notes
							where m.Category == "PR"
							select m.Note).ToList<string>());
						note.negativenote = string.Join("; ", (
							from m in item.Notes
							where m.Category == "NR"
							select m.Note).ToList<string>());
						result.Add(note);
					}
				}
			}
			return result;
		}
	}
}
