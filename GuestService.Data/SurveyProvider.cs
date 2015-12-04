using GuestService.Data.Survey;
using Sm.System.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Linq;
namespace GuestService.Data
{
	public static class SurveyProvider
	{
		private class SurveyFactory
		{
			internal InvitationInfo InvitationInfo(DataRow row)
			{
				return new InvitationInfo
				{
					Id = row.ReadInt("invitation"),
					ObjectType = row.ReadNullableTrimmedString("objecttype"),
					ObjectId = row.ReadInt("objectid"),
					ObjectName = row.ReadNullableTrimmedString("objectname"),
					Language = row.ReadNullableTrimmedString("lang"),
					Data = row.ReadNullableTrimmedString("data"),
					CreateDate = row.ReadDateTime("createdate"),
					AccessCode = row.ReadNullableTrimmedString("accesscode"),
					AccessCodeExpired = row.ReadNullableDateTime("accesscodeexpdate"),
					CompleteDate = row.ReadNullableDateTime("completedate"),
					Verified = row.ReadBoolean("verified"),
					ShareCode = row.ReadNullableTrimmedString("sharecode"),
					IsSurveyed = row.ReadBoolean("is_surveyed"),
					IsExpired = row.ReadBoolean("is_expired"),
					CanSurvey = row.ReadBoolean("can_survey"),
					IsShared = row.ReadBoolean("is_shared"),
					CanShare = row.ReadBoolean("can_share")
				};
			}
			internal QuestionnaireGroup QuestionnaireGroup(DataRow row)
			{
				return new QuestionnaireGroup
				{
					Id = row.ReadInt("questiongroup"),
					Caption = row.ReadNullableTrimmedString("caption")
				};
			}
			internal QuestionnaireQuestion QuestionnaireQuestion(DataRow row)
			{
				return new QuestionnaireQuestion
				{
					Id = row.ReadInt("question"),
					Category = row.ReadNullableTrimmedString("questioncategory"),
					IsMultiple = row.ReadBoolean("ismultiple"),
					Text = row.ReadNullableTrimmedString("questiontext"),
					IsNote = row.ReadBoolean("isnote"),
					NoteCaption = row.ReadNullableTrimmedString("notecaption")
				};
			}
			internal QuestionnaireIssue QuestionnaireIssue(DataRow row)
			{
				return new QuestionnaireIssue
				{
					Id = row.ReadInt("issue"),
					Text = row.ReadNullableTrimmedString("issue_text")
				};
			}
			internal CharacteristicRank CharacteristicRank(DataRow row)
			{
				return new CharacteristicRank
				{
					Id = row.ReadInt("characteristic"),
					Name = row.ReadNullableTrimmedString("name"),
					Rank = row.ReadNullableDecimal("averagerank")
				};
			}
			internal ExcursionRank ExcursionRank(DataRow row)
			{
				return new ExcursionRank
				{
					Excursion = row.ReadInt("excursion"),
					AverageRank = row.ReadNullableDecimal("averagerank"),
					SurveyCount = row.ReadInt("surveycount", 0)
				};
			}
			internal ExcursionRankInfo ExcursionRankInfo(DataRow row)
			{
				return new ExcursionRankInfo
				{
					Excursion = row.ReadInt("excursion"),
					AverageRank = row.ReadNullableDecimal("averagerank"),
					SurveyCount = row.ReadInt("surveycount", 0)
				};
			}
			internal SurveyNote SurveyNote(DataRow row)
			{
				return new SurveyNote
				{
					Invitation = row.ReadInt("invitation"),
					CompleteDate = row.ReadDateTime("completedate"),
					Language = row.ReadNullableTrimmedString("lang"),
					ParticipantPrefix = row.ReadNullableTrimmedString("prefix"),
					ParticipantName = row.ReadNullableTrimmedString("name"),
					Notes = new System.Collections.Generic.List<SurveyNoteItem>()
				};
			}
			internal ExcursionInvitation ExcursionInvitation(DataRow row)
			{
				return new ExcursionInvitation
				{
					AccessCode = row.ReadNullableTrimmedString("accesscode"),
					ExpareDate = row.ReadNullableDateTime("accesscodeexpdate")
				};
			}
		}
		private static SurveyProvider.SurveyFactory factory = new SurveyProvider.SurveyFactory();
		public static InvitationInfo GetInvitationInfo(string accesscode)
		{
			DataSet ds = DatabaseOperationProvider.QueryProcedure("[rating].[up_getInvitationInfo]", "invitation", new
			{
				accesscode
			});
			return (
				from DataRow row in ds.Tables["invitation"].Rows
				select SurveyProvider.factory.InvitationInfo(row)).FirstOrDefault<InvitationInfo>();
		}
		public static System.Collections.Generic.List<QuestionnaireGroup> GetQuestionnaire(string objecttype, string language)
		{
			DataSet ds = DatabaseOperationProvider.QueryProcedure("[rating].[up_getQuestionnaire]", "groups,questions,issues", new
			{
				objecttype = objecttype,
				lang = language
			});
			System.Collections.Generic.List<QuestionnaireGroup> result = (
				from DataRow row in ds.Tables["groups"].Rows
				select SurveyProvider.factory.QuestionnaireGroup(row)).ToList<QuestionnaireGroup>();
			foreach (QuestionnaireGroup group in result)
			{
				group.Questions.AddRange(
					from DataRow row in ds.Tables["questions"].Rows
					where row.ReadInt("questiongroup") == @group.Id
					select SurveyProvider.factory.QuestionnaireQuestion(row));
				foreach (QuestionnaireQuestion question in group.Questions)
				{
					question.Issues.AddRange(
						from DataRow row in ds.Tables["issues"].Rows
						where row.ReadInt("questiongroup") == @group.Id && row.ReadInt("question") == question.Id
						select SurveyProvider.factory.QuestionnaireIssue(row));
				}
			}
			return result;
		}
		public static void SetSurveyResult(SurveyResultsModel survey)
		{
			if (survey == null)
			{
				throw new System.ArgumentNullException("survey");
			}
			XName arg_C4_0 = "surveyResult";
			object[] array = new object[2];
			array[0] = ((survey.guest == null) ? null : new XElement("guest", new object[]
			{
				new XElement("name", survey.guest.name),
				new XElement("sex", survey.guest.sex)
			}));
			object[] arg_C2_0 = array;
			int arg_C2_1 = 1;
			System.Collections.Generic.IEnumerable<XElement> arg_C2_2;
			if (survey.questions != null)
			{
				arg_C2_2 = survey.questions.Select(delegate(System.Collections.Generic.KeyValuePair<string, SurveyQuestionModel> q)
				{
					XName arg_105_0 = "question";
					object[] array2 = new object[4];
					array2[0] = new XAttribute("id", q.Key);
					array2[1] = (string.IsNullOrEmpty(q.Value.issue) ? null : new XElement("issue", new XAttribute("id", q.Value.issue)));
					object[] arg_D4_0 = array2;
					int arg_D4_1 = 2;
					System.Collections.Generic.IEnumerable<XElement> arg_D4_2;
					if (q.Value.marks != null)
					{
						arg_D4_2 = 
							from m in q.Value.marks
							where (m.Value ?? "").ToLower() == "on"
							select new XElement("issue", new XAttribute("id", m.Key));
					}
					else
					{
						arg_D4_2 = null;
					}
					arg_D4_0[arg_D4_1] = arg_D4_2;
					array2[3] = ((q.Value.note == null) ? null : new XElement("note", q.Value.note));
					return new XElement(arg_105_0, array2);
				});
			}
			else
			{
				arg_C2_2 = null;
			}
			arg_C2_0[arg_C2_1] = arg_C2_2;
			XElement xml = new XElement(arg_C4_0, array);
			DataSet result = DatabaseOperationProvider.QueryProcedure("[rating].[up_setSurveyResult]", "result", new
			{
				accesscode = survey.accesscode,
				data = xml
			});
		}
		public static System.Collections.Generic.Dictionary<int, ExcursionRank> GetExcursionsRanking(System.Collections.Generic.IList<int> excursions, string language)
		{
			XElement xml = new XElement("excursions", 
				from e in excursions
				select new XElement("excursion", new XAttribute("id", e)));
			System.Collections.Generic.Dictionary<int, ExcursionRank> result = new System.Collections.Generic.Dictionary<int, ExcursionRank>();
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getExcursionsRanking", "ranks", new
			{
				excursions = xml,
				lang = language
			});
			System.Collections.Generic.IEnumerable<ExcursionRank> ratings = 
				from DataRow row in ds.Tables["ranks"].Rows
				select SurveyProvider.factory.ExcursionRank(row);
			foreach (ExcursionRank rating in ratings)
			{
				if (rating.AverageRank.HasValue)
				{
					result[rating.Excursion] = rating;
				}
			}
			return result;
		}
		public static ExcursionRankInfo GetExcursionRanking(int excursionId, string language)
		{
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getExcursionRanking", "ranks,characteristics", new
			{
				excursion = excursionId,
				lang = language
			});
			ExcursionRankInfo result = (
				from DataRow row in ds.Tables["ranks"].Rows
				select SurveyProvider.factory.ExcursionRankInfo(row)).FirstOrDefault<ExcursionRankInfo>();
			result.Characteristics = (
				from DataRow row in ds.Tables["characteristics"].Rows
				select SurveyProvider.factory.CharacteristicRank(row)).ToList<CharacteristicRank>();
			return result;
		}
		public static System.Collections.Generic.List<SurveyNote> GetExcursionNotes(int excursionId)
		{
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_guest_getExcursionSurveyNotes", "note,items", new
			{
				excursion = excursionId
			});
			System.Collections.Generic.List<SurveyNote> result = (
				from DataRow row in ds.Tables["note"].Rows
				select SurveyProvider.factory.SurveyNote(row)).ToList<SurveyNote>();
			System.Collections.Generic.Dictionary<int, SurveyNote> notesDictionary = new System.Collections.Generic.Dictionary<int, SurveyNote>();
			result.ForEach(delegate(SurveyNote m)
			{
				notesDictionary.Add(m.Invitation, m);
			});
			SurveyNote cache = null;
			foreach (DataRow noteRow in ds.Tables["items"].Rows.Cast<DataRow>())
			{
				int invitation = noteRow.ReadInt("invitation");
				if (cache == null || cache.Invitation != invitation)
				{
					cache = notesDictionary[invitation];
				}
				cache.Notes.Add(new SurveyNoteItem
				{
					Category = noteRow.ReadNullableTrimmedString("id"),
					Note = noteRow.ReadNullableTrimmedString("note")
				});
			}
			return result;
		}
		public static ExcursionInvitation CreateInvitation(int id, string name, string language)
		{
			string xml = null;
			DataSet ds = DatabaseOperationProvider.QueryProcedure("rating.up_createInvitation", "invitation", new
			{
				objecttype = "EXCURSION",
				objectid = id,
				objectname = name,
				lang = language,
				exparedate = System.DateTime.Now.AddHours(3.0),
				data = xml
			});
			return (
				from DataRow row in ds.Tables["invitation"].Rows
				select SurveyProvider.factory.ExcursionInvitation(row)).FirstOrDefault<ExcursionInvitation>();
		}
	}
}
