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
    public class Rooms : Entity
    {
        
        private Guid _Id;
        
        private Guid _DeptId;
        
        private String _Code;
        
        private String _Name;
        
        private Int32 _Status;
        
        private String _Comment;
        
        [DataMember()]
        public Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if ((value != _Id))
                {
                    _Id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
        
        [DataMember()]
        public Guid DeptId
        {
            get
            {
                return _DeptId;
            }
            set
            {
                if ((value != _DeptId))
                {
                    _DeptId = value;
                    OnPropertyChanged("DeptId");
                }
            }
        }
        
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
        public Int32 Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if ((value != _Status))
                {
                    _Status = value;
                    OnPropertyChanged("Status");
                }
            }
        }
        
        [DataMember()]
        public String Comment
        {
            get
            {
                return _Comment;
            }
            set
            {
                if ((value != _Comment))
                {
                    _Comment = value;
                    OnPropertyChanged("Comment");
                }
            }
        }
    }
}
