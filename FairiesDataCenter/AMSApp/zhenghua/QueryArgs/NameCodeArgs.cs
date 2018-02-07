
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	NameCodeArgs.cs
* 作者:	     郑华
* 创建日期:    2008-10-10
* 功能描述:    代码表

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：代码表查询参数类
	/// </summary>
	public class NameCodeArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbNameCode";
				
		/// <summary>
		/// 类型
		/// </summary>
		public QueryConditionField cnvcType = new QueryConditionField("cnvcType");

		/// <summary>
		/// 代码
		/// </summary>
		public QueryConditionField cnvcCode = new QueryConditionField("cnvcCode");

		/// <summary>
		/// 名称
		/// </summary>
		public QueryConditionField cnvcName = new QueryConditionField("cnvcName");

		/// <summary>
		/// 描述
		/// </summary>
		public QueryConditionField cnvcComments = new QueryConditionField("cnvcComments");
	}	
}
