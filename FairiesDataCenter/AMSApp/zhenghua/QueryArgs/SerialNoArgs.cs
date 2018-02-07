
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	SerialNoArgs.cs
* 作者:	     郑华
* 创建日期:    2009-7-24
* 功能描述:    产品及报损流水表

*                                                           Copyright(C) 2009 zhenghua
*************************************************************************************/
#region 引用
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **功能名称：产品及报损流水表查询参数类
	/// </summary>
	public class SerialNoArgs
	{
		/// <summary>
		/// 表名
		/// </summary>
		public string TableName = "tbSerialNo";
				
		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField cnnSerialNo = new QueryConditionField("cnnSerialNo");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField cnvcFill = new QueryConditionField("cnvcFill");
	}	
}
