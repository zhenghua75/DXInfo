﻿using System;
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
using System.ComponentModel;
using System.Collections.ObjectModel;
using DXInfo.Data.Contracts;
using FairiesCoolerCash.ViewModel;
using GalaSoft.MvvmLight.Messaging;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// 会员资料查询修改
    /// </summary>
    public partial class MemberQueryUserControl : UserControl
    {
        public MemberQueryUserControl()
        {
            InitializeComponent();
            Messenger.Default.Send(new DataGridMessageToken() { MyDataGrid = this.MemberList });
        }
    }
}
