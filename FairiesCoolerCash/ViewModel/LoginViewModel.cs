using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using DXInfo.Data.Contracts;
using System.Web.Security;
using System.Threading;
using System.IO;
using System.Diagnostics;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Controls;
using System.ComponentModel.DataAnnotations;
using FairiesCoolerCash.Business;

namespace FairiesCoolerCash.ViewModel
{
    public class LoginViewModel : BusinessViewModelBase
    {
        #region 构造
        public LoginViewModel(IFairiesMemberManageUow uow)
            : base(uow, new List<string>() { "UserName", "Password" })
        {
            winLoad();
            
            this.Title = ClientCommon.ClientSideTitle();
        }
        private void winLoad()
        {
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"AutoUpdate.exe.delete"))
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"AutoUpdate.exe.delete");
            }
        }

        public override void Cleanup()
        {
            base.Cleanup();
            Messenger.Default.Unregister<CloseViewMessageToken>(this);        
        }

        #endregion

        #region 登录
        private void login()
        {
            string userName = this.UserName;
            string passwd = this.Password;
            if (string.IsNullOrWhiteSpace(userName))
            {
                MessageBox.Show("请输入用户名");
                return;
            }
            if (string.IsNullOrWhiteSpace(passwd))
            {
                MessageBox.Show("请输入密码");
                return;
            }
            if (!Membership.ValidateUser(userName, passwd))
            {
                MessageBox.Show("用户名或密码错误，多次错误后此用户将被锁定");
                return;
            }
            MembershipUser user = Membership.GetUser(userName);
            if (user == null) throw new ArgumentException("操作员信息错误");
            Guid userId = Guid.Parse(user.ProviderUserKey.ToString());
            DXInfo.Models.aspnet_CustomProfile oper = Uow.aspnet_CustomProfile.GetById(g => g.UserId == userId);
            if (oper == null) throw new ArgumentException("操作员信息错误");

            DXInfo.Models.aspnet_Users auser = Uow.aspnet_Users.GetById(g => g.UserId == userId);

            var nc = Uow.NameCode.GetAll().Where(w => w.Type == "LocalDept").FirstOrDefault();
            if (nc == null)
            {
                MessageBox.Show("请首先设置本地门店", "设置本地门店", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(nc.Value))
            {
                MessageBox.Show("本地门店信息错误", "设置本地门店", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Guid deptId = Guid.Parse(nc.Value);
            if (!oper.DeptId.HasValue)
            {
                MessageBox.Show("请设置操作员门店信息");
                return;
            }
            if (oper.DeptId.Value != deptId && userName != "admin")
            {
                MessageBox.Show("不是这个门店操作员，不能登录");
                return;
            }
            DXInfo.Models.Depts dept = Uow.Depts.GetById(g=>g.DeptId==deptId);
            if (dept == null) throw new ArgumentException("门店信息错误");

            DXInfo.Principal.MyIdentity mi = new DXInfo.Principal.MyIdentity(oper, auser, dept, "MyIdentity");

            List<DXInfo.Models.aspnet_Sitemaps> lFunc = GetAllSitemapKeys(oper.UserId,auser.UserName);

            DXInfo.Principal.MyPrincipal mp = new DXInfo.Principal.MyPrincipal(mi, lFunc);

            if (Thread.CurrentPrincipal == null)
            {
                AppDomain.CurrentDomain.SetThreadPrincipal(mp);
            }
            else
            {
                Thread.CurrentPrincipal = mp;
            }
            //Uow.Dispose();
            this.UserName = null;
            this.Password = null;
            var rmw = ServiceLocator.Current.GetInstance<RibbonMainWindow>();
            App.Current.MainWindow = rmw;
            rmw.Show();
            Messenger.Default.Send(new CloseViewMessageToken());
        }
        private List<DXInfo.Models.aspnet_Sitemaps> GetAllSitemapKeys(Guid userId,string userName)
        {
            if (userName == "admin")
            {
                var menu = Uow.aspnet_Sitemaps.GetAll().Where(w => w.IsClient).OrderBy(o => o.ParaId).ToList();
                return menu;
            }
            else
            {
                //if (Business.Common.DynamicRibbonMenu(this.Uow))
                //{
                //    var ruleforrole = (from a in Uow.aspnet_AuthorizationRules.GetAll()
                //                       join b in Uow.aspnet_UsersInRoles.GetAll().Where(w => w.UserId == userId) on a.RoleId equals b.RoleId
                //                       join c in Uow.aspnet_Sitemaps.GetAll().Where(w => w.IsClient && w.Controller != "AdminViewModel") on a.SiteMapKey equals c.Code
                //                       select c).ToList<DXInfo.Models.aspnet_Sitemaps>();
                //    var ruleforuser = (from a in Uow.aspnet_AuthorizationRules.GetAll().Where(w => w.UserId == userId)
                //                       join b in Uow.aspnet_Sitemaps.GetAll().Where(w => w.IsClient && w.Controller != "AdminViewModel") on a.SiteMapKey equals b.Code
                //                       select b).ToList<DXInfo.Models.aspnet_Sitemaps>();
                //    var noauth = Uow.aspnet_Sitemaps.GetAll().Where(w => w.IsAuthorize == false && w.IsClient)
                //        .Select(s => s).ToList<DXInfo.Models.aspnet_Sitemaps>();

                //    return ruleforrole.Concat(ruleforuser).Concat(noauth).Distinct().OrderBy(o => o.ParaId).ToList<DXInfo.Models.aspnet_Sitemaps>();
                //}
                //else
                //{
                    var ruleforrole = (from a in Uow.aspnet_AuthorizationRules.GetAll()
                                       join b in Uow.aspnet_UsersInRoles.GetAll().Where(w => w.UserId == userId) on a.RoleId equals b.RoleId
                                       join c in Uow.aspnet_Sitemaps.GetAll().Where(w => w.IsClient) on a.SiteMapKey equals c.Code
                                       select c).ToList<DXInfo.Models.aspnet_Sitemaps>();
                    var ruleforuser = (from a in Uow.aspnet_AuthorizationRules.GetAll().Where(w => w.UserId == userId)
                                       join b in Uow.aspnet_Sitemaps.GetAll().Where(w => w.IsClient) on a.SiteMapKey equals b.Code
                                       select b).ToList<DXInfo.Models.aspnet_Sitemaps>();
                    var noauth = Uow.aspnet_Sitemaps.GetAll().Where(w => w.IsAuthorize == false && w.IsClient)
                        .Select(s => s).ToList<DXInfo.Models.aspnet_Sitemaps>();

                    return ruleforrole.Concat(ruleforuser).Concat(noauth).Distinct().OrderBy(o => o.ParaId).ToList<DXInfo.Models.aspnet_Sitemaps>();
                //}
            }
        }
        private ICommand _Login;
        public ICommand Login
        {
            get
            {
                if (_Login == null)
                {
                    _Login = new RelayCommand(login, LoginCanExecute);
                }
                return _Login;
            }
        }
        private bool LoginCanExecute()
        {
            return this.IsValid;
        }
        #endregion

        #region 退出
        private void exit()
        {
            Application.Current.Shutdown();
        }
        public ICommand Exit
        {
            get
            {
                return new RelayCommand(exit);
            }
        }
        #endregion

        #region 版本
        //private string _Version;
        public string Version
        {
            get
            {
                return "v. " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); ;
            }
            //set
            //{
            //    _Version = value;
            //    this.RaisePropertyChanged("Version");
            //}
        }
        #endregion

    }
}
