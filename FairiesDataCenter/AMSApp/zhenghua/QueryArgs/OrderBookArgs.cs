
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	OrderBookArgs.cs
* 作者:	     郑华
* 创建日期:    2008-10-4
* 功能描述:    订单表

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：订单表查询参数类
	/// </summary>
	public class OrderBookArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbOrderBook";
				
		/// <summary>
		/// 订单流水
		/// </summary>
		public QueryConditionField cnnOrderSerialNo = new QueryConditionField("cnnOrderSerialNo");

		/// <summary>
		/// 订单门市
		/// </summary>
		public QueryConditionField cnvcOrderDeptID = new QueryConditionField("cnvcOrderDeptID");

		/// <summary>
		/// 生产单位
		/// </summary>
		public QueryConditionField cnvcProduceDeptID = new QueryConditionField("cnvcProduceDeptID");

		/// <summary>
		/// 订单类型
		/// </summary>
		public QueryConditionField cnvcOrderType = new QueryConditionField("cnvcOrderType");

		/// <summary>
		/// 订单说明
		/// </summary>
		public QueryConditionField cnvcOrderComments = new QueryConditionField("cnvcOrderComments");

		/// <summary>
		/// 订单操作员
		/// </summary>
		public QueryConditionField cnvcOrderOperID = new QueryConditionField("cnvcOrderOperID");

		/// <summary>
		/// 订单时间
		/// </summary>
		public QueryConditionField cndOrderDate = new QueryConditionField("cndOrderDate");

		/// <summary>
		/// 发货日期
		/// </summary>
		public QueryConditionField cndShipDate = new QueryConditionField("cndShipDate");

		/// <summary>
		/// 客户姓名单位
		/// </summary>
		public QueryConditionField cnvcCustomName = new QueryConditionField("cnvcCustomName");

		/// <summary>
		/// 送货地址
		/// </summary>
		public QueryConditionField cnvcShipAddress = new QueryConditionField("cnvcShipAddress");

		/// <summary>
		/// 联系电话
		/// </summary>
		public QueryConditionField cnvcLinkPhone = new QueryConditionField("cnvcLinkPhone");

		/// <summary>
		/// 客人要求到货时间
		/// </summary>
		public QueryConditionField cndArrivedDate = new QueryConditionField("cndArrivedDate");

		/// <summary>
		/// 订单状态
		/// </summary>
		public QueryConditionField cnvcOrderState = new QueryConditionField("cnvcOrderState");
	}	
}
