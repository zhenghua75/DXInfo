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
    public class InvDepts : Entity
    {
        
        private Guid _Id;
        
        private Guid _Inv;
        
        private Guid _Dept;
        
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
        public Guid Inv
        {
            get
            {
                return _Inv;
            }
            set
            {
                if ((value != _Inv))
                {
                    _Inv = value;
                    OnPropertyChanged("Inv");
                }
            }
        }
        
        [DataMember()]
        public Guid Dept
        {
            get
            {
                return _Dept;
            }
            set
            {
                if ((value != _Dept))
                {
                    _Dept = value;
                    OnPropertyChanged("Dept");
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
