using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using DXInfo.Print;

namespace FairiesCoolerCash.Business
{
    #region 打印基类
    [AttributeUsage(AttributeTargets.Property)]
    public class DataColumnAttribute : Attribute { }
    public class MyBatchStickerPrintable : IPrintable
    {
        public Font HeadFont { get; set; }
        public Font BodyFont { get; set; }
        public string CardNo { get; set; }
        public string DeptName { get; set; }
        public DateTime CreateDate { get; set; }
        public string DeskNo { get; set; }
        public ObservableCollection<DXInfo.Models.InventoryEx> lInventoryEx { get; set; }
        public DXInfo.Models.InventoryEx InventoryEx { get; set; }
        public int Count { get; set; }
        public int Idx { get; set; }

        public MyBatchStickerPrintable()
        {
            HeadFont = new Font("宋体", 20, FontStyle.Bold);
            BodyFont = new Font("宋体", 7, FontStyle.Bold);
        }
        public virtual void Print(PrintElement element, Graphics graphics) { }

    }
    public class MyPrintable : IPrintable
    {
        [DataColumn]
        public string Customer { get; set; }
        [DataColumn]
        public int PeopleCount { get; set; }
        [DataColumn]
        public string Title { get; set; }
        [DataColumn]
        public Font HeadFont { get; set; }
        [DataColumn]
        public Font BodyFont { get; set; }
        [DataColumn]
        public Font BodyBigFont { get; set; }
        [DataColumn]
        public Font BodySmallFont { get; set; }
        [DataColumn]
        public PrintEngine MyEngine { get; private set; }
        [DataColumn]
        public string CardNo { get; set; }
        [DataColumn]
        public string MemberName { get; set; }
        [DataColumn]
        public decimal LastBalance { get; set; }
        [DataColumn]
        public decimal Amount { get; set; }
        [DataColumn]
        public decimal Donate { get; set; }
        [DataColumn]
        public string PayTypeName { get; set; }
        [DataColumn]
        public decimal Balance { get; set; }
        [DataColumn]
        public string FullName { get; set; }
        [DataColumn]
        public string UserName { get; set; }
        [DataColumn]
        public string DeptName { get; set; }
        [DataColumn]
        public DateTime CreateDate { get; set; }
        [DataColumn]
        public string DeskNo { get; set; }
        public ObservableCollection<DXInfo.Models.InventoryEx> lInventoryEx { get; set; }
        [DataColumn]
        public DXInfo.Models.InventoryEx InventoryEx { get; set; }
        [DataColumn]
        public decimal Sum { get; set; }
        [DataColumn]
        public decimal Quantity { get; set; }
        [DataColumn]
        public decimal Voucher { get; set; }
        [DataColumn]
        public decimal Cash { get; set; }
        [DataColumn]
        public decimal Change { get; set; }
        public List<DXInfo.Models.CardDonateInventoryEx> lCardDonateInventoryEx { get; set; }
        [DataColumn]
        public decimal Discount { get; set; }
        [DataColumn]
        public int Idx { get; set; }
        [DataColumn]
        public int Count { get; set; }
        [DataColumn]
        public string InvName { get; set; }
        [DataColumn]
        public string Comment { get; set; }
        [DataColumn]
        public DXInfo.Models.MyFormat MyFormat { get; set; }
        [DataColumn]
        public bool IsCupType { get; set; }
        public MyPrintable()
            : this(null)
        {
        }

        public MyPrintable(float top, float left, float right)
            : this(null, top, left, right)
        {
        }
        public MyPrintable(string printerName)
            : this(printerName, 0, 0, 0)
        {
        }

        public MyPrintable(string printerName, float top, float left, float right)
        {
            this.SetFont();
            this.SetFormat();
            MyEngine = new PrintEngine(printerName, top, left, right);
        }
        private void SetFont()
        {
            HeadFont = new Font("黑体", 22, FontStyle.Bold);
            BodyFont = new Font("黑体", 10, FontStyle.Bold);
            BodyBigFont = new Font("黑体", 12, FontStyle.Bold);
            BodySmallFont = new Font("黑体", 8, FontStyle.Bold);
        }
        private void SetFormat()
        {
            MyFormat = new DXInfo.Models.MyFormat();
        }
        public virtual void Print(PrintElement element, Graphics graphics) { }
        public void Print()
        {
            MyEngine.AddPrintObject(this);
            try
            {
                System.Drawing.Printing.PaperSize ps = MyEngine.DefaultPageSettings.PaperSize;

                float height = MyEngine.CalculateHeight();


                MyEngine.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(ps.PaperName, ps.Width, (int)height);//("Custom", 386, 813);
                MyEngine.Print();
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMsg(ex.Message);
                Helper.HandelException(ex);
            }
        }
    }
    #endregion

