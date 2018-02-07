using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using FairiesCoolerCash.Business;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Text.RegularExpressions;
using DXInfo.Data;
using AutoMapper;

namespace FairiesCoolerCash.ViewModel
{
    public class ModifyMemberViewModel : BusinessViewModelBase
    {
        public ModifyMemberViewModel(IFairiesMemberManageUow uow, Guid memberId, Guid cardId)
            : base(uow, new List<string>() { "SelectedCardLevel", "SelectedCardType", "CardNo", "MemberName" })
        {
            this.Member = uow.Members.GetById(g=>g.Id==memberId);
            this.Card = uow.Cards.GetById(g=>g.Id==cardId);
            this.CardNo = this.Card.CardNo;
            this.MemberName = this.Member.MemberName;  
            
        }
        public override void LoadData()
        {
            this.SetlCardType();
            this.SetlAllCardLevel();
        }
        public override void Cleanup()
        {
            base.Cleanup();
            this.Card = null;
            this.Member = null;
            this.CardNo = "";
            this.MemberName = "";
            this.SelectedCardLevel = null;
            this.SelectedCardType = null;
        }
        private bool Validate(out string msg)
        {
            msg = "";
            if (this.SelectedCardLevel == null)
            {
                msg = "请选择卡级别";
                return false;
            }
            if (string.IsNullOrEmpty(this.CardNo))
            {
                msg = "请输入卡号";
                return false;
            }
            if (this.SelectedCardType == null)
            {
                msg = "请选择卡类型";
                return false;
            }
            if (string.IsNullOrEmpty(this.MemberName))
            {
                msg = "请输入会员名";
                return false;
            }
            return true;
        }
        public ICommand ModifyMember
        {
            get
            {
                return new RelayCommand(ModifyMemberExecute, ModifyMemberCanExecute);
            }
        }
        private bool ModifyMemberCanExecute()
        {
            return this.IsValid;
        }
        private void ModifyMemberExecute()
        {
            string msg;
            if (!Validate(out msg))
            {
                Helper.ShowErrorMsg(msg);
                return;
            }
            DateTime dtNow = DateTime.Now;
            this.Member.MemberName = this.MemberName;
            this.Member.ModifyDate = dtNow;
            this.Member.ModifyDeptId = this.Dept.DeptId;
            this.Member.ModifyUserId = this.Oper.UserId;
            Uow.Members.Update(this.Member);

            DXInfo.Models.MembersLog memberLog = Mapper.Map<DXInfo.Models.Members, DXInfo.Models.MembersLog>(Member);
            memberLog.MemberId = Member.Id;
            memberLog.UserId = this.Oper.UserId;
            memberLog.DeptId = this.Dept.DeptId;
            memberLog.CreateDate = dtNow;
            Uow.MembersLog.Add(memberLog);

            if (!string.IsNullOrEmpty(this.CardPwd) && this.CardPwd != this.Card.CardPwd)
            {
                DXInfo.Models.Cards oldCard = Uow.Cards.GetById(g => g.Id == this.Card.Id);
                if (oldCard != null)
                {
                    oldCard.CardPwd = this.CardPwd;
                    Uow.Cards.Update(oldCard);

                    DXInfo.Models.CardsLog cardsLog = Mapper.Map<DXInfo.Models.Cards, DXInfo.Models.CardsLog>(oldCard);
                    cardsLog.CardId = oldCard.Id;
                    cardsLog.UserId = this.Oper.UserId;
                    cardsLog.DeptId = this.Dept.DeptId;
                    cardsLog.CreateDate = dtNow;
                    Uow.CardsLog.Add(cardsLog);
                }
            }
            Uow.Commit();
            Helper.ShowSuccMsg("修改会员成功");
        }
        protected override void AfterSelectCardLevel()
        {
        }   
    }
}
