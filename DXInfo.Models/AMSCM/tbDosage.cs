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
    public class tbDosage : Entity
    {
        
        private String _cnvcProductCode;
        
        private String _cnvcProductType;
        
        private String _cnvcCode;
        
        private String _cnvcName;
        
        private String _cnvcUnit;
        
        private Decimal? _cnnCount;
        
        private Decimal? _cnnPrice;
        
        private Decimal? _cnnSum;
        
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
        public String cnvcProductType
        {
            get
            {
                return _cnvcProductType;
            }
            set
            {
                if ((value != _cnvcProductType))
                {
                    _cnvcProductType = value;
                    OnPropertyChanged("cnvcProductType");
                }
            }
        }
        
        [DataMember()]
        public String cnvcCode
        {
            get
            {
                return _cnvcCode;
            }
            set
            {
                if ((value != _cnvcCode))
                {
                    _cnvcCode = value;
                    OnPropertyChanged("cnvcCode");
                }
            }
        }
        
        [DataMember()]
        public String cnvcName
        {
            get
            {
                return _cnvcName;
            }
            set
            {
                if ((value != _cnvcName))
                {
                    _cnvcName = value;
                    OnPropertyChanged("cnvcName");
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
    }
}
