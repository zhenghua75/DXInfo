using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace DXInfo.CodeGenerate
{
    public class DXInfoGenerate
    {
        #region 表结构查询语句
        private static readonly string tableStructSql = @"
SELECT 
    tableName       = d.name,
    tableDescription     = isnull(f.value,''),
    colOrder   = a.colorder-1,
    fieldName     = a.name,
    [identity]       = case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then convert(bit,1) else convert(bit,0) end,
    [key]       = case when exists(SELECT 1 FROM sysobjects where xtype='PK' and parent_obj=a.id and name in (
                     SELECT name FROM sysindexes WHERE indid in( SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid))) then convert(bit,1) else convert(bit,0) end,
    [type]       = b.name,
    [byte] = a.length,
    [Precision]       = COLUMNPROPERTY(a.id,a.name,'PRECISION'),
    [Scale]   = isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0),
    [isNull]     = case when a.isnullable=1 then convert(bit,1) else convert(bit,0) end,
    [defaultValue]     = isnull(e.text,''),
    [fieldDescription]   = isnull(g.[value],'')
FROM 
    syscolumns a
left join 
    systypes b 
on 
    a.xusertype=b.xusertype
inner join 
    sysobjects d 
on 
    a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties'
left join 
    syscomments e 
on 
    a.cdefault=e.id
left join 
sys.extended_properties   g 
on 
    a.id=G.major_id and a.colid=g.minor_id  
left join 

sys.extended_properties f
on 
    d.id=f.major_id and f.minor_id=0
where  d.name ='{0}'
order by a.id,a.colorder
";
        #endregion

        public string TableListSql { get; private set; }
        public string ConnectiongStringName { get; private set; }
        public DXInfoGenerate(string tableListSql, string connectionStringName)
        {
            this.TableListSql = tableListSql;
            this.ConnectiongStringName = connectionStringName;
        }
        public void GenerateCode()
        {
            modelGenerate();
            dbContextGenerate();
            iUomGenerate();
            UomGenerate();
        }
        private void modelGenerate()
        {
            string modelFilePath = ConfigurationManager.AppSettings["ModelFilePath"];
            modelFilePath = Path.Combine(modelFilePath, this.ConnectiongStringName);
            if (!Directory.Exists(modelFilePath))
            {
                Directory.CreateDirectory(modelFilePath);
            }
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectiongStringName].ConnectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(this.TableListSql, conn);
            SqlDataAdapter ad = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            ad.Fill(ds);

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string tableName = dr[0].ToString();

                CodeNamespaceImport[] lcnmSpace = new CodeNamespaceImport[2];
                lcnmSpace[0] = new CodeNamespaceImport("System");
                lcnmSpace[1] = new CodeNamespaceImport("System.Runtime.Serialization");
                string nmSpace = "DXInfo.Models";
                string className = tableName;

                CodeTypeReference[] baseClass = new CodeTypeReference[1];
                baseClass[0] = new CodeTypeReference("Entity");

                ModelGenerate mg = new ModelGenerate(lcnmSpace, nmSpace, className, baseClass, modelFilePath);

                //strsql = "sp_pkeys   '"+tableName+"'";
                //command = new SqlCommand(strsql, conn);
                //command.CommandType = CommandType.StoredProcedure;

                //ad = new SqlDataAdapter(command);
                //DataSet ds1 = new DataSet();
                //ad.Fill(ds1);
                //if (ds1.Tables[0].Rows.Count > 0)
                //{
                //    //有主键可以进行代码生成
                //}
                string strsql = string.Format(tableStructSql, tableName);
                command = new SqlCommand(strsql, conn);
                ad = new SqlDataAdapter(command);
                DataSet ds1 = new DataSet();
                ad.Fill(ds1);

                foreach (DataRow dr1 in ds1.Tables[0].Rows)
                {
                    string fieldName = "_" + dr1["fieldName"].ToString();
                    string propertyName = dr1["fieldName"].ToString();
                    string typeName = getType(dr1["type"].ToString(), Convert.ToBoolean(dr1["isNull"]));
                    mg.AddField(fieldName, typeName);
                    mg.AddProperty(propertyName, fieldName, typeName);
                }
                mg.GenerateCSharpCode();
            }
            conn.Close();
        }

        private string getType(string databaseTypeName, bool isNull)
        {
            string typeName = "";
            switch (databaseTypeName)
            {
                case "bigint":
                    typeName = "Int64";
                    break;
                case "bit":
                    typeName = "Boolean";
                    break;
                case "char":
                    typeName = "String";
                    break;
                case "date":
                    typeName = "DateTime";
                    break;
                case "datetime":
                    typeName = "DateTime";
                    break;
                case "decimal":
                    typeName = "Decimal";
                    break;
                case "image":
                    typeName = "Byte[]";
                    break;
                case "int":
                    typeName = "Int32";
                    break;
                case "ntext":
                    typeName = "String";
                    break;
                case "numeric":
                    typeName = "Decimal";
                    break;
                case "nvarchar":
                    typeName = "String";
                    break;
                case "time":
                    typeName = "TimeSpan";
                    break;
                case "timestamp":
                    typeName = "Byte[]";
                    break;
                case "uniqueidentifier":
                    typeName = "Guid";
                    break;
                case "varbinary":
                    typeName = "Byte[]";
                    break;
                case "varchar":
                    typeName = "String";
                    break;
                case "xml":
                    typeName = "String";
                    break;
                case "text":
                    typeName = "String";
                    break;
                default:
                    throw new ArgumentException("未定义C#类型", databaseTypeName);

            }
            if (isNull && typeName != "String" && typeName != "Byte[]")
                typeName += "?";
            return typeName;
        }

        private void dbContextGenerate()
        {
            string dbContextFilePath = ConfigurationManager.AppSettings["DbContextFilePath"];
            string configurationFilePath = ConfigurationManager.AppSettings["ConfigurationFilePath"];

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectiongStringName].ConnectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(this.TableListSql, conn);
            SqlDataAdapter ad = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            ad.Fill(ds);

            CodeNamespaceImport[] lcnmSpace = new CodeNamespaceImport[9];
            lcnmSpace[0] = new CodeNamespaceImport("System");
            lcnmSpace[1] = new CodeNamespaceImport("System.Collections.Generic");
            lcnmSpace[2] = new CodeNamespaceImport("System.Linq");
            lcnmSpace[3] = new CodeNamespaceImport("System.Text");
            lcnmSpace[4] = new CodeNamespaceImport("System.Data.Entity");
            lcnmSpace[5] = new CodeNamespaceImport("DXInfo.Models");
            lcnmSpace[6] = new CodeNamespaceImport("System.Data.Entity.ModelConfiguration.Conventions");
            lcnmSpace[7] = new CodeNamespaceImport("System.ComponentModel.DataAnnotations.Schema");
            lcnmSpace[8] = new CodeNamespaceImport("DXInfo.Data.Configuration");

            string nmSpace = "DXInfo.Data";
            //string className = dbName;// +"DbContext";

            CodeTypeReference[] baseClass = new CodeTypeReference[1];
            baseClass[0] = new CodeTypeReference("DbContext");

            DbContextGenerate dg = new DbContextGenerate(lcnmSpace, nmSpace, this.ConnectiongStringName, baseClass, dbContextFilePath);
            dg.AddConstructor();
            //dg.AddMethod();

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    string tableName = dr[0].ToString();
            //    dg.AddField(tableName, "_" + tableName);
            //}
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string tableName = dr[0].ToString();
                dg.AddProperty(tableName,"_"+tableName);
            }
            CodeStatementCollection csc = new CodeStatementCollection();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string tableName = dr[0].ToString();

                configurationGenerate(configurationFilePath, tableName, conn);
                CodeMethodInvokeExpression m2 = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("modelBuilder.Configurations"), "Add", new CodeSnippetExpression("new "+tableName+"Configuration()"));
                csc.Add(m2);
                //string strsql = string.Format(tableStructSql, tableName);
                //command = new SqlCommand(strsql, conn);
                //ad = new SqlDataAdapter(command);
                //DataSet ds1 = new DataSet();
                //ad.Fill(ds1);
                //DataTable dtKey = ds1.Tables[0].Clone();
                //foreach (DataRow dr1 in ds1.Tables[0].Rows)
                //{
                //    bool isKey = Convert.ToBoolean(dr1["key"]);
                //    if (isKey)
                //    {
                //        dtKey.Rows.Add(dr1.ItemArray);
                //    }
                //}
                //if (dtKey.Rows.Count == 1)
                //{
                //    string propertyName = dtKey.Rows[0]["fieldName"].ToString();
                //    CodeMethodInvokeExpression m2 = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("modelBuilder.Entity<" + tableName + ">()"), "HasKey", new CodeSnippetExpression("k => k." + propertyName));
                //    csc.Add(m2);
                //}
                //else if (dtKey.Rows.Count > 1)
                //{
                //    string tt = "k => new { ";
                //    foreach (DataRow dr1 in dtKey.Rows)
                //    {
                //        string propertyName = dr1["fieldName"].ToString();
                //        tt += "k." + propertyName + ",";
                //    }
                //    tt = tt.Substring(0, tt.Length - 1);
                //    tt += " }";
                //    CodeMethodInvokeExpression m2 = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("modelBuilder.Entity<" + tableName + ">()"), "HasKey",
                //        new CodeSnippetExpression(tt));
                //    csc.Add(m2);
                //}
                //foreach (DataRow dr1 in ds1.Tables[0].Rows)
                //{
                //    string propertyName = dr1["fieldName"].ToString();
                //    //精度
                //    int scale = Convert.ToInt32(dr1["Scale"]);
                //    int Precision = Convert.ToInt32(dr1["Precision"]);
                //    string typeName = dr1["type"].ToString();
                //    if (scale > 0 && typeName != "datetime" && typeName != "time")
                //    {
                //        CodeMethodInvokeExpression m2 = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("modelBuilder.Entity<" + tableName + ">().Property(o => o." + propertyName + ")"), "HasPrecision", new CodeSnippetExpression(dr1["Precision"].ToString() + "," + dr1["Scale"].ToString()));
                //        csc.Add(m2);
                //    }
                //    //identity
                //    string defaultValue = dr1["defaultValue"].ToString();
                //    bool isIdentity = Convert.ToBoolean(dr1["identity"]);
                //    if (defaultValue == "(newsequentialid())" || isIdentity)
                //    {
                //        CodeMethodInvokeExpression m2 = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("modelBuilder.Entity<" + tableName + ">().Property(o => o." + propertyName + ")"), "HasDatabaseGeneratedOption", new CodeSnippetExpression("DatabaseGeneratedOption.Identity"));
                //        csc.Add(m2);
                //    }
                //    //是否可空
                //    bool isNull = Convert.ToBoolean(dr1["isNull"]);
                //    if (!isNull)
                //    {
                //        CodeMethodInvokeExpression m2 = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("modelBuilder.Entity<" + tableName + ">().Property(o => o." + propertyName + ")"), "IsRequired");
                //        csc.Add(m2);
                //    }
                //    //unicode
                //    if (typeName == "nvarchar")
                //    {
                //        CodeMethodInvokeExpression m2 = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("modelBuilder.Entity<" + tableName + ">().Property(o => o." + propertyName + ")"), "IsUnicode");
                //        csc.Add(m2);
                //    }
                //    //字符串长度
                //    if ((typeName == "nvarchar" || typeName == "varchar" || typeName == "char"||typeName=="ntext") && Precision > -1)
                //    {
                //        CodeMethodInvokeExpression m2 = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("modelBuilder.Entity<" + tableName + ">().Property(o => o." + propertyName + ")"), "HasMaxLength", new CodeSnippetExpression(dr1["Precision"].ToString()));
                //        csc.Add(m2);
                //    }
                //}
            }
            dg.AddMethod(csc);
            dg.GenerateCSharpCode();
        }

        private void configurationGenerate(string configurationFilePath, string tableName, SqlConnection conn)
        {
            CodeNamespaceImport[] lcnmSpace = new CodeNamespaceImport[7];
            lcnmSpace[0] = new CodeNamespaceImport("System");
            lcnmSpace[1] = new CodeNamespaceImport("System.Collections.Generic");
            lcnmSpace[2] = new CodeNamespaceImport("System.Linq");
            lcnmSpace[3] = new CodeNamespaceImport("System.Text");
            lcnmSpace[4] = new CodeNamespaceImport("System.Data.Entity.ModelConfiguration");
            lcnmSpace[5] = new CodeNamespaceImport("DXInfo.Models");
            lcnmSpace[6] = new CodeNamespaceImport("System.ComponentModel.DataAnnotations.Schema");

            string nmSpace = "DXInfo.Data.Configuration";
            CodeTypeReference[] baseClass = new CodeTypeReference[1];
            baseClass[0] = new CodeTypeReference("EntityTypeConfiguration<"+tableName+">");

            ConfigurationGenerate cg = new ConfigurationGenerate(lcnmSpace, nmSpace, tableName, tableStructSql, conn, tableName + "Configuration", baseClass, configurationFilePath);
            cg.AddConstructor();
            cg.GenerateCSharpCode();
        }
        private void iUomGenerate()
        {
            string dbContextFilePath = ConfigurationManager.AppSettings["IUowFilePath"];
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectiongStringName].ConnectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(this.TableListSql, conn);
            SqlDataAdapter ad = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            ad.Fill(ds);

            CodeNamespaceImport[] lcnmSpace = new CodeNamespaceImport[5];
            lcnmSpace[0] = new CodeNamespaceImport("System");
            lcnmSpace[1] = new CodeNamespaceImport("System.Collections.Generic");
            lcnmSpace[2] = new CodeNamespaceImport("System.Linq");
            lcnmSpace[3] = new CodeNamespaceImport("System.Text");
            lcnmSpace[4] = new CodeNamespaceImport("DXInfo.Models");

            string nmSpace = "DXInfo.Data.Contracts";

            CodeTypeReference[] baseClass = new CodeTypeReference[1];
            baseClass[0] = new CodeTypeReference("IUow");

            IUowGenerate ig = new IUowGenerate(lcnmSpace, nmSpace, this.ConnectiongStringName, baseClass, dbContextFilePath);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string tableName = dr[0].ToString();
                ig.AddProperty(tableName);
            }
            
            ig.GenerateCSharpCode();
        }

        private void UomGenerate()
        {
            string dbContextFilePath = ConfigurationManager.AppSettings["UowFilePath"];
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings[this.ConnectiongStringName].ConnectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(this.TableListSql, conn);
            SqlDataAdapter ad = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            ad.Fill(ds);

            CodeNamespaceImport[] lcnmSpace = new CodeNamespaceImport[6];
            lcnmSpace[0] = new CodeNamespaceImport("System");
            lcnmSpace[1] = new CodeNamespaceImport("System.Collections.Generic");
            lcnmSpace[2] = new CodeNamespaceImport("System.Linq");
            lcnmSpace[3] = new CodeNamespaceImport("System.Text");
            lcnmSpace[4] = new CodeNamespaceImport("DXInfo.Data.Contracts");
            lcnmSpace[5] = new CodeNamespaceImport("DXInfo.Models");

            string nmSpace = "DXInfo.Data";

            CodeTypeReference[] baseClass = new CodeTypeReference[2];
            baseClass[0] = new CodeTypeReference("Uow<"+this.ConnectiongStringName+"DbContext>");
            baseClass[1] = new CodeTypeReference("I"+this.ConnectiongStringName+"Uow");
            UowGenerate ug = new UowGenerate(lcnmSpace, nmSpace, this.ConnectiongStringName, baseClass, dbContextFilePath);
            ug.AddConstructor();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string tableName = dr[0].ToString();
                ug.AddProperty(tableName);
            }

            ug.GenerateCSharpCode();
        }
    }
}
