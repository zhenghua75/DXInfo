using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace FairiesCoolerCash.ViewModel
{
    public class InvDynamicPriceViewModel : BusinessViewModelBase
    {
        public InvDynamicPriceViewModel(IFairiesMemberManageUow uow, DXInfo.Models.InventoryEx inventoryEx)
            : base(uow, new List<string>())
        {
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
