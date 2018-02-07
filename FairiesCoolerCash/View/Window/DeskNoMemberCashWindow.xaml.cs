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
    /// NoMemberCashWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DeskNoMemberCashWindow : Window
    {
        public decimal dCash { get; set; }
        public DeskNoMemberCashWindow(decimal dCash)
        {
            InitializeComponent();
            txtCash2.Text = dCash.ToString("c") ;
            this.dCash = dCash;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //if (string.IsNullOrEmpty(txtDeskNo.Text))
            //{
            //    MessageBox.Show("请输入号牌");
            //    return;
            //}
            if (!string.IsNullOrEmpty(txtCash.Text))
            {
                decimal dcash = Convert.ToDecimal(txtCash.Text);
                if (dcash < this.dCash)
                {
                    MessageBox.Show("收的钱应不小于消费金额");
                    return;
                }
            }
            this.DialogResult = true;
            this.Close();
        }
        //private void Text_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    Keyboard.IsOpen = App.IsOpen;
        //    Keyboard.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
        //}
        //private void Text_LostFocus(object sender, RoutedEventArgs e)
        //{
        //    Keyboard.IsOpen = false;
        //}

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (string.IsNullOrEmpty(txtDeskNo.Text))
            //{
            //    MessageBox.Show("请输入号牌");
            //    e.Cancel = true;
            //}
            
        }

        //private void Keyboard_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    txtDeskNo.Focus();
        //}

        //private void Keyboard_TouchDown(object sender, TouchEventArgs e)
        //{
        //    txtDeskNo.Focus();
        //}
        private void Keyboard2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtCash.Focus();
        }

        private void Keyboard2_TouchDown(object sender, TouchEventArgs e)
        {
            txtCash.Focus();
        }

        private void txtCash_TextChanged(object sender, TextChangedEventArgs e)
        {
            decimal dcash = Convert.ToDecimal(txtCash.Text);
            txtChange.Text = (dcash - dCash).ToString("c");
        }
    }
}
