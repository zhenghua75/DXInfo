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
using AutoMapper;
using DXInfo.Data.Contracts;
using DXInfo.Models;
using FairiesCoolerCash.ViewModel;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// InvDynamicPriceWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InvDynamicPriceWindow : Window
    {
        private readonly IMapper mapper;
        public InvDynamicPriceWindow(IFairiesMemberManageUow uow,IMapper mapper, InventoryEx iex)
        {
            InitializeComponent();
            this.mapper = mapper;
            this.DataContext = new InvDynamicPriceViewModel(uow,mapper, iex);
        }
    }
}
