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
    public class tbCheckSerial : Entity
    {
        
        private Int32 _cnnSerialNo;
        
        private String _cnvcName;
        
        private String _cnvcCode;
        
        private Int32 _cnnCount;
        
        private Int32 _cnnAddCount;
        
        private Int32 _cnnReduceCount;
        
        private DateTime _cndCreateDate;
        
        private String _cnvcDeptID;
        
        private String _cnvcOperID;
        
        private DateTime? _cndOperDate;
        
        private String _cnvcComments;
        
        private Boolean? _cnbIsSales;
        
        [DataMember()]
        public Int32 cnnSerialNo
        {
            get
            {
                return _cnnSerialNo;
            }
            set
            {
                if ((value != _cnnSerialNo))
                {
                    _cnnSerialNo = value;
                    OnPropertyChanged("cnnSerialNo");
                }
            }
        }
        
        [DataMember()]
        public String cnvcName
        {
            get
            {
                return _cnvcName;
            }
            set
            {
                if ((value != _cnvcName))
                {
                    _cnvcName = value;
                    OnPropertyChanged("cnvcName");
                }
            }
        }
        
        [DataMember()]
        public String cnvcCode
        {
            get
            {
                return _cnvcCode;
            }
            set
            {
                if ((value != _cnvcCode))
                {
                    _cnvcCode = value;
                    OnPropertyChanged("cnvcCode");
                }
            }
        }
        
        [DataMember()]
        public Int32 cnnCount
        {
            get
            {
                return _cnnCount;
            }
            set
            {
                if ((value != _cnnCount))
                {
                    _cnnCount = value;
                    OnPropertyChanged("cnnCount");
                }
            }
        }
        
        [DataMember()]
        public Int32 cnnAddCount
        {
            get
            {
                return _cnnAddCount;
            }
            set
            {
                if ((value != _cnnAddCount))
                {
                    _cnnAddCount = value;
                    OnPropertyChanged("cnnAddCount");
                }
            }
        }
        
        [DataMember()]
        public Int32 cnnReduceCount
        {
            get
            {
                return _cnnReduceCount;
            }
            set
            {
                if ((value != _cnnReduceCount))
                {
                    _cnnReduceCount = value;
                    OnPropertyChanged("cnnReduceCount");
                }
            }
        }
        
        [DataMember()]
        public DateTime cndCreateDate
        {
            get
            {
                return _cndCreateDate;
            }
            set
            {
                if ((value != _cndCreateDate))
                {
                    _cndCreateDate = value;
                    OnPropertyChanged("cndCreateDate");
                }
            }
        }
        
        [DataMember()]
        public String cnvcDeptID
        {
            get
            {
                return _cnvcDeptID;
            }
            set
            {
                if ((value != _cnvcDeptID))
                {
                    _cnvcDeptID = value;
                    OnPropertyChanged("cnvcDeptID");
                }
            }
        }
        
        [DataMember()]
        public String cnvcOperID
        {
            get
            {
                return _cnvcOperID;
            }
            set
            {
                if ((value != _cnvcOperID))
                {
                    _cnvcOperID = value;
                    OnPropertyChanged("cnvcOperID");
                }
            }
        }
        
        [DataMember()]
        public DateTime? cndOperDate
        {
            get
            {
                return _cndOperDate;
            }
            set
            {
                if ((value != _cndOperDate))
                {
                    _cndOperDate = value;
                    OnPropertyChanged("cndOperDate");
                }
            }
        }
        
        [DataMember()]
        public String cnvcComments
        {
            get
            {
                return _cnvcComments;
            }
            set
            {
                if ((value != _cnvcComments))
                {
                    _cnvcComments = value;
                    OnPropertyChanged("cnvcComments");
                }
            }
        }
        
        [DataMember()]
        public Boolean? cnbIsSales
        {
            get
            {
                return _cnbIsSales;
            }
            set
            {
                if ((value != _cnbIsSales))
                {
                    _cnbIsSales = value;
                    OnPropertyChanged("cnbIsSales");
                }
            }
        }
    }
}