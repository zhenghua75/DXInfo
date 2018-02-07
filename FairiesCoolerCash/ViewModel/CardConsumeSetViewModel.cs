using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows;

namespace FairiesCoolerCash.ViewModel
{
    public class CardConsumeSetViewModel:BusinessViewModelBase
    {
        public CardConsumeSetViewModel(IFairiesMemberManageUow uow,DXInfo.Models.InventoryEx inventoryEx)
            : base(uow, new List<string>())
        {
            this.SelectedInventoryEx = inventoryEx;        
        }

        private void ConfirmExecute()
        {
            //if (SelectedInventoryEx.lTasteEx.Where(w => !w.Name.Contains("无") && w.IsSelected).Count() > 1)
            //{
            //    MessageBox.Show("多冰、少冰、温、热等只能选一个");
            //    return;
            //}
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
