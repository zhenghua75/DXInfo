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
    public class tbMonthlyBalance : Entity
    {
        
        private Int32 _cnnYear;
        
        private Int32 _cnnMonth;
        
        private Boolean _cnbIsBalance;
        
        private String _cnvcCreater;
        
        private String _cnvcCreaterName;
        
        private DateTime _cndCreateDate;
        
        private String _cnvcModifier;
        
        private String _cnvcModifierName;
        
        private DateTime? _cndModifyDate;
        
        private String _cnvcBalancer;
        
        private String _cnvcBalancerName;
        
        private DateTime? _cndBalanceDate;
        
        [DataMember()]
        public Int32 cnnYear
        {
            get
            {
                return _cnnYear;
            }
            set
            {
                if ((value != _cnnYear))
                {
                    _cnnYear = value;
                    OnPropertyChanged("cnnYear");
                }
            }
        }
        
        [DataMember()]
        public Int32 cnnMonth
        {
            get
            {
                return _cnnMonth;
            }
            set
            {
                if ((value != _cnnMonth))
                {
                    _cnnMonth = value;
                    OnPropertyChanged("cnnMonth");
                }
            }
        }
        
        [DataMember()]
        public Boolean cnbIsBalance
        {
            get
            {
                return _cnbIsBalance;
            }
            set
            {
                if ((value != _cnbIsBalance))
                {
                    _cnbIsBalance = value;
                    OnPropertyChanged("cnbIsBalance");
                }
            }
        }
        
        [DataMember()]
        public String cnvcCreater
        {
            get
            {
                return _cnvcCreater;
            }
            set
            {
                if ((value != _cnvcCreater))
                {
                    _cnvcCreater = value;
                    OnPropertyChanged("cnvcCreater");
                }
            }
        }
        
        [DataMember()]
        public String cnvcCreaterName
        {
            get
            {
                return _cnvcCreaterName;
            }
            set
            {
                if ((value != _cnvcCreaterName))
                {
                    _cnvcCreaterName = value;
                    OnPropertyChanged("cnvcCreaterName");
                }
            }
        }
        
        [DataMember()]
        public DateTime cndCreateDate
        {
            get
            {
                return _cndCreateDate;
            }
            set
            {
                if ((value != _cndCreateDate))
                {
                    _cndCreateDate = value;
                    OnPropertyChanged("cndCreateDate");
                }
            }
        }
        
        [DataMember()]
        public String cnvcModifier
        {
            get
            {
                return _cnvcModifier;
            }
            set
            {
                if ((value != _cnvcModifier))
                {
                    _cnvcModifier = value;
                    OnPropertyChanged("cnvcModifier");
                }
            }
        }
        
        [DataMember()]
        public String cnvcModifierName
        {
            get
            {
                return _cnvcModifierName;
            }
            set
            {
                if ((value != _cnvcModifierName))
                {
                    _cnvcModifierName = value;
                    OnPropertyChanged("cnvcModifierName");
                }
            }
        }
        
        [DataMember()]
        public DateTime? cndModifyDate
        {
            get
            {
                return _cndModifyDate;
            }
            set
            {
                if ((value != _cndModifyDate))
                {
                    _cndModifyDate = value;
                    OnPropertyChanged("cndModifyDate");
                }
            }
        }
        
        [DataMember()]
        public String cnvcBalancer
        {
            get
            {
                return _cnvcBalancer;
            }
            set
            {
                if ((value != _cnvcBalancer))
                {
                    _cnvcBalancer = value;
                    OnPropertyChanged("cnvcBalancer");
                }
            }
        }
        
        [DataMember()]
        public String cnvcBalancerName
        {
            get
            {
                return _cnvcBalancerName;
            }
            set
            {
                if ((value != _cnvcBalancerName))
                {
                    _cnvcBalancerName = value;
                    OnPropertyChanged("cnvcBalancerName");
                }
            }
        }
        
        [DataMember()]
        public DateTime? cndBalanceDate
        {
            get
            {
                return _cndBalanceDate;
            }
            set
            {
                if ((value != _cndBalanceDate))
                {
                    _cndBalanceDate = value;
                    OnPropertyChanged("cndBalanceDate");
                }
            }
        }
    }
}