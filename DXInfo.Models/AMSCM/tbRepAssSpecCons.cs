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
    public class tbRepAssSpecCons : Entity
    {
        
        private String _vcDeptID;
        
        private String _vcDate;
        
        private Int32 _SpecConsTimes;
        
        private Int32 _GoodsCount;
        
        private String _vcConsType;
        
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
        public Int32 SpecConsTimes
        {
            get
            {
                return _SpecConsTimes;
            }
            set
            {
                if ((value != _SpecConsTimes))
                {
                    _SpecConsTimes = value;
                    OnPropertyChanged("SpecConsTimes");
                }
            }
        }
        
        [DataMember()]
        public Int32 GoodsCount
        {
            get
            {
                return _GoodsCount;
            }
            set
            {
                if ((value != _GoodsCount))
                {
                    _GoodsCount = value;
                    OnPropertyChanged("GoodsCount");
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
    }
}