using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using AutoMapper;

namespace FairiesCoolerCash.ViewModel
{
    public class PayTypeViewModel : BusinessViewModelBase
    {
        private readonly IMapper mapper;
        public PayTypeViewModel(IFairiesMemberManageUow uow,IMapper mapper, List<string> lValidationPropertyNames)
            : base(uow,mapper, lValidationPropertyNames)
        {
            this.mapper = mapper;
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
