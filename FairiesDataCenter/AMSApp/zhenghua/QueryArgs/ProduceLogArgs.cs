
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	ProduceLogArgs.cs
* 作者:	     郑华
* 创建日期:    2008-10-10
* 功能描述:    生产计划流水表

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：生产计划流水表查询参数类
	/// </summary>
	public class ProduceLogArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbProduceLog";
				
		/// <summary>
		/// 生产流水
		/// </summary>
		public QueryConditionField cnnProduceSerialNo = new QueryConditionField("cnnProduceSerialNo");

		/// <summary>
		/// 生产单位
		/// </summary>
		public QueryConditionField cnvcProduceDeptID = new QueryConditionField("cnvcProduceDeptID");

		/// <summary>
		/// 生产日期
		/// </summary>
		public QueryConditionField cndProduceDate = new QueryConditionField("cndProduceDate");

		/// <summary>
		/// 发货开始日期
		/// </summary>
		public QueryConditionField cndShipBeginDate = new QueryConditionField("cndShipBeginDate");

		/// <summary>
		/// 发货结束日期
		/// </summary>
		public QueryConditionField cndShipEndDate = new QueryConditionField("cndShipEndDate");

		/// <summary>
		/// 生产计划状态
		/// </summary>
		public QueryConditionField cnvcProduceState = new QueryConditionField("cnvcProduceState");

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
