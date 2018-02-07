
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	AssignLogArgs.cs
* 作者:	     郑华
* 创建日期:    2008-10-13
* 功能描述:    分货流水表

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：分货流水表查询参数类
	/// </summary>
	public class AssignLogArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbAssignLog";
				
		/// <summary>
		/// 分货流水
		/// </summary>
		public QueryConditionField cnnAssignSerialNo = new QueryConditionField("cnnAssignSerialNo");

		/// <summary>
		/// 订单流水
		/// </summary>
		public QueryConditionField cnnOrderSerialNo = new QueryConditionField("cnnOrderSerialNo");

		/// <summary>
		/// 发货单位
		/// </summary>
		public QueryConditionField cnvcShipDeptID = new QueryConditionField("cnvcShipDeptID");

		/// <summary>
		/// 发货人
		/// </summary>
		public QueryConditionField cnvcShipOperID = new QueryConditionField("cnvcShipOperID");

		/// <summary>
		/// 收货单位
		/// </summary>
		public QueryConditionField cnvcReceiveDeptID = new QueryConditionField("cnvcReceiveDeptID");

		/// <summary>
		/// 收货人
		/// </summary>
		public QueryConditionField cnvcReceiveOperID = new QueryConditionField("cnvcReceiveOperID");

		/// <summary>
		/// 发货时间
		/// </summary>
		public QueryConditionField cndShipDate = new QueryConditionField("cndShipDate");

		/// <summary>
		/// 收货时间
		/// </summary>
		public QueryConditionField cndReceiveDate = new QueryConditionField("cndReceiveDate");

		/// <summary>
		/// 店务
		/// </summary>
		public QueryConditionField cnvcSalesroomOperID = new QueryConditionField("cnvcSalesroomOperID");

		/// <summary>
		/// 运输
		/// </summary>
		public QueryConditionField cnvcTransportOperID = new QueryConditionField("cnvcTransportOperID");

		/// <summary>
		/// 成品仓
		/// </summary>
		public QueryConditionField cnvcStorageOperID = new QueryConditionField("cnvcStorageOperID");

		/// <summary>
		/// 客户签收
		/// </summary>
		public QueryConditionField cnvcCustomerValidate = new QueryConditionField("cnvcCustomerValidate");

		/// <summary>
		/// 客户意见反馈
		/// </summary>
		public QueryConditionField cnvcCustomerIdea = new QueryConditionField("cnvcCustomerIdea");

		/// <summary>
		/// 操作员
		/// </summary>
		public QueryConditionField cnvcOperID = new QueryConditionField("cnvcOperID");

		/// <summary>
		/// 操作时间
		/// </summary>
		public QueryConditionField cndOperDate = new QueryConditionField("cndOperDate");

		/// <summary>
		/// 备注
		/// </summary>
		public QueryConditionField cnvcComments = new QueryConditionField("cnvcComments");
	}	
}
