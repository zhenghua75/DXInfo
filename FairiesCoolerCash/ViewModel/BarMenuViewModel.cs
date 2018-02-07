using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DXInfo.Data.Contracts;
using System.ComponentModel;
using FairiesCoolerCash.Business;
using Microsoft.Practices.ServiceLocation;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Data.SqlClient;

namespace FairiesCoolerCash.ViewModel
{
    /// <summary>
    /// 吧台管理
    /// </summary>
    public class BarMenuViewModel:BusinessViewModelBase
    {
        #region 字段
        private DispatcherTimer RefreshTimer;
        private DXInfo.Restaurant.DeskManageFacade dmf;
        private void SetTimer()
        {
            this.RefreshTimer = new DispatcherTimer();
            this.RefreshTimer.Interval = TimeSpan.FromSeconds(60.0);
            this.RefreshTimer.Tick += new EventHandler(this.RefreshTimerCallback);
            this.RefreshTimer.Start();
        }
        private void RefreshTimerCallback(object obj, EventArgs arg)
        {
            lock (lockObject)
            {
                refresh();
            }
        }
        object lockObject= new object();
        #endregion

        public BarMenuViewModel(IFairiesMemberManageUow uow)
            : base(uow,new List<string>())
        {
            this.SetTimer();
            this.SectionType = (int)DXInfo.Models.SectionType.Bar;
            dmf = new DXInfo.Restaurant.DeskManageFacade(uow,Dept.DeptId,User.UserId);
        }
        public override void Cleanup()
        {
            base.Cleanup();
            if (this.RefreshTimer != null)
            {
                this.RefreshTimer.Tick -= new EventHandler(this.RefreshTimerCallback);
                this.RefreshTimer = null;
            }
            //if (dmf != null)
            //{
            //    dmf.Dispose();
            //    dmf = null;
            //}
        }

        #region 未完成-选择的
        public DXInfo.Models.MenuInfo _SelectedMenuInfo;
        public virtual DXInfo.Models.MenuInfo SelectedMenuInfo
        {
            get
            {
                return _SelectedMenuInfo;
            }
            set
            {
                _SelectedMenuInfo = value;
                this.RaisePropertyChanged("SelectedMenuInfo");
                if (_SelectedMenuInfo != null && _SelectedMenuInfo.deskes != null && _SelectedMenuInfo.deskes.Count > 0)
                {
                    _SelectedMenuInfo.SelectedDesk = _SelectedMenuInfo.deskes[0];
                }
            }
        }
        #endregion

        #region 已完成-选择的
        public DXInfo.Models.MenuInfo _SelectedMenuInfoComplete;
        public virtual DXInfo.Models.MenuInfo SelectedMenuInfoComplete
        {
            get
            {
                return _SelectedMenuInfoComplete;
            }
            set
            {
                _SelectedMenuInfoComplete = value;
                this.RaisePropertyChanged("SelectedMenuInfoComplete");
                if (_SelectedMenuInfoComplete.deskes != null && _SelectedMenuInfoComplete.deskes.Count > 0)
                {
                    _SelectedMenuInfoComplete.SelectedDesk = _SelectedMenuInfoComplete.deskes[0];
                }
            }
        }
        #endregion

        #region 未完成
        private ObservableCollection<DXInfo.Models.MenuInfo> _OCNoCtMenuInfo;
        public ObservableCollection<DXInfo.Models.MenuInfo> OCNoCtMenuInfo
        {
            get
            {
                return _OCNoCtMenuInfo;
            }
            set
            {
                _OCNoCtMenuInfo = value;
                this.RaisePropertyChanged("OCNoCtMenuInfo");
            }
        }
        #endregion

        #region 已完成
        private ObservableCollection<DXInfo.Models.MenuInfo> _OCCtMenuInfo;
        public ObservableCollection<DXInfo.Models.MenuInfo> OCCtMenuInfo
        {
            get
            {
                return _OCCtMenuInfo;
            }
            set
            {
                _OCCtMenuInfo = value;
                this.RaisePropertyChanged("OCCtMenuInfo");
            }
        }
        #endregion

