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
    public class tbProduceOrderLog : Entity
    {
        
        private Decimal _cnnProduceSerialNo;
        
        private Decimal _cnnOrderSerialNo;
        
        private String _cnvcType;
        
        [DataMember()]
        public Decimal cnnProduceSerialNo
        {
            get
            {
                return _cnnProduceSerialNo;
            }
            set
            {
                if ((value != _cnnProduceSerialNo))
                {
                    _cnnProduceSerialNo = value;
                    OnPropertyChanged("cnnProduceSerialNo");
                }
            }
        }
        
        [DataMember()]
        public Decimal cnnOrderSerialNo
        {
            get
            {
                return _cnnOrderSerialNo;
            }
            set
            {
                if ((value != _cnnOrderSerialNo))
                {
                    _cnnOrderSerialNo = value;
                    OnPropertyChanged("cnnOrderSerialNo");
                }
            }
        }
        
        [DataMember()]
        public String cnvcType
        {
            get
            {
                return _cnvcType;
            }
            set
            {
                if ((value != _cnvcType))
                {
                    _cnvcType = value;
                    OnPropertyChanged("cnvcType");
                }
            }
        }
    }
}
