using System;
using System.Web;
namespace GuestService.Data
{
	[System.Serializable]
	public class CompleteOperation
	{
		private const string SessionStateName = "completeOperation";
		public string OperationId
		{
			get;
			set;
		}
		public System.DateTime? OperationResultTime
		{
			get;
			set;
		}
		public string OperationResultType
		{
			get;
			set;
		}
		public string OperationResultData
		{
			get;
			set;
		}
		public bool IsProgress
		{
			get
			{
				return this.OperationId != null;
			}
		}
		public bool HasResult
		{
			get
			{
				return this.OperationResultTime.HasValue;
			}
		}
		public static CompleteOperation CreateFromSession(HttpSessionStateBase session)
		{
            if (session == null)
            {
                throw new ArgumentNullException("session");
            }
            CompleteOperation operation = session["completeOperation"] as CompleteOperation;
            if (operation == null)
            {
                session["completeOperation"] = operation = new CompleteOperation();
            }
            return operation;
        }
		public void Start()
		{
			this.Clear();
			this.OperationId = CompleteOperationProvider.Start();
		}
		public bool IsFinished()
		{
			bool result2;
			if (this.OperationId == null)
			{
				if (!this.OperationResultTime.HasValue)
				{
					throw new System.Exception("opearion is not started");
				}
				result2 = true;
			}
			else
			{
				if (CompleteOperationProvider.IsFinished(this.OperationId))
				{
					CompleteOperationResult result = CompleteOperationProvider.GetResult(this.OperationId);
					if (result != null)
					{
						this.OperationId = null;
						this.OperationResultTime = new System.DateTime?(result.ResultDate);
						this.OperationResultType = result.DataType;
						this.OperationResultData = result.Data;
						result2 = true;
						return result2;
					}
				}
				result2 = false;
			}
			return result2;
		}
		public void Clear()
		{
			this.OperationId = null;
			this.OperationResultTime = null;
			this.OperationResultType = null;
			this.OperationResultData = null;
		}
	}
}
