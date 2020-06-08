using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using DXInfo.Models;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using System.Threading;
using System.Windows;
using FairiesCoolerCash.Business;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Data;
using System.Collections.Specialized;
using System.ComponentModel;
using AutoMapper;
namespace FairiesCoolerCash.ViewModel
{
    public class PointsExchangeViewModel : BusinessViewModelBase
    {
        private readonly IMapper mapper;
        public PointsExchangeViewModel(IFairiesMemberManageUow uow, IMapper mapper) :base(uow,mapper,new List<string>())
        {
            this.mapper = mapper;
            Messenger.Default.Register<ViewCollectionViewSourceMessageToken>(this, Handle_ViewCollectionViewSourceMessageToken);
            Messenger.Default.Register<DataGridMessageToken>(this, Handle_DataGridMessageToken);
        }
        private void Handle_ViewCollectionViewSourceMessageToken(ViewCollectionViewSourceMessageToken token)
        {
            CVS_OCInventory = token.CVS;
        }
        private void Handle_DataGridMessageToken(DataGridMessageToken token)
        {
            this.MyDataGrid = token.MyDataGrid;
        }
        private CollectionViewSource CVS_OCInventory { get; set; }
        public override void LoadData()
        {
            this.SetOCInventoryCategory();
            this.SetOCInventory();
            SetOCInventoryEx();
            this.SetlTasteEx();
            this.SetlCupType();
            this.SetlPayType();
            //this.SetlPayTypeOfPutCard();
            //this.SetlPayTypeCard();
            this.SetlCardLevel();
            this.SetlCardType();
            this.IsCard = false;
            this.IsMoney = false;
        }

        
        private void SetOCInventoryCategory()
        {
            var q = (from ic in this.Uow.InventoryCategory.GetAll()
                       join idt in Uow.CategoryDepts.GetAll() on ic.Id equals idt.Category
                       orderby ic.Code
                       where idt.Dept == this.Dept.DeptId
                       select ic).ToList();
            this.OCInventoryCategory = new ObservableCollection<InventoryCategory>(q);
        }
        protected override void AfterSelectInventoryCategory()
        {
            base.AfterSelectInventoryCategory();
            if (this.SelectedInventoryCategory != null)
            {
                CVS_OCInventory.Filter += new FilterEventHandler(FilterByCategory);
            }
        }
        private void FilterByCategory(object sender, FilterEventArgs e)
        {
            var src = e.Item as DXInfo.Models.Inventory;
            if (src == null)
                e.Accepted = false;
            else if (this.SelectedInventoryCategory != null && this.SelectedInventoryCategory.Id != src.Category)
                e.Accepted = false;
        }

