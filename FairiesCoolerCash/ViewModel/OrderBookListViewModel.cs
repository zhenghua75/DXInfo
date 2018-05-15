using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using DXInfo.Models;
//using System.Data.Objects.SqlClient;
using System.Windows.Controls;
using FairiesCoolerCash.Business;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Data.Entity.SqlServer;
namespace FairiesCoolerCash.ViewModel
{
    /// <summary>
    /// 预定清单
    /// </summary>
    public class OrderBookListViewModel:ReportViewModelBase
    {
        public OrderBookListViewModel(IFairiesMemberManageUow uow)
            : base(uow)
        {
        }

        #region 查询
        protected override void query()
        {

            var q1 = from d in Uow.OrderBooks.GetAll()

                     join d1 in Uow.aspnet_CustomProfile.GetAll() on d.UserId equals d1.UserId into dd1
                     from dd1s in dd1.DefaultIfEmpty()

                     join d2 in Uow.Depts.GetAll() on d.DeptId equals d2.DeptId into dd2
                     from dd2s in dd2.DefaultIfEmpty()
                     select new
                     {
                         d.Id,
                         d.BookBeginDate,
                         d.BookEndDate,
                         d.Comment,
                         d.CreateDate,
                         d.Customer,
                         d.LinkPhone,
                         d.Quantity,
                         d.Status,
                         dd1s.FullName,
                         dd2s.DeptName
                     };

            
            var q = from d in Uow.OrderBookDeskes.GetAll()
                    join d1 in q1 on d.OrderBookId equals d1.Id into dd1
                    from dd1s in dd1.DefaultIfEmpty()

                    join d2 in Uow.Desks.GetAll() on d.DeskId equals d2.Id into dd2
                    from dd2s in dd2.DefaultIfEmpty()

                    join d3 in Uow.aspnet_CustomProfile.GetAll() on d.UserId equals d3.UserId into dd3
                    from dd3s in dd3.DefaultIfEmpty()

                    join n in Uow.NameCode.GetAll().Where(w => w.Type == "OrderBookStatus") on SqlFunctions.StringConvert((double?)dd1s.Status).Trim() equals n.Code into dd4
                    from dd4s in dd4.DefaultIfEmpty()

                    join n in Uow.NameCode.GetAll().Where(w => w.Type == "OrderBookDeskStatus") on SqlFunctions.StringConvert((double?)d.Status).Trim() equals n.Code into dd5
                    from dd5s in dd5.DefaultIfEmpty()
                    select new
                    {
                        dd1s.Id,
                        dd1s.BookBeginDate,
                        dd1s.BookEndDate,
                        dd1s.Comment,
                        dd1s.CreateDate,
                        dd1s.Customer,
                        dd1s.LinkPhone,
                        dd1s.Quantity,
                        dd1s.Status,
                        StatusName=dd4s.Name,
                        dd1s.FullName,
                        dd1s.DeptName,
                        d.DeskId,
                        DeskNo = dd2s.Code,
                        DeskFullName = dd3s.FullName,
                        DeskCreateDate = d.CreateDate,
                        DeskStatus = d.Status,
                        DeskStatusName=dd5s.Name,
                        OrderBookDeskId = d.Id,
                    };
            if (!string.IsNullOrWhiteSpace(this.Customer))
                q = q.Where(w => w.Customer.Contains(this.Customer));
            if (!string.IsNullOrWhiteSpace(this.LinkPhone))
                q = q.Where(w => w.LinkPhone.Contains(this.LinkPhone));
            if (this.BeginDate>DateTime.MinValue)
            {
                q = q.Where(w => w.CreateDate >= this.BeginDate);
            }
            if (this.EndDate>DateTime.MinValue)
            {
                q = q.Where(w => w.CreateDate <= this.EndDate);
            }
            if (this.SelectedOrderBookStatus != null)
            {
                q = q.Where(w => w.Status == this.SelectedOrderBookStatus.Id);
            }
            if (!string.IsNullOrWhiteSpace(this.DeskNo))
            {
                q = q.Where(w => w.DeskNo.Contains(this.DeskNo));
            }
            q = q.OrderBy(o => o.CreateDate);
            this.MyQuery = q;
        }
        #endregion

        #region 修改预定信息
        private void ModifyBookExecute(object sender)
        {
            Button btn = (Button)sender;
            Guid orderBookId = Guid.Parse(btn.Tag.ToString());

            bool ret = ClientCommon.OrderBook_ModifyBook(orderBookId);
            if (ret)
            {
                Helper.ShowSuccMsg("修改预订信息成功");
                query();
            }
        }
        public ICommand ModifyBook
        {
            get
            {
                return new RelayCommand<object>(ModifyBookExecute);
            }
        }
        #endregion

        #region 取消预定
        private void CancelBookExecute(object sender)
        {
            Button btn = (Button)sender;
            Guid orderBookId = Guid.Parse(btn.Tag.ToString());
            try
            {
                DXInfo.Restaurant.DeskManageFacade dmf = new DXInfo.Restaurant.DeskManageFacade(Uow, Dept.DeptId, User.UserId);
                dmf.dtOperDate = DateTime.Now;
                dmf.CancelBook(orderBookId);
                MessageBox.Show("取消预定成功");
                query();
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMsg(ex.Message);
                Helper.HandelException(ex);
            }
        }
        public ICommand CancelBook
        {
            get
            {
                return new RelayCommand<object>(CancelBookExecute);
            }
        }
        #endregion

        #region 开台
        private void OpenExecute(object sender)
        {
            //this.SelectedResult
            Button btn = (Button)sender;
            Guid orderId = Guid.Parse(btn.Tag.ToString());
            try
            {
                dynamic d = this.SelectedResult;
                Guid deskId = d.DeskId;
                int quantity = d.Quantity;

                DXInfo.Restaurant.DeskManageFacade dmf = new DXInfo.Restaurant.DeskManageFacade(Uow, Dept.DeptId, User.UserId);
                dmf.dtOperDate = DateTime.Now;
                DXInfo.Models.OrderDishes orderDish = new OrderDishes();
                DXInfo.Models.OrderDeskes orderDesk = new OrderDeskes();
                dmf.OpenBook(orderId, deskId, quantity, false,
                     ref orderDish, ref orderDesk);  
                MessageBox.Show("预订开台成功");
                query();
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMsg(ex.Message);
                Helper.HandelException(ex);
            }
        }
        public ICommand Open
        {
            get
            {
                return new RelayCommand<object>(OpenExecute);
            }
        }
        #endregion
    }
}
