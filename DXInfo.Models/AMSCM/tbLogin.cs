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
    public class tbLogin : Entity
    {
        
        private String _vcLoginID;
        
        private String _vcOperName;
        
        private String _vcLimit;
        
        private String _vcPwd;
        
        private String _vcDeptID;
        
        [DataMember()]
        public String vcLoginID
        {
            get
            {
                return _vcLoginID;
            }
            set
            {
                if ((value != _vcLoginID))
                {
                    _vcLoginID = value;
                    OnPropertyChanged("vcLoginID");
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
    }
}
