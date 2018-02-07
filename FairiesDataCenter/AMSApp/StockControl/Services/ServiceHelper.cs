using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using AMSApp.zhenghua.Business;
using System.Web.Script.Serialization;
using System.Text;
using AMSApp.zhenghua.Common;
using System.Data.SqlClient;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Reflection;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq.Expressions;
using DXInfo.Data.Contracts;
using System.ComponentModel;

namespace AMSApp.StockControl.Services
{
     [DataContract]
     public class JEasyUIResult
    {
        public JEasyUIResult(bool success, string msg)
        {
            this.success = success;
            this.msg = msg.Replace("\r\n", "");
            this.rows = "[]";
            this.total = "0";
        }
         [DataMember]
        public bool success { get; set; }
         [DataMember]
        public string msg { get; set; }
         [DataMember]
         public string rows { get; set; }
         public string total { get; set; }
    }
     public class SelectOption
     {
         public string id { get; set; }
         public string text { get; set; }
     }
     public enum StockStatus
     {
         Create=0,
         Check=1,
         Delete=2
     }
     public enum StockType
     {
         /// <summary>
         /// 期初库存
         /// </summary>
         Init = 0,
         /// <summary>
         /// 采购入库
         /// </summary>
         Purchase = 1,
         /// <summary>
         /// 完工入库
         /// </summary>
         Complete = 2,
         /// <summary>
         /// 销售出库
         /// </summary>
         Sell = 3,
         /// <summary>
         /// 盘点
         /// </summary>
         Check = 4,
         /// <summary>
         /// 月结
         /// </summary>
         Balance = 5,
         /// <summary>
         /// 领料单
         /// </summary>
         Material=6,
         /// <summary>
         /// 调拨
         /// </summary>
         Transfer=7
     }
    /// <summary>
    /// 调拨方向
    /// </summary>
     public enum TransferDirection
     {
         In=1,
         Out=2
     }
     public class ServiceHelper
     {

