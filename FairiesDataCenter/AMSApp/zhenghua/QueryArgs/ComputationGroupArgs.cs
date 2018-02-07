
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:    	ComputationGroupArgs.cs
* 作者:	     郑华
* 创建日期:    2010-3-6
* 功能描述:    计量单位组

*                                                           Copyright(C) 2010 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：计量单位组查询参数类
	/// </summary>
	public class ComputationGroupArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbComputationGroup";
				
		/// <summary>
		/// 计量单位组编码
		/// </summary>
		public QueryConditionField cnvcGroupCode = new QueryConditionField("cnvcGroupCode");

		/// <summary>
		/// 计量单位组名称
		/// </summary>
		public QueryConditionField cnvcGroupName = new QueryConditionField("cnvcGroupName");
	}	
}
