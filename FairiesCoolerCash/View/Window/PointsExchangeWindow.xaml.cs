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
    /// PointsExchangeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PointsExchangeWindow : Window
    {
        //private DXInfo.Models.FairiesMemberManage db = new DXInfo.Models.FairiesMemberManage();
        private readonly IFairiesMemberManageUow uow;
        private Common ClientCommon;
        public PointsExchangeWindow(IFairiesMemberManageUow uow)
        {
            this.uow = uow;
            InitializeComponent();
            this.ClientCommon = new Common(uow);
            string printTitle = ClientCommon.PrintTicketTitle(DXInfo.Models.NameCodeType.PrintTicketTitleOfMember);
            if (!string.IsNullOrEmpty(printTitle))
            {
                this.txtTitle.Text = printTitle;
            }
        }
        public PointsExchangeWindow(IFairiesMemberManageUow uow,dynamic d)
        {
            this.uow = uow;
            InitializeComponent();
            GridPrint.DataContext = d;
            string printTitle = ClientCommon.PrintTicketTitle(DXInfo.Models.NameCodeType.PrintTicketTitleOfMember);
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
                pDialog1.PrintVisual(GridPrint, "会员积分兑换打印");
                DialogResult = true;
                this.Close();
                return;
            }
            dynamic d = GridPrint.DataContext;
            using (TransactionScope transaction = new TransactionScope())
            {


                DXInfo.Models.Consume consume = new DXInfo.Models.Consume();
                consume.Card = d.Id;
                consume.Sum = d.Amount;
                consume.Discount = 100;
                consume.Amount = d.Amount;
                consume.Balance = d.Balance;
                consume.CreateDate = d.CreateDate;
                consume.DeptId = d.DeptId;
                consume.LastBalance = d.LastBalance;
                consume.UserId = d.UserId;
                consume.ConsumeType = 2;
                uow.Consume.Add(consume);


                DXInfo.Models.Bills bill = new DXInfo.Models.Bills();
                bill.Amount = d.Amount;
                bill.Balance = d.Balance;
                bill.BillType = "PointsExchangeWindow";
                bill.CardNo = d.CardNo;
                bill.CreateDate = d.CreateDate;
                bill.DeptName = d.DeptName;
                bill.FullName = d.FullName;
                bill.LastBalance = d.LastBalance;
                bill.MemberName = d.MemberName;

                uow.Bills.Add(bill);

                uow.Commit();

                Guid cid = d.Id;

                var p = uow.CardPoints.GetAll().Where(w => w.Card == cid);

                decimal dkp = 0;
                foreach (DXInfo.Models.CardPoints cp in p)
                {

                    if (cp.Point > d.Amount - dkp)
                    {
                        dkp = d.Amount;
                        cp.Point = cp.Point - d.Amount;
                        break;
                    }
                    else
                    {
                        dkp += cp.Point;
                        cp.Point = 0;
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
                    cl.Price = si.SalePoint;
                    cl.Quantity = si.Quantity;
                    cl.UserId = d.UserId;

                    uow.ConsumeList.Add(cl);

                    DXInfo.Models.BillInvLists bl = new DXInfo.Models.BillInvLists();
                    bl.Amount = si.Amount;
                    bl.Bill = bill.Id;
                    bl.CupType = si.CupType;
                    bl.Name = si.Name;
                    bl.Quantity = si.Quantity;
                    bl.SalePrice = si.SalePoint;
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
                uow.Commit();
                transaction.Complete();
            }
            PrintDialog pDialog = new PrintDialog();

            pDialog.PrintVisual(GridPrint, "会员积分兑换打印");
            DialogResult = true;
            this.Close();
        }
    }
}
