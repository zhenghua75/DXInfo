using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using AMSApp.zhenghua.EntityBase;

namespace AMSApp.zhenghua.Common
{
	/// <summary>
	/// ���ƣ��������ݿ��ѯ�ࡣ
	/// �汾��V1.0
	/// ������Fightop Lin
	/// ���ڣ�2006-06-25
	/// �������ṩ���ڲ�ѯ������ʵ�����ļ򵥲�ѯ����
	///
	/// Log ��1
	/// �汾��V1.1
	/// �޸ģ�Fightop Lin
	/// ���ڣ�2006-09-16
	/// ���������� ExecuteDelete��ExecuteUpdate�������ط�������ɻ���������ɾ�������¡�
	///     
	/// Log ��2
	/// �汾��V1.2
	/// �޸ģ�Fightop Lin
	/// ���ڣ�2006-09-20
	/// ���������� ExecuteQuery��ExecuteCount��ExecutePage��SqlTransaction���ذ档
	/// 
	/// Log ��3
	/// �汾��
	/// �޸ģ�
	/// ���ڣ�
	/// ������
	///   
	/// </summary>
	public class SingleTableQuery
	{
		#region ִ�в�ѯͳ����

		/// <summary>
		/// ִ�в�ѯͳ����
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="conn">���ݿ�����</param>
		/// <returns>������</returns>
		public static int ExcuteCount(String strTableName,SqlConnection conn)
		{
			return ExcuteCount(strTableName,null,conn,null);
		}

		/// <summary>
		/// ִ�в�ѯͳ����
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="tran">���ݿ��������</param>
		/// <returns>������</returns>
		public static int ExcuteCount(String strTableName,SqlTransaction tran)
		{
			return ExcuteCount(strTableName,null,null,tran);
		}

		/// <summary>
		/// ִ�в�ѯͳ����
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="queryCondition">��ѯ����</param>
		/// <param name="conn">���ݿ�����</param>
		/// <returns>������</returns>
		public static int ExcuteCount(String strTableName,QueryConditionDecorator queryCondition,SqlConnection conn)
		{
			return ExcuteCount(strTableName,queryCondition,conn,null);
		}

		/// <summary>
		/// ִ�в�ѯͳ����
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="queryCondition">��ѯ����</param>
		/// <param name="tran">���ݿ��������</param>
		/// <returns>������</returns>
		public static int ExcuteCount(String strTableName,QueryConditionDecorator queryCondition,SqlTransaction tran)
		{
			return ExcuteCount(strTableName,queryCondition,null,tran);
		}

		/// <summary>
		/// ִ�в�ѯͳ����
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="queryCondition">��ѯ����</param>
		/// <param name="conn">���ݿ�����</param>
		/// <param name="tran">���ݿ��������</param>
		/// <returns>������</returns>
		private static int ExcuteCount(String strTableName,QueryConditionDecorator queryCondition,SqlConnection conn,SqlTransaction tran)
		{
			string strSql = "SELECT Count(*) FROM " + strTableName;

			// ��������
			SqlParameter[] paraQuery = null;
			if(null != queryCondition )
			{
				QueryCondition condition = new QueryCondition();
				condition.ConnectTo(queryCondition);
            
				ArrayList lstSqlParameter = new ArrayList();
				strSql += condition.MakeCondition(lstSqlParameter);

				paraQuery = new SqlParameter[lstSqlParameter.Count];
				for(int i=0; i < lstSqlParameter.Count; i++)
				{
					paraQuery[i] = lstSqlParameter[i] as SqlParameter;
				}
			}

			// ִ�в�ѯ
			if ( null != paraQuery)
			{
				if(null != conn)
				{
					return (int)SqlHelper.ExecuteScalar(conn,CommandType.Text,strSql,paraQuery);
				}
				else
				{
					return (int)SqlHelper.ExecuteScalar(tran,CommandType.Text,strSql,paraQuery);
				}
			}
			else
			{
				if(null != conn)
				{
					return (int)SqlHelper.ExecuteScalar(conn,CommandType.Text,strSql);
				}
				else
				{
					return (int)SqlHelper.ExecuteScalar(tran,CommandType.Text,strSql);
				}
			}
		}

