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
    public class OrderBookDeskes : Entity
    {
        
        private Guid _Id;
        
        private Guid _OrderBookId;
        
        private Guid _DeskId;
        
        private DateTime _CreateDate;
        
        private Int32 _Status;
        
        private Guid? _UserId;
        
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
        public Guid OrderBookId
        {
            get
            {
                return _OrderBookId;
            }
            set
            {
                if ((value != _OrderBookId))
                {
                    _OrderBookId = value;
                    OnPropertyChanged("OrderBookId");
                }
            }
        }
        
        [DataMember()]
        public Guid DeskId
        {
            get
            {
                return _DeskId;
            }
            set
            {
                if ((value != _DeskId))
                {
                    _DeskId = value;
                    OnPropertyChanged("DeskId");
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
    }
}
