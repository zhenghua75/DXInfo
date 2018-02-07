
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	ProduceDetailReduceArgs.cs
* 作者:	     郑华
* 创建日期:    2008-10-12
* 功能描述:    生产计划产品细节减单表

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：生产计划产品细节减单表查询参数类
	/// </summary>
	public class ProduceDetailReduceArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbProduceDetailReduce";
				
		/// <summary>
		/// 生产流水
		/// </summary>
		public QueryConditionField cnnProduceSerialNo = new QueryConditionField("cnnProduceSerialNo");

		/// <summary>
		/// 减单流水
		/// </summary>
		public QueryConditionField cnnReduceSerialNo = new QueryConditionField("cnnReduceSerialNo");

		/// <summary>
		/// 产品编码
		/// </summary>
		public QueryConditionField cnvcCode = new QueryConditionField("cnvcCode");

		/// <summary>
		/// 产品名称
		/// </summary>
		public QueryConditionField cnvcName = new QueryConditionField("cnvcName");

		/// <summary>
		/// 单位
		/// </summary>
		public QueryConditionField cnvcUnit = new QueryConditionField("cnvcUnit");

		/// <summary>
		/// 数量
		/// </summary>
		public QueryConditionField cnnCount = new QueryConditionField("cnnCount");

		/// <summary>
		/// 状态
		/// </summary>
		public QueryConditionField cnvcState = new QueryConditionField("cnvcState");
	}	
}
