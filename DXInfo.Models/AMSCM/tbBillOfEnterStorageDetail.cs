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
    public class tbBillOfEnterStorageDetail : Entity
    {
        
        private Decimal _cnnEnterSerialNo;
        
        private String _cnvcProviderCode;
        
        private String _cnvcProductCode;
        
        private String _cnvcProductName;
        
        private String _cnvcStandardUnit;
        
        private Decimal? _cnnStandardCount;
        
        private String _cnvcUnit;
        
        private Decimal? _cnnPrice;
        
        private Decimal? _cnnCount;
        
        private Decimal? _cnnSum;
        
        [DataMember()]
        public Decimal cnnEnterSerialNo
        {
            get
            {
                return _cnnEnterSerialNo;
            }
            set
            {
                if ((value != _cnnEnterSerialNo))
                {
                    _cnnEnterSerialNo = value;
                    OnPropertyChanged("cnnEnterSerialNo");
                }
            }
        }
        
        [DataMember()]
        public String cnvcProviderCode
        {
            get
            {
                return _cnvcProviderCode;
            }
            set
            {
                if ((value != _cnvcProviderCode))
                {
                    _cnvcProviderCode = value;
                    OnPropertyChanged("cnvcProviderCode");
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
        public String cnvcStandardUnit
        {
            get
            {
                return _cnvcStandardUnit;
            }
            set
            {
                if ((value != _cnvcStandardUnit))
                {
                    _cnvcStandardUnit = value;
                    OnPropertyChanged("cnvcStandardUnit");
                }
            }
        }
        
        [DataMember()]
        public Decimal? cnnStandardCount
        {
            get
            {
                return _cnnStandardCount;
            }
            set
            {
                if ((value != _cnnStandardCount))
                {
                    _cnnStandardCount = value;
                    OnPropertyChanged("cnnStandardCount");
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
    }
}
