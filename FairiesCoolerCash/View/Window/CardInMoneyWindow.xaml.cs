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
using DXInfo.Data.Contracts;

namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// CardInMoneyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CardInMoneyWindow : Window
    {
        //private DXInfo.Models.FairiesMemberManage db = new DXInfo.Models.FairiesMemberManage();
        private readonly IFairiesMemberManageUow uow;
        public CardInMoneyWindow(IFairiesMemberManageUow uow, dynamic d)
        {
            this.uow = uow;
            
            InitializeComponent();
            string printTitle = Common.PrintTicketTitle(uow, DXInfo.Models.NameCodeType.PrintTicketTitleOfMember);
            if(!string.IsNullOrEmpty(printTitle))
            {
                this.txtTitle.Text = printTitle;
            }
            GridPrint.DataContext = d;
        }
        public bool IsPrint { get; set; }
        public bool IsPutCard { get; set; }
        public void Print()
        {
            Button_Click(null, null);

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsPrint)
            {
                PrintDialog pDialog1 = new PrintDialog();
                pDialog1.PrintVisual(GridPrint, "会员充值打印");
                DialogResult = true;
                this.Close();
                return;
            }
            dynamic d = GridPrint.DataContext;

            StringBuilder sb = new StringBuilder(33);
            sb.Append(d.CardNo);
            int value = Convert.ToInt32((d.Amount + d.Donate) * 100);
#if !DEBUG
            int st = CardRef.CoolerRechargeCard(sb, value);
#else
            //string strCardNo = "12347";
            int st = 0;
#endif
            if (st != 0)
            {
                MessageBox.Show(CardRef.GetStr(st));
                DialogResult = false;
                this.Close();
                return;
            }

            DXInfo.Models.Recharges recharge = new DXInfo.Models.Recharges();
            recharge.Amount = d.Amount;
            recharge.Donate = d.Donate;
            recharge.LastBalance = d.LastBalance;
            recharge.Balance = d.Balance;
            recharge.Card = d.Id;
            recharge.CreateDate = d.CreateDate;
            recharge.UserId = d.UserId;
            recharge.DeptId = d.DeptId;
            recharge.PayType = d.PayType;
            if (this.IsPutCard)
            {
                recharge.RechargeType = 2;
            }
            Guid cardid = d.Id;
            DXInfo.Models.Cards card = uow.Cards.GetById(cardid);//.Where(w => w.Id == cardid).FirstOrDefault();
            if (card == null)
                throw new ArgumentException("此卡信息未找到");
            card.Balance = recharge.Balance;

            uow.Recharges.Add(recharge);

            //小票
            DXInfo.Models.Bills bill = new DXInfo.Models.Bills();
            bill.Amount = d.Amount;
            bill.Balance = d.Balance;
            bill.BillType = "CardInMoneyWindow";
            bill.CardNo = d.CardNo;
            bill.CreateDate = d.CreateDate;
            bill.DeptName = d.DeptName;
            bill.Donate = d.Donate;
            bill.FullName = d.FullName;
            bill.LastBalance = d.LastBalance;
            bill.MemberName = d.MemberName;
            bill.PayTypeName = d.PayTypeName;

            uow.Bills.Add(bill);

            uow.Commit();

            //PrintDialog pDialog = new PrintDialog();
            //pDialog.PrintVisual(GridPrint, "会员充值打印");
            //MyPrint mp = new MyPrint();
            //mp.CardInMoneyPrint(uow, d);

            DialogResult = true;
            this.Close();
        }
    }
}
