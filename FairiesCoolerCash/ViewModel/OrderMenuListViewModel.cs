using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
//using System.Data.Objects.SqlClient;
using FairiesCoolerCash.Business;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using System.Data.Entity.SqlServer;
using AutoMapper;

namespace FairiesCoolerCash.ViewModel
{
    /// <summary>
    /// 点菜清单
    /// </summary>
    public class OrderMenuListViewModel : ReportViewModelBase
    {
        private readonly IMapper mapper;
        public OrderMenuListViewModel(IFairiesMemberManageUow uow,IMapper mapper)
            : base(uow,mapper)
        {       
            this.mapper = mapper;
        }
        #region 查询
        protected override void query()
        {
            var q1 = from d1 in Uow.OrderDeskes.GetAll()
                     join d2 in Uow.Desks.GetAll() on d1.DeskId equals d2.Id into dd2
                     from dd2s in dd2.DefaultIfEmpty()

                     join d3 in Uow.OrderDishes.GetAll() on d1.OrderId equals d3.Id into dd3
                     from dd3s in dd3.DefaultIfEmpty()

                     join d4 in Uow.Depts.GetAll() on dd3s.DeptId equals d4.DeptId into dd4
                     from dd4s in dd4.DefaultIfEmpty()

                     join d5 in Uow.aspnet_CustomProfile.GetAll() on dd3s.UserId equals d5.UserId into dd5
                     from dd5s in dd5.DefaultIfEmpty()

                     join d6 in Uow.aspnet_CustomProfile.GetAll() on d1.UserId equals d6.UserId into dd6
                     from dd6s in dd5.DefaultIfEmpty()

                     join d7 in Uow.NameCode.GetAll().Where(w => w.Type == "OrderDishStatus") on SqlFunctions.StringConvert((double?)dd3s.Status).Trim() equals d7.Code into dd7
                     from dd7s in dd7.DefaultIfEmpty()

                     select new
                     {
                         OrderDeskCreateDate = d1.CreateDate,
                         OrderDeskFullName = dd6s.FullName,
                         OrderDeskStatus = d1.Status,
                         OrderId = d1.OrderId,
                         DeskNo = dd2s.Code,
                         OrderDishUserId = dd3s.UserId,
                         OrderDishFullName = dd5s.FullName,
                         OrderDishCreateDate = dd3s.CreateDate,
                         OrderDishStatus = dd3s.Status,
                         OrderDishStatusName = dd7s.Name,
                         DeptName = dd4s.DeptName,
                         dd3s.Quantity
                     };



            var q = from d1 in Uow.OrderMenus.GetAll()
                    join d2 in q1 on d1.OrderId equals d2.OrderId into dd2
                    from dd2s in dd2.DefaultIfEmpty()

                    join d3 in Uow.Inventory.GetAll() on d1.InventoryId equals d3.Id into dd3
                    from dd3s in dd3.DefaultIfEmpty()

                    join d4 in Uow.aspnet_CustomProfile.GetAll() on d1.UserId equals d4.UserId into dd4
                    from dd4s in dd4.DefaultIfEmpty()

                    
                    join d6 in Uow.NameCode.GetAll().Where(w => w.Type == "OrderMenuStatus") on SqlFunctions.StringConvert((double?)d1.Status).Trim() equals d6.Code into dd6
                    from dd6s in dd6.DefaultIfEmpty()

                    where dd2s.OrderId!=null
                    select new
                    {
                        dd2s.DeskNo,
                        dd2s.OrderDishStatus,
                        //OrderDishStatusName=dd5s.Name,
                        dd2s.OrderDishStatusName,
                        dd2s.DeptName,
                        dd2s.OrderId,
                        dd2s.Quantity,
                        dd2s.OrderDishFullName,
                        dd2s.OrderDishCreateDate,
                        dd2s.OrderDeskFullName,
                        dd2s.OrderDeskCreateDate,
                        dd2s.OrderDeskStatus,
                        OrderMenuInvName = dd3s.Name,
                        OrderMenuStatus = d1.Status,
                        OrderMenuStatusName = dd6s.Name,
                        OrderMenuInvPrice = d1.Price,
                        OrderMenuInvQuantity = d1.Quantity,
                        OrderMenuInvAmount = d1.Amount,
                        OrderMenuUserId = d1.UserId,
                        OrderMenuFullName = dd4s.FullName,
                        OrderMenuCreateDate = d1.CreateDate,
                        dd2s.OrderDishUserId,
                    };


            if (this.BeginDate > DateTime.MinValue)
            {
                q = q.Where(w => w.OrderMenuCreateDate >= this.BeginDate);
            }
            if (this.EndDate > DateTime.MinValue)
            {
                DateTime dtEndDate = this.EndDate.AddDays(1);
                q = q.Where(w => w.OrderMenuCreateDate <= dtEndDate);
            }
            if (this.SelectedOrderDishStatus != null)
            {
                q = q.Where(w => w.OrderDishStatus == this.SelectedOrderDishStatus.Id);
            }
            if (this.SelectedOper != null)
            {
                //Guid userid = Guid.Parse(cmbOper.SelectedValue.ToString());
                q = q.Where(w => w.OrderMenuUserId == this.SelectedOper.UserId);
            }
            if (this.SelectedOrderMenuStatus != null)
            {
                q = q.Where(w => w.OrderMenuStatus == this.SelectedOrderMenuStatus.Id);
            }
            if (!string.IsNullOrWhiteSpace(this.DeskNo))
            {
                q = q.Where(w => w.DeskNo.Contains(this.DeskNo));
            }
            //var q2 = q.ToList();
            q = q.OrderBy(o => o.OrderMenuCreateDate);
            this.MyQuery = q;
            //List<DXInfo.Models.OrderDishStatusHelper> lOrderDishStatus;
            //OrderDishStatusHelper.GetOrderDishStatus(out lOrderDishStatus);
            //List<DXInfo.Models.OrderMenuStatus> lOrderMenuStatus;
            //OrderMenuStatus.GetOrderMenuStatus(out lOrderMenuStatus);

            //var q3 = from d in q2
            //         join d1 in lOrderDishStatus on d.OrderDishStatus equals d1.Id into dd1
            //         from dd1s in dd1.DefaultIfEmpty()
            //         join d2 in lOrderMenuStatus on d.OrderMenuStatus equals d2.Id into dd2
            //         from dd2s in dd2.DefaultIfEmpty()
            //         select new
            //         {
            //             d.DeptName,
            //             d.OrderId,
            //             d.Quantity,
            //             OrderDishStatus = dd1s.Name,
            //             d.OrderDishFullName,
            //             d.OrderDishCreateDate,

            //             d.DeskNo,
            //             d.OrderDeskFullName,
            //             d.OrderDeskCreateDate,

            //             d.OrderMenuInvName,
            //             OrderMenuStatus = dd2s.Name,
            //             d.OrderMenuInvPrice,
            //             d.OrderMenuInvQuantity,
            //             d.OrderMenuInvAmount,
            //             d.OrderMenuFullName,
            //             d.OrderMenuCreateDate
            //         };
            //OrderBookList.ItemsSource = q3.ToList();
        }
        #endregion
    }
}
