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
    public class tbOrderBook : Entity
    {
        
        private Decimal _cnnOrderSerialNo;
        
        private String _cnvcOrderDeptID;
        
        private String _cnvcProduceDeptID;
        
        private String _cnvcOrderType;
        
        private String _cnvcOrderComments;
        
        private String _cnvcOrderOperID;
        
        private DateTime? _cndOrderDate;
        
        private DateTime? _cndShipDate;
        
        private String _cnvcCustomName;
        
        private String _cnvcShipAddress;
        
        private String _cnvcLinkPhone;
        
        private DateTime? _cndArrivedDate;
        
        private String _cnvcOrderState;
        
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
        public String cnvcOrderDeptID
        {
            get
            {
                return _cnvcOrderDeptID;
            }
            set
            {
                if ((value != _cnvcOrderDeptID))
                {
                    _cnvcOrderDeptID = value;
                    OnPropertyChanged("cnvcOrderDeptID");
                }
            }
        }
        
        [DataMember()]
        public String cnvcProduceDeptID
        {
            get
            {
                return _cnvcProduceDeptID;
            }
            set
            {
                if ((value != _cnvcProduceDeptID))
                {
                    _cnvcProduceDeptID = value;
                    OnPropertyChanged("cnvcProduceDeptID");
                }
            }
        }
        
        [DataMember()]
        public String cnvcOrderType
        {
            get
            {
                return _cnvcOrderType;
            }
            set
            {
                if ((value != _cnvcOrderType))
                {
                    _cnvcOrderType = value;
                    OnPropertyChanged("cnvcOrderType");
                }
            }
        }
        
        [DataMember()]
        public String cnvcOrderComments
        {
            get
            {
                return _cnvcOrderComments;
            }
            set
            {
                if ((value != _cnvcOrderComments))
                {
                    _cnvcOrderComments = value;
                    OnPropertyChanged("cnvcOrderComments");
                }
            }
        }
        
        [DataMember()]
        public String cnvcOrderOperID
        {
            get
            {
                return _cnvcOrderOperID;
            }
            set
            {
                if ((value != _cnvcOrderOperID))
                {
                    _cnvcOrderOperID = value;
                    OnPropertyChanged("cnvcOrderOperID");
                }
            }
        }
        
        [DataMember()]
        public DateTime? cndOrderDate
        {
            get
            {
                return _cndOrderDate;
            }
            set
            {
                if ((value != _cndOrderDate))
                {
                    _cndOrderDate = value;
                    OnPropertyChanged("cndOrderDate");
                }
            }
        }
        
        [DataMember()]
        public DateTime? cndShipDate
        {
            get
            {
                return _cndShipDate;
            }
            set
            {
                if ((value != _cndShipDate))
                {
                    _cndShipDate = value;
                    OnPropertyChanged("cndShipDate");
                }
            }
        }
        
        [DataMember()]
        public String cnvcCustomName
        {
            get
            {
                return _cnvcCustomName;
            }
            set
            {
                if ((value != _cnvcCustomName))
                {
                    _cnvcCustomName = value;
                    OnPropertyChanged("cnvcCustomName");
                }
            }
        }
        
        [DataMember()]
        public String cnvcShipAddress
        {
            get
            {
                return _cnvcShipAddress;
            }
            set
            {
                if ((value != _cnvcShipAddress))
                {
                    _cnvcShipAddress = value;
                    OnPropertyChanged("cnvcShipAddress");
                }
            }
        }
        
        [DataMember()]
        public String cnvcLinkPhone
        {
            get
            {
                return _cnvcLinkPhone;
            }
            set
            {
                if ((value != _cnvcLinkPhone))
                {
                    _cnvcLinkPhone = value;
                    OnPropertyChanged("cnvcLinkPhone");
                }
            }
        }
        
        [DataMember()]
        public DateTime? cndArrivedDate
        {
            get
            {
                return _cndArrivedDate;
            }
            set
            {
                if ((value != _cndArrivedDate))
                {
                    _cndArrivedDate = value;
                    OnPropertyChanged("cndArrivedDate");
                }
            }
        }
        
        [DataMember()]
        public String cnvcOrderState
        {
            get
            {
                return _cnvcOrderState;
            }
            set
            {
                if ((value != _cnvcOrderState))
                {
                    _cnvcOrderState = value;
                    OnPropertyChanged("cnvcOrderState");
                }
            }
        }
    }
}
