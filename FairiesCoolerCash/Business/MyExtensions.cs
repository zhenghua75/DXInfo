using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace FairiesCoolerCash.Business
{
    public class ColumnDefinitionExtended : ColumnDefinition
    {
        // Variables
        public static DependencyProperty VisibleProperty;

        // Properties
        public Boolean Visible
        {
            get { return (Boolean)GetValue(VisibleProperty); }
            set { SetValue(VisibleProperty, value); }
        }

        // Constructors
        static ColumnDefinitionExtended()
        {
            VisibleProperty = DependencyProperty.Register("Visible",
                typeof(Boolean),
                typeof(ColumnDefinitionExtended),
                new PropertyMetadata(true, new PropertyChangedCallback(OnVisibleChanged)));

            ColumnDefinition.WidthProperty.OverrideMetadata(typeof(ColumnDefinitionExtended),
                new FrameworkPropertyMetadata(new GridLength(1, GridUnitType.Star), null,
                    new CoerceValueCallback(CoerceWidth)));
        }

        // Get/Set
        public static void SetVisible(DependencyObject obj, Boolean nVisible)
        {
            obj.SetValue(VisibleProperty, nVisible);
        }
        public static Boolean GetVisible(DependencyObject obj)
        {
            return (Boolean)obj.GetValue(VisibleProperty);
        }

        static void OnVisibleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            obj.CoerceValue(ColumnDefinition.WidthProperty);
        }
        static Object CoerceWidth(DependencyObject obj, Object nValue)
        {
            return (((ColumnDefinitionExtended)obj).Visible) ? nValue : new GridLength(0);
        }
    }
    public class RowDefinitionExtended : RowDefinition
    {
        // Variables
        public static DependencyProperty VisibleProperty;

        // Properties
        public Boolean Visible
        {
            get { return (Boolean)GetValue(VisibleProperty); }
            set { SetValue(VisibleProperty, value); }
        }

        // Constructors
        static RowDefinitionExtended()
        {
            VisibleProperty = DependencyProperty.Register("Visible",
                typeof(Boolean),
                typeof(RowDefinitionExtended),
                new PropertyMetadata(true, new PropertyChangedCallback(OnVisibleChanged)));

            RowDefinition.HeightProperty.OverrideMetadata(typeof(RowDefinitionExtended),
                new FrameworkPropertyMetadata(new GridLength(1, GridUnitType.Star), null,
                    new CoerceValueCallback(CoerceHeight)));
        }

        // Get/Set
        public static void SetVisible(DependencyObject obj, Boolean nVisible)
        {
            obj.SetValue(VisibleProperty, nVisible);
        }
        public static Boolean GetVisible(DependencyObject obj)
        {
            return (Boolean)obj.GetValue(VisibleProperty);
        }

        static void OnVisibleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            obj.CoerceValue(RowDefinition.HeightProperty);
        }
        static Object CoerceHeight(DependencyObject obj, Object nValue)
        {
            return (((RowDefinitionExtended)obj).Visible) ? nValue : new GridLength(0);
        }
    }
    public class DataGridContextHelper
    {
        static DataGridContextHelper()
        {

            DependencyProperty dp = FrameworkElement.DataContextProperty.AddOwner(typeof(DataGridColumn));
            FrameworkElement.DataContextProperty.OverrideMetadata(typeof(DataGrid),
            new FrameworkPropertyMetadata
               (null, FrameworkPropertyMetadataOptions.Inherits,
               new PropertyChangedCallback(OnDataContextChanged)));

        }


        public static void OnDataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DataGrid grid = d as DataGrid;
            if (grid != null)
            {
                foreach (DataGridColumn col in grid.Columns)
                {
                    col.SetValue(FrameworkElement.DataContextProperty, e.NewValue);
                }
            }
        }

    }
    public class BindingProxy : Freezable
    {
        #region Overrides of Freezable

        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        #endregion

        public object Data
        {
            get { return (object)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Data.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new UIPropertyMetadata(null));
    }
    public class NotifyCollectionChangeEventArgs : PropertyChangedEventArgs
    {
        public int Index { get; set; }

        public NotifyCollectionChangeEventArgs(int index, string propertyName)
            : base(propertyName)
        {
            Index = index;
        }
    }

    public class NotifiableCollection<T> : ObservableCollection<T> where T : class, INotifyPropertyChanged
    {
        public event EventHandler<NotifyCollectionChangeEventArgs> ItemChanged;

        protected override void ClearItems()
        {
            foreach (var item in this.Items)
            {
                item.PropertyChanged -= ItemPropertyChanged;
            }
            base.ClearItems();
        }

        protected override void SetItem(int index, T item)
        {
            this.Items[index].PropertyChanged -= ItemPropertyChanged;
            base.SetItem(index, item);
            this.Items[index].PropertyChanged += ItemPropertyChanged;
        }

        protected override void RemoveItem(int index)
        {
            this.Items[index].PropertyChanged -= ItemPropertyChanged;
            base.RemoveItem(index);
        }

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            item.PropertyChanged += ItemPropertyChanged;
        }

        private void ItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            T changedItem = sender as T;
            OnItemChanged(this.IndexOf(changedItem), e.PropertyName);
        }

        private void OnItemChanged(int index, string propertyName)
        {
            if (ItemChanged != null)
            {
                this.ItemChanged(this, new NotifyCollectionChangeEventArgs(index, propertyName));

            }
        }
    }

    public static class MyExtensions
    {
        public static DataTable ToDataTable(this object dataObject)
        {
            var tpDataObject = dataObject.GetType();

            DataTable tbl = new DataTable();
            DataRow dataRow = tbl.NewRow();
            foreach (var property in tpDataObject.GetProperties())
            {
                var attributes = property.GetCustomAttributes(typeof(DataColumnAttribute), true);
                if (null != attributes && attributes.Length > 0)
                {
                    if (property.CanRead)
                    {
                        object value = property.GetValue(dataObject, null);
                        DataColumn clm = tbl.Columns.Add(property.Name, property.PropertyType);
                        dataRow[clm] = value;
                    }
                }
            }

            tbl.Rows.Add(dataRow);
            tbl.AcceptChanges();
            return tbl;
        }

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
            Array.ForEach<PropertyInfo>(type.GetProperties(), p =>
            {
                pList.Add(p);
                if (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    dt.Columns.Add(p.Name, Nullable.GetUnderlyingType(p.PropertyType));
                else
                    dt.Columns.Add(p.Name, p.PropertyType);
            });
            if (list != null)
            {
                foreach (var item in list)
                {
                    //创建一个DataRow实例
                    DataRow row = dt.NewRow();
                    //给row 赋值
                    pList.ForEach(p => row[p.Name] = p.GetValue(item, null) == null ? DBNull.Value : p.GetValue(item, null));
                    //加入到DataTable
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }
    }
}
