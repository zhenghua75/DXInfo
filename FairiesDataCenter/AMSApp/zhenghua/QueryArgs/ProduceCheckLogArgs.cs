
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	ProduceCheckLogArgs.cs
* 作者:	     郑华
* 创建日期:    2008-10-28
* 功能描述:    生产盘点流水表

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：生产盘点流水表查询参数类
	/// </summary>
	public class ProduceCheckLogArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbProduceCheckLog";
				
		/// <summary>
		/// 生产流水
		/// </summary>
		public QueryConditionField cnnProduceSerialNo = new QueryConditionField("cnnProduceSerialNo");

		/// <summary>
		/// 生产单位
		/// </summary>
		public QueryConditionField cnvcProduceDeptID = new QueryConditionField("cnvcProduceDeptID");

		/// <summary>
		/// 操作员
		/// </summary>
		public QueryConditionField cnvcOperID = new QueryConditionField("cnvcOperID");

		/// <summary>
		/// 操作时间
		/// </summary>
		public QueryConditionField cndOperDate = new QueryConditionField("cndOperDate");

		/// <summary>
		/// 产品名称
		/// </summary>
		public QueryConditionField cnvcName = new QueryConditionField("cnvcName");

		/// <summary>
		/// 产品编码
		/// </summary>
		public QueryConditionField cnvcCode = new QueryConditionField("cnvcCode");

		/// <summary>
		/// 单位
		/// </summary>
		public QueryConditionField cnvcUnit = new QueryConditionField("cnvcUnit");

		/// <summary>
		/// 订单量
		/// </summary>
		public QueryConditionField cnnOrderCount = new QueryConditionField("cnnOrderCount");

		/// <summary>
		/// 加单量
		/// </summary>
		public QueryConditionField cnnAddCount = new QueryConditionField("cnnAddCount");

		/// <summary>
		/// 减单量
		/// </summary>
		public QueryConditionField cnnReduceCount = new QueryConditionField("cnnReduceCount");

		/// <summary>
		/// 生产量
		/// </summary>
		public QueryConditionField cnnProduceCount = new QueryConditionField("cnnProduceCount");

		/// <summary>
		/// 盘点量
		/// </summary>
		public QueryConditionField cnnCheckCount = new QueryConditionField("cnnCheckCount");
	}	
}
