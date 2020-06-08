using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Controls;
using FairiesCoolerCash.Business;
//using System.Data.Objects.SqlClient;
using System.Collections.ObjectModel;
using AutoMapper;
using Microsoft.Reporting.WinForms;
using System.Data;

namespace FairiesCoolerCash.ViewModel
{
    public class BillRepeatViewModel : ReportViewModelBase
    {
        public bool IsCupType { get; set; }
        public bool IsInvDynamicPrice { get; set; }
        private readonly IMapper mapper;
        public BillRepeatViewModel(IFairiesMemberManageUow uow, IMapper mapper)
            : base(uow,mapper)
        {
            this.IsCupType = BusinessCommon.IsCupType();
            this.IsInvDynamicPrice = BusinessCommon.IsInvDynamicPrice();
            this.mapper = mapper;
        }
        public override void LoadData()
        {
            this.SetlOper();
            this.SetlBillType();
        }
        #region 查询
        protected override void query()
        {
            string billType = DXInfo.Models.NameCodeType.BillType.ToString();
            //查询
            var ms = from s in Uow.Bills.GetAll()
                     join d1 in Uow.NameCode.GetAll() on s.BillType equals d1.Code into dd1
                     from dd1s in dd1.DefaultIfEmpty()
                     where s.DeptName == this.Dept.DeptName && dd1s.Type == billType
                     orderby s.CreateDate
                     select new
            {
                s.Id,
                s.Amount,
                s.Balance,
                BillTypeName = dd1s.Name,
                BillType = dd1s.Code,
                s.CardNo,
                s.CreateDate
                ,
                s.DeptName,
                s.Donate,
                s.FullName,
                s.LastBalance,
                s.MemberName
            };
            if (!string.IsNullOrWhiteSpace(this.MemberName))
                ms = ms.Where(w => w.MemberName.Contains(this.MemberName));
            if (!string.IsNullOrWhiteSpace(this.CardNo))
                ms = ms.Where(w => w.CardNo.Contains(this.CardNo));
            if (this.SelectedBillType!=null)
            {
                ms = ms.Where(w => w.BillType == this.SelectedBillType.Code);
            }
            if (this.SelectedOper!=null)
                ms = ms.Where(w => w.FullName == this.SelectedOper.FullName);
            if (this.BeginDate > DateTime.MinValue)
                ms = ms.Where(w => w.CreateDate >= this.BeginDate);
            if (this.EndDate > DateTime.MinValue)
            {
                DateTime dtNext = this.EndDate.AddDays(1);
                ms = ms.Where(w => w.CreateDate <= dtNext);
            }
            this.MyQuery = ms;
        }
        #endregion

