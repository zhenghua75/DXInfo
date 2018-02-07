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
    public class Consume : Entity
    {
        
        private Guid _Id;
        
        private Guid? _Card;
        
        private Guid? _PayType;
        
        private Decimal _LastBalance;
        
        private Decimal _Sum;
        
        private Decimal _PayVoucher;
        
        private Decimal _Voucher;
        
        private Decimal _Discount;
        
        private Decimal _Amount;
        
        private Decimal _Point;
        
        private Decimal _Balance;
        
        private Decimal _Cash;
        
        private Decimal _Change;
        
        private DateTime _CreateDate;
        
        private Guid _UserId;
        
        private Guid _DeptId;
        
        private Int32 _ConsumeType;
        
        private String _DeskNo;
        
        private Guid? _OrderId;
        
        private Decimal _Quantity;
        
        private Int32 _SourceType;

        private bool _IsValid;

        private Guid? _Member;

        private String _Sn;

        private String _OperatorsOnDuty;
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
        public Guid? Card
        {
            get
            {
                return _Card;
            }
            set
            {
                if ((value != _Card))
                {
                    _Card = value;
                    OnPropertyChanged("Card");
                }
            }
        }
        
        [DataMember()]
        public Guid? PayType
        {
            get
            {
                return _PayType;
            }
            set
            {
                if ((value != _PayType))
                {
                    _PayType = value;
                    OnPropertyChanged("PayType");
                }
            }
        }
        
        [DataMember()]
        public Decimal LastBalance
        {
            get
            {
                return _LastBalance;
            }
            set
            {
                if ((value != _LastBalance))
                {
                    _LastBalance = value;
                    OnPropertyChanged("LastBalance");
                }
            }
        }
        
        [DataMember()]
        public Decimal Sum
        {
            get
            {
                return _Sum;
            }
            set
            {
                if ((value != _Sum))
                {
                    _Sum = value;
                    OnPropertyChanged("Sum");
                }
            }
        }
        
        [DataMember()]
        public Decimal PayVoucher
        {
            get
            {
                return _PayVoucher;
            }
            set
            {
                if ((value != _PayVoucher))
                {
                    _PayVoucher = value;
                    OnPropertyChanged("PayVoucher");
                }
            }
        }
        
        [DataMember()]
        public Decimal Voucher
        {
            get
            {
                return _Voucher;
            }
            set
            {
                if ((value != _Voucher))
                {
                    _Voucher = value;
                    OnPropertyChanged("Voucher");
                }
            }
        }
        
        [DataMember()]
        public Decimal Discount
        {
            get
            {
                return _Discount;
            }
            set
            {
                if ((value != _Discount))
                {
                    _Discount = value;
                    OnPropertyChanged("Discount");
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
        public Decimal Point
        {
            get
            {
                return _Point;
            }
            set
            {
                if ((value != _Point))
                {
                    _Point = value;
                    OnPropertyChanged("Point");
                }
            }
        }
        
        [DataMember()]
        public Decimal Balance
        {
            get
            {
                return _Balance;
            }
            set
            {
                if ((value != _Balance))
                {
                    _Balance = value;
                    OnPropertyChanged("Balance");
                }
            }
        }
        
        [DataMember()]
        public Decimal Cash
        {
            get
            {
                return _Cash;
            }
            set
            {
                if ((value != _Cash))
                {
                    _Cash = value;
                    OnPropertyChanged("Cash");
                }
            }
        }
        
        [DataMember()]
        public Decimal Change
        {
            get
            {
                return _Change;
            }
            set
            {
                if ((value != _Change))
                {
                    _Change = value;
                    OnPropertyChanged("Change");
                }
            }
        }
        
        [DataMember()]
        public DateTime CreateDate
        {
            get
            {
                return _CreateDate;
            }
            set
            {
                if ((value != _CreateDate))
                {
                    _CreateDate = value;
                    OnPropertyChanged("CreateDate");
                }
            }
        }
        
        [DataMember()]
        public Guid UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if ((value != _UserId))
                {
                    _UserId = value;
                    OnPropertyChanged("UserId");
                }
            }
        }
        
        [DataMember()]
        public Guid DeptId
        {
            get
            {
                return _DeptId;
            }
            set
            {
                if ((value != _DeptId))
                {
                    _DeptId = value;
                    OnPropertyChanged("DeptId");
                }
            }
        }
        
        [DataMember()]
        public Int32 ConsumeType
        {
            get
            {
                return _ConsumeType;
            }
            set
            {
                if ((value != _ConsumeType))
                {
                    _ConsumeType = value;
                    OnPropertyChanged("ConsumeType");
                }
            }
        }
        
        [DataMember()]
        public String DeskNo
        {
            get
            {
                return _DeskNo;
            }
            set
            {
                if ((value != _DeskNo))
                {
                    _DeskNo = value;
                    OnPropertyChanged("DeskNo");
                }
            }
        }
        
        [DataMember()]
        public Guid? OrderId
        {
            get
            {
                return _OrderId;
            }
            set
            {
                if ((value != _OrderId))
                {
                    _OrderId = value;
                    OnPropertyChanged("OrderId");
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
        public Int32 SourceType
        {
            get
            {
                return _SourceType;
            }
            set
            {
                if ((value != _SourceType))
                {
                    _SourceType = value;
                    OnPropertyChanged("SourceType");
                }
            }
        }

        [DataMember()]
        public bool IsValid
        {
            get
            {
                return _IsValid;
            }
            set
            {
                if ((value != _IsValid))
                {
                    _IsValid = value;
                    OnPropertyChanged("IsValid");
                }
            }
        }

        [DataMember()]
        public Guid? Member
        {
            get
            {
                return _Member;
            }
            set
            {
                if ((value != _Member))
                {
                    _Member = value;
                    OnPropertyChanged("Member");
                }
            }
        }

        [DataMember()]
        public String Sn
        {
            get
            {
                return _Sn;
            }
            set
            {
                if ((value != _Sn))
                {
                    _Sn = value;
                    OnPropertyChanged("Sn");
                }
            }
        }

        [DataMember()]
        public String OperatorsOnDuty
        {
            get
            {
                return _OperatorsOnDuty;
            }
            set
            {
                if ((value != _OperatorsOnDuty))
                {
                    _OperatorsOnDuty = value;
                    OnPropertyChanged("OperatorsOnDuty");
                }
            }
        }
    }
}
