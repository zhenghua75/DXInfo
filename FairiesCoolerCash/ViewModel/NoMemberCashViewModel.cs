using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;

namespace FairiesCoolerCash.ViewModel
{
    public class NoMemberCashViewModel:BusinessViewModelBase
    {
        public System.Windows.Visibility PasswordVisibility { get; set; }
        public System.Windows.Visibility ReceivableVisibility { get; set; }
        public System.Windows.Visibility DeskNoVisibility { get; set; }
        private bool bReceivable = false;
        private bool bPassword = false;
        private bool bDeskNo = false;
        public NoMemberCashViewModel(IFairiesMemberManageUow uow,
            List<string> lValidationPropertyNames,
            decimal dReceivableAmount,
            string title,
            bool bReceivable,bool bPassword,bool bDeskNo)
            : base(uow, lValidationPropertyNames)//new List<string>() { "DeskNo","Cash"})
        {
            this.Title = title;
            this.ReceivableAmount = dReceivableAmount;
            this.Cash = dReceivableAmount;
            if (bReceivable)
            {
                this.ReceivableVisibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.ReceivableVisibility = System.Windows.Visibility.Collapsed;
            }
            if (bPassword)
            {
                this.PasswordVisibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.PasswordVisibility = System.Windows.Visibility.Collapsed;
            }
            if (bDeskNo)
            {
                this.DeskNoVisibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.DeskNoVisibility = System.Windows.Visibility.Collapsed;
            }
            this.bReceivable = bReceivable;
            this.bPassword = bPassword;
            this.bDeskNo = bDeskNo;
            
        }
        
        private void ConfirmExecute()
        {
            this.DialogResult = true;
        }
        private bool ConFirmCanExecute()
        {
            bool ret = true;
            if (bPassword)
            {
                if (string.IsNullOrEmpty(this.Password))
                {
                    ret = false;
                }
            }
            if (bDeskNo)
            {
                if (string.IsNullOrEmpty(this.DeskNo))
                {
                    ret = false;
                }
            }
            if (bReceivable)
            {
                ret = ReceivableAmount <= Cash || (this.Erasing && (int)ReceivableAmount/10==(int)Cash/10);
            }
            return ret;
        }
        public ICommand Confirm
        {
            get
            {
                return new RelayCommand(ConfirmExecute, ConFirmCanExecute);
            }
        }
    }
}
