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

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// DataGridPager.xaml 的交互逻辑
    /// </summary>
    public partial class DataGridPager : UserControl
    {
        public DataGridPager()
        {
            InitializeComponent();
            
        }
        
        private int pageIndex = 1;

        /// <summary>
        /// 当前页面索引
        /// </summary>
        public int PageIndex { get { return pageIndex; } set { pageIndex = value; } }

        private int pageSize = 10;
        /// <summary>
        /// 页面记录数 应与DataGrid的PageSize保持一致
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set
            {
                if (value != 0)
                    pageSize = value;
            }
        }

        /// <summary>
        /// 记录总数
        /// </summary>
        private int count = 10;

        public int Count
        {
            get { return count; }
            set
            {

                count = value;
                Load();
            }
        }

        /// <summary>
        /// 总的页数
        /// </summary>
        private int pageCount;

        private int countSize = 10;
        /// <summary>
        /// 要显示的页数 默认可翻页数为10 
        /// </summary>
        public int CountSize
        {
            get { return countSize; }
            set
            {
                if (value != 0)
                    countSize = value;
            }
        }

        /// <summary>
        /// 块数，总的页数除以要显示的页数
        /// </summary>
        private int counter = 1;

        /// <summary>
        /// 当前所在的块
        /// </summary>
        private int currentCounter = 1;

        public delegate void PageIndexChangingEvent(int pageIndex, EventArgs e);
        /// <summary>
        /// 翻页 需实现
        /// </summary>
        public event PageIndexChangingEvent PageIndexChanging;

        /// <summary>
        /// button Content对应的值
        /// </summary>
        private List<int> ShowList = new List<int>();

        /// <summary>
        /// 页面选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            switch (bt.Name)
            {
                case "FirstButton":
                    currentCounter = 1;
                    pageIndex = 1;
                    BindList();
                    break;
                case "LastButton":
                    currentCounter = counter;
                    BindList();
                    pageIndex = pageCount;
                    break;
                default:
                    pageIndex = int.Parse(bt.Content.ToString());
                    break;
            }
            PageChangeSet(e);
        }

        /// <summary>
        /// 设置button的Content
        /// </summary>
        private void BindList()
        {
            ShowList.Clear();
            if (counter > 1)
            {
                if (currentCounter == counter)
                {
                    for (int i = pageCount - countSize; i <= pageCount; i++)
                    {
                        ShowList.Add(i);
                    }
                }
                else
                {
                    for (int i = (currentCounter - 1) * countSize + 1; i <= countSize * (currentCounter); i++)
                    {
                        ShowList.Add(i);
                    }
                }

                ICBT.Items.Refresh();
            }
        }

        /// <summary>
        /// 翻页前的设置
        /// </summary>
        /// <param name="e"></param>
        private void PageChangeSet(EventArgs e)
        {
            if (counter > 1)
            {
                if (pageIndex == 1 || currentCounter == 1)
                {
                    FirstButton.Visibility = Visibility.Collapsed;
                    PreButton.Visibility = Visibility.Collapsed;
                    LastButton.Visibility = Visibility.Visible;
                    NextButton.Visibility = Visibility.Visible;
                }
                else if (pageIndex == pageCount || currentCounter == counter)
                {
                    LastButton.Visibility = Visibility.Collapsed;
                    NextButton.Visibility = Visibility.Collapsed;
                    FirstButton.Visibility = Visibility.Visible;
                    PreButton.Visibility = Visibility.Visible;
                }
                else
                {

                    PreButton.Visibility = Visibility.Visible;
                    LastButton.Visibility = Visibility.Visible;
                    NextButton.Visibility = Visibility.Visible;
                    FirstButton.Visibility = Visibility.Visible;
                }
            }
            CurrentTB.Text = pageIndex.ToString();
            PageIndexChanging(PageIndex, e);
        }

        /// <summary>
        /// 加载，当数据条数变化时，重新加载
        /// </summary>
        private void Load()
        {
            FirstButton.Visibility = Visibility.Collapsed;
            PreButton.Visibility = Visibility.Collapsed;
            int tempvalue = count % pageSize;
            if (tempvalue == 0)
            {
                pageCount = count / pageSize;
            }
            else
            {
                pageCount = count / pageSize + 1;
            }
            counter = (pageCount % countSize == 0) ? pageCount / countSize : (pageCount / countSize + 1);
            int finallySize;

            if (counter > 1)
            {
                LastButton.Visibility = Visibility.Visible;
                NextButton.Visibility = Visibility.Visible;
                finallySize = countSize;
            }
            else
            {
                LastButton.Visibility = Visibility.Collapsed;
                NextButton.Visibility = Visibility.Collapsed;
                finallySize = pageCount;
            }
            ShowList.Clear();
            for (int i = 1; i <= finallySize; i++)
            {
                ShowList.Add(i);
            }
            ICBT.ItemsSource = ShowList;
            ICBT.Items.Refresh();
            TotalTB.Text = pageCount.ToString();
            List<int> list = new List<int>();
            for (int i = 1; i <= pageCount; i++)
            {
                list.Add(i);
            }
            GOTOCB.ItemsSource = list;
            CurrentTB.Text = pageIndex.ToString();

        }

        /// <summary>
        /// 页面下拉框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GOTOCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                pageIndex = int.Parse(GOTOCB.SelectedValue.ToString());
                if (pageIndex % countSize == 0)
                {
                    currentCounter = pageIndex / countSize;
                }
                else
                {
                    currentCounter = pageIndex / countSize + 1;
                }
                BindList();
                PageChangeSet(e);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 前countSize页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreButton_Click(object sender, RoutedEventArgs e)
        {
            currentCounter--;
            pageIndex = countSize * (currentCounter - 1) + 1;
            BindList();
            PageChangeSet(e);
        }

        /// <summary>
        /// 后countSize页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            currentCounter++;
            pageIndex = (currentCounter - 1) * countSize + 1;
            BindList();
            PageChangeSet(e);
        }
    }
}
