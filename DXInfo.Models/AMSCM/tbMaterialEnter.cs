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
    public class tbMaterialEnter : Entity
    {
        
        private Decimal _cnnSerialNo;
        
        private String _cnvcBatchNo;
        
        private Decimal _cnnMaterialCode;
        
        private String _cnvcMaterialName;
        
        private String _cnvcStandardUnit;
        
        private String _cnvcUnit;
        
        private Decimal? _cnnPrice;
        
        private String _cnvcProviderName;
        
        private String _cnvcMaterialType;
        
        private Decimal? _cnnLastCount;
        
        private Decimal? _cnnEnterCount;
        
        private Decimal? _cnnCount;
        
        private String _cnvcOperType;
        
        private Decimal? _cnnLinkSerialNo;
        
        private DateTime? _cndEnterDate;
        
        private String _cnvcDeptID;
        
        private DateTime? _cndOperDate;
        
        private String _cnvcOperName;
        
        [DataMember()]
        public Decimal cnnSerialNo
        {
            get
            {
                return _cnnSerialNo;
            }
            set
            {
                if ((value != _cnnSerialNo))
                {
                    _cnnSerialNo = value;
                    OnPropertyChanged("cnnSerialNo");
                }
            }
        }
        
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
        public Decimal cnnMaterialCode
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
        public Decimal? cnnLastCount
        {
            get
            {
                return _cnnLastCount;
            }
            set
            {
                if ((value != _cnnLastCount))
                {
                    _cnnLastCount = value;
                    OnPropertyChanged("cnnLastCount");
                }
            }
        }
        
        [DataMember()]
        public Decimal? cnnEnterCount
        {
            get
            {
                return _cnnEnterCount;
            }
            set
            {
                if ((value != _cnnEnterCount))
                {
                    _cnnEnterCount = value;
                    OnPropertyChanged("cnnEnterCount");
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
        public String cnvcOperType
        {
            get
            {
                return _cnvcOperType;
            }
            set
            {
                if ((value != _cnvcOperType))
                {
                    _cnvcOperType = value;
                    OnPropertyChanged("cnvcOperType");
                }
            }
        }
        
        [DataMember()]
        public Decimal? cnnLinkSerialNo
        {
            get
            {
                return _cnnLinkSerialNo;
            }
            set
            {
                if ((value != _cnnLinkSerialNo))
                {
                    _cnnLinkSerialNo = value;
                    OnPropertyChanged("cnnLinkSerialNo");
                }
            }
        }
        
        [DataMember()]
        public DateTime? cndEnterDate
        {
            get
            {
                return _cndEnterDate;
            }
            set
            {
                if ((value != _cndEnterDate))
                {
                    _cndEnterDate = value;
                    OnPropertyChanged("cndEnterDate");
                }
            }
        }
        
        [DataMember()]
        public String cnvcDeptID
        {
            get
            {
                return _cnvcDeptID;
            }
            set
            {
                if ((value != _cnvcDeptID))
                {
                    _cnvcDeptID = value;
                    OnPropertyChanged("cnvcDeptID");
                }
            }
        }
        
        [DataMember()]
        public DateTime? cndOperDate
        {
            get
            {
                return _cndOperDate;
            }
            set
            {
                if ((value != _cndOperDate))
                {
                    _cndOperDate = value;
                    OnPropertyChanged("cndOperDate");
                }
            }
        }
        
        [DataMember()]
        public String cnvcOperName
        {
            get
            {
                return _cnvcOperName;
            }
            set
            {
                if ((value != _cnvcOperName))
                {
                    _cnvcOperName = value;
                    OnPropertyChanged("cnvcOperName");
                }
            }
        }
    }
}