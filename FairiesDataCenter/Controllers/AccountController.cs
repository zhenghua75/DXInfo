using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using ynhnTransportManage.Models;
using DXInfo.Profile;
using System.Data.Entity;
using DXInfo.Models;
using System.Web.Profile;
using System.Web.Helpers;
using System.Security.Permissions;
using Trirand.Web.Mvc;
using System.Data.Objects.SqlClient;
using System.Data;
using CommCenter;
using System.Collections;
using BusiComm;
using DXInfo.Data.Contracts;
using Ninject;
using System.Data.Objects;
using System.Data.Linq.SqlClient;
namespace ynhnTransportManage.Controllers
{
    public class AccountController : BaseController
    {
        #region 字段
        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }
        public IRolesService RoleService { get; set; }
        public IAMSCMUow AmscmUow { get; set; }
        #endregion

        #region 构造
        public AccountController(IFairiesMemberManageUow uow, IAMSCMUow amscmUow):base(uow)
        {
            this.AmscmUow = amscmUow;
            this.Uow.Db.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ COMMITTED;");    
        }
        
        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }
            if (RoleService == null) { RoleService = new AccountRoleService(); }
            base.Initialize(requestContext);
        }
        #endregion

        #region Index
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 用户
        private void SetUpUsersGrid(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Users_RequestData");
            grid.EditUrl = Url.Action("Users_EditData");
            SetUpGrid(grid);

            SetDropDownColumn(grid, "DeptId", this.GetDept());
            SetBoolColumn(grid, "IsApproved");
            SetBoolColumn(grid, "IsLockedOut");
            SetDateColumn(grid, "LastActivityDate");
            SetDateColumn(grid, "LastLoginDate");
            SetDateColumn(grid, "CreateDate");

            grid.ToolBarSettings.CustomButtons.Add(new JQGridToolBarButton()
            {
                Position = ToolBarButtonPosition.Last,
                ToolTip = "重置密码",
                Text = "重置密码",
                OnClick = "customButtonClicked",
                ButtonIcon = "ui-icon-extlink",
            });
            grid.ToolBarSettings.CustomButtons.Add(new JQGridToolBarButton()
            {
                Position = ToolBarButtonPosition.Last,
                ToolTip = "解锁",
                Text = "解锁",
                OnClick = "customButton2Clicked",
                ButtonIcon = "ui-icon-extlink",
            });
            MembershipUser user = Membership.GetUser();
            if (user!=null && user.UserName == "admin")
            {
                grid.ToolBarSettings.ShowDeleteButton = true;
                JQGridColumn col = grid.Columns.Find(f => f.EditActionIconsColumn);
                if (col != null)
                {
                    col.EditActionIconsSettings = new EditActionIconsSettings() { ShowEditIcon = true, ShowDeleteIcon = true };
                }
            }
            else
            {
                grid.ToolBarSettings.ShowDeleteButton = false;
            }
        }
        private UserGridModel SetUpUsersGridModel()
        {
            var gridModel = new UserGridModel();
            SetUpUsersGrid(gridModel.UserGrid);
            return gridModel;
        }
        [Authorize]
        public ActionResult Users()
        {
            var gridModel = SetUpUsersGridModel();
            return View(gridModel);            
        }
        private IQueryable queryUser()
        {
            var q = (from user in Uow.aspnet_Users.GetAll()
                     join memberships in Uow.aspnet_Membership.GetAll() on user.UserId equals memberships.UserId into um
                     from ms in um.DefaultIfEmpty()
                     join profile in Uow.aspnet_CustomProfile.GetAll() on ms.UserId equals profile.UserId into uc
                     from cp in uc.DefaultIfEmpty()
                     join depts in Uow.Depts.GetAll() on cp.DeptId equals depts.DeptId into dc
                     from d in dc.DefaultIfEmpty()
                     select new
                     {
                         user.UserId,
                         user.UserName,
                         FullName = (cp == null ? string.Empty : cp.FullName),
                         DeptId = (d == null ? Guid.Empty : d.DeptId),
                         DeptName = (d == null ? string.Empty : d.DeptName),
                         LastActivityDate = user.LastActivityDate,
                         IsApproved = ms == null ? false : ms.IsApproved,
                         IsLockedOut = ms == null ? false : ms.IsLockedOut,
                         LastLoginDate = ms == null ? DateTime.MinValue : ms.LastLoginDate,
                         CreateDate = ms == null ? DateTime.MinValue : ms.CreateDate,
                     });
            return q;
        }
        [Authorize]
        public ActionResult Users_RequestData()
        {
            var gridModel = SetUpUsersGridModel();
            return QueryAndExcel(gridModel.UserGrid, queryUser(), "用户.xls");
        }
        private void editUser(UserInfoModel editedUser)
        {
            MembershipService.UpdateUser(editedUser.UserId, editedUser.FullName, editedUser.DeptId);
            MembershipService.ChangeApproval(editedUser.UserId, editedUser.IsApproved);
        }
        private void delUser(UserInfoModel editedUser)
        {
            MembershipService.DeleteUser(editedUser.UserId);
            var cus = Uow.aspnet_CustomProfile.GetAll().Where(w => w.UserId == editedUser.UserId);
            if (cus.Count() > 0)
            {
                foreach (DXInfo.Models.aspnet_CustomProfile oldcus in cus)
                {
                    Uow.aspnet_CustomProfile.Delete(oldcus);
                }
                Uow.Commit();
            }
        }
        [Authorize]
        public ActionResult Users_EditData(UserInfoModel editedUser)
        {
            var gridModel = SetUpUsersGridModel();
            return ajaxCallBack<UserInfoModel>(gridModel.UserGrid, editedUser, null, editUser, delUser);
        }
        #endregion

        #region 登录
        public ActionResult IsNoActiveXCheck(string userName)
        {
            JsonResult json = new JsonResult();
            json.Data = new { success = businessCommon.IsNoActiveXCheck(userName) };
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return json;
        }
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                int icount = Uow.aspnet_Users.GetAll().Count();
                if (icount == 0)
                {
                    MembershipCreateStatus createStatus = MembershipService.CreateUser("admin", "123456", "系统管理员", Guid.Empty);
                    model.UserName = "admin";
                    model.Password = "123456";
                }
                if (MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    if (!string.IsNullOrEmpty(model.HardwareID))
                    {
                        //using (DXInfo.Models.FairiesMemberManage context = new DXInfo.Models.FairiesMemberManage())
                        //{
                        var key = Uow.ekey.GetAll().Where(w => w.HardwareID == model.HardwareID).FirstOrDefault();
                        var us = Uow.aspnet_Users.GetAll().Where(w => w.UserName == model.UserName).FirstOrDefault();
                            if (key == null)
                            {
                                DXInfo.Models.ekey tk = new ekey();
                                tk.HardwareID = model.HardwareID;
                                tk.CardNo = model.CardNo;
                                tk.CreateDate = DateTime.Now;
                                tk.IsUse = true;
                                tk.UserId = us != null ? us.UserId : Guid.Empty;
                                Uow.ekey.Add(tk);
                                Uow.Commit();
                            }
                            else
                            {
                                if (!key.IsUse)
                                {
                                    ModelState.AddModelError("", "ekey失效。");
                                    return View(model);
                                }
                            }
                        //}
                    }
                    if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("AMSApp"))
                    {
                        AMSLog clog = new AMSLog();
                        clog.WriteLine("LoginID:" + model.UserName + ";    Mac:" + model.MacAddress + ";");
                        Hashtable htapp = (Hashtable)this.HttpContext.Application["appconf"];
                        string strcons = (string)htapp["cons"];
                        DataTable dtMac = (DataTable)this.HttpContext.Application["MAC"];

                        if (dtMac == null || dtMac.Rows.Count == 0)
                        {
                            ModelState.AddModelError("", "请添加MAC地址。");
                            return View(model);
                        }
                        bool okflag = false;
                        if (model.UserName == "admin")
                        {
                            okflag = true;
                        }
                        else
                        {
                            for (int i = 0; i < dtMac.Rows.Count; i++)
                            {
                                if (dtMac.Rows[i][0].ToString() == model.MacAddress)
                                {
                                    okflag = true;
                                    break;
                                }
                            }
                        }
                        okflag = true;
                        if (!okflag)
                        {
                            ModelState.AddModelError("", "无访问权限" + model.MacAddress);
                            return View(model);
                        }
                        Manager m1 = new Manager(strcons);
                        CMSMStruct.LoginStruct ls1 = new CMSMStruct.LoginStruct();

                        CMSMStruct.OperStruct OperNew = new CMSMStruct.OperStruct();
                        OperNew.strMacAddress = model.MacAddress;
                        //using (AMSCM.Models.AMSCM context = new AMSCM.Models.AMSCM())
                        //{

                        var tbLogin = AmscmUow.tbLogin.GetById(g=>g.vcLoginID==model.UserName);
                        //var tbLogin = AmscmUow.tbLogin.GetAll().Where(w => w.vcLoginID == model.UserName).FirstOrDefault();

                        if (tbLogin == null)
                        {
                            ModelState.AddModelError("", "未配置AMSCM连接串");
                            return View(model);
                        }
                        ls1.strLoginID = tbLogin.vcLoginID;
                        ls1.strOperName = tbLogin.vcOperName;
                        ls1.strDeptID = tbLogin.vcDeptID;
                        ls1.strLimit = tbLogin.vcLimit;


                        OperNew.strDeptID = ls1.strDeptID;
                        OperNew.strOperID = ls1.strLoginID;
                        //}
                        m1.InsertOperLog(OperNew);
                        //Session["tbNotice"] = Helper.Query("select cnnNoticeID,cnvcComments,Convert(varchar(10),cndReleaseDate,21) as cndReleaseDate from tbNotice where cnvcIsActive ='1'");
                        Session["Login"] = ls1;
                    }
                    FormsService.SignIn(model.UserName, false);
                    
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "提供的用户名或密码不正确，多次错误后此用户将被锁定");
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }
        #endregion

        #region 注销
        public ActionResult LogOff()
        {
            FormsService.SignOut();
            return RedirectToAction("LogOn");
        }
        #endregion

        #region 注册
        [Authorize]
        public ActionResult Register()
        {
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                // 尝试注册用户                
                MembershipCreateStatus createStatus = MembershipService.CreateUser(model.UserName, model.Password, model.FullName,model.DeptId);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    ViewBag.PasswordLength = MembershipService.MinPasswordLength;
                    return RedirectToAction("RegisterSucess", "Account");
                }
                else
                {
                    ModelState.AddModelError("", AccountValidation.ErrorCodeToString(createStatus));
                }
            }
            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
            return View(model);
        }
        public ActionResult RegisterSucess()
        {
            return View();
        }
        #endregion

        #region 修改密码
        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "当前密码不正确或新密码无效。");
                }
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            ViewBag.PasswordLength = MembershipService.MinPasswordLength;
            return View(model);
        }
        public ActionResult ChangePassword2(Guid UserId)
        {
            aspnet_Users u = Uow.aspnet_Users.GetAll().Where(w => w.UserId == UserId).FirstOrDefault();
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (MembershipService.ChangePassword(UserId))
            {
                json.Data = new { Error = "" };
            }
            else
            {
                json.Data = new { Error = "帐户已被锁定！" };
            }
            return json;
        }
        public ActionResult UnLock(Guid UserId)
        {
            aspnet_Users u = Uow.aspnet_Users.GetAll().Where(w => w.UserId == UserId).FirstOrDefault();
            JsonResult json = new JsonResult();
            json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (MembershipService.UnlockUser(UserId))
            {
                json.Data = new { Error = "" };
            }
            else
            {
                json.Data = new { Error = "帐户已被锁定！" };
            }
            return json;
        }
        //public ActionResult DeleteUser(Guid UserId)
        //{
        //    //aspnet_Users u = Uow.aspnet_Users.GetAll().Where(w => w.UserId == UserId).FirstOrDefault();
        //    JsonResult json = new JsonResult();
        //    json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        //    if (MembershipService.DeleteUser(UserId))
        //    {
        //        json.Data = new { Error = "" };
        //    }
        //    else
        //    {
        //        json.Data = new { Error = "删除失败！" };
        //    }
        //    return json;
        //}
        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }
        #endregion        
        
        #region 角色
        private void SetUpRolesGrid(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Roles_RequestData");
            grid.EditUrl = Url.Action("Roles_EditData");            
            SetUpGrid(grid);
        }
        private RoleGridModel SetUpRolesGridModel()
        {
            var gridModel = new RoleGridModel();
            SetUpRolesGrid(gridModel.RoleGrid);
            return gridModel;
        }
        [Authorize]
        public ActionResult Roles()
        {
            var gridModel = SetUpRolesGridModel();
            return View(gridModel);
        }
        [Authorize]
        public ActionResult Roles_RequestData()
        {
            var gridModel = SetUpRolesGridModel();
            return QueryAndExcel(gridModel.RoleGrid, Uow.aspnet_Roles.GetAll(), "角色.xls");
        }
        private void addRole(aspnet_Roles role)
        {
            aspnet_Roles oldrole = Uow.aspnet_Roles.GetAll().FirstOrDefault(r => r.RoleName == role.RoleName);
            if (oldrole != null)
            {
                throw new BusinessException(role.RoleName + "角色已存在");
            }
            RoleService.Create(role.RoleName);
            aspnet_Roles oldrole2 = Uow.aspnet_Roles.GetAll().FirstOrDefault(r => r.RoleName == role.RoleName);
            if (oldrole2 != null)
            {
                oldrole2.Description = role.Description;
                Uow.aspnet_Roles.Update(oldrole2);
                Uow.Commit();
            }
        }
        private void editRole(aspnet_Roles role)
        {
            aspnet_Roles roleold = Uow.aspnet_Roles.GetAll().FirstOrDefault(r => r.RoleId == role.RoleId);
            roleold.Description = role.Description;
            Uow.aspnet_Roles.Update(roleold);
            Uow.Commit();
        }
        private void delRole(aspnet_Roles role)
        {
            RoleService.Delete(role.RoleId);
        }
        [Authorize]
        public ActionResult Roles_EditData(aspnet_Roles role)
        {
            var gridModel = SetUpRolesGridModel();
            return ajaxCallBack<aspnet_Roles>(gridModel.RoleGrid, role, addRole, editRole, delRole);
        }
        #endregion

        #region 权限
        [Authorize]
        public ActionResult AuthorizationRule()
        {
            var authGridModel = new AuthorizationGridModel();
            SetUpAuthGridModel(authGridModel);
            return View(authGridModel);
        }
        private void SetUpAuthGridModel(AuthorizationGridModel authorizationGridModel)
        {
            var authorizationGrid = authorizationGridModel.AuthorizationGrid;
            var roleGrid = authorizationGridModel.RolesGrid;

            authorizationGrid.DataUrl = Url.Action("AuthorizationRuleOfAuth_RequestData");
            authorizationGrid.EditUrl = Url.Action("AuthorizationRuleOfAuth_EditData");
            authorizationGrid.SearchDialogSettings.MultipleSearch = true;
            authorizationGrid.ToolBarSettings.ShowEditButton = true;
            authorizationGrid.SortSettings.InitialSortColumn = "Sort";
            authorizationGrid.SortSettings.InitialSortDirection = Trirand.Web.Mvc.SortDirection.Asc;

            this.SetBoolColumn(authorizationGrid, "IsInRole");
            this.SetBoolColumn(authorizationGrid, "IsClient");

            roleGrid.DataUrl = Url.Action("AuthorizationRuleOfRole_RequestData");
            roleGrid.ID = "RolesGrid";
            roleGrid.HierarchySettings.HierarchyMode = HierarchyMode.Parent;
            roleGrid.AppearanceSettings.Caption = this.Title;
            roleGrid.ClientSideEvents.SubGridRowExpanded = "showAuthsSubGrid";
            roleGrid.SearchDialogSettings.MultipleSearch = true;
            authorizationGrid.HierarchySettings.HierarchyMode = HierarchyMode.Child;
            
            authorizationGrid.ID = "AuthorizationGrid";

        }        
        public JsonResult AuthorizationRuleOfRole_RequestData()
        {
            var authGridModel = new AuthorizationGridModel();
            SetUpAuthGridModel(authGridModel);
            var roles = Uow.aspnet_Roles.GetAll();
            return authGridModel.RolesGrid.DataBind(roles);
        }
        public JsonResult AuthorizationRuleOfAuth_RequestData(Guid parentRowID)
        {

            var authGridModel = new AuthorizationGridModel();            
            SetUpAuthGridModel(authGridModel);
            JQGridTreeExpandData expandData = authGridModel.AuthorizationGrid.GetTreeExpandData();

            Guid empty = Guid.Empty;
            var auths = (from s in Uow.aspnet_Sitemaps.GetAll().Where(w => w.IsAuthorize == true && w.Code != "000")
                          join a in Uow.aspnet_AuthorizationRules.GetAll().Where(w => w.RoleId == parentRowID) on s.Code equals a.SiteMapKey into sa
                          from auth in sa.DefaultIfEmpty()
                          orderby s.Sort
                          select new
                          {
                              IsInRole = (auth == null ? false : true),
                              Code = s.Code,
                              s.Title,
                              s.IsClient,
                              s.ParentCode,
                              tree_loaded = true,
                              tree_level = 0,
                              tree_parent = s.ParentCode,
                              tree_leaf = false,
                              tree_expanded = false
                          }).ToList();

            var auths1 = auths.Select((map, idx) => new
            {
                map.IsInRole,
                map.Code,
                map.Title,
                map.IsClient,
                map.ParentCode,
                Sort = idx + 1,
                map.tree_loaded,
                map.tree_level,
                map.tree_parent,
                map.tree_leaf,
                map.tree_expanded
            });
            
            var auths2 = (from d in auths1
                          where d.ParentCode == "000"
                          select new
                          {
                              d.IsInRole,
                              d.Code,
                              d.Title,
                              d.IsClient,
                              Sort=d.Sort.ToString().PadLeft(5,'0'),
                              d.tree_loaded,
                              tree_level = 0,
                              tree_parent = "",
                              tree_leaf = false,
                              d.tree_expanded
                          }).ToList();

            var auths3 = (from d in auths1
                         join d2 in auths2 on d.ParentCode equals d2.Code
                          select new
                          {
                              d.IsInRole,
                              d.Code,
                              d.Title,
                              d.IsClient,
                              Sort = d2.Sort+ d.Sort.ToString().PadLeft(5, '0'),
                              d.tree_loaded,
                              tree_level = 1,
                              tree_parent = d2.Code,
                              tree_leaf = false,
                              d.tree_expanded
                          }).ToList();

            var auths4 = (from d in auths1
                          join d2 in auths3 on d.ParentCode equals d2.Code
                          select new
                          {
                              d.IsInRole,
                              d.Code,
                              d.Title,
                              d.IsClient,
                              Sort = d2.Sort + d.Sort.ToString().PadLeft(5, '0'),
                              d.tree_loaded,
                              tree_level = 2,
                              tree_parent = d2.Code,
                              tree_leaf = true,
                              d.tree_expanded
                          }).ToList();

            var auths5 = (from d in auths3
                          join d2 in auths4 on d.Code equals d2.tree_parent into dd1
                          from dd1s in dd1.DefaultIfEmpty()
                          select new
                          {
                              d.IsInRole,
                              d.Code,
                              d.Title,
                              d.IsClient,
                              Sort = d.Sort,
                              d.tree_loaded,
                              tree_level = 1,
                              tree_parent = d.tree_parent,
                              tree_leaf = dd1s==null?true:false,
                              d.tree_expanded
                          }).ToList();
            var authss = auths2.Union(auths5).Union(auths4).ToList();

            if (expandData.ParentID != null)
            {
                string parentId = expandData.ParentID;
                authss = authss.Where(w => w.tree_parent == parentId).ToList();
            }
            authss = authss.OrderBy(o=>o.Sort).ToList();
            return authGridModel.AuthorizationGrid.DataBind(authss.AsQueryable());
        }
        public ActionResult AuthorizationRuleOfAuth_EditData(Guid ParentRowId, string Code, bool? IsInRole)
        {
            bool isInRole = IsInRole.HasValue ? IsInRole.Value : true;
            var ruleold = Uow.aspnet_AuthorizationRules.GetAll().Where(s => s.RoleId == ParentRowId && s.SiteMapKey == Code).FirstOrDefault();
            if (ruleold != null)
            {
                if (!isInRole) Uow.aspnet_AuthorizationRules.Delete(ruleold);
            }
            else
            {
                if (isInRole)
                {
                    aspnet_AuthorizationRules rule = new aspnet_AuthorizationRules();
                    rule.RoleId = ParentRowId;
                    rule.SiteMapKey = Code;
                    Uow.aspnet_AuthorizationRules.Add(rule);
                }
            }
            Uow.Commit();

            return new EmptyResult();
        }
        #endregion

        #region 角色分配
        private void SetUpRoleAssignGridModel(RoleAssignGridModel gridModel)
        {
            var usersGrid = gridModel.UsersGrid;
            var rolesGrid = gridModel.RolesGrid;

            rolesGrid.AppearanceSettings.Caption = this.Title;
            usersGrid.DataUrl = Url.Action("RoleAssignOfUser_RequestData");
            usersGrid.EditUrl = Url.Action("RoleAssignOfUser_EditData");
            usersGrid.SearchDialogSettings.MultipleSearch = true;

            rolesGrid.DataUrl = Url.Action("RoleAssignOfRole_RequestData");
            rolesGrid.SearchDialogSettings.MultipleSearch = true;

            this.SetBoolColumn(usersGrid, "IsInRole");
        }
        [Authorize]
        public ActionResult RoleAssign()
        {
            var gridModel = new RoleAssignGridModel();
            SetUpRoleAssignGridModel(gridModel);
            return View(gridModel);
        }
        public JsonResult RoleAssignOfRole_RequestData()
        {
            var gridModel = new RoleAssignGridModel();
            SetUpRoleAssignGridModel(gridModel);
            var roles = Uow.aspnet_Roles.GetAll();
            return gridModel.RolesGrid.DataBind(roles);
        }
        public JsonResult RoleAssignOfUser_RequestData(Guid ParentRowId)
        {
            var gridModel = new RoleAssignGridModel();
            SetUpRoleAssignGridModel(gridModel);
            var oldrole = Uow.aspnet_Roles.GetAll().Where(w => w.RoleId == ParentRowId).FirstOrDefault();
            var users = from user in Uow.aspnet_Users.GetAll()
                        join profile in Uow.aspnet_CustomProfile.GetAll() on user.UserId equals profile.UserId into uc
                        from cp in uc.DefaultIfEmpty()
                        join depts in Uow.Depts.GetAll() on cp.DeptId equals depts.DeptId into dc
                        from d in dc.DefaultIfEmpty()
                        join auth in Uow.aspnet_UsersInRoles.GetAll().Where(s => s.RoleId == oldrole.RoleId) on user.UserId equals auth.UserId into ua
                        from a in ua.DefaultIfEmpty()
                        join role in Uow.aspnet_Roles.GetAll() on a.RoleId equals role.RoleId into ra
                        from ras in ra.DefaultIfEmpty()
                        select new
                        {
                            IsInRole = (ras == null ? false : true),
                            user.UserId,
                            user.UserName,
                            FullName = (cp == null ? string.Empty : cp.FullName),
                            DeptName = (d == null ? string.Empty : d.DeptName),
                        };

            return gridModel.UsersGrid.DataBind(users);
        }
        public ActionResult RoleAssignOfUser_EditData(Guid ParentRowId,Guid UserId,bool? IsInRole)
        {
            bool isInRole = IsInRole.HasValue?IsInRole.Value:true;
            var r = Uow.aspnet_Roles.GetAll().Where(w => w.RoleId == ParentRowId).FirstOrDefault();
            if (isInRole)
            {
                MembershipUser user = MembershipService.GetUser(UserId);
                RoleService.AddToRole(user, r.RoleName);                
            }
            else
            {
                MembershipUser user = MembershipService.GetUser(UserId);
                RoleService.RemoveFromRole(user, r.RoleName);
            }
            return new EmptyResult();
        }
        #endregion
    }
}