        #region 当前
        public DXInfo.Models.Info _CurrentInfo;
        public virtual DXInfo.Models.Info CurrentInfo
        {
            get
            {
                return _CurrentInfo;
            }
            set
            {
                _CurrentInfo = value;
                this.RaisePropertyChanged("CurrentInfo");
            }
        }
        #endregion

        #region 更新
        private void UpdateOrderMenuData()
        {
            IEnumerable<DXInfo.Models.MenuInfo> iMenuInfo = Uow.Db.SqlQuery<DXInfo.Models.MenuInfo>("sp_DXInfo_UpdateOrderMenuData @DeptType",
                new SqlParameter("DeptType", this.SectionType));
            List<DXInfo.Models.MenuInfo> lMenuInfo = iMenuInfo.ToList();

            IEnumerable<DXInfo.Models.MenuDeskInfo> iMenuDeskInfo = Uow.Db.SqlQuery<DXInfo.Models.MenuDeskInfo>("select * from vw_MenuDeskInfo");
            List<DXInfo.Models.MenuDeskInfo> lMenuDeskInfo = iMenuDeskInfo.ToList().OrderBy(o => o.DeskCode).ToList(); ;

            foreach (DXInfo.Models.MenuInfo mi in lMenuInfo)
            {
                List<DXInfo.Models.MenuDeskInfo> lMenuDeskInfoSub = lMenuDeskInfo.Where(w => w.OrderId == mi.OrderId).ToList();
                mi.deskes = new ObservableCollection<DXInfo.Models.MenuDeskInfo>(lMenuDeskInfoSub);
                string deskCodes = "";
                foreach (DXInfo.Models.MenuDeskInfo od in lMenuDeskInfoSub)
                {
                    deskCodes += od.DeskCode + ",";
                }
                mi.DeskCodes = deskCodes.Substring(0, deskCodes.Length - 1);
            }
            if (this.OCNoCtMenuInfo != null)
            {
                this.OCNoCtMenuInfo.Clear();
                this.OCNoCtMenuInfo = null;
            }
            List<DXInfo.Models.MenuInfo> lmi = lMenuInfo.OrderBy(o => o.Sort).ToList();
            this.OCNoCtMenuInfo = new ObservableCollection<DXInfo.Models.MenuInfo>(lmi);
        //}
        //private void UpdateOrderMenuDataComplete()
        //{
            //已出菜单
            IEnumerable<DXInfo.Models.MenuInfo> iMenuInfoComplete = Uow.Db.SqlQuery<DXInfo.Models.MenuInfo>("sp_DXInfo_UpdateOrderMenuDataComplete @DeptType",
                new SqlParameter("DeptType", this.SectionType));
            List<DXInfo.Models.MenuInfo> lMenuInfoComplete = iMenuInfoComplete.ToList();

            //IEnumerable<DXInfo.Models.MenuDeskInfo> iMenuDeskInfo = Uow.Db.SqlQuery<DXInfo.Models.MenuDeskInfo>("select * from vw_MenuDeskInfo");
            //List<DXInfo.Models.MenuDeskInfo> lMenuDeskInfo = iMenuDeskInfo.ToList().OrderBy(o=>o.DeskCode).ToList();

            foreach (DXInfo.Models.MenuInfo mi in lMenuInfoComplete)
            {
                List<DXInfo.Models.MenuDeskInfo> lMenuDeskInfoSub = lMenuDeskInfo.Where(w => w.OrderId == mi.OrderId).ToList();
                mi.deskes = new ObservableCollection<DXInfo.Models.MenuDeskInfo>(lMenuDeskInfoSub);
                string deskCodes = "";
                foreach (DXInfo.Models.MenuDeskInfo od in lMenuDeskInfoSub)
                {
                    deskCodes += od.DeskCode + ",";
                }
                mi.DeskCodes = deskCodes.Substring(0, deskCodes.Length - 1);
            }
            if (this.OCCtMenuInfo != null)
            {
                this.OCCtMenuInfo.Clear();
                this.OCCtMenuInfo = null;
            }
            this.OCCtMenuInfo = new ObservableCollection<DXInfo.Models.MenuInfo>(lMenuInfoComplete);
        }
        private void UpdateInfoData()
        {
            IEnumerable<DXInfo.Models.OrderMenuQuantityInfo> iQuantityInfo = Uow.Db.SqlQuery<DXInfo.Models.OrderMenuQuantityInfo>("select * from vw_UpdateInfoData");
            List<DXInfo.Models.OrderMenuQuantityInfo> lQuantityInfo = iQuantityInfo.ToList(); ;
            //DateTime dtNow = DateTime.Now;
            
            //List<DXInfo.Models.OrderMenuQuantityInfo> lQuantityInfo = (from d in Uow.OrderMenus.GetAll()
            //         join d1 in Uow.OrderDishes.GetAll() on d.OrderId equals d1.Id into dd1
            //         from dd1s in dd1.DefaultIfEmpty()
            //         where (dd1s.Status == 0 || dd1s.Status == 3)
            //         && d.Status > 0 && d.Status != 8 
            //         select new DXInfo.Models.OrderMenuQuantityInfo()
            //         {
            //             BillQuantity = d.BillQuantity,
            //             MenuQuantity = d.MenuQuantity,
            //             MissQuantity = d.MissQuantity,
            //             Quantity = d.Quantity,
            //         }).ToList();
            
            if (this.CurrentInfo == null) this.CurrentInfo = new DXInfo.Models.Info();

            this.CurrentInfo.MenuQuantity = Convert.ToInt32(lQuantityInfo.Sum(s => s.MenuQuantity));
            this.CurrentInfo.MissQuantity = Convert.ToInt32(lQuantityInfo.Sum(s => s.MissQuantity));
            this.CurrentInfo.NoMenuQuantity = Convert.ToInt32(lQuantityInfo.Sum(s => (s.Quantity - s.MenuQuantity-s.MissQuantity)));
            this.CurrentInfo.Quantity = Convert.ToInt32(lQuantityInfo.Sum(s => s.Quantity));
        }

