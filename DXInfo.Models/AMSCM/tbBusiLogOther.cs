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
    public class tbBusiLogOther : Entity
    {
        
        private Int64 _iSerial;
        
        private Int64? _iAssID;
        
        private String _vcCardID;
        
        private String _vcOperType;
        
        private String _vcOperName;
        
        private DateTime? _dtOperDate;
        
        private String _vcComments;
        
        private String _vcDeptID;
        
        [DataMember()]
        public Int64 iSerial
        {
            get
            {
                return _iSerial;
            }
            set
            {
                if ((value != _iSerial))
                {
                    _iSerial = value;
                    OnPropertyChanged("iSerial");
                }
            }
        }
        
        [DataMember()]
        public Int64? iAssID
        {
            get
            {
                return _iAssID;
            }
            set
            {
                if ((value != _iAssID))
                {
                    _iAssID = value;
                    OnPropertyChanged("iAssID");
                }
            }
        }
        
        [DataMember()]
        public String vcCardID
        {
            get
            {
                return _vcCardID;
            }
            set
            {
                if ((value != _vcCardID))
                {
                    _vcCardID = value;
                    OnPropertyChanged("vcCardID");
                }
            }
        }
        
        [DataMember()]
        public String vcOperType
        {
            get
            {
                return _vcOperType;
            }
            set
            {
                if ((value != _vcOperType))
                {
                    _vcOperType = value;
                    OnPropertyChanged("vcOperType");
                }
            }
        }
        
        [DataMember()]
        public String vcOperName
        {
            get
            {
                return _vcOperName;
            }
            set
            {
                if ((value != _vcOperName))
                {
                    _vcOperName = value;
                    OnPropertyChanged("vcOperName");
                }
            }
        }
        
        [DataMember()]
        public DateTime? dtOperDate
        {
            get
            {
                return _dtOperDate;
            }
            set
            {
                if ((value != _dtOperDate))
                {
                    _dtOperDate = value;
                    OnPropertyChanged("dtOperDate");
                }
            }
        }
        
        [DataMember()]
        public String vcComments
        {
            get
            {
                return _vcComments;
            }
            set
            {
                if ((value != _vcComments))
                {
                    _vcComments = value;
                    OnPropertyChanged("vcComments");
                }
            }
        }
        
        [DataMember()]
        public String vcDeptID
        {
            get
            {
                return _vcDeptID;
            }
            set
            {
                if ((value != _vcDeptID))
                {
                    _vcDeptID = value;
                    OnPropertyChanged("vcDeptID");
                }
            }
        }
    }
}