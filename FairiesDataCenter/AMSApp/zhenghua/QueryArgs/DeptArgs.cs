
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	DeptArgs.cs
* 作者:	     郑华
* 创建日期:    2008-10-30
* 功能描述:    部门表

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：部门表查询参数类
	/// </summary>
	public class DeptArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbDept";
				
		/// <summary>
		/// 部门名称
		/// </summary>
		public QueryConditionField cnvcDeptName = new QueryConditionField("cnvcDeptName");

		/// <summary>
		/// 部门编码
		/// </summary>
		public QueryConditionField cnvcDeptID = new QueryConditionField("cnvcDeptID");

		/// <summary>
		/// 部门类型
		/// </summary>
		public QueryConditionField cnvcDeptType = new QueryConditionField("cnvcDeptType");

		/// <summary>
		/// 上级部门编码
		/// </summary>
		public QueryConditionField cnvcParentDeptID = new QueryConditionField("cnvcParentDeptID");

		/// <summary>
		/// 描述
		/// </summary>
		public QueryConditionField cnvcComments = new QueryConditionField("cnvcComments");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField cnnPriority = new QueryConditionField("cnnPriority");
	}	
}
