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
using FairiesCoolerCash.ViewModel;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// NoMemberCashWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NoMemberCashWindow : Window
    {
        public NoMemberCashWindow(NoMemberCashViewModel ncv)
        {
            InitializeComponent();
            this.DataContext = ncv;
        }
        private void Keyboard1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtCash.Focus();
            //if (txtCash.SelectionStart == 0)
            //{
            //    txtCash.SelectionStart = txtCash.Text.Length;
            //}
        }

        private void Keyboard1_TouchDown(object sender, TouchEventArgs e)
        {
            txtCash.Focus();
            //if (txtCash.SelectionStart == 0)
            //{
            //    txtCash.SelectionStart = txtCash.Text.Length;
            //}
        }

        private void Keyboard2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtDeskNo.Focus();
            //txtDeskNo.SelectionStart = txtDeskNo.Text.Length;
        }

        private void Keyboard2_TouchDown(object sender, TouchEventArgs e)
        {
            txtDeskNo.Focus();
            //txtDeskNo.SelectionStart = txtDeskNo.Text.Length;
        }

        private void Keyboard3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtPwd.Focus();
        }

        private void Keyboard3_TouchDown(object sender, TouchEventArgs e)
        {
            txtPwd.Focus();
        }
    }
}
