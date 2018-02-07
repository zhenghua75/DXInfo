using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using AMSApp.zhenghua.EntityBase;

namespace AMSApp.zhenghua.Common
{
	/// <summary>
	/// 名称：单表数据库查询类。
	/// 版本：V1.0
	/// 创建：Fightop Lin
	/// 日期：2006-06-25
	/// 描述：提供基于查询参数、实体对象的简单查询功能
	///
	/// Log ：1
	/// 版本：V1.1
	/// 修改：Fightop Lin
	/// 日期：2006-09-16
	/// 描述：新增 ExecuteDelete、ExecuteUpdate两组重载方法，完成基于条件的删除及更新。
	///     
	/// Log ：2
	/// 版本：V1.2
	/// 修改：Fightop Lin
	/// 日期：2006-09-20
	/// 描述：新增 ExecuteQuery、ExecuteCount、ExecutePage的SqlTransaction重载版。
	/// 
	/// Log ：3
	/// 版本：
	/// 修改：
	/// 日期：
	/// 描述：
	///   
	/// </summary>
	public class SingleTableQuery
	{
		#region 执行查询统计行

		/// <summary>
		/// 执行查询统计行
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="conn">数据库连接</param>
		/// <returns>行数量</returns>
		public static int ExcuteCount(String strTableName,SqlConnection conn)
		{
			return ExcuteCount(strTableName,null,conn,null);
		}

		/// <summary>
		/// 执行查询统计行
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="tran">数据库操作事务</param>
		/// <returns>行数量</returns>
		public static int ExcuteCount(String strTableName,SqlTransaction tran)
		{
			return ExcuteCount(strTableName,null,null,tran);
		}

		/// <summary>
		/// 执行查询统计行
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="queryCondition">查询条件</param>
		/// <param name="conn">数据库连接</param>
		/// <returns>行数量</returns>
		public static int ExcuteCount(String strTableName,QueryConditionDecorator queryCondition,SqlConnection conn)
		{
			return ExcuteCount(strTableName,queryCondition,conn,null);
		}

		/// <summary>
		/// 执行查询统计行
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="queryCondition">查询条件</param>
		/// <param name="tran">数据库操作事务</param>
		/// <returns>行数量</returns>
		public static int ExcuteCount(String strTableName,QueryConditionDecorator queryCondition,SqlTransaction tran)
		{
			return ExcuteCount(strTableName,queryCondition,null,tran);
		}

