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
    public class OrderDishes : Entity
    {
        
        private Guid _Id;
        
        private Int32 _OrderNo;
        
        private String _OrderPwd;
        
        private Int32 _Quantity;
        
        private DateTime _CreateDate;
        
        private Guid? _DeptId;
        
        private Guid? _UserId;
        
        private Boolean _IsIpad;
        
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
        public Int32 OrderNo
        {
            get
            {
                return _OrderNo;
            }
            set
            {
                if ((value != _OrderNo))
                {
                    _OrderNo = value;
                    OnPropertyChanged("OrderNo");
                }
            }
        }
        
        [DataMember()]
        public String OrderPwd
        {
            get
            {
                return _OrderPwd;
            }
            set
            {
                if ((value != _OrderPwd))
                {
                    _OrderPwd = value;
                    OnPropertyChanged("OrderPwd");
                }
            }
        }
        
        [DataMember()]
        public Int32 Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                if ((value != _Quantity))
                {
                    _Quantity = value;
                    OnPropertyChanged("Quantity");
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
        public Guid? DeptId
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
        public Guid? UserId
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
        public Boolean IsIpad
        {
            get
            {
                return _IsIpad;
            }
            set
            {
                if ((value != _IsIpad))
                {
                    _IsIpad = value;
                    OnPropertyChanged("IsIpad");
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