        private void refresh()
        {
            UpdateOrderMenuData();
            //UpdateOrderMenuDataComplete();
            UpdateInfoData();
        }
        public ICommand Refresh
        {
            get
            {
                return new RelayCommand(refresh);
            }
        }
        #endregion

        #region 取消出菜
        private void cancelOutMenu(Guid orderMenuId,DXInfo.Models.MenuDeskInfo deskInfo,
            string printer,string invName,string comment)
        {
            try
            {
                dmf.dtOperDate = DateTime.Now;
                dmf.CancelOutMenu(orderMenuId, deskInfo.DeskId);

                MenuPrintObject mpo = new MenuPrintObject(printer,
                    invName, comment, deskInfo.DeskCode + "(取消出菜)",
                    Oper.FullName, dmf.dtOperDate);
                mpo.Print();                
            }
            catch (Exception ex)
            {
                Helper.ShowErrorMsg(ex.Message);
                Helper.HandelException(ex);
            }
            UpdateOrderMenuData();
            //UpdateOrderMenuDataComplete();
            UpdateInfoData();
        }
        #endregion

        #region 未完成-出菜
        private void NoCtOutMenuExecute()
        {
            if (this.SelectedMenuInfo != null && this.SelectedMenuInfo.SelectedDesk != null)
            {
                try
                {
                    dmf.dtOperDate = DateTime.Now;
                    dmf.OutMenu(this.SelectedMenuInfo.OrderMenuId, this.SelectedMenuInfo.SelectedDesk.DeskId);

                    MenuPrintObject mpo = new MenuPrintObject(this.SelectedMenuInfo.Printer,
                        this.SelectedMenuInfo.InvName, this.SelectedMenuInfo.Comment, this.SelectedMenuInfo.SelectedDesk.DeskCode + "(出菜)",
                        Oper.FullName, dmf.dtOperDate);
                    mpo.Print();                    
                }
                catch (Exception ex)
                {
                    Helper.ShowErrorMsg(ex.Message);
                    Helper.HandelException(ex);
                }
                UpdateOrderMenuData();
                //UpdateOrderMenuDataComplete();
                UpdateInfoData();
            }
        }
        private bool NoCtOutMenuCanExecute()
        {
            if (this.SelectedMenuInfo != null && this.SelectedMenuInfo.SelectedDesk != null)
                return true;
            return false;
        }
        public ICommand NoCtOutMenu
        {
            get
            {
                return new RelayCommand(NoCtOutMenuExecute, NoCtOutMenuCanExecute);
            }
        }
        #endregion

