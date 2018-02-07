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
using DXInfo.Data.Contracts;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// CardConsumePwdWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CardConsumePwdWindow : Window
    {
        //private DXInfo.Models.FairiesMemberManage db = new DXInfo.Models.FairiesMemberManage();
        private readonly IFairiesMemberManageUow uow;
        private DXInfo.Models.Cards card;
        public CardConsumePwdWindow(IFairiesMemberManageUow uow,Guid id)
        {
            this.uow = uow;
            InitializeComponent();
            card = uow.Cards.GetById(id);//.Where(w => w.Id == id).FirstOrDefault();
            if (card == null)
            {
                lblPwd.Visibility = System.Windows.Visibility.Collapsed;
                txtPwd.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                if (string.IsNullOrEmpty(card.CardPwd))
                {
                    lblPwd.Visibility = System.Windows.Visibility.Collapsed;
                    txtPwd.Visibility = System.Windows.Visibility.Collapsed;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDeskNo.Text))
            {
                MessageBox.Show("请输入号牌");
                return;
            }
            if (card != null)
            {
                if (!string.IsNullOrEmpty(card.CardPwd))
                {
                    if (card.CardPwd != txtPwd.Password)
                    {
                        MessageBox.Show("会员卡密码错误");
                        return;
                    }
                }
            }
            this.DialogResult = true;
            this.Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void txtPwd_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPwd.SelectAll();
        }

        private void txtPwd_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            txtPwd.SelectAll();
        }

        private void txtPwd_GotMouseCapture(object sender, MouseEventArgs e)
        {
            txtPwd.SelectAll();
        }

        private void txtPwd_GotTouchCapture(object sender, TouchEventArgs e)
        {
            txtPwd.SelectAll();
        }

        private void Keyboard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            txtDeskNo.Focus();
        }

        private void Keyboard_TouchDown(object sender, TouchEventArgs e)
        {
            txtDeskNo.Focus();
        }
    }
}
