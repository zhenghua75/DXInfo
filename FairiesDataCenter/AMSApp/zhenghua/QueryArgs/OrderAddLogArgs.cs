
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	OrderAddLogArgs.cs
* 作者:	     郑华
* 创建日期:    2008-10-7
* 功能描述:    订单加单流水表

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：订单加单流水表查询参数类
	/// </summary>
	public class OrderAddLogArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbOrderAddLog";
				
		/// <summary>
		/// 加单流水
		/// </summary>
		public QueryConditionField cnnAddSerialNo = new QueryConditionField("cnnAddSerialNo");

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
		/// 加单数量
		/// </summary>
		public QueryConditionField cnnAddCount = new QueryConditionField("cnnAddCount");

		/// <summary>
		/// 单位
		/// </summary>
		public QueryConditionField cnvcUnit = new QueryConditionField("cnvcUnit");

		/// <summary>
		/// 单价
		/// </summary>
		public QueryConditionField cnnPrice = new QueryConditionField("cnnPrice");

		/// <summary>
		/// 合计
		/// </summary>
		public QueryConditionField cnnSum = new QueryConditionField("cnnSum");

		/// <summary>
		/// 加单类型
		/// </summary>
		public QueryConditionField cnvcAddType = new QueryConditionField("cnvcAddType");

		/// <summary>
		/// 加单状态
		/// </summary>
		public QueryConditionField cnvcAddState = new QueryConditionField("cnvcAddState");

		/// <summary>
		/// 加单说明
		/// </summary>
		public QueryConditionField cnvcAddComments = new QueryConditionField("cnvcAddComments");

		/// <summary>
		/// 操作员
		/// </summary>
		public QueryConditionField cnvcOperID = new QueryConditionField("cnvcOperID");

		/// <summary>
		/// 操作时间
		/// </summary>
		public QueryConditionField cndOperDate = new QueryConditionField("cndOperDate");
	}	
}
