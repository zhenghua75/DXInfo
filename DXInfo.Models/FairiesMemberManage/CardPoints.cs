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
    public class CardPoints : Entity
    {
        
        private Guid _Id;
        
        private Guid _Card;
        
        private Decimal _Point;
        
        private DateTime _CreateDate;
        
        private Guid _UserId;
        
        private Guid _DeptId;
        
        private Int32 _PointType;
        
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
        public Guid Card
        {
            get
            {
                return _Card;
            }
            set
            {
                if ((value != _Card))
                {
                    _Card = value;
                    OnPropertyChanged("Card");
                }
            }
        }
        
        [DataMember()]
        public Decimal Point
        {
            get
            {
                return _Point;
            }
            set
            {
                if ((value != _Point))
                {
                    _Point = value;
                    OnPropertyChanged("Point");
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
        public Int32 PointType
        {
            get
            {
                return _PointType;
            }
            set
            {
                if ((value != _PointType))
                {
                    _PointType = value;
                    OnPropertyChanged("PointType");
                }
            }
        }
    }
}
