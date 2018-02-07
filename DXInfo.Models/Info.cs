using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DXInfo.Models
{
    public class OrderMenuQuantityInfo
    {
        public decimal? BillQuantity { get; set; }
        public decimal? MenuQuantity { get; set; }
        public decimal? MissQuantity { get; set; }
        public decimal? Quantity { get; set; }
    }
    public class Info : INotifyPropertyChanged
    {
        private int _MenuQuantity;
        public int MenuQuantity
        {
            get { return _MenuQuantity; }
            set
            {
                _MenuQuantity = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MenuQuantity"));
            }
        }

        private int _NoMenuQuantity;
        public int NoMenuQuantity
        {
            get { return _NoMenuQuantity; }
            set
            {
                _NoMenuQuantity = value;
                OnPropertyChanged(new PropertyChangedEventArgs("NoMenuQuantity"));
            }
        }

        private int _MissQuantity;
        public int MissQuantity
        {
            get { return _MissQuantity; }
            set
            {
                _MissQuantity = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MissQuantity"));
            }
        }

        private int _Quantity;
        public int Quantity
        {
            get { return _Quantity; }
            set
            {
                _Quantity = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Quantity"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }
}
