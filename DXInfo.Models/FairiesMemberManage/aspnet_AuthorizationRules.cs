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
    public class aspnet_AuthorizationRules : Entity
    {
        
        private Guid _RuleId;
        
        private String _SiteMapKey;
        
        private Guid? _RoleId;
        
        private Guid? _UserId;
        
        [DataMember()]
        public Guid RuleId
        {
            get
            {
                return _RuleId;
            }
            set
            {
                if ((value != _RuleId))
                {
                    _RuleId = value;
                    OnPropertyChanged("RuleId");
                }
            }
        }
        
        [DataMember()]
        public String SiteMapKey
        {
            get
            {
                return _SiteMapKey;
            }
            set
            {
                if ((value != _SiteMapKey))
                {
                    _SiteMapKey = value;
                    OnPropertyChanged("SiteMapKey");
                }
            }
        }
        
        [DataMember()]
        public Guid? RoleId
        {
            get
            {
                return _RoleId;
            }
            set
            {
                if ((value != _RoleId))
                {
                    _RoleId = value;
                    OnPropertyChanged("RoleId");
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
