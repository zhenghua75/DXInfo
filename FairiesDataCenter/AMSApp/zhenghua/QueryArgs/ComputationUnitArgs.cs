
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:    	ComputationUnitArgs.cs
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
	public class ComputationUnitArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbComputationUnit";
				
		/// <summary>
		/// 计量单位编码
		/// </summary>
		public QueryConditionField cnvcComunitCode = new QueryConditionField("cnvcComunitCode");

		/// <summary>
		/// 计量单位名称
		/// </summary>
		public QueryConditionField cnvcComUnitName = new QueryConditionField("cnvcComUnitName");

		/// <summary>
		/// 计量单位组编码
		/// </summary>
		public QueryConditionField cnvcGroupCode = new QueryConditionField("cnvcGroupCode");

		/// <summary>
		/// 是否主计量单位
		/// </summary>
		public QueryConditionField cnbMainUnit = new QueryConditionField("cnbMainUnit");

		/// <summary>
		/// 换算率
		/// </summary>
		public QueryConditionField cniChangRate = new QueryConditionField("cniChangRate");
	}	
}
