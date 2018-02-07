using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DXInfo.Models
{
    [NotMapped]
    public class CardDonateInventoryEx : CardDonateInventory//INotifyPropertyChanged
    {
        private String _Code;

        public String Code
        {
            get { return _Code; }
            set
            {
                _Code = value;
                OnPropertyChanged("Code");
            }
        }

        private String _Name;

        public String Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }

        private bool _IsCheck;
        public bool IsCheck
        {
            get { return _IsCheck; }
            set
            {
                _IsCheck = value;
                OnPropertyChanged("IsCheck");
            }
        }
        private int _Quantity;
        public int Quantity
        {
            get { return _Quantity; }
            set
            {
                _Quantity = value;
                OnPropertyChanged("Quantity");
            }
        }
        private decimal _SalePrice;
        public decimal SalePrice
        {
            get { return _SalePrice; }
            set
            {
                _SalePrice = value;
                OnPropertyChanged("SalePrice");
            }
        }
        private decimal _Amount;
        public decimal Amount
        {
            get { return _Amount; }
            set
            {
                _Amount = value;
                OnPropertyChanged("Amount");
            }
        }
    }
}
