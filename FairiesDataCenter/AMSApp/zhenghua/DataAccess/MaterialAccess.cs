
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	MaterialAccess.cs
* 作者:	     郑华
* 创建日期:    2008-9-29
* 功能描述:    原料材料表

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
	/// **功能名称：原料材料表数据访问类
	/// </summary>
	public class MaterialAccess
	{
		// 这里写你的代码


		public static DataTable GetAllMaterial(SqlConnection conn,string strMaterialCode,string strMaterialName)
		{
			string strSql = "select cnvcMaterialCode as cnvcOldMaterialCode,cnvcMaterialCode,cnvcMaterialName,b.cnvcName as cnvcLeastUnit,"
						+	" a.cnnConversion,a.cnvcUnit,a.cnvcStandardUnit,a.cnnStatdardCount,"
						+	" a.cnnPrice,c.cnvcName as cnvcProductType "
						+	" from tbMaterial a "
						+	" left outer join (select * from tbNameCode where cnvcType = 'LEASTUNIT') b "
						+	" on a.cnvcLeastUnit=b.cnvcCode "
						+	" left outer join (select * from tbNameCode where cnvcType='PRODUCTTYPE')c "
						+	" on a.cnvcProductType=c.cnvcCode "
						+	" where a.cnvcMaterialCode like '%"+strMaterialCode+"%' and a.cnvcMaterialName like '%"+strMaterialName+"%'";
			return SqlHelper.ExecuteDataTable(conn, CommandType.Text, strSql);
		}

		public static int UpdateMaterial(SqlTransaction trans,Material mat,string strOldMaterialCode)
		{
			string strSql = "update tbMaterial set "
			                + " cnvcMaterialCode = '" + mat.cnvcMaterialCode + "', "
			                + " cnvcMaterialName='" + mat.cnvcMaterialName + "', "
			                + " cnvcLeastUnit = '" + mat.cnvcLeastUnit + "', "
			                + " cnnConversion = " + mat.cnnConversion + ", "
			                + " cnvcUnit = '" + mat.cnvcUnit + "', "
			                + " cnvcStandardUnit = '" + mat.cnvcStandardUnit + "', "
			                + " cnnStatdardCount = " + mat.cnnStatdardCount + ", "
			                + " cnnPrice = " + mat.cnnPrice + ", "
			                + " cnvcProductType = '" + mat.cnvcProductType + "', "
			                + "cnvcProductClass='" + mat.cnvcProductClass + "'"
			                + " where cnvcMaterialCode = '" + strOldMaterialCode + "' ";
			return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strSql);
		}

		#region 生成器生成代码



		/// <summary>
		/// 取当前查询参数实例


		/// </summary>
		/// <returns>查询参数</returns>
		private static MaterialArgs GetCurrentArgs()
		{
			return new MaterialArgs();
		}
		
		#endregion
	}
}

