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
    public class CheckDifferences : Entity
    {
        
        private Guid _Id;
        
        private Guid _DeptId;
        
        private Guid _UserId;
        
        private Decimal _Amount;
        
        private Decimal _More;
        
        private Decimal _Less;
        
        private DateTime _DifDate;
        
        private Guid _OperUserId;
        
        private Guid _OperDeptId;
        
        private DateTime _CreateDate;
        
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
        public Guid UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if ((value != _UserId))
                {
                    _UserId = value;
                    OnPropertyChanged("UserId");
                }
            }
        }
        
        [DataMember()]
        public Decimal Amount
        {
            get
            {
                return _Amount;
            }
            set
            {
                if ((value != _Amount))
                {
                    _Amount = value;
                    OnPropertyChanged("Amount");
                }
            }
        }
        
        [DataMember()]
        public Decimal More
        {
            get
            {
                return _More;
            }
            set
            {
                if ((value != _More))
                {
                    _More = value;
                    OnPropertyChanged("More");
                }
            }
        }
        
        [DataMember()]
        public Decimal Less
        {
            get
            {
                return _Less;
            }
            set
            {
                if ((value != _Less))
                {
                    _Less = value;
                    OnPropertyChanged("Less");
                }
            }
        }
        
        [DataMember()]
        public DateTime DifDate
        {
            get
            {
                return _DifDate;
            }
            set
            {
                if ((value != _DifDate))
                {
                    _DifDate = value;
                    OnPropertyChanged("DifDate");
                }
            }
        }
        
        [DataMember()]
        public Guid OperUserId
        {
            get
            {
                return _OperUserId;
            }
            set
            {
                if ((value != _OperUserId))
                {
                    _OperUserId = value;
                    OnPropertyChanged("OperUserId");
                }
            }
        }
        
        [DataMember()]
        public Guid OperDeptId
        {
            get
            {
                return _OperDeptId;
            }
            set
            {
                if ((value != _OperDeptId))
                {
                    _OperDeptId = value;
                    OnPropertyChanged("OperDeptId");
                }
            }
        }
        
        [DataMember()]
        public DateTime CreateDate
        {
            get
            {
                return _CreateDate;
            }
            set
            {
                if ((value != _CreateDate))
                {
                    _CreateDate = value;
                    OnPropertyChanged("CreateDate");
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