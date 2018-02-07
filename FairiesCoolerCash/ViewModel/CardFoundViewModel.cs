using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using FairiesCoolerCash.Business;
using System.Windows.Controls;
using System.Windows;
using AutoMapper;

namespace FairiesCoolerCash.ViewModel
{
    public class CardFoundViewModel : ReportViewModelBase
    {
        public CardFoundViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
        }
        public override void LoadData()
        {
            this.SetlCardType();
            this.SetlCardLevel();
        }
        #region 查询
        protected override void query()
        {
            //查询
            var cs = from c in Uow.Cards.GetAll().Where(w => w.Status == 1)
                     join m in Uow.Members.GetAll() on c.Member equals m.Id into cm
                     from cms in cm.DefaultIfEmpty()

                     join l in Uow.CardLevels.GetAll() on c.CardLevel equals l.Id into cl
                     from cls in cl.DefaultIfEmpty()

                     join t in Uow.CardTypes.GetAll() on c.CardType equals t.Id into ct
                     from cts in ct.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on c.DeptId equals d.DeptId into cd
                     from cds in cd.DefaultIfEmpty()

                     join u in Uow.aspnet_CustomProfile.GetAll() on c.UserId equals u.UserId into cu
                     from cus in cu.DefaultIfEmpty()

                     join d1 in Uow.Depts.GetAll() on c.LossDeptId equals d1.DeptId into cd1
                     from cd1s in cd1.DefaultIfEmpty()

                     join u1 in Uow.aspnet_CustomProfile.GetAll() on c.LossUserId equals u1.UserId into cu1
                     from cu1s in cu1.DefaultIfEmpty()

                     orderby c.CardNo
                     select new
                     {
                         c.Id,
                         c.CardNo,
                         cl = cls.Id,
                         CardLevel = cls.Id,
                         CardLevelName = cls.Name,
                         c.CardType,
                         c.CreateDate,
                         cds.DeptName,
                         cus.FullName,
                         cms.MemberName,
                         cms.IdCard,
                         c.LossDate,
                         LossDeptName = cd1s.DeptName,
                         LossFullName = cu1s.FullName,
                         CardTypeName = cts.Name,
                         c.Balance,
                         cms.LinkAddress,
                         cms.LinkPhone,
                         cms.Email
                     };

            if (!string.IsNullOrWhiteSpace(this.MemberName))
                cs = cs.Where(w => w.MemberName.Contains(this.MemberName));
            if (!string.IsNullOrWhiteSpace(this.IdCard))
                cs = cs.Where(w => w.IdCard.Contains(this.IdCard));
            if (!string.IsNullOrEmpty(this.CardNo))
                cs = cs.Where(w => w.CardNo.Contains(this.CardNo));
            if (this.SelectedCardType != null)
            {
                cs = cs.Where(w => w.CardType == this.SelectedCardType.Id);
            }
            if (this.SelectedCardLevel != null)
            {
                cs = cs.Where(w => w.CardLevel == this.SelectedCardLevel.Id);
            }
            //cs.OrderBy(o => o.CardNo);
            //var cl1 = cs.ToList().Select(s => new
            //{
            //    s.Id,
            //    s.CardNo,
            //    CardLevel=s.CardLevelName,
            //    CardType = s.CardTypeName,//s.CardType == 0 ? "普通卡" : s.CardType == 1 ? "折扣卡" : "零售卡",
            //    s.CreateDate,
            //    s.DeptName,
            //    s.FullName,
            //    s.MemberName,
            //    s.IdCard,
            //    s.LossDate,
            //    s.LossDeptName,
            //    s.LossFullName,
            //    s.Balance,
            //    s.LinkPhone,
            //    s.LinkAddress,
            //    s.Email
            //});
            //this.QueryResult = cl1.ToObservableCollection<object>();
            this.MyQuery = cs;
        }
        #endregion

        #region 解挂
        public ICommand CardFound
        {
            get
            {
                return new RelayCommand<object>(cardFound);
            }
        }
        private void cardFound(object sender)
        {
            //解挂
            //DXInfo.Models.aspnet_CustomProfile user = this.Oper;
            
            //Guid userId = user.UserId;
            if (!ClientCommon.CheckUser(this.Oper)) return;
            Guid cid = Guid.Parse((sender as Button).Tag.ToString());
            var c = Uow.Cards.GetById(g => g.Id == cid);
            if (!ClientCommon.CheckCard(c)) return;
            MessageBoxResult result = MessageBox.Show("是否解挂此卡？卡号：" + c.CardNo, "解挂", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }
            DateTime dtNow = DateTime.Now;
            c.FoundDate = dtNow;
            c.FoundUserId = this.Oper.UserId;
            c.FoundDeptId = this.Dept.DeptId;
            c.Status = 0;
            Uow.Cards.Update(c);

            DXInfo.Models.CardsLog cardsLog = Mapper.Map<DXInfo.Models.Cards, DXInfo.Models.CardsLog>(c);
            cardsLog.CardId = c.Id;
            cardsLog.CreateDate = dtNow;
            cardsLog.UserId = this.Oper.UserId;
            cardsLog.DeptId = this.Dept.DeptId;
            Uow.CardsLog.Add(cardsLog);

            Uow.Commit();

            MessageBox.Show("解挂成功");
            this.query();
        }
        #endregion
    }
}
