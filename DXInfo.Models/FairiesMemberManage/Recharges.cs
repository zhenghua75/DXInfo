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
    public class Recharges : Entity
    {
        
        private Guid _Id;
        
        private Guid _Card;
        
        private Guid _PayType;
        
        private Decimal _LastBalance;
        
        private Decimal _Amount;
        
        private Decimal _Donate;
        
        private Decimal _Balance;
        
        private DateTime _CreateDate;
        
        private Guid _UserId;
        
        private Guid _DeptId;
        
        private Int32 _RechargeType;
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
        public Guid Card
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
        public Guid PayType
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
        public Decimal Donate
        {
            get
            {
                return _Donate;
            }
            set
            {
                if ((value != _Donate))
                {
                    _Donate = value;
                    OnPropertyChanged("Donate");
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
        public Int32 RechargeType
        {
            get
            {
                return _RechargeType;
            }
            set
            {
                if ((value != _RechargeType))
                {
                    _RechargeType = value;
                    OnPropertyChanged("RechargeType");
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