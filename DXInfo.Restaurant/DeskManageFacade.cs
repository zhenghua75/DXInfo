using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Collections;
using DXInfo.Models;
using DXInfo.Data.Contracts;
using AutoMapper;
using System.Collections.ObjectModel;
//using FairiesCoolerCash.Business;

namespace DXInfo.Restaurant
{
    //public static class ExtensionMethods
    //{
    //    public static int Remove<T>(
    //        this ObservableCollection<T> coll, Func<T, bool> condition)
    //    {
    //        var itemsToRemove = coll.Where(condition).ToList();

    //        foreach (var itemToRemove in itemsToRemove)
    //        {
    //            coll.Remove(itemToRemove);
    //        }

    //        return itemsToRemove.Count;
    //    }
    //}
    public class DeskManageFacade
    {
        private IFairiesMemberManageUow uow;
        private Guid deptId;
        private Guid userId;
        public DateTime dtOperDate { get; set; }
        public DeskManageFacade(IFairiesMemberManageUow uow,Guid deptId,Guid userId)
        {
            this.uow = uow;
            this.deptId = deptId;
            this.userId = userId;
            //this.dtOperDate = dtOperDate;
            Mapper.CreateMap<DXInfo.Models.OrderDishes, DXInfo.Models.OrderDishesHis>();
            Mapper.CreateMap<DXInfo.Models.OrderBooks, DXInfo.Models.OrderBooksHis>();
            Mapper.CreateMap<DXInfo.Models.OrderBookDeskes, DXInfo.Models.OrderBookDeskesHis>();
            Mapper.CreateMap<DXInfo.Models.OrderDeskes, DXInfo.Models.OrderDeskesHis>();
            Mapper.CreateMap<DXInfo.Models.OrderMenus, DXInfo.Models.OrderMenusHis>();
            Mapper.CreateMap<DXInfo.Models.OrderPackages, DXInfo.Models.OrderPackagesHis>();
        }

        #region 公共方法
        #region 检查桌台是否预订
        public bool CheckDeskIsBook(Guid deskId,DateTime dtBeginDate,DateTime dtEndDate)
        {
            int count = (from d in uow.OrderBookDeskes.GetAll()
                         join o in uow.OrderBooks.GetAll() on d.OrderBookId equals o.Id into od
                         from ods in od.DefaultIfEmpty()
                         where d.DeskId == deskId &&
                         ods.Status == (int)DXInfo.Models.OrderBookStatus.Booked &&
                         d.Status == (int)DXInfo.Models.OrderBookDeskStatus.Booked &&
                         (ods.BookBeginDate >= dtBeginDate && ods.BookEndDate <= dtEndDate)
                         select d).Count();
            return count > 0;
        }
        public DXInfo.Models.OrderBookDeskes GetBookDesk(Guid deskId)
        {
            DXInfo.Models.OrderBookDeskes obd = (from d in uow.OrderBookDeskes.GetAll()
                         join o in uow.OrderBooks.GetAll() on d.OrderBookId equals o.Id into od
                         from ods in od.DefaultIfEmpty()
                         where d.DeskId == deskId &&
                         ods.Status == (int)DXInfo.Models.OrderBookStatus.Booked &&
                         d.Status == (int)DXInfo.Models.OrderBookDeskStatus.Booked &&
                         ods.BookBeginDate <= DateTime.Now &&
                         ods.BookEndDate >= DateTime.Now
                         select d).FirstOrDefault();
            return obd;
        }
        #endregion

        public void cleanData()
        {
            
                var orderDishCanceld = (from d in uow.OrderDishes.GetAll()
                                        where d.Status == (int)DXInfo.Models.OrderDishStatus.Canceled
                                        orderby d.CreateDate descending
                                        select d.Id).Take(100).Distinct().ToList();
                foreach (Guid orderId in orderDishCanceld)
                {
                    CancelOpen(orderId);
                }

                var orderDishCheckouted = (from d in uow.OrderDishes.GetAll()
                                           where d.Status == (int)DXInfo.Models.OrderDishStatus.Checkouted
                                           orderby d.CreateDate descending
                                           select d.Id).Take(100).Distinct().ToList();
                foreach (Guid orderId in orderDishCheckouted)
                {
                    CancelChecked(orderId);
                }

                var orders = (from d in uow.OrderDeskes.GetAll()
                              join d1 in uow.OrderDishes.GetAll() on d.OrderId equals d1.Id into dd1
                              from dd1s in dd1.DefaultIfEmpty()
                              where d.Status == (int)DXInfo.Models.OrderDeskStatus.InUse &&
                                  dd1s.Id == null
                              orderby d.CreateDate descending
                              select d.OrderId).Take(100).Distinct().ToList();
                foreach (Guid orderId in orders)
                {
                    CancelNothing(orderId);
                }          
        }
        public bool CheckDeskIsUse(Guid deskId)
        {
            int count = (from d in uow.OrderDeskes.GetAll()
                          join d1 in uow.OrderDishes.GetAll() on d.OrderId equals d1.Id into dd1
                          from dd1s in dd1.DefaultIfEmpty()
                          where d.Status == (int)DXInfo.Models.OrderDeskStatus.InUse &&
                          d.DeskId == deskId&&
                              dd1s.Id != null
                         select d).Count();

            //int count = (from d in uow.OrderDeskes.GetAll()
            //             where d.Status == (int)DXInfo.Models.OrderDeskStatus.InUse &&
            //             d.DeskId == deskId
            //             select d).Count();
            return count > 0;
        }
        public void AddPrint(ref Hashtable htOtherPrint,ref Hashtable htLocalPrint, DXInfo.Models.InventoryEx iex,DXInfo.Models.PrintType pt)
        {
            if (!htOtherPrint.Contains(pt))
            {
                htOtherPrint[pt] = new Hashtable();
            }
            if (!htLocalPrint.Contains(pt))
            {
                htLocalPrint[pt] = new Hashtable();
            }
            Hashtable ht1 = htOtherPrint[pt] as Hashtable;
            Hashtable ht2 = htLocalPrint[pt] as Hashtable;

            ObservableCollection<InventoryEx> lie;
            if (!string.IsNullOrEmpty(iex.Printer))
            {
                if (ht1.Contains(iex.Printer))
                {
                    lie = ht1[iex.Printer] as ObservableCollection<InventoryEx>;
                    lie.Add(iex);
                    ht1[iex.Printer] = lie;
                }
                else
                {
                    lie = new ObservableCollection<InventoryEx>();
                    lie.Add(iex);
                    ht1.Add(iex.Printer, lie);
                }
            }
            if (ht2.Contains("LocalPrint"))
            {
                lie = ht2["LocalPrint"] as ObservableCollection<InventoryEx>;
                lie.Add(iex);
                ht2["LocalPrint"] = lie;
            }
            else
            {
                lie = new ObservableCollection<InventoryEx>();
                lie.Add(iex);
                ht2.Add("LocalPrint", lie);
            }

        }
        private void Order_OrderPackage(DXInfo.Models.OrderPackages op,bool isTemp = false)
        {
            if (op.Status == (int)DXInfo.Models.OrderMenuStatus.Normal)
            {
                op.OperDate = dtOperDate;
                if(!isTemp)
                    op.Status = (int)DXInfo.Models.OrderMenuStatus.Order;
                if (op.Id == Guid.Empty)
                {
                    uow.OrderPackages.Add(op);
                    uow.Commit();
                }
                else
                {
                    uow.OrderPackages.Update(op);
                }
                DXInfo.Models.OrderPackagesHis packagesHis =
                    Mapper.Map<DXInfo.Models.OrderPackagesHis>(op);
                packagesHis.LinkId = op.Id;
                uow.OrderPackagesHis.Add(packagesHis);
            }
        }
        private void Order_OrderDish(DXInfo.Models.OrderDishes odish,bool isTemp = false)
        {

            if (odish.Status == (int)DXInfo.Models.OrderDishStatus.Opened)
            {
                if (!isTemp)
                {
                    odish.Status = (int)DXInfo.Models.OrderDishStatus.Ordered;
                }
                odish.UserId = userId;
                odish.DeptId = deptId;
                uow.OrderDishes.Update(odish);

                DXInfo.Models.OrderDishesHis odHis = Mapper.Map<DXInfo.Models.OrderDishesHis>(odish);
                odHis.LinkId = odish.Id;
                odHis.CreateDate = dtOperDate;
                uow.OrderDishesHis.Add(odHis);
            }
        }
        private void Order_OrderMenu_UpdateOrderMenu(DXInfo.Models.OrderMenus orderMenu,
            DXInfo.Models.InventoryEx iex,bool isTemp)
        {

            orderMenu.Quantity = iex.Quantity;
            orderMenu.Amount = iex.Amount;
            orderMenu.Comment = iex.Comment;
            orderMenu.OrderUserId = userId;
            orderMenu.OrderCreateDate = dtOperDate;
            orderMenu.Oper = userId;
            orderMenu.OperDate = dtOperDate;
            if (!isTemp)
            {
                if (orderMenu.Quantity > orderMenu.MenuQuantity)
                {
                    orderMenu.Status = (int)DXInfo.Models.OrderMenuStatus.Order;
                }
                else
                {
                    orderMenu.Status = (int)DXInfo.Models.OrderMenuStatus.Out;
                }
            }
            
            uow.OrderMenus.Update(orderMenu);

            DXInfo.Models.OrderMenusHis menuHis = Mapper.Map<DXInfo.Models.OrderMenusHis>(orderMenu);
            menuHis.LinkId = orderMenu.Id;
            uow.OrderMenusHis.Add(menuHis);
        }
        #endregion

