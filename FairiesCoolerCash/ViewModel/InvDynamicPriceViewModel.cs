using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using AutoMapper;

namespace FairiesCoolerCash.ViewModel
{
    public class InvDynamicPriceViewModel : BusinessViewModelBase
    {
        private readonly IMapper mapper;
        public InvDynamicPriceViewModel(IFairiesMemberManageUow uow,IMapper mapper, DXInfo.Models.InventoryEx inventoryEx)
            : base(uow,mapper, new List<string>())
        {
            this.mapper = mapper;
            this.SelectedInventoryEx = inventoryEx;
        }
        
        private void ConfirmExecute()
        {
            this.DialogResult = true;
        }
        public ICommand Confirm
        {
            get
            {
                return new RelayCommand(ConfirmExecute);
            }
        }
    }
}
