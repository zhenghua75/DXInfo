
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	MaterialArgs.cs
* 作者:	     郑华
* 创建日期:    2008-10-22
* 功能描述:    原料材料表

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：原料材料表查询参数类
	/// </summary>
	public class MaterialArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbMaterial";
				
		/// <summary>
		/// 原料编码
		/// </summary>
		public QueryConditionField cnvcMaterialCode = new QueryConditionField("cnvcMaterialCode");

		/// <summary>
		/// 原料名称
		/// </summary>
		public QueryConditionField cnvcMaterialName = new QueryConditionField("cnvcMaterialName");

		/// <summary>
		/// 最小计量单位
		/// </summary>
		public QueryConditionField cnvcLeastUnit = new QueryConditionField("cnvcLeastUnit");

		/// <summary>
		/// 换算关系
		/// </summary>
		public QueryConditionField cnnConversion = new QueryConditionField("cnnConversion");

		/// <summary>
		/// 单位
		/// </summary>
		public QueryConditionField cnvcUnit = new QueryConditionField("cnvcUnit");

		/// <summary>
		/// 规格单位
		/// </summary>
		public QueryConditionField cnvcStandardUnit = new QueryConditionField("cnvcStandardUnit");

		/// <summary>
		/// 规格数量
		/// </summary>
		public QueryConditionField cnnStatdardCount = new QueryConditionField("cnnStatdardCount");

		/// <summary>
		/// 最小计量单位价格
		/// </summary>
		public QueryConditionField cnnPrice = new QueryConditionField("cnnPrice");

		/// <summary>
		/// 产品类型
		/// </summary>
		public QueryConditionField cnvcProductType = new QueryConditionField("cnvcProductType");

		/// <summary>
		/// 产品类别
		/// </summary>
		public QueryConditionField cnvcProductClass = new QueryConditionField("cnvcProductClass");
	}	
}
