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
    /// 预定清单
    /// </summary>
    public partial class OrderBookListUserControl : UserControl
    {
        public OrderBookListUserControl()
        {
            InitializeComponent();
            Messenger.Default.Send(new DataGridMessageToken() { MyDataGrid = this.OrderBookList });
        }
        
    }
}
