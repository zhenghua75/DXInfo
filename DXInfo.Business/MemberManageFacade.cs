using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using AutoMapper;
using System.Collections.ObjectModel;
using System.Transactions;

namespace DXInfo.Business
{
    public class CardInMoneyParaObj
    {
        public Guid DeptId { get; set; }
        public string DeptName { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public Guid CardId { get; set; }
        public string MemberName { get; set; }
        public Guid PayTypeId { get; set; }
        public string PayTypeName { get; set; }
        public decimal LastBalance { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreateDate { get; set; }
        public decimal Amount { get; set; }
        public decimal Donate { get; set; }
        public int RechargeType { get; set; }
        public string OperatorsOnDuty { get; set; }
    }
    public class CheckoutParaObj
    {
        public bool IsCard { get; set; }
        public int SourceType { get; set; }
        public Guid DeptId { get; set; }
        public string DeptName { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public Guid PayTypeId { get; set; }
        public string PayTypeName { get; set; }
        public Guid? CardId { get; set; }
        public string CardNo { get; set; }
        public decimal LastBalance { get; set; }
        public decimal Balance { get; set; }
        public Guid? MemberId { get; set; }
        public string MemberName { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsVirtual { get; set; }
        public bool IsCardLevelAuto { get; set; }

        public Guid OrderDishId { get; set; }
        public ObservableCollection<DXInfo.Models.InventoryEx> lInventoryEx { get; set; }
        public List<DXInfo.Models.CardDonateInventoryEx> lCardDonateInventoryEx { get; set; }
        public int ConsumeType { get; set; }
        public string BillType { get; set; }
        public decimal Cash { get; set; }
        public decimal Change { get; set; }
        public decimal Voucher { get; set; }
        public decimal PayVoucher { get; set; }
        public decimal Sum { get; set; }
        public decimal Discount { get; set; }
        public decimal Amount { get; set; }
        public decimal Quantity { get; set; }
        public string DeskNo { get; set; }
        public string Sn { get; set; }
        public string OperatorsOnDuty { get; set; }
        public int PeopleCount { get; set; }
        //public string Comment { get; set; }
        public bool Erasing { get; set; }
    }
    public class CancelCheckoutParaObj
    {
        public Guid ConsumeId { get; set; }
        public int ConsumeType { get; set; }
        public string BillType { get; set; }
        public decimal LastBalance { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public bool IsCard { get; set; }
        public bool IsVirtual { get; set; }
        public string CardNo { get; set; }
        public DateTime CreateDate { get; set; }
        public string DeptName { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public bool IsCardLevelAuto { get; set; }
        public Guid CardId { get; set; }
        public Guid DeptId { get; set; }
        public Guid UserId { get; set; }
        //public int SourceType { get; set; }
    }
    public class MemberManageFacade
    {
        private IFairiesMemberManageUow uow;
        public string ErrorMsg{get;set;}
        public MemberManageFacade(IFairiesMemberManageUow uow)
        {
            this.uow = uow;
        }

        #region 充值
        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="para"></param>
        public void CardInMoney(CardInMoneyParaObj para)
        {            
            if (para.Amount <= 0) throw new ArgumentException("请输入充值金额");
            if (para.PayTypeId == null || para.PayTypeId == Guid.Empty) throw new ArgumentNullException("请选择支付方式");

            DXInfo.Models.Recharges recharge = new DXInfo.Models.Recharges();
            recharge.Amount = para.Amount;
            recharge.Donate = para.Donate;
            recharge.LastBalance = para.LastBalance;
            recharge.Balance = para.Balance;
            recharge.Card = para.CardId;
            recharge.CreateDate = para.CreateDate;
            recharge.UserId = para.UserId;
            recharge.DeptId = para.DeptId;
            recharge.PayType = para.PayTypeId;
            recharge.RechargeType = para.RechargeType;
            recharge.OperatorsOnDuty = para.OperatorsOnDuty;
            uow.Recharges.Add(recharge);

            DXInfo.Models.Cards oldCard = uow.Cards.GetById(g => g.Id == para.CardId);
            if (oldCard == null) throw new ArgumentException("请先读卡");
            oldCard.Balance = recharge.Balance;
            uow.Cards.Update(oldCard);

            DXInfo.Models.CardsLog cardsLog = Mapper.Map<DXInfo.Models.Cards, DXInfo.Models.CardsLog>(oldCard);
            cardsLog.CardId = para.CardId;
            cardsLog.CreateDate = para.CreateDate;
            cardsLog.UserId = para.UserId;
            cardsLog.DeptId = para.DeptId;
            uow.CardsLog.Add(cardsLog);
            //小票
            DXInfo.Models.Bills bill = new DXInfo.Models.Bills();
            bill.Amount = para.Amount;
            bill.Balance = para.Balance;
            bill.BillType = "CardInMoneyWindow";
            bill.CardNo = oldCard.CardNo;
            bill.CreateDate = para.CreateDate;
            bill.DeptName = para.DeptName;
            bill.Donate = para.Donate;
            bill.FullName = para.UserName + "," + para.FullName;
            bill.LastBalance = para.LastBalance;
            bill.MemberName = para.MemberName;
            bill.PayTypeName = para.PayTypeName;
            uow.Bills.Add(bill);

            uow.Commit();
        }
        #endregion

        #region 结账
        /// <summary>
        /// 消费
        /// </summary>
        private void CardDonate(List<DXInfo.Models.CardDonateInventoryEx> lCardDonateInventoryEx, Guid consumeId, Guid billId)
        {
            //卡赠送处理
            foreach (DXInfo.Models.CardDonateInventoryEx cdi in lCardDonateInventoryEx)
            {
                DXInfo.Models.ConsumeDonateInv cdonate = new DXInfo.Models.ConsumeDonateInv();
                cdonate.Consume = consumeId;
                cdonate.Inventory = cdi.Inventory;//cdi.Id;
                uow.ConsumeDonateInv.Add(cdonate);

                //Guid gInvId = cdi.Id;
                var cdi1 = uow.CardDonateInventory.GetById(g => g.Id == cdi.Id);//.GetAll().Where(w => w.Inventory == gInvId).Where(w => w.CardId == card.Id).FirstOrDefault();
                if (cdi1 != null)
                {
                    cdi1.IsValidate = false;
                }
                uow.CardDonateInventory.Update(cdi1);

                DXInfo.Models.BillDonateInvLists bd = new DXInfo.Models.BillDonateInvLists();
                bd.Bill = billId;
                bd.InvName = cdi.Name;

                uow.BillDonateInvLists.Add(bd);
            }
        }
        private void CardPoint(Guid cardId,DateTime dCreateDate,Guid deptId,Guid userId,decimal dPoint)
        {
            var cps = uow.CardPoints.GetAll().Where(w => w.Card == cardId).Where(w => w.PointType == 0).FirstOrDefault();
            if (cps != null)
            {
                cps.Point = cps.Point + dPoint;
                uow.CardPoints.Update(cps);                
            }
            else
            {
                DXInfo.Models.CardPoints cp = new DXInfo.Models.CardPoints();
                cp.Card = cardId;
                cp.CreateDate = dCreateDate;
                cp.DeptId = deptId;
                cp.Point = dPoint;
                cp.PointType = 0;
                cp.UserId = userId;
                uow.CardPoints.Add(cp);
            }
        }
        private void CardCancelPoint(Guid cardId, DateTime dCreateDate, Guid deptId,Guid userId, decimal dPoint)
        {
            var cps = uow.CardPoints.GetAll().Where(w => w.Card == cardId).Where(w => w.PointType == 0).FirstOrDefault();
            if (cps != null)
            {
                cps.Point = cps.Point - dPoint;
                uow.CardPoints.Update(cps);
            }
            else
            {
                DXInfo.Models.CardPoints cp = new DXInfo.Models.CardPoints();
                cp.Card = cardId;
                cp.CreateDate = dCreateDate;
                cp.DeptId = deptId;
                cp.Point = -dPoint;
                cp.PointType = 0;
                cp.UserId = userId;
                uow.CardPoints.Add(cp);
            }
        }
        private void CardUpdate(bool isCardLevelAuto,Guid cardId,decimal dBalance,decimal dPoint)
        {
            DXInfo.Models.Cards newCard = uow.Cards.GetById(g => g.Id == cardId);
            if (newCard == null) throw new ArgumentException("卡信息未找到");
            newCard.Balance = dBalance;

            if (isCardLevelAuto)
            {
                DXInfo.Models.CardLevels oldCardLevel = uow.CardLevels.GetById(g => g.Id == newCard.CardLevel);
                if (dPoint > 0)
                {
                    decimal points = uow.CardPoints.GetAll().Where(w => w.Card == cardId).Sum(s => s.Point);
                    DXInfo.Models.CardLevels cardLevel = uow.CardLevels.GetAll().Where(w => w.Point < points + dPoint &&
                        w.Id != newCard.CardLevel).OrderByDescending(o => o.Point).FirstOrDefault();
                    if (cardLevel != null && cardLevel.Discount < oldCardLevel.Discount)
                    {
                        newCard.CardLevel = cardLevel.Id;
                    }
                }
            }
            uow.Cards.Update(newCard);

            DXInfo.Models.CardsLog cardLog = Mapper.Map<DXInfo.Models.CardsLog>(newCard);
            cardLog.CardId = newCard.Id;
            uow.CardsLog.Add(cardLog);
        }
        
        private bool OrderDisheUpdate(Guid orderDishId,DateTime createDate,Guid userId)
        {
            DXInfo.Models.OrderDishes newOrderDish = uow.OrderDishes.GetById(g => g.Id == orderDishId);
            if (newOrderDish == null)
            {
                ErrorMsg = "订单已结账！";
                return false;
            }
            if (newOrderDish.Status != (int)DXInfo.Models.OrderDishStatus.Ordered)
            {
                ErrorMsg = "确认下单后才可以结账！";
                return false;
            }
            uow.OrderDishes.Delete(newOrderDish);

            DXInfo.Models.OrderDishesHis newOrderDishHis = Mapper.Map<DXInfo.Models.OrderDishesHis>(newOrderDish);
            newOrderDishHis.Status = (int)DXInfo.Models.OrderDishStatus.Checkouted;
            newOrderDishHis.LinkId = newOrderDish.Id;
            newOrderDishHis.CreateDate = createDate;
            newOrderDishHis.UserId = userId;
            uow.OrderDishesHis.Add(newOrderDishHis);
            return true;
        }
        private void OrderDeskUpdate(Guid orderDishId,Guid userId,DateTime dCreateDate)
        {
            List<DXInfo.Models.OrderDeskes> lOrderDeskes = (from o in uow.OrderDeskes.GetAll() where o.OrderId == orderDishId select o).ToList();
            foreach (DXInfo.Models.OrderDeskes orderDesk in lOrderDeskes)
            {
                uow.OrderDeskes.Delete(orderDesk);

                DXInfo.Models.OrderDeskesHis deskHis = Mapper.Map<DXInfo.Models.OrderDeskesHis>(orderDesk);
                deskHis.Status = (int)DXInfo.Models.OrderDeskStatus.Idle;
                deskHis.LinkId = orderDesk.Id;
                deskHis.UserId = userId;
                deskHis.CreateDate = dCreateDate;
                uow.OrderDeskesHis.Add(deskHis);
            }
        }
        private void OrderMenuUpdate(Guid orderMenuId,DateTime dCreateDate,Guid userId)
        {
            DXInfo.Models.OrderMenus om = uow.OrderMenus.GetById(g => g.Id == orderMenuId);
            uow.OrderMenus.Delete(om);

            DXInfo.Models.OrderMenusHis omHis = Mapper.Map<DXInfo.Models.OrderMenusHis>(om);
            omHis.OperDate = dCreateDate;
            omHis.Status = (int)DXInfo.Models.OrderMenuStatus.Checkout;
            omHis.Oper = userId;
            omHis.LinkId = om.Id;
            uow.OrderMenusHis.Add(omHis);
        }
        private void OrderPackageUpdate(Guid orderId,DateTime dCreateDate,Guid userId)
        {
            List<DXInfo.Models.OrderPackages> lOrderPackage = uow.OrderPackages.GetAll().Where(w => w.OrderId == orderId).ToList();
            foreach (DXInfo.Models.OrderPackages orderPackage in lOrderPackage)
            {
                uow.OrderPackages.Delete(orderPackage);

                DXInfo.Models.OrderPackagesHis orderPackageHis = Mapper.Map<DXInfo.Models.OrderPackagesHis>(orderPackage);
                orderPackageHis.OperDate = dCreateDate;
                orderPackageHis.Status = (int)DXInfo.Models.OrderMenuStatus.Checkout;
                orderPackageHis.Oper = userId;
                orderPackageHis.LinkId = orderPackage.Id;
                uow.OrderPackagesHis.Add(orderPackageHis);
            }
        }
        private decimal Points(Guid deptId, ObservableCollection<DXInfo.Models.InventoryEx> lsi)
        {
            decimal point = 0;
            //首先获取本部门积分
            var cp1 = uow.ConsumePoints.GetAll().Where(w => w.DeptId == deptId).ToList();
            //若无本部门积分，则获取无部门积分
            var cp = cp1.Count > 0 ? cp1 : uow.ConsumePoints.GetAll().Where(w => !w.DeptId.HasValue).ToList();
            if (cp.Count > 0)
            {
                foreach (DXInfo.Models.InventoryEx si in lsi)
                {
                    //首先获取本分类积分
                    var cpc1 = cp.Where(w => w.Category == si.Category).ToList();
                    //若无本分类积分，则获取无分类积分
                    var cpc = cpc1.Count > 0 ? cpc1 : cp.Where(w => !w.Category.HasValue).ToList();
                    if (cpc.Count > 0)
                    {
                        decimal min = cpc.Min(m => m.Point / m.Amount);
                        point += si.Amount * min;
                    }
                }
            }
            return point;
        }
        public bool CheckOut(CheckoutParaObj para, Func<string, decimal, bool> CardConsume)
        {
            decimal dPoint = 0;
            if (para.ConsumeType == (int)DXInfo.Models.ConsumeType.Card)
            {
                if (para.Balance < 0)
                {
                    this.ErrorMsg = "卡余额不足";
                    return false;
                }
            }
            if (para.ConsumeType == (int)DXInfo.Models.ConsumeType.CardNoMoney ||
                para.ConsumeType == (int)DXInfo.Models.ConsumeType.NoMember)
            {
                if (para.Cash - para.Change < para.Amount)
                {
                    if (!(para.Erasing && (int)(para.Cash - para.Change) / 10 == (int)para.Amount / 10))
                    {
                        this.ErrorMsg = "收的钱应不小于消费金额";
                        return false;
                    }
                }
            }

            if (para.Amount > 0 && para.Voucher == 0 && para.IsCard)
            {
                dPoint = Points(para.DeptId, para.lInventoryEx);
            }

            if (para.Amount > 0 &&
                para.ConsumeType == (int)DXInfo.Models.ConsumeType.Card && 
                para.IsCard && 
                !para.IsVirtual)
            {
                if (!CardConsume(para.CardNo,para.Amount)) return false;
            }
            using (TransactionScope transaction = new TransactionScope())
            {
                if (para.SourceType == (int)DXInfo.Models.SourceType.WesternRestaurant)
                {
                    if (!OrderDisheUpdate(para.OrderDishId,para.CreateDate,para.UserId)) return false;
                    OrderDeskUpdate(para.OrderDishId,para.UserId,para.CreateDate);
                    OrderPackageUpdate(para.OrderDishId,para.CreateDate,para.UserId);
                }
                DXInfo.Models.Consume consume = new DXInfo.Models.Consume();
                consume.Sum = para.Sum;
                consume.Voucher = para.Voucher;
                consume.PayVoucher = para.PayVoucher;
                consume.Discount = para.Discount;
                if (para.CardId.HasValue)
                {
                    consume.Card = para.CardId.Value;
                }
                if (para.MemberId.HasValue)
                {
                    consume.Member = para.MemberId.Value;
                }
                consume.Amount = para.Amount;
                consume.Balance = para.Balance;
                consume.CreateDate = para.CreateDate;
                consume.DeptId = para.DeptId;
                consume.LastBalance = para.LastBalance;
                consume.Point = dPoint;
                consume.UserId = para.UserId;
                consume.ConsumeType = para.ConsumeType;
                consume.DeskNo = para.DeskNo;
                consume.PayType = para.PayTypeId;
                consume.Cash = para.Cash;
                consume.Change = para.Change;
                consume.SourceType = para.SourceType;
                consume.Quantity = para.Quantity;
                consume.IsValid = true;
                consume.Sn = para.Sn;
                consume.OperatorsOnDuty = para.OperatorsOnDuty;
                uow.Consume.Add(consume);


                DXInfo.Models.Bills bill = new DXInfo.Models.Bills();
                bill.Sum = para.Sum;
                bill.Voucher = para.Voucher;
                bill.Discount = para.Discount;

                bill.Amount = para.Amount;
                bill.Balance = para.Balance;
                bill.BillType = para.BillType;//"CardConsumeWindow";
                if (!string.IsNullOrEmpty(para.CardNo))
                {
                    bill.CardNo = para.CardNo;
                }
                bill.CreateDate = para.CreateDate;
                bill.DeptName = para.DeptName;
                bill.FullName = para.UserName + "," + para.FullName;
                bill.LastBalance = para.LastBalance;
                if (!string.IsNullOrEmpty(para.MemberName))
                {
                    bill.MemberName = para.MemberName;
                }
                bill.DeskNo = para.DeskNo;

                bill.PayTypeName = para.PayTypeName;
                bill.Cash = para.Cash;
                bill.Change = para.Change;
                bill.Sn = para.Sn;
                bill.PeopleCount = para.PeopleCount;
                uow.Bills.Add(bill);

                uow.Commit();
                if (para.IsCard && para.ConsumeType == (int)DXInfo.Models.ConsumeType.Card)
                {
                    CardUpdate(para.IsCardLevelAuto,para.CardId.Value,para.Balance, dPoint);
                }
                //卡赠送处理
                if (para.Voucher == 0 &&
                    para.IsCard &&
                    para.lCardDonateInventoryEx != null && 
                    para.lCardDonateInventoryEx.Count > 0)
                {
                    CardDonate(para.lCardDonateInventoryEx, consume.Id, bill.Id);
                }
                foreach (DXInfo.Models.InventoryEx si in para.lInventoryEx)
                {
                    if (para.SourceType == (int)DXInfo.Models.SourceType.WesternRestaurant)
                    {
                        OrderMenuUpdate(si.OrderMenuId,para.CreateDate,para.UserId);
                    }
                    DXInfo.Models.ConsumeList cl = new DXInfo.Models.ConsumeList();
                    //cl.Amount = si.Amount;
                    cl.Consume = consume.Id;
                    cl.CreateDate = para.CreateDate;
                    cl.IsValid = true;
                    cl.IsStock = false;
                    if (para.SourceType == (int)DXInfo.Models.SourceType.ColdDrinkShop)
                    {
                        if (si.CupType != null)
                        {
                            cl.Cup = si.CupType.Id;
                        }
                    }
                    cl.DeptId = para.DeptId;
                    cl.Inventory = si.Id;

                    cl.Quantity = si.Quantity;
                    cl.UserId = para.UserId;
                    //cl.Discount = dDiscount;

                    if (si.IsInvDynamicPrice)
                    {
                        cl.Discount = si.IsDiscount ? si.Discount : 100;
                    }
                    else
                    {
                        cl.Discount = si.IsDiscount ? para.Discount : 100;
                    }
                    cl.IsPackage = si.IsPackage;
                    cl.PackageId = si.PackageId;
                    if (para.SourceType == (int)DXInfo.Models.SourceType.WesternRestaurant)
                    {
                        cl.Price = si.SalePrice;
                        cl.Sum = si.Amount;
                        cl.Amount = si.IsDiscount ? si.Amount * para.Discount / 100 : si.Amount;
                    }
                    else
                    {
                        if (si.IsInvDynamicPrice)
                        {
                            cl.Price = si.SalePrice;
                            cl.AgreementPrice = si.AgreementPrice;
                            //if (si.IsInvPrice && si.InvPrice != null)
                            //{
                             //   cl.Price = si.InvPrice.SalePrice;
                            //}
                            cl.Sum = si.Amount;//cl.Price * cl.Quantity;
                            cl.Amount = si.CurrentAmount;
                            //cl.Amount = Convert.ToInt32(si.IsDiscount ? cl.Sum * cl.Discount / 100 : cl.Sum);
                            //cl.Amount = si.IsDiscount ? si.CurrentAmount * para.Discount / 100 : si.CurrentAmount;
                        }
                        else
                        {
                            cl.Price = si.CurrentSalePrice;
                            cl.Sum = si.CurrentAmount;
                            cl.Amount = si.IsDiscount ? si.CurrentAmount * para.Discount / 100 : si.CurrentAmount;
                        }
                    }
                    uow.ConsumeList.Add(cl);

                    DXInfo.Models.BillInvLists bl = new DXInfo.Models.BillInvLists();

                    bl.Bill = bill.Id;
                    if (para.SourceType == (int)DXInfo.Models.SourceType.ColdDrinkShop)
                    {
                        if (si.CupType != null)
                        {
                            bl.CupType = si.CupType.Name;
                        }
                    }
                    bl.Name = si.Name;
                    bl.Quantity = si.Quantity;
                    if (si.IsInvDynamicPrice)
                    {
                        bl.Discount = si.IsDiscount ? si.Discount : 100;
                    }
                    else
                    {
                        bl.Discount = si.IsDiscount ? para.Discount : 100;
                    }
                    if (para.SourceType == (int)DXInfo.Models.SourceType.WesternRestaurant)
                    {
                        bl.Amount = si.Amount;
                        bl.SalePrice = si.SalePrice;
                    }
                    else
                    {
                        if (si.IsInvDynamicPrice)
                        {
                            bl.SalePrice = si.SalePrice;
                            bl.AgreementPrice = si.AgreementPrice;
                            //if (si.IsInvPrice && si.InvPrice != null)
                            //{
                            //    bl.SalePrice = si.InvPrice.SalePrice;
                            //}
                            bl.Sum = si.Amount;//bl.SalePrice * bl.Quantity;
                            bl.Amount = si.CurrentAmount;//Convert.ToInt32(si.IsDiscount ? bl.Sum * bl.Discount / 100 : bl.Sum);
                        }
                        else
                        {
                            bl.SalePrice = si.CurrentSalePrice;
                            bl.Sum = si.CurrentAmount;
                            bl.Amount = si.IsDiscount ? si.CurrentAmount * para.Discount / 100 : si.CurrentAmount;
                        }
                    }
                    if (para.SourceType == (int)DXInfo.Models.SourceType.ColdDrinkShop)
                    {
                        bl.Tastes = si.lTasteEx.Where(w => w.IsSelected == true).Count() == 0 ? "" : si.lTasteEx.Where(w => w.IsSelected == true).Select(l => l.Name).Aggregate((total, next) => (total + "," + next));//si.Tastes;
                    }
                    uow.BillInvLists.Add(bl);

                    if (para.SourceType == (int)DXInfo.Models.SourceType.ColdDrinkShop && 
                        si.lTasteEx.Count > 0)
                    {
                        uow.Commit();
                        foreach (var lt in si.lTasteEx.Where(w=>w.IsSelected))
                        {
                            DXInfo.Models.ConsumeTastes ct = new DXInfo.Models.ConsumeTastes();
                            ct.ConsumeList = cl.Id;
                            ct.Taste = lt.Id;
                            uow.ConsumeTastes.Add(ct);

                        }
                    }

                    if (si.InvPrice != null)
                    {
                        uow.Commit();
                        DXInfo.Models.ConsumeInvPrice invPrice = Mapper.Map<DXInfo.Models.ConsumeInvPrice>(si.InvPrice);
                        invPrice.ConsumeListId = cl.Id;
                        invPrice.InvPriceId = si.InvPrice.Id;
                        uow.ConsumeInvPrice.Add(invPrice);
                        uow.Commit();
                    }
                }

                if (dPoint > 0 && para.IsCard)
                {
                    CardPoint(para.CardId.Value,para.CreateDate,para.DeptId,para.UserId,dPoint);
                }
                uow.Commit();
                transaction.Complete();
            }
            return true;
        }
        public bool CancelCheckOut(CancelCheckoutParaObj para, Func<string, decimal, bool> CardCancelConsume)
        {
            if (para.ConsumeType == (int)DXInfo.Models.ConsumeType.Card && 
                para.IsCard && 
                !para.IsVirtual)
            {
                if (!CardCancelConsume(para.CardNo,para.Amount)) return false;
            }
            using (TransactionScope transaction = new TransactionScope())
            {
                DXInfo.Models.Consume consume = uow.Consume.GetAll().Where(w => w.Id == para.ConsumeId && w.IsValid).FirstOrDefault();
                if (consume == null) return false;
                if (consume != null)
                {
                    consume.IsValid = false;
                    uow.Consume.Update(consume);
                }
                if (para.IsCard && para.ConsumeType == (int)DXInfo.Models.ConsumeType.Card)
                {
                    CardUpdate(para.IsCardLevelAuto,para.CardId,para.Balance,0);
                }
                DXInfo.Models.Bills oldBill = uow.Bills.GetAll().Where(w => w.Sn == consume.Sn&&!w.BillType.Contains("Cancel")).FirstOrDefault();
                if (oldBill != null)
                {
                    DXInfo.Models.Bills bill = DXInfo.Business.Helper.CloneOf<DXInfo.Models.Bills>(oldBill);
                    bill.Amount = para.Amount;
                    bill.Balance = para.Balance;
                    bill.CreateDate = para.CreateDate;
                    bill.DeptName = para.DeptName;
                    bill.FullName = para.UserName + "," + para.FullName;
                    bill.LastBalance = para.LastBalance;
                    bill.BillType = para.BillType;
                    uow.Bills.Add(bill);
                    uow.Commit();
                    List<DXInfo.Models.BillInvLists> lOldBillInvList = uow.BillInvLists.GetAll().Where(w => w.Bill == oldBill.Id).ToList();
                    if (lOldBillInvList != null)
                    {
                        foreach (DXInfo.Models.BillInvLists oldBillInvList in lOldBillInvList)
                        {
                            DXInfo.Models.BillInvLists billInvList = DXInfo.Business.Helper.CloneOf<DXInfo.Models.BillInvLists>(oldBillInvList);
                            billInvList.Bill = bill.Id;
                            uow.BillInvLists.Add(billInvList);
                        }
                    }
                }
                
                List<DXInfo.Models.ConsumeList> lConsumeList = uow.ConsumeList.GetAll().Where(w => w.Consume == consume.Id && w.IsValid).ToList();
                foreach (DXInfo.Models.ConsumeList cl in lConsumeList)
                {                    
                    cl.IsValid = false;
                    uow.ConsumeList.Update(cl);
                }

                if (consume.Point > 0 && para.IsCard)
                {
                    CardCancelPoint(para.CardId,para.CreateDate,para.DeptId,para.UserId,consume.Point);
                }
                uow.Commit();
                transaction.Complete();
            }
            return true;
        }
        public void StickerBill(ObservableCollection<DXInfo.Models.InventoryEx> oiex, string deskNo,DateTime dCreateDate,string deptName)
        {
            if (oiex.Where(w => !string.IsNullOrEmpty(w.Printer)).Count() == 0) return;
            int idx = 0;
            decimal dQuantity = oiex.Sum(s => s.Quantity);
            using (TransactionScope transaction = new TransactionScope())
            {
                foreach (DXInfo.Models.InventoryEx iex in oiex)
                {
                    if (!string.IsNullOrEmpty(iex.Printer))
                    {
                        int cout = Convert.ToInt32(iex.Quantity);
                        for (int i = 0; i < cout; i++)
                        {
                            idx++;
                            DXInfo.Models.Bills bill = new DXInfo.Models.Bills();
                            bill.BillType = DXInfo.Models.BillType.Sticker.ToString();
                            bill.CreateDate = dCreateDate;
                            bill.DeptName = deptName;
                            bill.DeskNo = deskNo;
                            bill.CardNo = idx.ToString();
                            bill.Amount = iex.CurrentSalePrice;
                            bill.Sum = dQuantity;
                            bill.PayTypeName = iex.Printer;
                            uow.Bills.Add(bill);
                            uow.Commit();

                            DXInfo.Models.BillInvLists bl = new DXInfo.Models.BillInvLists();
                            bl.Bill = bill.Id;
                            bl.CupType = iex.CupType.Name;
                            bl.Name = iex.Name;
                            bl.Quantity = idx;
                            bl.SalePrice = iex.CurrentSalePrice;
                            bl.Tastes = iex.lTasteEx.Where(w => w.IsSelected == true).Count() == 0 ? "" : iex.lTasteEx.Where(w => w.IsSelected == true).Select(l => l.Name).Aggregate((total, next) => (total + "," + next));//si.Tastes;
                            uow.BillInvLists.Add(bl);
                        }
                    }
                }
                uow.Commit();
                transaction.Complete();
            }
        }
        #endregion
        
    }
}
