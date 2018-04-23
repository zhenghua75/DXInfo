using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using FairiesCoolerCash.Business;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel.DataAnnotations;
using GalaSoft.MvvmLight.Messaging;
using Unity.ServiceLocation;
using AutoMapper;
using CommonServiceLocator;

namespace FairiesCoolerCash.ViewModel
{
    public class AddMemberViewModel:BusinessViewModelBase
    {
        
        
        public AddMemberViewModel(IFairiesMemberManageUow uow)
            : base(uow, new List<string>() { "SelectedCardLevel", "SelectedCardType", "CardNo", "MemberName" })
        {
            this.SetCardLevelAuto();
            if (this.IsCardLevelAuto)
            {
                List<string> lValidationPropertyNames = new List<string>() { "SelectedCardType", "CardNo", "MemberName" };
                this.SetValidate(lValidationPropertyNames);
            }
            else
            {
                this.SetlCardLevel();
            }
            this.InitObject();
            this.SetlCardType();
        }
        protected virtual void InitObject()
        {
            this.Card = new DXInfo.Models.Cards();
            this.Member = new DXInfo.Models.Members();
            this.CardNo = "";
            this.MemberName = "";
            this.SelectedCardLevel = null;
            this.SelectedCardType = null;
        }
        
        #region 添加会员
        private bool Validate(out string msg)
        {
            msg = "";

            if (!this.IsCardLevelAuto && this.SelectedCardLevel == null)
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
            if (!this.IsCardLevelAuto && !string.IsNullOrEmpty(this.SelectedCardLevel.BeginLetter))
            {
                if (!this.CardNo.StartsWith(this.SelectedCardLevel.BeginLetter))//&& this.Title != "修改会员")
                {
                    msg = "卡号必须以" + this.SelectedCardLevel.BeginLetter + "字母开头";
                    return false;
                }
            }
            string strComment;
            string strCardNoRule = ClientCommon.CardNoRule(this.SelectedCardType, out strComment);
            if (!Regex.IsMatch(this.CardNo, strCardNoRule))
            {
                if (!this.IsCardLevelAuto && !string.IsNullOrEmpty(this.SelectedCardLevel.BeginLetter))
                {
                    msg = strComment+"，且必须以" + this.SelectedCardLevel.BeginLetter + "字母开头";
                    return false;
                }
                else
                {
                    msg = strComment;
                    return false;
                }
            }
            return true;
        }
        private void addMember()
        {
            string msg;
            if (!Validate(out msg))
            {
                Helper.ShowErrorMsg(msg);
                return;
            }
            DateTime dtNow = DateTime.Now;
            this.Member.UserId = this.Oper.UserId;
            this.Member.CreateDate = dtNow;
            this.Member.DeptId = this.Dept.DeptId;

            int oldcount = Uow.Cards.GetAll().Where(w => w.CardNo == this.CardNo).Count();
            if (oldcount > 0)
            {
                Helper.ShowErrorMsg(this.CardNo + "：卡号已存在");
                return;
            }
            this.Card.CardNo = this.CardNo;
            if (this.IsCardLevelAuto)
            {
                var cardLevel = Uow.CardLevels.GetAll().Where(w => w.IsDefault &&
                    (w.DeptId == this.Dept.DeptId || w.DeptId == Guid.Empty)).OrderBy(o => o.Code).FirstOrDefault();
                if (cardLevel == null)
                {
                    Helper.ShowErrorMsg("请设置无积分时的默认卡级别");
                    return;
                }
                this.Card.CardLevel = cardLevel.Id;
            }
            else
            {
                this.Card.CardLevel = this.SelectedCardLevel.Id;
            }
            this.Card.CardType = this.SelectedCardType.Id;
            this.Card.CreateDate = dtNow;
            this.Card.DeptId = this.Dept.DeptId;
            this.Card.UserId = this.Oper.UserId;

            this.Member.MemberName = this.MemberName;
            
            if (!this.SelectedCardType.IsVirtual)
            {
                StringBuilder sb = new StringBuilder(33);
                sb.Append(this.CardNo);
//#if DEBUG
//                int st = 0;
//#else
                int st = CardRef.CoolerPutCard(sb);
//#endif
                if (st != 0)
                {
                    Helper.ShowErrorMsg(CardRef.GetStr(st));
                    return;
                }
            }
            using (TransactionScope transaction = new TransactionScope())
            {
                Uow.Members.Add(this.Member);
                Uow.Commit();

                this.Card.Member = this.Member.Id;
                Uow.Cards.Add(this.Card);
                Uow.Commit();

                DXInfo.Models.MembersLog memberLog = Mapper.Map<DXInfo.Models.Members, DXInfo.Models.MembersLog>(Member);
                memberLog.MemberId = Member.Id;
                Uow.MembersLog.Add(memberLog);

                DXInfo.Models.CardsLog cardsLog = Mapper.Map<DXInfo.Models.Cards, DXInfo.Models.CardsLog>(Card);
                cardsLog.CardId = Card.Id;
                Uow.CardsLog.Add(cardsLog);

                Uow.Commit();
                transaction.Complete();
            }            
            if (this.SelectedCardType.IsMoney)
            {
                MessageBoxResult mr = MessageBox.Show("添加新会员成功，是否充值？", "充值提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (mr == MessageBoxResult.Yes)
                {
                    var cmp = ServiceLocator.Current.GetInstance<PutCardInMondeyUserControl>();
                    this.NavigationUserControl(cmp);
                }
            }
            else
            {
                Helper.ShowSuccMsg("添加新会员成功。");
            }
            this.InitObject();
        }
        public ICommand AddMember
        {
            get
            {
                return new RelayCommand(addMember,AddMemberCanExecute);
            }
        }
        private bool AddMemberCanExecute()
        {
            return this.IsValid;
        }
        #endregion


        protected override void AfterSelectCardLevel()
        {
            if (!this.IsCardLevelAuto && this.SelectedCardLevel != null)
            {
                this.CardNo = this.SelectedCardLevel.BeginLetter;
            }
        }      
    }
}
