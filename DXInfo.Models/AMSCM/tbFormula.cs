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
    public class tbFormula : Entity
    {
        
        private String _cnvcProductCode;
        
        private String _cnvcProductName;
        
        private String _cnvcProductType;
        
        private String _cnvcProductClass;
        
        private Byte[] _cnbProductImage;
        
        private Decimal? _cnnMaterialCostSum;
        
        private Decimal? _cnnPackingCostSum;
        
        private Decimal? _cnnCostSum;
        
        private String _cnvcUnit;
        
        private Decimal? _cnnPortionCount;
        
        private String _cnvcPortionUnit;
        
        private String _cnvcFeel;
        
        private String _cnvcOrganise;
        
        private String _cnvcColor;
        
        private String _cnvcTaste;
        
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
        
        [DataMember()]
        public Byte[] cnbProductImage
        {
            get
            {
                return _cnbProductImage;
            }
            set
            {
                if ((value != _cnbProductImage))
                {
                    _cnbProductImage = value;
                    OnPropertyChanged("cnbProductImage");
                }
            }
        }
        
        [DataMember()]
        public Decimal? cnnMaterialCostSum
        {
            get
            {
                return _cnnMaterialCostSum;
            }
            set
            {
                if ((value != _cnnMaterialCostSum))
                {
                    _cnnMaterialCostSum = value;
                    OnPropertyChanged("cnnMaterialCostSum");
                }
            }
        }
        
        [DataMember()]
        public Decimal? cnnPackingCostSum
        {
            get
            {
                return _cnnPackingCostSum;
            }
            set
            {
                if ((value != _cnnPackingCostSum))
                {
                    _cnnPackingCostSum = value;
                    OnPropertyChanged("cnnPackingCostSum");
                }
            }
        }
        
        [DataMember()]
        public Decimal? cnnCostSum
        {
            get
            {
                return _cnnCostSum;
            }
            set
            {
                if ((value != _cnnCostSum))
                {
                    _cnnCostSum = value;
                    OnPropertyChanged("cnnCostSum");
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
        public Decimal? cnnPortionCount
        {
            get
            {
                return _cnnPortionCount;
            }
            set
            {
                if ((value != _cnnPortionCount))
                {
                    _cnnPortionCount = value;
                    OnPropertyChanged("cnnPortionCount");
                }
            }
        }
        
        [DataMember()]
        public String cnvcPortionUnit
        {
            get
            {
                return _cnvcPortionUnit;
            }
            set
            {
                if ((value != _cnvcPortionUnit))
                {
                    _cnvcPortionUnit = value;
                    OnPropertyChanged("cnvcPortionUnit");
                }
            }
        }
        
        [DataMember()]
        public String cnvcFeel
        {
            get
            {
                return _cnvcFeel;
            }
            set
            {
                if ((value != _cnvcFeel))
                {
                    _cnvcFeel = value;
                    OnPropertyChanged("cnvcFeel");
                }
            }
        }
        
        [DataMember()]
        public String cnvcOrganise
        {
            get
            {
                return _cnvcOrganise;
            }
            set
            {
                if ((value != _cnvcOrganise))
                {
                    _cnvcOrganise = value;
                    OnPropertyChanged("cnvcOrganise");
                }
            }
        }
        
        [DataMember()]
        public String cnvcColor
        {
            get
            {
                return _cnvcColor;
            }
            set
            {
                if ((value != _cnvcColor))
                {
                    _cnvcColor = value;
                    OnPropertyChanged("cnvcColor");
                }
            }
        }
        
        [DataMember()]
        public String cnvcTaste
        {
            get
            {
                return _cnvcTaste;
            }
            set
            {
                if ((value != _cnvcTaste))
                {
                    _cnvcTaste = value;
                    OnPropertyChanged("cnvcTaste");
                }
            }
        }
    }
}
