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
    public class tbMaterial : Entity
    {
        
        private String _cnvcMaterialCode;
        
        private String _cnvcMaterialName;
        
        private String _cnvcLeastUnit;
        
        private Decimal? _cnnConversion;
        
        private String _cnvcUnit;
        
        private String _cnvcStandardUnit;
        
        private Decimal? _cnnStatdardCount;
        
        private Decimal? _cnnPrice;
        
        private String _cnvcProductType;
        
        private String _cnvcProductClass;
        
        [DataMember()]
        public String cnvcMaterialCode
        {
            get
            {
                return _cnvcMaterialCode;
            }
            set
            {
                if ((value != _cnvcMaterialCode))
                {
                    _cnvcMaterialCode = value;
                    OnPropertyChanged("cnvcMaterialCode");
                }
            }
        }
        
        [DataMember()]
        public String cnvcMaterialName
        {
            get
            {
                return _cnvcMaterialName;
            }
            set
            {
                if ((value != _cnvcMaterialName))
                {
                    _cnvcMaterialName = value;
                    OnPropertyChanged("cnvcMaterialName");
                }
            }
        }
        
        [DataMember()]
        public String cnvcLeastUnit
        {
            get
            {
                return _cnvcLeastUnit;
            }
            set
            {
                if ((value != _cnvcLeastUnit))
                {
                    _cnvcLeastUnit = value;
                    OnPropertyChanged("cnvcLeastUnit");
                }
            }
        }
        
        [DataMember()]
        public Decimal? cnnConversion
        {
            get
            {
                return _cnnConversion;
            }
            set
            {
                if ((value != _cnnConversion))
                {
                    _cnnConversion = value;
                    OnPropertyChanged("cnnConversion");
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
        public Decimal? cnnStatdardCount
        {
            get
            {
                return _cnnStatdardCount;
            }
            set
            {
                if ((value != _cnnStatdardCount))
                {
                    _cnnStatdardCount = value;
                    OnPropertyChanged("cnnStatdardCount");
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
        public String cnvcProductClass
        {
            get
            {
                return _cnvcProductClass;
            }
            set
            {
                if ((value != _cnvcProductClass))
                {
                    _cnvcProductClass = value;
                    OnPropertyChanged("cnvcProductClass");
                }
            }
        }
    }
}
