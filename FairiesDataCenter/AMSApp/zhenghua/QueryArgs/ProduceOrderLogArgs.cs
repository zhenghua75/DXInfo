
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	ProduceOrderLogArgs.cs
* 作者:	     郑华
* 创建日期:    2008-10-10
* 功能描述:    生产流水订单流水对应表

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：生产流水订单流水对应表查询参数类
	/// </summary>
	public class ProduceOrderLogArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbProduceOrderLog";
				
		/// <summary>
		/// 生产流水
		/// </summary>
		public QueryConditionField cnvcProduceSerialNo = new QueryConditionField("cnvcProduceSerialNo");

		/// <summary>
		/// 订单流水
		/// </summary>
		public QueryConditionField cnnOrderSerialNo = new QueryConditionField("cnnOrderSerialNo");

		/// <summary>
		/// 下单类型
		/// </summary>
		public QueryConditionField cnvcType = new QueryConditionField("cnvcType");
	}	
}
