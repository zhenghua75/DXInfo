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
    public class tbMaterialPara : Entity
    {
        
        private String _cnvcBatchNo;
        
        private Int64 _cnnMaterialCode;
        
        private String _cnvcMaterialName;
        
        private String _cnvcStandardUnit;
        
        private String _cnvcUnit;
        
        private Decimal? _cnnPrice;
        
        private String _cnvcProviderName;
        
        private String _cnvcMaterialType;
        
        private Decimal? _cnnAlarmCount;
        
        private Decimal? _cnnCurCount;
        
        private String _cnvcFlag;
        
        [DataMember()]
        public String cnvcBatchNo
        {
            get
            {
                return _cnvcBatchNo;
            }
            set
            {
                if ((value != _cnvcBatchNo))
                {
                    _cnvcBatchNo = value;
                    OnPropertyChanged("cnvcBatchNo");
                }
            }
        }
        
        [DataMember()]
        public Int64 cnnMaterialCode
        {
            get
            {
                return _cnnMaterialCode;
            }
            set
            {
                if ((value != _cnnMaterialCode))
                {
                    _cnnMaterialCode = value;
                    OnPropertyChanged("cnnMaterialCode");
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
        public String cnvcProviderName
        {
            get
            {
                return _cnvcProviderName;
            }
            set
            {
                if ((value != _cnvcProviderName))
                {
                    _cnvcProviderName = value;
                    OnPropertyChanged("cnvcProviderName");
                }
            }
        }
        
        [DataMember()]
        public String cnvcMaterialType
        {
            get
            {
                return _cnvcMaterialType;
            }
            set
            {
                if ((value != _cnvcMaterialType))
                {
                    _cnvcMaterialType = value;
                    OnPropertyChanged("cnvcMaterialType");
                }
            }
        }
        
        [DataMember()]
        public Decimal? cnnAlarmCount
        {
            get
            {
                return _cnnAlarmCount;
            }
            set
            {
                if ((value != _cnnAlarmCount))
                {
                    _cnnAlarmCount = value;
                    OnPropertyChanged("cnnAlarmCount");
                }
            }
        }
        
        [DataMember()]
        public Decimal? cnnCurCount
        {
            get
            {
                return _cnnCurCount;
            }
            set
            {
                if ((value != _cnnCurCount))
                {
                    _cnnCurCount = value;
                    OnPropertyChanged("cnnCurCount");
                }
            }
        }
        
        [DataMember()]
        public String cnvcFlag
        {
            get
            {
                return _cnvcFlag;
            }
            set
            {
                if ((value != _cnvcFlag))
                {
                    _cnvcFlag = value;
                    OnPropertyChanged("cnvcFlag");
                }
            }
        }
    }
}