using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.IO;
using System.Xml.Serialization;

namespace DXInfo.Models
{
    public class ExcelLoadColumnConfiguration
    {
        public bool IsPrimary { get; set; }
        public bool IsChar { get; set; }
        /// <summary>
        /// 数据库列名
        /// </summary>
        public string DbColName { get; set; }
        /// <summary>
        /// EXCEL列名
        /// </summary>
        public string XlsColName { get; set; }
        /// <summary>
        /// 列描述
        /// </summary>
        public string ColDesc { get; set; }
        public bool IsLinkCol { get; set; }
        public string LinkTblName { get; set; }
        public string LinkSelColName { get; set; }
        public string LinkWhColName { get; set; }

        public bool IsDefaultValue{get;set;}
        public string DefaultValue{get;set;}//dr[""].tostring()+".jpg"
        public string DefaultValueXlsCol { get; set; }
        public string DefaultValueDbCol { get; set; }

        public bool IsReg { get; set; }
        public string MyReg { get; set; }

        public string Value { get; set; }

        public bool IsBoolean { get; set; }
        public string TrueValue { get; set; }
    }

    public class ExcelLoadTableConfiguration
    {
        /// <summary>
        /// 数据库表名
        /// </summary>
        public string DbTblName { get; set; }
        /// <summary>
        /// EXCEL sheet名
        /// </summary>
        public string XlsTblName { get; set; }
        /// <summary>
        /// 表描述
        /// </summary>
        public string TblDesc { get; set; }
        /// <summary>
        /// 列集合
        /// </summary>
        public List<ExcelLoadColumnConfiguration> lColumn { get; set; }
    }

    public class ExcelLoadConfiguration
    {
        /// <summary>
        /// 表集合
        /// </summary>
        public List<ExcelLoadTableConfiguration> lTable { get; set; }
    }
    
