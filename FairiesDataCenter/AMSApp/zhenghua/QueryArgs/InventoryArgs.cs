
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:    	InventoryArgs.cs
* 作者:	     郑华
* 创建日期:    2010-3-6
* 功能描述:    存货档案

*                                                           Copyright(C) 2010 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：存货档案查询参数类
	/// </summary>
	public class InventoryArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbInventory";
				
		/// <summary>
		/// 允许生产订单
		/// </summary>
		public QueryConditionField cnbProductBill = new QueryConditionField("cnbProductBill");

		/// <summary>
		/// 存货编码
		/// </summary>
		public QueryConditionField cnvcInvCode = new QueryConditionField("cnvcInvCode");

		/// <summary>
		/// 存货名称
		/// </summary>
		public QueryConditionField cnvcInvName = new QueryConditionField("cnvcInvName");

		/// <summary>
		/// 规格型号
		/// </summary>
		public QueryConditionField cnvcInvStd = new QueryConditionField("cnvcInvStd");

		/// <summary>
		/// 存货大类编码
		/// </summary>
		public QueryConditionField cnvcInvCCode = new QueryConditionField("cnvcInvCCode");

		/// <summary>
		/// 是否销售
		/// </summary>
		public QueryConditionField cnbSale = new QueryConditionField("cnbSale");

		/// <summary>
		/// 是否外购
		/// </summary>
		public QueryConditionField cnbPurchase = new QueryConditionField("cnbPurchase");

		/// <summary>
		/// 是否自制
		/// </summary>
		public QueryConditionField cnbSelf = new QueryConditionField("cnbSelf");

		/// <summary>
		/// 是否生产耗用
		/// </summary>
		public QueryConditionField cnbComsume = new QueryConditionField("cnbComsume");

		/// <summary>
		/// 参考成本
		/// </summary>
		public QueryConditionField cniInvCCost = new QueryConditionField("cniInvCCost");

		/// <summary>
		/// 最新成本
		/// </summary>
		public QueryConditionField cniInvNCost = new QueryConditionField("cniInvNCost");

		/// <summary>
		/// 安全库存量
		/// </summary>
		public QueryConditionField cniSafeNum = new QueryConditionField("cniSafeNum");

		/// <summary>
		/// 最低库存
		/// </summary>
		public QueryConditionField cniLowSum = new QueryConditionField("cniLowSum");

		/// <summary>
		/// 启用日期
		/// </summary>
		public QueryConditionField cndSDate = new QueryConditionField("cndSDate");

		/// <summary>
		/// 停用日期
		/// </summary>
		public QueryConditionField cndEDate = new QueryConditionField("cndEDate");

		/// <summary>
		/// 建档人
		/// </summary>
		public QueryConditionField cnvcCreatePerson = new QueryConditionField("cnvcCreatePerson");

		/// <summary>
		/// 变更人
		/// </summary>
		public QueryConditionField cnvcModifyPerson = new QueryConditionField("cnvcModifyPerson");

		/// <summary>
		/// 变更日期
		/// </summary>
		public QueryConditionField cndModifyDate = new QueryConditionField("cndModifyDate");

		/// <summary>
		/// 计价方式
		/// </summary>
		public QueryConditionField cnvcValueType = new QueryConditionField("cnvcValueType");

		/// <summary>
		/// 计量单位组编码
		/// </summary>
		public QueryConditionField cnvcGroupCode = new QueryConditionField("cnvcGroupCode");

		/// <summary>
		/// 主计量单位编码
		/// </summary>
		public QueryConditionField cnvcComUnitCode = new QueryConditionField("cnvcComUnitCode");

		/// <summary>
		/// 销售默认计量单位
		/// </summary>
		public QueryConditionField cnvcSAComUnitCode = new QueryConditionField("cnvcSAComUnitCode");

		/// <summary>
		/// 采购默认计量单位
		/// </summary>
		public QueryConditionField cnvcPUComUnitCode = new QueryConditionField("cnvcPUComUnitCode");

		/// <summary>
		/// 库存默认计量单位
		/// </summary>
		public QueryConditionField cnvcSTComUnitCode = new QueryConditionField("cnvcSTComUnitCode");

		/// <summary>
		/// 生产计量单位
		/// </summary>
		public QueryConditionField cnvcProduceUnitCode = new QueryConditionField("cnvcProduceUnitCode");

		/// <summary>
		/// 零售价格
		/// </summary>
		public QueryConditionField cnfRetailPrice = new QueryConditionField("cnfRetailPrice");

		/// <summary>
		/// 零售计量单位
		/// </summary>
		public QueryConditionField cnvcShopUnitCode = new QueryConditionField("cnvcShopUnitCode");

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
