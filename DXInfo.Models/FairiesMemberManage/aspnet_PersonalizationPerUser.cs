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
    public class aspnet_PersonalizationPerUser : Entity
    {
        
        private Guid _Id;
        
        private Guid? _PathId;
        
        private Guid? _UserId;
        
        private Byte[] _PageSettings;
        
        private DateTime _LastUpdatedDate;
        
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
        public Guid? PathId
        {
            get
            {
                return _PathId;
            }
            set
            {
                if ((value != _PathId))
                {
                    _PathId = value;
                    OnPropertyChanged("PathId");
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
        public Byte[] PageSettings
        {
            get
            {
                return _PageSettings;
            }
            set
            {
                if ((value != _PageSettings))
                {
                    _PageSettings = value;
                    OnPropertyChanged("PageSettings");
                }
            }
        }
        
        [DataMember()]
        public DateTime LastUpdatedDate
        {
            get
            {
                return _LastUpdatedDate;
            }
            set
            {
                if ((value != _LastUpdatedDate))
                {
                    _LastUpdatedDate = value;
                    OnPropertyChanged("LastUpdatedDate");
                }
            }
        }
    }
}