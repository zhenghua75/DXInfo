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
    public class STType : Entity
    {
        
        private String _Code;
        
        private String _Name;
        
        private String _RdCode;
        
        private Boolean _IsDefault;
        
        [DataMember()]
        public String Code
        {
            get
            {
                return _Code;
            }
            set
            {
                if ((value != _Code))
                {
                    _Code = value;
                    OnPropertyChanged("Code");
                }
            }
        }
        
        [DataMember()]
        public String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if ((value != _Name))
                {
                    _Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        
        [DataMember()]
        public String RdCode
        {
            get
            {
                return _RdCode;
            }
            set
            {
                if ((value != _RdCode))
                {
                    _RdCode = value;
                    OnPropertyChanged("RdCode");
                }
            }
        }
        
        [DataMember()]
        public Boolean IsDefault
        {
            get
            {
                return _IsDefault;
            }
            set
            {
                if ((value != _IsDefault))
                {
                    _IsDefault = value;
                    OnPropertyChanged("IsDefault");
                }
            }
        }
    }
}