		#endregion

		#region ִ�з�ҳ��ѯ���ر�

		/// <summary>
		/// ִ�з�ҳ��ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="startRecord">��ʼ��ѯ��¼</param>
		/// <param name="maxRecords">��ѯ��¼����</param>
		/// <param name="conn">���ݿ�����</param>
		/// <returns>�����</returns>
		public static DataTable ExcutePage(String strTableName,int startRecord,int maxRecords,SqlConnection conn)
		{
			return ExcutePage(strTableName,null,null,startRecord,maxRecords,conn,null);
		}

		/// <summary>
		/// ִ�з�ҳ��ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="startRecord">��ʼ��ѯ��¼</param>
		/// <param name="maxRecords">��ѯ��¼����</param>
		/// <param name="tran">���ݿ��������</param>
		/// <returns>�����</returns>
		public static DataTable ExcutePage(String strTableName,int startRecord,int maxRecords,SqlTransaction tran)
		{
			return ExcutePage(strTableName,null,null,startRecord,maxRecords,null,tran);
		}

		/// <summary>
		/// ִ�з�ҳ��ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="queryCondition">��ѯ����</param>
		/// <param name="startRecord">��ʼ��ѯ��¼</param>
		/// <param name="maxRecords">��ѯ��¼����</param>
		/// <param name="conn">���ݿ�����</param>
		/// <returns>�����</returns>
		public static DataTable ExcutePage(String strTableName,QueryConditionDecorator queryCondition,int startRecord,int maxRecords,SqlConnection conn)
		{
			return ExcutePage(strTableName,queryCondition,null,startRecord,maxRecords,conn,null);
		}

		/// <summary>
		/// ִ�з�ҳ��ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="queryCondition">��ѯ����</param>
		/// <param name="startRecord">��ʼ��ѯ��¼</param>
		/// <param name="maxRecords">��ѯ��¼����</param>
		/// <param name="tran">���ݿ��������</param>
		/// <returns>�����</returns>
		public static DataTable ExcutePage(String strTableName,QueryConditionDecorator queryCondition,int startRecord,int maxRecords,SqlTransaction tran)
		{
			return ExcutePage(strTableName,queryCondition,null,startRecord,maxRecords,null,tran);
		}

		/// <summary>
		/// ִ�з�ҳ��ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="queryOrder">���򼯺�</param>
		/// <param name="startRecord">��ʼ��ѯ��¼</param>
		/// <param name="maxRecords">��ѯ��¼����</param>
		/// <param name="conn">���ݿ�����</param>
		/// <returns>�����</returns>
		public static DataTable ExcutePage(String strTableName,QueryOrderCollection queryOrder,int startRecord,int maxRecords,SqlConnection conn)
		{
			return ExcutePage(strTableName,null,queryOrder,startRecord,maxRecords,conn,null);
		}

		/// <summary>
		/// ִ�з�ҳ��ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="queryOrder">���򼯺�</param>
		/// <param name="startRecord">��ʼ��ѯ��¼</param>
		/// <param name="maxRecords">��ѯ��¼����</param>
		/// <param name="tran">���ݿ��������</param>
		/// <returns>�����</returns>
		public static DataTable ExcutePage(String strTableName,QueryOrderCollection queryOrder,int startRecord,int maxRecords,SqlTransaction tran)
		{
			return ExcutePage(strTableName,null,queryOrder,startRecord,maxRecords,null,tran);
		}

		/// <summary>
		/// ִ�з�ҳ��ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="queryCondition">��ѯ����</param>
		/// <param name="queryOrder">���򼯺�</param>
		/// <param name="startRecord">��ʼ��ѯ��¼</param>
		/// <param name="maxRecords">��ѯ��¼����</param>
		/// <param name="conn">���ݿ�����</param>
		/// <returns>�����</returns>
		public static DataTable ExcutePage(String strTableName,QueryConditionDecorator queryCondition,
			QueryOrderCollection queryOrder,int startRecord,int maxRecords,SqlConnection conn)
		{
			return ExcutePage(strTableName,queryCondition,queryOrder,startRecord,maxRecords,conn,null);
		}

