using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using FairiesCoolerCash.Business;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Microsoft.Reporting.WinForms;
using System.Data;

namespace FairiesCoolerCash.ViewModel
{
    /// <summary>
    /// 发卡充值
    /// </summary>
    public class PutCardInMoneyViewModel : BusinessViewModelBase
    {
        public PutCardInMoneyViewModel(IFairiesMemberManageUow uow)
            : base(uow, new List<string>() { "SelectedPayType", "Amount" })
        {
            this.swipingCard();
        }
        public override void LoadData()
        {
            this.SetlPayType();
            this.SetlCardLevel();
            this.SetlCardType();
        }
        protected override void AfterSwipingCard()
        {
            base.AfterSwipingCard();
            this.SelectedPayType = this.lPayTypeOfPutCard.Find(f => f.Name == "现金");
        }
        #region 充值
        private void CardInMoneyExecute()
        {
            if (!this.Amount.HasValue) throw new ArgumentNullException("请输入充值金额");
            decimal dAmount = this.Amount.Value;
            decimal dDonate = 0;
            if (this.Donate.HasValue)
                dDonate = this.Donate.Value;
            decimal dLastBalance = 0;
            if (this.CardBalance.HasValue)
                dLastBalance = this.CardBalance.Value;
            decimal dBalance = dAmount + dDonate + dLastBalance;
            DateTime dCreateDate = DateTime.Now;
            if (!this.CardType.IsVirtual)
            {
                StringBuilder sb = new StringBuilder(33);
                sb.Append(this.Card.CardNo);
                int value = Convert.ToInt32((dAmount + dDonate) * 100);
//#if !DEBUG
            int st = CardRef.CoolerRechargeCard(sb, value);
//#else
//                //string strCardNo = "12347";
//                int st = 0;
//#endif
                if (st != 0)
                {
                    Helper.ShowErrorMsg(CardRef.GetStr(st));
                    return;
                }
            }

            DXInfo.Business.MemberManageFacade mb = new DXInfo.Business.MemberManageFacade(Uow);
            DXInfo.Business.CardInMoneyParaObj para = new DXInfo.Business.CardInMoneyParaObj();
            para.DeptId = Dept.DeptId;
            para.DeptName = Dept.DeptName;
            para.UserId = User.UserId;
            para.UserName = User.UserName;
            para.FullName = Oper.FullName;
            para.PayTypeId = SelectedPayType.Id;
            para.PayTypeName = SelectedPayType.Name;
            para.CardId = Card.Id;
            para.LastBalance = dLastBalance;
            para.Balance = dBalance;
            para.MemberName = Member.MemberName;
            para.CreateDate = dCreateDate;
            para.Amount = dAmount;
            para.Donate = dDonate;
            para.RechargeType = (int)DXInfo.Models.RechargeType.PutCardInMoney;
            mb.CardInMoney(para);

            if (this.IsThree)
            {
                LocalReport report = new LocalReport();
                report.ReportPath = GetThreePrintFile(DXInfo.Models.NameCodeType.ThreePrintInMoney);
                NoMemberThreePrintObject threePrintObject = new NoMemberThreePrintObject();
                threePrintObject.Title = ClientCommon.PrintTicketTitle(DXInfo.Models.NameCodeType.PrintTicketTitleOfMember);
                threePrintObject.CardNo = this.Card.CardNo;
                threePrintObject.MemberName = this.Member.MemberName;
                threePrintObject.LastBalance = dLastBalance;
                threePrintObject.Amount = dAmount;
                threePrintObject.Donate = dDonate;
                threePrintObject.PayTypeName = this.SelectedPayType.Name;
                threePrintObject.Balance = dBalance;
                threePrintObject.FullName = this.Oper.FullName;
                threePrintObject.UserName = this.User.UserName;
                threePrintObject.DeptName = this.Dept.DeptName;
                threePrintObject.CreateDate = dCreateDate;
                threePrintObject.ButtomTitle = ClientCommon.PrintTicketButtomTitle(DXInfo.Models.NameCodeType.ThreeButtomTitleInMoney);
                DataTable dt = threePrintObject.ToDataTable();
                report.DataSources.Add(
                   new ReportDataSource("DataSet1", dt)
                   );
                PrintRDLC printRDLC = new PrintRDLC();
                printRDLC.Run(report);
            }
            else
            {
                string title = ClientCommon.PrintTicketTitle(DXInfo.Models.NameCodeType.PrintTicketTitleOfMember);
                InMoneyPrintObject po = new InMoneyPrintObject(title,this.Card.CardNo,
                    this.Member.MemberName, dLastBalance, dBalance, dAmount, dDonate, this.SelectedPayType.Name
                    , this.Oper.FullName, this.User.UserName, this.Dept.DeptName, dCreateDate);
                po.Print();
            }
            MessageBox.Show("充值成功");

            this.ResetSwipingCard();
            this.Amount = null;
            this.Donate = null;            
            this.SelectedPayType = null;
        }
        private bool CardInMoneyCanExecute()
        {
            if (this.Card == null || this.Member == null || this.SelectedPayType == null || !this.Amount.HasValue || this.Amount == 0)
                return false;
            return true;
        }
        public ICommand CardInMoney
        {
            get
            {
                return new RelayCommand(CardInMoneyExecute, CardInMoneyCanExecute);
            }
        }
        #endregion
    }
}
