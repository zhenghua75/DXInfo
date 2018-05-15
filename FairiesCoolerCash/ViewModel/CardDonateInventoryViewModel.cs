using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;

namespace FairiesCoolerCash.ViewModel
{
    public class CardDonateInventoryViewModel:BusinessViewModelBase
    {
        public CardDonateInventoryViewModel(IFairiesMemberManageUow uow, List<DXInfo.Models.CardDonateInventoryEx> lCardDonateInventoryEx)
            : base(uow, new List<string>())
        {
            this.lCardDonateInventoryEx = lCardDonateInventoryEx;        
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