		/// <summary>
		/// ִ�з�ҳ��ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="queryCondition">��ѯ����</param>
		/// <param name="queryOrder">���򼯺�</param>
		/// <param name="startRecord">��ʼ��ѯ��¼</param>
		/// <param name="maxRecords">��ѯ��¼����</param>
		/// <param name="tran">���ݿ��������</param>
		/// <returns>�����</returns>
		public static DataTable ExcutePage(String strTableName,QueryConditionDecorator queryCondition,
			QueryOrderCollection queryOrder,int startRecord,int maxRecords,SqlTransaction tran)
		{
			return ExcutePage(strTableName,queryCondition,queryOrder,startRecord,maxRecords,null,tran);
		}
        
		/// <summary>
		/// ִ�з�ҳ��ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="queryCondition">��ѯ����</param>
		/// <param name="queryOrder">���򼯺�</param>
		/// <param name="startRecord">��ʼ��ѯ��¼</param>
		/// <param name="maxRecords">��ѯ��¼����</param>
		/// <param name="conn">���ݿ�����</param>
		/// <param name="tran">���ݿ��������</param>
		/// <returns>�����</returns>
		private static DataTable ExcutePage(String strTableName,QueryConditionDecorator queryCondition,
			QueryOrderCollection queryOrder,int startRecord,int maxRecords,SqlConnection conn,SqlTransaction tran)
		{
			string strSql = "SELECT * FROM " + strTableName;

			// ��������
			SqlParameter[] paraQuery = null;
			if(null != queryCondition )
			{
				QueryCondition condition = new QueryCondition();
				condition.ConnectTo(queryCondition);
            
				ArrayList lstSqlParameter = new ArrayList();
				strSql += condition.MakeCondition(lstSqlParameter);

				paraQuery = new SqlParameter[lstSqlParameter.Count];
				for(int i=0; i < lstSqlParameter.Count; i++)
				{
					paraQuery[i] = lstSqlParameter[i] as SqlParameter;
				}
			}

			// ��������
			if(null != queryOrder)
			{
				strSql += queryOrder.MakeOrder();
			}

			// ִ�в�ѯ
			if ( null != paraQuery)
			{
				if( null != conn)
				{
					return SqlHelper.ExecuteDataTable(conn,CommandType.Text,strSql,startRecord,maxRecords,paraQuery);
				}
				else
				{
					return SqlHelper.ExecuteDataTable(tran,CommandType.Text,strSql,startRecord,maxRecords,paraQuery);
				}
			}
			else
			{
				if( null != conn)
				{
					return SqlHelper.ExecuteDataTable(conn,CommandType.Text,strSql,startRecord,maxRecords);
				}
				else
				{
					return SqlHelper.ExecuteDataTable(tran,CommandType.Text,strSql,startRecord,maxRecords);
				}
			}
		}

		#endregion

		#region ִ�в�ѯ���ر�

		/// <summary>
		/// ִ�в�ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="conn">���ݿ�����</param>
		/// <returns>�����</returns>
		public static DataTable ExcuteQuery(String strTableName,SqlConnection conn)
		{
			return ExcuteQuery(strTableName,0,null,null,conn,null);
		}

		/// <summary>
		/// ִ�в�ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="tran">���ݿ��������</param>
		/// <returns>�����</returns>
		public static DataTable ExcuteQuery(String strTableName,SqlTransaction tran)
		{
			return ExcuteQuery(strTableName,0,null,null,null,tran);
		}

		/// <summary>
		/// ִ�в�ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="queryCondition">��ѯ����</param>
		/// <param name="conn">���ݿ�����</param>
		/// <returns>�����</returns>
		public static DataTable ExcuteQuery(String strTableName,QueryConditionDecorator queryCondition,SqlConnection conn)
		{
			return ExcuteQuery(strTableName,0,queryCondition,null,conn,null);
		}

