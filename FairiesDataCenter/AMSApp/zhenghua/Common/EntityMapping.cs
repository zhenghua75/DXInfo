#region Import NameSpace

using System;
using System.Collections;
using System.Reflection;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;

using AMSApp.zhenghua.EntityBase;
using AMSApp.zhenghua.Common;

#endregion

namespace AMSApp.zhenghua.Common
{
    /// <summary>
    /// 名称：实体对像映射类。
    /// 版本：V1.0
    /// 创建：Fightop Lin
    /// 日期：2006-06-20
    /// 描述：提供Create() Update() Delete() Get()方法将实体对像映射到数据库
    ///
    /// Log ：1
    /// 版本：v 2.0
    /// 修改：figthop
    /// 日期：2006.8.1
    /// 描述：增加并发更新、及删除的方法。
    ///
    /// Log ：2
    /// 版本：
    /// 修改：
    /// 日期：
    /// 描述：
    /// 
    /// </summary>
    public class EntityMapping
    {
        #region 获取记录映射

        /// <summary>
        /// 获取记录映射
        /// </summary>
        /// <param name="objEntity">实体</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>实体</returns>
        public static EntityObjectBase Get(EntityObjectBase objEntity, SqlConnection conn)
        {
            return Get(objEntity, conn, null);
        }


        /// <summary>
        /// 获取记录映射
        /// </summary>
        /// <param name="objEntity">实体</param>
        /// <param name="tran">数据库操作事务</param>
        /// <returns>实体</returns>
        public static EntityObjectBase Get(EntityObjectBase objEntity, SqlTransaction tran)
        {
            return Get(objEntity, null, tran);
        }


        /// <summary>
        /// 获取记录映射
        /// </summary>
        /// <param name="objEntity">实体</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">数据库操作事务</param>
        /// <returns>实体</returns>
        private static EntityObjectBase Get(EntityObjectBase objEntity, SqlConnection conn, SqlTransaction tran)
        {
            TableAttributes taEntity = objEntity.GetEntityColumns();

            string strKeyColumns = "";

            ArrayList lstPara = new ArrayList();
            for (int i = 0; i < taEntity.Columns.Count; i++)
            {// 组装参数
                ColumnAttributes caCurrent = taEntity.Columns[i] as ColumnAttributes;
                if (caCurrent.IsPrimaryKey)
                {
                    strKeyColumns += caCurrent.ColumnName + "=@" + caCurrent.ColumnName + " AND ";
                    SqlParameter paraCurrent = new SqlParameter("@" + caCurrent.ColumnName, caCurrent.Value);
                    lstPara.Add(paraCurrent);
                }
            }
			SqlParameter[] objPara = new SqlParameter[lstPara.Count];
			for(int i=0; i < lstPara.Count; i++)
			{
				objPara[i] = lstPara[i] as SqlParameter;
			}

            string strSql = "SELECT * FROM [TableName] WHERE [Key]";
            // 替换当前表数据
            strSql = strSql.Replace("[TableName]", taEntity.TableName);
            strSql = strSql.Replace("[Key]", strKeyColumns.Substring(0, strKeyColumns.Length - 5));

            DataTable tbResult = null;
            if (null != conn)
            {
                tbResult = SqlHelper.ExecuteDataset(conn, CommandType.Text, strSql, objPara).Tables[0];
            }
            else
            {
                tbResult = SqlHelper.ExecuteDataset(tran, CommandType.Text, strSql, objPara).Tables[0];
            }

            if (tbResult.Rows.Count > 0)
            {
                Type type = objEntity.GetType();
                EntityObjectBase objResult = Activator.CreateInstance(type) as EntityObjectBase;

                objResult.FromTable(tbResult);

                return objResult;
            }
            else
            {
                return null;
            }
        }

        #endregion

        #region 新建记录映射

