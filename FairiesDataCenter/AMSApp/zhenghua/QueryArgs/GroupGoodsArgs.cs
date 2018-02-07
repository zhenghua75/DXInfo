
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	GroupGoodsArgs.cs
* 作者:	     郑华
* 创建日期:    2008-10-23
* 功能描述:    生产组产品对应表

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：生产组产品对应表查询参数类
	/// </summary>
	public class GroupGoodsArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbGroupGoods";
				
		/// <summary>
		/// 产品类型
		/// </summary>
		public QueryConditionField cnvcProductType = new QueryConditionField("cnvcProductType");

		/// <summary>
		/// 产品类别
		/// </summary>
		public QueryConditionField cnvcProductClass = new QueryConditionField("cnvcProductClass");

		/// <summary>
		/// 生产组编码
		/// </summary>
		public QueryConditionField cnvcGroupCode = new QueryConditionField("cnvcGroupCode");

		/// <summary>
		/// 描述
		/// </summary>
		public QueryConditionField cnvcComments = new QueryConditionField("cnvcComments");
	}	
}
