using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Script.Serialization;
using System.Data;
using System.Collections;

namespace AMSApp.StockControl.Services
{
    /// <summary>
    /// DataTable JSON转换类
    /// </summary>
    public class DataTableConverter : JavaScriptConverter
    {
        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            DataTable dt = obj as DataTable; ;//将Datatable转成Dictionary完成序列化
            Dictionary<string, object> result = new Dictionary<string, object>();
            if (dt != null)
            {
                ArrayList arrList = new ArrayList();
                foreach (DataRow dr in dt.Rows)//循环每行
                {
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        dic.Add(dc.ColumnName, dr[dc.ColumnName]);//Dic中存储列名和每列值
                    }
                    arrList.Add(dic);//ArrayList中保存各行信息
                }
                result[dt.TableName] = arrList; ;//表名作为Key,ArrayList作为值
            }
            return result;

        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            if (dictionary == null)
                throw new ArgumentNullException("dictionary");

            if (type == typeof(DataTable))
            {   //将Dictionary转成Datatable完成反序列化
                foreach (KeyValuePair<string, object> table in dictionary)
                {
                    DataTable dt = new DataTable(table.Key);//表名
                    ArrayList rows = (ArrayList)table.Value;
                    //列名
                    Dictionary<string, object> row = serializer.ConvertToType<Dictionary<string, object>>(rows[0]);
                    foreach (string item in row.Keys)
                    {
                        dt.Columns.Add(item);
                    }
                    //每行数据
                    for (int i = 0; i < rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        Dictionary<string, object> dic = serializer.ConvertToType<Dictionary<string, object>>(rows[i]);
                        foreach (KeyValuePair<string, object> item in dic)
                        {
                            dr[item.Key] = item.Value;
                        }
                        dt.Rows.Add(dr);
                    }
                    return dt;
                }
            }
            return null;
        }

        /// <summary>
        /// 获取本转换器支持的类型
        /// </summary>
        public override IEnumerable<Type> SupportedTypes
        {
            get
            {
                return new Type[] { typeof(DataTable) };
            }
        }
    }
}
