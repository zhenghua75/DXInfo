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
    public class tbBillHis : Entity
    {
        
        private Int64 _iSerial;
        
        private Int64? _iAssID;
        
        private String _vcCardID;
        
        private Decimal? _nTRate;
        
        private Decimal? _nFee;
        
        private Decimal? _nPay;
        
        private Decimal? _nBalance;
        
        private Int32? _iIgValue;
        
        private String _vcConsType;
        
        private String _vcOperName;
        
        private DateTime? _dtConsDate;
        
        private String _vcDeptID;
        
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
        public Int64? iAssID
        {
            get
            {
                return _iAssID;
            }
            set
            {
                if ((value != _iAssID))
                {
                    _iAssID = value;
                    OnPropertyChanged("iAssID");
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
        public Decimal? nTRate
        {
            get
            {
                return _nTRate;
            }
            set
            {
                if ((value != _nTRate))
                {
                    _nTRate = value;
                    OnPropertyChanged("nTRate");
                }
            }
        }
        
        [DataMember()]
        public Decimal? nFee
        {
            get
            {
                return _nFee;
            }
            set
            {
                if ((value != _nFee))
                {
                    _nFee = value;
                    OnPropertyChanged("nFee");
                }
            }
        }
        
        [DataMember()]
        public Decimal? nPay
        {
            get
            {
                return _nPay;
            }
            set
            {
                if ((value != _nPay))
                {
                    _nPay = value;
                    OnPropertyChanged("nPay");
                }
            }
        }
        
        [DataMember()]
        public Decimal? nBalance
        {
            get
            {
                return _nBalance;
            }
            set
            {
                if ((value != _nBalance))
                {
                    _nBalance = value;
                    OnPropertyChanged("nBalance");
                }
            }
        }
        
        [DataMember()]
        public Int32? iIgValue
        {
            get
            {
                return _iIgValue;
            }
            set
            {
                if ((value != _iIgValue))
                {
                    _iIgValue = value;
                    OnPropertyChanged("iIgValue");
                }
            }
        }
        
        [DataMember()]
        public String vcConsType
        {
            get
            {
                return _vcConsType;
            }
            set
            {
                if ((value != _vcConsType))
                {
                    _vcConsType = value;
                    OnPropertyChanged("vcConsType");
                }
            }
        }
        
        [DataMember()]
        public String vcOperName
        {
            get
            {
                return _vcOperName;
            }
            set
            {
                if ((value != _vcOperName))
                {
                    _vcOperName = value;
                    OnPropertyChanged("vcOperName");
                }
            }
        }
        
        [DataMember()]
        public DateTime? dtConsDate
        {
            get
            {
                return _dtConsDate;
            }
            set
            {
                if ((value != _dtConsDate))
                {
                    _dtConsDate = value;
                    OnPropertyChanged("dtConsDate");
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
    }
}
