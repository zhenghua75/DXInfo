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
    public class tbSupplier : Entity
    {
        
        private String _cnvcCode;
        
        private String _cnvcName;
        
        private String _cnvcAddress;
        
        private String _cnvcPostCode;
        
        private String _cnvcPhone;
        
        private String _cnvcFax;
        
        private String _cnvcEmail;
        
        private String _cnvcLinkName;
        
        private String _cnvcCreateOper;
        
        private DateTime _cndCreateDate;
        
        private Boolean _cnbInvalid;
        
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
        public String cnvcAddress
        {
            get
            {
                return _cnvcAddress;
            }
            set
            {
                if ((value != _cnvcAddress))
                {
                    _cnvcAddress = value;
                    OnPropertyChanged("cnvcAddress");
                }
            }
        }
        
        [DataMember()]
        public String cnvcPostCode
        {
            get
            {
                return _cnvcPostCode;
            }
            set
            {
                if ((value != _cnvcPostCode))
                {
                    _cnvcPostCode = value;
                    OnPropertyChanged("cnvcPostCode");
                }
            }
        }
        
        [DataMember()]
        public String cnvcPhone
        {
            get
            {
                return _cnvcPhone;
            }
            set
            {
                if ((value != _cnvcPhone))
                {
                    _cnvcPhone = value;
                    OnPropertyChanged("cnvcPhone");
                }
            }
        }
        
        [DataMember()]
        public String cnvcFax
        {
            get
            {
                return _cnvcFax;
            }
            set
            {
                if ((value != _cnvcFax))
                {
                    _cnvcFax = value;
                    OnPropertyChanged("cnvcFax");
                }
            }
        }
        
        [DataMember()]
        public String cnvcEmail
        {
            get
            {
                return _cnvcEmail;
            }
            set
            {
                if ((value != _cnvcEmail))
                {
                    _cnvcEmail = value;
                    OnPropertyChanged("cnvcEmail");
                }
            }
        }
        
        [DataMember()]
        public String cnvcLinkName
        {
            get
            {
                return _cnvcLinkName;
            }
            set
            {
                if ((value != _cnvcLinkName))
                {
                    _cnvcLinkName = value;
                    OnPropertyChanged("cnvcLinkName");
                }
            }
        }
        
        [DataMember()]
        public String cnvcCreateOper
        {
            get
            {
                return _cnvcCreateOper;
            }
            set
            {
                if ((value != _cnvcCreateOper))
                {
                    _cnvcCreateOper = value;
                    OnPropertyChanged("cnvcCreateOper");
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
        public Boolean cnbInvalid
        {
            get
            {
                return _cnbInvalid;
            }
            set
            {
                if ((value != _cnbInvalid))
                {
                    _cnbInvalid = value;
                    OnPropertyChanged("cnbInvalid");
                }
            }
        }
    }
}
