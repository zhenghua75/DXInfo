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
    public class tbBillOfMaterials : Entity
    {
        
        private String _cnvcPartInvCode;
        
        private String _cnvcComponentInvCode;
        
        private Decimal _cnnBaseQtyN;
        
        private Decimal _cnnBaseQtyD;
        
        [DataMember()]
        public String cnvcPartInvCode
        {
            get
            {
                return _cnvcPartInvCode;
            }
            set
            {
                if ((value != _cnvcPartInvCode))
                {
                    _cnvcPartInvCode = value;
                    OnPropertyChanged("cnvcPartInvCode");
                }
            }
        }
        
        [DataMember()]
        public String cnvcComponentInvCode
        {
            get
            {
                return _cnvcComponentInvCode;
            }
            set
            {
                if ((value != _cnvcComponentInvCode))
                {
                    _cnvcComponentInvCode = value;
                    OnPropertyChanged("cnvcComponentInvCode");
                }
            }
        }
        
        [DataMember()]
        public Decimal cnnBaseQtyN
        {
            get
            {
                return _cnnBaseQtyN;
            }
            set
            {
                if ((value != _cnnBaseQtyN))
                {
                    _cnnBaseQtyN = value;
                    OnPropertyChanged("cnnBaseQtyN");
                }
            }
        }
        
        [DataMember()]
        public Decimal cnnBaseQtyD
        {
            get
            {
                return _cnnBaseQtyD;
            }
            set
            {
                if ((value != _cnnBaseQtyD))
                {
                    _cnnBaseQtyD = value;
                    OnPropertyChanged("cnnBaseQtyD");
                }
            }
        }
    }
}
