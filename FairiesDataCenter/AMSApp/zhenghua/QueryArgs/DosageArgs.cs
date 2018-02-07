
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	DosageArgs.cs
* 作者:	     郑华
* 创建日期:    2008-10-1
* 功能描述:    配料表 

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：配料表 查询参数类
	/// </summary>
	public class DosageArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbDosage";
				
		/// <summary>
		/// 产品编码
		/// </summary>
		public QueryConditionField cnvcProductCode = new QueryConditionField("cnvcProductCode");

		/// <summary>
		/// 产品类型
		/// </summary>
		public QueryConditionField cnvcProductType = new QueryConditionField("cnvcProductType");

		/// <summary>
		/// 原料编码
		/// </summary>
		public QueryConditionField cnvcCode = new QueryConditionField("cnvcCode");

		/// <summary>
		/// 原料名称
		/// </summary>
		public QueryConditionField cnvcName = new QueryConditionField("cnvcName");

		/// <summary>
		/// 单位
		/// </summary>
		public QueryConditionField cnvcUnit = new QueryConditionField("cnvcUnit");

		/// <summary>
		/// 用量
		/// </summary>
		public QueryConditionField cnnCount = new QueryConditionField("cnnCount");

		/// <summary>
		/// 价格
		/// </summary>
		public QueryConditionField cnnPrice = new QueryConditionField("cnnPrice");

		/// <summary>
		/// 成本
		/// </summary>
		public QueryConditionField cnnSum = new QueryConditionField("cnnSum");
	}	
}