		/// <summary>
		/// ִ�в�ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="queryCondition">��ѯ����</param>
		/// <param name="tran">���ݿ��������</param>
		/// <returns>�����</returns>
		public static DataTable ExcuteQuery(String strTableName,QueryConditionDecorator queryCondition,SqlTransaction tran)
		{
			return ExcuteQuery(strTableName,0,queryCondition,null,null,tran);
		}

		/// <summary>
		/// ִ�в�ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="queryOrder">���򼯺�</param>
		/// <param name="conn">���ݿ�����</param>
		/// <returns>�����</returns>
		public static DataTable ExcuteQuery(String strTableName,QueryOrderCollection queryOrder,SqlConnection conn)
		{
			return ExcuteQuery(strTableName,0,null,queryOrder,conn,null);
		}

		/// <summary>
		/// ִ�в�ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="queryOrder">���򼯺�</param>
		/// <param name="tran">���ݿ��������</param>
		/// <returns>�����</returns>
		public static DataTable ExcuteQuery(String strTableName,QueryOrderCollection queryOrder,SqlTransaction tran)
		{
			return ExcuteQuery(strTableName,0,null,queryOrder,null,tran);
		}

		/// <summary>
		/// ִ�в�ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="queryCondition">��ѯ����</param>
		/// <param name="queryOrder">���򼯺�</param>
		/// <param name="conn">���ݿ�����</param>
		/// <returns>�����</returns>
		public static DataTable ExcuteQuery(String strTableName,QueryConditionDecorator queryCondition,QueryOrderCollection queryOrder,SqlConnection conn)
		{
			return ExcuteQuery(strTableName,0,queryCondition,queryOrder,conn,null);
		}

		/// <summary>
		/// ִ�в�ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="queryCondition">��ѯ����</param>
		/// <param name="queryOrder">���򼯺�</param>
		/// <param name="tran">���ݿ��������</param>
		/// <returns>�����</returns>
		public static DataTable ExcuteQuery(String strTableName,QueryConditionDecorator queryCondition,QueryOrderCollection queryOrder,SqlTransaction tran)
		{
			return ExcuteQuery(strTableName,0,queryCondition,queryOrder,null,tran);
		}

		/// <summary>
		/// ִ�в�ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="iTopNumber">TOP N</param>
		/// <param name="conn">���ݿ�����</param>
		/// <returns>�����</returns>
		public static DataTable ExcuteQuery(String strTableName,int iTopNumber,SqlConnection conn)
		{
			return ExcuteQuery(strTableName,iTopNumber,null,null,conn,null);
		}

		/// <summary>
		/// ִ�в�ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="iTopNumber">TOP N</param>
		/// <param name="tran">���ݿ��������</param>
		/// <returns>�����</returns>
		public static DataTable ExcuteQuery(String strTableName,int iTopNumber,SqlTransaction tran)
		{
			return ExcuteQuery(strTableName,iTopNumber,null,null,null,tran);
		}

		/// <summary>
		/// ִ�в�ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="iTopNumber">TOP N</param>
		/// <param name="queryCondition">��ѯ����</param>
		/// <param name="conn">���ݿ�����</param>
		/// <returns>�����</returns>
		public static DataTable ExcuteQuery(String strTableName,int iTopNumber,QueryConditionDecorator queryCondition,SqlConnection conn)
		{
			return ExcuteQuery(strTableName,iTopNumber,queryCondition,null,conn,null);
		}

		/// <summary>
		/// ִ�в�ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="iTopNumber">TOP N</param>
		/// <param name="queryCondition">��ѯ����</param>
		/// <param name="tran">���ݿ��������</param>
		/// <returns>�����</returns>
		public static DataTable ExcuteQuery(String strTableName,int iTopNumber,QueryConditionDecorator queryCondition,SqlTransaction tran)
		{
			return ExcuteQuery(strTableName,iTopNumber,queryCondition,null,null,tran);
		}

