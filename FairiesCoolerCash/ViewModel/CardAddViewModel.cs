using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using GalaSoft.MvvmLight;
using FairiesCoolerCash.Business;
using System.Text.RegularExpressions;
using System.Windows;
using System.Transactions;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using AutoMapper;

namespace FairiesCoolerCash.ViewModel
{
    public class CardAddPageDetail : ObservableObject
    {
        #region 会员名
        private DXInfo.Models.Cards _Card;
        public DXInfo.Models.Cards Card
        {
            get
            {
                return _Card;
            }
            set
            {
                _Card = value;
                this.RaisePropertyChanged("Card");
            }
        }
        #endregion

        #region 工本费
        private decimal _Cost;
        public decimal Cost
        {
            get
            {
                return _Cost;
            }
            set
            {
                _Cost = value;
                this.RaisePropertyChanged("Cost");
            }
        }
        #endregion
    }
    public class CardAddViewModel : ReportViewModelBase
    {
        public CardAddViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
        }
        public override void LoadData()
        {
            this.SetlCardType();
            this.SetlCardLevel();
        }
        #region 行明细
        private CardAddPageDetail _Detail;
        public CardAddPageDetail Detail
        {
            get
            {
                return _Detail;
            }
            set
            {
                _Detail = value;
                this.RaisePropertyChanged("Detail");
            }
        }
        #endregion
        
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
                         c.CardLevel,
                         CardLevelName = cls.Name,
                         c.CardType,
                         CardTypeName = cts.Name,
                         cls.BeginLetter,
                         c.CreateDate,
                         cds.DeptName,
                         cus.FullName,
                         cms.MemberName,
                         cms.IdCard,
                         c.LossDate,
                         LossDeptName = cd1s.DeptName,
                         LossFullName = cu1s.FullName,
                         c.Balance,
                         cms.LinkPhone,
                         cms.LinkAddress,
                         cms.Email,                         
                     };

            if (!string.IsNullOrWhiteSpace(this.MemberName))
                cs = cs.Where(w => w.MemberName.Contains(this.MemberName));
            if (!string.IsNullOrWhiteSpace(this.IdCard))
                cs = cs.Where(w => w.IdCard.Contains(this.IdCard));
            if (!string.IsNullOrEmpty(this.CardNo))
                cs = cs.Where(w => w.CardNo.Contains(this.CardNo));
            if (this.SelectedCardType !=null)
            {
                cs = cs.Where(w => w.CardType == this.SelectedCardType.Id);
            }
            if (this.SelectedCardLevel!=null)
            {
                cs = cs.Where(w => w.CardLevel == this.SelectedCardLevel.Id);
            }
            if (!string.IsNullOrEmpty(this.LinkPhone))
            {
                cs = cs.Where(w => w.LinkPhone.Contains(this.LinkPhone));
            }
            //cs.OrderBy(o => o.CardNo);
            //this.QueryResult = cs.ToObservableCollection<object>();
            this.MyQuery = cs;
        }
        #endregion

        #region 选择
        protected override void AfterSelectResult()
        {
            base.AfterSelectResult();
            if (this.SelectedResult == null) return;
            if (this.Detail == null)
            {
                this.Detail = new CardAddPageDetail();
            }
            dynamic c = this.SelectedResult;//CardList.SelectedItem;
            Guid id = c.Id;
            DXInfo.Models.Cards card = Uow.Cards.GetById(g => g.Id == id);
            var d = Uow.CardLevels.GetById(g => g.Id == card.CardLevel);
            if (d != null)
            {
                card.SecondCardNo = d.BeginLetter;
            }
            this.Detail.Card = card;

            DXInfo.Models.NameCode nc = Uow.NameCode.GetAll().Where(w => w.Type == "CostFee").FirstOrDefault();
            if (nc != null)
            {
                this.Detail.Cost = Convert.ToDecimal(nc.Value);
            }
        }
        #endregion

        #region 补卡
        public ICommand CardAdd
        {
            get
            {
                return new RelayCommand(cardAdd);
            }
        }
        private void cardAdd()
        {
            //补卡
            if (string.IsNullOrWhiteSpace(Detail.Card.SecondCardNo))
            {
                Helper.ShowErrorMsg("请输入补卡卡号");
                return;
            }
            if (Detail.Card.CardLevel == Guid.Empty || Detail.Card.CardLevel == null)
            {
                Helper.ShowErrorMsg("请选择卡级别");
                return;
            }

            //DXInfo.Models.aspnet_CustomProfile user = this.Oper;
            //Guid userId = user.UserId;
            if (!ClientCommon.CheckUser(this.Oper)) return;

            var c = Uow.Cards.GetById(g => g.Id == Detail.Card.Id);
            if (!ClientCommon.CheckCard(c)) return;

            var d = Uow.CardLevels.GetById(g => g.Id == Detail.Card.CardLevel);
            if (d == null)
            {
                Helper.ShowErrorMsg("卡级别信息错误");
                return;
            }
            if (!string.IsNullOrEmpty(d.BeginLetter))
            {
                if (!Detail.Card.SecondCardNo.StartsWith(d.BeginLetter))
                {
                    Helper.ShowErrorMsg("卡号必须以" + d.BeginLetter + "字母开头");
                    return;
                }
            }
            var cardType = Uow.CardTypes.GetById(g => g.Id == Detail.Card.CardType);
            if (cardType == null)
            {
                Helper.ShowErrorMsg("卡型信息错误");
                return;
            }
            string strComment;
            string strCardNoRule = ClientCommon.CardNoRule(cardType, out strComment);
            if (!Regex.IsMatch(Detail.Card.SecondCardNo, strCardNoRule))
            {
                if (!string.IsNullOrEmpty(d.BeginLetter))
                {
                    Helper.ShowErrorMsg(strComment + "，且必须以" + d.BeginLetter + "字母开头");
                    return;
                }
                else
                {
                    Helper.ShowErrorMsg(strComment);
                    return;
                }
            }
            var c1 = Uow.Cards.GetAll().Where(w => w.CardNo == Detail.Card.SecondCardNo).FirstOrDefault();
            if (c1 != null)
            {
                Helper.ShowErrorMsg("卡号已存在");
                return;
            }

            StringBuilder sb = new StringBuilder(33);
            sb.Append(Detail.Card.SecondCardNo);
//#if DEBUG
//            int st = 0;
//#else
            int st = CardRef.CoolerPutCard(sb);
//#endif
            if (st != 0)
            {
                MessageBox.Show(CardRef.GetStr(st));
                return;
            }
            int value = Convert.ToInt32(Detail.Card.Balance * 100);
//#if !DEBUG
            st = CardRef.CoolerRechargeCard(sb, value);
//#endif
            //充值
            if (st != 0)
            {
                MessageBox.Show(CardRef.GetStr(st));
                return;
            }
            DateTime dtNow = DateTime.Now;
            using (TransactionScope transaction = new TransactionScope())
            {
                DXInfo.Models.Cards newcard = new DXInfo.Models.Cards();
                newcard.CardNo = Detail.Card.SecondCardNo;
                newcard.CardLevel = Detail.Card.CardLevel;
                newcard.CardType = Detail.Card.CardType;
                newcard.CreateDate = dtNow;
                newcard.UserId = this.Oper.UserId;
                newcard.DeptId = this.Dept.DeptId;
                newcard.Balance = Detail.Card.Balance;
                newcard.Member = Detail.Card.Member;
                newcard.CardPwd = Detail.Card.CardPwd;
                newcard.Comment = c.Comment;                
                Uow.Cards.Add(newcard);
                Uow.Commit();

                DXInfo.Models.CardsLog cardsLog = Mapper.Map<DXInfo.Models.Cards, DXInfo.Models.CardsLog>(newcard);
                cardsLog.CardId = newcard.Id;
                cardsLog.CreateDate = dtNow;
                cardsLog.UserId = this.Oper.UserId;
                cardsLog.DeptId = this.Dept.DeptId;
                Uow.CardsLog.Add(cardsLog);


                c.SecondCardNo = Detail.Card.SecondCardNo;
                c.AddDate = dtNow;
                c.AddDeptId = this.Dept.DeptId;
                c.AddUserId = this.Oper.UserId;
                c.Status = 2;
                Uow.Cards.Update(c);

                DXInfo.Models.CardsLog cardsLog1 = Mapper.Map<DXInfo.Models.Cards, DXInfo.Models.CardsLog>(c);
                cardsLog1.CardId = c.Id;
                cardsLog1.CreateDate = dtNow;
                cardsLog1.UserId = this.Oper.UserId;
                cardsLog1.DeptId = this.Dept.DeptId;
                Uow.CardsLog.Add(cardsLog1);

                DXInfo.Models.Recharges recharge = new DXInfo.Models.Recharges();
                recharge.Amount = Detail.Card.Balance;
                recharge.Balance = Detail.Card.Balance;
                recharge.CreateDate = DateTime.Now;
                recharge.DeptId = this.Dept.DeptId;
                recharge.UserId = this.Oper.UserId;
                recharge.LastBalance = 0;
                recharge.Donate = 0;
                recharge.RechargeType = 1;
                recharge.Card = newcard.Id;
                Uow.Recharges.Add(recharge);

                if (Detail.Cost > 0)
                {
                    recharge = new DXInfo.Models.Recharges();
                    recharge.Amount = Detail.Cost;
                    recharge.Balance = 0;
                    recharge.CreateDate = DateTime.Now;
                    recharge.DeptId = this.Dept.DeptId;
                    recharge.UserId = this.Oper.UserId;
                    recharge.LastBalance = 0;
                    recharge.Donate = 0;
                    recharge.RechargeType = 3;
                    recharge.Card = newcard.Id;

                    Uow.Recharges.Add(recharge);
                }
                var qpt = Uow.CardPoints.GetAll().Where(w => w.Card == Detail.Card.Id);
                if (qpt.Count() > 0)
                {
                    decimal pt = qpt.Sum(s => s.Point);
                    if (pt != 0)
                    {
                        DXInfo.Models.CardPoints cp = new DXInfo.Models.CardPoints();
                        cp.Card = newcard.Id;
                        cp.CreateDate = dtNow;
                        cp.DeptId = this.Dept.DeptId;
                        cp.Point = pt;
                        cp.UserId = this.Oper.UserId;
                        cp.PointType = 1;
                        Uow.CardPoints.Add(cp);

                    }
                }
                Uow.Commit();
                transaction.Complete();
            }
            MessageBox.Show("补卡成功");
            this.query();
            Detail = new CardAddPageDetail();
        }
        #endregion
    }
}
