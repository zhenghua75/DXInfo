using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using GalaSoft.MvvmLight.Messaging;
using FairiesCoolerCash.Business;
using System.Windows.Data;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using AutoMapper;
using System.Windows.Threading;
using System.Threading;
using System.Data.SqlClient;
using Microsoft.Reporting.WinForms;
using System.Data;
using System.Windows.Controls;
using System.Net;
using System.Configuration;

namespace FairiesCoolerCash.ViewModel
{
    
    public class ConsumeViewModelBase : BusinessViewModelBase
    {
        #region 属性
        /// <summary>
        /// 是否关联库存
        /// </summary>
        public bool IsStock { get; set; }
        public DXInfo.Models.CategoryType CurrentCategoryType { get; set; }
        public DXInfo.Models.InvType CurrentInvType { get; set; }
        public DXInfo.Models.DeptType DeptType { get; set; }
        
        //public List<DXInfo.Models.CurrentStock> lCurrentStock { get; set; }
        public Visibility CancelCheckOutVisibility { get; set; }
        public bool CancelCheckOutColumnVisibility { get; set; }

        public Visibility VoucherVisibility { get; set; }
        public bool VoucherColumnVisibility { get; set; }

        public Visibility PayTypeVisibility { get; set; }
        public bool PayTypeColumnVisibility { get; set; }

        public Visibility CardVisibility { get; set; }
        public bool CardColumnVisibility { get; set; }

        public bool CardPayTypeColumnVisibility { get; set; }
        #endregion

        #region 构造
        public ConsumeViewModelBase(IFairiesMemberManageUow uow)
            : base(uow, new List<string>())
        {
            Messenger.Default.Register<DataGridMessageToken>(this, Handle_DataGridMessageToken);
            
            bool barcode = BusinessCommon.BarcodeVisibility();
            this.BarcodeColumnVisibility = barcode;
            this.BarcodeVisibility = barcode ? Visibility.Visible : Visibility.Collapsed;

            bool ret = BusinessCommon.SearchCardVisibility();
            System.Windows.Visibility visi = ret ? Visibility.Visible : Visibility.Collapsed;
            this.SearchCardVisibility = visi;
            this.SearchCardColumnVisibility = ret;            

            this.IsInvPrice = BusinessCommon.IsInvPrice();

            this.CancelCheckOutColumnVisibility = BusinessCommon.CancelCheckOutColumnVisibility();
            if (this.CancelCheckOutColumnVisibility)
            {
                this.CancelCheckOutVisibility = Visibility.Visible;
            }
            else
            {
                this.CancelCheckOutVisibility = Visibility.Collapsed;
            }
            this.SetCardLevelAuto();

            this.VoucherColumnVisibility = BusinessCommon.VoucherVisibility();
            if (this.VoucherColumnVisibility)
            {
                this.VoucherVisibility = Visibility.Visible;
            }
            else
            {
                this.VoucherVisibility = Visibility.Collapsed;
            }

            this.PayTypeColumnVisibility = BusinessCommon.PayTypeVisibility();
            if (this.PayTypeColumnVisibility)
            {
                this.PayTypeVisibility = Visibility.Visible;
            }
            else
            {
                this.PayTypeVisibility = Visibility.Collapsed;
            }

            this.CardColumnVisibility = BusinessCommon.CardVisibility();
            if (this.CardColumnVisibility)
            {
                this.CardVisibility = Visibility.Visible;
            }
            else
            {
                this.CardVisibility = Visibility.Collapsed;
            }
            this.CardPayTypeColumnVisibility = this.CardColumnVisibility && this.PayTypeColumnVisibility;
            this.IsInvDynamicPrice = BusinessCommon.IsInvDynamicPrice();
        }        
        private void Handle_DataGridMessageToken(DataGridMessageToken token)
        {
            this.MyDataGrid = token.MyDataGrid;
        }
        public virtual void SetCurrentType() { }
        public override void LoadData()
        {
            this.SetCurrentType();
            this.SetOCInventoryCategory(); 
            this.SetOCInventory();                       
            SetOCInventoryEx();
            this.SetlTasteEx();
            this.SetlCupType();
            this.SetlPayType();
            this.SelectedPayType = this.lPayType.Find(f => f.Name == "现金");
            this.SetlCardLevel();
            this.SetlCardType();
            this.IsCard = false;
            this.IsMoney = false;

            this.SelectedPayType = this.lPayType.Where(w=>w.Name=="现金").FirstOrDefault();
        }
        
        #endregion

        #region 存货与分类
        private void SetOCInventoryCategory()
        {
            IEnumerable<DXInfo.Models.InventoryCategory> iInventoryCategory = Uow.Db.SqlQuery<DXInfo.Models.InventoryCategory>("sp_DXInfo_GetDeptInventoryCategory @DeptId,@CategoryType",
                new SqlParameter("DeptId", this.Dept.DeptId),
                new SqlParameter("CategoryType", (int)this.CurrentCategoryType));
            List<DXInfo.Models.InventoryCategory> lInventoryCategory = iInventoryCategory.ToList();
            this.OCInventoryCategory = new ObservableCollection<DXInfo.Models.InventoryCategory>(lInventoryCategory);
        }
        protected override void AfterSelectInventoryCategory()
        {
            base.AfterSelectInventoryCategory();
            if (this.SelectedInventoryCategory != null)
            {
                this.SetOCInventory();
            }
        }
        private void SetOCInventory()
        {
            List<DXInfo.Models.Inventory> lInventory;

            if (this.SelectedInventoryCategory != null)
            {
                IEnumerable<DXInfo.Models.Inventory> iInventory = Uow.Db.SqlQuery<DXInfo.Models.Inventory>("sp_DXInfo_GetDeptInventoryByCategory @DeptId,@InvType,@IsDeptPrice,@Category",
                new SqlParameter("DeptId", this.Dept.DeptId),
                new SqlParameter("InvType", (int)CurrentInvType),
                new SqlParameter("IsDeptPrice", this.Dept.IsDeptPrice),
                new SqlParameter("Category", this.SelectedInventoryCategory.Id));
                lInventory = iInventory.ToList();
            }
            else
            {
                IEnumerable<DXInfo.Models.Inventory> iInventory = Uow.Db.SqlQuery<DXInfo.Models.Inventory>("sp_DXInfo_GetDeptInventory @DeptId,@InvType,@IsDeptPrice",
                new SqlParameter("DeptId", this.Dept.DeptId),
                new SqlParameter("InvType", (int)CurrentInvType),
                new SqlParameter("IsDeptPrice", this.Dept.IsDeptPrice));
                lInventory = iInventory.ToList();
            }

            if (this.OCInventory != null)
            {
                this.OCInventory.Clear();
            }
            this.OCInventory = new ObservableCollection<DXInfo.Models.Inventory>(lInventory);
        }
        public void SetOCInventoryEx()
        {
            if (this.OCInventoryEx == null)
            {
                this.OCInventoryEx = new ObservableCollection<DXInfo.Models.InventoryEx>();
                this.OCInventoryEx.CollectionChanged += new NotifyCollectionChangedEventHandler(OCInventoryEx_CollectionChanged);
            }
        }
        private void SetSumData()
        {
            if (OCInventoryEx != null)
            {
                this.SumQuantity = OCInventoryEx.Sum(s => s.Quantity);
                switch (this.DeptType)
                {
                    case DXInfo.Models.DeptType.Sale:
                        this.SumAmount = OCInventoryEx.Sum(s => s.CurrentAmount);
                        break;
                    case DXInfo.Models.DeptType.Shop:
                        this.SumAmount = OCInventoryEx.Where(w =>
                            w.Status == (int)DXInfo.Models.OrderMenuStatus.Order ||
                            w.Status == (int)DXInfo.Models.OrderMenuStatus.Hurry ||
                            w.Status == (int)DXInfo.Models.OrderMenuStatus.Make ||
                            w.Status == (int)DXInfo.Models.OrderMenuStatus.Out).Sum(s => s.Amount);
                        break;
                }
            }
        }
        void OCInventoryEx_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {

            SetSumData();
            if (e.NewItems != null)
                foreach (DXInfo.Models.InventoryEx item in e.NewItems)
                    item.PropertyChanged += MyType_PropertyChanged;

            if (e.OldItems != null)
                foreach (DXInfo.Models.InventoryEx item in e.OldItems)
                    item.PropertyChanged -= MyType_PropertyChanged;
        }
        void MyType_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Quantity" || e.PropertyName == "CurrentSalePrice")
            {
                SetSumData();
            }
        }
        protected virtual void CreateInventoryEx(DXInfo.Models.Inventory inv) { }
        #endregion

        #region 添加库存
        protected void AddRetailOutStock()
        {
            //提交零售出库单
            List<DXInfo.Models.InventoryEx> lInvEx = this.OCInventoryEx.Where(w => w.WhId != null).ToList();
            List<Guid> lWhId = (from d in lInvEx
                                select d.WhId.Value).Distinct().ToList();
            foreach (Guid whId in lWhId)
            {
                DXInfo.Models.ClientRetailOutStock retail = new DXInfo.Models.ClientRetailOutStock();
                retail.DeptId = this.Dept.DeptId;
                retail.UserId = this.User.UserId;
                retail.WhId = whId;
                var q = (from d in lInvEx
                         where d.WhId == whId
                         select d).ToList();
                List<DXInfo.Models.ClientRetailOutStockDetail> lDetail = new List<DXInfo.Models.ClientRetailOutStockDetail>();
                foreach (DXInfo.Models.InventoryEx iex in q)
                {
                    DXInfo.Models.ClientRetailOutStockDetail detail = new DXInfo.Models.ClientRetailOutStockDetail();
                    detail.InvId = iex.Id;
                    if (iex.InvPrice != null)
                    {
                        detail.Batch = iex.InvPrice.Code;
                    }
                    detail.Num = iex.Quantity;
                    switch (this.DeptType)
                    {
                        case DXInfo.Models.DeptType.Sale:
                            detail.Price = iex.CurrentSalePrice;
                            break;
                        case DXInfo.Models.DeptType.Shop:
                            detail.Price = iex.SalePrice;
                            break;
                    }
                    
                    lDetail.Add(detail);
                }
                if (lDetail.Count > 0)
                {
                    retail.Detail = lDetail;
                    //提交
                    try
                    {
                        string stockVouchLocalCode = ClientCommon.StockVouchLocalCode();
                        DXInfo.Business.StockManageFacade myBusiness = new DXInfo.Business.StockManageFacade(Uow, this.Oper.UserId, this.Dept.DeptId, this.Dept.OrganizationId,this.Dept.DeptCode,this.User.UserName);
                        myBusiness.AddRetailOutStock(retail, stockVouchLocalCode);
                    }
                    catch (WebException we)
                    {
                        Helper.ShowErrorMsg(we.Message);
                    }
                }
            }
        }
        #endregion

        #region 查卡
        private System.Windows.Visibility _SearchCardVisibility;
        public System.Windows.Visibility SearchCardVisibility
        {
            get
            {
                return _SearchCardVisibility;
            }
            set
            {
                _SearchCardVisibility = value;
                this.RaisePropertyChanged("SearchCardVisibility");
            }
        }
        private bool _SearchCardColumnVisibility;
        public bool SearchCardColumnVisibility
        {
            get
            {
                return _SearchCardColumnVisibility;
            }
            set
            {
                _SearchCardColumnVisibility = value;
                this.RaisePropertyChanged("SearchCardColumnVisibility");
            }
        }
        #endregion

