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
    public class tbSysErrorLog : Entity
    {
        
        private Int64 _iSerial;
        
        private String _vcDeptID;
        
        private DateTime? _dtErrDate;
        
        private String _vcErrorDes;
        
        [DataMember()]
        public Int64 iSerial
        {
            get
            {
                return _iSerial;
            }
            set
            {
                if ((value != _iSerial))
                {
                    _iSerial = value;
                    OnPropertyChanged("iSerial");
                }
            }
        }
        
        [DataMember()]
        public String vcDeptID
        {
            get
            {
                return _vcDeptID;
            }
            set
            {
                if ((value != _vcDeptID))
                {
                    _vcDeptID = value;
                    OnPropertyChanged("vcDeptID");
                }
            }
        }
        
        [DataMember()]
        public DateTime? dtErrDate
        {
            get
            {
                return _dtErrDate;
            }
            set
            {
                if ((value != _dtErrDate))
                {
                    _dtErrDate = value;
                    OnPropertyChanged("dtErrDate");
                }
            }
        }
        
        [DataMember()]
        public String vcErrorDes
        {
            get
            {
                return _vcErrorDes;
            }
            set
            {
                if ((value != _vcErrorDes))
                {
                    _vcErrorDes = value;
                    OnPropertyChanged("vcErrorDes");
                }
            }
        }
    }
}
