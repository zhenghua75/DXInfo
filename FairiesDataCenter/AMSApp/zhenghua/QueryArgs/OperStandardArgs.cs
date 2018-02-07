
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	OperStandardArgs.cs
* 作者:	     郑华
* 创建日期:    2008-10-3
* 功能描述:    操作标准表

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：操作标准表查询参数类
	/// </summary>
	public class OperStandardArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbOperStandard";
				
		/// <summary>
		/// 产品编码
		/// </summary>
		public QueryConditionField cnvcProductCode = new QueryConditionField("cnvcProductCode");

		/// <summary>
		/// 操作序号
		/// </summary>
		public QueryConditionField cnnSort = new QueryConditionField("cnnSort");

		/// <summary>
		/// 操作标准
		/// </summary>
		public QueryConditionField cnvcStandard = new QueryConditionField("cnvcStandard");

		/// <summary>
		/// 关键控制点
		/// </summary>
		public QueryConditionField cnvcKey = new QueryConditionField("cnvcKey");
	}	
}
