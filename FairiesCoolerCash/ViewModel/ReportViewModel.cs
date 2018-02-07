using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
//using System.Data.Objects.SqlClient;
using FairiesCoolerCash.Business;
using System.Windows.Controls;
using System.Net;
using System.Data.Entity.SqlServer;

namespace FairiesCoolerCash.ViewModel
{    
    /// <summary>
    /// 消费明细查询
    /// </summary>
    public class Report2ViewModel:ReportViewModelBase
    {
        public Report2ViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {            

        }

        protected override void query()
        {
            //查询
            var q = from cl in Uow.ConsumeList.GetAll().Where(w => w.DeptId == this.Dept.DeptId)
                    join c in Uow.Consume.GetAll() on cl.Consume equals c.Id into clc
                    from clcs in clc.DefaultIfEmpty()

                    join p in Uow.PayTypes.GetAll() on clcs.PayType equals p.Id into cp
                    from cps in cp.DefaultIfEmpty()

                    join card in Uow.Cards.GetAll() on clcs.Card.Value equals card.Id into ccard
                    from ccards in ccard.DefaultIfEmpty()

                    join member in Uow.Members.GetAll() on ccards.Member equals member.Id into cm
                    from cms in cm.DefaultIfEmpty()

                    join i in Uow.Inventory.GetAll() on cl.Inventory equals i.Id into ci
                    from cis in ci.DefaultIfEmpty()

                    join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into cu
                    from cus in cu.DefaultIfEmpty()

                    join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cd
                    from cds in cd.DefaultIfEmpty()

                    join n in Uow.NameCode.GetAll().Where(w => w.Type == "CupType") on SqlFunctions.StringConvert((double?)cl.Cup).Trim() equals n.Code into dd5
                    from dd5s in dd5.DefaultIfEmpty()

                    join n in Uow.NameCode.GetAll().Where(w => w.Type == "ConsumeType") on SqlFunctions.StringConvert((double?)clcs.ConsumeType).Trim() equals n.Code into dd6
                    from dd6s in dd6.DefaultIfEmpty()

                    join d7 in Uow.UnitOfMeasures.GetAll() on cis.UnitOfMeasure equals d7.Id into dd7
                    from dd7s in dd7.DefaultIfEmpty()

                    select new ReportResult
                    {
                        Id=cl.Id,
                        CardNo=ccards.CardNo,
                        MemberName=cms.MemberName,
                        InventoryName = cis.Name,
                        UserId=cl.UserId,
                        FullName=cus.FullName,
                        DeptName=cds.DeptName,
                        Discount=cl.Discount,                        
                        Amount = cl.Amount,// * clcs.Discount / 100,
                        Sum = cl.Sum,
                        CreateDate=cl.CreateDate,
                        Price=cl.Price,
                        Quantity=cl.Quantity,
                        Cup=cl.Cup,
                        CupName = dd5s.Name,
                        ConsumeType=clcs.ConsumeType,
                        ConsumeTypeName = dd6s.Name,
                        PayType=clcs.PayType,
                        PayTypeName = clcs.ConsumeType == 0 ? "会员卡" : clcs.ConsumeType == 2 ? "积分兑换" : cps.Name,
                        UnitOfMeasureName = dd7s.Name,
                        OperatorsOnDuty=clcs.OperatorsOnDuty,
                    };
            if (!string.IsNullOrWhiteSpace(this.MemberName))
                q = q.Where(w => w.MemberName.Contains(MemberName));
            if (!string.IsNullOrWhiteSpace(CardNo))
                q = q.Where(w => w.CardNo.Contains(CardNo));
            if (this.SelectedOper != null)
            {
                q = q.Where(w => w.UserId == this.SelectedOper.UserId);
            }
            if (this.SelectedConsumeType != null)
            {
                q = q.Where(w => w.ConsumeType == this.SelectedConsumeType.Id);
            }
            if (this.SelectedPayType != null)
            {
                q = q.Where(w => w.PayType == this.SelectedPayType.Id);
            }
            if (this.BeginDate>DateTime.MinValue)
            {
                q = q.Where(w => w.CreateDate >= this.BeginDate);
            }
            if (this.EndDate>DateTime.MinValue)
            {
                DateTime dtEndDate = this.EndDate.AddDays(1);
                q = q.Where(w => w.CreateDate <= dtEndDate);
            }
            //代金券
            var q2 = from c in Uow.Consume.GetAll().Where(w => w.DeptId == this.Dept.DeptId && Voucher>0)

                    join p in Uow.PayTypes.GetAll() on c.PayType equals p.Id into cp
                    from cps in cp.DefaultIfEmpty()

                    join card in Uow.Cards.GetAll() on c.Card.Value equals card.Id into ccard
                    from ccards in ccard.DefaultIfEmpty()

                    join member in Uow.Members.GetAll() on ccards.Member equals member.Id into cm
                    from cms in cm.DefaultIfEmpty()


                    join u in Uow.aspnet_CustomProfile.GetAll() on c.UserId equals u.UserId into cu
                    from cus in cu.DefaultIfEmpty()

                    join d in Uow.Depts.GetAll() on c.DeptId equals d.DeptId into cd
                    from cds in cd.DefaultIfEmpty()


                    join n in Uow.NameCode.GetAll().Where(w => w.Type == "ConsumeType") on SqlFunctions.StringConvert((double?)c.ConsumeType).Trim() equals n.Code into dd6
                    from dd6s in dd6.DefaultIfEmpty()
                     select new ReportResult
                    {
                        Id=c.Id,
                        CardNo=ccards.CardNo,
                        MemberName=cms.MemberName,
                        InventoryName = "代金券",
                        UserId=c.UserId,
                        FullName=cus.FullName,
                        DeptName=cds.DeptName,
                        Discount=100,
                        Amount = c.PayVoucher,
                        Sum = c.Voucher,
                        CreateDate=c.CreateDate,
                        Price=-c.PayVoucher,
                        Quantity=1,
                        Cup=-1,
                        CupName = "标准杯",
                        ConsumeType=c.ConsumeType,
                        ConsumeTypeName = dd6s.Name,
                        PayType=c.PayType,
                        PayTypeName = c.ConsumeType == 0 ? "会员卡" : c.ConsumeType == 2 ? "积分兑换" : cps.Name,
                        UnitOfMeasureName = "",
                        OperatorsOnDuty = c.OperatorsOnDuty,
                    };
            if (!string.IsNullOrWhiteSpace(this.MemberName))
                q2 = q2.Where(w => w.MemberName.Contains(MemberName));
            if (!string.IsNullOrWhiteSpace(CardNo))
                q2 = q2.Where(w => w.CardNo.Contains(CardNo));
            if (this.SelectedOper != null)
            {
                q2 = q2.Where(w => w.UserId == this.SelectedOper.UserId);
            }
            if (this.SelectedConsumeType != null)
            {
                q2 = q2.Where(w => w.ConsumeType == this.SelectedConsumeType.Id);
            }
            if (this.SelectedPayType != null)
            {
                q2 = q2.Where(w => w.PayType == this.SelectedPayType.Id);
            }
            if (this.BeginDate > DateTime.MinValue)
            {
                q2 = q2.Where(w => w.CreateDate >= this.BeginDate);
            }
            if (this.EndDate > DateTime.MinValue)
            {
                DateTime dtEndDate = this.EndDate.AddDays(1);
                q2 = q2.Where(w => w.CreateDate <= dtEndDate);
            }
            
            var q3 = q.Concat(q2).OrderBy(o=>o.CreateDate);
            this.MyQuery = q3;

            if (q3.Count() > 0)
            {
                SumQuantity = q3.Sum(s => s.Quantity);
                SumPayable = q3.Sum(s => s.Sum);
                SumAmount = q3.Sum(s => s.Amount);
            }
        }
    }
    /// <summary>
    /// 充值明细查询
    /// </summary>
    public class Report3ViewModel : ReportViewModelBase
    {
        public Report3ViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
        }
        protected override void query()
        {
            //查询
            var q = from r in Uow.Recharges.GetAll().Where(w => w.DeptId == this.Dept.DeptId)
                    join c in Uow.Cards.GetAll() on r.Card equals c.Id into rc
                    from rcs in rc.DefaultIfEmpty()

                    join p in Uow.PayTypes.GetAll() on r.PayType equals p.Id into rp
                    from rps in rp.DefaultIfEmpty()

                    join m in Uow.Members.GetAll() on rcs.Member equals m.Id into rcsm
                    from rcsms in rcsm.DefaultIfEmpty()

                    join u in Uow.aspnet_CustomProfile.GetAll() on r.UserId equals u.UserId into ru
                    from rus in ru.DefaultIfEmpty()

                    join d in Uow.Depts.GetAll() on r.DeptId equals d.DeptId into rd
                    from rds in rd.DefaultIfEmpty()

                    join n in Uow.NameCode.GetAll().Where(w => w.Type == "RechargeType") on SqlFunctions.StringConvert((double?)r.RechargeType).Trim() equals n.Code into dd5
                    from dd5s in dd5.DefaultIfEmpty()
                    select new
                    {
                        r.Amount,
                        r.Balance,
                        r.CreateDate,
                        r.Donate,
                        r.Id,
                        r.LastBalance,
                        RechargeType = r.RechargeType,
                        RechargeTypeName = dd5s.Name,
                        r.UserId,
                        rcs.CardNo,
                        rcsms.MemberName,
                        rus.FullName,
                        rds.DeptName,
                        PayType = rps == null ? Guid.Empty : rps.Id,
                        PayTypeName = rps == null ? "" : rps.Name,
                        r.OperatorsOnDuty,
                    };
            if (!string.IsNullOrWhiteSpace(MemberName))
                q = q.Where(w => w.MemberName.Contains(MemberName));
            if (!string.IsNullOrWhiteSpace(CardNo))
                q = q.Where(w => w.CardNo.Contains(CardNo));
            if (this.SelectedOper != null)
            {
                q = q.Where(w => w.UserId == this.SelectedOper.UserId);
            }
            if (this.SelectedRechargeType != null)
            {
                q = q.Where(w => w.RechargeType == this.SelectedRechargeType.Id);
            }
            if (this.SelectedPayType != null)
            {
                q = q.Where(w => w.PayType == this.SelectedPayType.Id);
            }
            if (BeginDate > DateTime.MinValue)
            {
                q = q.Where(w => w.CreateDate >= BeginDate);
            }
            if (EndDate > DateTime.MinValue)
            {
                DateTime dtEndDate = EndDate.AddDays(1);
                q = q.Where(w => w.CreateDate <= dtEndDate);
            }

            this.MyQuery = q.OrderBy(o=>o.CreateDate);
            if (q.Count() > 0)
            {
                SumAmount = q.Sum(s => s.Amount);
                SumDonate = q.Sum(s => s.Donate);
                Sum = (Amount + Donate);
            }
        }
    }
    /// <summary>
    /// 消费分类统计
    /// </summary>
    public class Report4ViewModel : ReportViewModelBase
    {
        public Report4ViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
        }
        protected override void query()
        {
            //查询
            var q = from cl in Uow.ConsumeList.GetAll().Where(w => w.DeptId == this.Dept.DeptId)
                    join c in Uow.Consume.GetAll() on cl.Consume equals c.Id into clc
                    from clcs in clc.DefaultIfEmpty()

                    join i in Uow.Inventory.GetAll() on cl.Inventory equals i.Id into ci
                    from cis in ci.DefaultIfEmpty()

                    join ic in Uow.InventoryCategory.GetAll() on cis.Category equals ic.Id into iic
                    from iics in iic.DefaultIfEmpty()

                    join n in Uow.NameCode.GetAll().Where(w => w.Type == "CupType") on SqlFunctions.StringConvert((double?)cl.Cup).Trim() equals n.Code into dd5
                    from dd5s in dd5.DefaultIfEmpty()

                    join n in Uow.NameCode.GetAll().Where(w => w.Type == "ConsumeType") on SqlFunctions.StringConvert((double?)clcs.ConsumeType).Trim() equals n.Code into dd6
                    from dd6s in dd6.DefaultIfEmpty()

                    join d7 in Uow.UnitOfMeasures.GetAll() on cis.UnitOfMeasure equals d7.Id into dd7
                    from dd7s in dd7.DefaultIfEmpty()
                    select new
                    {
                        cl.Id,
                        InventoryName = cis.Name,
                        CategoryName = iics.Name,
                        Category = iics.Id,
                        cl.UserId,
                        cl.Amount,
                        cl.CreateDate,
                        cl.Quantity,
                        Cup = cl.Cup,
                        CupName=dd5s.Name,
                        ConsumeType = clcs.ConsumeType,
                        ConsumeTypeName = dd6s.Name,
                        UnitOfMeasureName = dd7s.Name,
                    };
            if (this.SelectedOper != null)
            {
                q = q.Where(w => w.UserId == this.SelectedOper.UserId);
            }
            if (this.SelectedConsumeType != null)
            {
                q = q.Where(w => w.ConsumeType == this.SelectedConsumeType.Id);
            }
            if (this.SelectedInventoryCategory != null)
            {
                q = q.Where(w => w.Category == this.SelectedInventoryCategory.Id);
            }
            if (BeginDate>DateTime.MinValue)
            {
                q = q.Where(w => w.CreateDate >= BeginDate);
            }
            if (EndDate>DateTime.MinValue)
            {
                DateTime dtEndDate = EndDate.AddDays(1);
                q = q.Where(w => w.CreateDate <= dtEndDate);
            }

            if (this.SelectedInventoryCategory != null)
            {
                var q2 = from q4 in q
                         group q4 by new { q4.CategoryName, q4.InventoryName, q4.ConsumeTypeName, q4.CupName,q4.UnitOfMeasureName }
                             into g
                             select new
                             {
                                 g.Key.CategoryName,
                                 g.Key.InventoryName,
                                 g.Key.ConsumeTypeName,
                                 g.Key.CupName,
                                 g.Key.UnitOfMeasureName,
                                 Amount = g.Sum(s => s.Amount),
                                 Quantity = g.Sum(s => s.Quantity)
                             };
                foreach (DataGridColumn dgc in this.MyDataGrid.Columns)
                {
                    if (dgc.Header.ToString() == "商品")
                    {
                        dgc.Visibility = System.Windows.Visibility.Visible;
                    }
                }
                this.MyQuery = q2.OrderBy(o=>o.CategoryName);
                if (q2.Count() > 0)
                {

                    SumAmount = q2.Sum(s => s.Amount);
                    SumQuantity = q2.Sum(s => s.Quantity);
                }
            }
            else
            {

                var q2 = from q4 in q
                         group q4 by new { q4.CategoryName, q4.ConsumeTypeName, q4.CupName,q4.UnitOfMeasureName }
                             into g
                             select new
                             {
                                 g.Key.CategoryName,
                                 g.Key.ConsumeTypeName,
                                 g.Key.CupName,
                                 g.Key.UnitOfMeasureName,
                                 Amount = g.Sum(s => s.Amount),
                                 Quantity = g.Sum(s => s.Quantity)
                             };
                foreach (DataGridColumn dgc in this.MyDataGrid.Columns)
                {
                    if (dgc.Header.ToString() == "商品")
                    {
                        dgc.Visibility = System.Windows.Visibility.Hidden;
                    }
                }
                this.MyQuery = q2.OrderBy(o=>o.CategoryName);
                if (q2.Count() > 0)
                {

                    SumAmount = q2.Sum(s => s.Amount);
                    SumQuantity = q2.Sum(s => s.Quantity);
                }
            }
        }
    }
    /// <summary>
    /// 销售排名统计
    /// </summary>
    public class Report5ViewModel : ReportViewModelBase
    {
        public Report5ViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
        }
        protected override void query()
        {
            //有两种，一：卡号和消费金额，二：商品销售排行
            //查询
            var q = from cl in Uow.ConsumeList.GetAll().Where(w => w.DeptId == this.Dept.DeptId)
                    join c in Uow.Consume.GetAll() on cl.Consume equals c.Id into clc
                    from clcs in clc.DefaultIfEmpty()

                    join c in Uow.Cards.GetAll() on clcs.Card equals c.Id into clcsc
                    from clcscs in clcsc.DefaultIfEmpty()

                    join m in Uow.Members.GetAll() on clcscs.Member equals m.Id into clcscsm
                    from clcscsms in clcscsm.DefaultIfEmpty()

                    join i in Uow.Inventory.GetAll() on cl.Inventory equals i.Id into ci
                    from cis in ci.DefaultIfEmpty()

                    join ic in Uow.InventoryCategory.GetAll() on cis.Category equals ic.Id into iic
                    from iics in iic.DefaultIfEmpty()

                    join d7 in Uow.UnitOfMeasures.GetAll() on cis.UnitOfMeasure equals d7.Id into dd7
                    from dd7s in dd7.DefaultIfEmpty()

                    select new
                    {
                        cl.Id,
                        InventoryName = cis.Name,
                        CategoryName = iics.Name,
                        Category = iics.Id,
                        cl.UserId,
                        clcscs.CardNo,
                        clcscsms.MemberName,
                        cl.Amount,
                        cl.CreateDate,
                        cl.Quantity,
                        iConsumeType = clcs.ConsumeType,
                        Cup = cl.Cup == -1 ? "标准杯" : cl.Cup == 0 ? "大杯" : cl.Cup == 1 ? "中杯" : "小杯",
                        ConsumeType = clcs.ConsumeType == 0 ? "会员卡消费" : clcs.ConsumeType == 1 ? "非会员消费" : clcs.ConsumeType == 2 ? "会员积分兑换" : "打折卡消费",
                        UnitOfMeasureName = dd7s.Name,
                    };
            if (this.SelectedOper != null)
            {
                //Guid userId = Guid.Parse(OperList.SelectedValue.ToString());
                q = q.Where(w => w.UserId == this.SelectedOper.UserId);
            }
            if (this.SelectedConsumeType != null)
            {
                //int iConsumeType = Convert.ToInt32(ConsumeType.SelectedValue.ToString());
                q = q.Where(w => w.iConsumeType == this.SelectedConsumeType.Id);
            }
            if (this.SelectedInventoryCategory != null)
            {
                //Guid cid = Guid.Parse(InvCategory.SelectedValue.ToString());
                q = q.Where(w => w.Category == this.SelectedInventoryCategory.Id);
            }
            if (BeginDate>DateTime.MinValue)
            {
                //DateTime dtBeginDate = Convert.ToDateTime(BeginDate.Text);
                q = q.Where(w => w.CreateDate >= BeginDate);
            }
            if (EndDate>DateTime.MinValue)
            {
                DateTime dtEndDate = EndDate.AddDays(1);
                q = q.Where(w => w.CreateDate <= dtEndDate);
            }

            if (this.IsCard)
            {
                var q2 = from q4 in q
                         group q4 by new { q4.CardNo, q4.MemberName, q4.ConsumeType, q4.Cup,q4.UnitOfMeasureName }
                             into g
                             select new
                             {
                                 g.Key.CardNo,
                                 g.Key.MemberName,
                                 g.Key.ConsumeType,
                                 g.Key.Cup,
                                 g.Key.UnitOfMeasureName,
                                 Amount = g.Sum(s => s.Amount),
                                 Quantity = g.Sum(s => s.Quantity)
                             };


                foreach (DataGridColumn dgc in this.MyDataGrid.Columns)
                {
                    if (dgc.Header.ToString() == "分类" || dgc.Header.ToString() == "商品")
                    {
                        dgc.Visibility = System.Windows.Visibility.Hidden;
                    }
                    if (dgc.Header.ToString() == "卡号" || dgc.Header.ToString() == "会员名")
                    {
                        dgc.Visibility = System.Windows.Visibility.Visible;
                    }
                }
                //MemberList.ItemsSource = q2.ToObservableCollection();
                this.MyQuery = q2.OrderBy(o=>o.CardNo);
                if (q2.Count() > 0)
                {

                    SumAmount = q2.Sum(s => s.Amount);
                    SumQuantity = q2.Sum(s => s.Quantity);
                }
            }
            else
            {
                if (this.SelectedInventoryCategory != null)
                {
                    var q2 = from q4 in q
                             group q4 by new { q4.CategoryName, q4.InventoryName, q4.ConsumeType, q4.Cup,q4.UnitOfMeasureName }
                                 into g
                                 select new
                                 {
                                     g.Key.CategoryName,
                                     g.Key.InventoryName,
                                     g.Key.ConsumeType,
                                     g.Key.Cup,
                                     g.Key.UnitOfMeasureName,
                                     Amount = g.Sum(s => s.Amount),
                                     Quantity = g.Sum(s => s.Quantity)
                                 };
                    foreach (DataGridColumn dgc in this.MyDataGrid.Columns)
                    {
                        if (dgc.Header.ToString() == "卡号" || dgc.Header.ToString() == "会员名")
                        {
                            dgc.Visibility = System.Windows.Visibility.Hidden;
                        }
                        if (dgc.Header.ToString() == "分类" || dgc.Header.ToString() == "商品")
                        {
                            dgc.Visibility = System.Windows.Visibility.Visible;
                        }
                    }
                    //MemberList.ItemsSource = q2.ToObservableCollection();
                    this.MyQuery = q2.OrderBy(o=>o.CategoryName);
                    if (q2.Count() > 0)
                    {

                        SumAmount = q2.Sum(s => s.Amount);
                        SumQuantity = q2.Sum(s => s.Quantity);
                    }
                }
                else
                {
                    var q2 = from q4 in q
                             group q4 by new { q4.CategoryName, q4.ConsumeType, q4.Cup,q4.UnitOfMeasureName }
                                 into g
                                 select new
                                 {
                                     g.Key.CategoryName,
                                     g.Key.ConsumeType,
                                     g.Key.Cup,
                                     g.Key.UnitOfMeasureName,
                                     Amount = g.Sum(s => s.Amount),
                                     Quantity = g.Sum(s => s.Quantity)
                                 };
                    foreach (DataGridColumn dgc in this.MyDataGrid.Columns)
                    {
                        if (dgc.Header.ToString() == "商品" || dgc.Header.ToString() == "卡号" || dgc.Header.ToString() == "会员名")
                        {
                            dgc.Visibility = System.Windows.Visibility.Hidden;
                        }
                        if (dgc.Header.ToString() == "分类")
                        {
                            dgc.Visibility = System.Windows.Visibility.Visible;
                        }
                    }
                    //MemberList.ItemsSource = q2.ToObservableCollection();
                    this.MyQuery = q2.OrderBy(o=>o.CategoryName);
                    if (q2.Count() > 0)
                    {

                        SumAmount = q2.Sum(s => s.Amount);
                        SumQuantity = q2.Sum(s => s.Quantity);
                    }
                }
            }
        }
    }
    /// <summary>
    /// 收银查询
    /// </summary>
    public class Report7ViewModel : ReportViewModelBase
    {
        public Report7ViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
        }
        protected override void query()
        {
            //查询

            var q = //from cl in Uow.ConsumeList.GetAll().Where(w => w.DeptId == App.MyIdentity.dept.DeptId)
                    from cl in Uow.Consume.GetAll().Where(w => w.DeptId == this.Dept.DeptId)// on cl.Consume equals c.Id into clc
                    //from clcs in clc.DefaultIfEmpty()

                    join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into cu
                    from cus in cu.DefaultIfEmpty()

                    where cl.ConsumeType == 1 || cl.ConsumeType == 3
                    select new
                    {
                        cl.Id,
                        cl.UserId,
                        cus.FullName,
                        cl.Amount,
                        cl.CreateDate,
                        ConsumeType = cl.ConsumeType == 1 ? "非会员消费" : "打折卡消费",
                        cl.OperatorsOnDuty,
                    };
            if (this.SelectedOper != null)
            {
                q = q.Where(w => w.UserId == this.SelectedOper.UserId);
            }
            if (BeginDate>DateTime.MinValue)
            {

                DateTime dtBeginDate = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day, BeginTime.Hour, BeginTime.Minute, BeginTime.Second, BeginTime.Millisecond);
                q = q.Where(w => w.CreateDate >= dtBeginDate);
            }
            if (EndDate>DateTime.MinValue)
            {
                DateTime dtEndDate = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hour, EndTime.Minute, EndTime.Second, EndTime.Millisecond);
                if (EndTime.TimeOfDay == TimeSpan.Zero)
                {
                    dtEndDate = dtEndDate.AddDays(1);
                }
                
                q = q.Where(w => w.CreateDate <= dtEndDate);
            }

            //充值
            var qy = from r in Uow.Recharges.GetAll().Where(w => w.DeptId == this.Dept.DeptId).Where(w => w.RechargeType == 0 || w.RechargeType == 2)
                     join u in Uow.aspnet_CustomProfile.GetAll() on r.UserId equals u.UserId into ru
                     from rus in ru.DefaultIfEmpty()
                     select new
                     {
                         r.Id,
                         r.UserId,
                         rus.FullName,
                         r.Amount,
                         r.CreateDate,
                         ConsumeType = r.RechargeType == 0 ? "会员卡充值" : "发卡充值",
                         r.OperatorsOnDuty,
                     };
            if (this.SelectedOper != null)
            {
                qy = qy.Where(w => w.UserId == this.SelectedOper.UserId);
            }
            if (BeginDate > DateTime.MinValue)
            {

                DateTime dtBeginDate = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day, BeginTime.Hour, BeginTime.Minute, BeginTime.Second, BeginTime.Millisecond);
                qy = qy.Where(w => w.CreateDate >= dtBeginDate);
            }
            if (EndDate > DateTime.MinValue)
            {
                DateTime dtEndDate = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hour, EndTime.Minute, EndTime.Second, EndTime.Millisecond);
                if (EndTime.TimeOfDay == TimeSpan.Zero)
                {
                    dtEndDate = dtEndDate.AddDays(1);
                }
                qy = qy.Where(w => w.CreateDate <= dtEndDate);
            }
            if (this.SelectedConsumeType == null)
            {
                var q1 = q;//.ToList();
                var qy1 = qy;//.ToList();
                var qq = q1.Union(qy1);


                var q2 = from q4 in qq
                         group q4 by new { q4.FullName,q4.OperatorsOnDuty, q4.ConsumeType }
                             into g
                             select new
                             {
                                 g.Key.FullName,
                                 g.Key.OperatorsOnDuty,
                                 g.Key.ConsumeType,
                                 Amount = g.Sum(s => s.Amount)
                             };
                //MemberList.ItemsSource = q2.ToObservableCollection();
                this.MyQuery = q2.OrderBy(o=>o.FullName);//.AsQueryable();
                if (q2.Count() > 0)
                    BalanceSum = q2.Sum(s => s.Amount);
            }
            else
            {
                switch (SelectedConsumeType.Id)
                {
                    case 0:
                        var q2 = from q4 in q
                                 where q4.ConsumeType == "非会员消费"
                                 group q4 by new { q4.FullName,q4.OperatorsOnDuty, q4.ConsumeType }
                                     into g
                                     select new
                                     {
                                         g.Key.FullName,
                                         g.Key.OperatorsOnDuty,
                                         g.Key.ConsumeType,
                                         Amount = g.Sum(s => s.Amount)
                                     };
                        this.MyQuery = q2.OrderBy(o=>o.FullName);
                        if (q2.Count() > 0)
                            BalanceSum = q2.Sum(s => s.Amount);
                        break;
                    case 1:
                        var q21 = from q4 in qy
                                  group q4 by new { q4.FullName,q4.OperatorsOnDuty, q4.ConsumeType }
                                      into g
                                      select new
                                      {
                                          g.Key.FullName,
                                          g.Key.OperatorsOnDuty,
                                          g.Key.ConsumeType,
                                          Amount = g.Sum(s => s.Amount)
                                      };
                        this.MyQuery = q21.OrderBy(o=>o.FullName);
                        if (q21.Count() > 0)
                            BalanceSum = q21.Sum(s => s.Amount);
                        break;
                    case 2:
                        var q22 = from q4 in q
                                  where q4.ConsumeType == "打折卡消费"
                                  group q4 by new { q4.FullName,q4.OperatorsOnDuty, q4.ConsumeType }
                                      into g
                                      select new
                                      {
                                          g.Key.FullName,
                                          g.Key.OperatorsOnDuty,
                                          g.Key.ConsumeType,
                                          Amount = g.Sum(s => s.Amount)
                                      };
                        this.MyQuery = q22.OrderBy(o=>o.FullName);
                        if (q22.Count() > 0)
                            BalanceSum = q22.Sum(s => s.Amount);
                        break;
                }
            }
        }         
    }
    /// <summary>
    /// 西餐厅报表基类
    /// </summary>
    public class WRReportViewModelBase : ReportViewModelBase
    {
        public WRReportViewModelBase(IFairiesMemberManageUow uow)
            : base(uow)
        {
            this.BeginTime = new DateTime(1, 1, 1, 3, 0, 0);
            this.EndTime = new DateTime(1, 1, 1, 3, 0, 0);
        }
    }
    /// <summary>
    /// 前厅消费明细查询
    /// </summary>
    public class WRReport10ViewModel : WRReportViewModelBase
    {
        public WRReport10ViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
        }
        protected override void query()
        {
            //查询
            var q = from cl in Uow.ConsumeList.GetAll().Where(w => w.DeptId == this.Dept.DeptId)
                    join c in Uow.Consume.GetAll() on cl.Consume equals c.Id into clc
                    from clcs in clc.DefaultIfEmpty()

                    join p in Uow.PayTypes.GetAll() on clcs.PayType equals p.Id into cp
                    from cps in cp.DefaultIfEmpty()

                    join card in Uow.Cards.GetAll() on clcs.Card.Value equals card.Id into ccard
                    from ccards in ccard.DefaultIfEmpty()

                    join member in Uow.Members.GetAll() on ccards.Member equals member.Id into cm
                    from cms in cm.DefaultIfEmpty()

                    join i in Uow.Inventory.GetAll() on cl.Inventory equals i.Id into ci
                    from cis in ci.DefaultIfEmpty()

                    join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into cu
                    from cus in cu.DefaultIfEmpty()

                    join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cd
                    from cds in cd.DefaultIfEmpty()

                    join ic in Uow.InventoryCategory.GetAll() on cis.Category equals ic.Id into iic
                    from iics in iic.DefaultIfEmpty()

                    where iics.SectionType == 2
                    select new
                    {
                        cl.Id,
                        ccards.CardNo,
                        cms.MemberName,
                        InventoryName = cis.Name,
                        cl.UserId,
                        cus.FullName,
                        cds.DeptName,
                        cl.Discount,
                        cl.Amount,
                        cl.Sum,
                        cl.CreateDate,
                        cl.Price,
                        cl.Quantity,
                        clcs.ConsumeType,
                        ConsumeTypeName = clcs.ConsumeType == 0 ? "会员卡消费" : clcs.ConsumeType == 1 ? "非会员消费" : clcs.ConsumeType == 2 ? "会员积分兑换" : "打折卡消费",
                        clcs.PayType,
                        PayTypeName = clcs.ConsumeType == 0 ? "会员卡" : clcs.ConsumeType == 2 ? "积分兑换" : cps.Name,
                        clcs.Voucher
                    };
            if (!string.IsNullOrWhiteSpace(MemberName))
                q = q.Where(w => w.MemberName.Contains(MemberName));
            if (!string.IsNullOrWhiteSpace(CardNo))
                q = q.Where(w => w.CardNo.Contains(CardNo));
            if (this.SelectedOper != null)
            {
                //Guid userId = Guid.Parse(OperList.SelectedValue.ToString());
                q = q.Where(w => w.UserId == this.SelectedOper.UserId);
            }
            if (this.SelectedConsumeType != null)
            {
                //int iConsumeType = Convert.ToInt32(ConsumeType.SelectedValue.ToString());
                q = q.Where(w => w.ConsumeType == this.SelectedConsumeType.Id);
            }
            if (this.SelectedPayType != null)
            {
                //DXInfo.Models.PayTypes pt = cmbPayType.SelectedItem as DXInfo.Models.PayTypes;
                q = q.Where(w => w.PayType == this.SelectedPayType.Id);
            }
            if (BeginDate>DateTime.MinValue)
            {
                //DateTime dtBeginDate = Convert.ToDateTime(BeginDate.Text + " " + BeginTime.Value.Value.ToShortTimeString());
                DateTime dtBeginDate = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day, BeginTime.Hour, BeginTime.Minute, BeginTime.Second, BeginTime.Millisecond);
                q = q.Where(w => w.CreateDate >= dtBeginDate);
            }
            if (EndDate > DateTime.MinValue)
            {
                DateTime dtEndDate = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hour, EndTime.Minute, EndTime.Second, EndTime.Millisecond);
                if (EndTime.TimeOfDay == TimeSpan.Zero)
                {
                    dtEndDate = dtEndDate.AddDays(1);
                }
                q = q.Where(w => w.CreateDate <= dtEndDate);
            }

            var q1 = (from s in q
                      select new ReportResult()
                      {
                          Id = s.Id,
                          CardNo = s.CardNo,
                          MemberName = s.MemberName,
                          InventoryName = s.InventoryName,
                          PayTypeName = s.PayTypeName,
                          ConsumeTypeName = s.ConsumeType == 0 ? "会员卡消费" : s.ConsumeType == 1 ? "非会员消费" : s.ConsumeType == 2 ? "会员积分兑换" : "打折卡消费",
                          FullName = s.FullName,
                          CreateDate = s.CreateDate,
                          DeptName = s.DeptName,
                          Sum = s.Sum,
                          Discount = s.Discount,
                          Amount = s.Amount,
                          Price = s.Price,
                          Quantity = s.Quantity
                      });
            var q2 = (from s in q.Where(w => w.Voucher > 0)
                      select new ReportResult()
                      {
                          Id = Guid.Empty,
                          CardNo = s.CardNo,
                          MemberName = s.MemberName,
                          InventoryName = "代金券",
                          PayTypeName = "代金券",
                          ConsumeTypeName = s.ConsumeType == 0 ? "会员卡消费" : s.ConsumeType == 1 ? "非会员消费" : s.ConsumeType == 2 ? "会员积分兑换" : "打折卡消费",
                          FullName = s.FullName,
                          CreateDate = s.CreateDate,
                          DeptName = s.DeptName,
                          Sum = -s.Voucher,
                          Discount = s.Discount,
                          Amount = -((s.Voucher * s.Discount) / 100),
                          Price = -s.Voucher,
                          Quantity = 1
                      }).Distinct();

            var q3 = q1.Concat(q2).OrderBy(o=>o.CreateDate);

            //MemberList.ItemsSource = q3.ToObservableCollection();
            this.MyQuery = q3;
            if (q3.Count() > 0)
            {
                SumQuantity = q3.Sum(s => s.Quantity);
                SumPayable = q3.Sum(s => s.Sum);
                SumAmount = q3.Sum(s => s.Amount);
            }
        }
    }
    /// <summary>
    /// 消费明细查询
    /// </summary>
    public class WRReport2ViewModel : WRReportViewModelBase
    {
        public WRReport2ViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
        }
        protected override void query()
        {
            //查询
            var q = from cl in Uow.ConsumeList.GetAll().Where(w => w.DeptId == this.Dept.DeptId)
                    join c in Uow.Consume.GetAll() on cl.Consume equals c.Id into clc
                    from clcs in clc.DefaultIfEmpty()

                    join p in Uow.PayTypes.GetAll() on clcs.PayType equals p.Id into cp
                    from cps in cp.DefaultIfEmpty()

                    join card in Uow.Cards.GetAll() on clcs.Card.Value equals card.Id into ccard
                    from ccards in ccard.DefaultIfEmpty()

                    join member in Uow.Members.GetAll() on ccards.Member equals member.Id into cm
                    from cms in cm.DefaultIfEmpty()

                    join i in Uow.Inventory.GetAll() on cl.Inventory equals i.Id into ci
                    from cis in ci.DefaultIfEmpty()

                    join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into cu
                    from cus in cu.DefaultIfEmpty()

                    join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cd
                    from cds in cd.DefaultIfEmpty()

                    select new
                    {
                        cl.Id,
                        ccards.CardNo,
                        cms.MemberName,
                        InventoryName = cis.Name,
                        cl.UserId,
                        cus.FullName,
                        cds.DeptName,
                        cl.Discount,
                        cl.Amount,
                        cl.Sum,
                        cl.CreateDate,
                        cl.Price,
                        cl.Quantity,
                        clcs.ConsumeType,
                        ConsumeTypeName = clcs.ConsumeType == 0 ? "会员卡消费" : clcs.ConsumeType == 1 ? "非会员消费" : clcs.ConsumeType == 2 ? "会员积分兑换" : "打折卡消费",
                        clcs.PayType,
                        PayTypeName = clcs.ConsumeType == 0 ? "会员卡" : clcs.ConsumeType == 2 ? "积分兑换" : cps.Name,
                        clcs.Voucher
                    };
            if (!string.IsNullOrWhiteSpace(MemberName))
                q = q.Where(w => w.MemberName.Contains(MemberName));
            if (!string.IsNullOrWhiteSpace(CardNo))
                q = q.Where(w => w.CardNo.Contains(CardNo));
            if (this.SelectedOper != null)
            {
                //Guid userId = Guid.Parse(OperList.SelectedValue.ToString());
                q = q.Where(w => w.UserId == this.SelectedOper.UserId);
            }
            if (this.SelectedConsumeType != null)
            {
                //int iConsumeType = Convert.ToInt32(ConsumeType.SelectedValue.ToString());
                q = q.Where(w => w.ConsumeType == this.SelectedConsumeType.Id);
            }
            if (this.SelectedPayType != null)
            {
                //DXInfo.Models.PayTypes pt = cmbPayType.SelectedItem as DXInfo.Models.PayTypes;
                q = q.Where(w => w.PayType == this.SelectedPayType.Id);
            }
            if (BeginDate>DateTime.MinValue)
            {
                //DateTime dtBeginDate = Convert.ToDateTime(BeginDate.Text + " " + BeginTime.Value.Value.ToShortTimeString());
                DateTime dtBeginDate = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day, BeginTime.Hour, BeginTime.Minute, BeginTime.Second, BeginTime.Millisecond);
                q = q.Where(w => w.CreateDate >= dtBeginDate);
            }
            if (EndDate>DateTime.MinValue)
            {
                DateTime dtEndDate = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hour, EndTime.Minute, EndTime.Second, EndTime.Millisecond);
                if (EndTime.TimeOfDay == TimeSpan.Zero)
                {
                    dtEndDate = dtEndDate.AddDays(1);
                }
                q = q.Where(w => w.CreateDate <= dtEndDate);
            }

            var q1 = (from s in q
                      select new ReportResult()
                      {
                          Id = s.Id,
                          CardNo = s.CardNo,
                          MemberName = s.MemberName,
                          InventoryName = s.InventoryName,
                          PayTypeName = s.PayTypeName,
                          ConsumeTypeName = s.ConsumeType == 0 ? "会员卡消费" : s.ConsumeType == 1 ? "非会员消费" : s.ConsumeType == 2 ? "会员积分兑换" : "打折卡消费",
                          FullName = s.FullName,
                          CreateDate = s.CreateDate,
                          DeptName = s.DeptName,
                          Sum = s.Sum,
                          Discount = s.Discount,
                          Amount = s.Amount,
                          Price = s.Price,
                          Quantity = s.Quantity
                      });
            var q2 = (from s in q.Where(w => w.Voucher > 0)
                      select new ReportResult()
                      {
                          Id = Guid.Empty,
                          CardNo = s.CardNo,
                          MemberName = s.MemberName,
                          InventoryName = "代金券",
                          PayTypeName = "代金券",
                          ConsumeTypeName = s.ConsumeType == 0 ? "会员卡消费" : s.ConsumeType == 1 ? "非会员消费" : s.ConsumeType == 2 ? "会员积分兑换" : "打折卡消费",
                          FullName = s.FullName,
                          CreateDate = s.CreateDate,
                          DeptName = s.DeptName,
                          Sum = -s.Voucher,
                          Discount = s.Discount,
                          Amount = -((s.Voucher * s.Discount) / 100),
                          Price = -s.Voucher,
                          Quantity = 1
                      }).Distinct();

            var q3 = q1.Concat(q2).OrderBy(o => o.CreateDate);
            //Debug.WriteLine(q3.ToString()); 
            //ObservableCollection<WRReport2Result> ll = q3.ToObservableCollection();

            //MemberList.ItemsSource = q3.ToObservableCollection();
            this.MyQuery = q3;
            if (q3.Count() > 0)
            {
                SumQuantity = q3.Sum(s => s.Quantity);
                SumPayable = q3.Sum(s => s.Sum);
                SumAmount = q3.Sum(s => s.Amount);
            }
        }
    }
    /// <summary>
    /// 充值明细查询
    /// </summary>
    public class WRReport3ViewModel : WRReportViewModelBase
    {
        public WRReport3ViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
        }
        protected override void query()
        {
            //查询
            var q = from r in Uow.Recharges.GetAll().Where(w => w.DeptId == this.Dept.DeptId)
                    join c in Uow.Cards.GetAll() on r.Card equals c.Id into rc
                    from rcs in rc.DefaultIfEmpty()

                    join p in Uow.PayTypes.GetAll() on r.PayType equals p.Id into rp
                    from rps in rp.DefaultIfEmpty()

                    join m in Uow.Members.GetAll() on rcs.Member equals m.Id into rcsm
                    from rcsms in rcsm.DefaultIfEmpty()

                    join u in Uow.aspnet_CustomProfile.GetAll() on r.UserId equals u.UserId into ru
                    from rus in ru.DefaultIfEmpty()

                    join d in Uow.Depts.GetAll() on r.DeptId equals d.DeptId into rd
                    from rds in rd.DefaultIfEmpty()
                    select new
                    {
                        r.Amount,
                        r.Balance,
                        r.CreateDate,
                        r.Donate,
                        r.Id,
                        r.LastBalance,
                        iRechargeType = r.RechargeType,
                        RechargeType = r.RechargeType == 0 ? "普通充值" : r.RechargeType == 1 ? "补卡充值" : r.RechargeType == 2 ? "发卡充值" : r.RechargeType == 3 ? "补卡费" : "其它",
                        r.UserId,
                        rcs.CardNo,
                        rcsms.MemberName,
                        rus.FullName,
                        rds.DeptName,
                        PayType = rps == null ? Guid.Empty : rps.Id,
                        PayTypeName = rps == null ? "" : rps.Name
                    };
            if (!string.IsNullOrWhiteSpace(MemberName))
                q = q.Where(w => w.MemberName.Contains(MemberName));
            if (!string.IsNullOrWhiteSpace(CardNo))
                q = q.Where(w => w.CardNo.Contains(CardNo));
            if (this.SelectedOper != null)
            {
                //Guid userId = Guid.Parse(OperList.SelectedValue.ToString());
                q = q.Where(w => w.UserId == SelectedOper.UserId);
            }
            if (this.SelectedRechargeType != null)
            {
                //int iRechargeType = Convert.ToInt32(RechargeType.SelectedValue.ToString());
                q = q.Where(w => w.iRechargeType == this.SelectedRechargeType.Id);
            }
            if (this.SelectedPayType != null)
            {
                //DXInfo.Models.PayTypes pt = cmbPayType.SelectedItem as DXInfo.Models.PayTypes;
                q = q.Where(w => w.PayType == this.SelectedPayType.Id);
            }
            if (BeginDate>DateTime.MinValue)
            {
                //DateTime dtBeginDate = Convert.ToDateTime(BeginDate.Text + " " + BeginTime.Value.Value.ToShortTimeString());
                DateTime dtBeginDate = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day, BeginTime.Hour, BeginTime.Minute, BeginTime.Second, BeginTime.Millisecond);
                q = q.Where(w => w.CreateDate >= dtBeginDate);
            }
            if (EndDate>DateTime.MinValue)
            {
                DateTime dtEndDate = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hour, EndTime.Minute, EndTime.Second, EndTime.Millisecond);
                if (EndTime.TimeOfDay == TimeSpan.Zero)
                {
                    dtEndDate = dtEndDate.AddDays(1);
                }
                q = q.Where(w => w.CreateDate <= dtEndDate);
            }

            //MemberList.ItemsSource = q.ToObservableCollection();
            this.MyQuery = q.OrderBy(o => o.CreateDate);
            if (q.Count() > 0)
            {
                SumAmount = q.Sum(s => s.Amount);
                //txtAmount.Text = dAmount.ToString("c");

                SumDonate = q.Sum(s => s.Donate);
                //txtDonate.Text = dDonate.ToString("c");
                Sum = (SumAmount + SumDonate);
            }
        }
    }
    /// <summary>
    /// 消费分类统计
    /// </summary>
    public class WRReport4ViewModel : WRReportViewModelBase
    {
        public WRReport4ViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
        }
        protected override void query()
        {
            //查询
            var q = from cl in Uow.ConsumeList.GetAll().Where(w => w.DeptId == this.Dept.DeptId)
                    join c in Uow.Consume.GetAll() on cl.Consume equals c.Id into clc
                    from clcs in clc.DefaultIfEmpty()

                    join i in Uow.Inventory.GetAll() on cl.Inventory equals i.Id into ci
                    from cis in ci.DefaultIfEmpty()

                    join ic in Uow.InventoryCategory.GetAll() on cis.Category equals ic.Id into iic
                    from iics in iic.DefaultIfEmpty()

                    select new
                    {
                        cl.Id,
                        InventoryName = cis.Name,
                        CategoryName = iics.Name,
                        Category = iics.Id,
                        cl.UserId,
                        cl.Amount,
                        cl.CreateDate,
                        cl.Quantity,
                        iConsumeType = clcs.ConsumeType,
                        ConsumeType = clcs.ConsumeType == 0 ? "会员卡消费" : clcs.ConsumeType == 1 ? "非会员消费" : clcs.ConsumeType == 2 ? "会员积分兑换" : "打折卡消费"
                    };
            if (this.SelectedOper != null)
            {
                //Guid userId = Guid.Parse(OperList.SelectedValue.ToString());
                q = q.Where(w => w.UserId == this.SelectedOper.UserId);
            }
            if (this.SelectedConsumeType != null)
            {
                //int iConsumeType = Convert.ToInt32(ConsumeType.SelectedValue.ToString());
                q = q.Where(w => w.iConsumeType == this.SelectedConsumeType.Id);
            }
            if (this.SelectedInventoryCategory != null)
            {
                //Guid cid = Guid.Parse(InvCategory.SelectedValue.ToString());
                q = q.Where(w => w.Category == this.SelectedInventoryCategory.Id);
            }
            if (BeginDate>DateTime.MinValue)
            {
                //DateTime dtBeginDate = Convert.ToDateTime(BeginDate.Text + " " + BeginTime.Value.Value.ToShortTimeString());
                DateTime dtBeginDate = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day, BeginTime.Hour, BeginTime.Minute, BeginTime.Second, BeginTime.Millisecond);
                q = q.Where(w => w.CreateDate >= dtBeginDate);
            }
            if (EndDate>DateTime.MinValue)
            {
                DateTime dtEndDate = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hour, EndTime.Minute, EndTime.Second, EndTime.Millisecond);
                if (EndTime.TimeOfDay == TimeSpan.Zero)
                {
                    dtEndDate = dtEndDate.AddDays(1);
                }
                q = q.Where(w => w.CreateDate <= dtEndDate);
            }
            if (this.SelectedInventoryCategory != null)
            {
                var q2 = from q4 in q
                         group q4 by new { q4.CategoryName, q4.InventoryName, q4.ConsumeType }
                             into g
                             select new
                             {
                                 g.Key.CategoryName,
                                 g.Key.InventoryName,
                                 g.Key.ConsumeType,
                                 Amount = g.Sum(s => s.Amount),
                                 Quantity = g.Sum(s => s.Quantity)
                             };
                foreach (DataGridColumn dgc in this.MyDataGrid.Columns)
                {
                    if (dgc.Header.ToString() == "商品")
                    {
                        dgc.Visibility = System.Windows.Visibility.Visible;
                    }
                }
                //MemberList.ItemsSource = q2.ToObservableCollection();
                this.MyQuery = q2.OrderBy(o=>o.CategoryName);
                if (q2.Count() > 0)
                {

                    SumAmount = q2.Sum(s => s.Amount);
                    SumQuantity = q2.Sum(s => s.Quantity);
                }
            }
            else
            {

                var q2 = from q4 in q
                         group q4 by new { q4.CategoryName, q4.ConsumeType }
                             into g
                             select new
                             {
                                 g.Key.CategoryName,
                                 g.Key.ConsumeType,
                                 Amount = g.Sum(s => s.Amount),
                                 Quantity = g.Sum(s => s.Quantity)
                             };
                foreach (DataGridColumn dgc in this.MyDataGrid.Columns)
                {
                    if (dgc.Header.ToString() == "商品")
                    {
                        dgc.Visibility = System.Windows.Visibility.Hidden;
                    }
                }
                //MemberList.ItemsSource = q2.ToObservableCollection();
                this.MyQuery = q2.OrderBy(o=>o.CategoryName);                     
                if (q2.Count() > 0)
                {

                    SumAmount = q2.Sum(s => s.Amount);
                    SumQuantity = q2.Sum(s => s.Quantity);
                }

            }
        }
    }
    /// <summary>
    /// 销售排名统计
    /// </summary>
    public class WRReport5ViewModel : WRReportViewModelBase
    {
        public WRReport5ViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
        }
        protected override void query()
        {
            //有两种，一：卡号和消费金额，二：商品销售排行
            //查询
            var q = from cl in Uow.ConsumeList.GetAll().Where(w => w.DeptId == this.Dept.DeptId)
                    join c in Uow.Consume.GetAll() on cl.Consume equals c.Id into clc
                    from clcs in clc.DefaultIfEmpty()

                    join c in Uow.Cards.GetAll() on clcs.Card equals c.Id into clcsc
                    from clcscs in clcsc.DefaultIfEmpty()

                    join m in Uow.Members.GetAll() on clcscs.Member equals m.Id into clcscsm
                    from clcscsms in clcscsm.DefaultIfEmpty()

                    join i in Uow.Inventory.GetAll() on cl.Inventory equals i.Id into ci
                    from cis in ci.DefaultIfEmpty()

                    join ic in Uow.InventoryCategory.GetAll() on cis.Category equals ic.Id into iic
                    from iics in iic.DefaultIfEmpty()

                    select new
                    {
                        cl.Id,
                        InventoryName = cis.Name,
                        CategoryName = iics.Name,
                        Category = iics.Id,
                        cl.UserId,
                        clcscs.CardNo,
                        clcscsms.MemberName,
                        cl.Amount,
                        cl.CreateDate,
                        cl.Quantity,
                        iConsumeType = clcs.ConsumeType,
                        ConsumeType = clcs.ConsumeType == 0 ? "会员卡消费" : clcs.ConsumeType == 1 ? "非会员消费" : clcs.ConsumeType == 2 ? "会员积分兑换" : "打折卡消费",
                    };
            if (this.SelectedOper != null)
            {
                //Guid userId = Guid.Parse(OperList.SelectedValue.ToString());
                q = q.Where(w => w.UserId == this.SelectedOper.UserId);
            }
            if (this.SelectedConsumeType != null)
            {
                //int iConsumeType = Convert.ToInt32(ConsumeType.SelectedValue.ToString());
                q = q.Where(w => w.iConsumeType == this.SelectedConsumeType.Id);
            }
            if (this.SelectedInventoryCategory != null)
            {
                //Guid cid = Guid.Parse(InvCategory.SelectedValue.ToString());
                q = q.Where(w => w.Category == this.SelectedInventoryCategory.Id);
            }
            if (BeginDate>DateTime.MinValue)
            {
                //DateTime dtBeginDate = Convert.ToDateTime(BeginDate.Text + " " + BeginTime.Value.Value.ToShortTimeString());
                DateTime dtBeginDate = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day, BeginTime.Hour, BeginTime.Minute, BeginTime.Second, BeginTime.Millisecond);
                q = q.Where(w => w.CreateDate >= dtBeginDate);
            }
            if (EndDate>DateTime.MinValue)
            {
                DateTime dtEndDate = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hour, EndTime.Minute, EndTime.Second, EndTime.Millisecond);
                if (EndTime.TimeOfDay == TimeSpan.Zero)
                {
                    dtEndDate = dtEndDate.AddDays(1);
                }
                q = q.Where(w => w.CreateDate <= dtEndDate);
            }

            if (this.IsCard)
            {
                var q2 = from q4 in q
                         group q4 by new { q4.CardNo, q4.MemberName, q4.ConsumeType }
                             into g
                             select new
                             {
                                 g.Key.CardNo,
                                 g.Key.MemberName,
                                 g.Key.ConsumeType,
                                 Amount = g.Sum(s => s.Amount),
                                 Quantity = g.Sum(s => s.Quantity)
                             };


                foreach (DataGridColumn dgc in this.MyDataGrid.Columns)
                {
                    if (dgc.Header.ToString() == "分类" || dgc.Header.ToString() == "商品")
                    {
                        dgc.Visibility = System.Windows.Visibility.Hidden;
                    }
                    if (dgc.Header.ToString() == "卡号" || dgc.Header.ToString() == "会员名")
                    {
                        dgc.Visibility = System.Windows.Visibility.Visible;
                    }
                }
                //MemberList.ItemsSource = q2.ToObservableCollection();
                this.MyQuery = q2.OrderBy(o=>o.CardNo);
                if (q2.Count() > 0)
                {

                    SumAmount = q2.Sum(s => s.Amount);
                    SumQuantity = q2.Sum(s => s.Quantity);
                }
            }
            else
            {
                if (this.SelectedInventoryCategory != null)
                {
                    var q2 = from q4 in q
                             group q4 by new { q4.CategoryName, q4.InventoryName, q4.ConsumeType }
                                 into g
                                 select new
                                 {
                                     g.Key.CategoryName,
                                     g.Key.InventoryName,
                                     g.Key.ConsumeType,
                                     Amount = g.Sum(s => s.Amount),
                                     Quantity = g.Sum(s => s.Quantity)
                                 };
                    foreach (DataGridColumn dgc in this.MyDataGrid.Columns)
                    {
                        if (dgc.Header.ToString() == "卡号" || dgc.Header.ToString() == "会员名")
                        {
                            dgc.Visibility = System.Windows.Visibility.Hidden;
                        }
                        if (dgc.Header.ToString() == "分类" || dgc.Header.ToString() == "商品")
                        {
                            dgc.Visibility = System.Windows.Visibility.Visible;
                        }
                    }
                    //MemberList.ItemsSource = q2.ToObservableCollection();
                    this.MyQuery = q2.OrderBy(o=>o.CategoryName);
                    if (q2.Count() > 0)
                    {

                        SumAmount = q2.Sum(s => s.Amount);
                        SumQuantity = q2.Sum(s => s.Quantity);
                    }
                }
                else
                {
                    var q2 = from q4 in q
                             group q4 by new { q4.CategoryName, q4.ConsumeType }
                                 into g
                                 select new
                                 {
                                     g.Key.CategoryName,
                                     g.Key.ConsumeType,
                                     Amount = g.Sum(s => s.Amount),
                                     Quantity = g.Sum(s => s.Quantity)
                                 };
                    foreach (DataGridColumn dgc in this.MyDataGrid.Columns)
                    {
                        if (dgc.Header.ToString() == "商品" || dgc.Header.ToString() == "卡号" || dgc.Header.ToString() == "会员名")
                        {
                            dgc.Visibility = System.Windows.Visibility.Hidden;
                        }
                        if (dgc.Header.ToString() == "分类")
                        {
                            dgc.Visibility = System.Windows.Visibility.Visible;
                        }
                    }
                    //MemberList.ItemsSource = q2.ToObservableCollection();
                    this.MyQuery = q2.OrderBy(o=>o.CategoryName);
                    if (q2.Count() > 0)
                    {

                        SumAmount = q2.Sum(s => s.Amount);
                        SumQuantity = q2.Sum(s => s.Quantity);
                    }
                }
            }
        }
    }
    /// <summary>
    /// 收银查询
    /// </summary>
    public class WRReport7ViewModel : WRReportViewModelBase
    {
        public WRReport7ViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
        }
        protected override void query()
        {
            //查询

            var q = from cl in Uow.Consume.GetAll().Where(w => w.DeptId == this.Dept.DeptId)

                    join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into cu
                    from cus in cu.DefaultIfEmpty()

                    join p in Uow.PayTypes.GetAll() on cl.PayType equals p.Id into cp
                    from cps in cp.DefaultIfEmpty()

                    where cl.ConsumeType == 1 || cl.ConsumeType == 3
                    select new
                    {
                        cl.Id,
                        cl.UserId,
                        cus.FullName,
                        cl.Amount,
                        cl.CreateDate,
                        cl.ConsumeType,
                        ConsumeTypeName = cl.ConsumeType == 1 ? "非会员消费" : "打折卡消费",
                        PayType = cl.PayType.Value,
                        PayTypeName = cps.Name,
                    };
            if (this.SelectedOper != null)
            {
                q = q.Where(w => w.UserId == this.SelectedOper.UserId);
            }
            if (BeginDate>DateTime.MinValue)
            {
                DateTime dtBeginDate = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day, BeginTime.Hour, BeginTime.Minute, BeginTime.Second, BeginTime.Millisecond);
                q = q.Where(w => w.CreateDate >= dtBeginDate);
            }
            if (EndDate>DateTime.MinValue)
            {
                DateTime dtEndDate = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hour, EndTime.Minute, EndTime.Second, EndTime.Millisecond);
                if (EndTime.TimeOfDay == TimeSpan.Zero)
                {
                    dtEndDate = dtEndDate.AddDays(1);
                }
                q = q.Where(w => w.CreateDate <= dtEndDate);
            }
            if (this.SelectedPayType != null)
            {
                q = q.Where(w => w.PayType == this.SelectedPayType.Id);
            }
            if (this.SelectedConsumeType != null)
            {
                q = q.Where(w => w.ConsumeType == this.SelectedConsumeType.Id);
            }
            var bb = q.ToList();
            //充值
            var qy = from r in Uow.Recharges.GetAll().Where(w => w.DeptId == this.Dept.DeptId).Where(w => w.RechargeType == 0 || w.RechargeType == 2)
                     join u in Uow.aspnet_CustomProfile.GetAll() on r.UserId equals u.UserId into ru
                     from rus in ru.DefaultIfEmpty()
                     join p in Uow.PayTypes.GetAll() on r.PayType equals p.Id into cp
                     from cps in cp.DefaultIfEmpty()
                     select new
                     {
                         r.Id,
                         r.UserId,
                         rus.FullName,
                         r.Amount,
                         r.CreateDate,
                         ConsumeType=r.RechargeType,
                         ConsumeTypeName = r.RechargeType == 0 ? "会员卡充值" : "发卡充值",
                         r.PayType,
                         PayTypeName=cps.Name
                     };
            if (this.SelectedOper != null)
            {
                qy = qy.Where(w => w.UserId == SelectedOper.UserId);
            }
            if (BeginDate>DateTime.MinValue)
            {
                DateTime dtBeginDate = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day, BeginTime.Hour, BeginTime.Minute, BeginTime.Second, BeginTime.Millisecond);
                qy = qy.Where(w => w.CreateDate >= dtBeginDate);
            }
            if (EndDate>DateTime.MinValue)
            {
                DateTime dtEndDate = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hour, EndTime.Minute, EndTime.Second, EndTime.Millisecond);
                if (EndTime.TimeOfDay == TimeSpan.Zero)
                {
                    dtEndDate = dtEndDate.AddDays(1);
                }
                qy = qy.Where(w => w.CreateDate <= dtEndDate);
            }
            if (this.SelectedPayType != null)
            {
                qy = qy.Where(w => w.PayType == this.SelectedPayType.Id);
            }
            var cc = qy.ToList();

            var qq = q.Union(qy);

            var q2 = from q4 in qq
                        group q4 by new { q4.FullName, q4.ConsumeTypeName,q4.PayTypeName }
                            into g
                            select new
                            {
                                g.Key.FullName,
                                g.Key.ConsumeTypeName,
                                g.Key.PayTypeName,
                                Amount = g.Sum(s => s.Amount)
                            };

            this.MyQuery = q2.OrderBy(o=>o.FullName);
            if (q2.Count() > 0)
                BalanceSum = q2.Sum(s => s.Amount);

        }        
    }
    /// <summary>
    /// 后厨消费明细查询
    /// </summary>
    public class WRReport8ViewModel : WRReportViewModelBase
    {
        public WRReport8ViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
        }
        protected override void query()
        {
            //查询
            var q = from cl in Uow.ConsumeList.GetAll().Where(w => w.DeptId == this.Dept.DeptId)
                    join c in Uow.Consume.GetAll() on cl.Consume equals c.Id into clc
                    from clcs in clc.DefaultIfEmpty()

                    join p in Uow.PayTypes.GetAll() on clcs.PayType equals p.Id into cp
                    from cps in cp.DefaultIfEmpty()

                    join card in Uow.Cards.GetAll() on clcs.Card.Value equals card.Id into ccard
                    from ccards in ccard.DefaultIfEmpty()

                    join member in Uow.Members.GetAll() on ccards.Member equals member.Id into cm
                    from cms in cm.DefaultIfEmpty()

                    join i in Uow.Inventory.GetAll() on cl.Inventory equals i.Id into ci
                    from cis in ci.DefaultIfEmpty()

                    join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into cu
                    from cus in cu.DefaultIfEmpty()

                    join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cd
                    from cds in cd.DefaultIfEmpty()

                    join ic in Uow.InventoryCategory.GetAll() on cis.Category equals ic.Id into iic
                    from iics in iic.DefaultIfEmpty()

                    where iics.SectionType == 0
                    select new
                    {
                        cl.Id,
                        ccards.CardNo,
                        cms.MemberName,
                        InventoryName = cis.Name,
                        cl.UserId,
                        cus.FullName,
                        cds.DeptName,
                        cl.Discount,
                        cl.Amount,
                        cl.Sum,
                        cl.CreateDate,
                        cl.Price,
                        cl.Quantity,
                        clcs.ConsumeType,
                        ConsumeTypeName = clcs.ConsumeType == 0 ? "会员卡消费" : clcs.ConsumeType == 1 ? "非会员消费" : clcs.ConsumeType == 2 ? "会员积分兑换" : "打折卡消费",
                        clcs.PayType,
                        PayTypeName = clcs.ConsumeType == 0 ? "会员卡" : clcs.ConsumeType == 2 ? "积分兑换" : cps.Name,
                        clcs.Voucher
                    };
            if (!string.IsNullOrWhiteSpace(MemberName))
                q = q.Where(w => w.MemberName.Contains(MemberName));
            if (!string.IsNullOrWhiteSpace(CardNo))
                q = q.Where(w => w.CardNo.Contains(CardNo));
            if (this.SelectedOper != null)
            {
                //Guid userId = Guid.Parse(OperList.SelectedValue.ToString());
                q = q.Where(w => w.UserId == SelectedOper.UserId);
            }
            if (this.SelectedConsumeType != null)
            {
                //int iConsumeType = Convert.ToInt32(ConsumeType.SelectedValue.ToString());
                q = q.Where(w => w.ConsumeType == SelectedConsumeType.Id);
            }
            if (this.SelectedPayType != null)
            {
                //DXInfo.Models.PayTypes pt = cmbPayType.SelectedItem as DXInfo.Models.PayTypes;
                q = q.Where(w => w.PayType == SelectedPayType.Id);
            }
            if (BeginDate>DateTime.MinValue)
            {
                //DateTime dtBeginDate = Convert.ToDateTime(BeginDate.Text + " " + BeginTime.Value.Value.ToShortTimeString());
                DateTime dtBeginDate = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day, BeginTime.Hour, BeginTime.Minute, BeginTime.Second, BeginTime.Millisecond);
                q = q.Where(w => w.CreateDate >= dtBeginDate);
            }
            if (EndDate>DateTime.MinValue)
            {
                DateTime dtEndDate = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hour, EndTime.Minute, EndTime.Second, EndTime.Millisecond);
                if (EndTime.TimeOfDay == TimeSpan.Zero)
                {
                    dtEndDate = dtEndDate.AddDays(1);
                }
                q = q.Where(w => w.CreateDate <= dtEndDate);
            }

            var q1 = (from s in q
                      select new ReportResult()
                      {
                          Id = s.Id,
                          CardNo = s.CardNo,
                          MemberName = s.MemberName,
                          InventoryName = s.InventoryName,
                          PayTypeName = s.PayTypeName,
                          ConsumeTypeName = s.ConsumeType == 0 ? "会员卡消费" : s.ConsumeType == 1 ? "非会员消费" : s.ConsumeType == 2 ? "会员积分兑换" : "打折卡消费",
                          FullName = s.FullName,
                          CreateDate = s.CreateDate,
                          DeptName = s.DeptName,
                          Sum = s.Sum,
                          Discount = s.Discount,
                          Amount = s.Amount,
                          Price = s.Price,
                          Quantity = s.Quantity
                      });
            var q2 = (from s in q.Where(w => w.Voucher > 0)
                      select new ReportResult()
                      {
                          Id = Guid.Empty,
                          CardNo = s.CardNo,
                          MemberName = s.MemberName,
                          InventoryName = "代金券",
                          PayTypeName = "代金券",
                          ConsumeTypeName = s.ConsumeType == 0 ? "会员卡消费" : s.ConsumeType == 1 ? "非会员消费" : s.ConsumeType == 2 ? "会员积分兑换" : "打折卡消费",
                          FullName = s.FullName,
                          CreateDate = s.CreateDate,
                          DeptName = s.DeptName,
                          Sum = -s.Voucher,
                          Discount = s.Discount,
                          Amount = -((s.Voucher * s.Discount) / 100),
                          Price = -s.Voucher,
                          Quantity = 1
                      }).Distinct();

            var q3 = q1.Concat(q2).OrderBy(o=>o.CreateDate);

            //MemberList.ItemsSource = q3.ToObservableCollection();
            this.MyQuery = q3;
            if (q3.Count() > 0)
            {
                SumQuantity = q3.Sum(s => s.Quantity);
                SumPayable = q3.Sum(s => s.Sum);
                SumAmount = q3.Sum(s => s.Amount);
            }
        }
    }
    /// <summary>
    /// 后厨2消费明细查询
    /// </summary>
    public class WRReport11ViewModel : WRReportViewModelBase
    {
        public WRReport11ViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
        }
        protected override void query()
        {
            //查询
            var q = from cl in Uow.ConsumeList.GetAll().Where(w => w.DeptId == this.Dept.DeptId)
                    join c in Uow.Consume.GetAll() on cl.Consume equals c.Id into clc
                    from clcs in clc.DefaultIfEmpty()

                    join p in Uow.PayTypes.GetAll() on clcs.PayType equals p.Id into cp
                    from cps in cp.DefaultIfEmpty()

                    join card in Uow.Cards.GetAll() on clcs.Card.Value equals card.Id into ccard
                    from ccards in ccard.DefaultIfEmpty()

                    join member in Uow.Members.GetAll() on ccards.Member equals member.Id into cm
                    from cms in cm.DefaultIfEmpty()

                    join i in Uow.Inventory.GetAll() on cl.Inventory equals i.Id into ci
                    from cis in ci.DefaultIfEmpty()

                    join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into cu
                    from cus in cu.DefaultIfEmpty()

                    join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cd
                    from cds in cd.DefaultIfEmpty()

                    join ic in Uow.InventoryCategory.GetAll() on cis.Category equals ic.Id into iic
                    from iics in iic.DefaultIfEmpty()

                    where iics.SectionType == 3
                    select new
                    {
                        cl.Id,
                        ccards.CardNo,
                        cms.MemberName,
                        InventoryName = cis.Name,
                        cl.UserId,
                        cus.FullName,
                        cds.DeptName,
                        cl.Discount,
                        cl.Amount,
                        cl.Sum,
                        cl.CreateDate,
                        cl.Price,
                        cl.Quantity,
                        clcs.ConsumeType,
                        ConsumeTypeName = clcs.ConsumeType == 0 ? "会员卡消费" : clcs.ConsumeType == 1 ? "非会员消费" : clcs.ConsumeType == 2 ? "会员积分兑换" : "打折卡消费",
                        clcs.PayType,
                        PayTypeName = clcs.ConsumeType == 0 ? "会员卡" : clcs.ConsumeType == 2 ? "积分兑换" : cps.Name,
                        clcs.Voucher
                    };
            if (!string.IsNullOrWhiteSpace(MemberName))
                q = q.Where(w => w.MemberName.Contains(MemberName));
            if (!string.IsNullOrWhiteSpace(CardNo))
                q = q.Where(w => w.CardNo.Contains(CardNo));
            if (this.SelectedOper != null)
            {
                //Guid userId = Guid.Parse(OperList.SelectedValue.ToString());
                q = q.Where(w => w.UserId == SelectedOper.UserId);
            }
            if (this.SelectedConsumeType != null)
            {
                //int iConsumeType = Convert.ToInt32(ConsumeType.SelectedValue.ToString());
                q = q.Where(w => w.ConsumeType == SelectedConsumeType.Id);
            }
            if (this.SelectedPayType != null)
            {
                //DXInfo.Models.PayTypes pt = cmbPayType.SelectedItem as DXInfo.Models.PayTypes;
                q = q.Where(w => w.PayType == SelectedPayType.Id);
            }
            if (BeginDate > DateTime.MinValue)
            {
                //DateTime dtBeginDate = Convert.ToDateTime(BeginDate.Text + " " + BeginTime.Value.Value.ToShortTimeString());
                DateTime dtBeginDate = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day, BeginTime.Hour, BeginTime.Minute, BeginTime.Second, BeginTime.Millisecond);
                q = q.Where(w => w.CreateDate >= dtBeginDate);
            }
            if (EndDate > DateTime.MinValue)
            {
                DateTime dtEndDate = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hour, EndTime.Minute, EndTime.Second, EndTime.Millisecond);
                if (EndTime.TimeOfDay == TimeSpan.Zero)
                {
                    dtEndDate = dtEndDate.AddDays(1);
                }
                q = q.Where(w => w.CreateDate <= dtEndDate);
            }

            var q1 = (from s in q
                      select new ReportResult()
                      {
                          Id = s.Id,
                          CardNo = s.CardNo,
                          MemberName = s.MemberName,
                          InventoryName = s.InventoryName,
                          PayTypeName = s.PayTypeName,
                          ConsumeTypeName = s.ConsumeType == 0 ? "会员卡消费" : s.ConsumeType == 1 ? "非会员消费" : s.ConsumeType == 2 ? "会员积分兑换" : "打折卡消费",
                          FullName = s.FullName,
                          CreateDate = s.CreateDate,
                          DeptName = s.DeptName,
                          Sum = s.Sum,
                          Discount = s.Discount,
                          Amount = s.Amount,
                          Price = s.Price,
                          Quantity = s.Quantity
                      });
            var q2 = (from s in q.Where(w => w.Voucher > 0)
                      select new ReportResult()
                      {
                          Id = Guid.Empty,
                          CardNo = s.CardNo,
                          MemberName = s.MemberName,
                          InventoryName = "代金券",
                          PayTypeName = "代金券",
                          ConsumeTypeName = s.ConsumeType == 0 ? "会员卡消费" : s.ConsumeType == 1 ? "非会员消费" : s.ConsumeType == 2 ? "会员积分兑换" : "打折卡消费",
                          FullName = s.FullName,
                          CreateDate = s.CreateDate,
                          DeptName = s.DeptName,
                          Sum = -s.Voucher,
                          Discount = s.Discount,
                          Amount = -((s.Voucher * s.Discount) / 100),
                          Price = -s.Voucher,
                          Quantity = 1
                      }).Distinct();

            var q3 = q1.Concat(q2).OrderBy(o => o.CreateDate);

            //MemberList.ItemsSource = q3.ToObservableCollection();
            this.MyQuery = q3;
            if (q3.Count() > 0)
            {
                SumQuantity = q3.Sum(s => s.Quantity);
                SumPayable = q3.Sum(s => s.Sum);
                SumAmount = q3.Sum(s => s.Amount);
            }
        }
    }
    /// <summary>
    /// 吧台消费明细查询
    /// </summary>
    public class WRReport9ViewModel : WRReportViewModelBase
    {
        public WRReport9ViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
        }
        protected override void query()
        {
            //查询
            var q = from cl in Uow.ConsumeList.GetAll().Where(w => w.DeptId == this.Dept.DeptId)
                    join c in Uow.Consume.GetAll() on cl.Consume equals c.Id into clc
                    from clcs in clc.DefaultIfEmpty()

                    join p in Uow.PayTypes.GetAll() on clcs.PayType equals p.Id into cp
                    from cps in cp.DefaultIfEmpty()

                    join card in Uow.Cards.GetAll() on clcs.Card.Value equals card.Id into ccard
                    from ccards in ccard.DefaultIfEmpty()

                    join member in Uow.Members.GetAll() on ccards.Member equals member.Id into cm
                    from cms in cm.DefaultIfEmpty()

                    join i in Uow.Inventory.GetAll() on cl.Inventory equals i.Id into ci
                    from cis in ci.DefaultIfEmpty()

                    join u in Uow.aspnet_CustomProfile.GetAll() on cl.UserId equals u.UserId into cu
                    from cus in cu.DefaultIfEmpty()

                    join d in Uow.Depts.GetAll() on cl.DeptId equals d.DeptId into cd
                    from cds in cd.DefaultIfEmpty()
                    join ic in Uow.InventoryCategory.GetAll() on cis.Category equals ic.Id into iic
                    from iics in iic.DefaultIfEmpty()

                    where iics.SectionType == 1
                    select new
                    {
                        cl.Id,
                        ccards.CardNo,
                        cms.MemberName,
                        InventoryName = cis.Name,
                        cl.UserId,
                        cus.FullName,
                        cds.DeptName,
                        cl.Discount,
                        cl.Amount,
                        cl.Sum,
                        cl.CreateDate,
                        cl.Price,
                        cl.Quantity,
                        clcs.ConsumeType,
                        ConsumeTypeName = clcs.ConsumeType == 0 ? "会员卡消费" : clcs.ConsumeType == 1 ? "非会员消费" : clcs.ConsumeType == 2 ? "会员积分兑换" : "打折卡消费",
                        clcs.PayType,
                        PayTypeName = clcs.ConsumeType == 0 ? "会员卡" : clcs.ConsumeType == 2 ? "积分兑换" : cps.Name,
                        clcs.Voucher
                    };
            if (!string.IsNullOrWhiteSpace(MemberName))
                q = q.Where(w => w.MemberName.Contains(MemberName));
            if (!string.IsNullOrWhiteSpace(CardNo))
                q = q.Where(w => w.CardNo.Contains(CardNo));
            if (this.SelectedOper != null)
            {
                //Guid userId = Guid.Parse(OperList.SelectedValue.ToString());
                q = q.Where(w => w.UserId == SelectedOper.UserId);
            }
            if (this.SelectedConsumeType != null)
            {
                //int iConsumeType = Convert.ToInt32(ConsumeType.SelectedValue.ToString());
                q = q.Where(w => w.ConsumeType == SelectedConsumeType.Id);
            }
            if (this.SelectedPayType != null)
            {
                //DXInfo.Models.PayTypes pt = cmbPayType.SelectedItem as DXInfo.Models.PayTypes;
                q = q.Where(w => w.PayType == SelectedPayType.Id);
            }
            if (BeginDate>DateTime.MinValue)
            {
                //DateTime dtBeginDate = Convert.ToDateTime(BeginDate.Text + " " + BeginTime.Value.Value.ToShortTimeString());
                DateTime dtBeginDate = new DateTime(BeginDate.Year, BeginDate.Month, BeginDate.Day, BeginTime.Hour, BeginTime.Minute, BeginTime.Second, BeginTime.Millisecond);
                q = q.Where(w => w.CreateDate >= dtBeginDate);
            }
            if (EndDate>DateTime.MinValue)
            {
                DateTime dtEndDate = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hour, EndTime.Minute, EndTime.Second, EndTime.Millisecond);
                if (EndTime.TimeOfDay == TimeSpan.Zero)
                {
                    dtEndDate = dtEndDate.AddDays(1);
                }
                q = q.Where(w => w.CreateDate <= dtEndDate);
            }

            var q1 = (from s in q
                      select new ReportResult()
                      {
                          Id = s.Id,
                          CardNo = s.CardNo,
                          MemberName = s.MemberName,
                          InventoryName = s.InventoryName,
                          PayTypeName = s.PayTypeName,
                          ConsumeTypeName = s.ConsumeType == 0 ? "会员卡消费" : s.ConsumeType == 1 ? "非会员消费" : s.ConsumeType == 2 ? "会员积分兑换" : "打折卡消费",
                          FullName = s.FullName,
                          CreateDate = s.CreateDate,
                          DeptName = s.DeptName,
                          Sum = s.Sum,
                          Discount = s.Discount,
                          Amount = s.Amount,
                          Price = s.Price,
                          Quantity = s.Quantity
                      });
            var q2 = (from s in q.Where(w => w.Voucher > 0)
                      select new ReportResult()
                      {
                          Id = Guid.Empty,
                          CardNo = s.CardNo,
                          MemberName = s.MemberName,
                          InventoryName = "代金券",
                          PayTypeName = "代金券",
                          ConsumeTypeName = s.ConsumeType == 0 ? "会员卡消费" : s.ConsumeType == 1 ? "非会员消费" : s.ConsumeType == 2 ? "会员积分兑换" : "打折卡消费",
                          FullName = s.FullName,
                          CreateDate = s.CreateDate,
                          DeptName = s.DeptName,
                          Sum = -s.Voucher,
                          Discount = s.Discount,
                          Amount = -((s.Voucher * s.Discount) / 100),
                          Price = -s.Voucher,
                          Quantity = 1
                      }).Distinct();

            var q3 = q1.Concat(q2).OrderBy(o => o.CreateDate);

            //MemberList.ItemsSource = q3.ToObservableCollection();
            this.MyQuery = q3;
            if (q3.Count() > 0)
            {
                SumQuantity = q3.Sum(s => s.Quantity);
                SumPayable = q3.Sum(s => s.Sum);
                SumAmount = q3.Sum(s => s.Amount);
            }
        }
    }
    public class CurrentStockInfo
    {
        public Guid DeptId { get; set; }
        public string DeptName { get; set; }
        public Guid WhId { get; set; }
        public string WhName { get; set; }
        public Guid InvId { get; set; }
        public string InvName { get; set; }
        public Guid STUnit { get; set; }
        public string STName { get; set; }
        public decimal Num { get; set; }
    }
    public class QueryCurrentStockViewModel : ReportViewModelBase
    {        
        public QueryCurrentStockViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
            this.SetBaseUri();
        }
        
        protected override void query()
        {
            List<CurrentStockInfo> lCurrentStockInfo = this.SetlCurrentStockInfo(this.InvName);
            this.MyQuery = lCurrentStockInfo.AsQueryable();
        }
        protected List<CurrentStockInfo> SetlCurrentStockInfo(string InvName)
        {
            List<CurrentStockInfo> lCurrentStockInfo = new List<CurrentStockInfo>();
            string url = "StockManage/GetCurrentStock.aspx?InvName=" + InvName;
            Uri uri = new Uri(BaseUri, url);
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Encoding = UTF8Encoding.UTF8;
                    string strHtml = client.DownloadString(uri);
                    lCurrentStockInfo = DXInfo.Business.Helper.JsonDeserialize<List<CurrentStockInfo>>(strHtml);
                }
            }
            catch (WebException we)
            {
                Helper.ShowErrorMsg(we.Message);
            }
            return lCurrentStockInfo;
        }
    }
}