        #region 当前库存
        //private IQueryable<DXInfo.Models.CurrentStock> GetCurrentStock(DXInfo.Models.InventoryEx inventoryEx)
        //{
        //    var q = from d in Uow.CurrentStock.GetAll()
        //            join d1 in Uow.Warehouse.GetAll() on d.WhId equals d1.Id into dd1
        //            from dd1s in dd1.DefaultIfEmpty()
        //            where d.InvId == inventoryEx.Id &&
        //            dd1s.Dept == this.Dept.DeptId &&
        //            !d.StopFlag &&
        //            d.Num > 0
        //            select d;
        //    return q;
        //}
        //protected void SetCurrentStock(DXInfo.Models.InventoryEx inventoryEx)
        //{
        //    if (this.IsStock)
        //    {
        //        DXInfo.Models.CurrentStock currentStock;
        //        IQueryable<DXInfo.Models.CurrentStock> q = GetCurrentStock(inventoryEx);
        //        if (inventoryEx.InvPrice == null)
        //        {
        //            currentStock = (from d in q
        //                            orderby d.Num, d.Batch
        //                            where d.Batch == null
        //                            select d).FirstOrDefault();
        //        }
        //        else
        //        {
        //            currentStock = (from d in q
        //                            orderby d.Num
        //                            where d.Batch == inventoryEx.InvPrice.Code
        //                            select d).FirstOrDefault();
        //        }
        //        if (currentStock != null)
        //        {
        //            inventoryEx.CurrentStock = currentStock;
        //        }
        //    }
        //}
        #endregion

        #region 单价
        private bool _IsInvPrice;
        public bool IsInvPrice
        {
            get
            {
                return _IsInvPrice;
            }
            set
            {
                _IsInvPrice = value;
                this.RaisePropertyChanged("IsInvPrice");
            }
        }
        public void SetInvPrice(DXInfo.Models.InventoryEx inventoryEx)
        {
            if (this.IsInvPrice)
            {
                if (inventoryEx.OrderMenuId != null && inventoryEx.OrderMenuId != Guid.Empty)
                {
                    var orderInvPrice = Uow.OrderInvPrice.GetAll()
                        .Where(w => w.OrderMenuId == inventoryEx.OrderMenuId)
                        .FirstOrDefault();
                    if (orderInvPrice != null)
                    {
                        inventoryEx.InvPrice = Mapper.Map<DXInfo.Models.InvPrice>(orderInvPrice);
                        inventoryEx.InvPrice.Id = orderInvPrice.InvPriceId;
                        inventoryEx.SalePrice = inventoryEx.InvPrice.SalePrice;
                        inventoryEx.SalePoint = inventoryEx.InvPrice.SalePoint;
                        inventoryEx.AgreementPrice = inventoryEx.InvPrice.SalePrice;
                    }
                }
                else
                {
                    List<DXInfo.Models.InvPrice> lInvPrice =
                        (from d in Uow.InvPrice.GetAll()
                         where d.InvId == inventoryEx.Id && !d.IsInvalid
                         orderby d.Code
                         select d)
                        .ToList();
                    if (lInvPrice.Count > 0)
                    {
                        inventoryEx.lInvPrice = lInvPrice; ;
                        InvPriceSetWindow csw = new InvPriceSetWindow(Uow, inventoryEx);
                        if (csw.ShowDialog().GetValueOrDefault())
                        {
                            if (inventoryEx.InvPrice != null)
                            {
                                //if (this.DeptType == DXInfo.Models.DeptType.Shop)
                                //{
                                inventoryEx.SalePrice = inventoryEx.InvPrice.SalePrice;
                                inventoryEx.SalePoint = inventoryEx.InvPrice.SalePoint;
                                //}
                                inventoryEx.AgreementPrice = inventoryEx.InvPrice.SalePrice;
                            }
                        }
                    }
                }
                if (inventoryEx.InvPrice != null)
                {
                    inventoryEx.Name += "(" + inventoryEx.InvPrice.Name + ")";
                }
            }
        }

        private bool _IsInvDynamicPrice;
        public bool IsInvDynamicPrice
        {
            get
            {
                return _IsInvDynamicPrice;
            }
            set
            {
                _IsInvDynamicPrice = value;
                this.RaisePropertyChanged("IsInvDynamicPrice");
            }
        }
        public void SetInvDynamicPrice(DXInfo.Models.InventoryEx inventoryEx)
        {
            if (this.IsInvDynamicPrice)
            {
                if (this.DeptType == DXInfo.Models.DeptType.Sale)
                {
                    InvDynamicPriceWindow idpw = new InvDynamicPriceWindow(Uow, inventoryEx);
                    idpw.ShowDialog();
                    if (idpw.DialogResult.HasValue && idpw.DialogResult.Value)
                    {
                    }
                    else
                    {
                    }
                }
            }
        }        
        #endregion

        #region 杯型
        private bool _IsCupType;
        public bool IsCupType
        {
            get
            {
                return _IsCupType;
            }
            set
            {
                _IsCupType = value;
                this.RaisePropertyChanged("IsCupType");
            }
        }
        public void SetCupType(DXInfo.Models.InventoryEx inventoryEx)
        {
            if (this.IsCupType)
            {
                inventoryEx.dSalePrice = this.GetdSalePrice(this.SelectedInventory);
                inventoryEx.dSalePoint = this.GetdSalePoint(this.SelectedInventory);
                inventoryEx.lCupType = this.lCupType;
                inventoryEx.CupType = inventoryEx.lCupType.Find(f => f.Id == (int)DXInfo.Models.CupType.Standard);
                CardConsumeSetWindow csw = new CardConsumeSetWindow(Uow, inventoryEx);
                csw.ShowDialog();
            }
        }
        #endregion

        #region 条码
        private System.Windows.Visibility _BarcodeVisibility;
        public System.Windows.Visibility BarcodeVisibility
        {
            get
            {
                return _BarcodeVisibility;
            }
            set
            {
                _BarcodeVisibility = value;
                this.RaisePropertyChanged("BarcodeVisibility");
            }
        }
        private bool _BarcodeColumnVisibility;
        public bool BarcodeColumnVisibility
        {
            get
            {
                return _BarcodeColumnVisibility;
            }
            set
            {
                _BarcodeColumnVisibility = value;
                this.RaisePropertyChanged("BarcodeColumnVisibility");
            }
        }
        private string _Barcode;
        public string Barcode
        {
            get
            {
                return _Barcode;
            }
            set
            {
                _Barcode = value;
                this.RaisePropertyChanged("Barcode");
            }
        }
        public ICommand BarcodeCmd
        {
            get
            {
                return new RelayCommand<object>(BarcodeExecute);
            }
        }
        private void BarcodeExecute(object sender)
        {
            IEnumerable<DXInfo.Models.Inventory> iInventory = Uow.Db.SqlQuery<DXInfo.Models.Inventory>("sp_DXInfo_GetDeptInventory @DeptId,@InvType,@IsDeptPrice",
                new SqlParameter("DeptId", this.Dept.DeptId),
                new SqlParameter("InvType", (int)CurrentInvType),
                new SqlParameter("IsDeptPrice", this.Dept.IsDeptPrice));
            var invs = iInventory.Where(w => w.Code == this.Barcode).ToList();
            if (invs.Count == 1)
            {
                this.SelectedInventory = invs[0];    
                this.Barcode = null;
            }
        }
        #endregion