        #region 预订

        #region 预订
        /// <summary>
        /// 预订
        /// </summary>
        public void Book(Guid deskId, int quantity, string linkName, string linkPhone, 
            DateTime bookBeginDate, DateTime bookEndDate)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                if (CheckDeskIsBook(deskId,bookBeginDate,bookEndDate)) throw new Exception("此桌台已预定");
                if (bookBeginDate <= dtOperDate && bookEndDate >= dtOperDate)
                {
                    if (CheckDeskIsUse(deskId)) throw new Exception("此桌台已在用");
                }
                DXInfo.Models.OrderBooks orderBook = new DXInfo.Models.OrderBooks();
                orderBook.CreateDate = dtOperDate;
                orderBook.DeptId = deptId;
                orderBook.UserId = userId;
                orderBook.Quantity = quantity;
                orderBook.Customer = linkName;
                orderBook.LinkPhone = linkPhone;
                orderBook.BookBeginDate = bookBeginDate;
                orderBook.BookEndDate = bookEndDate;
                orderBook.Status = (int)DXInfo.Models.OrderBookStatus.Booked;
                uow.OrderBooks.Add(orderBook);

                uow.Commit();

                DXInfo.Models.OrderBooksHis orderBookHis = Mapper.Map<DXInfo.Models.OrderBooksHis>(orderBook);
                orderBookHis.LinkId = orderBook.Id;
                uow.OrderBooksHis.Add(orderBookHis);

                DXInfo.Models.OrderBookDeskes desk = new DXInfo.Models.OrderBookDeskes();
                desk.DeskId = deskId;
                desk.OrderBookId = orderBook.Id;
                desk.CreateDate = dtOperDate;
                desk.Status = (int)DXInfo.Models.OrderBookDeskStatus.Booked;
                desk.UserId = userId;
                uow.OrderBookDeskes.Add(desk);
                uow.Commit();

                DXInfo.Models.OrderBookDeskesHis deskHis = Mapper.Map<DXInfo.Models.OrderBookDeskesHis>(desk);
                deskHis.LinkId = desk.Id;
                deskHis.CreateDate = dtOperDate;
                uow.OrderBookDeskesHis.Add(deskHis);

                uow.Commit();