		/// <summary>
		/// ִ�в�ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="iTopNumber">TOP N</param>
		/// <param name="queryOrder">���򼯺�</param>
		/// <param name="conn">���ݿ�����</param>
		/// <returns>�����</returns>
		public static DataTable ExcuteQuery(String strTableName,int iTopNumber,QueryOrderCollection queryOrder,SqlConnection conn)
		{
			return ExcuteQuery(strTableName,iTopNumber,null,queryOrder,conn,null);
		}

		/// <summary>
		/// ִ�в�ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="iTopNumber">TOP N</param>
		/// <param name="queryOrder">���򼯺�</param>
		/// <param name="tran">���ݿ��������</param>
		/// <returns>�����</returns>
		public static DataTable ExcuteQuery(String strTableName,int iTopNumber,QueryOrderCollection queryOrder,SqlTransaction tran)
		{
			return ExcuteQuery(strTableName,iTopNumber,null,queryOrder,null,tran);
		}

		/// <summary>
		/// ִ�в�ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="iTopNumber">TOP N</param>
		/// <param name="queryCondition">��ѯ����</param>
		/// <param name="queryOrder">���򼯺�</param>
		/// <param name="conn">���ݿ�����</param>
		/// <returns>�����</returns>
		public static DataTable ExcuteQuery(String strTableName,int iTopNumber,
			QueryConditionDecorator queryCondition,QueryOrderCollection queryOrder,SqlConnection conn)
		{
			return ExcuteQuery(strTableName,iTopNumber,queryCondition,queryOrder,conn,null);
		}

		/// <summary>
		/// ִ�в�ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="iTopNumber">TOP N</param>
		/// <param name="queryCondition">��ѯ����</param>
		/// <param name="queryOrder">���򼯺�</param>
		/// <param name="tran">���ݿ��������</param>
		/// <returns>�����</returns>
		public static DataTable ExcuteQuery(String strTableName,int iTopNumber,
			QueryConditionDecorator queryCondition,QueryOrderCollection queryOrder,SqlTransaction tran)
		{
			return ExcuteQuery(strTableName,iTopNumber,queryCondition,queryOrder,null,tran);
		}

		/// <summary>
		/// ִ�в�ѯ���ر�
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="iTopNumber">TOP N</param>
		/// <param name="queryCondition">��ѯ����</param>
		/// <param name="queryOrder">���򼯺�</param>
		/// <param name="conn">���ݿ�����</param>
		/// <param name="tran">���ݿ��������</param>
		/// <returns>�����</returns>
		private static DataTable ExcuteQuery(String strTableName,int iTopNumber,
			QueryConditionDecorator queryCondition,QueryOrderCollection queryOrder,SqlConnection conn,SqlTransaction tran)
		{
			string strSql = "SELECT";
			if(0 != iTopNumber)
			{
				strSql += " TOP " + iTopNumber;
			}
			strSql += " * FROM " + strTableName;

			// ��������
			SqlParameter[] paraQuery = null;
			if(null != queryCondition )
			{
				QueryCondition condition = new QueryCondition();
				condition.ConnectTo(queryCondition);
            
				ArrayList lstSqlParameter = new ArrayList();
				strSql += condition.MakeCondition(lstSqlParameter);

				paraQuery = new SqlParameter[lstSqlParameter.Count];
				for(int i=0; i < lstSqlParameter.Count; i++)
				{
					paraQuery[i] = lstSqlParameter[i] as SqlParameter;
				}
			}

			// ��������
			if(null != queryOrder)
			{
				strSql += queryOrder.MakeOrder();
			}

			// ִ�в�ѯ
			if ( null != paraQuery)
			{
				if(null != conn)
				{
					return SqlHelper.ExecuteDataTable(conn,CommandType.Text,strSql,paraQuery);
				}
				else
				{
					return SqlHelper.ExecuteDataTable(tran,CommandType.Text,strSql,paraQuery);
				}
			}
			else
			{
				if(null != conn)
				{
					return SqlHelper.ExecuteDataTable(conn,CommandType.Text,strSql);
				}
				else
				{
					return SqlHelper.ExecuteDataTable(tran,CommandType.Text,strSql);
				}
			}
		}

		#endregion
   
