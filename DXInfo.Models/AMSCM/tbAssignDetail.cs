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
    public class tbAssignDetail : Entity
    {
        
        private Decimal _cnnAssignSerialNo;
        
        private Decimal _cnnOrderSerialNo;
        
        private String _cnvcProductCode;
        
        private String _cnvcProductName;
        
        private String _cnvcUnit;
        
        private Decimal? _cnnPrice;
        
        private Decimal? _cnnOrderCount;
        
        private Decimal? _cnnCount;
        
        private Decimal? _cnnSum;
        
        private Decimal _cnnProduceSerialNo;
        
        [DataMember()]
        public Decimal cnnAssignSerialNo
        {
            get
            {
                return _cnnAssignSerialNo;
            }
            set
            {
                if ((value != _cnnAssignSerialNo))
                {
                    _cnnAssignSerialNo = value;
                    OnPropertyChanged("cnnAssignSerialNo");
                }
            }
        }
        
        [DataMember()]
        public Decimal cnnOrderSerialNo
        {
            get
            {
                return _cnnOrderSerialNo;
            }
            set
            {
                if ((value != _cnnOrderSerialNo))
                {
                    _cnnOrderSerialNo = value;
                    OnPropertyChanged("cnnOrderSerialNo");
                }
            }
        }
        
        [DataMember()]
        public String cnvcProductCode
        {
            get
            {
                return _cnvcProductCode;
            }
            set
            {
                if ((value != _cnvcProductCode))
                {
                    _cnvcProductCode = value;
                    OnPropertyChanged("cnvcProductCode");
                }
            }
        }
        
        [DataMember()]
        public String cnvcProductName
        {
            get
            {
                return _cnvcProductName;
            }
            set
            {
                if ((value != _cnvcProductName))
                {
                    _cnvcProductName = value;
                    OnPropertyChanged("cnvcProductName");
                }
            }
        }
        
        [DataMember()]
        public String cnvcUnit
        {
            get
            {
                return _cnvcUnit;
            }
            set
            {
                if ((value != _cnvcUnit))
                {
                    _cnvcUnit = value;
                    OnPropertyChanged("cnvcUnit");
                }
            }
        }
        
        [DataMember()]
        public Decimal? cnnPrice
        {
            get
            {
                return _cnnPrice;
            }
            set
            {
                if ((value != _cnnPrice))
                {
                    _cnnPrice = value;
                    OnPropertyChanged("cnnPrice");
                }
            }
        }
        
        [DataMember()]
        public Decimal? cnnOrderCount
        {
            get
            {
                return _cnnOrderCount;
            }
            set
            {
                if ((value != _cnnOrderCount))
                {
                    _cnnOrderCount = value;
                    OnPropertyChanged("cnnOrderCount");
                }
            }
        }
        
        [DataMember()]
        public Decimal? cnnCount
        {
            get
            {
                return _cnnCount;
            }
            set
            {
                if ((value != _cnnCount))
                {
                    _cnnCount = value;
                    OnPropertyChanged("cnnCount");
                }
            }
        }
        
        [DataMember()]
        public Decimal? cnnSum
        {
            get
            {
                return _cnnSum;
            }
            set
            {
                if ((value != _cnnSum))
                {
                    _cnnSum = value;
                    OnPropertyChanged("cnnSum");
                }
            }
        }
        
        [DataMember()]
        public Decimal cnnProduceSerialNo
        {
            get
            {
                return _cnnProduceSerialNo;
            }
            set
            {
                if ((value != _cnnProduceSerialNo))
                {
                    _cnnProduceSerialNo = value;
                    OnPropertyChanged("cnnProduceSerialNo");
                }
            }
        }
    }
}