        private void SetOCInventory()
        {
            var invs = from d in Uow.Inventory.GetAll()
                       join d1 in Uow.InvDepts.GetAll() on d.Id equals d1.Inv into dd1
                       from dd1s in dd1.DefaultIfEmpty()
                       join d2 in Uow.InventoryDeptPrice.GetAll().Where(w => w.DeptId == this.Dept.DeptId) on d.Id equals d2.InvId into dd2
                       from dd2s in dd2.DefaultIfEmpty()
                       orderby d.Code
                       where dd1s.Dept == this.Dept.DeptId
                               && !d.IsInvalid
                       select new
                       {
                           Id = d.Id,
                           Code = d.Code,
                           Name = d.Name,
                           Category = d.Category,
                           InvType = d.InvType,
                           SalePoint = (this.Dept.IsDeptPrice && dd2s != null && dd2s.SalePoint > 0) ? dd2s.SalePoint : d.SalePoint,
                           SalePoint0 = (this.Dept.IsDeptPrice && dd2s != null && dd2s.SalePoint0 > 0) ? dd2s.SalePoint0 : d.SalePoint0,
                           SalePoint1 = (this.Dept.IsDeptPrice && dd2s != null && dd2s.SalePoint1 > 0) ? dd2s.SalePoint1 : d.SalePoint1,
                           SalePoint2 = (this.Dept.IsDeptPrice && dd2s != null && dd2s.SalePoint2 > 0) ? dd2s.SalePoint2 : d.SalePoint2,
                           SalePrice = (this.Dept.IsDeptPrice && dd2s != null && dd2s.SalePrice > 0) ? dd2s.SalePrice : d.SalePrice,
                           SalePrice0 = (this.Dept.IsDeptPrice && dd2s != null && dd2s.SalePrice0 > 0) ? dd2s.SalePrice0 : d.SalePrice0,
                           SalePrice1 = (this.Dept.IsDeptPrice && dd2s != null && dd2s.SalePrice1 > 0) ? dd2s.SalePrice1 : d.SalePrice1,
                           SalePrice2 = (this.Dept.IsDeptPrice && dd2s != null && dd2s.SalePrice2 > 0) ? dd2s.SalePrice2 : d.SalePrice2,
                       };
            if (this.SelectedInventoryCategory != null)
            {
                invs = invs.Where(w => w.Category == this.SelectedInventoryCategory.Id);
            }
            var lInv = invs.ToList().Select(s => new DXInfo.Models.Inventory()
            {
                Id = s.Id,
                Code = s.Code,
                Name = s.Name,
                InvType = s.InvType,
                Category = s.Category,
                SalePoint = s.SalePoint,
                SalePoint0 = s.SalePoint0,
                SalePoint1 = s.SalePoint1,
                SalePoint2 = s.SalePoint2,
                SalePrice = s.SalePrice,
                SalePrice0 = s.SalePrice0,
                SalePrice1 = s.SalePrice1,
                SalePrice2 = s.SalePrice2,
            });
            this.OCInventory = new ObservableCollection<DXInfo.Models.Inventory>(lInv);
        }

        
        private void SetSumData()
        {
            if (OCInventoryEx != null)
            {
                this.SumQuantity = OCInventoryEx.Sum(s => s.Quantity);
                this.SumAmount = OCInventoryEx.Sum(s => s.CurrentAmount);
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


        private void CreateInventoryEx()
        {
            if (this.SelectedInventory != null)
            {
                //分2种创建冷饮店的、西餐厅的
                InventoryEx inventoryEx = mapper.Map<DXInfo.Models.InventoryEx>(this.SelectedInventory);
                inventoryEx.lTasteEx = this.lTasteEx.Clone() as DXInfo.Models.TasteExList;
                inventoryEx.dSalePrice = this.GetdSalePrice(this.SelectedInventory);
                inventoryEx.dSalePoint = this.GetdSalePoint(this.SelectedInventory);
                inventoryEx.lCupType = this.lCupType;
                inventoryEx.CupType = inventoryEx.lCupType.Find(f => f.Id == (int)DXInfo.Models.CupType.Standard);
                inventoryEx.Quantity = 1;
                inventoryEx.IsDiscount = true;
                this.OCInventoryEx.Add(inventoryEx);

                CardConsumeSetWindow csw = new CardConsumeSetWindow(Uow,mapper, inventoryEx);
                csw.ShowDialog();
            }
        }
        protected override void AfterSelectInventory()
        {
            if (this.SelectedInventory != null)
            {
                CreateInventoryEx();
            }
        }
        private void SetOCInventoryEx()
        {
            if (this.OCInventoryEx == null)
            {
                this.OCInventoryEx = new ObservableCollection<InventoryEx>();
            }
            this.OCInventoryEx.CollectionChanged += new NotifyCollectionChangedEventHandler(OCInventoryEx_CollectionChanged);
        }
        protected override void AfterSelectInventoryEx()
        {
            if (SelectedInventoryEx != null)
            {
                if (!(this.MyDataGrid.CurrentColumn.Header.ToString() == "数量" || this.MyDataGrid.CurrentColumn.Header.ToString() == "撤销"))
                {
                    CardConsumeSetWindow csw = new CardConsumeSetWindow(Uow,mapper, SelectedInventoryEx);
                    csw.ShowDialog();
                }
            }
        }
        protected override void AfterSelectCupType()
        {
            base.AfterSelectCupType();
            if (this.SelectedInventoryEx != null)
            {
                if (this.SelectedCupType != null)
                {
                    //MyEnum ct = this.SelectedCupType;
                    //var inv = Uow.Inventory.GetById(this.SelectedInventoryEx.Id);
                    //if (inv != null)
                    //{
                    //int cuptype = ct.Id;
                    this.SelectedInventoryEx.CupType = this.SelectedCupType;//ct.Id;
                    this.SelectedInventoryEx.SalePoint = this.SelectedInventoryEx.dSalePoint[this.SelectedCupType.Id];//cuptype == -1 ? inv.SalePoint : cuptype == 0 ? inv.SalePoint0 : cuptype == 1 ? inv.SalePoint1 : inv.SalePoint2;
                    //this.SelectedInventoryEx.Amount = this.SelectedInventoryEx.SalePoint * this.SelectedInventoryEx.Quantity;
                    //}
                }
            }
        }
        private void checkOut()
        {
            //结账
            if (this.Card == null)
            {
                MessageBox.Show("请刷卡");
                return;
            }
            if (this.OCInventoryEx == null || this.OCInventoryEx.Count == 0)
            {
                MessageBox.Show("请选择商品");
                return;
            }
            if (this.OCInventoryEx != null && this.OCInventoryEx.Count > 0)
            {
                ObservableCollection<InventoryEx> lsi = this.OCInventoryEx;
                decimal dAmount = lsi.Sum(s => s.CurrentPoint);
                if (dAmount > this.Points)
                {
                    MessageBox.Show("积分不足");
                    return;
                }
                var lselInv = lsi.Select(s => new
                {
                    s.Id,
                    s.Code,
                    s.Name,
                    s.SalePoint,
                    s.Quantity,
                    s.Amount,
                    CupType = s.CupType.Name,//Id == -1 ? "标准杯" : s.CupType == 0 ? "大杯" : s.CupType == 1 ? "中杯" : "小杯",
                    Cup = s.CupType.Id,
                    lTastes = s.lTasteEx.Where(w => w.IsSelected).ToList(),
                    Tastes = s.lTasteEx.Where(w => w.IsSelected == true).Count() == 0 ? "" : s.lTasteEx.Where(w => w.IsSelected == true).Select(l => l.Name).Aggregate((total, next) => (total + "," + next))
                    ,
                    ConsumeTastes = s.lTasteEx.Where(w => w.IsSelected == true)
                });
                var ctx = new
                {
                    Id = this.Card.Id,
                    CardNo = this.Card.CardNo,
                    MemberName = this.Member.MemberName,
                    UserId = this.Oper.UserId,
                    FullName = this.Oper.FullName,
                    DeptId = this.Oper.DeptId.Value,
                    DeptName = this.Dept.DeptName,
                    Amount = dAmount,
                    LastBalance = this.Points,
                    Balance = this.Points - dAmount,
                    CreateDate = DateTime.Now,
                    lSelInv = lselInv
                };
                PointsExchangeWindow cw = new PointsExchangeWindow(Uow, ctx);
                if (cw.ShowDialog().GetValueOrDefault())
                {
                    MessageBox.Show("会员积分兑换成功");
                    this.OCInventoryEx = new ObservableCollection<InventoryEx>();
                    this.Card = null;
                    this.Member = null;
                    this.SelectedInventoryEx = null;
                    this.Points = 0;
                    this.CardBalance = 0;
                    this.CardLevel = null;
                    this.CardType = null;
                }
            }
        }

        public ICommand CheckOut
        {
            get
            {
                return new RelayCommand(checkOut);
            }
        }

        private void DeleteInventoryExExecute()
        {
            //删除记录
            if (this.SelectedInventoryEx != null)
            {
                //InventoryEx selInv = GridSelected.SelectedItem as InventoryEx;
                //ObservableCollection<InventoryEx> lsi = GridSelected.ItemsSource as ObservableCollection<InventoryEx>;
                this.OCInventoryEx.Remove(this.SelectedInventoryEx);
            }
        }
        public ICommand DeleteInventoryEx
        {
            get
            {
                return new RelayCommand(DeleteInventoryExExecute);
            }
        }
    }
}