		#region ִ�и��²�ѯ

		/// <summary>
		/// ִ�и��²�ѯ
		/// </summary>
		/// <param name="objEntity">����ֵʵ��</param>
		/// <param name="conn">���ݿ�����</param>
		/// <returns>ɾ������</returns>
		public static int ExcuteUpdate(EntityObjectBase objEntity,SqlConnection conn)
		{
			return ExcuteUpdate(objEntity,null,conn,null);
		}


		/// <summary>
		/// ִ�и��²�ѯ
		/// </summary>
		/// <param name="objEntity">����ֵʵ��</param>
		/// <param name="tran">���ݿ��������</param>
		/// <returns>ɾ������</returns>
		public static int ExcuteUpdate(EntityObjectBase objEntity,SqlTransaction tran)
		{
			return ExcuteUpdate(objEntity,null,null,tran);
		}

		/// <summary>
		/// ִ�и��²�ѯ
		/// </summary>
		/// <param name="objEntity">����ֵʵ��</param>
		/// <param name="queryCondition">��ѯ����</param>
		/// <param name="conn">���ݿ�����</param>
		/// <returns>ɾ������</returns>
		public static int ExcuteUpdate(EntityObjectBase objEntity,QueryConditionDecorator queryCondition,SqlConnection conn)
		{
			return ExcuteUpdate(objEntity,queryCondition,conn,null);
		}

		/// <summary>
		/// ִ�и��²�ѯ
		/// </summary>
		/// <param name="objEntity">����ֵʵ��</param>
		/// <param name="queryCondition">��ѯ����</param>
		/// <param name="tran">���ݿ��������</param>
		/// <returns>ɾ������</returns>
		public static int ExcuteUpdate(EntityObjectBase objEntity,QueryConditionDecorator queryCondition,SqlTransaction tran)
		{
			return ExcuteUpdate(objEntity,queryCondition,null,tran);
		}


