using Sm.System.Database;
using System;
using System.Data;
namespace GuestService.Data
{
	public static class CompleteOperationProvider
	{
		public static string Start()
		{
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_asyncOperationStart", "result", null);
			DataRowCollection rows = ds.Tables["result"].Rows;
			string result;
			if (rows.Count > 0)
			{
				result = rows[0].ReadNullableTrimmedString("id");
			}
			else
			{
				result = null;
			}
			return result;
		}
		public static bool IsFinished(string id)
		{
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_asyncOperationFinished", "result", new
			{
				id
			});
			DataRowCollection rows = ds.Tables["result"].Rows;
			return rows.Count > 0 && rows[0].ReadBoolean("finished");
		}
		public static void SetResult(string id, string dataType, string data)
		{
			DatabaseOperationProvider.ExecuteProcedure("up_asyncOperationSetResult", new
			{
				id = id,
				dtype = dataType,
				data = data
			});
		}
		public static CompleteOperationResult GetResult(string id)
		{
			DataSet ds = DatabaseOperationProvider.QueryProcedure("up_asyncOperationGetResult", "result", new
			{
				id
			});
			DataRowCollection rows = ds.Tables["result"].Rows;
			CompleteOperationResult result;
			if (rows.Count > 0)
			{
				DataRow row = rows[0];
				result = new CompleteOperationResult
				{
					Id = row.ReadNullableTrimmedString("id"),
					ResultDate = row.ReadUnspecifiedDateTime("rdate"),
					DataType = row.ReadNullableTrimmedString("dtype"),
					Data = row.ReadNullableString("data")
				};
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
