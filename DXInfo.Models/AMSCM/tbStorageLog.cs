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
    public class tbStorageLog : Entity
    {
        
        private Int64 _cnnSerialNo;
        
        private String _cnvcStorageDeptID;
        
        private String _cnvcProductCode;
        
        private String _cnvcProductName;
        
        private String _cnvcUnit;
        
        private Decimal? _cnnCount;
        
        private Decimal? _cnnSafeCount;
        
        private Decimal? _cnnSafeUpCount;
        
        private String _cnvcOperType;
        
        private String _cnvcOperID;
        
        private DateTime? _cndOperDate;
        
        [DataMember()]
        public Int64 cnnSerialNo
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
        public String cnvcStorageDeptID
        {
            get
            {
                return _cnvcStorageDeptID;
            }
            set
            {
                if ((value != _cnvcStorageDeptID))
                {
                    _cnvcStorageDeptID = value;
                    OnPropertyChanged("cnvcStorageDeptID");
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
        public Decimal? cnnSafeCount
        {
            get
            {
                return _cnnSafeCount;
            }
            set
            {
                if ((value != _cnnSafeCount))
                {
                    _cnnSafeCount = value;
                    OnPropertyChanged("cnnSafeCount");
                }
            }
        }
        
        [DataMember()]
        public Decimal? cnnSafeUpCount
        {
            get
            {
                return _cnnSafeUpCount;
            }
            set
            {
                if ((value != _cnnSafeUpCount))
                {
                    _cnnSafeUpCount = value;
                    OnPropertyChanged("cnnSafeUpCount");
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
        public String cnvcOperID
        {
            get
            {
                return _cnvcOperID;
            }
            set
            {
                if ((value != _cnvcOperID))
                {
                    _cnvcOperID = value;
                    OnPropertyChanged("cnvcOperID");
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
    }
}
