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
    public class InventoryCategory : Entity
    {
        
        private Guid _Id;
        
        private String _Code;
        
        private String _Name;
        
        private String _Comment;
        
        private Boolean _IsDiscount;
        
        private Int32 _CategoryType;
        
        private Int32? _SectionType;
        
        private Int32? _ProductType;
        
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
        
        [DataMember()]
        public Boolean IsDiscount
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
        
        [DataMember()]
        public Int32 CategoryType
        {
            get
            {
                return _CategoryType;
            }
            set
            {
                if ((value != _CategoryType))
                {
                    _CategoryType = value;
                    OnPropertyChanged("CategoryType");
                }
            }
        }
        
        [DataMember()]
        public Int32? SectionType
        {
            get
            {
                return _SectionType;
            }
            set
            {
                if ((value != _SectionType))
                {
                    _SectionType = value;
                    OnPropertyChanged("SectionType");
                }
            }
        }
        
        [DataMember()]
        public Int32? ProductType
        {
            get
            {
                return _ProductType;
            }
            set
            {
                if ((value != _ProductType))
                {
                    _ProductType = value;
                    OnPropertyChanged("ProductType");
                }
            }
        }
    }
}