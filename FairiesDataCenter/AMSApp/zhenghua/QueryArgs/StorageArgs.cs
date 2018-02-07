
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	StorageArgs.cs
* 作者:	     郑华
* 创建日期:    2008-11-3
* 功能描述:    库存表

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：库存表查询参数类
	/// </summary>
	public class StorageArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbStorage";
				
		/// <summary>
		/// 仓库
		/// </summary>
		public QueryConditionField cnvcStorageDeptID = new QueryConditionField("cnvcStorageDeptID");

		/// <summary>
		/// 产品编码
		/// </summary>
		public QueryConditionField cnvcProductCode = new QueryConditionField("cnvcProductCode");

		/// <summary>
		/// 产品名称
		/// </summary>
		public QueryConditionField cnvcProductName = new QueryConditionField("cnvcProductName");

		/// <summary>
		/// 单位
		/// </summary>
		public QueryConditionField cnvcUnit = new QueryConditionField("cnvcUnit");

		/// <summary>
		/// 实际库存数量
		/// </summary>
		public QueryConditionField cnnCount = new QueryConditionField("cnnCount");

		/// <summary>
		/// 安全库存数量
		/// </summary>
		public QueryConditionField cnnSafeCount = new QueryConditionField("cnnSafeCount");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField cnnSafeUpCount = new QueryConditionField("cnnSafeUpCount");
	}	
}
