
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	GroupMakeArgs.cs
* 作者:	     郑华
* 创建日期:    2008-10-23
* 功能描述:    生产组制令对应表

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：生产组制令对应表查询参数类
	/// </summary>
	public class GroupMakeArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbGroupMake";
				
		/// <summary>
		/// 产品类型
		/// </summary>
		public QueryConditionField cnvcProductType = new QueryConditionField("cnvcProductType");

		/// <summary>
		/// 生产组
		/// </summary>
		public QueryConditionField cnvcGroupCode = new QueryConditionField("cnvcGroupCode");

		/// <summary>
		/// 制令名称
		/// </summary>
		public QueryConditionField cnvcMakeName = new QueryConditionField("cnvcMakeName");
	}	
}
