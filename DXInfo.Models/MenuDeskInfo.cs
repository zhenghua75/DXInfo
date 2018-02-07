using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace DXInfo.Models
{
    public class MenuDeskInfo : INotifyPropertyChanged
    {
        private Guid _OrderId;
        private Guid _OrderDeskId;
        private Guid _DeskId;
        private string _DeskCode;
        private bool _IsSelected;

        public Guid OrderId
        {
            get { return _OrderId; }
            set
            {
                _OrderId = value;
                OnPropertyChanged(new PropertyChangedEventArgs("OrderId"));
            }
        }
        public Guid OrderDeskId
        {
            get { return _OrderDeskId; }
            set
            {
                _OrderDeskId = value;
                OnPropertyChanged(new PropertyChangedEventArgs("OrderDeskId"));
            }
        }
        public Guid DeskId
        {
            get { return _DeskId; }
            set
            {
                _DeskId = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DeskId"));
            }
        }
        public string DeskCode
        {
            get { return _DeskCode; }
            set
            {
                _DeskCode = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DeskCode"));
            }
        }
        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                _IsSelected = value;
                OnPropertyChanged(new PropertyChangedEventArgs("IsSelected"));
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
