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
    public class tbSignList : Entity
    {
        
        private String _vcSignDate;
        
        private String _vcCardID;
        
        private String _vcClass;
        
        private String _vcEmpName;
        
        private String _vcDept;
        
        private String _vcOfficer;
        
        private DateTime? _dtSignIn;
        
        private DateTime? _dtSignOut;
        
        private String _vcSignState;
        
        private String _vcSignResult;
        
        private String _vcComments;
        
        [DataMember()]
        public String vcSignDate
        {
            get
            {
                return _vcSignDate;
            }
            set
            {
                if ((value != _vcSignDate))
                {
                    _vcSignDate = value;
                    OnPropertyChanged("vcSignDate");
                }
            }
        }
        
        [DataMember()]
        public String vcCardID
        {
            get
            {
                return _vcCardID;
            }
            set
            {
                if ((value != _vcCardID))
                {
                    _vcCardID = value;
                    OnPropertyChanged("vcCardID");
                }
            }
        }
        
        [DataMember()]
        public String vcClass
        {
            get
            {
                return _vcClass;
            }
            set
            {
                if ((value != _vcClass))
                {
                    _vcClass = value;
                    OnPropertyChanged("vcClass");
                }
            }
        }
        
        [DataMember()]
        public String vcEmpName
        {
            get
            {
                return _vcEmpName;
            }
            set
            {
                if ((value != _vcEmpName))
                {
                    _vcEmpName = value;
                    OnPropertyChanged("vcEmpName");
                }
            }
        }
        
        [DataMember()]
        public String vcDept
        {
            get
            {
                return _vcDept;
            }
            set
            {
                if ((value != _vcDept))
                {
                    _vcDept = value;
                    OnPropertyChanged("vcDept");
                }
            }
        }
        
        [DataMember()]
        public String vcOfficer
        {
            get
            {
                return _vcOfficer;
            }
            set
            {
                if ((value != _vcOfficer))
                {
                    _vcOfficer = value;
                    OnPropertyChanged("vcOfficer");
                }
            }
        }
        
        [DataMember()]
        public DateTime? dtSignIn
        {
            get
            {
                return _dtSignIn;
            }
            set
            {
                if ((value != _dtSignIn))
                {
                    _dtSignIn = value;
                    OnPropertyChanged("dtSignIn");
                }
            }
        }
        
        [DataMember()]
        public DateTime? dtSignOut
        {
            get
            {
                return _dtSignOut;
            }
            set
            {
                if ((value != _dtSignOut))
                {
                    _dtSignOut = value;
                    OnPropertyChanged("dtSignOut");
                }
            }
        }
        
        [DataMember()]
        public String vcSignState
        {
            get
            {
                return _vcSignState;
            }
            set
            {
                if ((value != _vcSignState))
                {
                    _vcSignState = value;
                    OnPropertyChanged("vcSignState");
                }
            }
        }
        
        [DataMember()]
        public String vcSignResult
        {
            get
            {
                return _vcSignResult;
            }
            set
            {
                if ((value != _vcSignResult))
                {
                    _vcSignResult = value;
                    OnPropertyChanged("vcSignResult");
                }
            }
        }
        
        [DataMember()]
        public String vcComments
        {
            get
            {
                return _vcComments;
            }
            set
            {
                if ((value != _vcComments))
                {
                    _vcComments = value;
                    OnPropertyChanged("vcComments");
                }
            }
        }
    }
}