        /// <summary>
        /// 新建记录映射
        /// </summary>
        /// <param name="objEntity">实体</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>自增字段值或影响行数</returns>
        public static long Create(EntityObjectBase objEntity,SqlConnection conn)
        {
            return Create(objEntity,conn,null);
        }

        /// <summary>
        /// 新建记录映射
        /// </summary>
        /// <param name="objEntity">实体</param>
        /// <param name="tran">数据库操作事务</param>
        /// <returns>自增字段值或影响行数</returns>
        public static long Create(EntityObjectBase objEntity,SqlTransaction tran)
        {
            return Create(objEntity,null,tran);
        }

        /// <summary>
        /// 新建记录映射
        /// </summary>
        /// <param name="objEntity">实体</param>
        /// <param name="tran">数据库事务</param>
        /// <returns>自增字段值或影响行数</returns>
        private static long Create(EntityObjectBase objEntity,SqlConnection conn, SqlTransaction tran)
        {
            TableAttributes taEntity = objEntity.GetEntityColumns();

            string strColumns = "";
            string strParaColumns = "";
            bool bIsIdentity = false;

            ArrayList lstPara = new ArrayList();
            for(int i = 0; i < taEntity.Columns.Count; i++)
            {// 组装参数
                ColumnAttributes caCurrent = taEntity.Columns[i] as ColumnAttributes;
                if(caCurrent.IsIdentity)
                {
                    bIsIdentity = true;
                    continue;
                }

                strColumns += caCurrent.ColumnName + ",";
                strParaColumns += "@" + caCurrent.ColumnName + ",";

                SqlParameter paraCurrent = null;
                if (caCurrent.IsVersionNumber)
                {
                    paraCurrent = new SqlParameter("@" + caCurrent.ColumnName, 1);
                }
                else
                {
                    //创建参数
                    paraCurrent = new SqlParameter("@" + caCurrent.ColumnName, caCurrent.Value);
                }

                lstPara.Add(paraCurrent);
            }
			SqlParameter[] objPara = new SqlParameter[lstPara.Count];
			for(int i=0; i < lstPara.Count; i++)
			{
				objPara[i] = lstPara[i] as SqlParameter;
			}
            
            string strSql = "INSERT INTO [TableName]([Columns]) VALUES([ParaColumns])";
            // 替换当前表数据
            strSql = strSql.Replace("[TableName]",  taEntity.TableName);
            strSql = strSql.Replace("[Columns]",    strColumns.Substring(0,strColumns.Length-1));
            strSql = strSql.Replace("[ParaColumns]",strParaColumns.Substring(0,strParaColumns.Length-1));

            if(bIsIdentity)
            {
                // 带自增字段的插入
                strSql += "  SELECT @@IDENTITY";
                if( null != conn )
                {
                    return long.Parse(SqlHelper.ExecuteScalar(conn,CommandType.Text,strSql,objPara).ToString());
                }
                else
                {
                    return long.Parse(SqlHelper.ExecuteScalar(tran,CommandType.Text,strSql,objPara).ToString());
                }
            }
            else
            {
                // 不带自增字段的插入
                if ( null != conn)
                {
                    return SqlHelper.ExecuteNonQuery(conn,CommandType.Text,strSql,objPara);
                }
                else
                {
                    return SqlHelper.ExecuteNonQuery(tran,CommandType.Text,strSql,objPara);
                }
            }
        }

        #endregion

        #region 更新记录映射

        /// <summary>
        /// 更新记录映射
        /// </summary>
        /// <param name="objEntity">实体</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>影响的行数</returns>
        public static int Update(EntityObjectBase objEntity, SqlConnection conn)
        {
            return Update(objEntity, conn, null);
        }

        /// <summary>
        /// 更新记录映射
        /// </summary>
        /// <param name="objEntity">实体</param>
        /// <param name="tran">数据库操作事务</param>
        /// <returns>影响的行数</returns>
        public static int Update(EntityObjectBase objEntity, SqlTransaction tran)
        {
            return Update(objEntity, null, tran);
        }

