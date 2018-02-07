namespace Trirand.Web.Mvc
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.ComponentModel;

    internal static class IEnumerableExtensions
    {
        private static DataTable ObtainDataTableFromIEnumerable(IEnumerable ien, JQGrid grid)
        {
            DataTable table = new DataTable();
            foreach (object obj2 in ien)
            {
                if (obj2 is DbDataRecord)
                {
                    DbDataRecord record = obj2 as DbDataRecord;
                    if (table.Columns.Count == 0)
                    {
                        foreach (JQGridColumn column in grid.Columns)
                        {
                            table.Columns.Add(column.DataField);
                        }
                    }
                    DataRow row = table.NewRow();
                    foreach (JQGridColumn column2 in grid.Columns)
                    {
                        row[column2.DataField] = record[column2.DataField];
                    }
                    table.Rows.Add(row);
                    continue;
                }
                if (obj2 is DataRow)
                {
                    DataRow row2 = obj2 as DataRow;
                    if (table.Columns.Count == 0)
                    {
                        foreach (JQGridColumn column3 in grid.Columns)
                        {
                            table.Columns.Add(column3.DataField);
                        }
                    }
                    DataRow row3 = table.NewRow();
                    foreach (JQGridColumn column4 in grid.Columns)
                    {
                        row3[column4.DataField] = row2[column4.DataField];
                    }
                    table.Rows.Add(row3);
                    continue;
                }
                PropertyInfo[] properties = obj2.GetType().GetProperties();
                if (table.Columns.Count == 0)
                {
                    foreach (PropertyInfo info in properties)
                    {
                        Type propertyType = info.PropertyType;
                        if (propertyType.IsGenericType && (propertyType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            propertyType = Nullable.GetUnderlyingType(propertyType);
                        }
                        table.Columns.Add(info.Name, propertyType);
                    }
                }
                DataRow row4 = table.NewRow();
                foreach (PropertyInfo info2 in properties)
                {
                    object obj3 = info2.GetValue(obj2, null);
                    if (obj3 != null)
                    {
                        row4[info2.Name] = obj3;
                    }
                    else
                    {
                        row4[info2.Name] = DBNull.Value;
                    }
                }
                table.Rows.Add(row4);
            }
            return table;
        }

        public static DataTable ToDataTable(this IEnumerable en, JQAutoComplete autoComplete)
        {
            JQGrid grid = new JQGrid();
            JQGridColumn item = new JQGridColumn();
            item.DataField = autoComplete.DataField;
            grid.Columns.Add(item);
            return en.ToDataTable(grid);
        }

        public static DataTable ToDataTable(this IEnumerable en, JQGrid grid)
        {
            DataTable table = new DataTable();
            DataView view = en as DataView;
            if (view != null)
            {
                return view.ToTable();
            }
            if (en != null)
            {
                table = ObtainDataTableFromIEnumerable(en, grid);
            }
            return table;
        }

        public static List<string> ToListOfString(this IEnumerable en, JQAutoComplete autoComplete)
        {
            DataTable table = en.ToDataTable(autoComplete);
            List<string> list = new List<string>();
            IEnumerator enumerator = table.Rows.GetEnumerator();

            Predicate<string> match = null;
            DataRow row;
            while (enumerator.MoveNext())
            {
                row = (DataRow)enumerator.Current;
                if (match == null)
                {
                    match = delegate(string s)
                    {
                        return s == row[autoComplete.DataField].ToString();
                    };
                }
                if (string.IsNullOrEmpty(list.Find(match)))
                {
                    list.Add(row[autoComplete.DataField].ToString());
                }
            }

            return list;
        }

        //public static DataTable ToDataTable<T>(this IEnumerable<T> array)
        //{
        //    var ret = new DataTable();
        //    foreach (PropertyDescriptor dp in TypeDescriptor.GetProperties(typeof(T)))
        //        ret.Columns.Add(dp.Name, dp.PropertyType);
        //    foreach (T item in array)
        //    {
        //        var Row = ret.NewRow();
        //        foreach (PropertyDescriptor dp in TypeDescriptor.GetProperties(typeof(T)))
        //            Row[dp.Name] = dp.GetValue(item);
        //        ret.Rows.Add(Row);
        //    }
        //    return ret;
        //}

        //public static DataTable CopyToDataTable<T>(this IEnumerable<T> array)
        //{
        //    var ret = new DataTable();
        //    foreach (PropertyDescriptor dp in TypeDescriptor.GetProperties(typeof(T)))
        //        // if (!dp.IsReadOnly)
        //        ret.Columns.Add(dp.Name, dp.PropertyType);
        //    foreach (T item in array)
        //    {
        //        var Row = ret.NewRow();
        //        foreach (PropertyDescriptor dp in TypeDescriptor.GetProperties(typeof(T)))
        //            // if (!dp.IsReadOnly)
        //            Row[dp.Name] = dp.GetValue(item);
        //        ret.Rows.Add(Row);
        //    }
        //    return ret;
        //}
    }
}

