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
    public partial class CardConsumeSetWindow : Window
    {
        private readonly IFairiesMemberManageUow uow;
        private readonly IMapper mapper;
        private InventoryEx iex;
        public CardConsumeSetWindow(IFairiesMemberManageUow uow, IMapper mapper, InventoryEx d)
        {
            this.mapper = mapper;
            this.uow = uow;
            InitializeComponent();
            this.DataContext = new CardConsumeSetViewModel(uow,mapper, d);
            iex = d;
        }        

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            //if (iex.lTasteEx.Where(w => !w.Name.Contains("无") && w.IsSelected).Count() > 1)
            //{
            //    MessageBox.Show("多冰、少冰、温、热等只能选一个");
            //    e.Cancel = true;
            //    return;
            //}
        }
    }
}
