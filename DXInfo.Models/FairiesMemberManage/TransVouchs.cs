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
    public class TransVouchs : Entity
    {
        
        private Guid _Id;
        
        private Guid _TVId;
        
        private Guid _InvId;
        
        private Guid _MainUnit;
        
        private Guid _STUnit;
        
        private Decimal _ExchRate;
        
        private Decimal _Quantity;
        
        private Decimal _Num;
        
        private Decimal _Price;
        
        private Decimal _Amount;
        
        private String _Batch;
        
        private DateTime? _MadeDate;
        
        private Int32? _ShelfLife;
        
        private Int32? _ShelfLifeType;
        
        private DateTime? _InvalidDate;
        
        private Guid? _Locator;
        
        private Guid? _SourceId;
        
        private Decimal _DueQuantity;
        
        private Decimal _DueNum;
        
        private Decimal _DueAmount;
        
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
        public Guid TVId
        {
            get
            {
                return _TVId;
            }
            set
            {
                if ((value != _TVId))
                {
                    _TVId = value;
                    OnPropertyChanged("TVId");
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
        public Guid? Locator
        {
            get
            {
                return _Locator;
            }
            set
            {
                if ((value != _Locator))
                {
                    _Locator = value;
                    OnPropertyChanged("Locator");
                }
            }
        }
        
        [DataMember()]
        public Guid? SourceId
        {
            get
            {
                return _SourceId;
            }
            set
            {
                if ((value != _SourceId))
                {
                    _SourceId = value;
                    OnPropertyChanged("SourceId");
                }
            }
        }
        
        [DataMember()]
        public Decimal DueQuantity
        {
            get
            {
                return _DueQuantity;
            }
            set
            {
                if ((value != _DueQuantity))
                {
                    _DueQuantity = value;
                    OnPropertyChanged("DueQuantity");
                }
            }
        }
        
        [DataMember()]
        public Decimal DueNum
        {
            get
            {
                return _DueNum;
            }
            set
            {
                if ((value != _DueNum))
                {
                    _DueNum = value;
                    OnPropertyChanged("DueNum");
                }
            }
        }
        
        [DataMember()]
        public Decimal DueAmount
        {
            get
            {
                return _DueAmount;
            }
            set
            {
                if ((value != _DueAmount))
                {
                    _DueAmount = value;
                    OnPropertyChanged("DueAmount");
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
