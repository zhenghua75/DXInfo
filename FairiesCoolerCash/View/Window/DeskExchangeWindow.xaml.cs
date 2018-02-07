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
    /// DeskExchangeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DeskExchangeWindow : Window
    {
        public DeskExchangeWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDeskNo.Text))
            {
                MessageBox.Show("请输入桌台号！");
                return;
            }
            this.DialogResult = true;
            this.Close();
        }
    }
}
