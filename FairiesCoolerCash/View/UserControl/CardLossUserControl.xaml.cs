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
using System.Threading;
using System.Web.Security;
using DXInfo.Data.Contracts;
using FairiesCoolerCash.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// 挂失
    /// 20130129 zhh 修改为mvvm
    /// 会员卡挂失
    /// 20130318 zhh 修改 xaml 绑定DataContext
    /// </summary>
    public partial class CardLossUserControl : UserControl
    {
        public CardLossUserControl()
        {
            InitializeComponent();
            Messenger.Default.Send(new DataGridMessageToken() { MyDataGrid = this.CardList });
        }
    }
}