         public const string Table_tbDept = "tbDept";
         public const string Table_tbLogin = "tbLogin";
         public const string Table_tbOper = "tbOper";
         public const string Table_tbNameCode = "tbNameCode";
         public const string Table_tbProductClass = "tbProductClass";
         public const string Table_tbComputationGroup = "tbComputationGroup";
         public const string Table_tbComputationUnit = "tbComputationUnit";
         public const string Table_tbInventory = "tbInventory";
         public const string Table_tbSupplier = "tbSupplier";
         public const string Table_tbWarehouse = "tbWarehouse";
         public const string ExceptionPolicy = "Policy";
         public const string LoginSessionKey = "Login";
         public const string SEMIPRODUCT = "SEMIPRODUCT";
         private static JavaScriptSerializer serializer;
         public static JavaScriptSerializer DataTableSerializer()
         {
             if (serializer == null)
             {
                 serializer = new JavaScriptSerializer();
                 serializer.RegisterConverters(new JavaScriptConverter[] { new DataTableConverter() });
             }
             return serializer;
         }
         public static string DataTableToEasyUIJson(DataTable dt)
         {

             //{"total":"6",
             //"rows":[
             //{"id":"7554","firstname":"asas","lastname":"asap","phone":"sasa","email":"asap@yahoo.com"},
             //{"id":"7555","firstname":"asas","lastname":"asa","phone":"sasaiii","email":"asaa@mail.com"},
             //{"id":"7558","firstname":"test","lastname":"test","phone":"test opoi","email":"test@yahoo.com"},
             //{"id":"7561","firstname":"teste","lastname":"teste","phone":"","email":""},
             //{"id":"7562","firstname":"Jacek","lastname":"Placek","phone":"","email":""},
             //{"id":"7563","firstname":"c","lastname":"vzx","phone":"zxv","email":"zxv@sffas.com"}
             //]}

             //string totalcount = dt.Rows.Count.ToString();
             StringBuilder rows = new StringBuilder();
             foreach (DataRow dr in dt.Rows)
             {
                 rows.Append("{");
                 foreach (DataColumn dc in dt.Columns)
                 {
                     rows.AppendFormat("\"{0}\":\"{1}\",", dc.ColumnName, dr[dc.ColumnName]);
                 }
                 if (rows.ToString().EndsWith(","))
                 {
                     rows.Remove(rows.Length - 1, 1);
                 }
                 rows.Append("},");
             }
             if (rows.ToString().EndsWith(","))
             {
                 rows.Remove(rows.Length - 1, 1);
             }
             StringBuilder sb = new StringBuilder();
             sb.AppendFormat("[{0}]", rows.ToString());
             return sb.ToString();
         }
         public static string DataTableToEasyUIDataGridJson(DataTable dt, string totalcount)
         {

             //{"total":"6",
             //"rows":[
             //{"id":"7554","firstname":"asas","lastname":"asap","phone":"sasa","email":"asap@yahoo.com"},
             //{"id":"7555","firstname":"asas","lastname":"asa","phone":"sasaiii","email":"asaa@mail.com"},
             //{"id":"7558","firstname":"test","lastname":"test","phone":"test opoi","email":"test@yahoo.com"},
             //{"id":"7561","firstname":"teste","lastname":"teste","phone":"","email":""},
             //{"id":"7562","firstname":"Jacek","lastname":"Placek","phone":"","email":""},
             //{"id":"7563","firstname":"c","lastname":"vzx","phone":"zxv","email":"zxv@sffas.com"}
             //]}

             //string totalcount = dt.Rows.Count.ToString();
             StringBuilder rows = new StringBuilder();
             foreach (DataRow dr in dt.Rows)
             {
                 rows.Append("{");
                 foreach (DataColumn dc in dt.Columns)
                 {
                     string str = "";
                     if (dc.DataType == typeof(bool))
                     {
                         str = Convert.ToBoolean(dr[dc.ColumnName]) ? "on" : "";
                     }
                     else
                     {
                         str = dr[dc.ColumnName].ToString();
                     }
                     rows.AppendFormat("\"{0}\":\"{1}\",", dc.ColumnName, str);
                 }
                 if (rows.ToString().EndsWith(","))
                 {
                     rows.Remove(rows.Length - 1, 1);
                 }
                 rows.Append("},");
             }
             if (rows.ToString().EndsWith(","))
             {
                 rows.Remove(rows.Length - 1, 1);
             }
             StringBuilder sb = new StringBuilder();
             sb.AppendFormat("{{\"total\":\"{0}\",\"rows\":[{1}]}}", totalcount, rows.ToString());
             return sb.ToString();
         }

         public static void FillApplication(HttpContext context)
         {
             if (context.Application[Table_tbDept] == null)
             {
                 context.Application[Table_tbDept] = Helper.Query("select * from tbDept");
             }
             if (context.Application[Table_tbLogin] == null)
             {
                 context.Application[Table_tbLogin] = Helper.Query("select * from tbLogin");
             }
             if (context.Application[Table_tbOper] == null)
             {
                 context.Application[Table_tbOper] = Helper.Query("select * from tbOper");
             }
             if (context.Application[Table_tbNameCode] == null)
             {
                 context.Application[Table_tbNameCode] = Helper.Query("select * from tbNameCode");
             }
             if (context.Application[Table_tbProductClass] == null)
             {
                 context.Application[Table_tbProductClass] = Helper.Query("select * from tbProductClass order by cnvcProducttype,cnvcproductclasscode");
             }
             if (context.Application[Table_tbComputationGroup] == null)
             {
                 context.Application[Table_tbComputationGroup] = Helper.Query("select * from tbComputationGroup");
             }
             if (context.Application[Table_tbComputationUnit] == null)
             {
                 context.Application[Table_tbComputationUnit] = Helper.Query("select * from tbComputationUnit");
             }
             if (context.Application[Table_tbInventory] == null)
             {
                 context.Application[Table_tbInventory] = Helper.Query("select * from tbInventory");
             }
             if (context.Application[Table_tbSupplier] == null)
             {
                 context.Application[Table_tbSupplier] = Helper.Query("select * from tbSupplier");
             }
             if (context.Application[Table_tbWarehouse] == null)
             {
                 context.Application[Table_tbWarehouse] = Helper.Query("select * from tbWarehouse");
             }
         }

