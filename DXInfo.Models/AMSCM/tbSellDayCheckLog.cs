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
    public class tbSellDayCheckLog : Entity
    {
        
        private Int64 _cnnCheckSerialNo;
        
        private String _cnvcCheckDeptID;
        
        private DateTime? _cndCheckDate;
        
        private String _cnvcWeather;
        
        private String _cnvcCheckOperID;
        
        private String _cnvcManageOperID;
        
        private String _cnvcOperID;
        
        private DateTime? _cndOperDate;
        
        [DataMember()]
        public Int64 cnnCheckSerialNo
        {
            get
            {
                return _cnnCheckSerialNo;
            }
            set
            {
                if ((value != _cnnCheckSerialNo))
                {
                    _cnnCheckSerialNo = value;
                    OnPropertyChanged("cnnCheckSerialNo");
                }
            }
        }
        
        [DataMember()]
        public String cnvcCheckDeptID
        {
            get
            {
                return _cnvcCheckDeptID;
            }
            set
            {
                if ((value != _cnvcCheckDeptID))
                {
                    _cnvcCheckDeptID = value;
                    OnPropertyChanged("cnvcCheckDeptID");
                }
            }
        }
        
        [DataMember()]
        public DateTime? cndCheckDate
        {
            get
            {
                return _cndCheckDate;
            }
            set
            {
                if ((value != _cndCheckDate))
                {
                    _cndCheckDate = value;
                    OnPropertyChanged("cndCheckDate");
                }
            }
        }
        
        [DataMember()]
        public String cnvcWeather
        {
            get
            {
                return _cnvcWeather;
            }
            set
            {
                if ((value != _cnvcWeather))
                {
                    _cnvcWeather = value;
                    OnPropertyChanged("cnvcWeather");
                }
            }
        }
        
        [DataMember()]
        public String cnvcCheckOperID
        {
            get
            {
                return _cnvcCheckOperID;
            }
            set
            {
                if ((value != _cnvcCheckOperID))
                {
                    _cnvcCheckOperID = value;
                    OnPropertyChanged("cnvcCheckOperID");
                }
            }
        }
        
        [DataMember()]
        public String cnvcManageOperID
        {
            get
            {
                return _cnvcManageOperID;
            }
            set
            {
                if ((value != _cnvcManageOperID))
                {
                    _cnvcManageOperID = value;
                    OnPropertyChanged("cnvcManageOperID");
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
    }
}
