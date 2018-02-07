
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	BusiLogArgs.cs
* 作者:	     郑华
* 创建日期:    2008-9-29
* 功能描述:    业务日志

*                                                           Copyright(C) 2008 fightop
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：业务日志查询参数类
	/// </summary>
	public class BusiLogArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbBusiLog";
				
		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField iSerial = new QueryConditionField("iSerial");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField iAssID = new QueryConditionField("iAssID");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField vcCardID = new QueryConditionField("vcCardID");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField vcOperType = new QueryConditionField("vcOperType");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField vcOperName = new QueryConditionField("vcOperName");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField dtOperDate = new QueryConditionField("dtOperDate");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField vcComments = new QueryConditionField("vcComments");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField vcDeptID = new QueryConditionField("vcDeptID");
	}	
}