        /// <summary>
        /// 更新记录映射
        /// </summary>
        /// <param name="objEntity">实体</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">数据库操作事务</param>
        /// <returns>影响的行数</returns>
        private static int Update(EntityObjectBase objEntity, SqlConnection conn, SqlTransaction tran)
        {
            TableAttributes taEntity = objEntity.GetEntityColumns();

            string strUpColumns = String.Empty;
            string strKeyColumns = String.Empty;

            ArrayList lstPara = new ArrayList();
            for (int i = 0; i < taEntity.Columns.Count; i++)
            {// 组装参数
                ColumnAttributes caCurrent = taEntity.Columns[i] as ColumnAttributes;
                if (caCurrent.IsPrimaryKey)
                {
                    strKeyColumns += caCurrent.ColumnName + "=@" + caCurrent.ColumnName + " AND ";
                }
                else if (caCurrent.IsVersionNumber)
                {
                    strUpColumns += caCurrent.ColumnName + "=" + caCurrent.ColumnName + " + 1,";

                    continue;
                }
                else if (caCurrent.IsModify)
                {
                    strUpColumns += caCurrent.ColumnName + "=@" + caCurrent.ColumnName + ",";
                }
                else
                {
                    continue;
                }

                //创建参数
                SqlParameter paraCurrent = new SqlParameter("@" + caCurrent.ColumnName, caCurrent.Value);
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

            string strCondition = strKeyColumns;
            string strSql = "UPDATE [TableName] SET [Columns] WHERE [Condition]";
            // 替换当前表数据
            strSql = strSql.Replace("[TableName]", taEntity.TableName);
            strSql = strSql.Replace("[Columns]", strUpColumns.Substring(0, strUpColumns.Length - 1));
            strSql = strSql.Replace("[Condition]", strCondition.Substring(0, strCondition.Length - 5));

            int upCount = 0;
            if (null != conn)
            {
                upCount = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, objPara);
            }
            else
            {
                upCount = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql, objPara);
            }

            return upCount;
        }

        #endregion

        #region 并发更新记录映射

        /// <summary>
        /// 并发更新记录映射
        /// </summary>
        /// <param name="objEntity">实体</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>影响的行数</returns>
        /// <exception cref="ConcurrentException">ConcurrentException</exception>
        public static int ConcurrentUpdate(EntityObjectBase objEntity, SqlConnection conn)
        {
            return ConcurrentUpdate(objEntity, conn, null);
        }

        /// <summary>
        /// 并发更新记录映射
        /// </summary>
        /// <param name="objEntity">实体</param>
        /// <param name="tran">数据库操作事务</param>
        /// <returns>影响的行数</returns>
        /// <exception cref="ConcurrentException">ConcurrentException</exception>
        public static int ConcurrentUpdate(EntityObjectBase objEntity, SqlTransaction tran)
        {
            return ConcurrentUpdate(objEntity, null, tran);
        }

