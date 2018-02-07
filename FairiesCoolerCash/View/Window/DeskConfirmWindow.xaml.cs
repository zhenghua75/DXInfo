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
using System.Windows.Shapes;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// DeskConfirmWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DeskConfirmWindow : Window
    {
        private System.Printing.PrintQueue _pq;
        public DeskConfirmWindow(dynamic d, System.Printing.PrintQueue pq)
        {
            InitializeComponent();
            GridPrint2.DataContext = d;
            _pq = pq;
            //txtCount = d.Count;
            //txtDeskNo2 = d.DeskNo;
        }
        public DeskConfirmWindow()
        {
            InitializeComponent();
        }
        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //DeskNoMemberCashWindow ncw = new DeskNoMemberCashWindow(100);
            //if (ncw.ShowDialog().GetValueOrDefault())
            //{
                
                
            //}
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            
        }
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pDialog = new PrintDialog();
            pDialog.PrintQueue = _pq;
            pDialog.PageRangeSelection = PageRangeSelection.AllPages;
            pDialog.UserPageRangeEnabled = true;
            pDialog.PrintVisual(GridPrint2, "GridPrint2");

            PrintDialog allDialog = new PrintDialog();
            allDialog.PageRangeSelection = PageRangeSelection.AllPages;
            allDialog.UserPageRangeEnabled = true;
            allDialog.PrintVisual(GridPrint2, "GridPrint2");
            this.DialogResult = true;
            this.Close();
        }
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Button_Click(null, null);
        }
    }
}
