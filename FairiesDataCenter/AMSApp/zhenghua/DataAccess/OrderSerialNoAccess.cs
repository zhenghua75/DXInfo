
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	OrderSerialNoAccess.cs
* 作者:	     郑华
* 创建日期:    2008-10-4
* 功能描述:    订单流水生成表

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ImportNameSpace
using System;
using System.Data;
using System.Data.SqlClient;

using AMSApp.zhenghua.EntityBase;
using AMSApp.zhenghua.Entity;
using AMSApp.zhenghua.QueryArgs;
using AMSApp.zhenghua.Common;
#endregion

namespace AMSApp.zhenghua.DataAccess
{
	/// <summary>
	/// **功能名称：订单流水生成表数据访问类
	/// </summary>
	public class OrderSerialNoAccess
	{
		// 这里写你的代码



		#region 生成器生成代码



		/// <summary>
		/// 取当前查询参数实例


		/// </summary>
		/// <returns>查询参数</returns>
		private static OrderSerialNoArgs GetCurrentArgs()
		{
			return new OrderSerialNoArgs();
		}
		
		#endregion
	}
}