		/// <summary>
		/// 执行查询统计行
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="queryCondition">查询条件</param>
		/// <param name="conn">数据库连接</param>
		/// <param name="tran">数据库操作事务</param>
		/// <returns>行数量</returns>
		private static int ExcuteCount(String strTableName,QueryConditionDecorator queryCondition,SqlConnection conn,SqlTransaction tran)
		{
			string strSql = "SELECT Count(*) FROM " + strTableName;

			// 生成条件
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

			// 执行查询
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

		#region 执行分页查询返回表

		/// <summary>
		/// 执行分页查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="startRecord">开始查询记录</param>
		/// <param name="maxRecords">查询记录数量</param>
		/// <param name="conn">数据库连接</param>
		/// <returns>结果集</returns>
		public static DataTable ExcutePage(String strTableName,int startRecord,int maxRecords,SqlConnection conn)
		{
			return ExcutePage(strTableName,null,null,startRecord,maxRecords,conn,null);
		}

		/// <summary>
		/// 执行分页查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="startRecord">开始查询记录</param>
		/// <param name="maxRecords">查询记录数量</param>
		/// <param name="tran">数据库操作事务</param>
		/// <returns>结果集</returns>
		public static DataTable ExcutePage(String strTableName,int startRecord,int maxRecords,SqlTransaction tran)
		{
			return ExcutePage(strTableName,null,null,startRecord,maxRecords,null,tran);
		}

		/// <summary>
		/// 执行分页查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="queryCondition">查询条件</param>
		/// <param name="startRecord">开始查询记录</param>
		/// <param name="maxRecords">查询记录数量</param>
		/// <param name="conn">数据库连接</param>
		/// <returns>结果集</returns>
		public static DataTable ExcutePage(String strTableName,QueryConditionDecorator queryCondition,int startRecord,int maxRecords,SqlConnection conn)
		{
			return ExcutePage(strTableName,queryCondition,null,startRecord,maxRecords,conn,null);
		}

		/// <summary>
		/// 执行分页查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="queryCondition">查询条件</param>
		/// <param name="startRecord">开始查询记录</param>
		/// <param name="maxRecords">查询记录数量</param>
		/// <param name="tran">数据库操作事务</param>
		/// <returns>结果集</returns>
		public static DataTable ExcutePage(String strTableName,QueryConditionDecorator queryCondition,int startRecord,int maxRecords,SqlTransaction tran)
		{
			return ExcutePage(strTableName,queryCondition,null,startRecord,maxRecords,null,tran);
		}

		/// <summary>
		/// 执行分页查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="queryOrder">排序集合</param>
		/// <param name="startRecord">开始查询记录</param>
		/// <param name="maxRecords">查询记录数量</param>
		/// <param name="conn">数据库连接</param>
		/// <returns>结果集</returns>
		public static DataTable ExcutePage(String strTableName,QueryOrderCollection queryOrder,int startRecord,int maxRecords,SqlConnection conn)
		{
			return ExcutePage(strTableName,null,queryOrder,startRecord,maxRecords,conn,null);
		}

		/// <summary>
		/// 执行分页查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="queryOrder">排序集合</param>
		/// <param name="startRecord">开始查询记录</param>
		/// <param name="maxRecords">查询记录数量</param>
		/// <param name="tran">数据库操作事务</param>
		/// <returns>结果集</returns>
		public static DataTable ExcutePage(String strTableName,QueryOrderCollection queryOrder,int startRecord,int maxRecords,SqlTransaction tran)
		{
			return ExcutePage(strTableName,null,queryOrder,startRecord,maxRecords,null,tran);
		}

		/// <summary>
		/// 执行分页查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="queryCondition">查询条件</param>
		/// <param name="queryOrder">排序集合</param>
		/// <param name="startRecord">开始查询记录</param>
		/// <param name="maxRecords">查询记录数量</param>
		/// <param name="conn">数据库连接</param>
		/// <returns>结果集</returns>
		public static DataTable ExcutePage(String strTableName,QueryConditionDecorator queryCondition,
			QueryOrderCollection queryOrder,int startRecord,int maxRecords,SqlConnection conn)
		{
			return ExcutePage(strTableName,queryCondition,queryOrder,startRecord,maxRecords,conn,null);
		}

		/// <summary>
		/// 执行分页查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="queryCondition">查询条件</param>
		/// <param name="queryOrder">排序集合</param>
		/// <param name="startRecord">开始查询记录</param>
		/// <param name="maxRecords">查询记录数量</param>
		/// <param name="tran">数据库操作事务</param>
		/// <returns>结果集</returns>
		public static DataTable ExcutePage(String strTableName,QueryConditionDecorator queryCondition,
			QueryOrderCollection queryOrder,int startRecord,int maxRecords,SqlTransaction tran)
		{
			return ExcutePage(strTableName,queryCondition,queryOrder,startRecord,maxRecords,null,tran);
		}
        
		/// <summary>
		/// 执行分页查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="queryCondition">查询条件</param>
		/// <param name="queryOrder">排序集合</param>
		/// <param name="startRecord">开始查询记录</param>
		/// <param name="maxRecords">查询记录数量</param>
		/// <param name="conn">数据库连接</param>
		/// <param name="tran">数据库操作事务</param>
		/// <returns>结果集</returns>
		private static DataTable ExcutePage(String strTableName,QueryConditionDecorator queryCondition,
			QueryOrderCollection queryOrder,int startRecord,int maxRecords,SqlConnection conn,SqlTransaction tran)
		{
			string strSql = "SELECT * FROM " + strTableName;

			// 生成条件
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

			// 生成排序
			if(null != queryOrder)
			{
				strSql += queryOrder.MakeOrder();
			}

			// 执行查询
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

		#region 执行查询返回表

		/// <summary>
		/// 执行查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="conn">数据库连接</param>
		/// <returns>结果集</returns>
		public static DataTable ExcuteQuery(String strTableName,SqlConnection conn)
		{
			return ExcuteQuery(strTableName,0,null,null,conn,null);
		}

		/// <summary>
		/// 执行查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="tran">数据库操作事务</param>
		/// <returns>结果集</returns>
		public static DataTable ExcuteQuery(String strTableName,SqlTransaction tran)
		{
			return ExcuteQuery(strTableName,0,null,null,null,tran);
		}

		/// <summary>
		/// 执行查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="queryCondition">查询条件</param>
		/// <param name="conn">数据库连接</param>
		/// <returns>结果集</returns>
		public static DataTable ExcuteQuery(String strTableName,QueryConditionDecorator queryCondition,SqlConnection conn)
		{
			return ExcuteQuery(strTableName,0,queryCondition,null,conn,null);
		}

		/// <summary>
		/// 执行查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="queryCondition">查询条件</param>
		/// <param name="tran">数据库操作事务</param>
		/// <returns>结果集</returns>
		public static DataTable ExcuteQuery(String strTableName,QueryConditionDecorator queryCondition,SqlTransaction tran)
		{
			return ExcuteQuery(strTableName,0,queryCondition,null,null,tran);
		}

		/// <summary>
		/// 执行查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="queryOrder">排序集合</param>
		/// <param name="conn">数据库连接</param>
		/// <returns>结果集</returns>
		public static DataTable ExcuteQuery(String strTableName,QueryOrderCollection queryOrder,SqlConnection conn)
		{
			return ExcuteQuery(strTableName,0,null,queryOrder,conn,null);
		}

		/// <summary>
		/// 执行查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="queryOrder">排序集合</param>
		/// <param name="tran">数据库操作事务</param>
		/// <returns>结果集</returns>
		public static DataTable ExcuteQuery(String strTableName,QueryOrderCollection queryOrder,SqlTransaction tran)
		{
			return ExcuteQuery(strTableName,0,null,queryOrder,null,tran);
		}

		/// <summary>
		/// 执行查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="queryCondition">查询条件</param>
		/// <param name="queryOrder">排序集合</param>
		/// <param name="conn">数据库连接</param>
		/// <returns>结果集</returns>
		public static DataTable ExcuteQuery(String strTableName,QueryConditionDecorator queryCondition,QueryOrderCollection queryOrder,SqlConnection conn)
		{
			return ExcuteQuery(strTableName,0,queryCondition,queryOrder,conn,null);
		}

		/// <summary>
		/// 执行查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="queryCondition">查询条件</param>
		/// <param name="queryOrder">排序集合</param>
		/// <param name="tran">数据库操作事务</param>
		/// <returns>结果集</returns>
		public static DataTable ExcuteQuery(String strTableName,QueryConditionDecorator queryCondition,QueryOrderCollection queryOrder,SqlTransaction tran)
		{
			return ExcuteQuery(strTableName,0,queryCondition,queryOrder,null,tran);
		}

		/// <summary>
		/// 执行查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="iTopNumber">TOP N</param>
		/// <param name="conn">数据库连接</param>
		/// <returns>结果集</returns>
		public static DataTable ExcuteQuery(String strTableName,int iTopNumber,SqlConnection conn)
		{
			return ExcuteQuery(strTableName,iTopNumber,null,null,conn,null);
		}

		/// <summary>
		/// 执行查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="iTopNumber">TOP N</param>
		/// <param name="tran">数据库操作事务</param>
		/// <returns>结果集</returns>
		public static DataTable ExcuteQuery(String strTableName,int iTopNumber,SqlTransaction tran)
		{
			return ExcuteQuery(strTableName,iTopNumber,null,null,null,tran);
		}

		/// <summary>
		/// 执行查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="iTopNumber">TOP N</param>
		/// <param name="queryCondition">查询条件</param>
		/// <param name="conn">数据库连接</param>
		/// <returns>结果集</returns>
		public static DataTable ExcuteQuery(String strTableName,int iTopNumber,QueryConditionDecorator queryCondition,SqlConnection conn)
		{
			return ExcuteQuery(strTableName,iTopNumber,queryCondition,null,conn,null);
		}

		/// <summary>
		/// 执行查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="iTopNumber">TOP N</param>
		/// <param name="queryCondition">查询条件</param>
		/// <param name="tran">数据库操作事务</param>
		/// <returns>结果集</returns>
		public static DataTable ExcuteQuery(String strTableName,int iTopNumber,QueryConditionDecorator queryCondition,SqlTransaction tran)
		{
			return ExcuteQuery(strTableName,iTopNumber,queryCondition,null,null,tran);
		}

		/// <summary>
		/// 执行查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="iTopNumber">TOP N</param>
		/// <param name="queryOrder">排序集合</param>
		/// <param name="conn">数据库连接</param>
		/// <returns>结果集</returns>
		public static DataTable ExcuteQuery(String strTableName,int iTopNumber,QueryOrderCollection queryOrder,SqlConnection conn)
		{
			return ExcuteQuery(strTableName,iTopNumber,null,queryOrder,conn,null);
		}

		/// <summary>
		/// 执行查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="iTopNumber">TOP N</param>
		/// <param name="queryOrder">排序集合</param>
		/// <param name="tran">数据库操作事务</param>
		/// <returns>结果集</returns>
		public static DataTable ExcuteQuery(String strTableName,int iTopNumber,QueryOrderCollection queryOrder,SqlTransaction tran)
		{
			return ExcuteQuery(strTableName,iTopNumber,null,queryOrder,null,tran);
		}

		/// <summary>
		/// 执行查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="iTopNumber">TOP N</param>
		/// <param name="queryCondition">查询条件</param>
		/// <param name="queryOrder">排序集合</param>
		/// <param name="conn">数据库连接</param>
		/// <returns>结果集</returns>
		public static DataTable ExcuteQuery(String strTableName,int iTopNumber,
			QueryConditionDecorator queryCondition,QueryOrderCollection queryOrder,SqlConnection conn)
		{
			return ExcuteQuery(strTableName,iTopNumber,queryCondition,queryOrder,conn,null);
		}

		/// <summary>
		/// 执行查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="iTopNumber">TOP N</param>
		/// <param name="queryCondition">查询条件</param>
		/// <param name="queryOrder">排序集合</param>
		/// <param name="tran">数据库操作事务</param>
		/// <returns>结果集</returns>
		public static DataTable ExcuteQuery(String strTableName,int iTopNumber,
			QueryConditionDecorator queryCondition,QueryOrderCollection queryOrder,SqlTransaction tran)
		{
			return ExcuteQuery(strTableName,iTopNumber,queryCondition,queryOrder,null,tran);
		}

		/// <summary>
		/// 执行查询返回表
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="iTopNumber">TOP N</param>
		/// <param name="queryCondition">查询条件</param>
		/// <param name="queryOrder">排序集合</param>
		/// <param name="conn">数据库连接</param>
		/// <param name="tran">数据库操作事务</param>
		/// <returns>结果集</returns>
		private static DataTable ExcuteQuery(String strTableName,int iTopNumber,
			QueryConditionDecorator queryCondition,QueryOrderCollection queryOrder,SqlConnection conn,SqlTransaction tran)
		{
			string strSql = "SELECT";
			if(0 != iTopNumber)
			{
				strSql += " TOP " + iTopNumber;
			}
			strSql += " * FROM " + strTableName;

			// 生成条件
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

			// 生成排序
			if(null != queryOrder)
			{
				strSql += queryOrder.MakeOrder();
			}

			// 执行查询
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
   
		#region 执行更新查询

		/// <summary>
		/// 执行更新查询
		/// </summary>
		/// <param name="objEntity">更新值实体</param>
		/// <param name="conn">数据库连接</param>
		/// <returns>删除行数</returns>
		public static int ExcuteUpdate(EntityObjectBase objEntity,SqlConnection conn)
		{
			return ExcuteUpdate(objEntity,null,conn,null);
		}


		/// <summary>
		/// 执行更新查询
		/// </summary>
		/// <param name="objEntity">更新值实体</param>
		/// <param name="tran">数据库操作事务</param>
		/// <returns>删除行数</returns>
		public static int ExcuteUpdate(EntityObjectBase objEntity,SqlTransaction tran)
		{
			return ExcuteUpdate(objEntity,null,null,tran);
		}

		/// <summary>
		/// 执行更新查询
		/// </summary>
		/// <param name="objEntity">更新值实体</param>
		/// <param name="queryCondition">查询条件</param>
		/// <param name="conn">数据库连接</param>
		/// <returns>删除行数</returns>
		public static int ExcuteUpdate(EntityObjectBase objEntity,QueryConditionDecorator queryCondition,SqlConnection conn)
		{
			return ExcuteUpdate(objEntity,queryCondition,conn,null);
		}

		/// <summary>
		/// 执行更新查询
		/// </summary>
		/// <param name="objEntity">更新值实体</param>
		/// <param name="queryCondition">查询条件</param>
		/// <param name="tran">数据库操作事务</param>
		/// <returns>删除行数</returns>
		public static int ExcuteUpdate(EntityObjectBase objEntity,QueryConditionDecorator queryCondition,SqlTransaction tran)
		{
			return ExcuteUpdate(objEntity,queryCondition,null,tran);
		}


		/// <summary>
		/// 执行更新查询
		/// </summary>
		/// <param name="objEntity">更新值实体</param>
		/// <param name="queryCondition">查询条件</param>
		/// <param name="conn">数据库连接</param>
		/// <param name="tran">数据库操作事务</param>
		/// <returns>删除行数</returns>
		private static int ExcuteUpdate(EntityObjectBase objEntity,QueryConditionDecorator queryCondition,SqlConnection conn,SqlTransaction tran)
		{
			TableAttributes taEntity = objEntity.GetEntityColumns();

			string strUpColumns = String.Empty;

			ArrayList lstPara = new ArrayList();
			for (int i = 0; i < taEntity.Columns.Count; i++)
			{// 组装参数
				ColumnAttributes caCurrent = taEntity.Columns[i] as ColumnAttributes;
				if (caCurrent.IsModify)
				{
					strUpColumns += caCurrent.ColumnName + "=@Up_" + caCurrent.ColumnName + ",";
				}
				else
				{
					continue;
				}

				//创建参数
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

			// 生成更新条件
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

				// 组合参数
				SqlParameter[] paraTemp = new SqlParameter[objPara.Length];
				objPara.CopyTo(paraTemp,0);
				objPara = new SqlParameter[paraTemp.Length + paraQuery.Length];
				paraTemp.CopyTo(objPara,0);
				paraQuery.CopyTo(objPara,paraTemp.Length);
			}

			string strSql = "UPDATE [TableName] SET [Columns] [Condition]";
			// 替换当前表数据
			strSql = strSql.Replace("[TableName]", taEntity.TableName);
			strSql = strSql.Replace("[Columns]", strUpColumns.Substring(0, strUpColumns.Length - 1));
			strSql = strSql.Replace("[Condition]", strCondition);

			// 执行查询
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

		#region 执行删除查询

		/// <summary>
		/// 执行删除查询
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="conn">数据库连接</param>
		/// <returns>删除行数</returns>
		public static int ExcuteDelete(String strTableName,SqlConnection conn)
		{
			return ExcuteDelete(strTableName,null,conn,null);
		}


		/// <summary>
		/// 执行删除查询
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="tran">数据库操作事务</param>
		/// <returns>删除行数</returns>
		public static int ExcuteDelete(String strTableName,SqlTransaction tran)
		{
			return ExcuteDelete(strTableName,null,null,tran);
		}

		/// <summary>
		/// 执行删除查询
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="queryCondition">查询条件</param>
		/// <param name="conn">数据库连接</param>
		/// <returns>删除行数</returns>
		public static int ExcuteDelete(String strTableName,QueryConditionDecorator queryCondition,SqlConnection conn)
		{
			return ExcuteDelete(strTableName,queryCondition,conn,null);
		}

		/// <summary>
		/// 执行删除查询
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="queryCondition">查询条件</param>
		/// <param name="tran">数据库操作事务</param>
		/// <returns>删除行数</returns>
		public static int ExcuteDelete(String strTableName,QueryConditionDecorator queryCondition,SqlTransaction tran)
		{
			return ExcuteDelete(strTableName,queryCondition,null,tran);
		}


		/// <summary>
		/// 执行删除查询
		/// </summary>
		/// <param name="strTableName">表名</param>
		/// <param name="queryCondition">查询条件</param>
		/// <param name="conn">数据库连接</param>
		/// <param name="tran">数据库操作事务</param>
		/// <returns>删除行数</returns>
		private static int ExcuteDelete(String strTableName,QueryConditionDecorator queryCondition,SqlConnection conn,SqlTransaction tran)
		{
			string strSql = "DELETE";
			strSql += " FROM " + strTableName;

			// 生成条件
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

			// 执行查询
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
