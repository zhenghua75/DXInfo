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
    public class tbInventory : Entity
    {
        
        private Boolean _cnbProductBill;
        
        private String _cnvcInvCode;
        
        private String _cnvcInvName;
        
        private String _cnvcInvStd;
        
        private String _cnvcInvCCode;
        
        private Boolean _cnbSale;
        
        private Boolean _cnbPurchase;
        
        private Boolean _cnbSelf;
        
        private Boolean _cnbComsume;
        
        private Decimal? _cniInvCCost;
        
        private Decimal? _cniInvNCost;
        
        private Decimal? _cniSafeNum;
        
        private Decimal? _cniLowSum;
        
        private DateTime? _cndSDate;
        
        private DateTime? _cndEDate;
        
        private String _cnvcCreatePerson;
        
        private String _cnvcModifyPerson;
        
        private DateTime? _cndModifyDate;
        
        private String _cnvcValueType;
        
        private String _cnvcGroupCode;
        
        private String _cnvcComUnitCode;
        
        private String _cnvcSAComUnitCode;
        
        private String _cnvcPUComUnitCode;
        
        private String _cnvcSTComUnitCode;
        
        private String _cnvcProduceUnitCode;
        
        private Decimal? _cnfRetailPrice;
        
        private String _cnvcShopUnitCode;
        
        private String _cnvcFeel;
        
        private String _cnvcOrganise;
        
        private String _cnvcColor;
        
        private String _cnvcTaste;
        
        private Int32 _cnnExpire;
        
        private Int32 _cnnDue;
        
        [DataMember()]
        public Boolean cnbProductBill
        {
            get
            {
                return _cnbProductBill;
            }
            set
            {
                if ((value != _cnbProductBill))
                {
                    _cnbProductBill = value;
                    OnPropertyChanged("cnbProductBill");
                }
            }
        }
        
        [DataMember()]
        public String cnvcInvCode
        {
            get
            {
                return _cnvcInvCode;
            }
            set
            {
                if ((value != _cnvcInvCode))
                {
                    _cnvcInvCode = value;
                    OnPropertyChanged("cnvcInvCode");
                }
            }
        }
        
        [DataMember()]
        public String cnvcInvName
        {
            get
            {
                return _cnvcInvName;
            }
            set
            {
                if ((value != _cnvcInvName))
                {
                    _cnvcInvName = value;
                    OnPropertyChanged("cnvcInvName");
                }
            }
        }
        
        [DataMember()]
        public String cnvcInvStd
        {
            get
            {
                return _cnvcInvStd;
            }
            set
            {
                if ((value != _cnvcInvStd))
                {
                    _cnvcInvStd = value;
                    OnPropertyChanged("cnvcInvStd");
                }
            }
        }
        
        [DataMember()]
        public String cnvcInvCCode
        {
            get
            {
                return _cnvcInvCCode;
            }
            set
            {
                if ((value != _cnvcInvCCode))
                {
                    _cnvcInvCCode = value;
                    OnPropertyChanged("cnvcInvCCode");
                }
            }
        }
        
        [DataMember()]
        public Boolean cnbSale
        {
            get
            {
                return _cnbSale;
            }
            set
            {
                if ((value != _cnbSale))
                {
                    _cnbSale = value;
                    OnPropertyChanged("cnbSale");
                }
            }
        }
        
        [DataMember()]
        public Boolean cnbPurchase
        {
            get
            {
                return _cnbPurchase;
            }
            set
            {
                if ((value != _cnbPurchase))
                {
                    _cnbPurchase = value;
                    OnPropertyChanged("cnbPurchase");
                }
            }
        }
        
        [DataMember()]
        public Boolean cnbSelf
        {
            get
            {
                return _cnbSelf;
            }
            set
            {
                if ((value != _cnbSelf))
                {
                    _cnbSelf = value;
                    OnPropertyChanged("cnbSelf");
                }
            }
        }
        
        [DataMember()]
        public Boolean cnbComsume
        {
            get
            {
                return _cnbComsume;
            }
            set
            {
                if ((value != _cnbComsume))
                {
                    _cnbComsume = value;
                    OnPropertyChanged("cnbComsume");
                }
            }
        }
        
        [DataMember()]
        public Decimal? cniInvCCost
        {
            get
            {
                return _cniInvCCost;
            }
            set
            {
                if ((value != _cniInvCCost))
                {
                    _cniInvCCost = value;
                    OnPropertyChanged("cniInvCCost");
                }
            }
        }
        
        [DataMember()]
        public Decimal? cniInvNCost
        {
            get
            {
                return _cniInvNCost;
            }
            set
            {
                if ((value != _cniInvNCost))
                {
                    _cniInvNCost = value;
                    OnPropertyChanged("cniInvNCost");
                }
            }
        }
        
        [DataMember()]
        public Decimal? cniSafeNum
        {
            get
            {
                return _cniSafeNum;
            }
            set
            {
                if ((value != _cniSafeNum))
                {
                    _cniSafeNum = value;
                    OnPropertyChanged("cniSafeNum");
                }
            }
        }
        
        [DataMember()]
        public Decimal? cniLowSum
        {
            get
            {
                return _cniLowSum;
            }
            set
            {
                if ((value != _cniLowSum))
                {
                    _cniLowSum = value;
                    OnPropertyChanged("cniLowSum");
                }
            }
        }
        
        [DataMember()]
        public DateTime? cndSDate
        {
            get
            {
                return _cndSDate;
            }
            set
            {
                if ((value != _cndSDate))
                {
                    _cndSDate = value;
                    OnPropertyChanged("cndSDate");
                }
            }
        }
        
        [DataMember()]
        public DateTime? cndEDate
        {
            get
            {
                return _cndEDate;
            }
            set
            {
                if ((value != _cndEDate))
                {
                    _cndEDate = value;
                    OnPropertyChanged("cndEDate");
                }
            }
        }
        
        [DataMember()]
        public String cnvcCreatePerson
        {
            get
            {
                return _cnvcCreatePerson;
            }
            set
            {
                if ((value != _cnvcCreatePerson))
                {
                    _cnvcCreatePerson = value;
                    OnPropertyChanged("cnvcCreatePerson");
                }
            }
        }
        
        [DataMember()]
        public String cnvcModifyPerson
        {
            get
            {
                return _cnvcModifyPerson;
            }
            set
            {
                if ((value != _cnvcModifyPerson))
                {
                    _cnvcModifyPerson = value;
                    OnPropertyChanged("cnvcModifyPerson");
                }
            }
        }
        
        [DataMember()]
        public DateTime? cndModifyDate
        {
            get
            {
                return _cndModifyDate;
            }
            set
            {
                if ((value != _cndModifyDate))
                {
                    _cndModifyDate = value;
                    OnPropertyChanged("cndModifyDate");
                }
            }
        }
        
        [DataMember()]
        public String cnvcValueType
        {
            get
            {
                return _cnvcValueType;
            }
            set
            {
                if ((value != _cnvcValueType))
                {
                    _cnvcValueType = value;
                    OnPropertyChanged("cnvcValueType");
                }
            }
        }
        
        [DataMember()]
        public String cnvcGroupCode
        {
            get
            {
                return _cnvcGroupCode;
            }
            set
            {
                if ((value != _cnvcGroupCode))
                {
                    _cnvcGroupCode = value;
                    OnPropertyChanged("cnvcGroupCode");
                }
            }
        }
        
        [DataMember()]
        public String cnvcComUnitCode
        {
            get
            {
                return _cnvcComUnitCode;
            }
            set
            {
                if ((value != _cnvcComUnitCode))
                {
                    _cnvcComUnitCode = value;
                    OnPropertyChanged("cnvcComUnitCode");
                }
            }
        }
        
        [DataMember()]
        public String cnvcSAComUnitCode
        {
            get
            {
                return _cnvcSAComUnitCode;
            }
            set
            {
                if ((value != _cnvcSAComUnitCode))
                {
                    _cnvcSAComUnitCode = value;
                    OnPropertyChanged("cnvcSAComUnitCode");
                }
            }
        }
        
        [DataMember()]
        public String cnvcPUComUnitCode
        {
            get
            {
                return _cnvcPUComUnitCode;
            }
            set
            {
                if ((value != _cnvcPUComUnitCode))
                {
                    _cnvcPUComUnitCode = value;
                    OnPropertyChanged("cnvcPUComUnitCode");
                }
            }
        }
        
        [DataMember()]
        public String cnvcSTComUnitCode
        {
            get
            {
                return _cnvcSTComUnitCode;
            }
            set
            {
                if ((value != _cnvcSTComUnitCode))
                {
                    _cnvcSTComUnitCode = value;
                    OnPropertyChanged("cnvcSTComUnitCode");
                }
            }
        }
        
        [DataMember()]
        public String cnvcProduceUnitCode
        {
            get
            {
                return _cnvcProduceUnitCode;
            }
            set
            {
                if ((value != _cnvcProduceUnitCode))
                {
                    _cnvcProduceUnitCode = value;
                    OnPropertyChanged("cnvcProduceUnitCode");
                }
            }
        }
        
        [DataMember()]
        public Decimal? cnfRetailPrice
        {
            get
            {
                return _cnfRetailPrice;
            }
            set
            {
                if ((value != _cnfRetailPrice))
                {
                    _cnfRetailPrice = value;
                    OnPropertyChanged("cnfRetailPrice");
                }
            }
        }
        
        [DataMember()]
        public String cnvcShopUnitCode
        {
            get
            {
                return _cnvcShopUnitCode;
            }
            set
            {
                if ((value != _cnvcShopUnitCode))
                {
                    _cnvcShopUnitCode = value;
                    OnPropertyChanged("cnvcShopUnitCode");
                }
            }
        }
        
        [DataMember()]
        public String cnvcFeel
        {
            get
            {
                return _cnvcFeel;
            }
            set
            {
                if ((value != _cnvcFeel))
                {
                    _cnvcFeel = value;
                    OnPropertyChanged("cnvcFeel");
                }
            }
        }
        
        [DataMember()]
        public String cnvcOrganise
        {
            get
            {
                return _cnvcOrganise;
            }
            set
            {
                if ((value != _cnvcOrganise))
                {
                    _cnvcOrganise = value;
                    OnPropertyChanged("cnvcOrganise");
                }
            }
        }
        
        [DataMember()]
        public String cnvcColor
        {
            get
            {
                return _cnvcColor;
            }
            set
            {
                if ((value != _cnvcColor))
                {
                    _cnvcColor = value;
                    OnPropertyChanged("cnvcColor");
                }
            }
        }
        
        [DataMember()]
        public String cnvcTaste
        {
            get
            {
                return _cnvcTaste;
            }
            set
            {
                if ((value != _cnvcTaste))
                {
                    _cnvcTaste = value;
                    OnPropertyChanged("cnvcTaste");
                }
            }
        }
        
        [DataMember()]
        public Int32 cnnExpire
        {
            get
            {
                return _cnnExpire;
            }
            set
            {
                if ((value != _cnnExpire))
                {
                    _cnnExpire = value;
                    OnPropertyChanged("cnnExpire");
                }
            }
        }
        
        [DataMember()]
        public Int32 cnnDue
        {
            get
            {
                return _cnnDue;
            }
            set
            {
                if ((value != _cnnDue))
                {
                    _cnnDue = value;
                    OnPropertyChanged("cnnDue");
                }
            }
        }
    }
}