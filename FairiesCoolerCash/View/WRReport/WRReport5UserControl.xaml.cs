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
using DXInfo.Data.Contracts;
using GalaSoft.MvvmLight.Messaging;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// 销售排名统计
    /// </summary>
    public partial class WRReport5UserControl : UserControl
    {
        public WRReport5UserControl()
        {
            InitializeComponent();
            Messenger.Default.Send(new DataGridMessageToken() { MyDataGrid = this.MemberList });
        }
    }
}
