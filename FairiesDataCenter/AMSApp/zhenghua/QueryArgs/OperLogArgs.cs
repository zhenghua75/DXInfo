
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	OperLogArgs.cs
* 作者:	     郑华
* 创建日期:    2008-10-10
* 功能描述:    操作日志表

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：操作日志表查询参数类
	/// </summary>
	public class OperLogArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbOperLog";
				
		/// <summary>
		/// 操作流水
		/// </summary>
		public QueryConditionField cnnOperSerialNo = new QueryConditionField("cnnOperSerialNo");

		/// <summary>
		/// 操作类型
		/// </summary>
		public QueryConditionField cnvcOperType = new QueryConditionField("cnvcOperType");

		/// <summary>
		/// 操作员
		/// </summary>
		public QueryConditionField cnvcOperID = new QueryConditionField("cnvcOperID");

		/// <summary>
		/// 部门
		/// </summary>
		public QueryConditionField cnvcDeptID = new QueryConditionField("cnvcDeptID");

		/// <summary>
		/// 操作时间
		/// </summary>
		public QueryConditionField cndOperDate = new QueryConditionField("cndOperDate");

		/// <summary>
		/// 描述
		/// </summary>
		public QueryConditionField cnvcComments = new QueryConditionField("cnvcComments");
	}	
}
