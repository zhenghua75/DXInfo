using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace DXInfo.Models
{
    public class MenuInfo : INotifyPropertyChanged
    {
        private string _Printer;
        public string Printer
        {
            get { return _Printer; }
            set
            {
                _Printer = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Printer"));
            }
        }
        private Guid _OrderMenuId;
        private Guid _OrderId;
        private string _InvName;
        private Guid _InventoryId;
        private string _FullName;
        private decimal _Quantity;
        private decimal _Price;
        private string _Comment;
        private ObservableCollection<MenuDeskInfo> _deskes;
        private MenuDeskInfo _SelectedDesk;
        private int _WaitMinutes;
        //private string _Status;
        private string _MenuButtonTitle;
        private string _BillButtonTitle;
        private string _MissButtonTitle;
        private DateTime _CreateDate;
        private DateTime _MenuCreateDate;
        private decimal _BillQuantity;
        private decimal _MissQuantity;
        private decimal _MenuQuantity;
        private int _Sort;
        private DateTime _SortDate;

        private int _Status;
        public int Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Status"));
            }
        }

        private string _DeskCodes;
        public string DeskCodes
        {
            get { return _DeskCodes; }
            set
            {
                _DeskCodes = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DeskCodes"));
            }
        }

        public Guid OrderMenuId
        {
            get { return _OrderMenuId; }
            set
            {
                _OrderMenuId = value;
                OnPropertyChanged(new PropertyChangedEventArgs("OrderMenuId"));
            }
        }
        public Guid OrderId
        {
            get { return _OrderId; }
            set
            {
                _OrderId = value;
                OnPropertyChanged(new PropertyChangedEventArgs("OrderId"));
            }
        }
        public string InvName
        {
            get { return _InvName; }
            set
            {
                _InvName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("InvName"));
            }
        }
        public Guid InventoryId
        {
            get { return _InventoryId; }
            set
            {
                _InventoryId = value;
                OnPropertyChanged(new PropertyChangedEventArgs("InventoryId"));
            }
        }
        public string FullName
        {
            get { return _FullName; }
            set
            {
                _FullName = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FullName"));
            }
        }
        public decimal Quantity
        {
            get { return _Quantity; }
            set
            {
                _Quantity = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Quantity"));
            }
        }
        public decimal Price
        {
            get { return _Price; }
            set
            {
                _Price = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Price"));
            }
        }
        public string Comment
        {
            get { return _Comment; }
            set
            {
                _Comment = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Comment"));
            }
        }
        public ObservableCollection<MenuDeskInfo> deskes
        {
            get { return _deskes; }
            set
            {
                _deskes = value;
                OnPropertyChanged(new PropertyChangedEventArgs("deskes"));
            }
        }
        public MenuDeskInfo SelectedDesk
        {
            get { return _SelectedDesk; }
            set
            {
                _SelectedDesk = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SelectedDesk"));
            }
        }
        public int WaitMinutes
        {
            get { return _WaitMinutes; }
            set
            {
                _WaitMinutes = value;
                OnPropertyChanged(new PropertyChangedEventArgs("WaitMinutes"));
            }
        }
        //public string Status
        //{
        //    get { return _Status; }
        //    set
        //    {
        //        _Status = value;
        //        OnPropertyChanged(new PropertyChangedEventArgs("Status"));
        //    }
        //}
        public string MenuButtonTitle
        {
            get { return _MenuButtonTitle; }
            set
            {
                _MenuButtonTitle = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MenuButtonTitle"));
            }
        }
        public string BillButtonTitle
        {
            get { return _BillButtonTitle; }
            set
            {
                _BillButtonTitle = value;
                OnPropertyChanged(new PropertyChangedEventArgs("BillButtonTitle"));
            }
        }
        public string MissButtonTitle
        {
            get { return _MissButtonTitle; }
            set
            {
                _MissButtonTitle = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MissButtonTitle"));
            }
        }
        public DateTime CreateDate
        {
            get { return _CreateDate; }
            set
            {
                _CreateDate = value;
                OnPropertyChanged(new PropertyChangedEventArgs("CreateDate"));
            }
        }
        public DateTime MenuCreateDate
        {
            get { return _MenuCreateDate; }
            set
            {
                _MenuCreateDate = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MenuCreateDate"));
            }
        }
        public decimal BillQuantity
        {
            get { return _BillQuantity; }
            set
            {
                _BillQuantity = value;
                OnPropertyChanged(new PropertyChangedEventArgs("BillQuantity"));
            }
        }
        public decimal MissQuantity
        {
            get { return _MissQuantity; }
            set
            {
                _MissQuantity = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MissQuantity"));
            }
        }
        public decimal MenuQuantity
        {
            get { return _MenuQuantity; }
            set
            {
                _MenuQuantity = value;
                OnPropertyChanged(new PropertyChangedEventArgs("MenuQuantity"));
            }
        }
        public int Sort
        {
            get { return _Sort; }
            set
            {
                _Sort = value;
                OnPropertyChanged(new PropertyChangedEventArgs("Sort"));
            }
        }
        public DateTime SortDate
        {
            get { return _SortDate; }
            set
            {
                _SortDate = value;
                OnPropertyChanged(new PropertyChangedEventArgs("SortDate"));
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
