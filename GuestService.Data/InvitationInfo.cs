using System;
namespace GuestService.Data
{
	public class InvitationInfo
	{
		public int Id
		{
			get;
			set;
		}
		public string ObjectType
		{
			get;
			set;
		}
		public int ObjectId
		{
			get;
			set;
		}
		public string ObjectName
		{
			get;
			set;
		}
		public string Language
		{
			get;
			set;
		}
		public string Data
		{
			get;
			set;
		}
		public System.DateTime CreateDate
		{
			get;
			set;
		}
		public string AccessCode
		{
			get;
			set;
		}
		public System.DateTime? AccessCodeExpired
		{
			get;
			set;
		}
		public System.DateTime? CompleteDate
		{
			get;
			set;
		}
		public bool Verified
		{
			get;
			set;
		}
		public string ShareCode
		{
			get;
			set;
		}
		public bool IsSurveyed
		{
			get;
			set;
		}
		public bool IsExpired
		{
			get;
			set;
		}
		public bool CanSurvey
		{
			get;
			set;
		}
		public bool IsShared
		{
			get;
			set;
		}
		public bool CanShare
		{
			get;
			set;
		}
	}
}
