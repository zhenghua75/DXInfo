using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using DXInfo.Data.Contracts;
using WpfKb.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using FairiesCoolerCash.Business;
using System.Windows;
//using System.Data.Objects.SqlClient;
using GalaSoft.MvvmLight.Messaging;
using System.Data.Entity.SqlServer;

namespace FairiesCoolerCash.ViewModel
{
    public class MemberQueryViewModel : ReportViewModelBase
    {
        public MemberQueryViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
            this.Card = new DXInfo.Models.Cards();
        }
        public override void LoadData()
        {
            this.SetlCardType();
            this.SetlAllCardLevel();
            this.SetlCardStatus();
        }
        #region 查询
        protected override void query()
        {
            //查询
            Guid ge = Guid.Empty;
            string ncType = DXInfo.Models.NameCodeType.CardStatus.ToString();
            var ms = 
                     from m in Uow.Members.GetAll()
                     join c in Uow.aspnet_CustomProfile.GetAll() on m.UserId equals c.UserId into mc
                     from mcs in mc.DefaultIfEmpty()

                     join cd in Uow.Cards.GetAll() on m.Id equals cd.Member into mcd
                     from mcds in mcd.DefaultIfEmpty()

                     join ct in Uow.CardTypes.GetAll() on mcds.CardType equals ct.Id into mct
                     from mcts in mct.DefaultIfEmpty()

                     join cl in Uow.CardLevels.GetAll() on mcds.CardLevel equals cl.Id into mcl
                     from mcls in mcl.DefaultIfEmpty()

                     join d in Uow.Depts.GetAll() on m.DeptId equals d.DeptId into md
                     from mds in md.DefaultIfEmpty()

                     join c1 in Uow.aspnet_CustomProfile.GetAll() on m.ModifyUserId equals c1.UserId into mc1
                     from mc1s in mc1.DefaultIfEmpty()

                     join d1 in Uow.Depts.GetAll() on m.ModifyDeptId equals d1.DeptId into md1
                     from md1s in md1.DefaultIfEmpty()

                     join n in Uow.NameCode.GetAll().Where(w=>w.Type==ncType) on SqlFunctions.StringConvert((double?)mcds.Status).Trim() equals n.Code  into nmcds
                     from nmcdss in nmcds.DefaultIfEmpty()
                     
                     where mcds!=null
                     orderby m.CreateDate
                     select new
                     {
                         m.Comments,
                         m.CreateDate,
                         mds.DeptName,
                         m.Email,
                         m.Id,
                         m.IdCard,
                         m.LinkAddress,
                         m.LinkPhone,
                         m.MemberName,
                         m.MemberType,
                         m.ModifyDate,
                         ModifyDeptName = md1s.DeptName,
                         ModifyFullName = mc1s.FullName,
                         mcs.FullName,
                         Status=mcds==null?-1:mcds.Status,
                         //CardStatus = mcds==null?"":mcds.Status == 0 ? "正常在用" : mcds.Status == 1 ? "已挂失" : "已补卡",
                         CardStatus=nmcdss==null?"":nmcdss.Name,
                         CardLevel = mcds==null?ge:mcds.CardLevel,
                         CardLevelName = mcls.Name,
                         CardType=mcds==null?ge:mcds.CardType,
                         CardTypeName = mcts.Name,
                         mcds.CardNo,
                         Balance=mcds==null?0:mcds.Balance,
                         m.Birthday,
                         m.Sex,
                         CardId = mcds==null?ge:mcds.Id
                     };
            if (!string.IsNullOrWhiteSpace(this.MemberName))
                ms = ms.Where(w => w.MemberName.Contains(this.MemberName));
            if (!string.IsNullOrWhiteSpace(this.IdCard))
                ms = ms.Where(w => w.IdCard.Contains(this.IdCard));
            if (!string.IsNullOrWhiteSpace(this.LinkPhone))
                ms = ms.Where(w => w.LinkPhone.Contains(this.LinkPhone));
            if (!string.IsNullOrWhiteSpace(this.LinkAddress))
                ms = ms.Where(w => w.LinkAddress.Contains(this.LinkAddress));
            if (!string.IsNullOrWhiteSpace(this.Email))
                ms = ms.Where(w => w.Email.Contains(this.Email));
            if (!string.IsNullOrWhiteSpace(this.Comments))
                ms = ms.Where(w => w.Comments.Contains(this.Comments));

            if (!string.IsNullOrEmpty(this.CardNo))
                ms = ms.Where(w => w.CardNo.Contains(this.CardNo));

            if (this.SelectedCardType != null)
            {
                ms = ms.Where(w => w.CardType == this.SelectedCardType.Id);
            }
            if (this.SelectedCardLevel != null)
            {
                ms = ms.Where(w => w.CardLevel == this.SelectedCardLevel.Id);
            }
            if (this.SelectedCardStatus !=null)
            {
                ms = ms.Where(w => w.Status == this.SelectedCardStatus.Id);
            }

            this.MyQuery = ms;
            this.BalanceSum = ms.Sum(s => (decimal?)s.Balance) ?? 0m;            
        }
        protected override void AfterSwipingCard()
        {
            base.AfterSwipingCard();
            this.CardNo = this.Card.CardNo;
        }
        #endregion

        #region 编辑
        private void EditResultExecute()
        {
            if (this.SelectedResult != null)
            {
                dynamic d = this.SelectedResult;
                ModifyMemberUserControl ap = new ModifyMemberUserControl(Uow, d.Id, d.CardId);
                this.NavigationUserControl(ap);
            }
        }
        public ICommand EditResult
        {
            get
            {
                return new RelayCommand(EditResultExecute);
            }
        }
        #endregion

        #region 确定
        private void OKExecute()
        {
            
            this.DialogResult = true;
            //Messenger.Default.Send(new CloseMemberQueryWindow());
        }
        public ICommand OK
        {
            get
            {
                return new RelayCommand(OKExecute);
            }
        }
        #endregion
    }
    
}
