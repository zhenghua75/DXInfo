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
using Microsoft.Practices.ServiceLocation;
using AutoMapper;

namespace FairiesCoolerCash.ViewModel
{
    #region 添加
    /// <summary>
    /// 添加单据
    /// </summary>
    public class ReceiptAddViewModel:BusinessViewModelBase
    {
        
        public ReceiptAddViewModel(IFairiesMemberManageUow uow)
            : base(uow, new List<string>() { "MemberName","Content","LinkPhone"})
        {
            this.InitObject();
        }
        protected virtual void InitObject()
        {
            this.Member = new DXInfo.Models.Members();
            this.Receipt = new DXInfo.Models.Receipts();
            this.MemberName = null;
            this.Content = null;
            this.LinkPhone = null;
        }
        #region 添加单据
        
        private void OperateExecute()
        {
            DateTime dtNow = DateTime.Now;
            this.Member.MemberName = this.MemberName;
            this.Member.UserId = this.Oper.UserId;
            this.Member.CreateDate = dtNow;
            this.Member.DeptId = this.Dept.DeptId;
            this.Member.LinkPhone = this.LinkPhone;
            this.Member.MemberType = DXInfo.Models.MemberType.Receipt;
            this.Receipt.Content = this.Content;
            this.Receipt.UserId = this.Oper.UserId;
            this.Receipt.CreateDate = dtNow;
            this.Receipt.DeptId = this.Dept.DeptId;
            this.Receipt.ReceiptType = this.ReceiptType;

            using (TransactionScope transaction = new TransactionScope())
            {
                Uow.Members.Add(this.Member);
                Uow.Commit();

                DXInfo.Models.MembersLog memberLog = Mapper.Map<DXInfo.Models.Members, DXInfo.Models.MembersLog>(Member);
                memberLog.MemberId = Member.Id;
                Uow.MembersLog.Add(memberLog);

                this.Receipt.Member = this.Member.Id;
                Uow.Receipts.Add(this.Receipt);
                Uow.Commit();

                DXInfo.Models.ReceiptHis receiptHis = Mapper.Map<DXInfo.Models.ReceiptHis>(this.Receipt);
                receiptHis.LinkId = this.Receipt.Id;
                Uow.ReceiptHis.Add(receiptHis);
                Uow.Commit();
                transaction.Complete();
            }
            Helper.ShowSuccMsg("添加单据成功。");

            this.InitObject();
        }
        public ICommand Operate
        {
            get
            {
                return new RelayCommand(OperateExecute, OperateCanExecute);
            }
        }
        private bool OperateCanExecute()
        {
            return this.IsValid;
        }
        #endregion       
    }    
    /// <summary>
    /// 添加订货单
    /// </summary>
    public class OrderAddViewModel : ReceiptAddViewModel
    {
        public OrderAddViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
            this.ReceiptType = (int)DXInfo.Models.ReceiptType.Order;
            this.Title = "添加订货单";
        }
    }
    /// <summary>
    /// 添加返修单
    /// </summary>
    public class ReworkAddViewModel : ReceiptAddViewModel
    {
        public ReworkAddViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
            this.ReceiptType = (int)DXInfo.Models.ReceiptType.Rework;
            this.Title = "添加返修单";
        }
    }
    #endregion

    #region 查询
    public class ReceiptQueryViewModel : ReportViewModelBase
    {
        public ReceiptQueryViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
        }
        #region 查询
        protected override void query()
        {
            //查询
            var ms =
                     from d in Uow.Receipts.GetAll()

                     join d1 in Uow.Members.GetAll() on d.Member equals d1.Id into dd1
                     from dd1s in dd1.DefaultIfEmpty()

                     join d2 in Uow.aspnet_CustomProfile.GetAll() on d.UserId equals d2.UserId into dd2
                     from dd2s in dd2.DefaultIfEmpty()

                     join d3 in Uow.aspnet_CustomProfile.GetAll() on d.ModifyUserId equals d3.UserId into dd3
                     from dd3s in dd3.DefaultIfEmpty()

                     join d4 in Uow.Depts.GetAll() on d.DeptId equals d4.DeptId into dd4
                     from dd4s in dd4.DefaultIfEmpty()

                     join d5 in Uow.Depts.GetAll() on d.ModifyDeptId equals d5.DeptId into dd5
                     from dd5s in dd5.DefaultIfEmpty()

                     where dd1s.MemberType == DXInfo.Models.MemberType.Receipt && d.ReceiptType == this.ReceiptType
                     orderby d.CreateDate
                     select new
                     {
                         d.Id,
                         d.Status,
                         d.ReceiptType,
                         d.Content,
                         d.Comment,
                         d.CreateDate,
                         dd2s.FullName,
                         dd4s.DeptName,
                         ModifyDeptName = dd5s.DeptName,
                         ModifyFullName = dd3s.FullName,
                         MemberId=dd1s.Id,
                         dd1s.Email,
                         dd1s.IdCard,
                         dd1s.LinkAddress,
                         dd1s.LinkPhone,
                         dd1s.MemberName,
                         dd1s.Birthday,
                         dd1s.Sex,
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
                ms = ms.Where(w => w.Comment.Contains(this.Comments));

            this.MyQuery = ms;
        }

        #endregion

        #region 编辑
        private void EditResultExecute()
        {
            if (this.SelectedResult != null)
            {
                dynamic d = this.SelectedResult;
                ReceiptUserControl ruc = new ReceiptUserControl();
                int receiptType = d.ReceiptType;
                int receiptStatus = d.Status;
                if (receiptStatus == (int)DXInfo.Models.ReceiptStatus.Complete)
                {
                    Helper.ShowSuccMsg("单据已完成，不能修改");
                    return;
                }
                Guid memberId = d.MemberId;
                Guid receiptId = d.Id;
                switch (receiptType)
                {
                    case (int)DXInfo.Models.ReceiptType.Order:
                        ruc.DataContext = new OrderModifyViewModel(Uow, memberId, receiptId);
                        break;
                    case (int)DXInfo.Models.ReceiptType.Rework:
                        ruc.DataContext = new ReworkModifyViewModel(Uow, memberId, receiptId);
                        break;
                }
                this.NavigationUserControl(ruc);
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
    public class OrderQueryViewModel : ReceiptQueryViewModel
    {
        public OrderQueryViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
            this.Title = "订货单查询修改";
            this.ReceiptType = (int)DXInfo.Models.ReceiptType.Order;
        }
    }
    public class ReworkQueryViewModel : ReceiptQueryViewModel
    {
        public ReworkQueryViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
            this.Title = "返修单查询修改";
            this.ReceiptType = (int)DXInfo.Models.ReceiptType.Rework;
        }
    }
    #endregion

    #region 修改
    public class ReceiptModifyViewModel : BusinessViewModelBase
    {
        public ReceiptModifyViewModel(IFairiesMemberManageUow uow, Guid memberId, Guid receiptId)
            : base(uow, new List<string>() { "MemberName", "Content", "LinkPhone" })
        {
            this.Member = uow.Members.GetById(g => g.Id == memberId);
            this.Receipt = uow.Receipts.GetById(g => g.Id == receiptId);
            this.MemberName = this.Member.MemberName;
            this.Content = this.Receipt.Content;
            this.LinkPhone = this.Member.LinkPhone;
        }
        public override void Cleanup()
        {
            base.Cleanup();
            this.Member = null;
            this.Receipt = null;
            this.Content = "";
            this.MemberName = "";
            this.LinkPhone = "";
        }
        public ICommand Operate
        {
            get
            {
                return new RelayCommand(OperateExecute, OperateCanExecute);
            }
        }
        private bool OperateCanExecute()
        {
            return this.IsValid;
        }
        private void OperateExecute()
        {
            DateTime dtNow = DateTime.Now;
            this.Member.MemberName = this.MemberName;
            this.Member.LinkPhone = this.LinkPhone;
            this.Member.ModifyDate = dtNow;
            this.Member.ModifyDeptId = this.Dept.DeptId;
            this.Member.ModifyUserId = this.Oper.UserId;
            Uow.Members.Update(this.Member);

            DXInfo.Models.MembersLog memberLog = Mapper.Map<DXInfo.Models.Members, DXInfo.Models.MembersLog>(Member);
            memberLog.MemberId = Member.Id;
            Uow.MembersLog.Add(memberLog);

            this.Receipt.Content = this.Content;
            this.Receipt.ModifyDate = dtNow;
            this.Receipt.ModifyDeptId = this.Dept.DeptId;
            this.Receipt.ModifyUserId = this.Oper.UserId;
            Uow.Receipts.Update(this.Receipt);

            DXInfo.Models.ReceiptHis receiptHis = Mapper.Map<DXInfo.Models.ReceiptHis>(this.Receipt);
            receiptHis.LinkId = this.Receipt.Id;
            Uow.ReceiptHis.Add(receiptHis);

            Uow.Commit();
            Helper.ShowSuccMsg("修改单据成功");
        }
    }

    public class OrderModifyViewModel : ReceiptModifyViewModel
    {
        public OrderModifyViewModel(IFairiesMemberManageUow uow, Guid memberId, Guid receiptId)
            : base(uow, memberId,receiptId)
        {
            this.Title = "修改订货单";
            this.ReceiptType = (int)DXInfo.Models.ReceiptType.Order;
        }
    }
    public class ReworkModifyViewModel : ReceiptModifyViewModel
    {
        public ReworkModifyViewModel(IFairiesMemberManageUow uow, Guid memberId, Guid receiptId)
            : base(uow, memberId, receiptId)
        {
            this.Title = "修改返修单";
            this.ReceiptType = (int)DXInfo.Models.ReceiptType.Rework;
        }
    }
    #endregion
}