		/// <summary>
		/// ִ�и��²�ѯ
		/// </summary>
		/// <param name="objEntity">����ֵʵ��</param>
		/// <param name="queryCondition">��ѯ����</param>
		/// <param name="conn">���ݿ�����</param>
		/// <param name="tran">���ݿ��������</param>
		/// <returns>ɾ������</returns>
		private static int ExcuteUpdate(EntityObjectBase objEntity,QueryConditionDecorator queryCondition,SqlConnection conn,SqlTransaction tran)
		{
			TableAttributes taEntity = objEntity.GetEntityColumns();

			string strUpColumns = String.Empty;

			ArrayList lstPara = new ArrayList();
			for (int i = 0; i < taEntity.Columns.Count; i++)
			{// ��װ����
				ColumnAttributes caCurrent = taEntity.Columns[i] as ColumnAttributes;
				if (caCurrent.IsModify)
				{
					strUpColumns += caCurrent.ColumnName + "=@Up_" + caCurrent.ColumnName + ",";
				}
				else
				{
					continue;
				}

				//��������
				SqlParameter paraCurrent = new SqlParameter("@Up_" + caCurrent.ColumnName, caCurrent.Value);
				lstPara.Add(paraCurrent);
			}

			if(String.Empty == strUpColumns.Trim())
			{
				return 0;
			}

			SqlParameter[] objPara = new SqlParameter[lstPara.Count];
			for(int i=0; i < lstPara.Count; i++)
			{
				objPara[i] = lstPara[i] as SqlParameter;
			}

			// ���ɸ�������
			string strCondition = String.Empty;
			if(null != queryCondition )
			{
				QueryCondition condition = new QueryCondition();
				condition.ConnectTo(queryCondition);
            
				ArrayList lstSqlParameter = new ArrayList();
				strCondition += condition.MakeCondition(lstSqlParameter);

				SqlParameter[] paraQuery = new SqlParameter[lstSqlParameter.Count];
				for(int i=0; i < lstSqlParameter.Count; i++)
				{
					paraQuery[i] = lstSqlParameter[i] as SqlParameter;
				}

				// ��ϲ���
				SqlParameter[] paraTemp = new SqlParameter[objPara.Length];
				objPara.CopyTo(paraTemp,0);
				objPara = new SqlParameter[paraTemp.Length + paraQuery.Length];
				paraTemp.CopyTo(objPara,0);
				paraQuery.CopyTo(objPara,paraTemp.Length);
			}

			string strSql = "UPDATE [TableName] SET [Columns] [Condition]";
			// �滻��ǰ������
			strSql = strSql.Replace("[TableName]", taEntity.TableName);
			strSql = strSql.Replace("[Columns]", strUpColumns.Substring(0, strUpColumns.Length - 1));
			strSql = strSql.Replace("[Condition]", strCondition);

			// ִ�в�ѯ
			if (null != conn)
			{
				return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, objPara);
			}
			else
			{
				return SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql, objPara);
			}
		}

		#endregion

		#region ִ��ɾ����ѯ

		/// <summary>
		/// ִ��ɾ����ѯ
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="conn">���ݿ�����</param>
		/// <returns>ɾ������</returns>
		public static int ExcuteDelete(String strTableName,SqlConnection conn)
		{
			return ExcuteDelete(strTableName,null,conn,null);
		}


		/// <summary>
		/// ִ��ɾ����ѯ
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="tran">���ݿ��������</param>
		/// <returns>ɾ������</returns>
		public static int ExcuteDelete(String strTableName,SqlTransaction tran)
		{
			return ExcuteDelete(strTableName,null,null,tran);
		}

		/// <summary>
		/// ִ��ɾ����ѯ
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="queryCondition">��ѯ����</param>
		/// <param name="conn">���ݿ�����</param>
		/// <returns>ɾ������</returns>
		public static int ExcuteDelete(String strTableName,QueryConditionDecorator queryCondition,SqlConnection conn)
		{
			return ExcuteDelete(strTableName,queryCondition,conn,null);
		}

		/// <summary>
		/// ִ��ɾ����ѯ
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="queryCondition">��ѯ����</param>
		/// <param name="tran">���ݿ��������</param>
		/// <returns>ɾ������</returns>
		public static int ExcuteDelete(String strTableName,QueryConditionDecorator queryCondition,SqlTransaction tran)
		{
			return ExcuteDelete(strTableName,queryCondition,null,tran);
		}


		/// <summary>
		/// ִ��ɾ����ѯ
		/// </summary>
		/// <param name="strTableName">����</param>
		/// <param name="queryCondition">��ѯ����</param>
		/// <param name="conn">���ݿ�����</param>
		/// <param name="tran">���ݿ��������</param>
		/// <returns>ɾ������</returns>
		private static int ExcuteDelete(String strTableName,QueryConditionDecorator queryCondition,SqlConnection conn,SqlTransaction tran)
		{
			string strSql = "DELETE";
			strSql += " FROM " + strTableName;

			// ��������
			SqlParameter[] paraQuery = null;
			if(null != queryCondition )
			{
				QueryCondition condition = new QueryCondition();
				condition.ConnectTo(queryCondition);
            
				ArrayList lstSqlParameter = new ArrayList();
				strSql += condition.MakeCondition(lstSqlParameter);

				paraQuery = new SqlParameter[lstSqlParameter.Count];
				for(int i=0; i < lstSqlParameter.Count; i++)
				{
					paraQuery[i] = lstSqlParameter[i] as SqlParameter;
				}
			}

			// ִ�в�ѯ
			if ( null != paraQuery)
			{
				if(null == conn)
				{
					return SqlHelper.ExecuteNonQuery(tran,CommandType.Text,strSql,paraQuery);
				}
				else
				{
					return SqlHelper.ExecuteNonQuery(conn,CommandType.Text,strSql,paraQuery);
				}
			}
			else
			{
				if(null == conn)
				{
					return SqlHelper.ExecuteNonQuery(tran,CommandType.Text,strSql);
				}
				else
				{
					return SqlHelper.ExecuteNonQuery(conn,CommandType.Text,strSql);
				}
			}
		}

		#endregion   
	}
}
