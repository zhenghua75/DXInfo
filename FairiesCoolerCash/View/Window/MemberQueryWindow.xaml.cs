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
using GalaSoft.MvvmLight.Messaging;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// MemberQueryWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MemberQueryWindow : Window
    {
        public MemberQueryWindow()
        {
            InitializeComponent();
            Messenger.Default.Send(new DataGridMessageToken() { MyDataGrid = this.MemberList });
            //Messenger.Default.Register<CloseMemberQueryWindow>(this, Handle_CloseMemberQueryWindowMessageToken);
        }
        //private void Handle_CloseMemberQueryWindowMessageToken(CloseMemberQueryWindow token)
        //{
        //    this.Close();
        //}

        //private void Window_Closed(object sender, EventArgs e)
        //{
        //    //Messenger.Default.Unregister<CloseMemberQueryWindow>(this);
        //    //Closed="Window_Closed"
        //}
    }
}