        /// <summary>
        /// 并发更新记录映射
        /// </summary>
        /// <param name="objEntity">实体</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">数据库操作事务</param>
        /// <returns>影响的行数</returns>
        /// <exception cref="ConcurrentException">ConcurrentException</exception>
        private static int ConcurrentUpdate(EntityObjectBase objEntity, SqlConnection conn, SqlTransaction tran)
        {
            TableAttributes taEntity = objEntity.GetEntityColumns();

            string strUpColumns = String.Empty;
            string strKeyColumns = String.Empty;
            string strOldVersion = String.Empty;
            string strKey = String.Empty;

            ArrayList lstPara = new ArrayList();
            for (int i = 0; i < taEntity.Columns.Count; i++)
            {// 组装参数
                ColumnAttributes caCurrent = taEntity.Columns[i] as ColumnAttributes;
                if (caCurrent.IsPrimaryKey)
                {
                    strKeyColumns += caCurrent.ColumnName + "=@" + caCurrent.ColumnName + " AND ";
                    strKey += caCurrent.ColumnName + " = " + caCurrent.Value.ToString() + " ";
                }
                else if (caCurrent.IsVersionNumber)
                {
                    strUpColumns += caCurrent.ColumnName + "=" + caCurrent.ColumnName + " + 1,";
                    strOldVersion = caCurrent.ColumnName + "=@Old" + caCurrent.ColumnName + " AND ";

                    //创建参数
                    SqlParameter paraOldCurrent = new SqlParameter("@Old" + caCurrent.ColumnName, caCurrent.Value);
                    lstPara.Add(paraOldCurrent);

                    continue;
                }
                else if (caCurrent.IsModify)
                {
                    strUpColumns += caCurrent.ColumnName + "=@" + caCurrent.ColumnName + ",";
                }
                else
                {
                    continue;
                }

                //创建参数
                SqlParameter paraCurrent = new SqlParameter("@" + caCurrent.ColumnName, caCurrent.Value);
                lstPara.Add(paraCurrent);
            }
            SqlParameter[] objPara = new SqlParameter[lstPara.Count];
			for(int i=0; i < lstPara.Count; i++)
			{
				objPara[i] = lstPara[i] as SqlParameter;
			}

            string strCondition = strKeyColumns + strOldVersion;
            string strSql = "UPDATE [TableName] SET [Columns] WHERE [Condition]";
            // 替换当前表数据
            strSql = strSql.Replace("[TableName]", taEntity.TableName);
            strSql = strSql.Replace("[Columns]", strUpColumns.Substring(0, strUpColumns.Length - 1));
            strSql = strSql.Replace("[Condition]", strCondition.Substring(0, strCondition.Length - 5));

            int upCount = 0;
            if (null != conn)
            {
                upCount = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, objPara);
            }
            else
            {
                upCount = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql, objPara);
            }

            if (upCount <= 0)
            {
                throw new ConcurrentException(taEntity.TableName, strKey);
            }

            return upCount;
        }

        #endregion

        #region 删除记录映射

        /// <summary>
        /// 删除记录映射
        /// </summary>
        /// <param name="objEntity">实体</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>影响行数</returns>
        public static int Delete(EntityObjectBase objEntity, SqlConnection conn)
        {
            return Delete(objEntity, conn, null);
        }

        /// <summary>
        /// 删除记录映射
        /// </summary>
        /// <param name="objEntity">实体</param>
        /// <param name="tran">数据库操作事务</param>
        /// <returns>影响行数</returns>
        public static int Delete(EntityObjectBase objEntity, SqlTransaction tran)
        {
            return Delete(objEntity, null, tran);
        }


        /// <summary>
        /// 删除记录映射
        /// </summary>
        /// <param name="objEntity">实体</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">数据库操作事务</param>
        /// <returns>影响行数</returns>
        private static int Delete(EntityObjectBase objEntity, SqlConnection conn, SqlTransaction tran)
        {
            TableAttributes taEntity = objEntity.GetEntityColumns();

            string strKeyColumns = String.Empty;

            ArrayList lstPara = new ArrayList();
            for (int i = 0; i < taEntity.Columns.Count; i++)
            {// 组装参数
                ColumnAttributes caCurrent = taEntity.Columns[i] as ColumnAttributes;
                if (caCurrent.IsPrimaryKey)
                {
                    strKeyColumns += caCurrent.ColumnName + "=@" + caCurrent.ColumnName + " AND ";
                }
                else
                {
                    continue;
                }

                SqlParameter paraCurrent = new SqlParameter("@" + caCurrent.ColumnName, caCurrent.Value);
                lstPara.Add(paraCurrent);
            }
			SqlParameter[] objPara = new SqlParameter[lstPara.Count];
			for(int i=0; i < lstPara.Count; i++)
			{
				objPara[i] = lstPara[i] as SqlParameter;
			}


            string strCondition = strKeyColumns;
            string strSql = "DELETE FROM [TableName] WHERE [Condition]";
            // 替换当前表数据
            strSql = strSql.Replace("[TableName]", taEntity.TableName);
            strSql = strSql.Replace("[Condition]", strCondition.Substring(0, strCondition.Length - 5));

            int upCount = 0;
            if (null != conn)
            {
                upCount = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, objPara);
            }
            else
            {
                upCount = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql, objPara);
            }

            return upCount;
        }

        #endregion

        #region 并发删除记录映射

        /// <summary>
        /// 并发删除记录映射
        /// </summary>
        /// <param name="objEntity">实体</param>
        /// <param name="conn">数据库连接</param>
        /// <returns>影响行数</returns>
        /// <exception cref="ConcurrentException">ConcurrentException</exception>
        public static int ConcurrentDelete(EntityObjectBase objEntity, SqlConnection conn)
        {
            return ConcurrentDelete(objEntity, conn, null);
        }

        /// <summary>
        /// 并发删除记录映射
        /// </summary>
        /// <param name="objEntity">实体</param>
        /// <param name="tran">数据库操作事务</param>
        /// <returns>影响行数</returns>
        /// <exception cref="ConcurrentException">ConcurrentException</exception>
        public static int ConcurrentDelete(EntityObjectBase objEntity, SqlTransaction tran)
        {
            return ConcurrentDelete(objEntity, null, tran);
        }


        /// <summary>
        /// 并发删除记录映射
        /// </summary>
        /// <param name="objEntity">实体</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="tran">数据库操作事务</param>
        /// <returns>影响行数</returns>
        /// <exception cref="ConcurrentException">ConcurrentException</exception>
        private static int ConcurrentDelete(EntityObjectBase objEntity,SqlConnection conn,SqlTransaction tran)
        {
            TableAttributes taEntity = objEntity.GetEntityColumns();

            string strKeyColumns = String.Empty;
            string strOldVersion = String.Empty;
            string strKey        = String.Empty;

            ArrayList lstPara = new ArrayList();
            for(int i = 0; i < taEntity.Columns.Count; i++)
            {// 组装参数
                ColumnAttributes caCurrent = taEntity.Columns[i] as ColumnAttributes;
                if(caCurrent.IsPrimaryKey)
                {
                    strKey        += caCurrent.ColumnName + " = " + caCurrent.Value + " ";
                    strKeyColumns += caCurrent.ColumnName + "=@" + caCurrent.ColumnName + " AND ";
                    
                }
                else if (caCurrent.IsVersionNumber)
                {
                    strOldVersion += caCurrent.ColumnName + "=@" + caCurrent.ColumnName + " AND ";
                }
                else
                {
                    continue;
                }

                SqlParameter paraCurrent = new SqlParameter("@" + caCurrent.ColumnName, caCurrent.Value);
                lstPara.Add(paraCurrent);
            }
			SqlParameter[] objPara = new SqlParameter[lstPara.Count];
			for(int i=0; i < lstPara.Count; i++)
			{
				objPara[i] = lstPara[i] as SqlParameter;
			}

            string strCondition = strKeyColumns + strOldVersion;
            string strSql = "DELETE FROM [TableName] WHERE [Condition]";
            // 替换当前表数据
            strSql = strSql.Replace("[TableName]",  taEntity.TableName);
            strSql = strSql.Replace("[Condition]",  strCondition.Substring(0, strCondition.Length - 5));

            int upCount = 0;
            if ( null != conn)
            {
                upCount = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, objPara);
            }
            else
            {
                upCount = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql, objPara);
            }

            if (upCount <= 0)
            {
                throw new ConcurrentException(taEntity.TableName, strKey);
            }

            return upCount;
        }

        #endregion
    }
}
