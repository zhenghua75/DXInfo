
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	AssignDetailArgs.cs
* 作者:	     郑华
* 创建日期:    2008-10-14
* 功能描述:    分货流水细节表

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：分货流水细节表查询参数类
	/// </summary>
	public class AssignDetailArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbAssignDetail";
				
		/// <summary>
		/// 分货流水
		/// </summary>
		public QueryConditionField cnnAssignSerialNo = new QueryConditionField("cnnAssignSerialNo");

		/// <summary>
		/// 订单流水
		/// </summary>
		public QueryConditionField cnnOrderSerialNo = new QueryConditionField("cnnOrderSerialNo");

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
		/// 单价
		/// </summary>
		public QueryConditionField cnnPrice = new QueryConditionField("cnnPrice");

		/// <summary>
		/// 叫货数量
		/// </summary>
		public QueryConditionField cnnOrderCount = new QueryConditionField("cnnOrderCount");

		/// <summary>
		/// 实发数量
		/// </summary>
		public QueryConditionField cnnCount = new QueryConditionField("cnnCount");

		/// <summary>
		/// 金额
		/// </summary>
		public QueryConditionField cnnSum = new QueryConditionField("cnnSum");
	}	
}