        #region 结账流水
        private string _Sn;
        public string Sn
        {
            get
            {
                return _Sn;
            }
            set
            {
                _Sn = value;
                this.RaisePropertyChanged("Sn");
            }
        }
        public bool IsCancelCheckOut { get; set; }        
        public DXInfo.Models.Consume CancelConsume { get; set; }
        public List<DXInfo.Models.ConsumeList> lCancelConsumeList { get; set; }
        private void ConsumeSnCmdExecute(object sender)
        {
            this.ResetSwipingCard();
            this.ResetCheckOut();

            CancelConsume = Uow.Consume.GetAll().Where(w => w.Sn == this.Sn).FirstOrDefault();
            if (CancelConsume != null)
            {
                if (!CancelConsume.IsValid)
                {
                    if (MessageBox.Show(this.Sn + "已撤销，是否按原单重新结账？", "结账撤销提示", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                    {
                        return;
                    }                  
                    this.IsCancelCheckOut = false;
                }
                else
                {
                    this.IsCancelCheckOut = true;
                }
                this.IsValid = CancelConsume.IsValid;
                lCancelConsumeList = Uow.ConsumeList.GetAll().Where(w => w.Consume == CancelConsume.Id).ToList();
                if (lCancelConsumeList.Count > 0)
                {
                    
                    if (CancelConsume.Card.HasValue)
                    {                        
                        Guid PayTypeCard = Guid.Parse(DXInfo.Business.Helper.PayType_Card);
                        if (CancelConsume.PayType.HasValue && CancelConsume.PayType.Value == PayTypeCard)
                        {
                            //this.ResetSwipingCard();
                            //this.ResetCheckOut();
                            this.swipingCard();
                        }
                        else
                        {
                            this.SearchCardById(CancelConsume.Card.Value);
                        }
                        if (this.Card == null)
                        {
                            MessageBox.Show("请放置卡");
                            return;
                        }
                    }
                    
                    
                    if (CancelConsume.PayType.HasValue)
                    {
                        if (this.lPayTypeCard != null && this.IsCard && this.IsMoney)
                        {
                            this.SelectedPayType = this.lPayTypeCard.Find(f => f.Id == CancelConsume.PayType.Value);
                        }

                        if (this.lPayTypeAll != null && this.IsCard && !this.IsMoney)
                        {
                            this.SelectedPayType = this.lPayTypeAll.Find(f => f.Id == CancelConsume.PayType.Value);
                        }

                        if (this.lPayType != null && !this.IsCard && !this.IsMoney)
                        {
                            this.SelectedPayType = this.lPayType.Find(f => f.Id == CancelConsume.PayType.Value);
                        }
                    }
                    //if (this.OCInventoryEx != null && this.OCInventoryEx.Count>0)
                    //{
                    //    this.OCInventory.Clear();
                    //}
                    //if (this.SelectedInventory != null)
                    //{
                    //    this.SelectedInventory = null;
                    //}
                    
                    foreach (DXInfo.Models.ConsumeList consumeList in lCancelConsumeList)
                    {
                        DXInfo.Models.Inventory inv = Uow.Inventory.GetById(g => g.Id == consumeList.Inventory);
                        //this.CreateInventoryEx(inv);
                        DXInfo.Models.InventoryEx inventoryEx = Mapper.Map<DXInfo.Models.InventoryEx>(inv);
                        inventoryEx.IsCupType = this.IsCupType;
                        inventoryEx.IsInvPrice = this.IsInvPrice;
                        inventoryEx.lTasteEx = this.lTasteEx.Clone() as DXInfo.Models.TasteExList;
                        inventoryEx.Quantity = 1;
                        inventoryEx.IsDiscount = JudgeIsDiscount(inv.Id);

                        //this.SetCupType(inventoryEx);
                        inventoryEx.dSalePrice = this.GetdSalePrice(inv);
                        inventoryEx.dSalePoint = this.GetdSalePoint(inv);
                        inventoryEx.lCupType = this.lCupType;
                        inventoryEx.CupType = inventoryEx.lCupType.Find(f => f.Id == consumeList.Cup);

                        List<DXInfo.Models.ConsumeTastes> lTastes = Uow.ConsumeTastes.GetAll().Where(w => w.ConsumeList == consumeList.Id).ToList();
                        foreach (DXInfo.Models.TasteEx tasteEx in inventoryEx.lTasteEx)
                        {
                            tasteEx.IsSelected = lTastes.Exists(delegate(DXInfo.Models.ConsumeTastes taste) { return tasteEx.Id == taste.Taste; });
                        }
                        //CardConsumeSetWindow csw = new CardConsumeSetWindow(Uow, inventoryEx);
                        //csw.ShowDialog();

                        //this.SetInvPrice(inventoryEx);
                        //this.SetCurrentStock(inventoryEx);
                        if (this.IsInvPrice)
                        {
                            //if (inventoryEx.OrderMenuId != null && inventoryEx.OrderMenuId != Guid.Empty)
                            //{
                            //    var orderInvPrice = Uow.OrderInvPrice.GetAll()
                            //        .Where(w => w.OrderMenuId == inventoryEx.OrderMenuId)
                            //        .FirstOrDefault();
                            //    if (orderInvPrice != null)
                            //    {
                            //        inventoryEx.InvPrice = Mapper.Map<DXInfo.Models.InvPrice>(orderInvPrice);
                            //        inventoryEx.InvPrice.Id = orderInvPrice.InvPriceId;
                            //    }
                            //}
                            //else
                            //{
                            List<DXInfo.Models.InvPrice> lInvPrice =
                                (from d in Uow.InvPrice.GetAll()
                                 where d.InvId == inventoryEx.Id && !d.IsInvalid
                                 orderby d.Code
                                 select d)
                                .ToList();
                            if (lInvPrice.Count > 0)
                            {
                                inventoryEx.lInvPrice = lInvPrice; ;
                                //InvPriceSetWindow csw = new InvPriceSetWindow(Uow, inventoryEx);
                                //if (csw.ShowDialog().GetValueOrDefault())
                                //{
                                DXInfo.Models.ConsumeInvPrice invPrice = Uow.ConsumeInvPrice.GetAll().Where(w => w.ConsumeListId == consumeList.Id).FirstOrDefault();
                                if (invPrice != null)
                                {
                                    inventoryEx.InvPrice = lInvPrice.Find(f => f.Id == invPrice.InvPriceId);
                                    if (inventoryEx.InvPrice != null)
                                    {
                                        if (this.DeptType == DXInfo.Models.DeptType.Shop)
                                        {
                                            inventoryEx.SalePrice = inventoryEx.InvPrice.SalePrice;
                                            inventoryEx.SalePoint = inventoryEx.InvPrice.SalePoint;
                                        }
                                    }
                                }
                                //}
                            }
                            //}
                            if (inventoryEx.InvPrice != null)
                            {
                                inventoryEx.Name += "(" + inventoryEx.InvPrice.Name + ")";
                            }
                        }

                        this.SetOCInventoryEx();
                        this.OCInventoryEx.Add(inventoryEx);
                    }
                }
            }
            this.Sn = null;
        }
        public ICommand ConsumeSnCmd
        {
            get
            {
                return new RelayCommand<object>(ConsumeSnCmdExecute);
            }
        }
        #endregion

        #region 是否授权结账
        private bool IsAuthorize()
        {
            bool isauthorize = false;
            if (this.User.UserName == "admin")
            {
                isauthorize = true;
            }
            else
            {
                DXInfo.Principal.MyPrincipal myPricipal = Thread.CurrentPrincipal as DXInfo.Principal.MyPrincipal;
                if (myPricipal != null)
                {
                    isauthorize = myPricipal.IsInRole("DeskManage-btnCheckout");
                }
            }
            return isauthorize;
        }
        private bool IsAuthorizeCancel()
        {
            bool isauthorize = false;
            if (this.User.UserName == "admin")
            {
                isauthorize = true;
            }
            else
            {
                DXInfo.Principal.MyPrincipal myPricipal = Thread.CurrentPrincipal as DXInfo.Principal.MyPrincipal;
                if (myPricipal != null)
                {
                    isauthorize = myPricipal.IsInRole("CardConsume-CancelCheckout");
                }
            }
            return isauthorize;
        }
        #endregion

        #region 刷卡
        protected override void AfterSwipingCard()
        {
            base.AfterSwipingCard();
            if (string.IsNullOrEmpty(this.Member.MemberName) ||
                string.IsNullOrEmpty(this.Member.LinkPhone) ||
                string.IsNullOrEmpty(this.Member.Sex) || !this.Member.Birthday.HasValue)

                MessageBox.Show("请补齐会员资料，必须补齐的字段有：会员名、联系电话、性别和生日。");
        }

        #endregion

        #region 结账
        private System.Windows.Visibility _OrderBookVisibility;
        public System.Windows.Visibility OrderBookVisibility
        {
            get
            {
                return _OrderBookVisibility;
            }
            set
            {
                _OrderBookVisibility = value;
                this.RaisePropertyChanged("OrderBookVisibility");
            }
        }
        private System.Windows.Visibility _OrderMenuVisibility;
        public System.Windows.Visibility OrderMenuVisibility
        {
            get
            {
                return _OrderMenuVisibility;
            }
            set
            {
                _OrderMenuVisibility = value;
                this.RaisePropertyChanged("OrderMenuVisibility");
            }
        }
        public virtual void AfterCheckOut()
        {
        }
        private void CheckOutExecute()
        {
            if (this.DeptType== DXInfo.Models.DeptType.Shop)
            {
                if (this.SelectedDeskEx == null)
                {
                    MessageBox.Show("请选择桌台");
                    return;
                }
                if (this.SelectedOrderDish == null)
                {
                    MessageBox.Show("请首先开台");
                    return;
                }
                Guid orderId = this.SelectedOrderDish.Id;
                var q = (from d in Uow.OrderMenus.GetAll()
                         where (d.Status == 3 || d.MissQuantity > 0) && d.OrderId == orderId
                         select d).Count();
                if (q > 0)
                {
                    if (MessageBox.Show("有缺菜是否已经减单或者退单", "结账提示", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                    {
                        return;
                    }
                }
            }
            if (this.SelectedPayType == null)
            {
                MessageBox.Show("请选择支付方式");
                return;
            }
            if (this.IsCard)
            {
                if (this.IsMoney)
                {
                    MemberBalance();
                }
                else
                {
                    Guid payType_Card = Guid.Parse(DXInfo.Business.Helper.PayType_Card);
                    Guid payType_TakeOut = Guid.Parse(DXInfo.Business.Helper.PayType_TakeOut);
                    if (this.SelectedPayType.Id == payType_Card ||
                        this.SelectedPayType.Id == payType_TakeOut)
                    {
                        MemberBalance();
                    }
                    else
                    {
                        MemberNoMoneyBalance();
                    }
                }

            }
            else
            {
                NoMemberBalance();
            }

        }
        public void ResetCheckOut()
        {
            this.SelectedPayType = null;
            this.Voucher = null;
            this.SelectedInventoryEx = null;
            if (this.OCInventoryEx != null)
            {
                this.OCInventoryEx.Clear();
                this.OCInventoryEx = null;
            }
            this.IsCard = false;
            this.IsMoney = false;
            if (this.lPayType != null)
            {
                this.SelectedPayType = this.lPayType.Find(f => f.Name == "现金");
            }
            if (this.DeptType == DXInfo.Models.DeptType.Shop)
            {
                this.SelectedOrderDish = null;
                this.SelectedOrderDesk = null;
                this.OpenOperName = "";
                if (this.lSelectedOrderPackage != null)
                {
                    this.lSelectedOrderPackage.Clear();
                    this.lSelectedOrderPackage = null;
                }

                this.SelectedOrderBook = null;
                this.SelectedOrderBookDesk = null;

                if (this.OCOrderBookEx != null)
                {
                    this.OCOrderBookEx.Clear();
                    this.OCOrderBookEx = null;
                }
                this.OrderBookVisibility = Visibility.Collapsed;
                this.OrderMenuVisibility = Visibility.Collapsed;

                //this.SelectedDeskEx = null;
            }
        }
        private bool CheckOutCanExecute()
        {
            bool isCanExecute = false;
            switch (this.DeptType)
            {
                case DXInfo.Models.DeptType.Sale:
                    if (this.OCInventoryEx != null &&
                        this.OCInventoryEx.Count > 0 &&
                        this.SelectedPayType != null&&
                        !this.IsCancelCheckOut)
                        isCanExecute = true;
                    break;
                case DXInfo.Models.DeptType.Shop:
                    if (this.SelectedOrderDish != null &&
                        this.SelectedOrderDish.Status==(int)DXInfo.Models.OrderDishStatus.Ordered &&
                        this.SelectedPayType != null &&
                        this.OCInventoryEx != null &&
                        this.OCInventoryEx.Count > 0 &&
                        this.IsAuthorize())
                        isCanExecute = true;
                    break;
            }
            return isCanExecute;
        }
        public ICommand CheckOut
        {
            get
            {
                return new RelayCommand(CheckOutExecute, CheckOutCanExecute);
            }
        }
        private bool BalanceCheck()
        {
            if (this.Card == null || this.CardLevel == null)
            {
                MessageBox.Show("请刷卡");
                return false;
            }
            if (this.OCInventoryEx == null || this.OCInventoryEx.Count == 0)
            {
                MessageBox.Show("请选择商品");
                return false;
            }
            if (this.SelectedPayType == null)
            {
                MessageBox.Show("请选择支付方式");
                return false;
            }
            return true;
        }
        private decimal GetDiscount()
        {
            decimal dDiscount = this.CardLevel.Discount;
            if (this.IsOut)
            {
                dDiscount = 100;
            }
            return dDiscount;
        }
        private decimal GetVoucher()
        {
            decimal dVoucher = 0;
            if (this.Voucher.HasValue)
            {
                dVoucher = this.Voucher.Value;
            }
            return dVoucher;
        }
        public bool JudgeIsDiscount(Guid invId)
        {
            bool isDiscount = (from d in Uow.Inventory.GetAll()
                               join d1 in Uow.InventoryCategory.GetAll() on d.Category equals d1.Id into dd1
                               from dd1s in dd1.DefaultIfEmpty()
                               where d.Id == invId
                               select dd1s == null ? true : dd1s.IsDiscount).FirstOrDefault();
            return isDiscount;
        }
        private bool GetOrderMenu(Guid orderId, ref ObservableCollection<DXInfo.Models.InventoryEx> oiex)
        {
            var lom = Uow.OrderMenus.GetAll().Where(w => w.OrderId == orderId &&
                    (w.Status == (int)DXInfo.Models.OrderMenuStatus.Hurry ||
                    w.Status == (int)DXInfo.Models.OrderMenuStatus.Make ||
                    w.Status == (int)DXInfo.Models.OrderMenuStatus.Order ||
                    w.Status == (int)DXInfo.Models.OrderMenuStatus.Out)).ToList();
            List<DXInfo.Models.InventoryEx> liex = new List<DXInfo.Models.InventoryEx>();
            
            lom.ForEach(delegate(DXInfo.Models.OrderMenus om)
            {
                DXInfo.Models.InventoryEx iex = Mapper.Map<DXInfo.Models.InventoryEx>(om);
                iex.Id = om.InventoryId;
                iex.OrderMenuId = om.Id;
                iex.SalePrice = om.Price;
                liex.Add(iex);
            });
            oiex = new ObservableCollection<DXInfo.Models.InventoryEx>(liex);
            
            foreach (DXInfo.Models.InventoryEx iex in oiex)
            {
                DXInfo.Models.Inventory inv = Uow.Inventory.GetById(g => g.Id == iex.Id);
                if (inv == null)
                {
                    Helper.ShowErrorMsg("存货错误!");
                    return false;
                }
                iex.Name = inv.Name;
                iex.Code = inv.Code;
                iex.WhId = inv.WhId;
                iex.Locator = inv.Locator;
                if (!iex.IsPackage)
                {
                    DXInfo.Models.InventoryCategory ic = Uow.InventoryCategory.GetById(g => g.Id == inv.Category);
                    if (ic == null)
                    {
                        Helper.ShowErrorMsg("存货分类错误");
                        return false;
                    }
                    iex.IsDiscount = ic.IsDiscount;
                }
                this.SetInvPrice(iex);
                //this.SetCurrentStock(iex);
            }
            return true;
            
        }
        private decimal GetAmount(ObservableCollection<DXInfo.Models.InventoryEx> oiex, decimal dDiscount)//,decimal dVoucher)
        {
            decimal dAmount = 0;
            switch (this.DeptType)
            {
                case DXInfo.Models.DeptType.Sale:
                    decimal dAmount01 = oiex.Where(w => w.IsDiscount).Sum(s => s.CurrentAmount);
                    decimal dAmount02 = oiex.Where(w => !w.IsDiscount).Sum(s => s.CurrentAmount);
                    decimal dAmount03 = (Math.Round(dAmount01 * dDiscount / 100, 2) + dAmount02);
                    //dAmount = dVoucher > dAmount03 ? 0 : dAmount03 - dVoucher;
                    dAmount = dAmount03;
                    break;
                case DXInfo.Models.DeptType.Shop:
                    decimal dAmount1 = oiex.Where(w => w.IsDiscount).Sum(s => s.Amount);
                    decimal dAmount2 = oiex.Where(w => !w.IsDiscount).Sum(s => s.Amount);
                    decimal dAmount3 = (Math.Round(dAmount1 * dDiscount / 100, 2) + dAmount2);
                    //dAmount = dVoucher > dAmount3 ? 0 : dAmount3 - dVoucher;
                    dAmount = dAmount3;
                    break;
            }
            return dAmount;
        }
        private decimal GetSum(ObservableCollection<DXInfo.Models.InventoryEx> oiex)
        {
            decimal dAmount = 0;
            //switch (this.DeptType)
            //{
            //    case DXInfo.Models.DeptType.Sale:
            //        dAmount = oiex.Sum(s => s.Amount);
            //        break;
            //    case DXInfo.Models.DeptType.Shop:
                    dAmount = oiex.Sum(s => s.Amount);
            //        break;
            //}
            return dAmount;
        }        
        private string GetTitle()
        {
            string title = "";
            
            switch (this.DeptType)
            {
                case DXInfo.Models.DeptType.Sale:
                    title = ClientCommon.PrintTicketTitle(DXInfo.Models.NameCodeType.PrintTicketTitle1OfCold);
                    break;
                case DXInfo.Models.DeptType.Shop:
                    title = ClientCommon.PrintTicketTitle(DXInfo.Models.NameCodeType.PrintTicketTitle1OfWR);
                    break;
            }
            return title;
        }                
        private bool GetOCInvEx(ref ObservableCollection<DXInfo.Models.InventoryEx> oiex)
        {
            bool ret = true;
            switch (this.DeptType)
            {
                case DXInfo.Models.DeptType.Sale:
                    oiex = this.OCInventoryEx;
                    break;
                case DXInfo.Models.DeptType.Shop:
                    ret = GetOrderMenu(this.SelectedOrderDish.Id, ref oiex);
                    break;
            }
            return ret;
        }
        private List<DXInfo.Models.CardDonateInventoryEx> GetCardDonateInventoryEx()
        {
            List<DXInfo.Models.CardDonateInventoryEx> lCardDonateInventoryEx = ClientCommon.GetCardDonateInventoryEx(Card.Id);
            ClientCommon.CardDonateInventoryExDeptPrice(Dept, lCardDonateInventoryEx);
            if (lCardDonateInventoryEx.Count > 0)
            {
                CardDonateInventoryViewModel donateVM = new CardDonateInventoryViewModel(Uow, lCardDonateInventoryEx);
                CardDonateInventoryWindow donateWindow = new CardDonateInventoryWindow(donateVM);
                if (donateWindow.ShowDialog().GetValueOrDefault())
                {
                    var di = lCardDonateInventoryEx.Where(w => w.IsCheck).ToList();
                    return di;
                }
            }
            return null;
        }
        private bool CheckLackMenu(ObservableCollection<DXInfo.Models.InventoryEx> oiex)
        {
            decimal d = oiex.Sum(s => s.MissQuantity);
            if (d > 0)
            {
                if (MessageBox.Show("有缺菜是否已经减掉或者退菜", "结账", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                {
                    return false;
                }
            }
            return true;
        }
        private void SetImageFilePath(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                string imageFileName = dr["ImageFileName"].ToString();
                if (!string.IsNullOrEmpty(imageFileName))
                {
                    string path = ConfigurationManager.AppSettings["imageFilePath"];
                    string imageFilePath = "file:///" + System.IO.Path.Combine(path, imageFileName);
                    dr["ImageFileName"] = imageFilePath;
                }
            }
        }
        private void MemberBalance()
        {
            if (!BalanceCheck()) return;
            ObservableCollection<DXInfo.Models.InventoryEx> oiex = new ObservableCollection<DXInfo.Models.InventoryEx>();
            if (!GetOCInvEx(ref oiex)) return;
            decimal dVoucher = GetVoucher();
            decimal dDiscount = GetDiscount();
            decimal dSum = GetSum(oiex);                        
            decimal dPayAmount = GetAmount(oiex, dDiscount);//, dPayVoucher);
            decimal dPayVoucher = dVoucher > dPayAmount ? dPayAmount : dVoucher;
            decimal dAmount = dPayAmount > dPayVoucher ? dPayAmount - dPayVoucher : 0;
            string title = GetTitle();

            if (!this.CardBalance.HasValue || (this.CardBalance.Value == 0 && dAmount > this.CardBalance.Value))
            {
                MessageBox.Show("余额不足");
                return;
            }
            decimal dLastBalance = this.CardBalance.Value;
            if (dAmount > this.CardBalance.Value)
            {
                MessageBox.Show("余额不足");
                return;
            }

            bool bPassword = !string.IsNullOrEmpty(this.Card.CardPwd);
            List<string> lValidationPropertyNames;
            DXInfo.Business.MemberManageFacade mb = new DXInfo.Business.MemberManageFacade(Uow);
            bool checkOuted;
            NoMemberCashViewModel ncv;
            NoMemberCashWindow ncw;
            
            decimal dQuantity = oiex.Sum(s => s.Quantity);
            
            decimal dBalance = dLastBalance - dAmount;
            List<DXInfo.Models.CardDonateInventoryEx> lCardDonateInventoryEx = GetCardDonateInventoryEx();
            DateTime dCreateDate = DateTime.Now;
            string sn = Dept.DeptCode + dCreateDate.ToString("yyyyMMddHHmmssfff");

            DXInfo.Business.CheckoutParaObj para = new DXInfo.Business.CheckoutParaObj();
            para.IsCard = true;
            para.DeptId = Dept.DeptId;
            para.DeptName = Dept.DeptName;
            para.UserId = User.UserId;
            para.UserName = User.UserName;
            para.FullName = Oper.FullName;
            para.PayTypeId = SelectedPayType.Id;
            para.PayTypeName = SelectedPayType.Name;
            para.CardId = Card.Id;
            para.CardNo = Card.CardNo;
            para.LastBalance = dLastBalance;
            para.Balance = dBalance;
            para.MemberId = Member.Id;
            para.MemberName = Member.MemberName;
            para.CreateDate=dCreateDate;
            para.IsVirtual = this.CardType.IsVirtual;
            para.IsCardLevelAuto = this.IsCardLevelAuto;
            para.lInventoryEx = oiex;
            para.lCardDonateInventoryEx = lCardDonateInventoryEx;
            para.ConsumeType = (int)DXInfo.Models.ConsumeType.Card;            
            para.Voucher = dVoucher;
            para.PayVoucher =dPayVoucher;
            para.Sum = dSum;
            para.Discount= dDiscount;
            para.Amount =dAmount;
            para.Quantity =dQuantity;            
            para.Sn=sn;
            switch (this.DeptType)
            {
                case DXInfo.Models.DeptType.Sale:
                    #region 零售
                    bool deskNoVisi = BusinessCommon.DeskNoVisibility();
                    if (bPassword)
                    {
                        lValidationPropertyNames = new List<string>() { "DeskNo", "Password" };
                    }
                    else
                    {
                        if (deskNoVisi)
                        {
                            lValidationPropertyNames = new List<string>() { "DeskNo" };
                        }
                        else
                        {
                            lValidationPropertyNames = new List<string>();
                        }
                    }
                    ncv = new NoMemberCashViewModel(Uow, lValidationPropertyNames, 0, "消费", false, bPassword, deskNoVisi);
                    ncw = new NoMemberCashWindow(ncv);
                    ncw.ShowDialog();

                    if (ncv.DialogResult.HasValue && ncv.DialogResult.Value)
                    {
                        if (bPassword && this.Card.CardPwd != ncv.Password)
                        {
                            Helper.ShowErrorMsg("密码错误");
                            return;
                        }
                        DeskNo = ncv.DeskNo;
                        para.SourceType = (int)DXInfo.Models.SourceType.ColdDrinkShop;
                        para.DeskNo =DeskNo;
                        para.BillType = DXInfo.Models.BillType.CardConsumeWindow.ToString();
                        checkOuted = mb.CheckOut(para,FairiesCoolerCash.Business.Helper.CardConsume);
                        if (!checkOuted)
                        {
                            Helper.ShowErrorMsg(mb.ErrorMsg);
                            return;
                        }
                        if (this.IsTicket2)
                        {
                            MemberConsumePrintObject2 po2 = new MemberConsumePrintObject2(title, oiex, lCardDonateInventoryEx, 
                                this.Card.CardNo, this.Member.MemberName, dLastBalance,
                                dBalance, dSum, dDiscount, dAmount, dVoucher, DeskNo,
                                 this.Oper.FullName, this.User.UserName, this.Dept.DeptName, dCreateDate, this.IsCupType);
                            po2.Print();
                        }

                        if (this.IsTicket1)
                        {
                            MemberConsumePrintObject po = new MemberConsumePrintObject(oiex, lCardDonateInventoryEx, DeskNo, Dept.DeptName, dCreateDate, dSum, dQuantity, this.IsCupType);
                            ;
                            po.Print();
                        }

                        if (this.IsStickerPrint)
                        {
                            try
                            {
                                mb.StickerBill(oiex, DeskNo,dCreateDate,Dept.DeptName);
                                ClientCommon.PrintSticker(oiex, DeskNo, dCreateDate, Dept.DeptName);
                            }
                            catch (Exception ex)
                            {
                                Helper.ShowErrorMsg(ex.Message);
                                Helper.HandelException(ex);
                            }

                        }
                        if (this.IsThree)
                        {
                            LocalReport report = new LocalReport();
                            report.ReportPath = GetThreePrintFile(DXInfo.Models.NameCodeType.SaleThreePrintMemmber);//@"Report1.rdlc";
                            NoMemberThreePrintObject threePrintObject = new NoMemberThreePrintObject();
                            threePrintObject.Title = title;
                            //threePrintObject.DeskNo = DeskNo;
                            //threePrintObject.PeopleCount = this.SelectedOrderDish.Quantity;
                            threePrintObject.Amount = dAmount;
                            threePrintObject.CreateDate = dCreateDate;
                            threePrintObject.ButtomTitle = GetButtomTitle(this.DeptType);
                            threePrintObject.Sum = dSum;
                            threePrintObject.DeptName = Dept.DeptName;
                            threePrintObject.Voucher = dVoucher;
                            threePrintObject.FullName = Oper.FullName;
                            threePrintObject.UserName = User.UserName;
                            threePrintObject.PayTypeName = SelectedPayType.Name;

                            threePrintObject.CardNo = this.Card.CardNo;
                            threePrintObject.MemberName = this.Member.MemberName;
                            threePrintObject.Discount = dDiscount;
                            threePrintObject.Balance = dBalance;
                            threePrintObject.LastBalance = dLastBalance;
                            threePrintObject.Sn = sn;
                            DataTable dt = threePrintObject.ToDataTable();
                            DataTable dt2 = oiex.ToDataTable<DXInfo.Models.InventoryEx>();
                            DataTable dt3 = lCardDonateInventoryEx.ToDataTable<DXInfo.Models.CardDonateInventoryEx>();
                            this.SetImageFilePath(dt2);
                            report.DataSources.Add(
                               new ReportDataSource("DataSet1", dt)
                               );
                            report.DataSources.Add(
                               new ReportDataSource("DataSet2", dt2)
                               );
                            report.DataSources.Add(
                               new ReportDataSource("DataSet3", dt3)
                               );
                            PrintRDLC printRDLC = new PrintRDLC();
                            printRDLC.Run(report);
                        }
                        this.AfterCheckOut();
                        this.ResetSwipingCard();
                        this.ResetCheckOut();                        
                    }
#endregion
                    break;
                case DXInfo.Models.DeptType.Shop:
                    if (!CheckLackMenu(oiex)) return;
                    if (bPassword)
                    {
                        lValidationPropertyNames = new List<string>() { "Password" };
                        ncv = new NoMemberCashViewModel(Uow, lValidationPropertyNames, 0, "消费", false, bPassword, false);
                        ncw = new NoMemberCashWindow(ncv);
                        ncw.ShowDialog();
                        if (!ncv.DialogResult.HasValue || !ncv.DialogResult.Value)
                        {
                            return;
                        }
                        if (this.Card.CardPwd != ncv.Password)
                        {
                            Helper.ShowErrorMsg("密码错误");
                            return;
                        }
                    }
                    para.SourceType = (int)DXInfo.Models.SourceType.WesternRestaurant;
                    para.DeskNo =DeskNo;
                    para.OrderDishId = this.SelectedOrderDish.Id;
                    para.PeopleCount = this.SelectedOrderDish.Quantity;
                    para.BillType = DXInfo.Models.BillType.WRCardConsumeWindow.ToString();
                    checkOuted = mb.CheckOut(para,FairiesCoolerCash.Business.Helper.CardConsume);

                    if (!checkOuted)
                    {
                        Helper.ShowErrorMsg(mb.ErrorMsg);
                        return;
                    }
                    if (this.IsThree)
                    {
                        LocalReport report = new LocalReport();
                        report.ReportPath = GetThreePrintFile(DXInfo.Models.NameCodeType.ThreePrintMemmber);//@"Report1.rdlc";
                        NoMemberThreePrintObject threePrintObject = new NoMemberThreePrintObject();
                        threePrintObject.Title = title;
                        threePrintObject.DeskNo = DeskNo;
                        threePrintObject.PeopleCount = this.SelectedOrderDish.Quantity;
                        threePrintObject.Amount = dAmount;
                        threePrintObject.CreateDate = dCreateDate;
                        threePrintObject.ButtomTitle = GetButtomTitle(this.DeptType);
                        threePrintObject.Sum = dSum;
                        threePrintObject.DeptName = Dept.DeptName;
                        threePrintObject.Voucher = dVoucher;
                        threePrintObject.FullName = Oper.FullName;
                        threePrintObject.UserName = User.UserName;
                        threePrintObject.PayTypeName = SelectedPayType.Name;
                        
                        threePrintObject.CardNo = this.Card.CardNo;
                        threePrintObject.MemberName = this.Member.MemberName;                        
                        threePrintObject.Discount = dDiscount;                        
                        threePrintObject.Balance = dBalance;
                        threePrintObject.LastBalance = dLastBalance;
                        threePrintObject.Sn = sn;
                        DataTable dt = threePrintObject.ToDataTable();
                        DataTable dt2 = oiex.ToDataTable<DXInfo.Models.InventoryEx>();
                        DataTable dt3 = lCardDonateInventoryEx.ToDataTable<DXInfo.Models.CardDonateInventoryEx>();
                        report.DataSources.Add(
                           new ReportDataSource("DataSet1", dt)
                           );
                        report.DataSources.Add(
                           new ReportDataSource("DataSet2", dt2)
                           );
                        report.DataSources.Add(
                           new ReportDataSource("DataSet3", dt3)
                           );
                        PrintRDLC printRDLC = new PrintRDLC();
                        printRDLC.Run(report);
                    }
                    else
                    {
                        WRMemberConsumePrintObject2 wrpo2 = new WRMemberConsumePrintObject2(title, oiex, lCardDonateInventoryEx, this.Card.CardNo, this.Member.MemberName, dLastBalance,
                            dBalance, dSum, dDiscount, dAmount, dVoucher, DeskNo,
                             this.Oper.FullName, this.User.UserName, this.Dept.DeptName, dCreateDate,this.Dept.Comment);
                        wrpo2.Print();
                    }
                    this.AfterCheckOut();
                    this.ResetSwipingCard();
                    this.ResetCheckOut();                    
                    break;
            }

        }        
        private void MemberNoMoneyBalance()
        {
            if (!BalanceCheck()) return;
            ObservableCollection<DXInfo.Models.InventoryEx> oiex = new ObservableCollection<DXInfo.Models.InventoryEx>();
            if (!GetOCInvEx(ref oiex)) return;
            decimal dVoucher = GetVoucher();
            decimal dDiscount = GetDiscount();
            decimal dSum = GetSum(oiex);            
            string title = GetTitle();

            decimal dReceivableAmount = 0;
            decimal dPayAmount = 0;

            switch (this.DeptType)
            {
                case DXInfo.Models.DeptType.Sale:
                    dPayAmount = GetAmount(oiex, dDiscount);//, dPayVoucher);                    
                    break;
                case DXInfo.Models.DeptType.Shop:
                    dPayAmount = Math.Floor(GetAmount(oiex, dDiscount));//, dPayVoucher));
                    break;
            }
            decimal dPayVoucher = dVoucher > dPayAmount ? dPayAmount : dVoucher;
            decimal dAmount = dPayAmount > dPayVoucher ? dPayAmount - dPayVoucher : 0;
            dReceivableAmount = dAmount;

            bool bPassword = !string.IsNullOrEmpty(this.Card.CardPwd);
            List<string> lValidationPropertyNames;
            NoMemberCashViewModel ncv;
            NoMemberCashWindow ncw;
            
            decimal dQuantity = oiex.Sum(s => s.Quantity);

            decimal dLastBalance = this.CardBalance.Value;
            decimal dBalance = dLastBalance;// -dAmount;
            DXInfo.Business.MemberManageFacade mb = new DXInfo.Business.MemberManageFacade(Uow);
            List<DXInfo.Models.CardDonateInventoryEx> lCardDonateInventoryEx = GetCardDonateInventoryEx();
            DateTime dCreateDate = DateTime.Now;
            string sn = Dept.DeptCode + dCreateDate.ToString("yyyyMMddHHmmssfff");

            DXInfo.Business.CheckoutParaObj para = new DXInfo.Business.CheckoutParaObj();
            para.IsCard = true;
            para.DeptId = Dept.DeptId;
            para.DeptName = Dept.DeptName;
            para.UserId = User.UserId;
            para.UserName = User.UserName;
            para.FullName = Oper.FullName;
            para.PayTypeId = SelectedPayType.Id;
            para.PayTypeName = SelectedPayType.Name;
            para.CardId = Card.Id;
            para.CardNo = Card.CardNo;
            para.LastBalance = dLastBalance;
            para.Balance = dBalance;
            para.MemberId = Member.Id;
            para.MemberName = Member.MemberName;
            para.CreateDate = dCreateDate;
            para.IsVirtual = this.CardType.IsVirtual;
            para.IsCardLevelAuto = this.IsCardLevelAuto;
            para.lInventoryEx = oiex;
            para.lCardDonateInventoryEx = lCardDonateInventoryEx;
            para.ConsumeType = (int)DXInfo.Models.ConsumeType.CardNoMoney;
            para.Voucher = dVoucher;
            para.PayVoucher = dPayVoucher;
            para.Sum = dSum;
            para.Discount = dDiscount;
            para.Amount = dAmount;
            para.Quantity = dQuantity;
            para.Sn = sn;
            para.Erasing = this.Erasing;
            switch (this.DeptType)
            {
                case DXInfo.Models.DeptType.Sale:
                    #region 零售
                    bool deskNoVisi = BusinessCommon.DeskNoVisibility();
                    if (bPassword)
                    {
                        lValidationPropertyNames = new List<string>() { "DeskNo", "Password"};
                    }
                    else
                    {
                        if (deskNoVisi)
                        {
                            lValidationPropertyNames = new List<string>() { "DeskNo" };
                        }
                        else
                        {
                            lValidationPropertyNames = new List<string>();
                        }
                    }
                    ncv = new NoMemberCashViewModel(Uow, lValidationPropertyNames, dReceivableAmount, "收您", true, bPassword, deskNoVisi);
                    ncw = new NoMemberCashWindow(ncv);
                    ncw.ShowDialog();

                    if (ncv.DialogResult.HasValue && ncv.DialogResult.Value)
                    {
                        if (bPassword && this.Card.CardPwd != ncv.Password)
                        {
                            Helper.ShowErrorMsg("密码错误");
                            return;
                        }
                        decimal dCash = dReceivableAmount;
                        if (ncv.Cash.HasValue)
                        {
                            dCash = ncv.Cash.Value;
                        }
                        if (dCash < dAmount)
                        {
                            Helper.ShowErrorMsg("收的钱应不小于消费金额");
                            return;
                        }
                        DeskNo = ncv.DeskNo;

                        if (dCash < dAmount) throw new ArgumentException("收的钱应不小于消费金额");
                        decimal dChange = dCash - dAmount;

                        para.DeskNo = DeskNo;
                        para.SourceType = (int)DXInfo.Models.SourceType.ColdDrinkShop;
                        para.BillType = DXInfo.Models.BillType.CardConsume3Window.ToString();
                        para.Change = dChange;
                        para.Cash = dCash;
                        bool checkOuted = mb.CheckOut(para,FairiesCoolerCash.Business.Helper.CardConsume);
                        if (!checkOuted)
                        {
                            Helper.ShowErrorMsg(mb.ErrorMsg);
                            return;
                        }
                        if (this.IsTicket2)
                        {
                            MemberConsumePrintObject3 po3 = new MemberConsumePrintObject3(title, oiex, lCardDonateInventoryEx, this.Card.CardNo, this.Member.MemberName, dCash,
                                dChange, dSum, dDiscount, dAmount, dVoucher, SelectedPayType.Name, DeskNo,
                                 this.Oper.FullName, this.User.UserName, this.Dept.DeptName, dCreateDate, this.IsCupType);
                            po3.Print();
                        }

                        if (this.IsTicket1)
                        {
                            MemberConsumePrintObject po = new MemberConsumePrintObject(oiex, lCardDonateInventoryEx, DeskNo, Dept.DeptName, dCreateDate, dSum, dQuantity, this.IsCupType);
                            ;
                            po.Print();
                        }

                        if (this.IsStickerPrint)
                        {
                            try
                            {
                                mb.StickerBill(oiex, DeskNo,dCreateDate,Dept.DeptName);
                                ClientCommon.PrintSticker(oiex, DeskNo, dCreateDate, Dept.DeptName);
                            }
                            catch (Exception ex)
                            {
                                Helper.ShowErrorMsg(ex.Message);
                                Helper.HandelException(ex);
                            }
                        }
                        if (this.IsThree)
                        {
                            LocalReport report = new LocalReport();
                            report.ReportPath = GetThreePrintFile(DXInfo.Models.NameCodeType.SaleThreePrintMemmberNoMoney);
                            NoMemberThreePrintObject threePrintObject = new NoMemberThreePrintObject();
                            threePrintObject.Title = title;
                            threePrintObject.Amount = dAmount;
                            threePrintObject.CreateDate = dCreateDate;
                            threePrintObject.Change = dChange;
                            threePrintObject.Cash = dCash;
                            threePrintObject.ButtomTitle = GetButtomTitle(this.DeptType);
                            threePrintObject.Sum = dSum;
                            threePrintObject.DeptName = Dept.DeptName;
                            threePrintObject.Voucher = dVoucher;
                            threePrintObject.FullName = Oper.FullName;
                            threePrintObject.UserName = User.UserName;
                            threePrintObject.PayTypeName = SelectedPayType.Name;

                            threePrintObject.CardNo = this.Card.CardNo;
                            threePrintObject.MemberName = this.Member.MemberName;
                            threePrintObject.Discount = dDiscount;
                            threePrintObject.Sn = sn;

                            DataTable dt = threePrintObject.ToDataTable();
                            DataTable dt2 = oiex.ToDataTable<DXInfo.Models.InventoryEx>();
                            DataTable dt3 = lCardDonateInventoryEx.ToDataTable<DXInfo.Models.CardDonateInventoryEx>();
                            this.SetImageFilePath(dt2);
                            report.DataSources.Add(
                               new ReportDataSource("DataSet1", dt)
                               );
                            report.DataSources.Add(
                               new ReportDataSource("DataSet2", dt2)
                               );
                            report.DataSources.Add(
                               new ReportDataSource("DataSet3", dt3)
                               );
                            PrintRDLC printRDLC = new PrintRDLC();
                            printRDLC.Run(report);
                        }
                        this.AfterCheckOut();
                        this.ResetSwipingCard();
                        this.ResetCheckOut();
                    }
#endregion
                    break;
                case DXInfo.Models.DeptType.Shop:
                    if (!CheckLackMenu(oiex)) return;
                    if (bPassword)
                    {
                        lValidationPropertyNames = new List<string>() { "Password"};
                    }
                    else
                    {
                        lValidationPropertyNames = new List<string>() ;
                    }
                    ncv = new NoMemberCashViewModel(Uow, lValidationPropertyNames, dReceivableAmount, "收您", true, bPassword, false);
                    ncw = new NoMemberCashWindow(ncv);
                    ncw.ShowDialog();

                    if (ncv.DialogResult.HasValue && ncv.DialogResult.Value)
                    {
                        if (bPassword && this.Card.CardPwd != ncv.Password)
                        {
                            Helper.ShowErrorMsg("密码错误");
                            return;
                        }
                        decimal dCash = dReceivableAmount;
                        if (ncv.Cash.HasValue)
                        {
                            dCash = ncv.Cash.Value;
                        }
                        if (dCash < dAmount)
                        {
                            if (!(this.Erasing && (int)dCash / 10 == (int)dAmount / 10))
                            {
                                Helper.ShowErrorMsg("收的钱应不小于消费金额");
                                return;
                            }
                        }
                        decimal dChange = dCash>dAmount?dCash - dAmount:0;

                        para.DeskNo = DeskNo;
                        para.SourceType = (int)DXInfo.Models.SourceType.WesternRestaurant;
                        para.BillType = DXInfo.Models.BillType.WRCardConsume3Window.ToString();
                        para.Change = dChange;
                        para.Cash = dCash;
                        para.OrderDishId = this.SelectedOrderDish.Id;
                        para.PeopleCount = this.SelectedOrderDish.Quantity;
                        bool checkOuted = mb.CheckOut(para, FairiesCoolerCash.Business.Helper.CardConsume);
                        if (!checkOuted)
                        {
                            Helper.ShowErrorMsg(mb.ErrorMsg);
                            return;
                        }
                        if (this.IsThree)
                        {
                            LocalReport report = new LocalReport();
                            report.ReportPath = GetThreePrintFile(DXInfo.Models.NameCodeType.ThreePrintMemmberNoMoney);
                            NoMemberThreePrintObject threePrintObject = new NoMemberThreePrintObject();
                            threePrintObject.Title = title;
                            threePrintObject.DeskNo = DeskNo;
                            threePrintObject.PeopleCount = this.SelectedOrderDish.Quantity;
                            threePrintObject.Amount = dAmount;
                            threePrintObject.CreateDate = dCreateDate;
                            threePrintObject.Change = dChange;
                            threePrintObject.Cash = dCash;
                            threePrintObject.ButtomTitle = GetButtomTitle(this.DeptType);
                            threePrintObject.Sum = dSum;
                            threePrintObject.DeptName = Dept.DeptName;
                            threePrintObject.Voucher = dVoucher;
                            threePrintObject.FullName = Oper.FullName;
                            threePrintObject.UserName = User.UserName;
                            threePrintObject.PayTypeName = SelectedPayType.Name;

                            threePrintObject.CardNo = this.Card.CardNo;
                            threePrintObject.MemberName = this.Member.MemberName;
                            threePrintObject.Discount = dDiscount;
                            threePrintObject.Sn = sn;
                            DataTable dt = threePrintObject.ToDataTable();
                            DataTable dt2 = oiex.ToDataTable<DXInfo.Models.InventoryEx>();
                            DataTable dt3 = lCardDonateInventoryEx.ToDataTable<DXInfo.Models.CardDonateInventoryEx>();
                            report.DataSources.Add(
                               new ReportDataSource("DataSet1", dt)
                               );
                            report.DataSources.Add(
                               new ReportDataSource("DataSet2", dt2)
                               );
                            report.DataSources.Add(
                               new ReportDataSource("DataSet3", dt3)
                               );
                            PrintRDLC printRDLC = new PrintRDLC();
                            printRDLC.Run(report);
                        }
                        else
                        {
                            WRMemberConsumePrintObject3 po3 = new WRMemberConsumePrintObject3(title, oiex, lCardDonateInventoryEx, this.Card.CardNo, this.Member.MemberName, dCash,
                                dChange, dSum, dDiscount, dAmount, dVoucher, SelectedPayType.Name, DeskNo,
                                 this.Oper.FullName, this.User.UserName, this.Dept.DeptName, dCreateDate,this.Dept.Comment);
                            po3.Print();
                        }
                        this.AfterCheckOut();
                        this.ResetSwipingCard();
                        this.ResetCheckOut();
                    }
                    break;
            }
        }
        private void NoMemberBalance()
        {
            //结账
            ObservableCollection<DXInfo.Models.InventoryEx> oiex = new ObservableCollection<DXInfo.Models.InventoryEx>();
            if (!GetOCInvEx(ref oiex)) return;
            decimal dVoucher = GetVoucher();
            decimal dDiscount = 100;
            decimal dSum = GetSum(oiex);
            string title = GetTitle();
            DateTime dCreateDate = DateTime.Now;
            string sn = Dept.DeptCode + dCreateDate.ToString("yyyyMMddHHmmssfff");            
            decimal dQuantity = oiex.Sum(s => s.Quantity);

            decimal dPayAmount =0;
            decimal dReceivableAmount = 0;
            switch (this.DeptType)
            {
                case DXInfo.Models.DeptType.Sale:
                    dPayAmount = GetAmount(oiex, 100);
                    break;
                case DXInfo.Models.DeptType.Shop:
                    dPayAmount = Math.Floor(dSum);
                    break;
            }
            decimal dPayVoucher = dVoucher > dPayAmount ? dPayAmount : dVoucher;
            decimal dAmount = dPayAmount > dPayVoucher ? dPayAmount - dPayVoucher : 0;
            dReceivableAmount = dAmount;

            if (this.IsInvDynamicPrice)
            {
                dDiscount = Convert.ToInt32(dAmount / dSum * 100);
            }
            NoMemberCashViewModel ncv;
            NoMemberCashWindow ncw;
            DXInfo.Business.MemberManageFacade mb = new DXInfo.Business.MemberManageFacade(Uow);
            List<string> lValidationPropertyNames;

            DXInfo.Business.CheckoutParaObj para = new DXInfo.Business.CheckoutParaObj();
            para.IsCard = false;
            para.DeptId = Dept.DeptId;
            para.DeptName = Dept.DeptName;
            para.UserId = User.UserId;
            para.UserName = User.UserName;
            para.FullName = Oper.FullName;
            para.PayTypeId = SelectedPayType.Id;
            para.PayTypeName = SelectedPayType.Name;
            //para.CardId = Card.Id;
            //para.CardNo = Card.CardNo;
            //para.LastBalance = dLastBalance;
            //para.Balance = dBalance;
            //para.MemberId = Member.Id;
            //para.MemberName = Member.MemberName;
            para.CreateDate = dCreateDate;
            //para.IsVirtual = this.CardType.IsVirtual;
            para.IsCardLevelAuto = this.IsCardLevelAuto;
            para.lInventoryEx = oiex;
            para.lCardDonateInventoryEx = lCardDonateInventoryEx;
            para.ConsumeType = (int)DXInfo.Models.ConsumeType.NoMember;
            para.Voucher = dVoucher;
            para.PayVoucher = dPayVoucher;
            para.Sum = dSum;
            para.Discount = dDiscount;
            para.Amount = dAmount;
            para.Quantity = dQuantity;
            para.Sn = sn;
            para.Erasing = this.Erasing;
            //para.Comment = Dept.Comment;
            switch (this.DeptType)
            {
                case DXInfo.Models.DeptType.Sale:
                    #region 零售
                    //dAmount = GetAmount(oiex, dDiscount);//, dVoucher);
                    //dReceivableAmount = dAmount;
                    if (!this.PayTypeColumnVisibility)
                    {
                        PayTypeViewModel ptvw = new PayTypeViewModel(Uow, new List<string>());
                        PayTypeWindow ptw = new PayTypeWindow(ptvw);
                        ptw.ShowDialog();
                        if (ptw.DialogResult.HasValue && ptw.DialogResult.Value)
                        {
                            this.SelectedPayType = ptvw.SelectedPayType;
                        }
                        else
                        {
                            return;
                        }
                    }
                    bool deskNoVisi = BusinessCommon.DeskNoVisibility();
                    if (deskNoVisi)
                    {
                        lValidationPropertyNames = new List<string>() { "DeskNo" };
                    }
                    else
                    {
                        lValidationPropertyNames = new List<string>();
                    }
                    ncv = new NoMemberCashViewModel(Uow, lValidationPropertyNames, dReceivableAmount, "收您", true, false, deskNoVisi);
                    ncw = new NoMemberCashWindow(ncv);
                    ncw.ShowDialog();

                    if (ncv.DialogResult.HasValue && ncv.DialogResult.Value)
                    {
                        decimal dCash = dReceivableAmount;
                        if (ncv.Cash.HasValue)
                        {
                            dCash = ncv.Cash.Value;
                        }
                        DeskNo = ncv.DeskNo;

                        if (dCash < dAmount)
                        {
                            Helper.ShowErrorMsg("收的钱应不小于消费金额");
                            return;
                        }
                        decimal dChange = dCash - dAmount;

                        para.DeskNo = DeskNo;
                        para.SourceType = (int)DXInfo.Models.SourceType.ColdDrinkShop;
                        para.BillType = DXInfo.Models.BillType.NoMemberConsumeWindow.ToString();
                        para.Change = dChange;
                        para.Cash = dCash;
                        bool checkOuted = mb.CheckOut(para, FairiesCoolerCash.Business.Helper.CardConsume);
                        if (!checkOuted)
                        {
                            Helper.ShowErrorMsg(mb.ErrorMsg);
                            return;
                        }
                        if (this.IsTicket3)
                        {
                            
                            NoMemberConsumePrintObject2 po2 = new NoMemberConsumePrintObject2(title, oiex, dSum, dAmount, dVoucher, dCash, dChange, SelectedPayType.Name, DeskNo, 
                                Oper.FullName,User.UserName, Dept.DeptName, dCreateDate,this.IsCupType);
                            po2.Print();
                        }
                        if (this.IsTicket1)
                        {
                            NoMemberConsumePrintObject po = new NoMemberConsumePrintObject(oiex, DeskNo, Dept.DeptName, dCreateDate, dSum, dQuantity, this.IsCupType);
                            ;
                            po.Print();
                        }

                        if (this.IsStickerPrint)
                        {
                            try
                            {
                                mb.StickerBill(oiex, DeskNo,dCreateDate,Dept.DeptName);
                                ClientCommon.PrintSticker(oiex, DeskNo, dCreateDate, Dept.DeptName);
                            }
                            catch (Exception ex)
                            {
                                Helper.ShowErrorMsg(ex.Message);
                                Helper.HandelException(ex);
                            }
                        }
                        if (this.IsThree)
                        {
                            LocalReport report = new LocalReport();
                            report.ReportPath = GetThreePrintFile(DXInfo.Models.NameCodeType.SaleThreePrintNoMemmber);//@"Report1.rdlc";
                            NoMemberThreePrintObject threePrintObject = new NoMemberThreePrintObject();
                            threePrintObject.Title = title;
                            threePrintObject.Amount = dAmount;
                            threePrintObject.CreateDate = dCreateDate;
                            threePrintObject.Change = dChange;
                            threePrintObject.Cash = dCash;
                            threePrintObject.ButtomTitle = GetButtomTitle(this.DeptType);
                            threePrintObject.Sum = dSum;
                            threePrintObject.DeptName = Dept.DeptName;
                            threePrintObject.Voucher = dVoucher;
                            threePrintObject.FullName = Oper.FullName;
                            threePrintObject.UserName = User.UserName;
                            threePrintObject.PayTypeName = SelectedPayType.Name;
                            threePrintObject.Sn = sn;
                            threePrintObject.Discount = dDiscount;
                            DataTable dt = threePrintObject.ToDataTable();
                            DataTable dt2 = oiex.ToDataTable<DXInfo.Models.InventoryEx>();

                            this.SetImageFilePath(dt2);
                            report.DataSources.Add(
                               new ReportDataSource("DataSet1", dt)
                               );
                            report.DataSources.Add(
                               new ReportDataSource("DataSet2", dt2)
                               );
                            PrintRDLC printRDLC = new PrintRDLC();
                            printRDLC.Run(report);
                        }
                        this.AfterCheckOut();
                        this.ResetCheckOut();
                    }
#endregion
                    break;
                case DXInfo.Models.DeptType.Shop:                    
                    if (!CheckLackMenu(oiex)) return;
                    ncv = new NoMemberCashViewModel(Uow, new List<string>(), dReceivableAmount, "收您", true, false, false);
                    ncw = new NoMemberCashWindow(ncv);
                    ncw.ShowDialog();

                    if (ncv.DialogResult.HasValue && ncv.DialogResult.Value)
                    {
                        decimal dCash = dReceivableAmount;
                        if (ncv.Cash.HasValue)
                        {
                            dCash = ncv.Cash.Value;
                        }

                        if (dCash < dAmount)
                        {
                            if (!(this.Erasing && (int)dCash / 10 == (int)dAmount / 10))
                            {
                                Helper.ShowErrorMsg("收的钱应不小于消费金额");
                                return;
                            }
                        }
                        decimal dChange = dCash > dAmount?dCash - dAmount:0;
                        DeskNo = this.SelectedDeskEx.Code;

                        para.DeskNo = DeskNo;
                        para.SourceType = (int)DXInfo.Models.SourceType.WesternRestaurant;
                        para.BillType = DXInfo.Models.BillType.WRNoMemberConsumeWindow.ToString();
                        para.Change = dChange;
                        para.Cash = dCash;
                        para.OrderDishId = this.SelectedOrderDish.Id;
                        para.PeopleCount = this.SelectedOrderDish.Quantity;
                        bool checkOuted = mb.CheckOut(para, FairiesCoolerCash.Business.Helper.CardConsume);
                        if (!checkOuted)
                        {
                            Helper.ShowErrorMsg(mb.ErrorMsg);
                            return;
                        }
                        if (this.IsThree)
                        {
                            LocalReport report = new LocalReport();
                            report.ReportPath = GetThreePrintFile(DXInfo.Models.NameCodeType.ThreePrintNoMemmber);
                            NoMemberThreePrintObject threePrintObject = new NoMemberThreePrintObject();
                            threePrintObject.Title = title;
                            threePrintObject.DeskNo = DeskNo;
                            threePrintObject.PeopleCount = this.SelectedOrderDish.Quantity;
                            threePrintObject.Amount = dAmount;
                            threePrintObject.CreateDate = dCreateDate;
                            threePrintObject.Change = dChange;
                            threePrintObject.Cash = dCash;
                            threePrintObject.ButtomTitle = GetButtomTitle(this.DeptType);
                            threePrintObject.Sum = dSum;
                            threePrintObject.DeptName = Dept.DeptName;
                            threePrintObject.Voucher = dVoucher;
                            threePrintObject.FullName = Oper.FullName;
                            threePrintObject.UserName = User.UserName;
                            threePrintObject.PayTypeName = SelectedPayType.Name;
                            threePrintObject.Sn = sn;                                
                            DataTable dt = threePrintObject.ToDataTable();
                            DataTable dt2 = oiex.ToDataTable<DXInfo.Models.InventoryEx>();
                            report.DataSources.Add(
                               new ReportDataSource("DataSet1", dt)
                               );
                            report.DataSources.Add(
                               new ReportDataSource("DataSet2", dt2)
                               );
                            PrintRDLC printRDLC = new PrintRDLC();
                            printRDLC.Run(report);
                        }
                        else
                        {
                            WRNoMemberConsumePrintObject2 po2 = new WRNoMemberConsumePrintObject2(title, oiex, dSum, dAmount, dVoucher, dCash, dChange, SelectedPayType.Name, DeskNo,
                                Oper.FullName, User.UserName, Dept.DeptName, dCreateDate,Dept.Comment);
                            po2.Print();
                        }
                        this.AfterCheckOut();
                        this.ResetCheckOut();
                    }
                    break;
            }
        }
        #endregion
        
        #region 结账撤销
        public void ResetCancelCheckOut() 
        {
            this.IsValid = false;
            this.IsCancelCheckOut = false;
            this.CancelConsume = null;
            if (this.lCancelConsumeList != null)
            {
                if (this.lCancelConsumeList.Count > 0)
                {
                    this.lCancelConsumeList.Clear();
                }
                this.lCancelConsumeList = null;
            }
        }
        private void AfterCancelCheckOut() { }
        private bool CancelCheckOutCanExecute()
        {
            bool isCanExecute = false;
            switch (this.DeptType)
            {
                case DXInfo.Models.DeptType.Sale:
                    if (this.OCInventoryEx != null &&
                        this.OCInventoryEx.Count > 0 &&
                        this.SelectedPayType != null &&
                        this.IsCancelCheckOut &&
                        this.IsAuthorizeCancel() &&
                        this.CancelConsume != null &&
                        this.lCancelConsumeList != null &&
                        this.lCancelConsumeList.Count > 0)
                        isCanExecute = true;
                    break;
                case DXInfo.Models.DeptType.Shop:
                    if (this.SelectedOrderDish != null &&
                        this.SelectedOrderDish.Status == (int)DXInfo.Models.OrderDishStatus.Ordered &&
                        this.SelectedPayType != null &&
                        this.OCInventoryEx != null &&
                        this.OCInventoryEx.Count > 0 &&
                        this.IsCancelCheckOut &&
                        this.CancelConsume != null &&
                        this.lCancelConsumeList != null &&
                        this.lCancelConsumeList.Count > 0 &&
                        this.IsValid)
                        isCanExecute = true;
                    break;
            }
            return isCanExecute;
        }
        private void CancelCheckOutExecute()
        {
            switch (CancelConsume.ConsumeType)
            {
                case (int)DXInfo.Models.ConsumeType.Card:
                    MemberCancelBalance();
                    break;
                case (int)DXInfo.Models.ConsumeType.CardNoMoney:
                    MemberNoMoneyCancelBalance();
                    break;
                case (int)DXInfo.Models.ConsumeType.NoMember:
                    NoMemberCancelBalance();
                    break;
            }            
        }
        public ICommand CancelCheckOut
        {
            get
            {
                return new RelayCommand(CancelCheckOutExecute, CancelCheckOutCanExecute);
            }
        }
        private void MemberCancelBalance()
        {
            ObservableCollection<DXInfo.Models.InventoryEx> oiex = new ObservableCollection<DXInfo.Models.InventoryEx>();
            if (!GetOCInvEx(ref oiex)) return;
            DateTime dCreateDate = DateTime.Now;
            decimal dSum = CancelConsume.Sum;
            decimal dDiscount = CancelConsume.Discount;
            decimal dVoucher = CancelConsume.Voucher;
            decimal dAmount = CancelConsume.Amount;
            decimal dLastBalance = this.CardBalance.Value;
            DXInfo.Business.MemberManageFacade mb = new DXInfo.Business.MemberManageFacade(Uow);
            bool cancelCheckOuted;
            decimal dBalance = dLastBalance + dAmount;
            string title = GetTitle() + "(撤销)";
            decimal dQuantity = oiex.Sum(s => s.Quantity);

            DXInfo.Business.CancelCheckoutParaObj para = new DXInfo.Business.CancelCheckoutParaObj();
            para.IsCard = true;
            para.DeptId = this.Dept.DeptId;
            para.DeptName = this.Dept.DeptName;
            para.UserId = this.Oper.UserId;
            para.UserName = this.User.UserName;
            para.FullName = this.Oper.FullName;
            para.CardId = this.Card.Id;
            para.CardNo = this.Card.CardNo;
            para.LastBalance = dLastBalance;
            para.Balance = dBalance;
            para.CreateDate = dCreateDate;
            para.IsVirtual = this.CardType.IsVirtual;
            para.IsCardLevelAuto = this.IsCardLevelAuto;
            para.ConsumeId = this.CancelConsume.Id;
            para.ConsumeType = (int)DXInfo.Models.ConsumeType.Card;
            para.BillType = DXInfo.Models.BillType.CardCancelCheckOut.ToString();
            para.Amount = dAmount;

            switch (this.DeptType)
            {
                case DXInfo.Models.DeptType.Sale:                    
                    cancelCheckOuted = mb.CancelCheckOut(para,FairiesCoolerCash.Business.Helper.CardCancelConsume);
                    if (!cancelCheckOuted)
                    {
                        Helper.ShowErrorMsg(mb.ErrorMsg);
                        return;
                    }
                    if (this.IsTicket2)
                    {
                        MemberConsumePrintObject2 po2 = new MemberConsumePrintObject2(title, oiex, lCardDonateInventoryEx,
                            this.Card.CardNo, this.Member.MemberName, dLastBalance,
                            dBalance, dSum, dDiscount, dAmount, dVoucher, DeskNo,
                             this.Oper.FullName, this.User.UserName, this.Dept.DeptName, dCreateDate, this.IsCupType);
                        po2.Print();
                    }

                    if (this.IsTicket1)
                    {
                        MemberConsumePrintObject po = new MemberConsumePrintObject(oiex, lCardDonateInventoryEx, DeskNo, Dept.DeptName, dCreateDate, dSum, dQuantity, this.IsCupType);
                        ;
                        po.Print();
                    }

                    if (this.IsStickerPrint)
                    {
                        try
                        {
                            mb.StickerBill(oiex, DeskNo,dCreateDate,this.Dept.DeptName);
                            ClientCommon.PrintSticker(oiex, DeskNo, dCreateDate, Dept.DeptName);
                        }
                        catch (Exception ex)
                        {
                            Helper.ShowErrorMsg(ex.Message);
                            Helper.HandelException(ex);
                        }

                    }
                    if (this.IsThree)
                    {
                        LocalReport report = new LocalReport();
                        report.ReportPath = GetThreePrintFile(DXInfo.Models.NameCodeType.SaleThreePrintMemmber);
                        NoMemberThreePrintObject threePrintObject = new NoMemberThreePrintObject();
                        threePrintObject.Title = title;
                        threePrintObject.Amount = dAmount;
                        threePrintObject.CreateDate = dCreateDate;
                        threePrintObject.ButtomTitle = GetButtomTitle(this.DeptType);
                        threePrintObject.Sum = dSum;
                        threePrintObject.DeptName = Dept.DeptName;
                        threePrintObject.Voucher = dVoucher;
                        threePrintObject.FullName = Oper.FullName;
                        threePrintObject.UserName = User.UserName;
                        threePrintObject.PayTypeName = SelectedPayType.Name;

                        threePrintObject.CardNo = this.Card.CardNo;
                        threePrintObject.MemberName = this.Member.MemberName;
                        threePrintObject.Discount = dDiscount;
                        threePrintObject.Balance = dBalance;
                        threePrintObject.LastBalance = dLastBalance;
                        threePrintObject.Sn = CancelConsume.Sn;
                        DataTable dt = threePrintObject.ToDataTable();
                        DataTable dt2 = oiex.ToDataTable<DXInfo.Models.InventoryEx>();
                        DataTable dt3 = lCardDonateInventoryEx.ToDataTable<DXInfo.Models.CardDonateInventoryEx>();
                        this.SetImageFilePath(dt2);
                        report.DataSources.Add(
                           new ReportDataSource("DataSet1", dt)
                           );
                        report.DataSources.Add(
                           new ReportDataSource("DataSet2", dt2)
                           );
                        report.DataSources.Add(
                           new ReportDataSource("DataSet3", dt3)
                           );
                        PrintRDLC printRDLC = new PrintRDLC();
                        printRDLC.Run(report);
                    }
                    if (MessageBox.Show("是否重新结账？", "结账撤销", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                    {
                        this.AfterCancelCheckOut();
                        this.ResetSwipingCard();
                        this.ResetCheckOut();                        
                    }
                    this.ResetCancelCheckOut();
                    break;
            }
        }
        private void MemberNoMoneyCancelBalance()
        {
            ObservableCollection<DXInfo.Models.InventoryEx> oiex = new ObservableCollection<DXInfo.Models.InventoryEx>();
            if (!GetOCInvEx(ref oiex)) return;
            decimal dVoucher = CancelConsume.Voucher;
            decimal dDiscount = CancelConsume.Discount;
            decimal dSum = CancelConsume.Sum;
            string title = GetTitle() + "(撤销)";
            decimal dAmount = CancelConsume.Amount;

            decimal dQuantity = oiex.Sum(s => s.Quantity);

            decimal dLastBalance = this.CardBalance.Value;
            decimal dBalance = dLastBalance;
            decimal dChange = CancelConsume.Change;
            decimal dCash = CancelConsume.Cash;
            DXInfo.Business.MemberManageFacade mb = new DXInfo.Business.MemberManageFacade(Uow);
            List<DXInfo.Models.CardDonateInventoryEx> lCardDonateInventoryEx = GetCardDonateInventoryEx();
            DateTime dCreateDate = DateTime.Now;

            DXInfo.Business.CancelCheckoutParaObj para = new DXInfo.Business.CancelCheckoutParaObj();
            para.IsCard = true;
            para.DeptId = this.Dept.DeptId;
            para.DeptName = this.Dept.DeptName;
            para.UserId = this.Oper.UserId;
            para.UserName = this.User.UserName;
            para.FullName = this.Oper.FullName;
            para.CardId = this.Card.Id;
            para.CardNo = this.Card.CardNo;
            para.LastBalance = dLastBalance;
            para.Balance = dBalance;
            para.CreateDate = dCreateDate;
            para.IsVirtual = this.CardType.IsVirtual;
            para.IsCardLevelAuto = this.IsCardLevelAuto;
            para.ConsumeId = this.CancelConsume.Id;
            para.ConsumeType = (int)DXInfo.Models.ConsumeType.CardNoMoney;
            para.BillType = DXInfo.Models.BillType.CardNoMoneyCancelCheckOut.ToString();
            para.Amount = dAmount;
            switch (this.DeptType)
            {
                case DXInfo.Models.DeptType.Sale:
                    bool checkOuted = mb.CancelCheckOut(para,FairiesCoolerCash.Business.Helper.CardCancelConsume);
                    if (!checkOuted)
                    {
                        Helper.ShowErrorMsg(mb.ErrorMsg);
                        return;
                    }
                    if (this.IsTicket2)
                    {
                        MemberConsumePrintObject3 po3 = new MemberConsumePrintObject3(title, oiex, lCardDonateInventoryEx, this.Card.CardNo, this.Member.MemberName, dCash,
                            dChange, dSum, dDiscount, dAmount, dVoucher, SelectedPayType.Name, DeskNo,
                             this.Oper.FullName, this.User.UserName, this.Dept.DeptName, dCreateDate, this.IsCupType);
                        po3.Print();
                    }

                    if (this.IsTicket1)
                    {
                        MemberConsumePrintObject po = new MemberConsumePrintObject(oiex, lCardDonateInventoryEx, DeskNo, Dept.DeptName, dCreateDate, dSum, dQuantity, this.IsCupType);
                        ;
                        po.Print();
                    }

                    if (this.IsStickerPrint)
                    {
                        try
                        {
                            mb.StickerBill(oiex, DeskNo,dCreateDate,this.Dept.DeptName);
                            ClientCommon.PrintSticker(oiex, DeskNo, dCreateDate, Dept.DeptName);
                        }
                        catch (Exception ex)
                        {
                            Helper.ShowErrorMsg(ex.Message);
                            Helper.HandelException(ex);
                        }
                    }
                    if (this.IsThree)
                    {
                        LocalReport report = new LocalReport();
                        report.ReportPath = GetThreePrintFile(DXInfo.Models.NameCodeType.SaleThreePrintMemmberNoMoney);
                        NoMemberThreePrintObject threePrintObject = new NoMemberThreePrintObject();
                        threePrintObject.Title = title;
                        threePrintObject.Amount = dAmount;
                        threePrintObject.CreateDate = dCreateDate;
                        threePrintObject.Change = dChange;
                        threePrintObject.Cash = dCash;
                        threePrintObject.ButtomTitle = GetButtomTitle(this.DeptType);
                        threePrintObject.Sum = dSum;
                        threePrintObject.DeptName = Dept.DeptName;
                        threePrintObject.Voucher = dVoucher;
                        threePrintObject.FullName = Oper.FullName;
                        threePrintObject.UserName = User.UserName;
                        threePrintObject.PayTypeName = SelectedPayType.Name;

                        threePrintObject.CardNo = this.Card.CardNo;
                        threePrintObject.MemberName = this.Member.MemberName;
                        threePrintObject.Discount = dDiscount;
                        threePrintObject.Sn = CancelConsume.Sn;
                        DataTable dt = threePrintObject.ToDataTable();
                        DataTable dt2 = oiex.ToDataTable<DXInfo.Models.InventoryEx>();
                        DataTable dt3 = lCardDonateInventoryEx.ToDataTable<DXInfo.Models.CardDonateInventoryEx>();
                        this.SetImageFilePath(dt2);
                        report.DataSources.Add(
                           new ReportDataSource("DataSet1", dt)
                           );
                        report.DataSources.Add(
                           new ReportDataSource("DataSet2", dt2)
                           );
                        report.DataSources.Add(
                           new ReportDataSource("DataSet3", dt3)
                           );
                        PrintRDLC printRDLC = new PrintRDLC();
                        printRDLC.Run(report);
                    }
                    if (MessageBox.Show("是否重新结账？", "结账撤销", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                    {
                        this.AfterCancelCheckOut();
                        this.ResetSwipingCard();
                        this.ResetCheckOut();                        
                    }
                    this.ResetCancelCheckOut();
                    break;
            }
        }
        private void NoMemberCancelBalance()
        {
            ObservableCollection<DXInfo.Models.InventoryEx> oiex = new ObservableCollection<DXInfo.Models.InventoryEx>();
            if (!GetOCInvEx(ref oiex)) return;
            decimal dVoucher = CancelConsume.Voucher;
            decimal dSum = CancelConsume.Sum;
            string title = GetTitle() + "(撤销)";
            DateTime dCreateDate = DateTime.Now;
            decimal dQuantity = oiex.Sum(s => s.Quantity);
            decimal dAmount = CancelConsume.Amount;
            decimal dChange = CancelConsume.Change;
            decimal dCash = CancelConsume.Cash;
            DXInfo.Business.MemberManageFacade mb = new DXInfo.Business.MemberManageFacade(Uow);
            DXInfo.Business.CancelCheckoutParaObj para = new DXInfo.Business.CancelCheckoutParaObj();
            para.IsCard = false;
            para.DeptId = this.Dept.DeptId;
            para.DeptName = this.Dept.DeptName;
            para.UserId = this.Oper.UserId;
            para.UserName = this.User.UserName;
            para.FullName = this.Oper.FullName;
            //para.CardId = this.Card.Id;
            //para.CardNo = this.Card.CardNo;
            //para.LastBalance = dLastBalance;
            //para.Balance = dBalance;
            para.CreateDate = dCreateDate;
            //para.IsVirtual = this.CardType.IsVirtual;
            para.IsCardLevelAuto = this.IsCardLevelAuto;
            para.ConsumeId = this.CancelConsume.Id;
            para.ConsumeType = (int)DXInfo.Models.ConsumeType.NoMember;
            para.BillType = DXInfo.Models.BillType.NoMemberCancelCheckOut.ToString();
            para.Amount = dAmount;
            switch (this.DeptType)
            {
                case DXInfo.Models.DeptType.Sale:
                    bool checkOuted = mb.CancelCheckOut(para,FairiesCoolerCash.Business.Helper.CardCancelConsume);
                    if (!checkOuted)
                    {
                        Helper.ShowErrorMsg(mb.ErrorMsg);
                        return;
                    }
                    if (this.IsTicket3)
                    {
                        NoMemberConsumePrintObject2 po2 = new NoMemberConsumePrintObject2(title, oiex, dSum, dAmount, dVoucher, dCash, dChange, SelectedPayType.Name, DeskNo,
                            Oper.FullName, User.UserName, Dept.DeptName, dCreateDate,this.IsCupType);
                        po2.Print();
                    }
                    if (this.IsTicket1)
                    {
                        NoMemberConsumePrintObject po = new NoMemberConsumePrintObject(oiex, DeskNo, Dept.DeptName, dCreateDate, dSum, dQuantity, this.IsCupType);
                        ;
                        po.Print();
                    }

                    if (this.IsStickerPrint)
                    {
                        try
                        {
                            mb.StickerBill(oiex, DeskNo,dCreateDate,Dept.DeptName);
                            ClientCommon.PrintSticker(oiex, DeskNo, dCreateDate, Dept.DeptName);
                        }
                        catch (Exception ex)
                        {
                            Helper.ShowErrorMsg(ex.Message);
                            Helper.HandelException(ex);
                        }
                    }
                    if (this.IsThree)
                    {
                        LocalReport report = new LocalReport();
                        report.ReportPath = GetThreePrintFile(DXInfo.Models.NameCodeType.SaleThreePrintNoMemmber);
                        NoMemberThreePrintObject threePrintObject = new NoMemberThreePrintObject();
                        threePrintObject.Title = title;
                        threePrintObject.Amount = dAmount;
                        threePrintObject.CreateDate = dCreateDate;
                        threePrintObject.Change = dChange;
                        threePrintObject.Cash = dCash;
                        threePrintObject.ButtomTitle = GetButtomTitle(this.DeptType);
                        threePrintObject.Sum = dSum;
                        threePrintObject.DeptName = Dept.DeptName;
                        threePrintObject.Voucher = dVoucher;
                        threePrintObject.FullName = Oper.FullName;
                        threePrintObject.UserName = User.UserName;
                        threePrintObject.PayTypeName = SelectedPayType.Name;
                        threePrintObject.Sn = CancelConsume.Sn;
                        DataTable dt = threePrintObject.ToDataTable();
                        DataTable dt2 = oiex.ToDataTable<DXInfo.Models.InventoryEx>();
                        this.SetImageFilePath(dt2);
                        report.DataSources.Add(
                           new ReportDataSource("DataSet1", dt)
                           );
                        report.DataSources.Add(
                           new ReportDataSource("DataSet2", dt2)
                           );
                        PrintRDLC printRDLC = new PrintRDLC();
                        printRDLC.Run(report);
                    }
                    if (MessageBox.Show("是否重新结账？", "结账撤销", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                    {
                        this.AfterCancelCheckOut();
                        this.ResetCheckOut();                        
                    }
                    this.ResetCancelCheckOut();
                    break;
            }
        }
        #endregion

        #region dispose
        public override void Cleanup()
        {
            base.Cleanup();
            Messenger.Default.Unregister<ViewCollectionViewSourceMessageToken>(this);
            Messenger.Default.Unregister<DataGridMessageToken>(this);
        }
        #endregion
    }
}
