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
    public class CardDonateInventoryViewModel:BusinessViewModelBase
    {
        private readonly IMapper mapper;
        public CardDonateInventoryViewModel(IFairiesMemberManageUow uow,IMapper mapper, List<DXInfo.Models.CardDonateInventoryEx> lCardDonateInventoryEx)
            : base(uow,mapper, new List<string>())
        {
            this.mapper = mapper;
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
