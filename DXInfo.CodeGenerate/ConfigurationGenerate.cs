using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom;
using System.Data.SqlClient;
using System.Data;

namespace DXInfo.CodeGenerate
{
    public class ConfigurationGenerate : GenerateBase
    {
        public string TableName { get; set; }
        public string TableStructSql { get; set; }
        public SqlConnection conn;
        public ConfigurationGenerate(CodeNamespaceImport[] lcnmSpace, 
            string nmSpace,string tableName, 
            string tableStructSql,
            SqlConnection conn,
            string className, CodeTypeReference[] lBaseClass, string generatePath)
            :base(lcnmSpace,nmSpace,className,lBaseClass,generatePath)
        {
            this.TableName = tableName;
            this.TableStructSql = tableStructSql;
            this.conn = conn;
        }

        public override void AddConstructor()
        {
            CodeConstructor constructorMethod = new CodeConstructor();
            constructorMethod.Attributes = MemberAttributes.Public;
            constructorMethod.Name = TableName + "Configuration";

            string strsql = string.Format(TableStructSql, TableName);
            SqlCommand command = new SqlCommand(strsql, conn);
            SqlDataAdapter ad = new SqlDataAdapter(command);
            DataSet ds1 = new DataSet();
            ad.Fill(ds1);
            DataTable dtKey = ds1.Tables[0].Clone();
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                bool isKey = Convert.ToBoolean(dr1["key"]);
                if (isKey)
                {
                    dtKey.Rows.Add(dr1.ItemArray);
                }
            }
            if (dtKey.Rows.Count == 1)
            {
                string propertyName = dtKey.Rows[0]["fieldName"].ToString();
                CodeMethodInvokeExpression m2 = new CodeMethodInvokeExpression(new CodeSnippetExpression("this"), "HasKey", new CodeSnippetExpression("k => k." + propertyName));
                constructorMethod.Statements.Add(m2);
            }
            else if (dtKey.Rows.Count > 1)
            {
                string tt = "k => new { ";
                foreach (DataRow dr1 in dtKey.Rows)
                {
                    string propertyName = dr1["fieldName"].ToString();
                    tt += "k." + propertyName + ",";
                }
                tt = tt.Substring(0, tt.Length - 1);
                tt += " }";
                CodeMethodInvokeExpression m2 = new CodeMethodInvokeExpression(new CodeSnippetExpression("this"), "HasKey",
                    new CodeSnippetExpression(tt));
                constructorMethod.Statements.Add(m2);
            }
            foreach (DataRow dr1 in ds1.Tables[0].Rows)
            {
                string propertyName = dr1["fieldName"].ToString();
                //精度
                int scale = Convert.ToInt32(dr1["Scale"]);
                int Precision = Convert.ToInt32(dr1["Precision"]);
                string typeName = dr1["type"].ToString();
                if (scale > 0 && typeName != "datetime" && typeName != "time")
                {
                    CodeMethodInvokeExpression m2 = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("this.Property(o => o." + propertyName + ")"), "HasPrecision", new CodeSnippetExpression(dr1["Precision"].ToString() + "," + dr1["Scale"].ToString()));
                    constructorMethod.Statements.Add(m2);
                }
                //identity
                string defaultValue = dr1["defaultValue"].ToString();
                bool isIdentity = Convert.ToBoolean(dr1["identity"]);
                if (defaultValue == "(newsequentialid())" || isIdentity)
                {
                    CodeMethodInvokeExpression m2 = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("this.Property(o => o." + propertyName + ")"), "HasDatabaseGeneratedOption", new CodeSnippetExpression("DatabaseGeneratedOption.Identity"));
                    constructorMethod.Statements.Add(m2);
                }
                //是否可空
                bool isNull = Convert.ToBoolean(dr1["isNull"]);
                if (!isNull)
                {
                    CodeMethodInvokeExpression m2 = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("this.Property(o => o." + propertyName + ")"), "IsRequired");
                    constructorMethod.Statements.Add(m2);
                }
                //unicode
                if (typeName == "nvarchar")
                {
                    CodeMethodInvokeExpression m2 = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("this.Property(o => o." + propertyName + ")"), "IsUnicode");
                    constructorMethod.Statements.Add(m2);
                }
                //字符串长度
                if ((typeName == "nvarchar" || typeName == "varchar" || typeName == "char" || typeName == "ntext") && Precision > -1)
                {
                    CodeMethodInvokeExpression m2 = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("this.Property(o => o." + propertyName + ")"), "HasMaxLength", new CodeSnippetExpression(dr1["Precision"].ToString()));
                    constructorMethod.Statements.Add(m2);
                }
            }
            this.MyClass.Members.Add(constructorMethod);
        }
    }
}
