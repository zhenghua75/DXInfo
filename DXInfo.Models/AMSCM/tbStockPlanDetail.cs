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
    public class tbStockPlanDetail : Entity
    {
        
        private String _cnvcProductCode;
        
        private String _cnvcProductName;
        
        private Decimal? _cnnPlanCount;
        
        private String _cnvcUnit;
        
        private String _cnvcMonth;
        
        private String _cnvcPlanDeptID;
        
        [DataMember()]
        public String cnvcProductCode
        {
            get
            {
                return _cnvcProductCode;
            }
            set
            {
                if ((value != _cnvcProductCode))
                {
                    _cnvcProductCode = value;
                    OnPropertyChanged("cnvcProductCode");
                }
            }
        }
        
        [DataMember()]
        public String cnvcProductName
        {
            get
            {
                return _cnvcProductName;
            }
            set
            {
                if ((value != _cnvcProductName))
                {
                    _cnvcProductName = value;
                    OnPropertyChanged("cnvcProductName");
                }
            }
        }
        
        [DataMember()]
        public Decimal? cnnPlanCount
        {
            get
            {
                return _cnnPlanCount;
            }
            set
            {
                if ((value != _cnnPlanCount))
                {
                    _cnnPlanCount = value;
                    OnPropertyChanged("cnnPlanCount");
                }
            }
        }
        
        [DataMember()]
        public String cnvcUnit
        {
            get
            {
                return _cnvcUnit;
            }
            set
            {
                if ((value != _cnvcUnit))
                {
                    _cnvcUnit = value;
                    OnPropertyChanged("cnvcUnit");
                }
            }
        }
        
        [DataMember()]
        public String cnvcMonth
        {
            get
            {
                return _cnvcMonth;
            }
            set
            {
                if ((value != _cnvcMonth))
                {
                    _cnvcMonth = value;
                    OnPropertyChanged("cnvcMonth");
                }
            }
        }
        
        [DataMember()]
        public String cnvcPlanDeptID
        {
            get
            {
                return _cnvcPlanDeptID;
            }
            set
            {
                if ((value != _cnvcPlanDeptID))
                {
                    _cnvcPlanDeptID = value;
                    OnPropertyChanged("cnvcPlanDeptID");
                }
            }
        }
    }
}
