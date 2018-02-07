
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	OrderSerialNoArgs.cs
* 作者:	     郑华
* 创建日期:    2008-10-4
* 功能描述:    订单流水生成表

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：订单流水生成表查询参数类
	/// </summary>
	public class OrderSerialNoArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbOrderSerialNo";
				
		/// <summary>
		/// 订单流水
		/// </summary>
		public QueryConditionField cnnSerialNo = new QueryConditionField("cnnSerialNo");

		/// <summary>
		/// 填充
		/// </summary>
		public QueryConditionField cnvcFill = new QueryConditionField("cnvcFill");
	}	
}
