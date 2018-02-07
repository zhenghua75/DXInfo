
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	MakeLogArgs.cs
* 作者:	     郑华
* 创建日期:    2008-10-12
* 功能描述:    制令单流水表

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：制令单流水表查询参数类
	/// </summary>
	public class MakeLogArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbMakeLog";
				
		/// <summary>
		/// 生产流水
		/// </summary>
		public QueryConditionField cnnProduceSerialNo = new QueryConditionField("cnnProduceSerialNo");

		/// <summary>
		/// 制令流水
		/// </summary>
		public QueryConditionField cnnMakeSerialNo = new QueryConditionField("cnnMakeSerialNo");

		/// <summary>
		/// 生产组
		/// </summary>
		public QueryConditionField cnvcGroup = new QueryConditionField("cnvcGroup");

		/// <summary>
		/// 制令单名称
		/// </summary>
		public QueryConditionField cnvcMakeName = new QueryConditionField("cnvcMakeName");

		/// <summary>
		/// 制令类型
		/// </summary>
		public QueryConditionField cnvcMakeType = new QueryConditionField("cnvcMakeType");

		/// <summary>
		/// 班次
		/// </summary>
		public QueryConditionField cnvcClass = new QueryConditionField("cnvcClass");

		/// <summary>
		/// 生控主管
		/// </summary>
		public QueryConditionField cnvcInChargeOperID = new QueryConditionField("cnvcInChargeOperID");

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