         public static void DataTableConvert(HttpContext context, DataTable dt, string columnName,string newColumnName, string strApplicationName, string strIDColumnName, string strCommentsColumnName, string filter)
         {
             if (dt == null)
             {
                 throw new ArgumentNullException("DataTable");
             }
             string strTemp;
             string strCommentColumnName = newColumnName;
             //判断新列是否存在，已经存在就不添加，不存在就添加
             if (dt.Columns[strCommentColumnName] == null)
             {
                 dt.Columns.Add(strCommentColumnName, typeof(string));
             }
             FillApplication(context);
             DataTable dt2 = (DataTable)context.Application[strApplicationName];
             DataView dv = new DataView(dt2);
             if (dt2 == null)
             {
                 throw new Exception("Application 中代码没有找到！");
             }

             foreach (DataRow dr in dt.Rows)
             {
                 strTemp = CodeConvert(context, dv, strIDColumnName, dr[columnName].ToString(), strCommentsColumnName, filter);
                 dr[strCommentColumnName] = strTemp;
             }
         }
         public static void DataTableConvert(HttpContext context, DataTable dt, string columnName, string strApplicationName, string strIDColumnName, string strCommentsColumnName)
         {
             DataTableConvert(context, dt, columnName, columnName + "Comments", strApplicationName, strIDColumnName, strCommentsColumnName, "");
         }
         public static void DataTableConvert(HttpContext context, DataTable dt, string columnName, string strApplicationName, string strIDColumnName, string strCommentsColumnName, string filter)
         {
             DataTableConvert(context, dt, columnName, columnName + "Comments", strApplicationName, strIDColumnName, strCommentsColumnName, filter);
         }
         private static string CodeConvert(HttpContext context, DataView dw, string strIDColumnName, string selectId, string strCommentsColumnName, string strfilter)
         {
             string strRemark;             
             if (strfilter == "")
             {
                 strfilter = strIDColumnName + " = '" + selectId + "'";
             }
             else
             {
                 strfilter = strfilter + " and " + strIDColumnName + " = '" + selectId + "'";
             }
             dw.RowFilter = strfilter;
             if (dw.Count == 1)
             {
                 strRemark = dw[0].Row[strCommentsColumnName].ToString();
             }
             else
             {
                 strRemark = "";
             }
             return strRemark;
         }
         public static void DataTableConvert(HttpContext context,DataTable dt,string columnName,string newColumnName)
         {
             if (dt == null)
             {
                 throw new ArgumentNullException("DataTable");
             }
             string strCommentColumnName = newColumnName;
             //判断新列是否存在，已经存在就不添加，不存在就添加
             if (dt.Columns[strCommentColumnName] == null)
             {
                 dt.Columns.Add(strCommentColumnName, typeof(string));
             }
             foreach (DataRow dr in dt.Rows)
             {
                 dr[strCommentColumnName] = Convert.ToBoolean(dr[columnName])?"是":"否";
             }
         }
         public static void DataTableConvert(HttpContext context, DataTable dt, string columnName)
         {
             DataTableConvert(context, dt, columnName, columnName + "Comments");
         }
         public static DataTable QueryPage(int page, int rows, string tablename, string orderstr, out string totalRecord)
         {
             SqlConnection conn = ConnectionPool.BorrowConnection();
             SqlCommand cmd = new SqlCommand();
             if (conn.State != ConnectionState.Open)
             {
                 conn.Open();
             }
             cmd.Connection = conn;
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.CommandText = "proc_select_page_row";
             cmd.Parameters.Add("@pageindex", SqlDbType.Int).Value = page;
             cmd.Parameters.Add("@pagesize", SqlDbType.Int).Value = rows;
             cmd.Parameters.Add("@tablename", SqlDbType.VarChar, 50).Value = tablename;
             cmd.Parameters.Add("@fields", SqlDbType.VarChar, 1000).Value = "*";
             cmd.Parameters.Add("@keyid", SqlDbType.VarChar, 50).Value = "";
             cmd.Parameters.Add("@condition", SqlDbType.VarChar, 1000).Value = "";
             cmd.Parameters.Add("@orderstr", SqlDbType.VarChar, 500).Value = orderstr;
             System.Data.SqlClient.SqlParameter parameter1 =
                         cmd.Parameters.Add("@totalRecord", System.Data.SqlDbType.Int);
             parameter1.Direction = System.Data.ParameterDirection.Output;
             SqlDataAdapter da = new SqlDataAdapter(cmd);
             DataTable dt = new DataTable();
             try
             {
                 da.Fill(dt);
             }
             catch (SqlException se)
             {
                 throw se;
             }
             totalRecord = parameter1.Value.ToString();
             return dt;
         }

