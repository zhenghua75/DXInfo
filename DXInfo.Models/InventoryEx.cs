using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace DXInfo.Models
{
    [NotMapped]
    public class InventoryEx : Inventory
    {
        private Guid _OrderId;
        public Guid OrderId
        {
            get { return _OrderId; }
            set
            {
                _OrderId = value;
                OnPropertyChanged("OrderId");
            }
        }

        private Guid _OrderMenuId;
        public Guid OrderMenuId
        {
            get { return _OrderMenuId; }
            set
            {
                _OrderMenuId = value;
                OnPropertyChanged("OrderMenuId");
            }
        }

        private Guid? _PackageId;
        public Guid? PackageId
        {
            get { return _PackageId; }
            set
            {
                _PackageId = value;
                OnPropertyChanged("PackageId");
            }
        }

        private bool _IsDiscount;
        public bool IsDiscount
        {
            get { return _IsDiscount; }
            set
            {
                _IsDiscount = value;
                OnPropertyChanged("IsDiscount");
            }
        }

        private int _Discount;
        public int Discount
        {
            get { return _Discount; }
            set
            {
                _Discount = value;
                OnPropertyChanged("Discount");
                if (SalePrice > 0)
                {
                    _AgreementPrice = Math.Round(SalePrice * DiscountRate, 2);
                    OnPropertyChanged("AgreementPrice");
                }
            }
        }

        public decimal DiscountRate
        {
            get { return Convert.ToDecimal(_Discount) / 100; }
        }

        private bool _IsCupType;
        public bool IsCupType
        {
            get { return _IsCupType; }
            set
            {
                _IsCupType = value;
                OnPropertyChanged("IsCupType");
            }
        }

        private bool _IsInvPrice;
        public bool IsInvPrice
        {
            get { return _IsInvPrice; }
            set
            {
                _IsInvPrice = value;
                OnPropertyChanged("IsInvPrice");
            }
        }

        private bool _IsInvDynamicPrice;
        public bool IsInvDynamicPrice
        {
            get { return _IsInvDynamicPrice; }
            set
            {
                _IsInvDynamicPrice = value;
                OnPropertyChanged("IsInvDynamicPrice");
            }
        }

        private decimal _AgreementPrice;
        public decimal AgreementPrice
        {
            get { return _AgreementPrice; }
            set
            {
                _AgreementPrice = Math.Round(value,2);
                OnPropertyChanged("AgreementPrice");
                if (SalePrice > 0)
                {
                    _Discount = Convert.ToInt32(_AgreementPrice / SalePrice * 100);
                    OnPropertyChanged("Discount");
                }
            }
        }

        private MyEnum _CupType;
        public MyEnum CupType
        {
            get { return _CupType; }
            set
            {

                _CupType = value;
                OnPropertyChanged("CupType");
                OnPropertyChanged("CurrentAmount");
                OnPropertyChanged("CurrentSalePrice");
                OnPropertyChanged("CurrentSalePoint");
            }
        }

        private DXInfo.Models.InvPrice _InvPrice;
        public DXInfo.Models.InvPrice InvPrice
        {
            get { return _InvPrice; }
            set
            {

                _InvPrice = value;
                OnPropertyChanged("InvPrice");
                OnPropertyChanged("CurrentAmount");
                OnPropertyChanged("CurrentSalePrice");
                OnPropertyChanged("CurrentSalePoint");
            }
        }


        private DXInfo.Models.TasteExList _lTasteEx;
        public DXInfo.Models.TasteExList lTasteEx
        {
            get { return _lTasteEx; }
            set
            {
                _lTasteEx = value;
                OnPropertyChanged("lTasteEx");
            }
        }

        private Decimal _Quantity;
        public Decimal Quantity
        {
            get
            {
                return _Quantity;
            }
            set
            {
                _Quantity = value;
                OnPropertyChanged("Quantity");
                OnPropertyChanged("CurrentAmount");
                OnPropertyChanged("Amount");
            }
        }

        public decimal CurrentSalePrice
        {
            get
            {
                if (_IsCupType)
                {
                    if (_dSalePrice == null || _CupType == null) return 0;
                    if (_dSalePrice.Where(w => w.Key == _CupType.Id).Count() == 0) return 0;
                    return _dSalePrice[_CupType.Id];
                }
                if (_IsInvDynamicPrice)
                {
                    return _AgreementPrice;
                }
                return SalePrice;
            }
        }
        
        public decimal CurrentSalePoint
        {
            get
            {
                if (_IsCupType)
                {
                    if (_dSalePoint == null || _CupType == null) return 0;
                    if (_dSalePoint.Where(w => w.Key == _CupType.Id).Count() == 0) return 0;
                    return _dSalePoint[_CupType.Id];
                }
                return SalePoint;
            }
        }
        public decimal CurrentAmount
        {
            get
            {
                if (_IsCupType)
                {
                    if (_dSalePrice == null || _CupType == null) return 0;
                    if (_dSalePrice.Where(w => w.Key == _CupType.Id).Count() == 0) return 0;
                    return _Quantity * _dSalePrice[_CupType.Id];
                }
                if (_IsInvDynamicPrice)
                {
                    return _Quantity * _AgreementPrice;
                }
                return _Quantity * SalePrice;
            }
        }
        
        public decimal Amount
        {
            get
            {
                if (_IsCupType)
                {
                    if (_dSalePrice == null || _CupType == null) return 0;
                    if (_dSalePrice.Where(w => w.Key == _CupType.Id).Count() == 0) return 0;
                    return _Quantity * _dSalePrice[_CupType.Id];
                }
                return Quantity * SalePrice;
            }
        }

        public decimal CurrentPoint
        {
            get
            {
                if (_IsCupType)
                {
                    if (_dSalePoint == null || _CupType == null) return 0;
                    if (_dSalePoint.Where(w => w.Key == _CupType.Id).Count() == 0) return 0;
                    return _Quantity * _dSalePoint[_CupType.Id];
                }
                return _Quantity * SalePoint;
            }
        }

        public decimal Point
        {
            get
            {
                return Quantity * SalePoint;
            }
        }

        private Dictionary<int, decimal> _dSalePrice;
        public Dictionary<int, decimal> dSalePrice
        {
            get
            {
                return _dSalePrice;
            }
            set
            {
                _dSalePrice = value;
            }
        }

        private Dictionary<int, decimal> _dSalePoint;
        public Dictionary<int, decimal> dSalePoint
        {
            get
            {
                return _dSalePoint;
            }
            set
            {
                _dSalePoint = value;
            }
        }

        private List<MyEnum> _lCupType;
        public List<MyEnum> lCupType
        {
            get { return _lCupType; }
            set
            {
                _lCupType = value;
                OnPropertyChanged("lCupType");
            }
        }

        private List<DXInfo.Models.InvPrice> _lInvPrice;
        public List<DXInfo.Models.InvPrice> lInvPrice
        {
            get { return _lInvPrice; }
            set
            {
                _lInvPrice = value;
                OnPropertyChanged("lInvPrice");
            }
        }

        private int _WaitMinutes;
        public int WaitMinutes
        {
            get { return _WaitMinutes; }
            set
            {
                _WaitMinutes = value;
                OnPropertyChanged("WaitMinutes");
            }
        }

        private Guid _UserId;
        public Guid UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if ((value != _UserId))
                {
                    _UserId = value;
                    OnPropertyChanged("UserId");
                }
            }
        }

        private Int32 _Status;
        public Int32 Status
        {
            get
            {
                return _Status;
            }
            set
            {
                if ((value != _Status))
                {
                    _Status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                OnPropertyChanged("UserName");
            }
        }

        private Int32 _PackageSn;
        public Int32 PackageSn
        {
            get
            {
                return _PackageSn;
            }
            set
            {
                if ((value != _PackageSn))
                {
                    _PackageSn = value;
                    OnPropertyChanged("PackageSn");
                }
            }
        }

        private DateTime? _MenuCreateDate;
        public DateTime? MenuCreateDate
        {
            get
            {
                return _MenuCreateDate;
            }
            set
            {
                if ((value != _MenuCreateDate))
                {
                    _MenuCreateDate = value;
                    OnPropertyChanged("MenuCreateDate");
                }
            }
        }

        private Decimal _MenuQuantity;
        public Decimal MenuQuantity
        {
            get
            {
                return _MenuQuantity;
            }
            set
            {
                if ((value != _MenuQuantity))
                {
                    _MenuQuantity = value;
                    OnPropertyChanged("MenuQuantity");
                }
            }
        }

        private Decimal _MissQuantity;
        public Decimal MissQuantity
        {
            get
            {
                return _MissQuantity;
            }
            set
            {
                if ((value != _MissQuantity))
                {
                    _MissQuantity = value;
                    OnPropertyChanged("MissQuantity");
                }
            }
        }

        private DateTime _CreateDate;
        public DateTime CreateDate
        {
            get
            {
                return _CreateDate;
            }
            set
            {
                if ((value != _CreateDate))
                {
                    _CreateDate = value;
                    OnPropertyChanged("CreateDate");
                }
            }
        }
    }
}