    public class ExcelLoad
    {
        public void InitConf(string path)
        {
            XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(DXInfo.Models.ExcelLoadConfiguration));
            ExcelLoadConfiguration xlc = new ExcelLoadConfiguration();
            xlc.lTable = new List<ExcelLoadTableConfiguration>();
            ExcelLoadTableConfiguration xltc = new ExcelLoadTableConfiguration();
            xltc.lColumn = new List<ExcelLoadColumnConfiguration>();
            ExcelLoadColumnConfiguration xlcc = new ExcelLoadColumnConfiguration();
            xltc.lColumn.Add(xlcc);
            xlc.lTable.Add(xltc);
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                xmlSerializer.Serialize(fs, xlc);
            }
        }
        
        private DataTable GetXlsData(string savePath,
            string sheetName)
        {
            string strConn;
            //strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + 
            //    savePath + ";" + "Extended Properties=Excel 8.0";
            strConn = "Provider=Microsoft.Ace.OleDb.12.0;" +
                "data source=" + savePath +
                ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();
                OleDbDataAdapter myCommand = new OleDbDataAdapter("select * from [" + sheetName + "$]", strConn);
                DataSet myDataSet = new DataSet();
                try
                {
                    myCommand.Fill(myDataSet, "ExcelInfo");
                }
                catch (Exception ex)
                {
                    throw new BusinessException(ex.Message);
                }
                DataTable table = myDataSet.Tables["ExcelInfo"].DefaultView.ToTable();
                return table;
            }
        }
        private string GetLinkValue(SqlConnection conn,
            ExcelLoadColumnConfiguration xlcc,
            string str)
        {
            using (SqlCommand cmd1 = new SqlCommand(string.Format("select " +
                        xlcc.LinkSelColName +
                        " from " + xlcc.LinkTblName +
                    " where " + xlcc.LinkWhColName + "={0}", str), conn))
            {
                object obj = cmd1.ExecuteScalar();
                if (obj == null) throw new DXInfo.Models.BusinessException("导入的文件中：" + str + xlcc.XlsColName + "不存在，请先添加该" + xlcc.XlsColName);
                return obj.ToString();
            }
        }
        private int GetCount(SqlConnection conn,
            ExcelLoadColumnConfiguration prixlcc, 
            string privalue)
        {
            using (SqlCommand cmd2 = new SqlCommand("select count(1) from " + 
                prixlcc.DbColName + "=" + privalue, conn))
            {
                object obj2 = cmd2.ExecuteScalar();
                int count = 0;
                if (obj2 != null)
                {
                    count = Convert.ToInt32(obj2);
                    if (count > 1)
                    {
                        throw new BusinessException(prixlcc.XlsColName + "为：" + privalue + "记录重复");
                    }
                }
                return count;
            }
        }
        private string CheckCol(DataRow dr,
            SqlConnection conn,
            ExcelLoadColumnConfiguration prixlcc, 
            ExcelLoadColumnConfiguration xlcc,
            string defaultValue)
        {
            string value = "";
            string str = "";
            if (!string.IsNullOrEmpty(xlcc.XlsColName))
            {
                str = dr[xlcc.XlsColName].ToString();
            }
            if (xlcc.IsLinkCol)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    value = GetLinkValue(conn, xlcc, str);
                }
            }
            else
            {
                //value = str;
                if (xlcc.IsDefaultValue)
                {
                    if (string.IsNullOrEmpty(str))
                    {
                        if (!string.IsNullOrEmpty(xlcc.DefaultValueXlsCol))
                        {
                            str = dr[xlcc.DefaultValueXlsCol].ToString() + xlcc.DefaultValue;
                        }
                        else
                        {
                            str = xlcc.DefaultValue;
                        }
                    }
                }
                if (xlcc.IsBoolean)
                {
                    if (string.IsNullOrEmpty(str))
                    {
                        str = "0";
                    }
                    else
                    {
                        str = str == xlcc.TrueValue ? "1" : "0";
                    }
                }
                value = string.Format(str, defaultValue);

                string myreg = "";
                switch (myreg)
                {
                    case "ImageFileName":
                        myreg = DXInfo.Models.MyReg.ImageFileName;
                        break;
                    case "PlusNumber":
                        myreg = DXInfo.Models.MyReg.PlusNumber;
                        break;
                }

                if (!Regex.IsMatch(value, myreg))
                {
                    throw new DXInfo.Models.BusinessException(prixlcc.XlsColName + "为：" + value + "的【" + xlcc.XlsColName + "】的格式不正确。");
                }
            }
            return value;
        }
        private string GetSql(SqlConnection conn,
            DataRow dr,
            ExcelLoadTableConfiguration xltc,
            ExcelLoadColumnConfiguration prixlcc,
            string defaultValue)
        {
            string privalue = dr[prixlcc.XlsColName].ToString();
            if (string.IsNullOrEmpty(privalue)) throw new Exception("请录入"+prixlcc.XlsColName);
            
            int count = GetCount(conn, prixlcc, privalue);
            string sql1 = string.Empty, 
                sql2 = string.Empty,
                sql3 = string.Empty, 
                sql4 = string.Empty, 
                sql5 = string.Empty,
                sql = string.Empty;
            if (count == 1)
            {
                //更新
                sql1 = "update " + xltc.DbTblName + "set ";
                if (!prixlcc.IsChar)
                {
                    sql3 = string.Format("where {0}={1}",prixlcc.DbColName,privalue);
                }
                else
                {
                    sql3 = string.Format("where {0}='{1}'", prixlcc.DbColName, privalue);
                }
                foreach (ExcelLoadColumnConfiguration xlcc in xltc.lColumn)
                {
                    //if (!string.IsNullOrEmpty(xlcc.DefaultValueDbCol))
                    //{
                    //    defaultValue = xltc.lColumn.Where(w => w.DbColName == xlcc.DefaultValueDbCol).Select(s => s.Value).FirstOrDefault();
                    //}
                    string value = CheckCol(dr, conn, prixlcc, xlcc,defaultValue);
                    //xlcc.Value = value;
                    if (!xlcc.IsChar)
                    {
                        sql2 += string.Format("{0}={1},", xlcc.DbColName,value);
                    }
                    else
                    {
                        sql2 += string.Format("{0}='{1}',", xlcc.DbColName, value);
                    }
                    //inv.InvType = 2;
                }
                sql = sql1 + sql2 + sql3;
            }
            else
            {
                //插入
                sql1 = "insert into " + xltc.DbTblName + "(";
                sql2 = "";
                sql3 = ")values(";
                sql4 = "";
                sql5 = ")";
                foreach (ExcelLoadColumnConfiguration xlcc in xltc.lColumn)
                {
                    sql2 += xlcc.DbColName + ",";
                    string value = CheckCol(dr, conn, prixlcc, xlcc,defaultValue);
                    if (!xlcc.IsChar)
                    {
                        sql4 += value + ",";
                    }
                    else
                    {
                        sql4 += string.Format("'{0}',", value);
                    }

                    //inv.InvType = 2;
                }
                sql = sql1 + sql2 + sql3 + sql4 + sql5;
            }
            return sql;
        }
        private void CheckTbl(DataTable table, 
            ExcelLoadColumnConfiguration prixlcc)
        {
            //判断条码重复
            var groupedData = (from b in table.AsEnumerable()
                               group b by b.Field<string>(prixlcc.XlsColName) into g
                               select new
                               {
                                   Code = g.Key,
                                   Count = g.Count(),
                               }).ToList().Where(w => w.Count > 1).Select(s => s.Code).ToList();
            if (groupedData.Count > 0)
            {
                string retstr = groupedData.Aggregate(delegate(string str1, string str2) { return str1 + str2; });
                throw new DXInfo.Models.BusinessException("以下" + prixlcc.XlsColName + "重复：" + retstr);
            }
        }
        public void LoadExcel(string connStr,
            string savePath,
            string defaultValue,
            string configurationPath,
            string tableName)
        {
            XmlSerializer xmlSerializer= new System.Xml.Serialization.XmlSerializer(typeof(DXInfo.Models.ExcelLoadConfiguration));
            ExcelLoadConfiguration xlc;
            using (FileStream fs = new FileStream(configurationPath, FileMode.Open))
            {
                xlc = (ExcelLoadConfiguration)xmlSerializer.Deserialize(fs);
            }
            if (xlc == null || xlc.lTable==null || xlc.lTable.Count==0)
            {
                throw new DXInfo.Models.BusinessException("无配置文件");
            }
            List<ExcelLoadTableConfiguration> lTable = xlc.lTable.Where(w => w.DbTblName == tableName).ToList();
            if(lTable==null||lTable.Count!=1)
            {
                throw new DXInfo.Models.BusinessException("表设置只能有且必须有一个");
            }
            ExcelLoadTableConfiguration xltc = lTable[0];
            if (xltc == null || xltc.lColumn == null || xltc.lColumn.Count == 0)
            {
                throw new DXInfo.Models.BusinessException("无表或者列设置");
            }
            List<ExcelLoadColumnConfiguration> lPriColumn = xltc.lColumn.Where(w => w.IsPrimary).ToList();
            if(lPriColumn==null||lPriColumn.Count!=1)
            {
                throw new DXInfo.Models.BusinessException("主键设置不正确，主键只能有一个且必须一个");
            }
            ExcelLoadColumnConfiguration prixlcc=lPriColumn[0];
            DataTable table = GetXlsData(savePath, xltc.XlsTblName);
            CheckTbl(table, prixlcc);
            try
            {
                List<string> lSql = new List<string>();
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    foreach (DataRow dr in table.Rows)
                    {
                        string sql = GetSql(conn, dr, xltc, prixlcc, defaultValue);
                        if (!string.IsNullOrEmpty(sql))
                        {
                            lSql.Add(sql);
                        }
                    }
                }
                if (lSql.Count > 0)
                {
                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();
                        SqlTransaction trans = conn.BeginTransaction();
                        cmd.Connection = conn;
                        cmd.Transaction = trans;
                        try
                        {
                            foreach (string sql in lSql)
                            {
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }
                            trans.Commit();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw ex;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.InnerException != null)
                    {
                        throw new BusinessException(ex.InnerException.InnerException.Message);
                    }
                    throw new BusinessException(ex.InnerException.Message);
                }
                throw new BusinessException(ex.Message);
            }
        }
    }
}