        #region 打印
        public ICommand Print
        {
            get
            {
                return new RelayCommand<object>(print);
            }
        }
        private void print(object sender)
        {
            //打印
            Guid mid = Guid.Parse((sender as Button).Tag.ToString());
            DXInfo.Models.Bills bill = Uow.Bills.GetById(g => g.Id == mid);
            if (bill != null)
            {
                List<DXInfo.Models.BillInvLists> lBillInvList = Uow.BillInvLists.GetAll().Where(w => w.Bill == bill.Id).ToList();
                List<DXInfo.Models.BillDonateInvLists> lBillDonateInvList = Uow.BillDonateInvLists.GetAll().Where(w => w.Bill == bill.Id).ToList();


                ObservableCollection<DXInfo.Models.InventoryEx> oiex = new ObservableCollection<DXInfo.Models.InventoryEx>();
                foreach (DXInfo.Models.BillInvLists billInvList in lBillInvList)
                {                    
                    DXInfo.Models.InventoryEx iex = mapper.Map<DXInfo.Models.InventoryEx>(billInvList);
                    iex.CupType = new DXInfo.Models.MyEnum();
                    iex.CupType.Name = billInvList.CupType;
                    iex.lTasteEx = new DXInfo.Models.TasteExList();
                    iex.IsInvDynamicPrice = this.IsInvDynamicPrice;
                    if (!string.IsNullOrEmpty(billInvList.Tastes))
                    {
                        string[] strTastes = billInvList.Tastes.Split(',');
                        foreach (string taste in strTastes)
                        {
                            if (!string.IsNullOrEmpty(taste))
                            {
                                DXInfo.Models.TasteEx tex = new DXInfo.Models.TasteEx();
                                tex.IsSelected = true;
                                tex.Name = taste;
                                iex.lTasteEx.Add(tex);
                            }
                        }
                    }
                    oiex.Add(iex);
                }
                List<DXInfo.Models.CardDonateInventoryEx> lcdi = new List<DXInfo.Models.CardDonateInventoryEx>();
                foreach (DXInfo.Models.BillDonateInvLists billInvList in lBillDonateInvList)
                {
                    DXInfo.Models.CardDonateInventoryEx cdi = mapper.Map<DXInfo.Models.CardDonateInventoryEx>(billInvList);
                    lcdi.Add(cdi);
                }
                DateTime dCreateDate = bill.CreateDate.Value;
                decimal dSum = bill.Sum.HasValue ? bill.Sum.Value : 0;
                decimal dQuantity = oiex.Sum(s => s.Quantity);
                string title = "";
                decimal dBalance = bill.Balance.HasValue ? bill.Balance.Value : 0;
                decimal dDiscount = bill.Discount.HasValue ? bill.Discount.Value : 0;
                decimal dAmount = bill.Amount.HasValue ? bill.Amount.Value : 0;
                decimal dVoucher = bill.Voucher.HasValue ? bill.Voucher.Value : 0;
                DeskNo = bill.DeskNo;
                List<string> lFullName = new List<string>();
                string[] strFullNames = bill.FullName.Split(',');
                foreach (string strFullName in strFullNames)
                {
                    lFullName.Add(strFullName);
                }
                if (lFullName.Count == 1)
                {
                    lFullName.Add(strFullNames[0]);
                }
                string userName = lFullName[0];
                string operName = lFullName[1];
                string deptName = bill.DeptName;
                decimal dLastBalance = bill.LastBalance.HasValue ? bill.LastBalance.Value : 0;
                decimal dDonate = bill.Donate.HasValue ? bill.Donate.Value : 0;
                string payTypeName = bill.PayTypeName;
                string cardNo = bill.CardNo;
                string memberName = bill.MemberName;
                decimal dCash = bill.Cash;
                decimal dChange = bill.Change;
                switch (bill.BillType)
                {
                    case "CardConsumeWindow":
                        title = ClientCommon.PrintTicketTitle(DXInfo.Models.NameCodeType.PrintTicketTitle1OfCold);
                        if (this.IsTicket1)
                        {
                            MemberConsumePrintObject po = new MemberConsumePrintObject(oiex, lcdi, DeskNo, deptName, dCreateDate, dSum, dQuantity,this.IsCupType);
                            ;
                            po.Print();
                        }
                        if (this.IsTicket2)
                        {
                            MemberConsumePrintObject2 po2 = new MemberConsumePrintObject2(title, oiex, lcdi, cardNo, memberName, dLastBalance,
                                dBalance, dSum, dDiscount, dAmount, dVoucher, DeskNo,
                                 operName, userName, deptName, dCreateDate, this.IsCupType);
                            po2.Print();
                        }
                        if (this.IsThree)
                        {
                            LocalReport report = new LocalReport();
                            report.ReportPath = GetThreePrintFile(DXInfo.Models.NameCodeType.SaleThreePrintMemmber);//@"Report1.rdlc";
                            NoMemberThreePrintObject threePrintObject = new NoMemberThreePrintObject();
                            threePrintObject.Title = title;
                            threePrintObject.DeskNo = DeskNo;
                            threePrintObject.PeopleCount = bill.PeopleCount.HasValue?bill.PeopleCount.Value:0;
                            threePrintObject.Amount = dAmount;
                            threePrintObject.CreateDate = dCreateDate;
                            threePrintObject.ButtomTitle = GetButtomTitle(DXInfo.Models.DeptType.Sale);
                            threePrintObject.Sum = dSum;
                            threePrintObject.DeptName = Dept.DeptName;
                            threePrintObject.Voucher = dVoucher;
                            threePrintObject.FullName = Oper.FullName;
                            threePrintObject.UserName = User.UserName;
                            threePrintObject.PayTypeName = payTypeName;

                            threePrintObject.CardNo = cardNo;
                            threePrintObject.MemberName = memberName;
                            threePrintObject.Discount = dDiscount;
                            threePrintObject.Balance = dBalance;
                            threePrintObject.LastBalance = dLastBalance;

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
                        break;
                    case "CardInMoneyWindow":
                        if (this.IsThree)
                        {
                            LocalReport report = new LocalReport();
                            report.ReportPath = GetThreePrintFile(DXInfo.Models.NameCodeType.ThreePrintInMoney);
                            NoMemberThreePrintObject threePrintObject = new NoMemberThreePrintObject();
                            threePrintObject.Title = ClientCommon.PrintTicketTitle(DXInfo.Models.NameCodeType.PrintTicketTitleOfMember);
                            threePrintObject.CardNo = cardNo;
                            threePrintObject.MemberName = memberName;
                            threePrintObject.LastBalance = dLastBalance;
                            threePrintObject.Amount = dAmount;
                            threePrintObject.Donate = dDonate;
                            threePrintObject.PayTypeName = payTypeName;
                            threePrintObject.Balance = dBalance;
                            threePrintObject.FullName = operName;
                            threePrintObject.UserName = userName;
                            threePrintObject.DeptName = deptName;
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
                            title = ClientCommon.PrintTicketTitle(DXInfo.Models.NameCodeType.PrintTicketTitleOfMember);
                            InMoneyPrintObject ipo = new InMoneyPrintObject(title,cardNo,
                    memberName, dLastBalance, dBalance, dAmount, dDonate, payTypeName
                    , operName, userName, deptName, dCreateDate);
                            ipo.Print();
                        }
                        break;
                    case "NoMemberConsumeWindow":
                        title = ClientCommon.PrintTicketTitle(DXInfo.Models.NameCodeType.PrintTicketTitle1OfCold);
                        if (this.IsTicket1)
                        {
                            NoMemberConsumePrintObject npo = new NoMemberConsumePrintObject(oiex, DeskNo, deptName, dCreateDate, dSum, dQuantity,this.IsCupType);
                            ;
                            npo.Print();
                        }
                        if (this.IsTicket3)
                        {
                            NoMemberConsumePrintObject2 npo2 = new NoMemberConsumePrintObject2(title, oiex, dSum, dAmount, dVoucher, dCash, dChange, payTypeName, DeskNo,
                                operName, userName, deptName, dCreateDate,this.IsCupType);
                            npo2.Print();
                        }
                        if (this.IsThree)
                        {
                            LocalReport report = new LocalReport();
                            report.ReportPath = GetThreePrintFile(DXInfo.Models.NameCodeType.SaleThreePrintNoMemmber);//@"Report1.rdlc";
                            NoMemberThreePrintObject threePrintObject = new NoMemberThreePrintObject();
                            threePrintObject.Title = title;
                            threePrintObject.DeskNo = DeskNo;
                            threePrintObject.PeopleCount = bill.PeopleCount.HasValue ? bill.PeopleCount.Value : 0;
                            threePrintObject.Amount = dAmount;
                            threePrintObject.CreateDate = dCreateDate;
                            threePrintObject.Change = dChange;
                            threePrintObject.Cash = dCash;
                            threePrintObject.ButtomTitle = GetButtomTitle(DXInfo.Models.DeptType.Sale);
                            threePrintObject.Sum = dSum;
                            threePrintObject.DeptName = Dept.DeptName;
                            threePrintObject.Voucher = dVoucher;
                            threePrintObject.FullName = Oper.FullName;
                            threePrintObject.UserName = User.UserName;
                            threePrintObject.PayTypeName = payTypeName;
                            threePrintObject.Discount = dDiscount;
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
                        break;
                    case "PointsExchangeWindow":
                        var bl1 = (from d in lBillInvList select new { d.Amount, d.Bill, d.CupType, d.Id, d.Name, d.Quantity, d.SalePrice, d.Tastes, EnglishName = d.CupType, Price = d.SalePrice }).ToList();
                        var bd = Uow.BillDonateInvLists.GetAll().Where(w => w.Bill == bill.Id).Select(s => new { Name = s.InvName }).ToList();
                        var p = new
                        {
                            bill.Amount,
                            bill.Balance,
                            bill.CardNo,
                            bill.DeptName,
                            bill.Donate,
                            bill.FullName,
                            bill.LastBalance,
                            bill.Sum,
                            bill.Voucher,
                            bill.Discount,
                            bill.MemberName,
                            bill.CreateDate,
                            lSelInv = bl1,
                            bill.PayTypeName,
                            bill.Change,
                            bill.Cash,
                            bill.DeskNo,
                            CardDonateInventory = bd
                        };
                        PointsExchangeWindow cw3 = new PointsExchangeWindow(Uow, p);
                        cw3.IsPrint = true;
                        cw3.ShowDialog();
                        break;
                    case "WRCardConsumeWindow":
                        title = ClientCommon.PrintTicketTitle(DXInfo.Models.NameCodeType.PrintTicketTitle1OfWR);
                        if (this.IsThree)
                        {
                            LocalReport report = new LocalReport();
                            report.ReportPath = GetThreePrintFile(DXInfo.Models.NameCodeType.ThreePrintMemmber);//@"Report1.rdlc";
                            NoMemberThreePrintObject threePrintObject = new NoMemberThreePrintObject();
                            threePrintObject.Title = title;
                            threePrintObject.DeskNo = DeskNo;
                            threePrintObject.PeopleCount = bill.PeopleCount.HasValue ? bill.PeopleCount.Value : 0;
                            threePrintObject.Amount = dAmount;
                            threePrintObject.CreateDate = dCreateDate;
                            threePrintObject.ButtomTitle = GetButtomTitle(DXInfo.Models.DeptType.Shop);
                            threePrintObject.Sum = dSum;
                            threePrintObject.DeptName = Dept.DeptName;
                            threePrintObject.Voucher = dVoucher;
                            threePrintObject.FullName = Oper.FullName;
                            threePrintObject.UserName = User.UserName;
                            threePrintObject.PayTypeName = payTypeName;

                            threePrintObject.CardNo = cardNo;
                            threePrintObject.MemberName = memberName;
                            threePrintObject.Discount = dDiscount;
                            threePrintObject.Balance = dBalance;
                            threePrintObject.LastBalance = dLastBalance;

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
                            WRMemberConsumePrintObject2 wpo2 = new WRMemberConsumePrintObject2(title, oiex, lcdi, cardNo, memberName, dLastBalance,
                                dBalance, dSum, dDiscount, dAmount, dVoucher, DeskNo,
                                 operName, userName, deptName, dCreateDate,this.Dept.Comment);
                            wpo2.Print();
                        }
                        break;
                    case "WRNoMemberConsumeWindow":
                        title = ClientCommon.PrintTicketTitle(DXInfo.Models.NameCodeType.PrintTicketTitle1OfWR);
                        if (this.IsThree)
                        {
                            LocalReport report = new LocalReport();
                            report.ReportPath = GetThreePrintFile(DXInfo.Models.NameCodeType.ThreePrintNoMemmber);//@"Report1.rdlc";
                            NoMemberThreePrintObject threePrintObject = new NoMemberThreePrintObject();
                            threePrintObject.Title = title;
                            threePrintObject.DeskNo = DeskNo;
                            threePrintObject.PeopleCount = bill.PeopleCount.HasValue ? bill.PeopleCount.Value : 0;
                            threePrintObject.Amount = dAmount;
                            threePrintObject.CreateDate = dCreateDate;
                            threePrintObject.Change = dChange;
                            threePrintObject.Cash = dCash;
                            threePrintObject.ButtomTitle = GetButtomTitle(DXInfo.Models.DeptType.Shop);
                            threePrintObject.Sum = dSum;
                            threePrintObject.DeptName = Dept.DeptName;
                            threePrintObject.Voucher = dVoucher;
                            threePrintObject.FullName = Oper.FullName;
                            threePrintObject.UserName = User.UserName;
                            threePrintObject.PayTypeName = payTypeName;

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
                            WRNoMemberConsumePrintObject2 wnpo2 = new WRNoMemberConsumePrintObject2(title, oiex, dSum, dAmount, dVoucher, dCash, dChange, payTypeName, DeskNo,
                                operName, userName, deptName, dCreateDate,this.Dept.Comment);
                            wnpo2.Print();
                        }
                        break;
                    case "WRCardConsume3Window":
                        title = ClientCommon.PrintTicketTitle(DXInfo.Models.NameCodeType.PrintTicketTitle1OfWR);
                        if (this.IsThree)
                        {
                            LocalReport report = new LocalReport();
                            report.ReportPath = GetThreePrintFile(DXInfo.Models.NameCodeType.ThreePrintMemmberNoMoney);
                            NoMemberThreePrintObject threePrintObject = new NoMemberThreePrintObject();
                            threePrintObject.Title = title;
                            threePrintObject.DeskNo = DeskNo;
                            threePrintObject.PeopleCount = bill.PeopleCount.HasValue ? bill.PeopleCount.Value : 0;
                            threePrintObject.Amount = dAmount;
                            threePrintObject.CreateDate = dCreateDate;
                            threePrintObject.Change = dChange;
                            threePrintObject.Cash = dCash;
                            threePrintObject.ButtomTitle = GetButtomTitle(DXInfo.Models.DeptType.Shop);
                            threePrintObject.Sum = dSum;
                            threePrintObject.DeptName = Dept.DeptName;
                            threePrintObject.Voucher = dVoucher;
                            threePrintObject.FullName = Oper.FullName;
                            threePrintObject.UserName = User.UserName;
                            threePrintObject.PayTypeName = payTypeName;

                            threePrintObject.CardNo = cardNo;
                            threePrintObject.MemberName = memberName;
                            threePrintObject.Discount = dDiscount;

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
                            WRMemberConsumePrintObject3 wpo3 = new WRMemberConsumePrintObject3(title, oiex, lcdi, cardNo, memberName, dCash,
                                    dChange, dSum, dDiscount, dAmount, dVoucher, payTypeName, DeskNo,
                                     operName, userName, deptName, dCreateDate,this.Dept.Comment);
                            wpo3.Print();
                        }
                        break;
                    case "CardConsume3Window":
                        title = ClientCommon.PrintTicketTitle(DXInfo.Models.NameCodeType.PrintTicketTitle1OfCold);
                        if (this.IsTicket1)
                        {
                            MemberConsumePrintObject mpo = new MemberConsumePrintObject(oiex, lcdi, DeskNo, deptName, dCreateDate, dSum, dQuantity, this.IsCupType);
                            ;
                            mpo.Print();
                        }
                        if (this.IsTicket2)
                        {
                            MemberConsumePrintObject3 mpo3 = new MemberConsumePrintObject3(title, oiex, lcdi, cardNo, memberName, dCash,
                                dChange, dSum, dDiscount, dAmount, dVoucher, payTypeName, DeskNo,
                                 operName, userName, deptName, dCreateDate, this.IsCupType);
                            mpo3.Print();
                        }
                        if (this.IsThree)
                        {
                            LocalReport report = new LocalReport();
                            report.ReportPath = GetThreePrintFile(DXInfo.Models.NameCodeType.SaleThreePrintMemmberNoMoney);
                            NoMemberThreePrintObject threePrintObject = new NoMemberThreePrintObject();
                            threePrintObject.Title = title;
                            threePrintObject.DeskNo = DeskNo;
                            threePrintObject.PeopleCount = bill.PeopleCount.HasValue ? bill.PeopleCount.Value : 0;
                            threePrintObject.Amount = dAmount;
                            threePrintObject.CreateDate = dCreateDate;
                            threePrintObject.Change = dChange;
                            threePrintObject.Cash = dCash;
                            threePrintObject.ButtomTitle = GetButtomTitle(DXInfo.Models.DeptType.Sale);
                            threePrintObject.Sum = dSum;
                            threePrintObject.DeptName = Dept.DeptName;
                            threePrintObject.Voucher = dVoucher;
                            threePrintObject.FullName = Oper.FullName;
                            threePrintObject.UserName = User.UserName;
                            threePrintObject.PayTypeName = payTypeName;

                            threePrintObject.CardNo = cardNo;
                            threePrintObject.MemberName = memberName;
                            threePrintObject.Discount = dDiscount;

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
                        break;
                    case "Sticker":
                        //MyBusiness mb = new MyBusiness(Uow,this.Oper.UserId,this.Dept.DeptId,this.Dept.OrganizationId);
                        int count = Convert.ToInt32(dSum);
                        DXInfo.Models.InventoryEx iex = oiex[0];
                        int idx = Convert.ToInt32(iex.Quantity);
                        StickerPrintObject opo = new StickerPrintObject(payTypeName, iex, DeskNo,
                                deptName, dCreateDate, idx, count,iex.Name,iex.SalePrice.ToString(),iex.CupType.Name);
                        opo.Print();
                        break;
                }
            }

        }
        #endregion
    }
}