        #region 未完成-取消出菜
        private void NoCtCancelOutMenuExecute()
        {
            if (this.SelectedMenuInfo != null && this.SelectedMenuInfo.SelectedDesk != null)
            {
                cancelOutMenu(this.SelectedMenuInfo.OrderMenuId,
                    this.SelectedMenuInfo.SelectedDesk,
                    this.SelectedMenuInfo.Printer,
                    this.SelectedMenuInfo.InvName,
                    this.SelectedMenuInfo.Comment);
            }
        }
        private bool NoCtCancelOutMenuCanExecute()
        {
            if (this.SelectedMenuInfo != null && 
                this.SelectedMenuInfo.SelectedDesk != null&&
                this.SelectedMenuInfo.MenuQuantity>0)
                return true;
            return false;
        }
        public ICommand NoCtCancelOutMenu
        {
            get
            {
                return new RelayCommand(NoCtCancelOutMenuExecute, NoCtCancelOutMenuCanExecute);
            }
        }
        #endregion

        #region 未完成-分单
        private void NoCtSubMenuExecute()
        {
            if (this.SelectedMenuInfo != null)
            {
                try
                {
                    dmf.dtOperDate = DateTime.Now;
                    dmf.BillMenu(this.SelectedMenuInfo.OrderMenuId);
                    UpdateOrderMenuData();
                    UpdateInfoData();
                }
                catch (Exception ex)
                {
                    Helper.ShowErrorMsg(ex.Message);
                    Helper.HandelException(ex);
                }
            }
        }
        private bool NoCtSubMenuCanExecute()
        {
            if (this.SelectedMenuInfo != null &&
                this.SelectedMenuInfo.Quantity > this.SelectedMenuInfo.BillQuantity)
                return true;
            return false;
        }
        public ICommand NoCtSubMenu
        {
            get
            {
                return new RelayCommand(NoCtSubMenuExecute, NoCtSubMenuCanExecute);
            }
        }
        #endregion

        #region 未完成-取消分单
        private void NoCtCancelSubMenuExecute()
        {
            if (this.SelectedMenuInfo != null)
            {
                try
                {
                    dmf.dtOperDate = DateTime.Now;
                    dmf.CancelBillMenu(this.SelectedMenuInfo.OrderMenuId);
                    UpdateOrderMenuData();
                    UpdateInfoData();
                }
                catch (Exception ex)
                {
                    Helper.ShowErrorMsg(ex.Message);
                    Helper.HandelException(ex);
                }
            }
        }
        private bool NoCtCancelSubMenuCanExecute()
        {
            if (this.SelectedMenuInfo != null&&
                this.SelectedMenuInfo.BillQuantity>0)
                return true;
            return false;
        }
        public ICommand NoCtCancelSubMenu
        {
            get
            {
                return new RelayCommand(NoCtCancelSubMenuExecute, NoCtCancelSubMenuCanExecute);
            }
        }
        #endregion