                transaction.Complete();
            }
        }
        #endregion

        #region 取消预订
        /// <summary>
        /// 取消预订
        /// </summary>
        public void CancelBook(Guid orderBookId)
        {
            DXInfo.Models.OrderBooks book = uow.OrderBooks.GetById(g=>g.Id==orderBookId);
            if (book == null)
            {
                throw new Exception("未找到预订信息，不能取消预定");
            }
            if (book.Status != (int)DXInfo.Models.OrderBookStatus.Booked)
            {
                throw new Exception("此桌台不在预定状态，不能取消预定");
            }
            book.Status = (int)DXInfo.Models.OrderBookStatus.Canceled;
            book.UserId = userId;
            book.DeptId = deptId;
            uow.OrderBooks.Update(book);

            DXInfo.Models.OrderBooksHis orderBookHis = Mapper.Map<DXInfo.Models.OrderBooksHis>(book);
            orderBookHis.LinkId = book.Id;
            orderBookHis.CreateDate = dtOperDate;
            uow.OrderBooksHis.Add(orderBookHis);

            List<DXInfo.Models.OrderBookDeskes> q = (from d in uow.OrderBookDeskes.GetAll() where d.OrderBookId == orderBookId select d).ToList();
            foreach (DXInfo.Models.OrderBookDeskes obd in q)
            {
                obd.Status = (int)DXInfo.Models.OrderBookDeskStatus.Canceled;
                obd.UserId = userId;
                uow.OrderBookDeskes.Update(obd);

                DXInfo.Models.OrderBookDeskesHis deskHis = Mapper.Map<DXInfo.Models.OrderBookDeskesHis>(obd);
                deskHis.LinkId = obd.Id;
                deskHis.CreateDate = dtOperDate;
                uow.OrderBookDeskesHis.Add(deskHis);
            }
            uow.Commit();
        }
        #endregion

        #region 开台
        /// <summary>
        /// 预定-开台
        /// </summary>
        public void OpenBook(Guid orderBookId,Guid deskId, int quantity, bool isIpad, 
            ref DXInfo.Models.OrderDishes orderDish,
            ref DXInfo.Models.OrderDeskes orderDesk)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                DXInfo.Models.OrderBooks orderBook = uow.OrderBooks.GetById(g=>g.Id==orderBookId);
                if (orderBook.Status != (int)DXInfo.Models.OrderBookStatus.Booked)
                    throw new Exception("此桌台不在预定状态，不能开台");
                if(this.CheckDeskIsUse(deskId))
                    throw new Exception("此桌台已在用，不能开台");
                orderBook.Status = (int)DXInfo.Models.OrderBookStatus.Opened;
                orderBook.DeptId = deptId;
                orderBook.UserId = userId;
                uow.OrderBooks.Update(orderBook);

                DXInfo.Models.OrderBooksHis orderBookHis = Mapper.Map<DXInfo.Models.OrderBooksHis>(orderBook);//new DXInfo.Models.OrderBooksHis();
                orderBookHis.LinkId = orderBookId;
                orderBookHis.CreateDate = dtOperDate;
                uow.OrderBooksHis.Add(orderBookHis);

                DXInfo.Models.OrderSequences os = new DXInfo.Models.OrderSequences();
                os.Fill = "0";
                uow.OrderSequences.Add(os);
                uow.Commit();

                DXInfo.Models.OrderDishes od = new DXInfo.Models.OrderDishes();
                od.OrderNo = os.Id;
                od.Quantity = quantity;//orderBook.Quantity;
                od.CreateDate = dtOperDate;
                od.DeptId = deptId;
                od.UserId = userId;
                od.IsIpad = isIpad;
                od.Status = (int)DXInfo.Models.OrderDishStatus.Opened;
                od.Comment = "OrderBooks.Id=" + orderBook.Id.ToString();
                uow.OrderDishes.Add(od);
                uow.Commit();
                orderDish = od;
                DXInfo.Models.OrderDishesHis odHis = Mapper.Map<DXInfo.Models.OrderDishesHis>(od);
                odHis.LinkId = od.Id;
                uow.OrderDishesHis.Add(odHis);

                List<DXInfo.Models.OrderBookDeskes> q = (from d in uow.OrderBookDeskes.GetAll() where d.OrderBookId == orderBook.Id select d).ToList();
                foreach (DXInfo.Models.OrderBookDeskes obd in q)
                {
                    if (obd.Status == (int)DXInfo.Models.OrderBookDeskStatus.Booked)
                    {
                        obd.Status = (int)DXInfo.Models.OrderBookDeskStatus.Canceled;
                        obd.UserId = userId;
                        uow.OrderBookDeskes.Update(obd);

                        DXInfo.Models.OrderBookDeskesHis orderBookDeskHis = Mapper.Map<DXInfo.Models.OrderBookDeskesHis>(obd);
                        orderBookDeskHis.LinkId = obd.Id;
                        orderBookDeskHis.CreateDate = dtOperDate;
                        uow.OrderBookDeskesHis.Add(orderBookDeskHis);

                        DXInfo.Models.OrderDeskes desk = new DXInfo.Models.OrderDeskes();
                        desk.DeskId = obd.DeskId;
                        desk.OrderId = od.Id;
                        desk.CreateDate = dtOperDate;
                        desk.Status = (int)DXInfo.Models.OrderDeskStatus.InUse;
                        desk.UserId = userId;
                        uow.OrderDeskes.Add(desk);
                        uow.Commit();
                        if (desk.DeskId == deskId)
                        {
                            orderDesk = desk;
                        }
                        DXInfo.Models.OrderDeskesHis deskHis = Mapper.Map<DXInfo.Models.OrderDeskesHis>(desk);
                        deskHis.LinkId = desk.Id;
                        uow.OrderDeskesHis.Add(deskHis);
                    }
                }
                uow.Commit();
                transaction.Complete();
            }
        }
        #endregion

        #region 撤台
        /// <summary>
        /// 预订-撤台
        /// </summary>
        public void CancelBookDesk(Guid orderBookId, Guid orderBookDeskId)
        {
            List<DXInfo.Models.OrderBookDeskes> q = (from d in uow.OrderBookDeskes.GetAll()
                                                     where d.OrderBookId == orderBookId &&
                                                         d.Status == 0
                                                     select d).ToList();
            if (q.Count == 1)
            {
                CancelBook(orderBookId);
            }
            else
            {
                DXInfo.Models.OrderBookDeskes chkDesk = uow.OrderBookDeskes.GetById(g=>g.Id==orderBookDeskId);//.GetAll().Where(w => w.Id == orderBookDesk.Id).FirstOrDefault();
                if (chkDesk.Status == (int)DXInfo.Models.OrderBookDeskStatus.Canceled) throw new Exception("此桌台已经撤销");
                chkDesk.Status = (int)DXInfo.Models.OrderBookDeskStatus.Canceled;
                chkDesk.UserId = userId;
                uow.OrderBookDeskes.Update(chkDesk);

                DXInfo.Models.OrderBookDeskesHis oldhis = Mapper.Map<DXInfo.Models.OrderBookDeskesHis>(chkDesk);
                oldhis.CreateDate = dtOperDate;
                oldhis.LinkId = chkDesk.Id;
                uow.OrderBookDeskesHis.Add(oldhis);

                uow.Commit();
            }
        }
        #endregion

        #region 换台
        /// <summary>
        /// 预定-换台
        /// </summary>
        public void ExchangeBookDesk(Guid newDeskId, Guid orderBookDeskId,DateTime dtBeginDate,DateTime dtEndDate)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                if (CheckDeskIsBook(newDeskId,dtBeginDate,dtEndDate)) throw new Exception("桌台已被预订");

                DXInfo.Models.OrderBookDeskes orderBookDesk = uow.OrderBookDeskes.GetById(g=>g.Id==orderBookDeskId);
                orderBookDesk.Status = (int)DXInfo.Models.OrderBookDeskStatus.Canceled;
                orderBookDesk.UserId = userId;
                uow.OrderBookDeskes.Update(orderBookDesk);

                DXInfo.Models.OrderBookDeskesHis oldhis = Mapper.Map<DXInfo.Models.OrderBookDeskesHis>(orderBookDesk);
                oldhis.CreateDate = dtOperDate;
                oldhis.LinkId = orderBookDesk.Id;
                uow.OrderBookDeskesHis.Add(oldhis);


                DXInfo.Models.OrderBookDeskes neworderdesk = new DXInfo.Models.OrderBookDeskes();
                neworderdesk.CreateDate = dtOperDate;
                neworderdesk.DeskId = newDeskId;
                neworderdesk.OrderBookId = orderBookDesk.OrderBookId;
                neworderdesk.Status = (int)DXInfo.Models.OrderBookDeskStatus.Booked;
                neworderdesk.UserId = userId;
                uow.OrderBookDeskes.Add(neworderdesk);

                uow.Commit();

                DXInfo.Models.OrderBookDeskesHis newhis = Mapper.Map<DXInfo.Models.OrderBookDeskesHis>(neworderdesk);
                newhis.LinkId = neworderdesk.Id;
                uow.OrderBookDeskesHis.Add(newhis);

                uow.Commit();
                transaction.Complete();
            }
        }
        #endregion

        #region 加台
        /// <summary>
        /// 预订-加台
        /// </summary>
        public void AddBookDesk(Guid newDeskId, Guid orderBookId, DateTime dtBeginDate, DateTime dtEndDate)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                if (CheckDeskIsBook(newDeskId, dtBeginDate, dtEndDate)) throw new Exception("桌台已被预订");

                DXInfo.Models.OrderBookDeskes newOrderBookDesk = new DXInfo.Models.OrderBookDeskes();
                newOrderBookDesk.CreateDate = dtOperDate;
                newOrderBookDesk.DeskId = newDeskId;
                newOrderBookDesk.OrderBookId = orderBookId;
                newOrderBookDesk.Status = 0;
                newOrderBookDesk.UserId = userId;
                uow.OrderBookDeskes.Add(newOrderBookDesk);

                uow.Commit();

                DXInfo.Models.OrderBookDeskesHis newhis = Mapper.Map<DXInfo.Models.OrderBookDeskesHis>(newOrderBookDesk);
                newhis.LinkId = newOrderBookDesk.Id;
                uow.OrderBookDeskesHis.Add(newhis);

                uow.Commit();
                transaction.Complete();
            }
        }
        #endregion

        #endregion

        #region 桌台

        #region 开台
        /// <summary>
        /// 开台
        /// </summary>
        public void Open(int quantity,Guid deskId,bool isIpad,ref DXInfo.Models.OrderDishes orderDish,
            ref DXInfo.Models.OrderDeskes orderDesk)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                if (CheckDeskIsUse(deskId)) throw new Exception("桌台已被占用");
                DXInfo.Models.OrderSequences os = new DXInfo.Models.OrderSequences();
                os.Fill = "0";
                uow.OrderSequences.Add(os);
                uow.Commit();

                DXInfo.Models.OrderDishes od = new DXInfo.Models.OrderDishes();
                od.OrderNo = os.Id;
                od.Quantity = quantity;
                od.CreateDate = dtOperDate;
                od.DeptId = deptId;
                od.UserId = userId;
                od.IsIpad = isIpad;
                od.Status = (int)DXInfo.Models.OrderDishStatus.Opened;
                uow.OrderDishes.Add(od);
                uow.Commit();

                DXInfo.Models.OrderDishesHis odHis = Mapper.Map<DXInfo.Models.OrderDishesHis>(od);
                odHis.LinkId = od.Id;
                uow.OrderDishesHis.Add(odHis);

                DXInfo.Models.OrderDeskes desk = new DXInfo.Models.OrderDeskes();
                desk.DeskId = deskId;
                desk.OrderId = od.Id;
                desk.CreateDate = dtOperDate;
                desk.Status = (int)DXInfo.Models.OrderDeskStatus.InUse;
                desk.UserId = userId;
                uow.OrderDeskes.Add(desk);
                uow.Commit();

                DXInfo.Models.OrderDeskesHis deskHis = Mapper.Map<DXInfo.Models.OrderDeskesHis>(desk);
                deskHis.LinkId = desk.Id;
                uow.OrderDeskesHis.Add(deskHis);

                uow.Commit();
                transaction.Complete();

                orderDish = od;
                orderDesk = desk;
            }
        }
        #endregion

        #region 撤单
        /// <summary>
        /// 撤单
        /// </summary>
        public void CancelOpen(Guid orderDishId)
        {
            DXInfo.Models.OrderDishes orderDish = uow.OrderDishes.GetById(g=>g.Id==orderDishId);
            if (orderDish != null)
            {
                uow.OrderDishes.Delete(orderDish);

                DXInfo.Models.OrderDishesHis orderDishHis = Mapper.Map<DXInfo.Models.OrderDishesHis>(orderDish);
                orderDishHis.Status = (int)DXInfo.Models.OrderDishStatus.Canceled;
                orderDishHis.UserId = userId;
                orderDishHis.DeptId = deptId;
                orderDishHis.LinkId = orderDish.Id;
                uow.OrderDishesHis.Add(orderDishHis);
            }
            //DXInfo.Models.OrderDishesHis odHis = Mapper.Map<DXInfo.Models.OrderDishesHis>(orderDish);
            //odHis.LinkId = orderDish.Id;
            //odHis.CreateDate = dtOperDate;
            //uow.OrderDishesHis.Add(odHis);

            List<DXInfo.Models.OrderDeskes> q = (from d in uow.OrderDeskes.GetAll()
                                                 where d.OrderId == orderDishId
                                                 select d).ToList();
            foreach (DXInfo.Models.OrderDeskes orderDesk in q)
            {
                uow.OrderDeskes.Delete(orderDesk);

                DXInfo.Models.OrderDeskesHis deskHis = Mapper.Map<DXInfo.Models.OrderDeskesHis>(orderDesk);
                deskHis.Status = (int)DXInfo.Models.OrderDeskStatus.Idle;
                deskHis.UserId = userId;
                deskHis.LinkId = orderDesk.Id;
                //deskHis.CreateDate = dtOperDate;
                uow.OrderDeskesHis.Add(deskHis);
            }
            List<DXInfo.Models.OrderMenus> lOrderMenus = (from d in uow.OrderMenus.GetAll()
                                                         where d.OrderId == orderDishId
                                                              select d).ToList();
            foreach(DXInfo.Models.OrderMenus orderMenu in lOrderMenus)
            {
                uow.OrderMenus.Delete(orderMenu);

                DXInfo.Models.OrderMenusHis menuHis = Mapper.Map<DXInfo.Models.OrderMenusHis>(orderMenu);
                menuHis.Oper=userId;
                menuHis.OperDate = this.dtOperDate;
                menuHis.LinkId = orderMenu.Id;
                menuHis.Status = (int)DXInfo.Models.OrderMenuStatus.Withdraw;
                uow.OrderMenusHis.Add(menuHis);
            }
            uow.Commit();
        }
        public void CancelChecked(Guid orderDishId)
        {
            DXInfo.Models.OrderDishes orderDish = uow.OrderDishes.GetById(g => g.Id == orderDishId);
            if (orderDish != null)
            {
                uow.OrderDishes.Delete(orderDish);

                DXInfo.Models.OrderDishesHis orderDishHis = Mapper.Map<DXInfo.Models.OrderDishesHis>(orderDish);
                orderDishHis.Status = (int)DXInfo.Models.OrderDishStatus.Checkouted;
                orderDishHis.UserId = userId;
                orderDishHis.DeptId = deptId;
                orderDishHis.LinkId = orderDish.Id;
                uow.OrderDishesHis.Add(orderDishHis);
            }
            //DXInfo.Models.OrderDishesHis odHis = Mapper.Map<DXInfo.Models.OrderDishesHis>(orderDish);
            //odHis.LinkId = orderDish.Id;
            //odHis.CreateDate = dtOperDate;
            //uow.OrderDishesHis.Add(odHis);

            List<DXInfo.Models.OrderDeskes> q = (from d in uow.OrderDeskes.GetAll()
                                                 where d.OrderId == orderDishId
                                                 select d).ToList();
            foreach (DXInfo.Models.OrderDeskes orderDesk in q)
            {
                uow.OrderDeskes.Delete(orderDesk);

                DXInfo.Models.OrderDeskesHis deskHis = Mapper.Map<DXInfo.Models.OrderDeskesHis>(orderDesk);
                deskHis.Status = (int)DXInfo.Models.OrderDeskStatus.Idle;
                deskHis.UserId = userId;
                deskHis.LinkId = orderDesk.Id;
                //deskHis.CreateDate = dtOperDate;
                uow.OrderDeskesHis.Add(deskHis);
            }
            List<DXInfo.Models.OrderMenus> lOrderMenus = (from d in uow.OrderMenus.GetAll()
                                                          where d.OrderId == orderDishId
                                                          select d).ToList();
            foreach (DXInfo.Models.OrderMenus orderMenu in lOrderMenus)
            {
                uow.OrderMenus.Delete(orderMenu);

                DXInfo.Models.OrderMenusHis menuHis = Mapper.Map<DXInfo.Models.OrderMenusHis>(orderMenu);
                menuHis.Oper = userId;
                menuHis.OperDate = this.dtOperDate;
                menuHis.LinkId = orderMenu.Id;
                menuHis.Status = (int)DXInfo.Models.OrderMenuStatus.Checkout;
                uow.OrderMenusHis.Add(menuHis);
            }
            uow.Commit();
        }
        public void CancelNothing(Guid orderDishId)
        {
            DXInfo.Models.OrderDishes orderDish = uow.OrderDishes.GetById(g => g.Id == orderDishId);
            if (orderDish!=null)
            {
                uow.OrderDishes.Delete(orderDish);

                DXInfo.Models.OrderDishesHis orderDishHis = Mapper.Map<DXInfo.Models.OrderDishesHis>(orderDish);
                orderDishHis.Status = (int)DXInfo.Models.OrderDishStatus.Checkouted;
                orderDishHis.UserId = userId;
                orderDishHis.DeptId = deptId;
                orderDishHis.LinkId = orderDish.Id;
                uow.OrderDishesHis.Add(orderDishHis);
            }
            //DXInfo.Models.OrderDishesHis odHis = Mapper.Map<DXInfo.Models.OrderDishesHis>(orderDish);
            //odHis.LinkId = orderDish.Id;
            //odHis.CreateDate = dtOperDate;
            //uow.OrderDishesHis.Add(odHis);

            List<DXInfo.Models.OrderDeskes> q = (from d in uow.OrderDeskes.GetAll()
                                                 where d.OrderId == orderDishId
                                                 select d).ToList();
            foreach (DXInfo.Models.OrderDeskes orderDesk in q)
            {
                uow.OrderDeskes.Delete(orderDesk);

                DXInfo.Models.OrderDeskesHis deskHis = Mapper.Map<DXInfo.Models.OrderDeskesHis>(orderDesk);
                //deskHis.Status = orderDesk.Status;//(int)DXInfo.Models.OrderDeskStatus.Idle;
                deskHis.UserId = userId;
                deskHis.LinkId = orderDesk.Id;
                //deskHis.CreateDate = dtOperDate;
                uow.OrderDeskesHis.Add(deskHis);
            }
            List<DXInfo.Models.OrderMenus> lOrderMenus = (from d in uow.OrderMenus.GetAll()
                                                          where d.OrderId == orderDishId
                                                          select d).ToList();
            foreach (DXInfo.Models.OrderMenus orderMenu in lOrderMenus)
            {
                uow.OrderMenus.Delete(orderMenu);

                DXInfo.Models.OrderMenusHis menuHis = Mapper.Map<DXInfo.Models.OrderMenusHis>(orderMenu);
                menuHis.Oper = userId;
                menuHis.OperDate = this.dtOperDate;
                menuHis.LinkId = orderMenu.Id;
                //menuHis.Status = (int)DXInfo.Models.OrderMenuStatus.Checkout;
                uow.OrderMenusHis.Add(menuHis);
            }
            uow.Commit();
        }
        #endregion

        #region 撤台
        /// <summary>
        /// 撤台
        /// </summary>
        /// <param name="orderDishId"></param>
        /// <param name="orderDeskId"></param>
        public void CancelOpenDesk(Guid orderDishId, Guid orderDeskId)
        {
            DXInfo.Models.OrderDeskes chkDesk = uow.OrderDeskes.GetById(g=>g.Id==orderDeskId);
            if (chkDesk.Status == (int)DXInfo.Models.OrderDeskStatus.Idle) throw new Exception("此桌台已经撤销");

            List<DXInfo.Models.OrderDeskes> q = (from d in uow.OrderDeskes.GetAll()
                                                 where d.OrderId == orderDishId && d.Status == 0
                                                 select d).ToList();
            if (q.Count == 1)
            {
                CancelOpen(orderDishId);
            }
            else
            {
                
                uow.OrderDeskes.Delete(chkDesk);

                DXInfo.Models.OrderDeskesHis oldhis = Mapper.Map<DXInfo.Models.OrderDeskesHis>(chkDesk);
                oldhis.CreateDate = dtOperDate;
                oldhis.LinkId = chkDesk.Id;
                oldhis.Status = (int)DXInfo.Models.OrderDeskStatus.Idle;
                oldhis.UserId = userId;
                uow.OrderDeskesHis.Add(oldhis);

                uow.Commit();
            }
        }
        #endregion

        #region 换台
        /// <summary>
        /// 换台
        /// </summary>
        public void ExchangeDesk(Guid orderDishId,Guid newDeskId, Guid orderDeskId)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                if (CheckDeskIsUse(newDeskId)) throw new Exception("桌台已被占用");

                DXInfo.Models.OrderDeskes od = uow.OrderDeskes.GetById(g => g.Id == orderDeskId);
                od.Status = (int)DXInfo.Models.OrderDeskStatus.Idle;
                od.UserId = userId;
                uow.OrderDeskes.Delete(od);

                DXInfo.Models.OrderDeskesHis oldhis = Mapper.Map<DXInfo.Models.OrderDeskesHis>(od);
                oldhis.CreateDate = dtOperDate;
                oldhis.LinkId = od.Id;
                uow.OrderDeskesHis.Add(oldhis);


                DXInfo.Models.OrderDeskes neworderdesk = new DXInfo.Models.OrderDeskes();
                neworderdesk.CreateDate = dtOperDate;
                neworderdesk.DeskId = newDeskId;
                neworderdesk.OrderId = orderDishId;
                neworderdesk.Status = (int)DXInfo.Models.OrderDeskStatus.InUse;
                neworderdesk.UserId = userId;
                uow.OrderDeskes.Add(neworderdesk);

                uow.Commit();

                DXInfo.Models.OrderDeskesHis newhis = Mapper.Map<DXInfo.Models.OrderDeskesHis>(neworderdesk);                
                newhis.LinkId = neworderdesk.Id;
                uow.OrderDeskesHis.Add(newhis);

                uow.Commit();
                transaction.Complete();
            }
        }
        #endregion

        #region 加台
        /// <summary>
        /// 加台
        /// </summary>
        public void AddDesk(Guid newDeskId, Guid orderDishId)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                if (CheckDeskIsUse(newDeskId)) throw new Exception("桌台已被占用");

                DXInfo.Models.OrderDeskes newOrderDesk = new DXInfo.Models.OrderDeskes();
                newOrderDesk.CreateDate = dtOperDate;
                newOrderDesk.DeskId = newDeskId;
                newOrderDesk.OrderId = orderDishId;
                newOrderDesk.Status = 0;
                newOrderDesk.UserId = userId;
                uow.OrderDeskes.Add(newOrderDesk);

                uow.Commit();

                DXInfo.Models.OrderDeskesHis newhis = Mapper.Map<DXInfo.Models.OrderDeskesHis>(newOrderDesk);
                newhis.LinkId = newOrderDesk.Id;

                uow.OrderDeskesHis.Add(newhis);
                uow.Commit();
                transaction.Complete();
            }
        }
        #endregion

        #region 并单
        public void MergeOrderDish(Guid newDeskId, Guid orderDishId)
        {
            //List<Guid> lNewOrderDishId =
            //    (from d in uow.OrderDeskes.GetAll()
            //     where d.DeskId == newDeskId &&
            //     d.Status == (int)DXInfo.Models.OrderDeskStatus.InUse
            //     select d.OrderId).Distinct().ToList();
            List<Guid> lNewOrderDishId = (from d in uow.OrderDeskes.GetAll()
                                          join d1 in uow.OrderDishes.GetAll() on d.OrderId equals d1.Id into dd1
                                          from dd1s in dd1.DefaultIfEmpty()
                                          where d.Status == (int)DXInfo.Models.OrderDeskStatus.InUse &&
                                              d.DeskId == newDeskId &&
                                              dd1s.Id != null
                                          select d.OrderId).Distinct().ToList();

            foreach (Guid newOrderDishId in lNewOrderDishId)
            {
                DXInfo.Models.OrderDishes newOrderDish = uow.OrderDishes.GetById(g=>g.Id==newOrderDishId);
                if (newOrderDish.Status == (int)DXInfo.Models.OrderDishStatus.Opened ||
                    newOrderDish.Status == (int)DXInfo.Models.OrderDishStatus.Ordered)
                {
                    newOrderDish.Status = (int)DXInfo.Models.OrderDishStatus.Canceled;
                    newOrderDish.UserId = userId;
                    newOrderDish.DeptId = deptId;
                    uow.OrderDishes.Delete(newOrderDish);

                    DXInfo.Models.OrderDishesHis odHis = Mapper.Map<DXInfo.Models.OrderDishesHis>(newOrderDish);
                    odHis.LinkId = newOrderDish.Id;
                    odHis.CreateDate = dtOperDate;
                    uow.OrderDishesHis.Add(odHis);

                    List<DXInfo.Models.OrderMenus> lNewOrderMenu =
                        (from d in uow.OrderMenus.GetAll()
                         where d.OrderId == newOrderDishId
                         select d).ToList();

                    foreach (DXInfo.Models.OrderMenus om in lNewOrderMenu)
                    {
                        om.OrderId = orderDishId;
                        om.UserId = userId;
                        uow.OrderMenus.Update(om);

                        DXInfo.Models.OrderMenusHis menuHis = Mapper.Map<DXInfo.Models.OrderMenusHis>(om);
                        menuHis.LinkId = om.Id;
                        menuHis.CreateDate = dtOperDate;
                        uow.OrderMenusHis.Add(menuHis);
                    }
                    List<DXInfo.Models.OrderDeskes> lNewOrderDesk =
                        (from d in uow.OrderDeskes.GetAll()
                         where d.OrderId == newOrderDishId &&
                         d.Status == (int)DXInfo.Models.OrderDeskStatus.InUse
                         select d).ToList();

                    foreach (DXInfo.Models.OrderDeskes newOrderDesk in lNewOrderDesk)
                    {
                        newOrderDesk.OrderId = orderDishId;
                        newOrderDesk.UserId = userId;
                        uow.OrderDeskes.Update(newOrderDesk);

                        DXInfo.Models.OrderDeskesHis oldhis = Mapper.Map<DXInfo.Models.OrderDeskesHis>(newOrderDesk);
                        oldhis.CreateDate = dtOperDate;
                        oldhis.LinkId = newOrderDesk.Id;
                        oldhis.UserId = userId;
                        uow.OrderDeskesHis.Add(oldhis);
                    }

                    List<DXInfo.Models.OrderPackages> lNewOrderPackage =
                        (from d in uow.OrderPackages.GetAll()
                         where d.OrderId == newOrderDishId
                         select d).ToList();
                    foreach (DXInfo.Models.OrderPackages newOrderPackage in lNewOrderPackage)
                    {
                        newOrderPackage.OrderId = orderDishId;
                        newOrderPackage.Oper = userId;
                        newOrderPackage.OperDate = dtOperDate;
                        uow.OrderPackages.Update(newOrderPackage);

                        DXInfo.Models.OrderPackagesHis opHis = Mapper.Map<DXInfo.Models.OrderPackagesHis>(newOrderPackage);
                        opHis.LinkId = newOrderPackage.Id;
                        uow.OrderPackagesHis.Add(opHis);
                    }
                }
            }
            uow.Commit();
        }
        #endregion        

        #region 下单
        private void Order_OrderInvPrice(DXInfo.Models.InvPrice invPrice, Guid orderMenuId)
        {
            DXInfo.Models.OrderInvPrice oInvPrice = Mapper.Map<DXInfo.Models.OrderInvPrice>(invPrice);
            oInvPrice.InvPriceId = invPrice.Id;
            oInvPrice.OrderMenuId = orderMenuId;
            uow.OrderInvPrice.Add(oInvPrice);
            uow.Commit();
        }
        private void Order_OrderMenu(DXInfo.Models.InventoryEx iex,DXInfo.Models.OrderDishStatus orderDishStatus,
            ref Hashtable htOtherPrint,
            ref Hashtable htLocalPrint,
            bool isTemp = false)
        {
            if (iex.OrderMenuId != Guid.Empty)
            {
                DXInfo.Models.OrderMenus orderMenu = uow.OrderMenus.GetById(g=>g.Id==iex.OrderMenuId);
                if (orderMenu != null)
                {
                    if (orderMenu.Status == (int)DXInfo.Models.OrderMenuStatus.Normal)
                    {
                        if(!isTemp)
                            orderMenu.Status = (int)DXInfo.Models.OrderMenuStatus.Order;
                        Order_OrderMenu_UpdateOrderMenu(orderMenu, iex,isTemp);
                        iex.Status = isTemp?(int)DXInfo.Models.OrderMenuStatus.Normal:(int)DXInfo.Models.OrderMenuStatus.Order;

                        if (!isTemp)
                        {
                            if (orderDishStatus == OrderDishStatus.Opened)
                            {
                                AddPrint(ref htOtherPrint, ref htLocalPrint, iex, PrintType.Order);
                            }
                            else
                            {
                                AddPrint(ref htOtherPrint, ref htLocalPrint, iex, PrintType.Menu);
                            }
                        }
                    }
                    else
                    {
                        if ((orderMenu.Quantity != iex.Quantity || orderMenu.Comment != iex.Comment) &&
                            orderMenu.Status != (int)DXInfo.Models.OrderMenuStatus.Withdraw &&
                            orderMenu.Status != (int)DXInfo.Models.OrderMenuStatus.ReturnAfterOut)
                        {
                            decimal backQuantity = iex.Quantity - orderMenu.Quantity;
                            if (backQuantity != 0)
                            {
                                orderMenu.BackQuantity = backQuantity;
                                orderMenu.BackReseaon = "加减单";
                                orderMenu.BackCreateDate = dtOperDate;
                                orderMenu.BackUserId = userId;

                                if (backQuantity > 0)
                                {
                                    AddPrint(ref htOtherPrint, ref htLocalPrint, iex, PrintType.Add);
                                }
                                else
                                {
                                    AddPrint(ref htOtherPrint, ref htLocalPrint, iex, PrintType.Sub);
                                }
                            }
                            else
                            {
                                AddPrint(ref htOtherPrint, ref htLocalPrint, iex, PrintType.Taste);
                            }
                            Order_OrderMenu_UpdateOrderMenu(orderMenu, iex,false);
                        }
                    }
                    
                }
            }
            else
            {
                DXInfo.Models.OrderMenus orderMenu = Mapper.Map<DXInfo.Models.OrderMenus>(iex);
                orderMenu.InventoryId = iex.Id;
                orderMenu.CreateDate = dtOperDate;
                orderMenu.UserId = userId;
                orderMenu.Oper = userId;
                orderMenu.OperDate = dtOperDate;
                orderMenu.Status = isTemp?(int)DXInfo.Models.OrderMenuStatus.Normal:(int)DXInfo.Models.OrderMenuStatus.Order;
                orderMenu.Price = iex.SalePrice;
                orderMenu.OrderUserId = userId;
                orderMenu.OrderCreateDate = dtOperDate;
                uow.OrderMenus.Add(orderMenu);

                uow.Commit();

                iex.OrderMenuId = orderMenu.Id;
                iex.Status = isTemp?(int)DXInfo.Models.OrderMenuStatus.Normal:(int)DXInfo.Models.OrderMenuStatus.Order;
                DXInfo.Models.OrderMenusHis menuHis = Mapper.Map<DXInfo.Models.OrderMenusHis>(orderMenu);
                menuHis.LinkId = orderMenu.Id;
                uow.OrderMenusHis.Add(menuHis);

                if (iex.InvPrice != null)
                {
                    Order_OrderInvPrice(iex.InvPrice, orderMenu.Id);
                }
                if (orderDishStatus == OrderDishStatus.Opened)
                {
                    AddPrint(ref htOtherPrint, ref htLocalPrint, iex, PrintType.Order);
                }
                else
                {
                    AddPrint(ref htOtherPrint, ref htLocalPrint, iex, PrintType.Menu);
                }
            }
        }
        public void Order(Guid orderDishId,
            ObservableCollection<DXInfo.Models.InventoryEx> OCInventoryEx,
            List<DXInfo.Models.OrderPackages> lOrderPackage,
            ref Hashtable htOtherPrint,
            ref Hashtable htLocalPrint,
            bool isTemp = false)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                DXInfo.Models.OrderDishes orderDish = uow.OrderDishes.GetById(g=>g.Id==orderDishId);
                DXInfo.Models.OrderDishStatus orderDishStatus = (DXInfo.Models.OrderDishStatus)orderDish.Status;
                if (!(orderDish.Status == (int)DXInfo.Models.OrderDishStatus.Opened ||
                    orderDish.Status == (int)DXInfo.Models.OrderDishStatus.Ordered))
                    throw new Exception("桌台已撤销或已结账");

                List<DXInfo.Models.OrderMenus> lOrderMenuAll = uow.OrderMenus.GetAll()
                    .Where(w => w.OrderId == orderDishId).ToList();

                List<DXInfo.Models.OrderMenus> lOrderMenu = lOrderMenuAll
                    .Where(w => w.Status == (int)DXInfo.Models.OrderMenuStatus.Normal
                    ).ToList();
                
                foreach (DXInfo.Models.InventoryEx iex in OCInventoryEx.ToList())
                {
                    if (iex.OrderMenuId != null &&
                        iex.OrderMenuId != Guid.Empty &&
                        lOrderMenuAll.Where(w => w.Id == iex.OrderMenuId).Count() == 0)
                    {
                        OCInventoryEx.Remove(iex);
                    }
                }
                lOrderMenu.RemoveAll(delegate(DXInfo.Models.OrderMenus om) { return OCInventoryEx.Where(w => w.OrderMenuId == om.Id).Count() > 0; });
                
                foreach (DXInfo.Models.OrderMenus om in lOrderMenu)
                {
                    if (om.Status == (int)DXInfo.Models.OrderMenuStatus.Normal)
                    {
                        DXInfo.Models.InventoryEx iex = Mapper.Map<DXInfo.Models.InventoryEx>(om);
                        DXInfo.Models.Inventory ie = uow.Inventory.GetById(g=>g.Id==om.InventoryId);
                        iex.Code = ie.Code;
                        iex.Name = ie.Name;
                        iex.OrderMenuId = om.Id;
                        OCInventoryEx.Add(iex);
                    }
                }
                foreach (DXInfo.Models.InventoryEx iex in OCInventoryEx)
                {
                    Order_OrderMenu(iex, orderDishStatus, ref htOtherPrint, ref htLocalPrint,isTemp);
                }
                if (lOrderPackage != null)
                {
                    List<DXInfo.Models.OrderPackages> lOldOrderPackage = uow.OrderPackages.GetAll().Where(w => w.OrderId == orderDishId).ToList();
                    lOldOrderPackage.RemoveAll(delegate(DXInfo.Models.OrderPackages op) { return lOrderPackage.Where(w => w.Id == op.Id).Count() > 0; });
                    lOrderPackage.AddRange(lOldOrderPackage);

                    foreach (DXInfo.Models.OrderPackages op in lOrderPackage.Where(w => w.Status == (int)DXInfo.Models.OrderMenuStatus.Normal))
                    {
                        Order_OrderPackage(op,isTemp);
                    }
                }
                Order_OrderDish(orderDish,isTemp);
                uow.Commit();
                transaction.Complete();
            }
        }
        #endregion

        #endregion

        public string GetOrderDeskCodes(IFairiesMemberManageUow uow, Guid orderId)
        {
            string deskCodes = "";
            List<DXInfo.Models.OrderDeskes> lorderdeskes = uow.OrderDeskes.GetAll().Where(w => w.OrderId == orderId && w.Status == 0).ToList();
            foreach (DXInfo.Models.OrderDeskes od in lorderdeskes)
            {
                DXInfo.Models.Desks desk = uow.Desks.GetById(g=>g.Id==od.DeskId);//.Where(w => w.Id == od.DeskId).FirstOrDefault();
                deskCodes += desk.Code + ",";
            }
            deskCodes = deskCodes.Substring(0, deskCodes.Length - 1);
            return deskCodes;
        }

        public DXInfo.Models.OrderBooks GetBookByDesk(Guid deskId)
        {
            var q2 = from d in uow.OrderBookDeskes.GetAll()
                     join o in uow.OrderBooks.GetAll() on d.OrderBookId equals o.Id into od
                     from ods in od.DefaultIfEmpty()
                     where d.DeskId == deskId && ods.Status == 0 && d.Status == 0 && ods.BookBeginDate <= DateTime.Now && ods.BookEndDate >= DateTime.Now
                     select d;
            var q3 = q2.ToList();
            if (q3.Count > 0)
            {
                var q4 = q3[0];
                var q5 = uow.OrderBooks.GetAll().Where(w => w.Id == q4.OrderBookId).FirstOrDefault();
                return q5;
            }
            return null;
        }
        #region 菜品

        #region 下单
        //正常、退菜状态下单后成下单状态
        private void MenuOrder_OrderMenu(DXInfo.Models.InventoryEx iex,
            ref Hashtable htOtherPrint,
            ref Hashtable htLocalPrint,
            bool isTemp)
        {
            //iex.Status = (int)DXInfo.Models.OrderMenuStatus.Order;
            if (iex.OrderMenuId != Guid.Empty)
            {
                DXInfo.Models.OrderMenus orderMenu = uow.OrderMenus.GetById(g=>g.Id==iex.OrderMenuId);
                orderMenu.BackQuantity = 0;
                orderMenu.BackReseaon = "";
                orderMenu.BackCreateDate = null;
                orderMenu.BackUserId = null;
                if (orderMenu != null)
                {
                    if (orderMenu.Status == (int)DXInfo.Models.OrderMenuStatus.Normal||
                        orderMenu.Status == (int)DXInfo.Models.OrderMenuStatus.Withdraw||
                        orderMenu.Status == (int)DXInfo.Models.OrderMenuStatus.ReturnAfterOut)
                    {
                        if(!isTemp)
                            AddPrint(ref htOtherPrint, ref htLocalPrint, iex, PrintType.Menu);
                        orderMenu.Status = isTemp?(int)DXInfo.Models.OrderMenuStatus.Normal:(int)DXInfo.Models.OrderMenuStatus.Order;
                        Order_OrderMenu_UpdateOrderMenu(orderMenu, iex,isTemp);
                    }
                    //else
                    //{
                    //    if (orderMenu.Quantity != iex.Quantity || orderMenu.Comment != iex.Comment)
                    //    {
                    //        decimal backQuantity = iex.Quantity - orderMenu.Quantity;
                    //        if (backQuantity != 0)
                    //        {
                    //            orderMenu.BackQuantity = backQuantity;
                    //            orderMenu.BackReseaon = "加减单";
                    //            orderMenu.BackCreateDate = dtOperDate;
                    //            orderMenu.BackUserId = userId;

                    //            if (backQuantity > 0)
                    //            {
                    //                AddPrint(ref htOtherPrint, ref htLocalPrint, iex, PrintType.Add);
                    //            }
                    //            else
                    //            {
                    //                AddPrint(ref htOtherPrint, ref htLocalPrint, iex, PrintType.Sub);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            AddPrint(ref htOtherPrint, ref htLocalPrint, iex, PrintType.Taste);
                    //        }
                    //        Order_OrderMenu_UpdateOrderMenu(orderMenu, iex);
                    //    }
                    //}
                }
            }
            else
            {
                DXInfo.Models.OrderMenus orderMenu = Mapper.Map<DXInfo.Models.OrderMenus>(iex);
                orderMenu.InventoryId = iex.Id;
                orderMenu.CreateDate = dtOperDate;
                orderMenu.UserId = userId;
                orderMenu.Oper = userId;
                orderMenu.OperDate = dtOperDate;
                orderMenu.Status = isTemp?(int)DXInfo.Models.OrderMenuStatus.Normal:(int)DXInfo.Models.OrderMenuStatus.Order;
                orderMenu.Price = iex.SalePrice;
                orderMenu.OrderUserId = userId;
                orderMenu.OrderCreateDate = dtOperDate;
                uow.OrderMenus.Add(orderMenu);

                uow.Commit();
                iex.OrderMenuId = orderMenu.Id;
                iex.Status = isTemp ? (int)DXInfo.Models.OrderMenuStatus.Normal : (int)DXInfo.Models.OrderMenuStatus.Order;
                DXInfo.Models.OrderMenusHis menuHis = Mapper.Map<DXInfo.Models.OrderMenusHis>(orderMenu);
                menuHis.LinkId = orderMenu.Id;
                uow.OrderMenusHis.Add(menuHis);
                if (iex.InvPrice != null)
                {
                    Order_OrderInvPrice(iex.InvPrice, orderMenu.Id);
                }
                AddPrint(ref htOtherPrint, ref htLocalPrint, iex, PrintType.Menu);
            }
        }
        public void MenuOrder(Guid orderDishId,            
            List<DXInfo.Models.InventoryEx> lInventoryEx,
            List<DXInfo.Models.OrderPackages> lOrderPackage,
            ref Hashtable htOtherPrint,
            ref Hashtable htLoalPrint,
            bool isTemp = false)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                if (lOrderPackage != null && lOrderPackage.Count > 0)
                {
                    foreach (DXInfo.Models.OrderPackages op in lOrderPackage)
                    {
                        Order_OrderPackage(op,isTemp);
                    }
                }
                foreach (DXInfo.Models.InventoryEx iex in lInventoryEx)
                {
                    MenuOrder_OrderMenu(iex, ref htOtherPrint, ref htLoalPrint,isTemp);
                }
                uow.Commit();
                transaction.Complete();
            }
            
        }        
        #endregion

        #region 退单
        private void MenuCancelOrder_OrderPackage(DXInfo.Models.OrderPackages orderPackage)
        {
            if (orderPackage != null)
            {
                orderPackage.Status = orderPackage.Status == 1 ? 2 : orderPackage.Status == 6 ? 7 : orderPackage.Status == 7 ? 6 : 1;
                orderPackage.OperDate = dtOperDate;
                orderPackage.Oper = userId;
                if (orderPackage.Status == 1 || orderPackage.Status == 7)
                {
                    orderPackage.BackUserId = userId;
                    orderPackage.BackCreateDate = dtOperDate;
                    orderPackage.BackQuantity = orderPackage.Quantity;
                    orderPackage.BackReseaon = "退单";
                }
                else
                {
                    orderPackage.BackCreateDate = null;
                    orderPackage.BackQuantity = 0;
                    orderPackage.BackReseaon = null;
                    orderPackage.BackUserId = null;
                }
                uow.OrderPackages.Update(orderPackage);

                DXInfo.Models.OrderPackagesHis packagesHis = Mapper.Map<DXInfo.Models.OrderPackagesHis>(orderPackage);
                packagesHis.LinkId = orderPackage.Id;
                uow.OrderPackagesHis.Add(packagesHis);
            }
        }
        private void MenuCancelOrder_OrderMenu(DXInfo.Models.InventoryEx iex, ref Hashtable htOtherPrint,ref Hashtable htLocalPrint)
        {
            DXInfo.Models.OrderMenus orderMenu = uow.OrderMenus.GetById(g=>g.Id==iex.OrderMenuId);
            if (orderMenu.Status == (int)DXInfo.Models.OrderMenuStatus.Normal) throw new Exception("请首先下单");

            if (orderMenu.Status != (int)DXInfo.Models.OrderMenuStatus.ReturnAfterOut &&
                orderMenu.Status != (int)DXInfo.Models.OrderMenuStatus.Withdraw)
            {
                orderMenu.Status = orderMenu.Status == (int)DXInfo.Models.OrderMenuStatus.Out ?
                    (int)DXInfo.Models.OrderMenuStatus.ReturnAfterOut :
                    (int)DXInfo.Models.OrderMenuStatus.Withdraw;

                orderMenu.OperDate = dtOperDate;
                orderMenu.Oper = userId;
                orderMenu.BackUserId = userId;
                orderMenu.BackCreateDate = dtOperDate;
                orderMenu.BackQuantity = orderMenu.Quantity;
                orderMenu.BackReseaon = "退单";
                uow.OrderMenus.Update(orderMenu);

                DXInfo.Models.OrderMenusHis menuHis = Mapper.Map<DXInfo.Models.OrderMenusHis>(orderMenu);
                menuHis.LinkId = orderMenu.Id;
                uow.OrderMenusHis.Add(menuHis);

                //iex.Status = orderMenu.Status;

                AddPrint(ref htOtherPrint,ref htLocalPrint,iex,PrintType.CancelOrder);//ref htCancelOrder, iex);
            }
        }
        public void MenuCancelOrder(List<DXInfo.Models.InventoryEx> lInventoryEx,
            List<DXInfo.Models.OrderPackages> lOrderPackage, ref Hashtable htOtherPrint,ref Hashtable htLocalPrint)
        {
            if (lOrderPackage != null && lOrderPackage.Count > 0)
            {
                foreach (DXInfo.Models.OrderPackages op in lOrderPackage)
                {
                    MenuCancelOrder_OrderPackage(op);
                }
            }
            foreach (DXInfo.Models.InventoryEx iex in lInventoryEx)
            {
                MenuCancelOrder_OrderMenu(iex, ref htOtherPrint,ref htLocalPrint);
            }
            uow.Commit();
        }
        
        #endregion

        #region 缺菜
        /// <summary>
        /// 缺菜
        /// </summary>
        public void MissMenu(Guid orderMenuId)
        {
            DXInfo.Models.OrderMenus om = uow.OrderMenus.GetById(g=>g.Id==orderMenuId);
            if (om == null) throw new ArgumentNullException("菜品", "未找到");
            if (om.Status == (int)DXInfo.Models.OrderMenuStatus.Make) throw new Exception("已分单完成的菜不能进行缺菜操作");
            if (om.Status == (int)DXInfo.Models.OrderMenuStatus.Out) throw new Exception("已出菜完成的菜不能进行缺菜操作");
            if (om.Quantity == om.BillQuantity) throw new Exception("已分单完成的菜不能进行缺菜操作");

            om.MissCreateDate = dtOperDate;
            om.MissUserId = userId;
            om.MissQuantity = om.Quantity - om.BillQuantity;
            if (om.MissQuantity == om.Quantity)
            {
                om.Status = (int)DXInfo.Models.OrderMenuStatus.Lack;
            }
            om.Oper = userId;
            om.OperDate = dtOperDate;
            uow.OrderMenus.Update(om);

            OrderMenusHis menuHis = Mapper.Map<DXInfo.Models.OrderMenusHis>(om);
            menuHis.LinkId = om.Id;
            uow.OrderMenusHis.Add(menuHis);

            KitchenMiss missMenu = new KitchenMiss();
            missMenu.CreateDate = om.MissCreateDate;
            missMenu.UserId = userId;
            missMenu.Quantity = om.MissQuantity;
            uow.KitchenMiss.Add(missMenu);

            int lackCount = (from d in uow.MenuStatus.GetAll()
                             where d.Dept == deptId && d.Inventory == om.InventoryId
                             && d.Status == (int)DXInfo.Models.OrderMenuStatus.Lack
                             select d).Count();
            if (lackCount == 0)
            {
                MenuStatus menuStatus = new MenuStatus();
                menuStatus.Dept = deptId;
                menuStatus.Inventory = om.InventoryId;
                menuStatus.Status = (int)DXInfo.Models.OrderMenuStatus.Lack;
                uow.MenuStatus.Add(menuStatus);
            }

            uow.Commit();
        }
        /// <summary>
        /// 撤销缺菜
        /// </summary>
        public void CancelMissMenu(Guid orderMenuId)
        {
            DXInfo.Models.OrderMenus om = uow.OrderMenus.GetById(g=>g.Id==orderMenuId);
            if (om == null) throw new ArgumentNullException("菜品未找到");

            if (om.MissQuantity == 0) throw new Exception("已缺菜的菜品才可进行取消缺菜操作");

            decimal missQuantity = om.MissQuantity;
            om.MissQuantity = 0;
            om.MissCreateDate = null;
            om.MissUserId = null;

            if (om.Status == (int)DXInfo.Models.OrderMenuStatus.Lack)
            {                
                om.Status = (int)DXInfo.Models.OrderMenuStatus.Order;
            }
            om.Oper = userId;
            om.OperDate = dtOperDate;
            uow.OrderMenus.Update(om);

            OrderMenusHis menuHis = Mapper.Map<DXInfo.Models.OrderMenusHis>(om);
            menuHis.LinkId = om.Id;
            uow.OrderMenusHis.Add(menuHis);

            KitchenMiss missMenu = new KitchenMiss();
            missMenu.CreateDate = dtOperDate;
            missMenu.UserId = userId;
            missMenu.Quantity = -missQuantity;
            uow.KitchenMiss.Add(missMenu);

            List<DXInfo.Models.MenuStatus> lLackMenu = (from d in uow.MenuStatus.GetAll()
                             where d.Dept == deptId && d.Inventory == om.InventoryId
                             && d.Status == (int)DXInfo.Models.OrderMenuStatus.Lack
                             select d).ToList();
            foreach (DXInfo.Models.MenuStatus lackMenu in lLackMenu)
            {
                uow.MenuStatus.Delete(lackMenu);
            }

            uow.Commit();
        }
        #endregion

        #region 催菜
        public void HurryMenu(Guid orderMenuId)
        {
            DXInfo.Models.OrderMenus om = uow.OrderMenus.GetById(g=>g.Id==orderMenuId);
            if (om == null) throw new Exception("菜品未找到");
            if (om.Status == (int)DXInfo.Models.OrderMenuStatus.Out) throw new Exception("已出完菜，不能催菜");
            if (om.Status == (int)DXInfo.Models.OrderMenuStatus.Lack) throw new Exception("已缺菜，不能催菜");
            if (om.Status == (int)DXInfo.Models.OrderMenuStatus.Withdraw || 
                om.Status == (int)DXInfo.Models.OrderMenuStatus.ReturnAfterOut) throw new Exception("已退菜，不能催菜");

            om.HurryUserId = userId;
            om.HurryCreateDate = dtOperDate;
            om.Status = (int)DXInfo.Models.OrderMenuStatus.Hurry;
            om.Oper = userId;
            om.OperDate = dtOperDate;
            uow.OrderMenus.Update(om);

            OrderMenusHis menuHis = Mapper.Map<DXInfo.Models.OrderMenusHis>(om);
            menuHis.LinkId = om.Id;
            uow.OrderMenusHis.Add(menuHis);
            uow.Commit();
        }
        #endregion

        #region 分单
        public void BillMenu(Guid orderMenuId)
        {
            using (TransactionScope transaction = new TransactionScope())
            {
                DXInfo.Models.OrderMenus om = uow.OrderMenus.GetById(g => g.Id == orderMenuId);
                if (om == null) throw new Exception("菜品未找到");
                if (!(om.Status == (int)DXInfo.Models.OrderMenuStatus.Order||
                    om.Status==(int)DXInfo.Models.OrderMenuStatus.Hurry)) throw new Exception("已下单或催菜的菜品才可分单");
                if (om.BillQuantity == om.Quantity - om.MissQuantity) throw new Exception("菜品已分单完成");

                om.BillCreateDate = dtOperDate;
                om.BillUserId = userId;
                om.BillQuantity = om.BillQuantity + 1;
                if (om.BillQuantity == om.Quantity-om.MissQuantity&&
                    (om.Status == (int)DXInfo.Models.OrderMenuStatus.Order||
                    om.Status ==(int)DXInfo.Models.OrderMenuStatus.Hurry))
                {
                    om.Status = (int)DXInfo.Models.OrderMenuStatus.Make;
                }
                om.Oper = userId;
                om.OperDate = dtOperDate;
                uow.OrderMenus.Update(om);

                OrderMenusHis menuHis = Mapper.Map<DXInfo.Models.OrderMenusHis>(om);
                menuHis.LinkId = om.Id;
                uow.OrderMenusHis.Add(menuHis);

                uow.Commit();
                transaction.Complete();
            }
        }
        public void CancelBillMenu(Guid orderMenuId)
        {
            DXInfo.Models.OrderMenus om = uow.OrderMenus.GetById(g=>g.Id==orderMenuId);
            if (om == null) throw new ArgumentNullException("菜品", "未找到");
            if (om.BillQuantity == 0) throw new Exception("已分单的菜品才可取消分单");
            if (om.Status==(int)DXInfo.Models.OrderMenuStatus.Out) throw new Exception("已出菜的菜品不能取消分单");

            om.BillQuantity = om.BillQuantity - 1;
            if (om.BillQuantity == 0)
            {
                om.BillCreateDate = null;
                om.BillUserId = null;
                if (om.Status == (int)DXInfo.Models.OrderMenuStatus.Make)
                {
                    if (om.HurryCreateDate.HasValue)
                    {
                        om.Status = (int)DXInfo.Models.OrderMenuStatus.Hurry;
                    }
                    else
                    {
                        om.Status = (int)DXInfo.Models.OrderMenuStatus.Order;
                    }
                }
            }

            om.Oper = userId;
            om.OperDate = dtOperDate;
            uow.OrderMenus.Update(om);

            OrderMenusHis menuHis = Mapper.Map<DXInfo.Models.OrderMenusHis>(om);
            menuHis.LinkId = om.Id;
            uow.OrderMenusHis.Add(menuHis);

            uow.Commit();
        }
        #endregion

        #region 出菜
        public void OutMenu(Guid orderMenuId, Guid deskId)
        {
            DXInfo.Models.OrderMenus om = uow.OrderMenus.GetById(g=>g.Id==orderMenuId);
            if (om == null) throw new ArgumentNullException("菜品", "未找到");
            if (!(om.Status == (int)DXInfo.Models.OrderMenuStatus.Order||
                om.Status == (int)DXInfo.Models.OrderMenuStatus.Hurry||
                om.Status == (int)DXInfo.Models.OrderMenuStatus.Make)) throw new Exception("已下单、催菜或者分单完成的才能出菜");
            if (om.Quantity == om.MenuQuantity - om.MissQuantity) throw new Exception("已出菜完成");

            om.MenuCreateDate = dtOperDate;
            om.MenuUserId = userId;
            om.MenuQuantity = om.MenuQuantity + 1;
            if (om.MenuQuantity == om.Quantity - om.MissQuantity)
            {
                om.Status = (int)DXInfo.Models.OrderMenuStatus.Out;
            }
            om.Oper = userId;
            om.OperDate = dtOperDate;
            uow.OrderMenus.Update(om);

            OrderMenusHis menuHis = Mapper.Map<DXInfo.Models.OrderMenusHis>(om);
            menuHis.LinkId = om.Id;
            uow.OrderMenusHis.Add(menuHis);

            DXInfo.Models.KitchenMenuDesk menuDesk = new KitchenMenuDesk();
            menuDesk.CreateDate = om.MenuCreateDate.Value;
            menuDesk.DeskId = deskId;
            menuDesk.OrderMenuId = orderMenuId;
            menuDesk.Quantity = 1;
            menuDesk.UserId = userId;
            uow.KitchenMenuDesk.Add(menuDesk);

            uow.Commit();
        }
        public void CancelOutMenu(Guid orderMenuId, Guid deskId)
        {
            DXInfo.Models.OrderMenus om = uow.OrderMenus.GetById(g=>g.Id==orderMenuId);
            if (om == null) throw new ArgumentNullException("菜品", "未找到");
            if (om.MenuQuantity == 0) throw new Exception("已出菜的菜品才可以取消出菜");

            om.MenuQuantity = om.MenuQuantity - 1;
            if (om.MenuQuantity == 0)
            {
                om.MenuCreateDate = null;
                om.MenuUserId = null;
                if (om.BillQuantity == om.Quantity)
                {
                    om.Status = (int)DXInfo.Models.OrderMenuStatus.Make;
                }
                else
                {
                    if (om.HurryCreateDate.HasValue)
                    {
                        om.Status = (int)DXInfo.Models.OrderMenuStatus.Hurry;
                    }
                    else
                    {
                        om.Status = (int)DXInfo.Models.OrderMenuStatus.Order;
                    }
                }
            }
            
            
            om.Oper = userId;
            om.OperDate = dtOperDate;
            uow.OrderMenus.Update(om);

            OrderMenusHis menuHis = Mapper.Map<DXInfo.Models.OrderMenusHis>(om);
            menuHis.LinkId = om.Id;
            uow.OrderMenusHis.Add(menuHis);

            DXInfo.Models.KitchenMenuDesk menuDesk = new KitchenMenuDesk();
            menuDesk.CreateDate = dtOperDate;
            menuDesk.DeskId = deskId;
            menuDesk.OrderMenuId = orderMenuId;
            menuDesk.Quantity = -1;
            menuDesk.UserId = userId;
            uow.KitchenMenuDesk.Add(menuDesk);

            uow.Commit();
        }
        #endregion

        #endregion

        public void LackMenu(Guid invId)
        {
            MenuStatus ms = new MenuStatus();
            ms.Inventory = invId;
            ms.Dept = deptId;
            ms.Status = (int)DXInfo.Models.OrderMenuStatus.Lack;

            uow.MenuStatus.Add(ms);
            uow.Commit();
        }
        public void CancelLackMenu(Guid invId)
        {
            List<DXInfo.Models.MenuStatus> lMenuStatus = uow.MenuStatus.GetAll().Where(w => w.Inventory == invId && w.Dept == deptId).ToList();
            foreach (DXInfo.Models.MenuStatus ms in lMenuStatus)
            {
                uow.MenuStatus.Delete(ms);
            }
            uow.Commit();
        }
        private bool disposed = false;
        //public void Dispose()
        //{
        //    //throw new NotImplementedException();
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
        //~DeskManageFacade()
        //{
        //    Dispose(false);
        //}
        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!disposed)
        //    {
        //        if (disposing)
        //        {
        //            //释放非托管资源
        //        }
        //        //释放托管资源
        //        if (uow != null)
        //        {
        //            uow.Dispose();
        //            uow = null;
        //        }
        //        disposed = true;
        //    }
        //}
    }
}