         public static string JsonSerializer<T>(T t)
         {
             DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
             MemoryStream ms = new MemoryStream();
             ser.WriteObject(ms, t);
             string jsonString = Encoding.UTF8.GetString(ms.ToArray());
             ms.Close();
             //替换Json的Date字符串
             string p = @"\\/Date\((\d+)\+\d+\)\\/";
             MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
             Regex reg = new Regex(p);
             jsonString = reg.Replace(jsonString, matchEvaluator);
             return jsonString;
         }
         public static T JsonDeserialize<T>(string jsonString)
         {
             //将"yyyy-MM-dd HH:mm:ss"格式的字符串转为"\/Date(1294499956278+0800)\/"格式
             string p = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}";
             MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);
             Regex reg = new Regex(p);
             jsonString = reg.Replace(jsonString, matchEvaluator);
             DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
             MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
             T obj = (T)ser.ReadObject(ms);
             return obj;
         }
         private static string ConvertJsonDateToDateString(Match m)
         {
             string result = string.Empty;
             DateTime dt = new DateTime(1970, 1, 1);
             dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
             dt = dt.ToLocalTime();
             result = dt.ToString("yyyy-MM-dd HH:mm:ss");
             return result;
         }
         private static string ConvertDateStringToJsonDate(Match m)
         {
             string result = string.Empty;
             DateTime dt = DateTime.Parse(m.Groups[0].Value);
             dt = dt.ToUniversalTime();
             TimeSpan ts = dt - DateTime.Parse("1970-01-01");
             result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
             return result;
         }


         public static string Obj2Json<T>(T data)
         {
             try
             {
                 System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(data.GetType());
                 using (MemoryStream ms = new MemoryStream())
                 {
                     serializer.WriteObject(ms, data);
                     return Encoding.UTF8.GetString(ms.ToArray());
                 }
             }
             catch
             {
                 return null;
             }
         }
         public static Object Json2Obj(String json, Type t)
         {
             try
             {
                 System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(t);
                 using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                 {

                     return serializer.ReadObject(ms);
                 }
             }
             catch
             {
                 return null;
             }
         }
         public static T Json2Obj<T>(string json)
         {
             T obj = Activator.CreateInstance<T>();
             using (System.IO.MemoryStream ms = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(json)))
             {
                 System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(obj.GetType());
                 return (T)serializer.ReadObject(ms);
             }
         }


         public static void DoExportToExcel(HttpContext context,string fileName, GridView view)
         {

             context.Response.ClearContent();
             context.Response.AddHeader("content-disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8).ToString());
             context.Response.ContentType = "application/excel";
             StringWriter writer = new StringWriter();
             HtmlTextWriter writer2 = new HtmlTextWriter(writer);
             view.RenderControl(writer2);
             context.Response.Write(writer.ToString());
             context.Response.End();
         }

         public static void SetEntity<S,D>(S source, D dest)
         {
             Type tSource = source.GetType();
             Type tDest = dest.GetType();
             PropertyInfo[] sPis = tSource.GetProperties();
             foreach (PropertyInfo pi in sPis)
             {
                 PropertyInfo dPi = tDest.GetProperty(pi.Name);
                 if (dPi != null)
                 {
                     dPi.SetValue(dest, pi.GetValue(source,null),null);
                 }
             }
         }
         public static void AddStockDetal(IAMSCMUow uow, DXInfo.Models.tbStockMain tbStockMain, string cnvcInvCode
            , string cnvcComUnitCode, decimal cnnQuantity, string cnvcMainComUnitCode, decimal cnnMainQuantity, decimal cnnPrice, decimal cnnAmount)
         {
             DXInfo.Models.tbStockDetail tbStockDetail = new DXInfo.Models.tbStockDetail();
             tbStockDetail.cnnMainId = tbStockMain.cnnMainId;
             tbStockDetail.cnvcInvCode = cnvcInvCode;
             tbStockDetail.cnvcComUnitCode = cnvcComUnitCode;
             tbStockDetail.cnnQuantity = cnnQuantity;
             tbStockDetail.cnvcMainComUnitCode = cnvcMainComUnitCode;
             tbStockDetail.cnnMainQuantity = cnnMainQuantity;
             tbStockDetail.cnnPrice = cnnPrice;
             tbStockDetail.cnnAmount = cnnAmount;
             tbStockDetail.cndOperDate = tbStockMain.cndCreateDate;
             tbStockDetail.cnvcOper = tbStockMain.cnvcCreaterId;
             tbStockDetail.cnvcOperName = tbStockMain.cnvcCreaterName;

             uow.tbStockDetail.Add(tbStockDetail);
             uow.Commit();

             DXInfo.Models.tbStockDetailLog tbStockDetailLog = new DXInfo.Models.tbStockDetailLog();
             ServiceHelper.SetEntity<DXInfo.Models.tbStockDetail, DXInfo.Models.tbStockDetailLog>(tbStockDetail, tbStockDetailLog);
             uow.tbStockDetailLog.Add(tbStockDetailLog);
         }
         public static List<DXInfo.Models.tbBillOfMaterials> getBOM(IAMSCMUow uow)
         {
             List<DXInfo.Models.tbBillOfMaterials> ltbBillOfMaterials = (from d in uow.tbBillOfMaterials.GetAll() select d).ToList();
             ltbBillOfMaterials.ForEach(delegate(DXInfo.Models.tbBillOfMaterials tbBillOfMaterials)
             {
                 tbBillOfMaterials.cnnBaseQtyN = tbBillOfMaterials.cnnBaseQtyD / tbBillOfMaterials.cnnBaseQtyD;
                 tbBillOfMaterials.cnnBaseQtyD = 1;
             });
             return ltbBillOfMaterials;
         }
         public static List<DXInfo.Models.tbBillOfMaterials> ProcBOM(List<DXInfo.Models.tbBillOfMaterials> ltbBillOfMaterials, string strinvcode)
         {

             List<DXInfo.Models.tbBillOfMaterials> lComponentInv = new List<DXInfo.Models.tbBillOfMaterials>();
             var l = (from d in ltbBillOfMaterials where d.cnvcPartInvCode == strinvcode select d).ToList();
             foreach (DXInfo.Models.tbBillOfMaterials bom in l)
             {
                 getComponent(ltbBillOfMaterials, bom, lComponentInv,1);
             }
             return lComponentInv;
         }
         private static void getComponent(List<DXInfo.Models.tbBillOfMaterials> ltbBillOfMaterials, DXInfo.Models.tbBillOfMaterials bom, List<DXInfo.Models.tbBillOfMaterials> lComponentInv,decimal quantity)
         {
             var l = (from d in ltbBillOfMaterials where d.cnvcPartInvCode == bom.cnvcComponentInvCode select d);
             if (l.Count() > 0)
             {
                 foreach (DXInfo.Models.tbBillOfMaterials bom1 in l)
                 {
                     bom1.cnnBaseQtyN = bom1.cnnBaseQtyN * quantity;
                     getComponent(ltbBillOfMaterials, bom1, lComponentInv,bom1.cnnBaseQtyN);
                 }
             }
             else
             {
                 lComponentInv.Add(bom);
             }
         }
         public static List<DXInfo.Models.tbBillOfMaterials> ProcBOM2(List<DXInfo.Models.tbBillOfMaterials> ltbBillOfMaterials, string strinvcode)
         {

             List<DXInfo.Models.tbBillOfMaterials> lComponentInv = new List<DXInfo.Models.tbBillOfMaterials>();
             var l = (from d in ltbBillOfMaterials where d.cnvcPartInvCode == strinvcode select d).ToList();
             foreach (DXInfo.Models.tbBillOfMaterials bom in l)
             {
                 getComponent2(ltbBillOfMaterials, bom, lComponentInv);
             }
             return lComponentInv;
         }
         private static void getComponent2(List<DXInfo.Models.tbBillOfMaterials> ltbBillOfMaterials, DXInfo.Models.tbBillOfMaterials bom, List<DXInfo.Models.tbBillOfMaterials> lComponentInv)
         {
             var l = (from d in ltbBillOfMaterials where d.cnvcPartInvCode == bom.cnvcComponentInvCode select d).ToList();
             if (l.Count > 0)
             {
                 lComponentInv.Add(bom);
                 foreach (DXInfo.Models.tbBillOfMaterials bom1 in l)
                 {
                     getComponent2(ltbBillOfMaterials, bom1, lComponentInv);
                 }
             }
         }
         public static void SyncGoods(DXInfo.Models.tbInventory inv, IAMSCMUow uow)
         {
             DXInfo.Models.tbGoods tbGoods = uow.tbGoods.GetById(g=>g.vcGoodsID==inv.cnvcInvCode);
             if (inv.cnbSale)
             {
                 if (tbGoods == null)
                 {
                     DXInfo.Models.tbGoods gs = new DXInfo.Models.tbGoods();
                     gs.vcGoodsID = inv.cnvcInvCode;
                     gs.vcGoodsName = inv.cnvcInvName;
                     gs.vcSpell = Helper.GetChineseSpell(inv.cnvcInvName);
                     gs.nPrice = Convert.ToDecimal(inv.cnfRetailPrice);
                     gs.nRate = 0;
                     gs.iIgValue = -1;
                     gs.cNewFlag = "0";
                     gs.vcComments = "存货档案添加同步";
                     uow.tbGoods.Add(gs);
                 }
                 else
                 {
                     if (tbGoods.vcGoodsName != inv.cnvcInvName)
                     {
                         tbGoods.vcGoodsName = inv.cnvcInvName;
                         tbGoods.vcSpell = Helper.GetChineseSpell(inv.cnvcInvName);
                     }
                     if (inv.cnfRetailPrice > 0)
                         tbGoods.nPrice = Convert.ToDecimal(inv.cnfRetailPrice);
                     tbGoods.vcComments = "存货档案修改同步";
                 }
             }
             else
             {
                 if (tbGoods != null)
                 {
                     uow.tbGoods.Delete(tbGoods);
                 }
             }
         }
     }
     public static class DataTableExtensions2
     {
         public static DataTable ToDataTable<T>(this IList<T> data)
         {
             PropertyDescriptorCollection props =
                 TypeDescriptor.GetProperties(typeof(T));
             DataTable table = new DataTable();
             for (int i = 0; i < props.Count; i++)
             {
                 PropertyDescriptor prop = props[i];
                 table.Columns.Add(prop.Name, prop.PropertyType);
             }
             object[] values = new object[props.Count];
             foreach (T item in data)
             {
                 for (int i = 0; i < values.Length; i++)
                 {
                     values[i] = props[i].GetValue(item);
                 }
                 table.Rows.Add(values);
             }
             return table;
         }
     }
    public static class DataTableExtensions
    {
        /// <summary>
        /// 转化一个DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> list)
        {
            //创建属性的集合
            List<PropertyInfo> pList = new List<PropertyInfo>();
            //获得反射的入口
            Type type = typeof(T);
            DataTable dt = new DataTable();
            //把所有的public属性加入到集合 并添加DataTable的列
            Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); 
                 if(p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition()==typeof(Nullable<>))
                 dt.Columns.Add(p.Name, Nullable.GetUnderlyingType(p.PropertyType));
                 else
                dt.Columns.Add(p.Name, p.PropertyType); });
            foreach (var item in list)
            {
                //创建一个DataRow实例
                DataRow row = dt.NewRow();
                //给row 赋值
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null) == null ? DBNull.Value : p.GetValue(item, null));
                //加入到DataTable
                dt.Rows.Add(row);
            }
            return dt;
        }

        /// <summary>
        /// DataTable 转换为List 集合
        /// </summary>
        /// <typeparam name="TResult">类型</typeparam>
        /// <param name="dt">DataTable</param>
        /// <returns></returns>
        public static List<T> ToList<T>(this DataTable dt) where T : class, new()
        {
            //创建一个属性的列表
            List<PropertyInfo> prlist = new List<PropertyInfo>();
            //获取TResult的类型实例  反射的入口
            Type t = typeof(T);
            //获得TResult 的所有的Public 属性 并找出TResult属性和DataTable的列名称相同的属性(PropertyInfo) 并加入到属性列表 
            Array.ForEach<PropertyInfo>(t.GetProperties(), p => { if (dt.Columns.IndexOf(p.Name) != -1) prlist.Add(p); });
            //创建返回的集合
            List<T> oblist = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                //创建TResult的实例
                T ob = new T();
                //找到对应的数据  并赋值
                prlist.ForEach(p => { if (row[p.Name] != DBNull.Value) p.SetValue(ob, row[p.Name], null); });
                //放入到返回的集合中.
                oblist.Add(ob);
            }
            return oblist;
        }


        /// <summary>
        /// 将集合类转换成DataTable
        /// </summary>
        /// <param name="list">集合</param>
        /// <returns></returns>
        public static DataTable ToDataTableTow(IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        /**/
        /// <summary>
        /// 将泛型集合类转换成DataTable
        /// </summary>
        /// <typeparam name="T">集合项类型</typeparam>
        /// <param name="list">集合</param>
        /// <returns>数据集(表)</returns>
        public static DataTable ToDataTable<T>(IList<T> list)
        {
            return ToDataTable<T>(list, null);
        }

        /**/
        /// <summary>
        /// 将泛型集合类转换成DataTable
        /// </summary>
        /// <typeparam name="T">集合项类型</typeparam>
        /// <param name="list">集合</param>
        /// <param name="propertyName">需要返回的列的列名</param>
        /// <returns>数据集(表)</returns>
        public static DataTable ToDataTable<T>(IList<T> list, params string[] propertyName)
        {
            List<string> propertyNameList = new List<string>();
            if (propertyName != null)
                propertyNameList.AddRange(propertyName);

            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    if (propertyNameList.Count == 0)
                    {
                        result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                    else
                    {
                        if (propertyNameList.Contains(pi.Name))
                            result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        if (propertyNameList.Count == 0)
                        {
                            object obj = pi.GetValue(list[i], null);
                            tempList.Add(obj);
                        }
                        else
                        {
                            if (propertyNameList.Contains(pi.Name))
                            {
                                object obj = pi.GetValue(list[i], null);
                                tempList.Add(obj);
                            }
                        }
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }

        
    }
}