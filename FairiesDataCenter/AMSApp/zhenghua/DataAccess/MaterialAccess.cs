
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	MaterialAccess.cs
* ����:	     ֣��
* ��������:    2008-9-29
* ��������:    ԭ�ϲ��ϱ�

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
	/// **�������ƣ�ԭ�ϲ��ϱ����ݷ�����
	/// </summary>
	public class MaterialAccess
	{
		// ����д��Ĵ���


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

		#region ���������ɴ���



		/// <summary>
		/// ȡ��ǰ��ѯ����ʵ��


		/// </summary>
		/// <returns>��ѯ����</returns>
		private static MaterialArgs GetCurrentArgs()
		{
			return new MaterialArgs();
		}
		
		#endregion
	}
}

