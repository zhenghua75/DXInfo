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
using DXInfo.Data.Contracts;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// CardConsumeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CardConsumeWindow : Window
    {
        //private DXInfo.Models.FairiesMemberManage db = new DXInfo.Models.FairiesMemberManage();
        private readonly IFairiesMemberManageUow uow;
        public CardConsumeWindow(IFairiesMemberManageUow uow)
        {
            this.uow = uow;
            InitializeComponent();
            string printTitle = Common.PrintTicketTitle(uow, DXInfo.Models.NameCodeType.PrintTicketTitle1OfCold);
            if (!string.IsNullOrEmpty(printTitle))
            {
                this.txtTitle.Text = printTitle;
            }
        }
        public CardConsumeWindow(dynamic d)
        {
            InitializeComponent();
            GridPrint.DataContext = d;
            if (d.CardDonateInventory == null || d.CardDonateInventory.Count==0)
            {
                CDI.Visibility = System.Windows.Visibility.Collapsed;
            }
            if (d.Voucher == 0)
            {
                txtV1.Visibility = System.Windows.Visibility.Collapsed;
                txtV2.Visibility = System.Windows.Visibility.Collapsed;
            }
            string printTitle = Common.PrintTicketTitle(uow, DXInfo.Models.NameCodeType.PrintTicketTitle1OfCold);
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
            if (d.Amount > 0)
            {
                StringBuilder sb = new StringBuilder(33);
                sb.Append(d.CardNo);
                int value = Convert.ToInt32(d.Amount * 100);
                int st =  CardRef.CoolerConsumeCard(sb, value);
                if (st != 0)
                {
                    MessageBox.Show(CardRef.GetStr(st));
                    return;
                }
            }
            using (TransactionScope transaction = new TransactionScope())
            {


                DXInfo.Models.Consume consume = new DXInfo.Models.Consume();
                consume.Sum = d.Sum;
                consume.Voucher = d.Voucher;
                consume.PayVoucher = d.PayVoucher;
                consume.Discount = d.Discount;
                consume.Card = d.Id;
                consume.Amount = d.Amount;
                consume.Balance = d.Balance;
                consume.CreateDate = d.CreateDate;
                consume.DeptId = d.DeptId;
                consume.LastBalance = d.LastBalance;
                consume.Point = d.Point;
                consume.UserId = d.UserId;
                consume.Point = d.Point;
                consume.ConsumeType = 0;
                consume.DeskNo = txtDeskNo.Text;
                uow.Consume.Add(consume);


                DXInfo.Models.Bills bill = new DXInfo.Models.Bills();
                bill.Sum = d.Sum;
                bill.Voucher = d.Voucher;
                bill.Discount = d.Discount;

                bill.Amount = d.Amount;
                bill.Balance = d.Balance;
                bill.BillType = "CardConsumeWindow";
                bill.CardNo = d.CardNo;
                bill.CreateDate = d.CreateDate;
                bill.DeptName = d.DeptName;
                bill.FullName = d.FullName;
                bill.LastBalance = d.LastBalance;
                bill.MemberName = d.MemberName;
                bill.DeskNo = txtDeskNo.Text;
                uow.Bills.Add(bill);

                uow.Commit();

                Guid CardId = d.Id;
                DXInfo.Models.Cards card = uow.Cards.GetById(CardId);//.Where(w => w.Id == CardId).FirstOrDefault();
                if (card == null)
                    throw new ArgumentException("卡信息未找到");
                card.Balance = d.Balance;

                if (d.CardDonateInventory!=null && d.CardDonateInventory.Count > 0)
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
                foreach (var si in d.lSelInv)
                {
                    DXInfo.Models.ConsumeList cl = new DXInfo.Models.ConsumeList();
                    cl.Amount = si.Amount;
                    cl.Consume = consume.Id;
                    cl.CreateDate = d.CreateDate;
                    cl.Cup = si.Cup;
                    cl.DeptId = d.DeptId;
                    cl.Inventory = si.Id;
                    cl.Price = si.SalePrice;
                    cl.Quantity = si.Quantity;
                    cl.UserId = d.UserId;

                    uow.ConsumeList.Add(cl);

                    DXInfo.Models.BillInvLists bl = new DXInfo.Models.BillInvLists();
                    bl.Amount = si.Amount;
                    bl.Bill = bill.Id;
                    bl.CupType = si.CupType;
                    bl.Name = si.Name;
                    bl.Quantity = si.Quantity;
                    bl.SalePrice = si.SalePrice;
                    bl.Tastes = si.Tastes;

                    uow.BillInvLists.Add(bl);



                    if (si.lTastes.Count > 0)
                    {
                        uow.Commit();
                        foreach (var lt in si.lTastes)
                        {
                            DXInfo.Models.ConsumeTastes ct = new DXInfo.Models.ConsumeTastes();
                            ct.ConsumeList = cl.Id;
                            ct.Taste = lt.Id;
                            uow.ConsumeTastes.Add(ct);

                            


                        }
                    }
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
            pDialog.PrintVisual(GridPrint, "会员卡消费打印");

            DialogResult = true;
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!IsPrint)
            {
                dynamic d = GridPrint.DataContext;

                CardConsumePwdWindow ncw = new CardConsumePwdWindow(uow,d.Id);
                if (ncw.ShowDialog().GetValueOrDefault())
                {
                    txtDeskNo.Text = ncw.txtDeskNo.Text;
                }
                else
                {
                    this.Close();
                }
            }
        }
    }
}
