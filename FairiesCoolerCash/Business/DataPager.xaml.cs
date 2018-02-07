using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// DataPager.xaml 的交互逻辑
    /// </summary>
    public partial class DataPager : UserControl, INotifyPropertyChanged
    {
        #region 构造函数
        private int PageCount;
        private ObservableCollection<ObservableCollection<Object>> Pages;
        public DataPager()
        {
            InitializeComponent();
            this.PagerSetting = new PagerSettings();
        }
        public PagerSettings PagerSetting { get; set; }
        #endregion

        #region 生成翻页数据
        public void GeneratePages()
        {
            if (ItemsSource != null)
            {
                PageCount = (int)Math.Ceiling(ItemsSource.Count / (double)ItemsPerPage);
                Pages = new ObservableCollection<ObservableCollection<object>>();
                for (int i = 0; i < PageCount; i++)
                {
                    ObservableCollection<object> page = new ObservableCollection<object>();
                    for (int j = 0; j < ItemsPerPage; j++)
                    {
                        if (i * ItemsPerPage + j > ItemsSource.Count - 1) break;
                        page.Add(ItemsSource[i * ItemsPerPage + j]);
                    }
                    Pages.Add(page);
                }
                if (PageCount == 0)
                {
                    CurrentPage = null;
                }
                else
                {
                    CurrentPage = Pages[0];
                }
                CurrentPageNumber = 1;
                TotalPages = PageCount;
            }
        }
        #endregion        

        #region 当前页数据
        private ObservableCollection<Object> _CurrentPage;

        public ObservableCollection<Object> CurrentPage
        {
            get { return _CurrentPage; }
            set
            {
                _CurrentPage = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("CurrentPage"));
            }
        }
        #endregion

        #region 当前页码
        private int _CurrentPageNumber;
        public int CurrentPageNumber
        {
            get { return _CurrentPageNumber; }
            set
            {
                _CurrentPageNumber = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("CurrentPageNumber"));
            }
        }
        #endregion

        #region 总页数
        private int _TotalPages;
        public int TotalPages
        {
            get {
                return _TotalPages;
            }
            set
            {
                _TotalPages = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("TotalPages"));
            }
        }
        #endregion

        #region 每页行数 默认20
        public int ItemsPerPage
        {
            get { return (int)GetValue(ItemsPerPageProperty); }
            set { SetValue(ItemsPerPageProperty, value); }
        }
        public static readonly DependencyProperty ItemsPerPageProperty =
            DependencyProperty.Register("ItemsPerPage", typeof(int), typeof(DataPager), new UIPropertyMetadata(20));
        #endregion

        #region 数据集合
        //private ObservableCollection<Object> _ItemsSource;
        //public ObservableCollection<Object> ItemsSource
        //{
        //    get { return _ItemsSource; }
        //    set
        //    {
        //        _ItemsSource = value;
        //        GeneratePages();
        //    }
        //}

        public ObservableCollection<object> ItemsSource
        {
            get
            {
                return GetValue(ItemsSourceProperty) as ObservableCollection<object>;
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<object>), typeof(DataPager), new PropertyMetadata(null, MyCustom_PropertyChanged));
        private static void MyCustom_PropertyChanged(DependencyObject sender,DependencyPropertyChangedEventArgs e)
        {
            DataPager dp = sender as DataPager;
            dp.GeneratePages();
        }
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region 翻页
        private void FirstPage_Click(object sender, RoutedEventArgs e)
        {
            if (Pages != null)
            {
                CurrentPage = Pages[0];
                CurrentPageNumber = 1;
                //TotalPages = PageCount;
            }
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (Pages != null)
            {
                CurrentPageNumber = (CurrentPageNumber - 1) < 1 ? 1 : CurrentPageNumber - 1;
                CurrentPage = Pages[CurrentPageNumber - 1];
                //TotalPages = PageCount;
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (Pages != null)
            {
                CurrentPageNumber = (CurrentPageNumber + 1) > PageCount ? PageCount : CurrentPageNumber + 1;
                CurrentPage = Pages[CurrentPageNumber - 1];
                //TotalPages = PageCount;
            }
        }

        private void LastPage_Click(object sender, RoutedEventArgs e)
        {
            if (Pages != null)
            {
                CurrentPage = Pages[PageCount - 1];
                CurrentPageNumber = PageCount;
                //TotalPages = PageCount;
            }
        }

        private void GoPage_Click(object sender, RoutedEventArgs e)
        {
            if (Pages != null)
            {
                if (CurrentPageNumber > PageCount)
                {
                    MessageBox.Show("页码超出范围");
                    return;
                }
                CurrentPage = Pages[CurrentPageNumber - 1];
                //TotalPages = PageCount;
            }
        }
        #endregion

    }
}
