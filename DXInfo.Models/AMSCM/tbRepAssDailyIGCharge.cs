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
    public class tbRepAssDailyIGCharge : Entity
    {
        
        private String _vcCardID;
        
        private String _vcDate;
        
        private DateTime? _dtIgDate;
        
        private DateTime? _dtFillDate;
        
        private Decimal? _DailyIG;
        
        private Decimal? _DailyCharge;
        
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
        public String vcDate
        {
            get
            {
                return _vcDate;
            }
            set
            {
                if ((value != _vcDate))
                {
                    _vcDate = value;
                    OnPropertyChanged("vcDate");
                }
            }
        }
        
        [DataMember()]
        public DateTime? dtIgDate
        {
            get
            {
                return _dtIgDate;
            }
            set
            {
                if ((value != _dtIgDate))
                {
                    _dtIgDate = value;
                    OnPropertyChanged("dtIgDate");
                }
            }
        }
        
        [DataMember()]
        public DateTime? dtFillDate
        {
            get
            {
                return _dtFillDate;
            }
            set
            {
                if ((value != _dtFillDate))
                {
                    _dtFillDate = value;
                    OnPropertyChanged("dtFillDate");
                }
            }
        }
        
        [DataMember()]
        public Decimal? DailyIG
        {
            get
            {
                return _DailyIG;
            }
            set
            {
                if ((value != _DailyIG))
                {
                    _DailyIG = value;
                    OnPropertyChanged("DailyIG");
                }
            }
        }
        
        [DataMember()]
        public Decimal? DailyCharge
        {
            get
            {
                return _DailyCharge;
            }
            set
            {
                if ((value != _DailyCharge))
                {
                    _DailyCharge = value;
                    OnPropertyChanged("DailyCharge");
                }
            }
        }
    }
}