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
using AutoMapper;

namespace FairiesCoolerCash.ViewModel
{
    /// <summary>
    /// 缺菜清单
    /// </summary>
    public class LackMenuListViewModel : ReportViewModelBase
    {
        private DXInfo.Restaurant.DeskManageFacade dmf;
        private readonly IMapper mapper;
        public LackMenuListViewModel(IFairiesMemberManageUow uow, IMapper mapper)
            : base(uow,mapper)
        {
            this.mapper = mapper;
            dmf = new DXInfo.Restaurant.DeskManageFacade(Uow,mapper, this.Dept.DeptId, this.Oper.UserId);
            this.SelectedOrderMenuStatus = this.lOrderMenuStatus.FirstOrDefault(f => f.Id == (int)DXInfo.Models.OrderMenuStatus.Lack);
        }
        #region 查询
        protected override void query()
        {
            var invs = from d in Uow.Inventory.GetAll().Where(w=>!w.IsInvalid &&
                       w.InvType == (int)DXInfo.Models.InvType.WesternRestaurant)

                       join d1 in Uow.InvDepts.GetAll() on d.Id equals d1.Inv into dd1
                       from dd1s in dd1.DefaultIfEmpty()

                       join d2 in Uow.MenuStatus.GetAll().Where(w=>w.Dept == this.Dept.DeptId &&
                           w.Status==(int)DXInfo.Models.OrderMenuStatus.Lack
                       ) on d.Id equals d2.Inventory into dd2
                       from dd2s in dd2.DefaultIfEmpty()

                       orderby d.Code
                       where dd1s.Dept == this.Dept.DeptId
                       select new
                       {
                           d.Id,
                           d.Code,
                           d.Name,
                           Status = dd2s == null ? 0 : dd2s.Status,
                       };
            if (!string.IsNullOrEmpty(this.Code))
            {
                invs = invs.Where(w => w.Code.Contains(this.Code));
            }
            if (!string.IsNullOrEmpty(this.Name))
            {
                invs = invs.Where(w => w.Name.Contains(this.Name));
            }
            if (this.SelectedOrderMenuStatus!=null)
            {
                invs = invs.Where(w => w.Status == this.SelectedOrderMenuStatus.Id);
            }
            this.MyQuery = invs;
        }
        #endregion

        #region 缺菜
        private void LackMenuExecute()
        {
            dynamic d = this.SelectedResult;                 
            Guid invId = d.Id;
            dmf.LackMenu(invId);
            Helper.ShowSuccMsg("设置缺菜成功");
            this.query();
        }
        private bool LackMenuCanExecute()
        {
            if (this.SelectedResult != null)
            {
                dynamic d = this.SelectedResult;
                int status = d.Status;
                if (status == (int)DXInfo.Models.OrderMenuStatus.Normal)
                {
                    return true;
                }
            }
            return false;
        }
        public ICommand LackMenu
        {
            get
            {
                return new RelayCommand(LackMenuExecute, LackMenuCanExecute);
            }
        }
        #endregion

        #region 取消缺菜
        private void CancelLackMenuExecute()
        {
            dynamic d = this.SelectedResult;
            Guid invId = d.Id;
            dmf.CancelLackMenu(invId);
            Helper.ShowSuccMsg("取消缺菜成功");
            this.query();
        }
        private bool CancelLackMenuCanExecute()
        {
            if (this.SelectedResult != null)
            {
                dynamic d = this.SelectedResult;
                int status = d.Status;
                if (status == (int)DXInfo.Models.OrderMenuStatus.Lack)
                {
                    return true;
                }
            }
            return false;
        }
        public ICommand CancelLackMenu
        {
            get
            {
                return new RelayCommand(CancelLackMenuExecute, CancelLackMenuCanExecute);
            }
        }
        #endregion
    }
}
