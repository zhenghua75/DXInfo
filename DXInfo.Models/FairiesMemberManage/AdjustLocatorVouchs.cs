//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.18052
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DXInfo.Models
{
    using System;
    using System.Runtime.Serialization;
    
    
    [Serializable()]
    [DataContract()]
    public class AdjustLocatorVouchs : Entity
    {
        
        private Guid _Id;
        
        private Guid _ALVId;
        
        private Guid _InvId;
        
        private Guid _MainUnit;
        
        private Guid _STUnit;
        
        private Decimal _ExchRate;
        
        private Decimal _Quantity;
        
        private Decimal _Num;
        
        private String _Batch;
        
        private Decimal _Price;
        
        private Decimal _Amount;
        
        private DateTime? _MadeDate;
        
        private Int32? _ShelfLife;
        
        private Int32? _ShelfLifeType;
        
        private DateTime? _InvalidDate;
        
        private Guid _OutLocator;
        
        private Guid _InLocator;
        
        private String _Memo;
        
        [DataMember()]
        public Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if ((value != _Id))
                {
                    _Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        
        [DataMember()]
        public Guid ALVId
        {
            get
            {
                return _ALVId;
            }
            set
            {
                if ((value != _ALVId))
                {
                    _ALVId = value;
                    OnPropertyChanged("ALVId");
                }
            }
        }
        
        [DataMember()]
        public Guid InvId
        {
            get
            {
                return _InvId;
            }
            set
            {
                if ((value != _InvId))
                {
                    _InvId = value;
                    OnPropertyChanged("InvId");
                }
            }
        }
        
        [DataMember()]
        public Guid MainUnit
        {
            get
            {
                return _MainUnit;
            }
            set
            {
                if ((value != _MainUnit))
                {
                    _MainUnit = value;
                    OnPropertyChanged("MainUnit");
                }
            }
        }
        
        [DataMember()]
        public Guid STUnit
        {
            get
            {
                return _STUnit;
            }
            set
            {
                if ((value != _STUnit))
                {
                    _STUnit = value;
                    OnPropertyChanged("STUnit");
                }
            }
        }
        
        [DataMember()]
        public Decimal ExchRate
        {
            get
            {
                return _ExchRate;
            }
            set
            {
                if ((value != _ExchRate))
                {
                    _ExchRate = value;
                    OnPropertyChanged("ExchRate");
                }
            }
        }
        
        [DataMember()]
        public Decimal Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                if ((value != _Quantity))
                {
                    _Quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }
        }
        
        [DataMember()]
        public Decimal Num
        {
            get
            {
                return _Num;
            }
            set
            {
                if ((value != _Num))
                {
                    _Num = value;
                    OnPropertyChanged("Num");
                }
            }
        }
        
        [DataMember()]
        public String Batch
        {
            get
            {
                return _Batch;
            }
            set
            {
                if ((value != _Batch))
                {
                    _Batch = value;
                    OnPropertyChanged("Batch");
                }
            }
        }
        
        [DataMember()]
        public Decimal Price
        {
            get
            {
                return _Price;
            }
            set
            {
                if ((value != _Price))
                {
                    _Price = value;
                    OnPropertyChanged("Price");
                }
            }
        }
        
        [DataMember()]
        public Decimal Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                if ((value != _Amount))
                {
                    _Amount = value;
                    OnPropertyChanged("Amount");
                }
            }
        }
        
        [DataMember()]
        public DateTime? MadeDate
        {
            get
            {
                return _MadeDate;
            }
            set
            {
                if ((value != _MadeDate))
                {
                    _MadeDate = value;
                    OnPropertyChanged("MadeDate");
                }
            }
        }
        
        [DataMember()]
        public Int32? ShelfLife
        {
            get
            {
                return _ShelfLife;
            }
            set
            {
                if ((value != _ShelfLife))
                {
                    _ShelfLife = value;
                    OnPropertyChanged("ShelfLife");
                }
            }
        }
        
        [DataMember()]
        public Int32? ShelfLifeType
        {
            get
            {
                return _ShelfLifeType;
            }
            set
            {
                if ((value != _ShelfLifeType))
                {
                    _ShelfLifeType = value;
                    OnPropertyChanged("ShelfLifeType");
                }
            }
        }
        
        [DataMember()]
        public DateTime? InvalidDate
        {
            get
            {
                return _InvalidDate;
            }
            set
            {
                if ((value != _InvalidDate))
                {
                    _InvalidDate = value;
                    OnPropertyChanged("InvalidDate");
                }
            }
        }
        
        [DataMember()]
        public Guid OutLocator
        {
            get
            {
                return _OutLocator;
            }
            set
            {
                if ((value != _OutLocator))
                {
                    _OutLocator = value;
                    OnPropertyChanged("OutLocator");
                }
            }
        }
        
        [DataMember()]
        public Guid InLocator
        {
            get
            {
                return _InLocator;
            }
            set
            {
                if ((value != _InLocator))
                {
                    _InLocator = value;
                    OnPropertyChanged("InLocator");
                }
            }
        }
        
        [DataMember()]
        public String Memo
        {
            get
            {
                return _Memo;
            }
            set
            {
                if ((value != _Memo))
                {
                    _Memo = value;
                    OnPropertyChanged("Memo");
                }
            }
        }
    }
}
