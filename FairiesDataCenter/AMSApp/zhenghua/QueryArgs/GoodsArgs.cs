
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	GoodsArgs.cs
* 作者:	     郑华
* 创建日期:    2008-10-10
* 功能描述:    商品表

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：商品表查询参数类
	/// </summary>
	public class GoodsArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbGoods";
				
		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField vcGoodsID = new QueryConditionField("vcGoodsID");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField vcGoodsName = new QueryConditionField("vcGoodsName");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField vcSpell = new QueryConditionField("vcSpell");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField nPrice = new QueryConditionField("nPrice");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField nRate = new QueryConditionField("nRate");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField iIgValue = new QueryConditionField("iIgValue");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField cNewFlag = new QueryConditionField("cNewFlag");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField vcComments = new QueryConditionField("vcComments");
	}	
}