        #region 未完成-缺菜
        private void NoCtLackMenuExecute()
        {
            if (this.SelectedMenuInfo != null)
            {
                try
                {
                    dmf.dtOperDate = DateTime.Now;
                    dmf.MissMenu(this.SelectedMenuInfo.OrderMenuId);
                    UpdateOrderMenuData();
                    //UpdateOrderMenuDataComplete();
                    UpdateInfoData();
                }
                catch (Exception ex)
                {
                    Helper.ShowErrorMsg(ex.Message);
                    Helper.HandelException(ex);
                }
            }
        }
        private bool NoCtLackMenuCanExecute()
        {
            if (this.SelectedMenuInfo != null)
                return true;
            return false;
        }
        public ICommand NoCtLackMenu
        {
            get
            {
                return new RelayCommand(NoCtLackMenuExecute, NoCtLackMenuCanExecute);
            }
        }
        #endregion

        #region 未完成-取消缺菜
        private void NoCtCancelLackMenuExecute()
        {
            if (this.SelectedMenuInfo != null)
            {
                try
                {
                    dmf.dtOperDate = DateTime.Now;
                    dmf.CancelMissMenu(this.SelectedMenuInfo.OrderMenuId);
                    UpdateOrderMenuData();
                    //UpdateOrderMenuDataComplete();
                    UpdateInfoData();
                }
                catch (Exception ex)
                {
                    Helper.ShowErrorMsg(ex.Message);
                    Helper.HandelException(ex);
                }
            }
        }
        private bool NoCtCancelLackMenuCanExecute()
        {
            if (this.SelectedMenuInfo != null&&
                this.SelectedMenuInfo.MissQuantity>0)
                return true;
            return false;
        }
        public ICommand NoCtCancelLackMenu
        {
            get
            {
                return new RelayCommand(NoCtCancelLackMenuExecute, NoCtCancelLackMenuCanExecute);
            }
        }
        #endregion

        #region 已完成-取消出菜
        private void CtCancelOutMenuExecute()
        {
            if (this.SelectedMenuInfoComplete != null && 
                this.SelectedMenuInfoComplete.SelectedDesk != null)
            {
                cancelOutMenu(this.SelectedMenuInfoComplete.OrderMenuId,
                    this.SelectedMenuInfoComplete.SelectedDesk,
                    this.SelectedMenuInfoComplete.Printer,
                    this.SelectedMenuInfoComplete.InvName,
                    this.SelectedMenuInfoComplete.Comment);
            }
        }
        private bool CtCancelOutMenuCanExecute()
        {
            if (this.SelectedMenuInfoComplete != null && 
                this.SelectedMenuInfoComplete.SelectedDesk != null&&
                this.SelectedMenuInfoComplete.MenuQuantity>0)
                return true;
            return false;
        }
        public ICommand CtCancelOutMenu
        {
            get
            {
                return new RelayCommand(CtCancelOutMenuExecute, CtCancelOutMenuCanExecute);
            }
        }
        #endregion

        #region 已完成-取消缺菜
        private void CtCancelLackMenuExecute()
        {
            if (this.SelectedMenuInfoComplete != null)
            {
                try
                {
                    dmf.dtOperDate = DateTime.Now;
                    dmf.CancelMissMenu(this.SelectedMenuInfoComplete.OrderMenuId);
                    UpdateOrderMenuData();
                    //UpdateOrderMenuDataComplete();
                    UpdateInfoData();
                }
                catch (Exception ex)
                {
                    Helper.ShowErrorMsg(ex.Message);
                    Helper.HandelException(ex);
                }
            }
        }
        private bool CtCancelLackMenuCanExecute()
        {
            if (this.SelectedMenuInfoComplete != null&&
                this.SelectedMenuInfoComplete.MissQuantity>0)
                return true;
            return false;
        }
        public ICommand CtCancelLackMenu
        {
            get
            {
                return new RelayCommand(CtCancelLackMenuExecute, CtCancelLackMenuCanExecute);
            }
        }
        #endregion
    }
}
