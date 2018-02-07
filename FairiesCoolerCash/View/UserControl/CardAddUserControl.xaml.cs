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
using System.Web.Security;
using System.Threading;
using System.Data.Common;
using System.Transactions;
using DXInfo.Data.Contracts;
using System.Text.RegularExpressions;
using FairiesCoolerCash.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// 会员卡补卡
    /// </summary>
    public partial class CardAddUserControl : UserControl
    {
        public CardAddUserControl()
        {
            InitializeComponent();
            Messenger.Default.Send(new DataGridMessageToken() { MyDataGrid = this.CardList });
        }        
    }
}
