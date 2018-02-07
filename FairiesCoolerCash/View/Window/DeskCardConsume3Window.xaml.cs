using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Transactions;
using System.Collections.ObjectModel;
using DXInfo.Data.Contracts;
using AutoMapper;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// CardConsumeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DeskCardConsume3Window : Window
    {
        //private DXInfo.Models.FairiesMemberManage db = new DXInfo.Models.FairiesMemberManage();
        private readonly IFairiesMemberManageUow uow;
        private bool IsCancel { get; set; }
        public DeskCardConsume3Window(IFairiesMemberManageUow uow)
        {
            this.uow = uow;
            InitializeComponent();
            string printTitle = Common.PrintTicketTitle(uow, DXInfo.Models.NameCodeType.PrintTicketTitle1OfWR);
            if (!string.IsNullOrEmpty(printTitle))
            {
                this.txtTitle.Text = printTitle;
            }
        }
        public DeskCardConsume3Window(IFairiesMemberManageUow uow,dynamic d)
        {
            this.uow = uow;
            InitializeComponent();
            GridPrint.DataContext = d;
            GridPrint2.DataContext = d;
            if (d.CardDonateInventory == null || d.CardDonateInventory.Count==0)
            {
                CDI.Visibility = System.Windows.Visibility.Collapsed;
                CDI2.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (d.Voucher == 0)
            {
                txtV1.Visibility = System.Windows.Visibility.Collapsed;
                txtV2.Visibility = System.Windows.Visibility.Collapsed;
            }
            decimal dsum = d.Sum;
            decimal damount = d.Amount;
            txtDiscountAmount.Text = (dsum - damount).ToString("f2");
            string printTitle = Common.PrintTicketTitle(uow, DXInfo.Models.NameCodeType.PrintTicketTitle1OfWR);
            if (!string.IsNullOrEmpty(printTitle))
            {
                this.txtTitle.Text = printTitle;
            }
        }
        public bool IsPrint { get; set; }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsPrint)
            {
                PrintDialog pDialog1 = new PrintDialog();
                pDialog1.PrintVisual(GridPrint, "会员卡消费打印");
                DialogResult = true;
                this.Close();
                return;
            }
            dynamic d = GridPrint.DataContext;

            decimal dCash = Convert.ToDecimal(txtCash.Text);
            decimal dChange = Convert.ToDecimal(txtChange.Text);
            if (dCash - dChange < d.Amount) throw new ArgumentException("收的钱应不小于消费金额");

            using (TransactionScope transaction = new TransactionScope())
            {

                Guid orderId = d.OrderId;
                DXInfo.Models.OrderDishes orderDish = uow.OrderDishes.GetById(orderId);//.Where(w => w.Id == orderId).FirstOrDefault();
                if (orderDish.Status != 3)
                {
                    MessageBox.Show("确认下单后才可以结账！");
                    DialogResult = false;
                    this.Close();
                    return;
                }
                orderDish.Status = 1;
                var q = from o in uow.OrderDeskes.GetAll() where o.OrderId == orderDish.Id select o;
                foreach (DXInfo.Models.OrderDeskes orderDesk in q)
                {
                    orderDesk.Status = 1;

                    DXInfo.Models.OrderDeskesHis deskHis = new DXInfo.Models.OrderDeskesHis();
                    deskHis.DeskId = orderDesk.DeskId;
                    deskHis.LinkId = orderDesk.Id;
                    deskHis.OrderId = orderDesk.OrderId;
                    deskHis.UserId = d.UserId;
                    deskHis.CreateDate = DateTime.Now;
                    deskHis.Status = 1;
                    uow.OrderDeskesHis.Add(deskHis);
                }

                DXInfo.Models.Consume consume = new DXInfo.Models.Consume();
                consume.OrderId = d.OrderId;
                consume.Sum = d.Sum;
                consume.Voucher = d.Voucher;
                consume.PayVoucher = d.PayVoucher;
                consume.Discount = d.Discount;
                consume.Card = d.Id;
                consume.Amount = d.Amount;
                consume.CreateDate = d.CreateDate;
                consume.DeptId = d.DeptId;
                consume.Point = d.Point;
                consume.UserId = d.UserId;
                consume.Point = d.Point;
                consume.ConsumeType = 3;
                consume.SourceType = 1;

                consume.Cash = dCash;
                consume.Change = dChange;

                consume.Quantity = d.Count;
                if (d.PayType != Guid.Empty)
                {
                    consume.PayType = d.PayType;
                }
                uow.Consume.Add(consume);


                DXInfo.Models.Bills bill = new DXInfo.Models.Bills();
                bill.Sum = d.Sum;
                bill.Voucher = d.Voucher;
                bill.Discount = d.Discount;

                bill.Amount = d.Amount;
                bill.BillType = "WRCardConsume3Window";
                bill.CardNo = d.CardNo;
                bill.CreateDate = d.CreateDate;
                bill.DeptName = d.DeptName;
                bill.FullName = d.FullName;
                bill.MemberName = d.MemberName;

                bill.PayTypeName = d.PayTypeName;
                bill.Cash = dCash;
                bill.Change = dChange;
                uow.Bills.Add(bill);

                uow.Commit();

                Guid CardId = d.Id;
                DXInfo.Models.Cards card = uow.Cards.GetById(CardId);//.Where(w => w.Id == CardId).FirstOrDefault();
                if (card == null)
                    throw new ArgumentException("卡信息未找到");

                if (d.CardDonateInventory != null && d.CardDonateInventory.Count > 0)
                {
                    foreach (var cdi in d.CardDonateInventory)
                    {
                        DXInfo.Models.ConsumeDonateInv cdonate = new DXInfo.Models.ConsumeDonateInv();
                        cdonate.Consume = consume.Id;
                        cdonate.Inventory = cdi.Id;
                        uow.ConsumeDonateInv.Add(cdonate);

                        Guid gInvId = cdi.Id;
                        var cdi1 = uow.CardDonateInventory.GetAll().Where(w => w.Inventory == gInvId).Where(w => w.CardId == CardId).FirstOrDefault();
                        if (cdi1 != null)
                        {
                            cdi1.IsValidate = false;
                        }

                        DXInfo.Models.BillDonateInvLists bd = new DXInfo.Models.BillDonateInvLists();
                        bd.Bill = bill.Id;
                        bd.InvName = cdi.Name;

                        uow.BillDonateInvLists.Add(bd);
                    }
                }
                //DXInfo.Restaurant.DeskManageFacade dmf = new DXInfo.Restaurant.DeskManageFacade();
                foreach (var si in d.lSelInv)
                {
                    Guid orderMenuId = si.OrderMenuId;
                    DXInfo.Models.OrderMenus om = uow.OrderMenus.GetById(orderMenuId);//.Where(w => w.Id == orderMenuId).FirstOrDefault();
                    om.OperDate = DateTime.Now;
                    om.UserId = App.MyIdentity.oper.UserId;
                    om.Status = 8;
                    uow.OrderMenus.Update(om);

                    DXInfo.Models.OrderMenusHis omHis = Mapper.Map<DXInfo.Models.OrderMenusHis>(om);//dmf.CreateOrderMenuHis(om, App.MyIdentity.oper.UserId, DateTime.Now);
                    omHis.LinkId = om.Id;
                    uow.OrderMenusHis.Add(omHis);

                    DXInfo.Models.ConsumeList cl = new DXInfo.Models.ConsumeList();
                    cl.Amount = si.IsDiscount ? Math.Floor(si.Amount * d.Discount / 100) : si.Amount;
                    cl.Consume = consume.Id;
                    cl.CreateDate = d.CreateDate;
                    cl.DeptId = d.DeptId;
                    cl.Inventory = si.Id;
                    cl.Price = si.Price;
                    cl.Quantity = si.Quantity;
                    cl.UserId = d.UserId;
                    cl.Sum = si.Amount;
                    cl.Discount = si.IsDiscount ? d.Discount : 100;
                    cl.IsPackage = si.IsPackage;
                    cl.PackageId = si.PackageId;
                    uow.ConsumeList.Add(cl);

                    DXInfo.Models.BillInvLists bl = new DXInfo.Models.BillInvLists();
                    bl.Amount = si.Amount;
                    bl.Bill = bill.Id;
                    bl.CupType = si.EnglishName;
                    bl.Name = si.Name;
                    bl.Quantity = si.Quantity;
                    bl.SalePrice = si.Price;
                    uow.BillInvLists.Add(bl);
                }


                if (d.Point > 0)
                {
                    var cps = uow.CardPoints.GetAll().Where(w => w.Card == CardId).Where(w => w.PointType == 0).FirstOrDefault();
                    if (cps != null)
                    {
                        cps.Point = cps.Point + d.Point;
                    }
                    else
                    {
                        DXInfo.Models.CardPoints cp = new DXInfo.Models.CardPoints();
                        cp.Card = d.Id;
                        cp.CreateDate = d.CreateDate;
                        cp.DeptId = d.DeptId;
                        cp.Point = d.Point;
                        cp.PointType = 0;
                        cp.UserId = d.UserId;

                        uow.CardPoints.Add(cp);
                    }
                }
                uow.Commit();
                transaction.Complete();
            }
            PrintDialog pDialog = new PrintDialog();
            pDialog.PageRangeSelection = PageRangeSelection.AllPages;
            pDialog.UserPageRangeEnabled = true;
            this.Measure(new Size(pDialog.PrintableAreaWidth, pDialog.PrintableAreaHeight));
            this.Arrange(new Rect(new Point(0, 0), this.DesiredSize));
            pDialog.PrintVisual(GridPrint, "会员卡消费打印");
            DialogResult = true;
            this.Close();
        }
        private void Text_GotFocus(object sender, RoutedEventArgs e)
        {
            Keyboard.IsOpen = App.IsOpen;
            Keyboard.Placement = System.Windows.Controls.Primitives.PlacementMode.MousePoint;
        }
        private void Text_LostFocus(object sender, RoutedEventArgs e)
        {
            Keyboard.IsOpen = false;

            UpdateCash();
        }
        private void UpdateCash()
        {
            dynamic d = GridPrint.DataContext;
            decimal dCash = 0;
            try
            {
                dCash = Convert.ToDecimal(txtCash.Text);
            }
            catch
            {
                throw new ArgumentException("金额请输入数字");
            }

            if (dCash < d.Amount) throw new ArgumentException("收的钱应不小于消费金额");

            decimal dChange = 0;
            dChange = dCash - d.Amount;
            txtChange.Text = dChange.ToString();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsPrint)
            {
                dynamic d = GridPrint.DataContext;
                if (!string.IsNullOrEmpty(d.CardPwd))
                {
                    DeskCardCashWindow ncw = new DeskCardCashWindow(uow, d.Id);
                    if (ncw.ShowDialog().GetValueOrDefault())
                    {
                        txtCount.Text = Convert.ToString(d.Count);
                        this.IsCancel = false;
                    }
                    else
                    {
                        this.IsCancel = true;
                        this.Close();
                    }
                }
                DeskNoMemberCashWindow ncw1 = new DeskNoMemberCashWindow(d.Amount);
                if (ncw1.ShowDialog().GetValueOrDefault())
                {
                    decimal dCash = 0;
                    try
                    {
                        if (!string.IsNullOrEmpty(ncw1.txtCash.Text))
                        {
                            dCash = Convert.ToDecimal(ncw1.txtCash.Text);
                        }
                        else
                        {
                            dCash = d.Amount;
                        }
                    }
                    catch
                    {
                        throw new ArgumentException("金额请输入数字");
                    }

                    if (dCash < d.Amount) throw new ArgumentException("收的钱应不小于消费金额");

                    decimal dChange = 0;
                    dChange = dCash - d.Amount;
                    txtCash.Text = dCash.ToString();
                    txtChange.Text = dChange.ToString();
                    txtCount.Text = Convert.ToString(d.Count);
                    this.IsCancel = false;

                }
                else
                {
                    this.IsCancel = true;
                    this.Close();
                }
            }
        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            if (!this.IsCancel)
            {
                Button_Click(null, null);
            }
        }
    }
}
