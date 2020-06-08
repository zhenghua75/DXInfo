using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Controls;
using FairiesCoolerCash.Business;
using System.Windows;
using AutoMapper;

namespace FairiesCoolerCash.ViewModel
{
    public class CardLossViewModel : ReportViewModelBase
    {
        private readonly IMapper mapper;
        public CardLossViewModel(IFairiesMemberManageUow uow, IMapper mapper)
            : base(uow,mapper)
        {
            this.mapper = mapper;
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
            var cs = from c in Uow.Cards.GetAll().Where(w => w.Status == 0)
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

                     orderby c.CardNo
                     select new
                     {
                         c.Id,
                         c.CardNo,
                         cl = cls.Id,
                         CardLevel=cls.Id,
                         CardLevelName = cls.Name,
                         c.CardType,
                         c.Balance,
                         c.CreateDate,
                         cds.DeptName,
                         cus.FullName,
                         cms.MemberName,
                         cms.IdCard,
                         CardTypeName = cts.Name,
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
            //var cl1 = cs.ToList().Select(s => new
            //{
            //    s.Id,
            //    s.CardNo,
            //    CardLevel=s.CardLevelName,
            //    CardType = s.CardTypeName,
            //    s.Balance,
            //    s.CreateDate,
            //    s.DeptName,
            //    s.FullName,
            //    s.MemberName,
            //    s.IdCard,
            //    s.LinkAddress,
            //    s.LinkPhone,
            //    s.Email
            //});
            //this.QueryResult = cl1.ToObservableCollection<object>();
            this.MyQuery = cs;
        }
        #endregion

        #region 挂失
        public ICommand CardLoss
        {
            get
            {
                return new RelayCommand<object>(cardLoss);
            }
        }
        private void cardLoss(object sender)
        {
            //挂失
            if(!ClientCommon.CheckUser(this.Oper)) return;
            Guid cid = Guid.Parse((sender as Button).Tag.ToString());
            var c = Uow.Cards.GetById(g => g.Id == cid);
            if (!ClientCommon.CheckCard(c)) return;

            MessageBoxResult result = MessageBox.Show("是否挂失此卡？卡号：" + c.CardNo, "挂失", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes)
            {
                return;
            }
            DateTime dtNow = DateTime.Now;
            c.LossDate = dtNow;
            c.LossUserId = this.Oper.UserId;
            c.LossDeptId = this.Dept.DeptId;
            c.Status = 1;
            Uow.Cards.Update(c);

            DXInfo.Models.CardsLog cardsLog = mapper.Map<DXInfo.Models.Cards, DXInfo.Models.CardsLog>(c);
            cardsLog.CardId = c.Id;
            cardsLog.CreateDate = dtNow;
            cardsLog.UserId = this.Oper.UserId;
            cardsLog.DeptId = this.Dept.DeptId;
            Uow.CardsLog.Add(cardsLog);

            Uow.Commit();
            MessageBox.Show("挂失成功");
            this.query();
        }
        #endregion
    }
}
