using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using DXInfo.Data.Contracts;
using System.Windows;
using System.Collections.ObjectModel;
using DXInfo.Models;
using FairiesCoolerCash.Business;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Collections;
using System.Transactions;
using System.Windows.Data;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
//using System.Data.Objects.SqlClient;
//using System.Data.Objects;
using System.Windows.Threading;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace FairiesCoolerCash.ViewModel
{
    public class StockDeskManageViewModel : DeskManageViewModel
    {
        public StockDeskManageViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
        }
        public override void SetCurrentType()
        {
            this.CurrentCategoryType = CategoryType.StockManage;
            this.CurrentInvType = InvType.StockManage;
            this.DeptType = DeptType.Shop;
            this.IsStock = true;
        }
        //public override void AfterCheckOut()
        //{
        //    base.AfterCheckOut();
        //    AddRetailOutStock();
        //}
    }
    public class DeskManageViewModel : ConsumeViewModelBase
    {
        #region 属性
        public int ScrollBarHeight { get; set; }
        public int ScrollBarWidth { get; set; }
        #endregion

        #region 字段
        private DXInfo.Restaurant.DeskManageFacade DeskManageFacade;
        private DispatcherTimer RefreshDeskTimer;
        #endregion

        #region 构造
        public DeskManageViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
            this.DeskManageFacade = new DXInfo.Restaurant.DeskManageFacade(uow,Dept.DeptId,User.UserId);

            this.ScrollBarHeight = 36;
            this.ScrollBarWidth = 60;

            this.SetlRoom();
            this.SetlDeskEx();
            this.SetOCDesk(null);
            this.OrderBookVisibility = Visibility.Collapsed;
            this.OrderMenuVisibility = Visibility.Collapsed;
            this.SetTimer();            
        }

        public override void SetCurrentType()
        {
            this.DeptType = DeptType.Shop;
            this.CurrentCategoryType = CategoryType.WesternRestaurant;
            this.CurrentInvType = InvType.WesternRestaurant;
        }
                
        #endregion

        #region 刷新
        object lockObject = new object();
        private void RefreshDeskTimerCallback(object obj, EventArgs arg)
        {
            lock (lockObject)
            {
                this.RefreshDeskExecute();
            }
        }
        private void SetTimer()
        {
            this.RefreshDeskTimer = new DispatcherTimer();
            this.RefreshDeskTimer.Interval = TimeSpan.FromSeconds(60.0);
            this.RefreshDeskTimer.Tick += new EventHandler(this.RefreshDeskTimerCallback);
            this.RefreshDeskTimer.Start();

        }
        #endregion

        #region 打印
        private void Print(string printerName, ObservableCollection<InventoryEx> confirmmenu, string msg, DateTime dtOperDate)
        {
            if (confirmmenu.Count > 0)
            {
                decimal dSum = confirmmenu.Sum(s => s.Amount);
                decimal dQuantity = confirmmenu.Sum(s => s.Quantity);

                OrderPrintObject opo = new OrderPrintObject(confirmmenu, msg,
                        this.Dept.DeptName, dtOperDate, dSum, dQuantity,
                        printerName);
                opo.Print();

                //OrderPrintObject opo1 = new OrderPrintObject(confirmmenu, msg,
                //    this.MyIdentity.dept.DeptName, dtOperDate, dSum, dQuantity);
                //opo1.Print();
            }
        }
        private void Print(ObservableCollection<InventoryEx> confirmmenu, string msg, DateTime dtOperDate)
        {
            if (confirmmenu.Count > 0)
            {
                decimal dSum = confirmmenu.Sum(s => s.Amount);
                decimal dQuantity = confirmmenu.Sum(s => s.Quantity);

                //OrderPrintObject opo = new OrderPrintObject(confirmmenu, msg,
                //        this.MyIdentity.dept.DeptName, dtOperDate, dSum, dQuantity,
                //        printerName);
                //opo.Print();

                OrderPrintObject opo1 = new OrderPrintObject(confirmmenu, msg,
                    this.Dept.DeptName, dtOperDate, dSum, dQuantity);
                opo1.Print();
            }
        }
        private void AddOrderMenuPrint(Guid orderId, ref Hashtable htOtherPrint, ref Hashtable htLocalPrint, DXInfo.Models.PrintType pt)
        {
            string deskCodes = this.DeskManageFacade.GetOrderDeskCodes(Uow, orderId);
            List<OrderMenus> lom = (from d in Uow.OrderMenus.GetAll().Where(w => w.OrderId == orderId &&
                (w.Status == (int)DXInfo.Models.OrderMenuStatus.Hurry ||
                w.Status == (int)DXInfo.Models.OrderMenuStatus.Lack ||
                w.Status == (int)DXInfo.Models.OrderMenuStatus.Make ||
                w.Status == (int)DXInfo.Models.OrderMenuStatus.Order))
                                    select d).ToList();

            foreach (DXInfo.Models.OrderMenus om in lom)
            {
                DXInfo.Models.Inventory inv = Uow.Inventory.GetById(g => g.Id == om.InventoryId);
                DXInfo.Models.InventoryEx iex = Mapper.Map<DXInfo.Models.InventoryEx>(om);
                iex.Code = inv.Code;
                iex.Name = inv.Name;
                //iex.Amount = om.Price * om.Quantity;
                iex.SalePrice = om.Price;
                //iex.Quantity = om.Quantity;
                this.DeskManageFacade.AddPrint(ref htOtherPrint, ref htLocalPrint, iex, pt);
            }
        }
        private void PrintOrder_Print(Hashtable ht, string deskCodes, string oldDeskNo, string newDeskNo, DateTime dtOperDate)
        {
            string printerName = string.Empty;
            string msg = string.Empty;
            ObservableCollection<InventoryEx> confirmmenu;
            foreach (DictionaryEntry de in ht)
            {
                DXInfo.Models.PrintType pt = (DXInfo.Models.PrintType)de.Key;
                switch (pt)
                {
                    case PrintType.Add:
                        msg = deskCodes + "(加单)";
                        break;
                    case PrintType.Sub:
                        msg = deskCodes + "(减单)";
                        break;
                    case PrintType.Menu:
                        msg = deskCodes + "(加菜)";
                        break;
                    case PrintType.Taste:
                        msg = deskCodes + "(口味变化)";
                        break;
                    case PrintType.CancelOrder:
                        msg = deskCodes + "(退单)";
                        break;
                    case PrintType.CancelOpen:
                        msg = deskCodes + "(撤销)";
                        break;
                    case PrintType.Exchange:
                        msg = oldDeskNo + "(转)" + newDeskNo;
                        break;
                    case PrintType.Repeat:
                        msg = deskCodes + "(重打)";
                        break;
                    case PrintType.Order:
                        msg = deskCodes + "(下单)";
                        break;
                }
                Hashtable ht1 = de.Value as Hashtable;
                foreach (DictionaryEntry de1 in ht1)
                {
                    confirmmenu = de1.Value as ObservableCollection<InventoryEx>;
                    printerName = de1.Key.ToString();
                    if (printerName == "LocalPrint")
                    {
                        Print(confirmmenu, msg, dtOperDate);
                    }
                    else
                    {
                        string[] printers = printerName.Split(',');
                        foreach (string printer in printers)
                        {
                            Print(printer, confirmmenu, msg, dtOperDate);
                        }
                        //Print(printerName, confirmmenu, msg, dtOperDate);
                    }

                }
            }
        }
        private void PrintOrder(Guid? orderDishId,
            Hashtable htOtherPrint, Hashtable htLocalPrint,
            string oldDeskNo, string newDeskNo,
            DateTime dtOperDate)
        {
            //加单、减单、加菜、退单、撤销、口味变化
            string deskCodes = "";
            if (orderDishId.HasValue)
            {
                deskCodes = this.DeskManageFacade.GetOrderDeskCodes(Uow, this.SelectedOrderDish.Id);
            }

            if (htOtherPrint != null && htOtherPrint.Count > 0)
            {
                PrintOrder_Print(htOtherPrint, deskCodes, oldDeskNo, newDeskNo, dtOperDate);
            }
            if (!this.IsThree)
            {
                if (htLocalPrint != null && htLocalPrint.Count > 0)
                {
                    PrintOrder_Print(htLocalPrint, deskCodes, oldDeskNo, newDeskNo, dtOperDate);
                }
            }
        }
        private void PrintOrder(Hashtable htOtherPrint, Hashtable htLocalPrint,
            string oldDeskNo, string newDeskNo,
            DateTime dtOperDate, string deskCodes)
        {
            //加单、减单、加菜、退单、撤销、口味变化
            if (htOtherPrint != null && htOtherPrint.Count > 0)
            {
                PrintOrder_Print(htOtherPrint, deskCodes, oldDeskNo, newDeskNo, dtOperDate);
            }
            if (htLocalPrint != null && htLocalPrint.Count > 0)
            {
                PrintOrder_Print(htLocalPrint, deskCodes, oldDeskNo, newDeskNo, dtOperDate);
            }
        }
        #endregion

        #region 存货与分类
        private bool popupPackageWin(Guid packageId, int packageSn, out List<DXInfo.Models.InventoryEx> lPackageDeskOrderMenu)
        {            
            lPackageDeskOrderMenu = new List<DXInfo.Models.InventoryEx>();
            if (this.SelectedInventory == null && this.SelectedOrderDish == null)
                return false;

            var packages = (from d1 in Uow.Packages.GetAll()
                            join d2 in Uow.Inventory.GetAll() on d1.InventoryId equals d2.Id into dd1
                            from dd1s in dd1.DefaultIfEmpty()
                            where d1.PackageId == packageId
                            select new
                            {
                                Id = d1.InventoryId,
                                dd1s.Code,
                                dd1s.Name,
                                SalePrice = d1.Price,
                                d1.OptionalGroup,
                                d1.IsOptional,
                                d1.Quantity
                            }).ToList();
            var lRequiredPackage = packages.Where(w => !w.IsOptional).ToList();
            foreach (var inv in lRequiredPackage)
            {
                DXInfo.Models.InventoryEx selInv = new DXInfo.Models.InventoryEx();
                selInv.Id = inv.Id;
                selInv.Code = inv.Code;
                selInv.Name = inv.Name;
                selInv.SalePrice = inv.SalePrice;
                selInv.Quantity = inv.Quantity>0?inv.Quantity:1;//1;
                selInv.Comment = "TC";
                selInv.IsPackage = true;
                selInv.PackageId = packageId;
                selInv.PackageSn = packageSn;
                selInv.OrderId = this.SelectedOrderDish.Id;
                lPackageDeskOrderMenu.Add(selInv);
            }
            var lOptionalPackage = packages.Where(w => w.IsOptional).ToList();
            if (lOptionalPackage.Count > 0)
            {
                PackageOptionalWindow pw = new PackageOptionalWindow();
                pw.DataContext = lOptionalPackage;
                if (!pw.ShowDialog().GetValueOrDefault()) return false;

                var optionalCount = lOptionalPackage.Select(s => s.OptionalGroup).Distinct().Count();
                if (pw.lvOptional.SelectedItems.Count != optionalCount)
                {
                    MessageBox.Show("套餐可选菜品组每个必须选一个，且只能选一个");
                    return false;
                }
                foreach (dynamic si in pw.lvOptional.SelectedItems)
                {
                    DXInfo.Models.InventoryEx selInv = new DXInfo.Models.InventoryEx();
                    selInv.Id = si.Id;
                    selInv.Code = si.Code;
                    selInv.Name = si.Name;
                    selInv.SalePrice = si.SalePrice;
                    selInv.Quantity = si.Quantity>0?si.Quantity:1;//1;
                    selInv.Comment = "TC";
                    selInv.IsPackage = true;
                    selInv.PackageId = packageId;
                    selInv.PackageSn = packageSn;
                    selInv.OrderId = this.SelectedOrderDish.Id;
                    lPackageDeskOrderMenu.Add(selInv);
                }
            }
            return true;
        }        
        protected override void AfterSelectInventory()
        {
            #region 校验
            if (this.SelectedInventory == null)
            {
                return;
            }
            if (this.SelectedOrderDish == null)
            {
                MessageBox.Show("请开台");
                return;
            }
            #endregion

            int lackCount = (from d in Uow.MenuStatus.GetAll()
                                           where d.Dept == Dept.DeptId && d.Inventory == this.SelectedInventory.Id
                                           && d.Status == (int)DXInfo.Models.OrderMenuStatus.Lack
                                           select d).Count();
            if (lackCount > 0)
            {
                Helper.ShowErrorMsg("“" + this.SelectedInventory.Name + "”缺菜！");
                return;
            }
            if (this.SelectedInventory.IsPackage)
            {
                int packageSn = this.lSelectedOrderPackage.Count + 1;
                List<DXInfo.Models.InventoryEx> lPackageDeskOrderMenu;
                if (!popupPackageWin(this.SelectedInventory.Id, packageSn, out lPackageDeskOrderMenu)) return;
                lPackageDeskOrderMenu.ForEach(delegate(DXInfo.Models.InventoryEx dom) { this.OCInventoryEx.Add(dom); });

                OrderPackages op = new OrderPackages();
                op.InventoryId = this.SelectedInventory.Id;
                op.Price = this.SelectedInventory.SalePrice;
                op.Amount = this.SelectedInventory.SalePrice;
                op.Quantity = 1;
                op.PackageSn = packageSn;
                op.OrderId = this.SelectedOrderDish.Id;
                op.CreateDate = DateTime.Now;
                op.UserId = this.User.UserId;
                op.Oper = this.Oper.UserId;
                this.lSelectedOrderPackage.Add(op);
            }
            else
            {
                CreateInventoryEx(this.SelectedInventory);
            }
        }
        protected override void CreateInventoryEx(DXInfo.Models.Inventory inv)
        {
            //if (this.SelectedInventory != null && this.SelectedOrderDish != null)
            //{
                InventoryEx inventoryEx = Mapper.Map<DXInfo.Models.InventoryEx>(inv);
                inventoryEx.Quantity = 1;
                inventoryEx.IsPackage = false;
                inventoryEx.OrderId = this.SelectedOrderDish.Id;
                
                this.SetInvPrice(inventoryEx);
                //this.SetCurrentStock(inventoryEx);
                this.SetOCInventoryEx();
                this.OCInventoryEx.Add(inventoryEx);
            //}
        }
        private void CreateInventoryEx(Guid orderId)
        {
            this.SetOCInventoryEx();
            if (this.OCInventoryEx.Count > 0)
            {
                this.OCInventoryEx.Clear();
            }
            DateTime dtNow = DateTime.Now;
            var query = (from m in Uow.OrderMenus.GetAll().Where(w => w.OrderId == orderId)
                     join i in Uow.Inventory.GetAll() on m.InventoryId equals i.Id into mi
                     from mis in mi.DefaultIfEmpty()
                     join o in Uow.aspnet_CustomProfile.GetAll() on m.UserId equals o.UserId into mo
                     from mos in mo.DefaultIfEmpty()
                     select new DXInfo.Models.InventoryEx()
                     {
                         OrderId = m.OrderId,
                         OrderMenuId = m.Id,
                         Id = m.InventoryId,
                         Category = mis.Category,
                         Code = mis.Code,
                         Name = mis.Name,
                         EnglishName = mis.EnglishName,
                         SalePrice = m.Price,
                         Quantity = m.Quantity,
                         Comment = m.Comment,
                         Status = m.Status,
                         UserName = mos.FullName,
                         MenuCreateDate = m.MenuCreateDate,
                         CreateDate = m.CreateDate,
                         MissQuantity = m.MissQuantity,
                         MenuQuantity = m.MenuQuantity,
                         IsPackage = m.IsPackage,
                         PackageId = m.PackageId,
                         PackageSn = m.PackageSn,
                         Printer = mis.Printer,
                         WhId = mis.WhId,
                         Locator = mis.Locator,
                     }
                     );
            var q = query.ToList();
            foreach (DXInfo.Models.InventoryEx sd in q)
            {
                sd.WaitMinutes = Convert.ToInt32(sd.MenuCreateDate.HasValue ? (sd.MenuCreateDate.Value - sd.CreateDate).TotalMinutes : (dtNow - sd.CreateDate).TotalMinutes);
                this.SetInvPrice(sd);
                //this.SetCurrentStock(sd);
                this.OCInventoryEx.Add(sd);
            }

        }
        #endregion

        #region 房间与桌台
        public override void AfterCheckOut()
        {
            //base.AfterCheckOut();
            SetOCDesk(null);
        }
        private void RefreshDeskExecute()
        {            
            SetOCDesk(null);
        }
        public ICommand RefreshDesk
        {
            get
            {
                return new RelayCommand(RefreshDeskExecute);
            }
        }
        protected override void AfterSelectRoom()
        {
            if (this.SelectedRoom != null)
            {
                SetOCDesk(this.SelectedRoom.Id);
            }
            else
            {
                SetOCDesk(null);
            }
        }
        private void SetOCDesk(Guid? roomId)
        {
            if (this.lDeskEx == null)
                return;
            List<DXInfo.Models.DeskEx> ldeskex = new List<DeskEx>(this.lDeskEx);
            if (roomId.HasValue)
            {
                ldeskex = ldeskex.Where(w => w.RoomId == roomId.Value).ToList();
            }
            ldeskex.ForEach(delegate(DXInfo.Models.DeskEx desk) { desk.Status = 2; });

            var orderBookDeskes = from d in Uow.OrderBookDeskes.GetAll()
                                  join d1 in Uow.OrderBooks.GetAll() on d.OrderBookId equals d1.Id into dd1
                                  from dd1s in dd1.DefaultIfEmpty()
                                  join d2 in Uow.Desks.GetAll() on d.DeskId equals d2.Id into dd2
                                  from dd2s in dd2.DefaultIfEmpty()

                                  where dd1s.Status == (int)DXInfo.Models.OrderBookStatus.Booked
                                  && d.Status == (int)DXInfo.Models.OrderBookDeskStatus.Booked
                                  && dd1s.BookBeginDate <= DateTime.Now && dd1s.BookEndDate >= DateTime.Now
                                  select new
                                  {
                                      dd2s.RoomId,
                                      d.DeskId,
                                      Status = 1
                                  };
            if (roomId.HasValue)
            {
                orderBookDeskes = orderBookDeskes.Where(w => w.RoomId == roomId.Value);
            }
            var lorderBookDeskes = orderBookDeskes.ToList().Distinct().ToList();

            foreach (var obd in lorderBookDeskes)
            {
                DXInfo.Models.Desks desk = ldeskex.Find(f => f.Id == obd.DeskId);
                if (desk != null)
                {
                    desk.Status = obd.Status;
                }
            }

            var orderDeskes = from d in Uow.OrderDeskes.GetAll()
                              join d1 in Uow.OrderDishes.GetAll() on d.OrderId equals d1.Id into dd1
                              from dd1s in dd1.DefaultIfEmpty()
                              join d2 in Uow.Desks.GetAll() on d.DeskId equals d2.Id into dd2
                              from dd2s in dd2.DefaultIfEmpty()

                              where (dd1s.Status == (int)DXInfo.Models.OrderDishStatus.Opened
                              || dd1s.Status == (int)DXInfo.Models.OrderDishStatus.Ordered)
                              && d.Status == (int)DXInfo.Models.OrderDeskStatus.InUse
                              select new { dd2s.RoomId,d.DeskId, Status = dd1s.Status };
            if (roomId.HasValue)
            {
                orderDeskes = orderDeskes.Where(w => w.RoomId == roomId.Value);
            }
            var lorderDeskes = orderDeskes.ToList().Distinct().ToList();

            foreach (var od in lorderDeskes)
            {
                DXInfo.Models.Desks desk = ldeskex.Find(f => f.Id == od.DeskId);
                if (desk != null)
                {
                    desk.Status = od.Status;
                }
            }
            //if (this.OCDeskEx != null)
            //{
            //    this.OCDeskEx.Clear();
            //}
            this.OCDeskEx = new ObservableCollection<DeskEx>(ldeskex);            
        }
        //private void ResetCheckout()
        //{
            

        //}
        

        private List<DXInfo.Models.OrderBookEx> GetOrderBookEx(Guid? deskId)
        {
            DateTime dtNow = DateTime.Now;
            var q = from d in Uow.OrderBookDeskes.GetAll()

                     join d1 in Uow.OrderBooks.GetAll() on d.OrderBookId equals d1.Id into dd1
                     from dd1s in dd1.DefaultIfEmpty()

                     join d2 in Uow.aspnet_CustomProfile.GetAll() on dd1s.UserId equals d2.UserId into dd2
                     from dd2s in dd2.DefaultIfEmpty()

                     join d3 in Uow.Desks.GetAll() on d.DeskId equals d3.Id into dd3
                     from dd3s in dd3.DefaultIfEmpty()

                     where
                         //d.DeskId == this.SelectedDesk.Id && 
                     d.Status == (int)DXInfo.Models.OrderBookDeskStatus.Booked
                     && dd1s.Status == (int)DXInfo.Models.OrderBookStatus.Booked
                     && DbFunctions.DiffDays(dd1s.BookEndDate, dtNow) <= 0
                     orderby dd1s.BookBeginDate

                     select new DXInfo.Models.OrderBookEx()
                     {
                         Id = dd1s.Id,
                         BookBeginDate = dd1s.BookBeginDate,
                         BookEndDate = dd1s.BookEndDate,
                         Comment = dd1s.Comment,
                         CreateDate = dd1s.CreateDate,
                         Customer = dd1s.Customer,
                         LinkPhone = dd1s.LinkPhone,
                         Quantity = dd1s.Quantity,
                         FullName = dd2s.FullName,
                         DeskNo = dd3s.Code + dd3s.Name,
                         OrderBookDeskId = d.Id,
                         DeskId = d.DeskId,
                     };//).ToList();
            if(deskId.HasValue)
            {
                q = q.Where(w=>w.DeskId==deskId.Value);
            }
            List<DXInfo.Models.OrderBookEx> lobe = q.ToList();
            return lobe;
        }
        protected override void AfterSelectDeskEx()
        {
            this.ResetSwipingCard();
            this.ResetCheckOut();
            if (this.SelectedDeskEx != null)
            {
                //DXInfo.Models.OrderDeskes orderDesk = Uow.OrderDeskes
                //    .GetAll()
                //    .Where(w => w.Status == (int)DXInfo.Models.OrderDeskStatus.InUse &&
                //        w.DeskId == this.SelectedDeskEx.Id).FirstOrDefault();
                DXInfo.Models.OrderDeskes orderDesk = (from d in Uow.OrderDeskes.GetAll()
                                                       join d1 in Uow.OrderDishes.GetAll() on d.OrderId equals d1.Id into dd1
                                                       from dd1s in dd1.DefaultIfEmpty()
                                                       where d.Status == (int)DXInfo.Models.OrderDeskStatus.InUse &&
                                                           d.DeskId == this.SelectedDeskEx.Id &&
                                                           dd1s.Id != null
                                                       select d).FirstOrDefault();
                if (orderDesk != null)
                {
                    this.OrderMenuVisibility = Visibility.Visible;
                    DXInfo.Models.OrderDishes od = Uow.OrderDishes.GetById(g => g.Id == orderDesk.OrderId);
                    if (od.Status != (int)DXInfo.Models.OrderDishStatus.Canceled &&
                        od.Status != (int)DXInfo.Models.OrderDishStatus.Checkouted)
                    {
                        this.SelectedDeskEx.Status = od.Status;
                        this.SelectedOrderDesk = orderDesk;
                        this.SelectedOrderDish = od;
                        if (this.SelectedOrderDish.UserId.HasValue)
                        {
                            DXInfo.Models.aspnet_CustomProfile oper = Uow.aspnet_CustomProfile.GetById(g => g.UserId == this.SelectedOrderDish.UserId.Value);
                            if (oper != null)
                            {
                                this.OpenOperName = oper.FullName;
                            }
                        }
                        this.lSelectedOrderPackage = Uow.OrderPackages.GetAll().Where(w => w.OrderId == orderDesk.OrderId).ToList();
                        CreateInventoryEx(orderDesk.OrderId);
                    }
                }
                else
                {
                    int currentStatus = 2;
                    List<DXInfo.Models.OrderBookEx> q = GetOrderBookEx(this.SelectedDeskEx.Id);
                    if (q.Count > 0)
                    {
                        this.OrderBookVisibility = Visibility.Visible;
                        this.OCOrderBookEx = new ObservableCollection<OrderBookEx>(q);
                        DateTime dtNow = DateTime.Now;
                        int currentBook = q.Where(w => w.BookBeginDate <= dtNow && w.BookEndDate >= dtNow).Count();
                        if (currentBook > 0)
                        {
                            currentStatus = 1;
                        }
                    }
                    this.SelectedDeskEx.Status = currentStatus;
                }
            }
            else
            {
                this.SelectedOrderDesk = null;
                this.SelectedOrderDish = null;
                if (this.lSelectedOrderPackage != null)
                {
                    this.lSelectedOrderPackage.Clear();
                    this.lSelectedOrderPackage = null;
                }
                if (this.OCInventoryEx != null)
                {
                    this.OCInventoryEx.Clear();
                    this.OCInventoryEx = null;
                }
                this.OrderMenuVisibility = Visibility.Collapsed;
                this.OrderBookVisibility = Visibility.Collapsed;
                if (this.OCOrderBookEx != null)
                {
                    this.OCOrderBookEx.Clear();
                    this.OCOrderBookEx = null;
                }
            }
        }
        #endregion

        #region OrderBook预约

        #region 修改预定信息
        private void ModifyBookExecute()
        {
            if (this.SelectedOrderBookEx != null)
            {
                bool ret = ClientCommon.OrderBook_ModifyBook(this.SelectedOrderBookEx.Id);
                if (ret)
                {
                    Helper.ShowSuccMsg("修改预订信息成功");
                    this.AfterSelectDeskEx();
                }
            }
        }
        private bool ModifyBookCanExecute()
        {
            if (this.SelectedOrderBookEx != null)
                return true;
            return false;
        }
        public ICommand ModifyBook
        {
            get
            {
                return new RelayCommand(ModifyBookExecute, ModifyBookCanExecute);
            }
        }
        #endregion

        #region 取消预定
        private void CancelBookExecute()
        {
            if (this.SelectedOrderBookEx != null)
            {
                this.DeskManageFacade.dtOperDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));//DateTime.Now;
                this.DeskManageFacade.CancelBook(this.SelectedOrderBookEx.Id);
                MessageBox.Show("取消预定成功");  
                this.AfterSelectDeskEx();
            }
        }
        private bool CancelBookCanExecute()
        {
            if (this.SelectedOrderBookEx != null)
                return true;
            return false;
        }
        public ICommand CancelBook
        {
            get
            {
                return new RelayCommand(CancelBookExecute, CancelBookCanExecute);
            }
        }
        #endregion

        #region 开台
        private void OpenBookExecute()
        {
            if (this.SelectedOrderBookEx != null)
            {
                DeskQuantityWindow dqw = new DeskQuantityWindow();
                if (dqw.ShowDialog().GetValueOrDefault())
                {
                    int quantity = dqw.Quantity;

                    this.DeskManageFacade.dtOperDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));//DateTime.Now;
                    DXInfo.Models.OrderDishes orderDish = new OrderDishes();
                    DXInfo.Models.OrderDeskes orderDesk = new OrderDeskes();
                    this.DeskManageFacade.OpenBook(this.SelectedOrderBookEx.Id,this.SelectedOrderBookEx.DeskId,quantity, false,
                         ref orderDish, ref orderDesk); 
                    MessageBox.Show("预订开台成功");
                    this.AfterSelectRoom();
                    this.AfterSelectDeskEx();
                }
            }
        }
        private bool OpenBookCanExecute()
        {
            if (this.SelectedOrderBookEx != null)
                return true;
            return false;
        }
        public ICommand OpenBook
        {
            get
            {
                return new RelayCommand(OpenBookExecute, OpenBookCanExecute);
            }
        }
        #endregion

        #region 换台
        private void ExchangeBookDeskExecute()
        {
            if (this.SelectedOrderBookEx != null)
            {
                DeskExchangeWindow dew = new DeskExchangeWindow();
                if (dew.ShowDialog().GetValueOrDefault())
                {
                    string DeskNo = dew.txtDeskNo.Text;
                    DXInfo.Models.Desks desk = this.lDeskEx.Where(w => w.Code == DeskNo).FirstOrDefault();
                    if (desk == null)
                    {
                        MessageBox.Show("输入的桌台号无效！");
                        return;
                    }
                    this.DeskManageFacade.dtOperDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));//DateTime.Now;
                    this.DeskManageFacade.ExchangeBookDesk(desk.Id, this.SelectedOrderBookEx.OrderBookDeskId,
                        this.SelectedOrderBookEx.BookBeginDate,
                        this.SelectedOrderBookEx.BookEndDate);
                    Helper.ShowSuccMsg("换台成功");
                    this.AfterSelectRoom();
                    this.AfterSelectDeskEx();
                }
            }
        }
        private bool ExchangeBookDeskCanExecute()
        {
            if (this.SelectedOrderBookEx != null)
                return true;
            return false;
        }
        public ICommand ExchangeBookDesk
        {
            get
            {
                return new RelayCommand(ExchangeBookDeskExecute, ExchangeBookDeskCanExecute);
            }
        }
        #endregion

        #region 加台
        private void AddBookDeskExecute()
        {
            if (this.SelectedOrderBookEx != null)
            {
                DeskExchangeWindow dew = new DeskExchangeWindow();
                if (dew.ShowDialog().GetValueOrDefault())
                {
                    string DeskNo = dew.txtDeskNo.Text;
                    DXInfo.Models.Desks desk = this.lDeskEx.Where(w => w.Code == DeskNo).FirstOrDefault();
                    if (desk == null)
                    {
                        MessageBox.Show("输入的桌台号无效！");
                        return;
                    }
                    this.DeskManageFacade.dtOperDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));//DateTime.Now;
                    this.DeskManageFacade.AddBookDesk(desk.Id, this.SelectedOrderBookEx.Id,
                        this.SelectedOrderBookEx.BookBeginDate,
                        this.SelectedOrderBookEx.BookEndDate);
                    Helper.ShowSuccMsg("加台成功");
                    this.AfterSelectRoom();
                }
            }
        }
        private bool AddBookDeskCanExecute()
        {
            if (this.SelectedOrderBookEx != null)
                return true;
            return false;
        }
        public ICommand AddBookDesk
        {
            get
            {
                return new RelayCommand(AddBookDeskExecute, AddBookDeskCanExecute);
            }
        }
        #endregion

        #region 撤台
        private void CancelBookDeskExecute()
        {
            if (this.SelectedOrderBookEx != null)
            {
                this.DeskManageFacade.dtOperDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));//DateTime.Now;
                this.DeskManageFacade.CancelBookDesk(this.SelectedOrderBookEx.Id,this.SelectedOrderBookEx.OrderBookDeskId);
                MessageBox.Show("撤台成功");
                this.AfterSelectRoom();
                this.AfterSelectDeskEx();
            }
        }
        private bool CancelBookDeskCanExecute()
        {
            if (this.SelectedOrderBookEx != null)
                return true;
            return false;
        }
        public ICommand CancelBookDesk
        {
            get
            {
                return new RelayCommand(CancelBookDeskExecute, CancelBookDeskCanExecute);
            }
        }
        #endregion

        #endregion

        #region OrderMenu菜品

        #region 删除菜品
        private void DeleteMenuExecute()
        {
            //删除记录
            if (this.SelectedInventoryEx != null && this.SelectedOrderDish != null)
            {
                if (this.SelectedInventoryEx.OrderMenuId != Guid.Empty)
                {
                    ClientCommon.OrderDish_DeleteMenu(this.SelectedInventoryEx.OrderMenuId);
                    this.AfterSelectDeskEx();
                }
                else
                {
                    if (!this.SelectedInventoryEx.IsPackage)
                    {
                        this.OCInventoryEx.Remove(this.SelectedInventoryEx);
                    }
                    else
                    {

                        foreach (DXInfo.Models.InventoryEx iex in this.OCInventoryEx.Where(w => w.IsPackage && w.PackageId == this.SelectedInventoryEx.PackageId && w.PackageSn == this.SelectedInventoryEx.PackageSn).ToList())
                        {
                            this.OCInventoryEx.Remove(iex);
                        }
                        
                    }
                    this.SelectedInventoryEx = null;
                }
            }
        }
        private bool DeleteMenuCanExecute()
        {
            if (this.SelectedInventoryEx != null && 
                this.SelectedOrderDish != null &&
                this.SelectedInventoryEx.Status == (int)DXInfo.Models.OrderMenuStatus.Normal)
                return true;
            return false;
        }
        public ICommand DeleteMenu
        {
            get
            {
                return new RelayCommand(DeleteMenuExecute, DeleteMenuCanExecute);
            }
        }
        #endregion

        #region 催菜
        private void HurryMenuExecute()
        {
            if (this.SelectedOrderDish != null && this.SelectedInventoryEx != null)
            {
                DXInfo.Models.InventoryEx selInv = this.SelectedInventoryEx;
                if (selInv.Status == 1)
                {
                    MessageBox.Show("已退菜不能催菜！");
                    return;
                }
                if (selInv.Status == 3)
                {
                    MessageBox.Show("缺菜不能催菜！");
                    return;
                }
                if (selInv.OrderMenuId == Guid.Empty)
                {
                    MessageBox.Show("未下单不能催菜！");
                    return;
                }
                this.DeskManageFacade.dtOperDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));//DateTime.Now;
                this.DeskManageFacade.HurryMenu(selInv.OrderMenuId);
                this.CreateInventoryEx(this.SelectedOrderDish.Id);
                MessageBox.Show("催菜成功");
            }
        }
        private bool HurryMenuCanExecute()
        {      
            if (this.SelectedOrderDish != null &&
                this.SelectedInventoryEx != null &&
                (this.SelectedInventoryEx.Status == (int)DXInfo.Models.OrderMenuStatus.Order ||
                this.SelectedInventoryEx.Status == (int)DXInfo.Models.OrderMenuStatus.Hurry) &&
                this.SelectedInventoryEx.OrderMenuId != Guid.Empty)
                return true;
            return false;
        }
        public ICommand HurryMenu
        {
            get
            {
                return new RelayCommand(HurryMenuExecute, HurryMenuCanExecute);
            }
        }
        #endregion

        #region 退单

        private void MenuCancelOrderExecute()
        {
            if (this.SelectedInventoryEx != null &&
                this.SelectedOrderDesk != null &&
                this.SelectedOrderDish != null)
            {
                if (MessageBox.Show("是否确认退菜？", "退菜", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                    return;

                Hashtable htOtherPrint = new Hashtable();
                Hashtable htLocalPrint = new Hashtable();
                DateTime dtOperDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));//DateTime.Now;
                List<DXInfo.Models.InventoryEx> lie = new List<InventoryEx>();
                List<DXInfo.Models.OrderPackages> lop = null;
                if (this.SelectedInventoryEx.IsPackage)
                {
                    lop =
                        this.lSelectedOrderPackage.Where(w => w.InventoryId == this.SelectedInventoryEx.PackageId &&
                            w.PackageSn == this.SelectedInventoryEx.PackageSn &&
                            w.Status != (int)DXInfo.Models.OrderMenuStatus.Order).ToList();
                    lie = this.OCInventoryEx.Where(w => w.PackageId == this.SelectedInventoryEx.PackageId &&
                            w.PackageSn == this.SelectedInventoryEx.PackageSn).ToList();
                }
                else
                {
                    lie.Add(this.SelectedInventoryEx);
                }
                this.DeskManageFacade.dtOperDate = dtOperDate;
                this.DeskManageFacade.MenuCancelOrder(lie, lop, ref htOtherPrint,ref htLocalPrint);
                try
                {
                    PrintOrder(this.SelectedOrderDish.Id, htOtherPrint, htLocalPrint, null, null, dtOperDate);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    MessageBox.Show("退菜成功！");
                    //this.CreateInventoryEx(this.SelectedOrderDish.Id);
                    this.AfterSelectDeskEx();
                }
            }
        }
        private bool MenuCancelOrderCanExecute()
        {
            if (this.SelectedInventoryEx != null &&
                this.SelectedOrderDesk != null &&
                this.SelectedOrderDish != null &&
                this.SelectedInventoryEx.OrderMenuId != Guid.Empty&&
                this.SelectedInventoryEx.Status != (int)DXInfo.Models.OrderMenuStatus.Withdraw&&
                this.SelectedInventoryEx.Status != (int)DXInfo.Models.OrderMenuStatus.ReturnAfterOut)
                return true;
            return false;
        }
        public ICommand MenuCancelOrder
        {
            get
            {
                return new RelayCommand(MenuCancelOrderExecute, MenuCancelOrderCanExecute);
            }
        }
        #endregion

        #region 下单
        private void MenuOrderExecute()
        {
            MenuOrderMethod();
        }
        private void MenuOrderMethod(bool isTemp = false)
        {
            if (this.SelectedOrderDish == null ||
                this.SelectedInventoryEx == null)
                return;
            DateTime dtOperDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));//DateTime.Now;
            this.DeskManageFacade.dtOperDate = dtOperDate;
            if (this.SelectedInventoryEx.Status == (int)DXInfo.Models.OrderMenuStatus.Lack)
            {
                if (MessageBox.Show("是否取消缺菜？", "确认重新下单", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                {
                    return;
                }
                this.DeskManageFacade.CancelMissMenu(this.SelectedInventoryEx.OrderMenuId);
            }
            Hashtable htOtherPrint = new Hashtable();
            Hashtable htLocalPrint = new Hashtable();


            List<DXInfo.Models.InventoryEx> lie = new List<InventoryEx>();
            List<DXInfo.Models.OrderPackages> lop = null;
            if (this.SelectedInventoryEx.IsPackage)
            {
                lop =
                    this.lSelectedOrderPackage.Where(w => w.InventoryId == this.SelectedInventoryEx.PackageId &&
                        w.PackageSn == this.SelectedInventoryEx.PackageSn &&
                        w.Status != (int)DXInfo.Models.OrderMenuStatus.Order).ToList();
                lie = this.OCInventoryEx.Where(w => w.PackageId == this.SelectedInventoryEx.PackageId &&
                        w.PackageSn == this.SelectedInventoryEx.PackageSn).ToList();
            }
            else
            {
                lie.Add(this.SelectedInventoryEx);
            }

            this.DeskManageFacade.MenuOrder(this.SelectedOrderDish.Id, lie, lop,
                ref htOtherPrint, ref htLocalPrint, isTemp);
            try
            {
                if (!isTemp)
                {
                    PrintOrder(this.SelectedOrderDish.Id, htOtherPrint, htLocalPrint, null, null, dtOperDate);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(isTemp)
                {
                    MessageBox.Show("已存单");
                }
                else
                {
                    MessageBox.Show("下单成功！");
                }
                //this.CreateInventoryEx(this.SelectedOrderDish.Id);
                this.AfterSelectDeskEx();
            }
        }
        private bool MenuOrderCanExecute()
        {
            if (this.SelectedOrderDish != null &&
                this.SelectedOrderDish.Status==(int)DXInfo.Models.OrderDishStatus.Ordered &&
                this.SelectedInventoryEx != null&&
                (this.SelectedInventoryEx.Status == (int)DXInfo.Models.OrderMenuStatus.Normal||
                this.SelectedInventoryEx.Status == (int)DXInfo.Models.OrderMenuStatus.ReturnAfterOut||
                this.SelectedInventoryEx.Status==(int)DXInfo.Models.OrderMenuStatus.Withdraw))
                return true;
            return false;
        }
        public ICommand MenuOrder
        {
            get
            {
                return new RelayCommand(MenuOrderExecute, MenuOrderCanExecute);
            }
        }
        #endregion

        #region 存单
        private void SaveMenuOrderExecute()
        {
            MenuOrderMethod(true);
        }
        public ICommand SaveMenuOrder
        {
            get
            {
                return new RelayCommand(SaveMenuOrderExecute, MenuOrderCanExecute);
            }
        }
        #endregion
        #endregion

        #region 桌台

        #region 开台
        private void OpenExecute()
        {
            if (this.SelectedDeskEx != null &&
                this.SelectedOrderDish == null)
            {
                DeskQuantityWindow dqw = new DeskQuantityWindow();
                if (dqw.ShowDialog().GetValueOrDefault())
                {
                    this.DeskManageFacade.dtOperDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    DXInfo.Models.OrderDishes orderDish=new OrderDishes();
                    DXInfo.Models.OrderDeskes orderDesk=new OrderDeskes();
                    this.DeskManageFacade.Open(dqw.Quantity, this.SelectedDeskEx.Id, false,
                        ref orderDish,ref orderDesk);
                    MessageBox.Show("开台成功！");
                    this.AfterSelectDeskEx();
                }                
            }
        }
        private bool OpenCanExecute()
        {
            if (this.SelectedDeskEx != null && this.SelectedOrderDish==null)
                return true;
            return false;
        }
        public ICommand Open
        {
            get
            {
                return new RelayCommand(OpenExecute, OpenCanExecute);
            }
        }
        #endregion

        #region 预定
        private void BookExecute()
        {
            if (this.SelectedDeskEx != null)
            {
                DeskBookWindow dqw = new DeskBookWindow();
                if (dqw.ShowDialog().GetValueOrDefault())
                {
                    int quantity = 0;
                    if (!string.IsNullOrEmpty(dqw.txtQuantity.Text)) quantity = Convert.ToInt32(dqw.txtQuantity.Text);
                    DateTime dtBookBeginDate = Convert.ToDateTime(dqw.dpBeginDate.Text + " " + dqw.tpBeginTime.Value.GetValueOrDefault().Hour + ":" + dqw.tpBeginTime.Value.GetValueOrDefault().Minute);
                    DateTime dtBookEndDate = Convert.ToDateTime(dqw.dpEndDate.Text + " " + dqw.tpEndTime.Value.GetValueOrDefault().Hour + ":" + dqw.tpEndTime.Value.GetValueOrDefault().Minute);
                    this.DeskManageFacade.dtOperDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));//DateTime.Now;
                    this.DeskManageFacade.Book(this.SelectedDeskEx.Id, quantity, dqw.txtLinkName.Text, dqw.txtLinkPhone.Text, dtBookBeginDate, dtBookEndDate);
                    MessageBox.Show("预定成功！");
                    this.AfterSelectDeskEx();
                }
            }
        }
        private bool BookCanExecute()
        {
            if (this.SelectedDeskEx != null)
                return true;
            return false;
        }
        public ICommand Book
        {
            get
            {
                return new RelayCommand(BookExecute,BookCanExecute);
            }
        }
        #endregion

        #region 撤销
        private void CancelOpenPrint(Guid orderDishId, ref Hashtable htOtherPrint,
            ref Hashtable htLocalPrint)
        {
            //打印                    
            htOtherPrint = new Hashtable();
            htLocalPrint = new Hashtable();
            List<OrderMenus> lom = (from d in Uow.OrderMenus.GetAll().Where(w => w.OrderId == orderDishId &&
                (w.Status == (int)DXInfo.Models.OrderMenuStatus.Hurry ||
                w.Status == (int)DXInfo.Models.OrderMenuStatus.Lack ||
                w.Status == (int)DXInfo.Models.OrderMenuStatus.Make ||
                w.Status == (int)DXInfo.Models.OrderMenuStatus.Order))
                                    select d).ToList();
            
            foreach (DXInfo.Models.OrderMenus om in lom)
            {
                DXInfo.Models.Inventory inv = Uow.Inventory.GetById(g => g.Id == om.InventoryId);
                DXInfo.Models.InventoryEx iex = Mapper.Map<DXInfo.Models.InventoryEx>(om);
                iex.Code = inv.Code;
                iex.Name = inv.Name;
                iex.Printer = inv.Printer;
                this.DeskManageFacade.AddPrint(ref htOtherPrint,ref htLocalPrint, iex,PrintType.CancelOpen);
            }
            //PrintOrder(orderDishId,htOtherPrint,htLocalPrint,null,null, dtOperDate);
            
        }
        private void CancelOpenExecute()
        {
            if (this.SelectedOrderDish != null)
            {
                if (MessageBox.Show("是否确认撤销？", "撤销", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    string deskCodes = this.DeskManageFacade.GetOrderDeskCodes(Uow, this.SelectedOrderDish.Id);
                    DateTime dtOperDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    this.DeskManageFacade.dtOperDate = dtOperDate;//DateTime.Now;
                    Hashtable htOtherPrint = new Hashtable();
                    Hashtable htLocalPrint = new Hashtable();
                    CancelOpenPrint(this.SelectedOrderDish.Id, ref htOtherPrint,ref htLocalPrint);
                    this.DeskManageFacade.CancelOpen(this.SelectedOrderDish.Id);
                    try
                    {
                        PrintOrder(htOtherPrint, htLocalPrint, null, null, dtOperDate, deskCodes);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        MessageBox.Show("撤销成功！");
                        this.AfterSelectDeskEx();
                    }
                }
            }
        }
        private bool CancelOpenCanExecute()
        {
            if (this.SelectedOrderDish != null )
                return true;
            return false;
        }
        public ICommand CancelOpen
        {
            get
            {
                return new RelayCommand(CancelOpenExecute, CancelOpenCanExecute);
            }
        }
        #endregion

        #region 换台
        private void ExchangeExecute()
        {
            if (this.SelectedDeskEx == null)
            {
                Helper.ShowErrorMsg("请选择桌台");
                return;
            }
            if (this.SelectedOrderDish == null || this.SelectedOrderDesk == null)
            {
                Helper.ShowErrorMsg("此桌台未开台，请首先开台");
                return;
            }
            DeskExchangeWindow dew = new DeskExchangeWindow();
            if (dew.ShowDialog().GetValueOrDefault())
            {
                string DeskNo = dew.txtDeskNo.Text;
                DXInfo.Models.Desks desk = this.lDeskEx.Where(w => w.Code == DeskNo).FirstOrDefault();
                if (desk == null)
                {
                    MessageBox.Show("输入的桌台号无效！");
                    return;
                }

                Hashtable htOtherPrint = new Hashtable();
                Hashtable htLocalPrint = new Hashtable();
                DateTime dtOperDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                this.DeskManageFacade.dtOperDate = dtOperDate;//DateTime.Now;
                this.DeskManageFacade.ExchangeDesk(this.SelectedOrderDish.Id, desk.Id, this.SelectedOrderDesk.Id);
                AddOrderMenuPrint(this.SelectedOrderDish.Id, ref htOtherPrint, ref htLocalPrint, PrintType.Exchange);
                try
                {
                    PrintOrder(null, htOtherPrint, htLocalPrint, this.SelectedDeskEx.Code, DeskNo, dtOperDate);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    MessageBox.Show("换台成功！");
                    this.AfterSelectRoom();
                    this.AfterSelectDeskEx();
                }
            }
        }
        private bool ExchangeCanExecute()
        {
            if (this.SelectedDeskEx != null && 
                this.SelectedOrderDish!=null && 
                this.SelectedOrderDesk!=null)
                return true;
            return false;
        }
        public ICommand Exchange
        {
            get
            {
                return new RelayCommand(ExchangeExecute, ExchangeCanExecute);
            }
        }
        #endregion

        #region 并单
        private void MergeExecute()
        {
            DeskExchangeWindow dew = new DeskExchangeWindow();
            if (dew.ShowDialog().GetValueOrDefault())
            {
                string DeskNo = dew.txtDeskNo.Text;
                DXInfo.Models.Desks desk = this.lDeskEx.Where(w => w.Code == DeskNo).FirstOrDefault();
                if (desk == null)
                {
                    MessageBox.Show("输入的桌台号无效！");
                    return;
                }
                int count = Uow.OrderDeskes.GetAll().Where(w => w.DeskId == desk.Id && w.Status == (int)DXInfo.Models.OrderDeskStatus.InUse).Count();
                if(count==0)
                {
                    MessageBox.Show("输入的桌台号未开台！");
                    return;
                }
                if (this.SelectedOrderDish != null)
                {
                    this.DeskManageFacade.dtOperDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));//DateTime.Now;
                    this.DeskManageFacade.MergeOrderDish(desk.Id, this.SelectedOrderDish.Id);
                    MessageBox.Show("并单成功！");                    
                    this.AfterSelectRoom();
                    this.AfterSelectDeskEx();
                }
            }
        }
        private bool MergeCanExecute()
        {
            if (this.SelectedOrderDish != null)
                return true;
            return false;
        }
        public ICommand Merge
        {
            get
            {
                return new RelayCommand(MergeExecute, MergeCanExecute);
            }
        }
        #endregion

        #region 撤台
        private void CancelOpenDeskExecute()
        {            
            if (this.SelectedOrderDesk != null && this.SelectedDeskEx != null)
            {
                DXInfo.Models.OrderDeskes chkDesk = Uow.OrderDeskes.GetById(g => g.Id == this.SelectedOrderDesk.Id);
                if (chkDesk.Status == (int)DXInfo.Models.OrderDeskStatus.Idle) throw new Exception("此桌台已经撤销");

                List<DXInfo.Models.OrderDeskes> q = (from d in Uow.OrderDeskes.GetAll()
                                                     where d.OrderId == this.SelectedOrderDish.Id && d.Status == 0
                                                     select d).ToList();
                if (q.Count == 1)
                {
                    CancelOpenExecute();
                }
                else
                {
                    if (MessageBox.Show("是否撤台？", "撤台", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                    {
                        return;
                    }
                    DateTime dtOperDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    this.DeskManageFacade.dtOperDate = dtOperDate;
                    this.DeskManageFacade.CancelOpenDesk(this.SelectedOrderDish.Id, this.SelectedOrderDesk.Id);
                    MessageBox.Show("撤台成功");
                }
                this.AfterSelectDeskEx();
            }
        }
        private bool CancelOpenDeskCanExecute()
        {
            if (this.SelectedOrderDish != null && 
                this.SelectedOrderDesk != null)
                return true;
            return false;
        }
        public ICommand CancelOpenDesk
        {
            get
            {
                return new RelayCommand(CancelOpenDeskExecute, CancelOpenDeskCanExecute);
            }
        }
        #endregion

        #region 下单
        private void OrderExecute()
        {
            OrderMethod();
        }
        private void OrderMethod(bool isTemp = false)
        {
            if (this.SelectedOrderDish != null &&
                this.OCInventoryEx != null &&
                this.OCInventoryEx.Count > 0)
            {
                Hashtable htOtherPrint = new Hashtable();
                Hashtable htLocalPrint = new Hashtable();
                DateTime dtOperDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                this.DeskManageFacade.dtOperDate = dtOperDate;
                this.DeskManageFacade.Order(this.SelectedOrderDish.Id, this.OCInventoryEx, this.lSelectedOrderPackage, 
                    ref htOtherPrint, ref htLocalPrint,isTemp);
                try
                {
                    if(!isTemp)
                        PrintOrder(this.SelectedOrderDish.Id, htOtherPrint, htLocalPrint, null, null, dtOperDate);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    this.AfterSelectDeskEx();
                    if (isTemp)
                        MessageBox.Show("存单成功");
                    else
                        MessageBox.Show("下单成功！");
                }
                
                
            }
        }
        private bool OrderCanExecute()
        {
            if (this.SelectedDeskEx != null && this.SelectedOrderDish != null && this.OCInventoryEx != null && this.OCInventoryEx.Count > 0)
                return true;
            return false;
        }
        public ICommand Order
        {
            get
            {
                return new RelayCommand(OrderExecute, OrderCanExecute);
            }
        }
        #endregion

        #region 存单
        private void SaveOrderExecute()
        {
            OrderMethod(true);
        }
        public ICommand SaveOrder
        {
            get
            {
                return new RelayCommand(SaveOrderExecute, OrderCanExecute);
            }
        }
        #endregion
        #region 加台
        private void AddDeskExecute()
        {
            if (this.SelectedDeskEx == null)
            {
                Helper.ShowErrorMsg("请选择桌台");
                return;
            }
            if (this.SelectedOrderDish == null)
            {
                Helper.ShowErrorMsg("此桌台未开台，请首先开台");
                return;
            }
            DeskExchangeWindow dew = new DeskExchangeWindow();
            if (dew.ShowDialog().GetValueOrDefault())
            {
                string DeskNo = dew.txtDeskNo.Text;
                DXInfo.Models.Desks desk = this.lDeskEx.Where(w => w.Code == DeskNo).FirstOrDefault();
                if (desk == null)
                {
                    MessageBox.Show("输入的桌台号无效！");
                    return;
                }
                try
                {
                    this.DeskManageFacade.dtOperDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));//DateTime.Now;
                    this.DeskManageFacade.AddDesk(desk.Id, this.SelectedOrderDish.Id);
                    Helper.ShowSuccMsg("加台成功");
                    this.AfterSelectRoom();
                    this.AfterSelectDeskEx();
                }
                catch (Exception ex)
                {
                    Helper.ShowErrorMsg(ex.Message);
                    Helper.HandelException(ex);
                }
            }
        }
        private bool AddDeskCanExecute()
        {
            if (this.SelectedOrderDish != null && this.SelectedDeskEx != null)
                return true;
            return false;
        }
        public ICommand AddDesk
        {
            get
            {
                return new RelayCommand(AddDeskExecute, AddDeskCanExecute);
            }
        }
        #endregion

        #region 重新打印
        private void RepeatPrintExecute()
        {
            if (this.SelectedOrderDish != null)
            {
                Hashtable htOtherPrint = new Hashtable();
                Hashtable htLocalPrint = new Hashtable();
                AddOrderMenuPrint(this.SelectedOrderDish.Id,ref htOtherPrint,ref htLocalPrint,PrintType.Repeat);
                PrintOrder(this.SelectedOrderDish.Id, htOtherPrint,htLocalPrint, null, null, DateTime.Now);
            }
        }
        private bool RepeatPrintCanExecute()
        {
            if (this.SelectedOrderDish != null)
                return true;
            return false;
        }
        public ICommand RepeatPrint
        {
            get
            {
                return new RelayCommand(RepeatPrintExecute, RepeatPrintCanExecute);
            }
        }
        #endregion        

        #endregion

        #region dispose
        public override void Cleanup()
        {
            base.Cleanup();
            if (this.RefreshDeskTimer != null)
            {
                this.RefreshDeskTimer.Tick -= new EventHandler(this.RefreshDeskTimerCallback);
                this.RefreshDeskTimer = null;
            }
            //if (this.DeskManageFacade != null)
            //{
            //    this.DeskManageFacade.Dispose();
            //    this.DeskManageFacade = null;
            //}
        }
        #endregion
    }
}
