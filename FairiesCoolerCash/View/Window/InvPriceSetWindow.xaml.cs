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
using DXInfo.Models;
using DXInfo.Data.Contracts;
using FairiesCoolerCash.ViewModel;
using AutoMapper;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// CardConsumeSetWindow.xaml 的交互逻辑
    /// </summary>
    public partial class InvPriceSetWindow : Window
    {
        private readonly IFairiesMemberManageUow uow;
        private readonly IMapper mapper;
        private InventoryEx iex;
        public InvPriceSetWindow(IFairiesMemberManageUow uow,IMapper mapper, InventoryEx d)
        {
            this.uow = uow;
            this.mapper = mapper;
            InitializeComponent();
            this.DataContext = new CardConsumeSetViewModel(uow,mapper, d);
            iex = d;
        }        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
}
