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
    public class tbRepAssLarg : Entity
    {
        
        private String _vcDeptID;
        
        private String _vcDate;
        
        private Int32 _LargTimes;
        
        private Int32 _GoodsCount;
        
        private String _vcAssBelongDept;
        
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
        public Int32 LargTimes
        {
            get
            {
                return _LargTimes;
            }
            set
            {
                if ((value != _LargTimes))
                {
                    _LargTimes = value;
                    OnPropertyChanged("LargTimes");
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
        public String vcAssBelongDept
        {
            get
            {
                return _vcAssBelongDept;
            }
            set
            {
                if ((value != _vcAssBelongDept))
                {
                    _vcAssBelongDept = value;
                    OnPropertyChanged("vcAssBelongDept");
                }
            }
        }
    }
}
