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
    public class tbOper : Entity
    {
        
        private String _vcOperID;
        
        private String _vcOperName;
        
        private String _vcLimit;
        
        private String _vcPwd;
        
        private String _vcDeptID;
        
        private String _vcActiveFlag;
        
        private String _vcPwdBeginFlag;

        private bool? _IsDiscount;

        [DataMember()]
        public String vcOperID
        {
            get
            {
                return _vcOperID;
            }
            set
            {
                if ((value != _vcOperID))
                {
                    _vcOperID = value;
                    OnPropertyChanged("vcOperID");
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
        public String vcLimit
        {
            get
            {
                return _vcLimit;
            }
            set
            {
                if ((value != _vcLimit))
                {
                    _vcLimit = value;
                    OnPropertyChanged("vcLimit");
                }
            }
        }
        
        [DataMember()]
        public String vcPwd
        {
            get
            {
                return _vcPwd;
            }
            set
            {
                if ((value != _vcPwd))
                {
                    _vcPwd = value;
                    OnPropertyChanged("vcPwd");
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
        
        [DataMember()]
        public String vcActiveFlag
        {
            get
            {
                return _vcActiveFlag;
            }
            set
            {
                if ((value != _vcActiveFlag))
                {
                    _vcActiveFlag = value;
                    OnPropertyChanged("vcActiveFlag");
                }
            }
        }
        
        [DataMember()]
        public String vcPwdBeginFlag
        {
            get
            {
                return _vcPwdBeginFlag;
            }
            set
            {
                if ((value != _vcPwdBeginFlag))
                {
                    _vcPwdBeginFlag = value;
                    OnPropertyChanged("vcPwdBeginFlag");
                }
            }
        }

        [DataMember()]
        public bool? IsDiscount
        {
            get
            {
                return _IsDiscount;
            }
            set
            {
                if ((value != _IsDiscount))
                {
                    _IsDiscount = value;
                    OnPropertyChanged("IsDiscount");
                }
            }
        }
    }
}
