
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	FormulaArgs.cs
* 作者:	     郑华
* 创建日期:    2008-9-29
* 功能描述:    配方表

*                                                           Copyright(C) 2008 fightop
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：配方表查询参数类
	/// </summary>
	public class FormulaArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbFormula";
				
		/// <summary>
		/// 产品编码
		/// </summary>
		public QueryConditionField cnvcProductCode = new QueryConditionField("cnvcProductCode");

		/// <summary>
		/// 产品名称
		/// </summary>
		public QueryConditionField cnvcProductName = new QueryConditionField("cnvcProductName");

		/// <summary>
		/// 产品类型
		/// </summary>
		public QueryConditionField cnvcProductType = new QueryConditionField("cnvcProductType");

		/// <summary>
		/// 产品类别
		/// </summary>
		public QueryConditionField cnvcProductClass = new QueryConditionField("cnvcProductClass");

		/// <summary>
		/// 产品图片
		/// </summary>
		public QueryConditionField cnbProductImage = new QueryConditionField("cnbProductImage");

		/// <summary>
		/// 原料成本合计
		/// </summary>
		public QueryConditionField cnnMaterialCostSum = new QueryConditionField("cnnMaterialCostSum");

		/// <summary>
		/// 材料成本合计
		/// </summary>
		public QueryConditionField cnnPackingCostSum = new QueryConditionField("cnnPackingCostSum");

		/// <summary>
		/// 成本合计
		/// </summary>
		public QueryConditionField cnnCostSum = new QueryConditionField("cnnCostSum");

		/// <summary>
		/// 单位
		/// </summary>
		public QueryConditionField cnvcUnit = new QueryConditionField("cnvcUnit");

		/// <summary>
		/// 份产数量
		/// </summary>
		public QueryConditionField cnnPortionCount = new QueryConditionField("cnnPortionCount");

		/// <summary>
		/// 份产单位
		/// </summary>
		public QueryConditionField cnvcPortionUnit = new QueryConditionField("cnvcPortionUnit");

		/// <summary>
		/// 口感
		/// </summary>
		public QueryConditionField cnvcFeel = new QueryConditionField("cnvcFeel");

		/// <summary>
		/// 组织
		/// </summary>
		public QueryConditionField cnvcOrganise = new QueryConditionField("cnvcOrganise");

		/// <summary>
		/// 颜色
		/// </summary>
		public QueryConditionField cnvcColor = new QueryConditionField("cnvcColor");

		/// <summary>
		/// 口味
		/// </summary>
		public QueryConditionField cnvcTaste = new QueryConditionField("cnvcTaste");
	}	
}
