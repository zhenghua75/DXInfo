using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace FairiesCoolerCash.ViewModel
{
    public class PayTypeViewModel : BusinessViewModelBase
    {
        public PayTypeViewModel(IFairiesMemberManageUow uow, List<string> lValidationPropertyNames)
            : base(uow, lValidationPropertyNames)
        {
            this.SetlPayType();
        }

        private void ConfirmExecute()
        {
            this.DialogResult = this.SelectedPayType != null;
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