    #region 充值打印
    /// <summary>
    /// 充值打印
    /// </summary>
    public class InMoneyPrintObject : MyPrintable
    {
        public InMoneyPrintObject(string title,
            string cardNo, string memberName, decimal lastBalance, decimal balance, decimal amount, decimal donate,
            string payTypeName, string fullName, string userName, string deptName, DateTime createDate)
            : base(0, 0, 40)
        {
            this.Title = title;
            this.CardNo = cardNo;
            this.MemberName = memberName;
            this.LastBalance = lastBalance;
            this.Amount = amount;
            this.Donate = donate;
            this.PayTypeName = payTypeName;
            this.Balance = balance;
            this.FullName = fullName;
            this.UserName = userName;
            this.DeptName = deptName;
            this.CreateDate = createDate;
        }
        public override void Print(PrintElement element, Graphics graphics)
        {
            SizeF sf = graphics.MeasureString("上次余额", BodyFont);
            float maxLength = sf.Width;
            element.AddMiddleText(Title, HeadFont);
            element.AddHorizontalRule();
            element.AddPairText("卡号", CardNo, maxLength, BodyFont, BodyFont);
            element.AddPairText("会员名", string.Format(MyFormat, "{0:NameStar}", MemberName),
                maxLength, BodyFont, BodyFont);
            element.AddPairText("上次余额", string.Format(MyFormat, "{0:DelZero}", LastBalance),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("充值金额", string.Format(MyFormat, "{0:DelZero}", Amount),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("赠送金额", string.Format(MyFormat, "{0:DelZero}", Donate),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("支付方式", PayTypeName, maxLength, BodyFont, BodyFont);
            element.AddPairText("当前余额", string.Format(MyFormat, "{0:DelZero}", Balance),
                maxLength, BodyFont, BodyBigFont);
            element.AddHorizontalRule();
            //element.AddPairText("操作员", FullName, maxLength, BodyFont);
            element.AddPairText("操作员", UserName, maxLength, BodyFont, BodyFont);
            element.AddPairText("门店", DeptName, maxLength, BodyFont, BodyFont);
            element.AddPairText("充值时间", CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                maxLength, BodyFont, BodySmallFont);
        }
    }
    #endregion

    #region 消费打印 58 纸右边24
    public class NoMemberConsumePrintObject : MyPrintable
    {
        public NoMemberConsumePrintObject(ObservableCollection<DXInfo.Models.InventoryEx> lInventoryEx,
            string deskNo, string deptName, DateTime createDate,
            decimal sum,
            decimal quantity,bool isCupType)
            : base(0, 0, 40)
        {
            this.DeskNo = deskNo;
            this.lInventoryEx = lInventoryEx;
            this.Sum = sum;
            this.Quantity = quantity;
            this.DeptName = deptName;
            this.CreateDate = createDate;
            this.IsCupType = isCupType;
        }
        public override void Print(PrintElement element, Graphics graphics)
        {
            SizeF sf = graphics.MeasureString("总计", BodyFont);
            SizeF sf1 = graphics.MeasureString("标准杯", BodyFont);
            SizeF sf2 = graphics.MeasureString("数量", BodyFont);
            SizeF sf3 = graphics.MeasureString("鸡汁京白菜（素菜）鸡", BodyFont);
            float maxLength = sf.Width;
            float maxLength1 = sf1.Width;
            float maxLength2 = sf2.Width;
            float maxLength3 = sf3.Width;
            element.AddMiddleText(DeskNo, HeadFont);
            element.AddHorizontalRule();
            if (this.IsCupType)
            {
                element.AddTripleText("名称", "杯型", "数量", maxLength1, maxLength2, BodyFont, BodyFont, BodyFont);
                if (lInventoryEx != null && lInventoryEx.Count > 0)
                {
                    for (int i = 0; i < lInventoryEx.Count; i++)
                    {
                        string count = (i + 1).ToString() + ".";
                        DXInfo.Models.InventoryEx ie = lInventoryEx[i];
                        string invName = DXInfo.Business.Helper.Truncate(ie.Name, 8);
                        element.AddTripleText(count + invName, ie.CupType.Name,
                            string.Format(MyFormat, "{0:DelZero}", ie.Quantity),
                            maxLength1, maxLength2, BodyFont, BodyFont, BodyBigFont);
                        string taste = "  ";
                        if (ie.lTasteEx != null && ie.lTasteEx.Count > 0)
                        {

                            ie.lTasteEx.Where(w => w.IsSelected).ToList().ForEach(delegate(DXInfo.Models.TasteEx te) { taste += te.Name + ","; });
                        }
                        if (taste.Length > 1)
                        {
                            taste = taste.Substring(0, taste.Length - 1);
                            element.AddText(taste, BodyFont);
                        }
                    }
                }
            }
            else
            {
                element.AddPairText("名称", "数量", maxLength3, BodyFont, BodyFont);
                if (lInventoryEx != null && lInventoryEx.Count > 0)
                {
                    for (int i = 0; i < lInventoryEx.Count; i++)
                    {
                        string count = (i + 1).ToString() + ".";
                        DXInfo.Models.InventoryEx ie = lInventoryEx[i];
                        string invName = DXInfo.Business.Helper.Truncate(ie.Name, 8);
                        element.AddPairText(count + invName, 
                            string.Format(MyFormat, "{0:DelZero}", ie.Quantity),
                            maxLength3, BodyFont, BodyFont);
                        string taste = "  ";
                        if (ie.lTasteEx != null && ie.lTasteEx.Count > 0)
                        {

                            ie.lTasteEx.Where(w => w.IsSelected).ToList().ForEach(delegate(DXInfo.Models.TasteEx te) { taste += te.Name + ","; });
                        }
                        if (taste.Length > 1)
                        {
                            taste = taste.Substring(0, taste.Length - 1);
                            element.AddText(taste, BodyFont);
                        }
                    }
                }
            }
            element.AddHorizontalRule();
            element.AddPairText("总计", string.Format(MyFormat, "{0:DelZero}", Sum),
                maxLength, BodyFont, BodyFont);
            element.AddPairText("总数", string.Format(MyFormat, "{0:DelZero}", Quantity),
                maxLength, BodyFont, BodyFont);
            element.AddPairText("门店", DeptName, maxLength, BodyFont, BodyFont);
            element.AddPairText("日期", CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                maxLength, BodyFont, BodySmallFont);
            element.AddText("凭此小票或号牌在吧台领取饮品", BodyFont);
            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddHorizontalRule();
        }
    }
    public class NoMemberConsumePrintObject2 : MyPrintable
    {
        public NoMemberConsumePrintObject2(
            string title,
            ObservableCollection<DXInfo.Models.InventoryEx> lInventoryEx,
            decimal sum,
            decimal amount,
            decimal voucher, decimal cash, decimal change,
            string payTypeName,
            string deskNo, string fullName, string userName,
            string deptName, DateTime createDate,bool isCupType
            )
            : base(0, 0, 40)
        {
            this.Title = title;
            this.DeskNo = deskNo;
            this.lInventoryEx = lInventoryEx;
            this.Sum = sum;
            this.DeptName = deptName;
            this.CreateDate = createDate;
            this.Voucher = voucher;
            this.Amount = amount;
            this.FullName = fullName;
            this.UserName = userName;
            this.Cash = cash;
            this.Change = change;
            this.PayTypeName = payTypeName;
            this.IsCupType = isCupType;
            
        }
        public override void Print(PrintElement element, Graphics graphics)
        {
            SizeF sf = graphics.MeasureString("消费金额", BodyFont);
            SizeF sf1 = graphics.MeasureString("标准杯", BodyFont);
            SizeF sf2 = graphics.MeasureString("数量", BodyFont);
            SizeF sf3 = graphics.MeasureString("鸡汁京白菜（素菜）鸡", BodyFont);
            float maxLength = sf.Width;
            float maxLength1 = sf1.Width;
            float maxLength2 = sf2.Width;
            float maxLength3 = sf3.Width;
            element.AddMiddleText(Title, HeadFont);
            #region 商品
            element.AddHorizontalRule();
            if (this.IsCupType)
            {
                element.AddTripleText("名称", "杯型", "数量", maxLength1, maxLength2, BodyFont, BodyFont, BodyFont);
                if (lInventoryEx != null && lInventoryEx.Count > 0)
                {
                    for (int i = 0; i < lInventoryEx.Count; i++)
                    {
                        string count = (i + 1).ToString() + ".";
                        DXInfo.Models.InventoryEx ie = lInventoryEx[i];
                        string invName = DXInfo.Business.Helper.Truncate(ie.Name, 8);
                        element.AddTripleText(count + invName, ie.CupType.Name,
                            string.Format(MyFormat, "{0:DelZero}", ie.Quantity),
                            maxLength1, maxLength2, BodyFont, BodyFont, BodyBigFont);
                        string taste = "  ";
                        if (ie.lTasteEx != null && ie.lTasteEx.Count > 0)
                        {

                            ie.lTasteEx.Where(w => w.IsSelected).ToList().ForEach(delegate(DXInfo.Models.TasteEx te) { taste += te.Name + ","; });
                        }
                        if (taste.Length > 1)
                        {
                            taste = taste.Substring(0, taste.Length - 1);
                            element.AddBreakText(taste, BodyFont);
                        }
                    }
                }
            }
            else
            {
                element.AddPairText("名称",  "数量",maxLength3,BodyFont,BodyFont);
                if (lInventoryEx != null && lInventoryEx.Count > 0)
                {
                    for (int i = 0; i < lInventoryEx.Count; i++)
                    {
                        string count = (i + 1).ToString() + ".";
                        DXInfo.Models.InventoryEx ie = lInventoryEx[i];
                        string invName = DXInfo.Business.Helper.Truncate(ie.Name, 8);
                        element.AddPairText(count + invName, 
                            string.Format(MyFormat, "{0:DelZero}", ie.Quantity),
                            maxLength3, BodyBigFont, BodyBigFont);
                        string taste = "  ";
                        if (ie.lTasteEx != null && ie.lTasteEx.Count > 0)
                        {

                            ie.lTasteEx.Where(w => w.IsSelected).ToList().ForEach(delegate(DXInfo.Models.TasteEx te) { taste += te.Name + ","; });
                        }
                        if (taste.Length > 1)
                        {
                            taste = taste.Substring(0, taste.Length - 1);
                            element.AddBreakText(taste, BodyFont);
                        }
                    }
                }
            }
            #endregion

            element.AddHorizontalRule();
            element.AddPairText("总计", string.Format(MyFormat, "{0:DelZero}", Sum),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("代金券", string.Format(MyFormat, "{0:DelZero}", Voucher),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("消费金额", string.Format(MyFormat, "{0:DelZero}", Amount),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("支付方式", PayTypeName, maxLength, BodyFont, BodyFont);
            element.AddPairText("收您", string.Format(MyFormat, "{0:DelZero}", Cash),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("找零", string.Format(MyFormat, "{0:DelZero}", Change),
                maxLength, BodyFont, BodyBigFont);
            //element.AddPairText("操作员", FullName, maxLength, BodyFont);
            element.AddPairText("操作员", UserName, maxLength, BodyFont, BodyFont);
            element.AddPairText("门店", DeptName, maxLength, BodyFont, BodyFont);
            element.AddPairText("号牌", DeskNo, maxLength, BodyFont, BodyFont);
            element.AddPairText("消费时间", CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                maxLength, BodyFont, BodySmallFont);
            element.AddText("", BodyFont);
            element.AddText("凭小票或号牌在吧台领取饮品", BodyFont);
            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddHorizontalRule();
        }
    }

    public class MemberConsumePrintObject : MyPrintable
    {
        public MemberConsumePrintObject(
            ObservableCollection<DXInfo.Models.InventoryEx> lInventoryEx,
            List<DXInfo.Models.CardDonateInventoryEx> lCardDonateInventoryEx,
            string deskNo, string deptName, DateTime createDate, decimal sum,
            decimal quantity,bool isCupType)
            : base(0, 0, 40)
        {
            this.DeskNo = deskNo;
            this.lInventoryEx = lInventoryEx;
            this.Sum = sum;
            this.Quantity = quantity;
            this.DeptName = deptName;
            this.CreateDate = createDate;
            this.lCardDonateInventoryEx = lCardDonateInventoryEx;
            this.IsCupType = isCupType;
        }
        public override void Print(PrintElement element, Graphics graphics)
        {
            SizeF sf = graphics.MeasureString("总计", BodyFont);
            SizeF sf1 = graphics.MeasureString("标准杯", BodyFont);
            SizeF sf2 = graphics.MeasureString("数量", BodyFont);
            SizeF sf3 = graphics.MeasureString("鸡汁京白菜（素菜）鸡", BodyFont);

            float maxLength = sf.Width;
            float maxLength1 = sf1.Width;
            float maxLength2 = sf2.Width;
            float maxLength3 = sf3.Width;
            element.AddMiddleText(DeskNo, HeadFont);

            if (lCardDonateInventoryEx != null && lCardDonateInventoryEx.Count > 0)
            {
                element.AddHorizontalRule();
                string strCardDonateInv = "赠送商品:";
                foreach (DXInfo.Models.CardDonateInventoryEx q in lCardDonateInventoryEx)
                {
                    strCardDonateInv += q.Name + ",";
                }
                strCardDonateInv = strCardDonateInv.Substring(0, strCardDonateInv.Length - 1);
                element.AddBreakText(strCardDonateInv, BodyFont);
            }
            element.AddHorizontalRule();
            if (this.IsCupType)
            {
                element.AddTripleText("名称", "杯型", "数量", maxLength1, maxLength2, BodyFont, BodyFont, BodyFont);
                if (lInventoryEx != null && lInventoryEx.Count > 0)
                {
                    for (int i = 0; i < lInventoryEx.Count; i++)
                    {
                        string count = (i + 1).ToString() + ".";
                        DXInfo.Models.InventoryEx ie = lInventoryEx[i];
                        string invName = DXInfo.Business.Helper.Truncate(ie.Name, 8);
                        element.AddTripleText(count + invName, ie.CupType.Name,
                            string.Format(MyFormat, "{0:DelZero}", ie.Quantity),
                            maxLength1, maxLength2, BodyFont, BodyFont, BodyBigFont);
                        string taste = "  ";
                        if (ie.lTasteEx != null && ie.lTasteEx.Count > 0)
                        {

                            ie.lTasteEx.Where(w => w.IsSelected).ToList().ForEach(delegate(DXInfo.Models.TasteEx te) { taste += te.Name + ","; });
                        }
                        if (taste.Length > 1)
                        {
                            taste = taste.Substring(0, taste.Length - 1);
                            element.AddBreakText(taste, BodyFont);
                        }
                    }
                }
            }
            else
            {
                element.AddPairText("名称", "数量", maxLength3, BodyFont, BodyFont);
                if (lInventoryEx != null && lInventoryEx.Count > 0)
                {
                    for (int i = 0; i < lInventoryEx.Count; i++)
                    {
                        string count = (i + 1).ToString() + ".";
                        DXInfo.Models.InventoryEx ie = lInventoryEx[i];
                        string invName = DXInfo.Business.Helper.Truncate(ie.Name, 8);
                        element.AddPairText(count + invName, 
                            string.Format(MyFormat, "{0:DelZero}", ie.Quantity),
                            maxLength3, BodyFont, BodyBigFont);
                        string taste = "  ";
                        if (ie.lTasteEx != null && ie.lTasteEx.Count > 0)
                        {

                            ie.lTasteEx.Where(w => w.IsSelected).ToList().ForEach(delegate(DXInfo.Models.TasteEx te) { taste += te.Name + ","; });
                        }
                        if (taste.Length > 1)
                        {
                            taste = taste.Substring(0, taste.Length - 1);
                            element.AddBreakText(taste, BodyFont);
                        }
                    }
                }
            }
            element.AddHorizontalRule();
            element.AddPairText("总计", string.Format(MyFormat, "{0:DelZero}", Sum),
                maxLength, BodyFont, BodyFont);
            element.AddPairText("总数", string.Format(MyFormat, "{0:DelZero}", Quantity),
                maxLength, BodyFont, BodyFont);
            element.AddPairText("门店", DeptName, maxLength, BodyFont, BodyFont);
            element.AddPairText("日期", CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                maxLength, BodyFont, BodySmallFont);
            //element.AddText("凭此小票或号牌在吧台领取饮品", BodyFont);
            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddHorizontalRule();
        }
    }
    public class MemberConsumePrintObject2 : MyPrintable
    {
        public MemberConsumePrintObject2(
            string title,
            ObservableCollection<DXInfo.Models.InventoryEx> lInventoryEx,
            List<DXInfo.Models.CardDonateInventoryEx> lCardDonateInventoryEx,
            string cardNo, string memberName,
            decimal lastBalance,
            decimal balance,
            decimal sum,
            decimal discount,
            decimal amount,
            decimal voucher,
            string deskNo, string fullName, string userName
            , string deptName, DateTime createDate,bool isCupType
            )
            : base(0, 0, 40)
        {
            this.Title = title;
            this.DeskNo = deskNo;
            this.lInventoryEx = lInventoryEx;
            this.Sum = sum;
            this.DeptName = deptName;
            this.CreateDate = createDate;
            this.lCardDonateInventoryEx = lCardDonateInventoryEx;
            this.CardNo = cardNo;
            this.MemberName = memberName;
            this.Voucher = voucher;
            this.Discount = discount;
            this.Amount = amount;
            this.Balance = balance;
            this.FullName = fullName;
            this.UserName = userName;
            this.LastBalance = lastBalance;
            this.IsCupType = isCupType;
        }
        public override void Print(PrintElement element, Graphics graphics)
        {
            SizeF sf = graphics.MeasureString("上次余额", BodyFont);
            SizeF sf1 = graphics.MeasureString("标准杯", BodyFont);
            SizeF sf2 = graphics.MeasureString("数量", BodyFont);
            SizeF sf3 = graphics.MeasureString("鸡汁京白菜（素菜）鸡", BodyFont);
            float maxLength = sf.Width;
            float maxLength1 = sf1.Width;
            float maxLength2 = sf2.Width;
            float maxLength3 = sf3.Width;
            element.AddMiddleText(Title, HeadFont);
            element.AddPairText("卡号", this.CardNo, maxLength, BodyFont, BodyFont);
            element.AddPairText("会员名", string.Format(MyFormat, "{0:NameStar}", this.MemberName),
                maxLength, BodyFont, BodyFont);
            element.AddPairText("上次余额", string.Format(MyFormat, "{0:DelZero}", this.LastBalance),
                maxLength, BodyFont, BodyBigFont);

            #region 赠送商品
            if (lCardDonateInventoryEx != null && lCardDonateInventoryEx.Count > 0)
            {
                element.AddHorizontalRule();
                string strCardDonateInv = "赠送商品:";
                foreach (DXInfo.Models.CardDonateInventoryEx q in lCardDonateInventoryEx)
                {
                    strCardDonateInv += q.Name + ",";
                }
                strCardDonateInv = strCardDonateInv.Substring(0, strCardDonateInv.Length - 1);
                element.AddBreakText(strCardDonateInv, BodyFont);
            }
            #endregion

            #region 商品
            element.AddHorizontalRule();
            if (this.IsCupType)
            {
                element.AddTripleText("名称", "杯型", "数量", maxLength1, maxLength2, BodyFont, BodyFont, BodyFont);
                if (lInventoryEx != null && lInventoryEx.Count > 0)
                {
                    for (int i = 0; i < lInventoryEx.Count; i++)
                    {
                        string count = (i + 1).ToString() + ".";
                        DXInfo.Models.InventoryEx ie = lInventoryEx[i];
                        string invName = DXInfo.Business.Helper.Truncate(ie.Name, 8);
                        element.AddTripleText(count + invName, ie.CupType.Name,
                            string.Format(MyFormat, "{0:DelZero}", ie.Quantity),
                            maxLength1, maxLength2, BodyFont, BodyFont, BodyBigFont);
                        string taste = "  ";
                        if (ie.lTasteEx != null && ie.lTasteEx.Count > 0)
                        {

                            ie.lTasteEx.Where(w => w.IsSelected).ToList().ForEach(delegate(DXInfo.Models.TasteEx te) { taste += te.Name + ","; });
                        }
                        if (taste.Length > 1)
                        {
                            taste = taste.Substring(0, taste.Length - 1);
                            element.AddBreakText(taste, BodyFont);
                        }
                    }
                }
            }
            else
            {
                element.AddPairText("名称", "数量", maxLength3, BodyFont, BodyFont);
                if (lInventoryEx != null && lInventoryEx.Count > 0)
                {
                    for (int i = 0; i < lInventoryEx.Count; i++)
                    {
                        string count = (i + 1).ToString() + ".";
                        DXInfo.Models.InventoryEx ie = lInventoryEx[i];
                        string invName = DXInfo.Business.Helper.Truncate(ie.Name, 8);
                        element.AddPairText(count + invName, 
                            string.Format(MyFormat, "{0:DelZero}", ie.Quantity),
                            maxLength3, BodyFont, BodyBigFont);
                        string taste = "  ";
                        if (ie.lTasteEx != null && ie.lTasteEx.Count > 0)
                        {

                            ie.lTasteEx.Where(w => w.IsSelected).ToList().ForEach(delegate(DXInfo.Models.TasteEx te) { taste += te.Name + ","; });
                        }
                        if (taste.Length > 1)
                        {
                            taste = taste.Substring(0, taste.Length - 1);
                            element.AddBreakText(taste, BodyFont);
                        }
                    }
                }
            }
            #endregion

            element.AddHorizontalRule();
            element.AddPairText("总计", string.Format(MyFormat, "{0:DelZero}", Sum),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("代金券", string.Format(MyFormat, "{0:DelZero}", Voucher),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("折扣", (Discount / 100).ToString("p0"),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("消费金额", string.Format(MyFormat, "{0:DelZero}", Amount),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("当前余额", string.Format(MyFormat, "{0:DelZero}", Balance),
                maxLength, BodyFont, BodyBigFont);
            //element.AddPairText("操作员", FullName, maxLength, BodyFont); 
            element.AddPairText("操作员", UserName, maxLength, BodyFont, BodyFont);
            element.AddPairText("门店", DeptName, maxLength, BodyFont, BodyFont);
            element.AddPairText("号牌", DeskNo, maxLength, BodyFont, BodyFont);
            element.AddPairText("消费时间", CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                maxLength, BodyFont, BodySmallFont);
            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            //element.AddWhiteLine();
            //element.AddWhiteLine();
            //element.AddWhiteLine();
            //element.AddWhiteLine();
            element.AddHorizontalRule();
        }
    }
    public class MemberConsumePrintObject3 : MyPrintable
    {
        public MemberConsumePrintObject3(
            string title,
            ObservableCollection<DXInfo.Models.InventoryEx> lInventoryEx,
            List<DXInfo.Models.CardDonateInventoryEx> lCardDonateInventoryEx,
            string cardNo, string memberName,
            decimal cash,
            decimal change,
            decimal sum,
            decimal discount,
            decimal amount,
            decimal voucher,
            string payTypeName,
            string deskNo, string fullName, string userName,
            string deptName, DateTime createDate, bool isCupType
            )
            : base(0, 0, 40)
        {
            this.Title = title;
            this.DeskNo = deskNo;
            this.lInventoryEx = lInventoryEx;
            this.Sum = sum;
            this.DeptName = deptName;
            this.CreateDate = createDate;
            this.lCardDonateInventoryEx = lCardDonateInventoryEx;
            this.CardNo = cardNo;
            this.MemberName = memberName;
            this.Voucher = voucher;
            this.Discount = discount;
            this.Amount = amount;
            this.Cash = cash;
            this.FullName = fullName;
            this.UserName = userName;
            this.Change = change;
            this.PayTypeName = payTypeName;
            this.IsCupType = isCupType;
        }
        public override void Print(PrintElement element, Graphics graphics)
        {
            SizeF sf = graphics.MeasureString("上次余额", BodyFont);
            SizeF sf1 = graphics.MeasureString("标准杯", BodyFont);
            SizeF sf2 = graphics.MeasureString("数量", BodyFont);
            SizeF sf3 = graphics.MeasureString("鸡汁京白菜（素菜）鸡", BodyFont);
            float maxLength = sf.Width;
            float maxLength1 = sf1.Width;
            float maxLength2 = sf2.Width;
            float maxLength3 = sf3.Width;
            element.AddMiddleText(Title, HeadFont);
            element.AddPairText("卡号", this.CardNo, maxLength, BodyFont, BodyFont);
            element.AddPairText("会员名",
                string.Format(MyFormat, "{0:NameStar}", this.MemberName),
                maxLength, BodyFont, BodyFont);

            #region 赠送商品
            if (lCardDonateInventoryEx != null && lCardDonateInventoryEx.Count > 0)
            {
                element.AddHorizontalRule();
                string strCardDonateInv = "赠送商品:";
                foreach (DXInfo.Models.CardDonateInventoryEx q in lCardDonateInventoryEx)
                {
                    strCardDonateInv += q.Name + ",";
                }
                strCardDonateInv = strCardDonateInv.Substring(0, strCardDonateInv.Length - 1);
                element.AddBreakText(strCardDonateInv, BodyFont);
            }
            #endregion

            #region 商品
            element.AddHorizontalRule();
            if (this.IsCupType)
            {
                element.AddTripleText("名称", "杯型", "数量", maxLength1, maxLength2, BodyFont, BodyFont, BodyFont);
                if (lInventoryEx != null && lInventoryEx.Count > 0)
                {
                    for (int i = 0; i < lInventoryEx.Count; i++)
                    {
                        string count = (i + 1).ToString() + ".";
                        DXInfo.Models.InventoryEx ie = lInventoryEx[i];
                        string invName = DXInfo.Business.Helper.Truncate(ie.Name, 8);
                        element.AddTripleText(count + invName, ie.CupType.Name,
                            string.Format(MyFormat, "{0:DelZero}", ie.Quantity),
                            maxLength1, maxLength2, BodyFont, BodyFont, BodyBigFont);
                        string taste = "  ";
                        if (ie.lTasteEx != null && ie.lTasteEx.Count > 0)
                        {

                            ie.lTasteEx.Where(w => w.IsSelected).ToList().ForEach(delegate(DXInfo.Models.TasteEx te) { taste += te.Name + ","; });
                        }
                        if (taste.Length > 1)
                        {
                            taste = taste.Substring(0, taste.Length - 1);
                            element.AddBreakText(taste, BodyFont);
                        }
                    }
                }
            }
            else
            {
                element.AddPairText("名称", "数量", maxLength3, BodyFont, BodyFont);
                if (lInventoryEx != null && lInventoryEx.Count > 0)
                {
                    for (int i = 0; i < lInventoryEx.Count; i++)
                    {
                        string count = (i + 1).ToString() + ".";
                        DXInfo.Models.InventoryEx ie = lInventoryEx[i];
                        string invName = DXInfo.Business.Helper.Truncate(ie.Name, 8);
                        element.AddPairText(count + invName, 
                            string.Format(MyFormat, "{0:DelZero}", ie.Quantity),
                            maxLength3, BodyFont, BodyBigFont);
                        string taste = "  ";
                        if (ie.lTasteEx != null && ie.lTasteEx.Count > 0)
                        {

                            ie.lTasteEx.Where(w => w.IsSelected).ToList().ForEach(delegate(DXInfo.Models.TasteEx te) { taste += te.Name + ","; });
                        }
                        if (taste.Length > 1)
                        {
                            taste = taste.Substring(0, taste.Length - 1);
                            element.AddBreakText(taste, BodyFont);
                        }
                    }
                }
            }
            #endregion

            element.AddHorizontalRule();
            element.AddPairText("总计", string.Format(MyFormat, "{0:DelZero}", Sum),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("代金券", string.Format(MyFormat, "{0:DelZero}", Voucher),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("折扣", (Discount / 100).ToString("p0"),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("消费金额", string.Format(MyFormat, "{0:DelZero}", Amount),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("支付方式", PayTypeName, maxLength, BodyFont, BodyFont);
            element.AddPairText("收您", string.Format(MyFormat, "{0:DelZero}", Cash),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("找零", string.Format(MyFormat, "{0:DelZero}", Change),
                maxLength, BodyFont, BodyBigFont);
            //element.AddPairText("操作员", FullName, maxLength, BodyFont);
            element.AddPairText("操作员", UserName, maxLength, BodyFont, BodyFont);
            element.AddPairText("门店", DeptName, maxLength, BodyFont, BodyFont);
            element.AddPairText("号牌", DeskNo, maxLength, BodyFont, BodyFont);
            element.AddPairText("消费时间", CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                maxLength, BodyFont, BodySmallFont);
            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddHorizontalRule();
        }
    }
    #endregion

    #region 消费打印 80纸 右边30
    /// <summary>
    /// 非会员结账
    /// </summary>
    public class WRNoMemberConsumePrintObject2 : MyPrintable
    {

        public WRNoMemberConsumePrintObject2(
            string title,
            ObservableCollection<DXInfo.Models.InventoryEx> lInventoryEx,
            decimal sum,
            decimal amount,
            decimal voucher, decimal cash, decimal change,
            string payTypeName,
            string deskNo, string fullName, string userName,
            string deptName, DateTime createDate,
            string comment
            )
            : base(0, 0, 30)
        {
            this.Title = title;
            this.DeskNo = deskNo;
            this.lInventoryEx = lInventoryEx;
            this.Sum = sum;
            this.DeptName = deptName;
            this.CreateDate = createDate;
            this.Voucher = voucher;
            this.Amount = amount;
            this.FullName = fullName;
            this.UserName = userName;
            this.Cash = cash;
            this.Change = change;
            this.PayTypeName = payTypeName;
            this.Comment = comment;
        }
        public override void Print(PrintElement element, Graphics graphics)
        {
            SizeF sf = graphics.MeasureString("消费金额", BodyFont);
            SizeF sf1 = graphics.MeasureString("9999.00", BodyFont);
            SizeF sf2 = graphics.MeasureString("数量", BodyFont);
            SizeF sf3 = graphics.MeasureString("9999", BodyFont);

            float maxLength = sf.Width;
            float maxLength1 = sf1.Width;
            float maxLength2 = sf2.Width;
            float maxLength3 = sf3.Width;
            element.AddMiddleText(Title + "(" + DeskNo + ")", HeadFont);
            #region 商品
            element.AddHorizontalRule();
            element.AddFourText("名称", "单价", "数量", "金额", maxLength1, maxLength2, maxLength3, BodyFont);
            if (lInventoryEx != null && lInventoryEx.Count > 0)
            {
                for (int i = 0; i < lInventoryEx.Count; i++)
                {
                    string count = (i + 1).ToString() + ".";
                    DXInfo.Models.InventoryEx ie = lInventoryEx[i];
                    element.AddFourText(count + ie.Name,
                        string.Format(MyFormat, "{0:DelZero}", ie.SalePrice),
                        string.Format(MyFormat, "{0:DelZero}", ie.Quantity),
                        string.Format(MyFormat, "{0:DelZero}", ie.Amount), maxLength1, maxLength2, maxLength3, BodyFont);

                }
            }
            #endregion

            element.AddHorizontalRule();
            element.AddPairText("总金额", string.Format(MyFormat, "{0:DelZero}", Sum),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("代金券", string.Format(MyFormat, "{0:DelZero}", Voucher),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("消费金额", string.Format(MyFormat, "{0:DelZero}", Amount),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("支付方式", PayTypeName, maxLength, BodyFont, BodyFont);
            element.AddPairText("收您", string.Format(MyFormat, "{0:DelZero}", Cash),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("找零", string.Format(MyFormat, "{0:DelZero}", Change),
                maxLength, BodyFont, BodyBigFont);
            //element.AddPairText("操作员", FullName, maxLength, BodyFont);
            element.AddPairText("操作员", UserName, maxLength, BodyFont, BodyFont);
            element.AddPairText("门店", DeptName, maxLength, BodyFont, BodyFont);
            element.AddPairText("预定电话", Comment, maxLength, BodyFont, BodyFont);
            //element.AddPairText("号牌", DeskNo, maxLength, BodyFont);
            element.AddPairText("消费时间", CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                maxLength, BodyFont, BodySmallFont);

            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddHorizontalRule();
        }
    }
    /// <summary>
    /// 会员卡结账打印
    /// </summary>
    public class WRMemberConsumePrintObject2 : MyPrintable
    {
        public WRMemberConsumePrintObject2(
            string title,
            ObservableCollection<DXInfo.Models.InventoryEx> lInventoryEx,
            List<DXInfo.Models.CardDonateInventoryEx> lCardDonateInventoryEx,
            string cardNo, string memberName,
            decimal lastBalance,
            decimal balance,
            decimal sum,
            decimal discount,
            decimal amount,
            decimal voucher,
            string deskNo, string fullName, string userName,
            string deptName, DateTime createDate,
            string comment
            )
            : base(0, 0, 30)
        {
            this.Title = title;
            this.DeskNo = deskNo;
            this.lInventoryEx = lInventoryEx;
            this.Sum = sum;
            this.DeptName = deptName;
            this.CreateDate = createDate;
            this.lCardDonateInventoryEx = lCardDonateInventoryEx;
            this.CardNo = cardNo;
            this.MemberName = memberName;
            this.Voucher = voucher;
            this.Discount = discount;
            this.Amount = amount;
            this.Balance = balance;
            this.FullName = fullName;
            this.UserName = userName;
            this.LastBalance = lastBalance;
            this.Comment = comment;
        }
        public override void Print(PrintElement element, Graphics graphics)
        {
            SizeF sf = graphics.MeasureString("本次共计优惠", BodyFont);
            SizeF sf1 = graphics.MeasureString("9999.00", BodyFont);
            SizeF sf2 = graphics.MeasureString("数量", BodyFont);
            SizeF sf3 = graphics.MeasureString("9999", BodyFont);

            float maxLength = sf.Width;
            float maxLength1 = sf1.Width;
            float maxLength2 = sf2.Width;
            float maxLength3 = sf3.Width;

            element.AddMiddleText(Title+"("+DeskNo+")", HeadFont);
            element.AddPairText("卡号", this.CardNo, maxLength, BodyFont, BodyFont);
            element.AddPairText("会员名", string.Format(MyFormat, "{0:NameStar}", this.MemberName),
                maxLength, BodyFont, BodyFont);
            element.AddPairText("上次余额", string.Format(MyFormat, "{0:DelZero}", this.LastBalance),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("本次共计优惠", string.Format(MyFormat, "{0:DelZero}", (Sum - Amount)),
                maxLength, BodyFont, BodyBigFont);
            #region 赠送商品
            if (lCardDonateInventoryEx != null && lCardDonateInventoryEx.Count > 0)
            {
                element.AddHorizontalRule();
                string strCardDonateInv = "赠送商品:";
                foreach (DXInfo.Models.CardDonateInventoryEx q in lCardDonateInventoryEx)
                {
                    strCardDonateInv += q.Name + ",";
                }
                strCardDonateInv = strCardDonateInv.Substring(0, strCardDonateInv.Length - 1);
                element.AddBreakText(strCardDonateInv, BodyFont);
            }
            #endregion

            #region 商品
            element.AddHorizontalRule();
            element.AddFourText("名称", "单价", "数量", "金额", maxLength1, maxLength2, maxLength3, BodyFont);
            if (lInventoryEx != null && lInventoryEx.Count > 0)
            {
                for (int i = 0; i < lInventoryEx.Count; i++)
                {
                    string count = (i + 1).ToString() + ".";
                    DXInfo.Models.InventoryEx ie = lInventoryEx[i];
                    element.AddFourText(count + ie.Name,
                        string.Format(MyFormat, "{0:DelZero}", ie.SalePrice),
                        string.Format(MyFormat, "{0:DelZero}", ie.Quantity),
                        string.Format(MyFormat, "{0:DelZero}", ie.Amount),
                        maxLength1, maxLength2, maxLength3, BodyFont);

                }
            }
            #endregion

            element.AddHorizontalRule();
            element.AddPairText("总计", string.Format(MyFormat, "{0:DelZero}", Sum),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("代金券", string.Format(MyFormat, "{0:DelZero}", Voucher),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("折扣", (Discount / 100).ToString("p0"),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("消费金额", string.Format(MyFormat, "{0:DelZero}", Amount),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("当前余额", string.Format(MyFormat, "{0:DelZero}", Balance),
                maxLength, BodyFont, BodyBigFont);
            //element.AddPairText("操作员", FullName, maxLength, BodyFont);
            element.AddPairText("操作员", UserName, maxLength, BodyFont, BodyFont);
            element.AddPairText("门店", DeptName, maxLength, BodyFont, BodyFont);
            element.AddPairText("预定电话", Comment, maxLength, BodyFont, BodyFont);
            //element.AddPairText("号牌", DeskNo, maxLength, BodyFont);
            element.AddPairText("消费时间", CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                maxLength, BodyFont, BodySmallFont);

            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddHorizontalRule();
        }
    }
    /// <summary>
    /// 会员卡现金结账
    /// </summary>
    public class WRMemberConsumePrintObject3 : MyPrintable
    {
        //List<DXInfo.Models.CardDonateInventoryEx> lCardDonateInventoryEx { get; set; }
        public WRMemberConsumePrintObject3(
            string title,
            ObservableCollection<DXInfo.Models.InventoryEx> lInventoryEx,
            List<DXInfo.Models.CardDonateInventoryEx> lCardDonateInventoryEx,
            string cardNo, string memberName,
            decimal cash,
            decimal change,
            decimal sum,
            decimal discount,
            decimal amount,
            decimal voucher,
            string payTypeName,
            string deskNo, string fullName, string userName,
            string deptName, DateTime createDate,
            string comment
            )
            : base(0, 0, 30)
        {
            this.Title = title;
            this.DeskNo = deskNo;
            this.lInventoryEx = lInventoryEx;
            this.Sum = sum;
            this.DeptName = deptName;
            this.CreateDate = createDate;
            this.lCardDonateInventoryEx = lCardDonateInventoryEx;
            this.CardNo = cardNo;
            this.MemberName = memberName;
            this.Voucher = voucher;
            this.Discount = discount;
            this.Amount = amount;
            this.Cash = cash;
            this.FullName = fullName;
            this.UserName = userName;
            this.Change = change;
            this.PayTypeName = payTypeName;
            this.Comment = comment;
        }
        public override void Print(PrintElement element, Graphics graphics)
        {
            SizeF sf = graphics.MeasureString("本次共计优惠", BodyFont);
            SizeF sf1 = graphics.MeasureString("9999.00", BodyFont);
            SizeF sf2 = graphics.MeasureString("数量", BodyFont);
            SizeF sf3 = graphics.MeasureString("9999", BodyFont);
            float maxLength = sf.Width;
            float maxLength1 = sf1.Width;
            float maxLength2 = sf2.Width;
            float maxLength3 = sf3.Width;

            element.AddMiddleText(Title + "(" + DeskNo + ")", HeadFont);
            element.AddPairText("卡号", this.CardNo, maxLength, BodyFont, BodyFont);
            element.AddPairText("会员名", string.Format(MyFormat, "{0:NameStar}", this.MemberName),
                maxLength, BodyFont, BodyFont);
            element.AddPairText("本次共计优惠", string.Format(MyFormat, "{0:DelZero}", (Sum - Amount)),
                maxLength, BodyFont, BodyBigFont);
            #region 赠送商品
            if (lCardDonateInventoryEx != null && lCardDonateInventoryEx.Count > 0)
            {
                element.AddHorizontalRule();
                string strCardDonateInv = "赠送商品:";
                foreach (DXInfo.Models.CardDonateInventoryEx q in lCardDonateInventoryEx)
                {
                    strCardDonateInv += q.Name + ",";
                }
                strCardDonateInv = strCardDonateInv.Substring(0, strCardDonateInv.Length - 1);
                element.AddBreakText(strCardDonateInv, BodyFont);
            }
            #endregion

            #region 商品
            element.AddHorizontalRule();
            element.AddFourText("名称", "单价", "数量", "金额", maxLength1, maxLength2, maxLength3, BodyFont);
            if (lInventoryEx != null && lInventoryEx.Count > 0)
            {
                for (int i = 0; i < lInventoryEx.Count; i++)
                {
                    string count = (i + 1).ToString() + ".";
                    DXInfo.Models.InventoryEx ie = lInventoryEx[i];
                    element.AddFourText(count + ie.Name,
                        string.Format(MyFormat, "{0:DelZero}", ie.SalePrice),
                        string.Format(MyFormat, "{0:DelZero}", ie.Quantity),
                        string.Format(MyFormat, "{0:DelZero}", ie.Amount), maxLength1, maxLength2, maxLength3, BodyFont);

                }
            }
            #endregion

            element.AddHorizontalRule();
            element.AddPairText("总计", string.Format(MyFormat, "{0:DelZero}", Sum),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("代金券", string.Format(MyFormat, "{0:DelZero}", Voucher),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("折扣", (Discount / 100).ToString("p0"),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("消费金额", string.Format(MyFormat, "{0:DelZero}", Amount),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("支付方式", PayTypeName, maxLength, BodyFont, BodyFont);
            element.AddPairText("收您", string.Format(MyFormat, "{0:DelZero}", Cash),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("找零", string.Format(MyFormat, "{0:DelZero}", Change),
                maxLength, BodyFont, BodyBigFont);
            //element.AddPairText("操作员", FullName, maxLength, BodyFont);
            element.AddPairText("操作员", UserName, maxLength, BodyFont, BodyFont);
            element.AddPairText("门店", DeptName, maxLength, BodyFont, BodyFont);
            element.AddPairText("预定电话", Comment, maxLength, BodyFont, BodyFont);
            //element.AddPairText("号牌", DeskNo, maxLength, BodyFont);
            element.AddPairText("消费时间", CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                maxLength, BodyFont, BodySmallFont);

            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddHorizontalRule();
        }
    }
    #endregion

    #region 下单
    public class OrderPrintObject : MyPrintable
    {
        public OrderPrintObject(
            ObservableCollection<DXInfo.Models.InventoryEx> lInventoryEx,
            string deskNo, string deptName, DateTime createDate,
            decimal sum,
            decimal quantity)
            : base(0, 0, 30)
        {
            this.DeskNo = deskNo;
            this.lInventoryEx = lInventoryEx;
            this.Sum = sum;
            this.Quantity = quantity;
            this.DeptName = deptName;
            this.CreateDate = createDate;
        }
        public OrderPrintObject(
            ObservableCollection<DXInfo.Models.InventoryEx> lInventoryEx,
            string deskNo, string deptName, DateTime createDate,
            decimal sum,
            decimal quantity, string printerName)
            : base(printerName, 0, 0, 30)
        {
            this.DeskNo = deskNo;
            this.lInventoryEx = lInventoryEx;
            this.Sum = sum;
            this.Quantity = quantity;
            this.DeptName = deptName;
            this.CreateDate = createDate;
            HeadFont = new Font("黑体", 22, FontStyle.Bold);
            BodyFont = new Font("黑体", 10, FontStyle.Bold);
        }
        public override void Print(PrintElement element, Graphics graphics)
        {
            SizeF sf = graphics.MeasureString("总计", BodyFont);
            SizeF sf1 = graphics.MeasureString("数量", BodyFont);

            float maxLength = sf.Width;
            float maxLength1 = sf1.Width;

            element.AddMiddleText(DeskNo, HeadFont);
            element.AddHorizontalRule();
            element.AddAlignPairText("名称", "数量", maxLength1, BodyFont);
            if (lInventoryEx != null && lInventoryEx.Count > 0)
            {
                for (int i = 0; i < lInventoryEx.Count; i++)
                {
                    string count = (i + 1).ToString() + ".";
                    DXInfo.Models.InventoryEx ie = lInventoryEx[i];
                    element.AddAlignPairText(count + ie.Name,
                        string.Format(MyFormat, "{0:DelZero}", ie.Quantity), maxLength1, BodyFont);
                    if (!string.IsNullOrEmpty(ie.Comment))
                    {
                        element.AddBreakText(ie.Comment, BodyFont);
                    }
                }
            }
            element.AddHorizontalRule();
            element.AddPairText("总计", string.Format(MyFormat, "{0:DelZero}", Sum),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("总数", string.Format(MyFormat, "{0:DelZero}", Quantity),
                maxLength, BodyFont, BodyBigFont);
            element.AddPairText("门店", DeptName, maxLength, BodyFont, BodyFont);
            element.AddPairText("日期", CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                maxLength, BodyFont, BodySmallFont);

            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddText("", BodyFont);
            element.AddHorizontalRule();
        }
    }
    #endregion

    #region 不干胶打印
    public class StickerBatchPrintObject : MyBatchStickerPrintable
    {
        public StickerBatchPrintObject(string printerName,
            DXInfo.Models.InventoryEx inventoryEx,
            string deskNo, string deptName, DateTime createDate, int idx, int count)
        {
            this.DeskNo = deskNo;
            this.InventoryEx = inventoryEx;
            this.DeptName = deptName;
            this.CreateDate = createDate;
            Idx = idx;
            Count = count;
        }
        public override void Print(PrintElement element, Graphics graphics)
        {
            string strInvName = InventoryEx.Name;
            DXInfo.Models.MyFormat MyFormat = new DXInfo.Models.MyFormat();
            string strPrice = string.Format(MyFormat, "{0:DelZero}", InventoryEx.CurrentSalePrice);
            string strCuptType = InventoryEx.CupType.Name;

            string strIdx = Idx.ToString() + "/" + Count.ToString();
            strIdx = strIdx.PadLeft(7);
            //element.AddImage("StickerLogo.bmp", strIdx, new Font("宋体", 10), DeskNo, HeadFont);
            element.AddWhiteLine();
            element.AddWhiteLine();
            element.AddWhiteLine();
            string taste1 = "";
            if (InventoryEx.lTasteEx != null && InventoryEx.lTasteEx.Count > 0)
            {

                InventoryEx.lTasteEx.Where(w => w.IsSelected).ToList().ForEach(delegate(DXInfo.Models.TasteEx te)
                {
                    if (!te.Name.Contains("无"))
                    {
                        taste1 += te.Name + " ";
                    }
                });
            }
            string invName = strInvName.PadRight(10);
            //element.AddText(invName + taste1, BodyFont);//DeskNo-strIdx
            element.AddText(invName + DeskNo + "-" + strIdx, BodyFont);
            element.AddText(strCuptType
                + "     ￥:"
                + strPrice
                + "元", BodyFont);

            string taste = "要求:";
            if (InventoryEx.lTasteEx != null && InventoryEx.lTasteEx.Count > 0)
            {

                InventoryEx.lTasteEx.Where(w => w.IsSelected).ToList().ForEach(delegate(DXInfo.Models.TasteEx te)
                {
                    //if (te.Name.Contains("无"))
                    //{
                        taste += te.Name + " ";
                    //}
                });
            }
            if (taste.Length > 3)
            {
                taste = taste.Substring(0, taste.Length - 1);
            }
            element.AddText(taste, BodyFont);
            element.AddText("出品时间:" + CreateDate.ToString("M月d日 HH:mm"), BodyFont);
            element.AddText("门店:" + DeptName, BodyFont);
            string stickerPhone = "服务热线:400-649-3337";
            if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("StickerPhone"))
            {
                stickerPhone = System.Configuration.ConfigurationManager.AppSettings["StickerPhone"];
            }
            element.AddText(stickerPhone, BodyFont);
        }
    }
    public class StickerPrintObject : MyPrintable
    {
        private string strInvName;
        private string strPrice;
        private string strCuptType;
        public StickerPrintObject(
            string printerName, DXInfo.Models.InventoryEx inventoryEx,
            string deskNo, string deptName, DateTime createDate, int idx, int count)
            : base(printerName, 10, 10, 0)
        {
            this.DeskNo = deskNo;
            this.InventoryEx = inventoryEx;
            this.DeptName = deptName;
            this.CreateDate = createDate;

            HeadFont = new Font("宋体", 7, FontStyle.Bold);
            BodyFont = new Font("宋体", 7, FontStyle.Bold);
            Idx = idx;
            Count = count;
            strInvName = InventoryEx.Name;
            strPrice = string.Format(MyFormat, "{0:DelZero}", InventoryEx.CurrentSalePrice);
            strCuptType = InventoryEx.CupType.Name;
        }
        public StickerPrintObject(
            string printerName, DXInfo.Models.InventoryEx inventoryEx,
            string deskNo, string deptName, DateTime createDate, int idx, int count,
            string invName, string price, string cupType)
            : base(printerName, 10, 10, 0)
        {
            this.DeskNo = deskNo;
            this.InventoryEx = inventoryEx;
            this.DeptName = deptName;
            this.CreateDate = createDate;

            HeadFont = new Font("宋体", 7, FontStyle.Bold);
            BodyFont = new Font("宋体", 7, FontStyle.Bold);
            Idx = idx;
            Count = count;
            strInvName = invName;
            strPrice = price;
            strCuptType = cupType;
        }
        public override void Print(PrintElement element, Graphics graphics)
        {
            string strIdx = Idx.ToString() + "/" + Count.ToString();
            strIdx = strIdx.PadLeft(7);
            element.AddImage("StickerLogo.bmp", strIdx, new Font("宋体", 10), DeskNo, new Font("宋体", 20, FontStyle.Bold));
            string taste1 = "";
            if (InventoryEx.lTasteEx != null && InventoryEx.lTasteEx.Count > 0)
            {

                InventoryEx.lTasteEx.Where(w => w.IsSelected).ToList().ForEach(delegate(DXInfo.Models.TasteEx te)
                {
                    //if (!te.Name.Contains("无"))
                    //{
                        taste1 += te.Name + " ";
                    //}
                });
            }
            string invName = strInvName.PadRight(10);
            element.AddText(invName + taste1, HeadFont);
            element.AddText(strCuptType
                + "     ￥:"
                + strPrice
                + "元", BodyFont);

            string taste = "要求:";
            if (InventoryEx.lTasteEx != null && InventoryEx.lTasteEx.Count > 0)
            {

                InventoryEx.lTasteEx.Where(w => w.IsSelected).ToList().ForEach(delegate(DXInfo.Models.TasteEx te)
                {
                    if (te.Name.Contains("无"))
                    {
                        taste += te.Name + " ";
                    }
                });
            }
            if (taste.Length > 3)
            {
                taste = taste.Substring(0, taste.Length - 1);
            }
            element.AddText(taste, BodyFont);

            element.AddText("出品时间:" + CreateDate.ToString("M月d日 HH:mm"), BodyFont);
            element.AddText("门店:" + DeptName, BodyFont);
            string stickerPhone = "服务热线:400-649-3337";
            if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("StickerPhone"))
            {
                stickerPhone = System.Configuration.ConfigurationManager.AppSettings["StickerPhone"];
            }
            element.AddText(stickerPhone, BodyFont);
        }
        //public  void Print1(PrintElement element, Graphics graphics)
        //{
        //    element.AddText(InventoryEx.Name, HeadFont);
        //    element.AddText("￥:" + InventoryEx.CurrentSalePrice.ToString() + "元", BodyFont);
        //    element.AddText(InventoryEx.CupType.Name,BodyFont);
        //    string taste = "  ";
        //    if (InventoryEx.lTasteEx != null && InventoryEx.lTasteEx.Count > 0)
        //    {

        //        InventoryEx.lTasteEx.Where(w => w.IsSelected).ToList().ForEach(delegate(DXInfo.Models.TasteEx te) { taste += te.Name + " "; });
        //    }
        //    if (taste.Length > 1)
        //    {
        //        taste = taste.Substring(0, taste.Length - 1);
        //        element.AddBreakText(taste, BodyFont);
        //    }
        //    else
        //    {
        //        element.AddText(taste, BodyFont);
        //    }

        //    element.AddTwoFontText(CreateDate.ToString("yy-MM-dd HH:mm"), DeskNo,BodyFont, new Font("宋体", 20, FontStyle.Bold));
        //    element.AddText(DeptName, BodyFont);
        //}
    }
    #endregion

    #region 后厨打印
    public class MenuPrintObject : MyPrintable
    {
        public MenuPrintObject(
            string printerName, string invName, string comment,
            string deskNo, string fullName, DateTime createDate)
            : base(printerName, 0, 0, 30)
        {
            this.DeskNo = deskNo;
            this.FullName = fullName;
            this.CreateDate = createDate;
            this.InvName = invName;
            this.Comment = comment;
        }

        public override void Print(PrintElement element, Graphics graphics)
        {
            SizeF sf = graphics.MeasureString("服务员", BodyFont);
            SizeF sf1 = graphics.MeasureString("数量", BodyFont);

            float maxLength = sf.Width;
            float maxLength1 = sf1.Width;

            element.AddMiddleText(DeskNo, HeadFont);
            element.AddHorizontalRule();
            element.AddAlignPairText("名称", "数量", maxLength1, BodyFont);

            element.AddAlignPairText(InvName, "1", maxLength1, BodyFont);
            if (!string.IsNullOrEmpty(Comment))
            {
                element.AddBreakText(Comment, BodyFont);
            }
            element.AddHorizontalRule();
            element.AddPairText("服务员", FullName, maxLength, BodyFont, BodyFont);
            element.AddPairText("时间", CreateDate.ToString("yyyy-MM-dd HH:mm:ss"),
                maxLength, BodyFont, BodySmallFont);
        }
    }
    #endregion

    #region 三联单打印
    /// <summary>
    /// 非会员结账
    /// </summary>
    public class NoMemberThreePrintObject : MyPrintable
    {
        [DataColumn]
        public string ButtomTitle { get; set; }
        [DataColumn]
        public string Sn { get; set; }
    }
    /// <summary>
    /// 会员卡现金结账
    /// </summary>
    public class MemberNoMoneyThreePrintObject : MyPrintable
    {
        [DataColumn]
        public string ButtomTitle { get; set; }
        [DataColumn]
        public string Sn { get; set; }
    }
    /// <summary>
    /// 会员卡结账
    /// </summary>
    public class MemberThreePrintObject : MyPrintable
    {
        [DataColumn]
        public string ButtomTitle { get; set; }
        [DataColumn]
        public string Sn { get; set; }
    }
    /// <summary>
    /// 充值
    /// </summary>
    public class InMoneyThreePrintObject : MyPrintable
    {
        [DataColumn]
        public string ButtomTitle { get; set; }
    }
    #endregion
}
