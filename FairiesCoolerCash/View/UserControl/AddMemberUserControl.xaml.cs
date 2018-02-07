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
using System.Transactions;
using System.Reflection;
using System.Windows.Controls.Primitives;
using DXInfo.Data.Contracts;
using System.Text.RegularExpressions;
using AutoMapper;
using GalaSoft.MvvmLight.Messaging;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// 添加新会员
    /// </summary>
    public partial class AddMemberUserControl : UserControl
    {
        public AddMemberUserControl()
        {
            InitializeComponent();
        }
    }
}
