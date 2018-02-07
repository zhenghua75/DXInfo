
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	StorageLogArgs.cs
* 作者:	     郑华
* 创建日期:    2008-11-3
* 功能描述:    库存日志表

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：库存日志表查询参数类
	/// </summary>
	public class StorageLogArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbStorageLog";
				
		/// <summary>
		/// 日志流水
		/// </summary>
		public QueryConditionField cnnSerialNo = new QueryConditionField("cnnSerialNo");

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
		/// 安全上限库存数量
		/// </summary>
		public QueryConditionField cnnSafeUpCount = new QueryConditionField("cnnSafeUpCount");

		/// <summary>
		/// 操作类型
		/// </summary>
		public QueryConditionField cnvcOperType = new QueryConditionField("cnvcOperType");

		/// <summary>
		/// 操作员
		/// </summary>
		public QueryConditionField cnvcOperID = new QueryConditionField("cnvcOperID");

		/// <summary>
		/// 操作日期
		/// </summary>
		public QueryConditionField cndOperDate = new QueryConditionField("cndOperDate");
	}	
}
