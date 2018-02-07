using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Trirand.Web.Mvc;
using System.Web.UI.WebControls;
using Ninject;
using DXInfo.Data.Contracts;

namespace ynhnTransportManage.Models
{
    #region 模型

    public class ChangePasswordModel
    {        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "当前密码")]
        public string OldPassword { get; set; }

        [Required]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认新密码")]
        [Compare("NewPassword", ErrorMessage = "新密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required(ErrorMessage="用户名字段是必需的")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage="密码字段是必需的")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Display(Name = "记住我?")]
        public bool RememberMe { get; set; }

        public string HardwareID { get; set; }
        public string CardNo { get; set; }
        public string MacAddress { get; set; }
    }


    public class RegisterModel
    {
        [Required(ErrorMessage="用户名字段是必需的")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage="姓名字段是必需的")]
        [DataType(DataType.Text)]
        [Display(Name = "姓名")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "部门字段是必需的")]
        [Display(Name="门店")]
        public Guid DeptId { get; set; }

        [Required(ErrorMessage="密码字段是必需的")]
        [ValidatePasswordLength]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }
    public class UserInfoModel
    {
        [Display(Name = "用户Id")]
        public Guid UserId { get; set; }
        
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "姓名")]
        public string FullName { get; set; }

        public Guid DeptId { get; set; }

        public string DeptCode { get; set; }

        [Display(Name = "门店")]
        public string DeptName { get; set; }

        public DateTime LastActivityDate { get; set; }
        //public bool IsOnline { get; set; }
        //public DateTime LastActivityDate { get; set; }
        //public string Online { get; set; }
        public bool IsApproved { get; set; }

        public DateTime LastLoginDate { get; set; }
        public DateTime CreateDate { get; set; }
        //public string RoleDescription { get; set; }
    }
    public class RoleInfoModel
    {
        [Required]
        [Display(Name = "角色ID")]
        public Guid RoleId { get; set; }
        [Required]
        [Display(Name = "角色名")]
        public string RoleName { get; set; }

         [Required]
        [Display(Name = "描述")]
        public string Description { get; set; }
    }
    public class SitemapInfoModel
    {
        public bool IsInRole { get; set; }
        [Required]
        [Display(Name = "编码")]
        public String Code { get; set; }
        [Required]
        [Display(Name = "标题")]
        public String Title { get; set; }
        public string RoleName { get; set; }
        //[Display(Name = "描述")]
        //public String Description { get; set; }
        //[Required]
        //[Display(Name = "模块")]
        //public String Controller { get; set; }
        //[Required]
        //[Display(Name = "页面(或动作)")]
        //public String Action { get; set; }
        //[Display(Name = "参数名")]
        //public String ParaId { get; set; }
        //[Display(Name = "路径")]
        //public String Url { get; set; }
        //[Display(Name = "上级菜单编码")]
        //public String ParentCode { get; set; }
        //[Required]
        //[Display(Name = "是否权限控制")]
        //public Boolean IsAuthorize { get; set; }
        //[Required]
        //[Display(Name = "是否菜单")]
        //public Boolean IsMenu { get; set; }			
    }
    public class EditUserModel
    {
        public Guid UserId { get; set; }

        [Required]
        [Display(Name="用户名")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "姓名")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "门店")]
        public Guid DeptId { get; set; }
    }

    public class RolesInfoModel
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool IsInRole { get; set; }
    }
    public class AddUserToRolesModel
    {
        [Display(Name = "用户Id")]        
        public Guid UserId { get; set; }

        [Display(Name = "用户名")]
        [Editable(false)]
        public string UserName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "姓名")]
        [Editable(false)]
        public string FullName { get; set; }

        [Display(Name = "门店")]
        [Editable(false)]
        public string DeptName { get; set; }

        [Display(Name = "角色")]
        public IEnumerable<RolesInfoModel> Roles { get; set; }
    }
    public class ForRoleAddUserModel
    {
        [Display(Name = "角色ID")]
        public Guid RoleId { get; set; }
        [Display(Name = "角色名")]
        public string RoleName { get; set; }
        [Display(Name = "描述")]
        public string Description { get; set; }
        public IEnumerable<UserInfoForRoleModel> Users { get; set; }
    }
    public class UserInfoForRoleModel
    {
        [Display(Name = "用户Id")]
        public Guid UserId { get; set; }

        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "姓名")]
        public string FullName { get; set; }

        [Display(Name = "门店")]
        public string DeptName { get; set; }

        public DateTime LastActivityDate { get; set; }
        public bool IsApproved { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsInRole { get; set; }
    }
    //public class AddUserToRolesModel2
    //{
    //    [Display(Name = "用户Id")]
    //    public Guid UserId { get; set; }

    //    [Display(Name = "用户名")]
    //    public string UserName { get; set; }

    //    [DataType(DataType.Text)]
    //    [Display(Name = "姓名")]
    //    public string FullName { get; set; }

    //    [Display(Name = "部门")]
    //    public string DeptName { get; set; }

    //    [Display(Name = "角色")]
    //    public IEnumerable<string> Roles2 { get; set; }
    //}

    public class DetailsUserModel
    {
        //[Required]
        //[Display(Name = "用户名")]
        //public string UserName { get; set; }

        //[Required]
        //[DataType(DataType.Text)]
        //[Display(Name = "姓名")]
        //public string FullName { get; set; }

        //[Required]
        //[Display(Name = "部门")]
        //public string DeptName { get; set; }


        public DXInfo.Profile.CustomProfile Profle { get; set; }
        public DXInfo.Models.Depts Dept { get; set; }
        public MembershipUser User { get; set; }
    }
    #endregion

    #region Services
    // FormsAuthentication 类型是密封的且包含静态成员，因此很难对
    // 调用其成员的代码进行单元测试。下面的接口和 Helper 类演示
    // 如何围绕这种类型创建一个抽象包装，以便可以对 AccountController
    // 代码进行单元测试。

    public interface IMembershipService
    {
        int MinPasswordLength { get; }

        bool ValidateUser(string userName, string password);
        MembershipCreateStatus CreateUser(string userName, string password, string fullname,Guid deptId);
        bool ChangePassword(Guid userId, string oldPassword, string newPassword);
        bool ChangePassword(Guid userId);
        bool ChangePassword(string userName, string oldPassword, string newPassword);
        bool ChangePassword(string userName, string newPassword);
        bool ChangePassword(Guid userId, string newPassword);
        void UpdateUser(Guid userId, string fullname, Guid deptId);
        void UpdateUser(string userName, string fullname, Guid deptId);
        MembershipUser GetUser(Guid userId);
        DXInfo.Models.Depts GetDept(Guid deptId);
        bool DeleteUser(Guid userId);
        bool DeleteUser(string userName);
        void ChangeApproval(Guid userId, bool IsApproved);
        bool UnlockUser(Guid userId);
    }

    public class AccountMembershipService : IMembershipService
    {
        private readonly MembershipProvider _provider;
        [Inject]
        public IFairiesMemberManageUow Uow { get; set; }
        public AccountMembershipService()
            : this(null)
        {
        }

        public AccountMembershipService(MembershipProvider provider)
        {
            _provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength
        {
            get
            {
                return _provider.MinRequiredPasswordLength;
            }
        }

        public bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("值不能为 null 或为空。", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("值不能为 null 或为空。", "password");
            
            return _provider.ValidateUser(userName, password);
        }

        public MembershipCreateStatus CreateUser(string userName, string password, string fullName,Guid deptId)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("值不能为 null 或为空。", "userName");
            if (String.IsNullOrEmpty(password)) throw new ArgumentException("值不能为 null 或为空。", "password");
            if (String.IsNullOrEmpty(fullName)) throw new ArgumentException("值不能为 null 或为空。", "email");
            //if (deptId == Guid.Empty) throw new ArgumentException("值不能为 null 或为空。", "deptId");
            MembershipCreateStatus status;
            _provider.CreateUser(userName, password, null, null, null, true, null, out status);

            DXInfo.Profile.CustomProfile profile = DXInfo.Profile.CustomProfile.GetUserProfile(userName);            
            profile.DeptId = deptId;
            profile.FullName = fullName;
            profile.Save();

            return status;
        }
        public void UpdateUser(string userName, string fullName, Guid deptId)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentException("值不能为 null 或为空。", "userName");
            if (String.IsNullOrEmpty(fullName)) throw new ArgumentException("值不能为 null 或为空。", "fullName");
            if (deptId == Guid.Empty) throw new ArgumentException("值不能为 null 或为空。", "deptId");

            DXInfo.Profile.CustomProfile profile = DXInfo.Profile.CustomProfile.GetUserProfile(userName);
            profile.DeptId = deptId;
            profile.FullName = fullName;
            profile.Save();
        }
        public void UpdateUser(Guid userId, string fullName, Guid deptId)
        {
            if (userId==Guid.Empty) throw new ArgumentException("值不能为 null 或为空。", "userId");
            if (String.IsNullOrEmpty(fullName)) throw new ArgumentException("值不能为 null 或为空。", "fullName");
            if (deptId == Guid.Empty) throw new ArgumentException("值不能为 null 或为空。", "deptId");            
            MembershipUser user = _provider.GetUser(userId, false);
            //user.UserName = userName;
            //_provider.UpdateUser(user);

            DXInfo.Profile.CustomProfile profile = DXInfo.Profile.CustomProfile.GetUserProfile(user.UserName);
            profile.DeptId = deptId;
            profile.FullName = fullName;
            profile.Save();
            
        }
        public MembershipUser GetUser(Guid userId)
        {
            if (userId == Guid.Empty) throw new ArgumentException("值不能为 null 或为空。", "userId");
            return _provider.GetUser(userId, false);
        }

        public bool ChangePassword(Guid userId, string oldPassword, string newPassword)
        {
            if (userId == Guid.Empty) throw new ArgumentException("值不能为 null 或为空。", "userId");
            if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("值不能为 null 或为空。", "oldPassword");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("值不能为 null 或为空。", "newPassword");

            // 在某些出错情况下，基础 ChangePassword() 将引发异常，
            // 而不是返回 false。
            try
            {
                MembershipUser currentUser = _provider.GetUser(userId, true /* userIsOnline */);
                return currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }
        public bool ChangePassword(Guid userId)
        {
            if (userId == Guid.Empty) throw new ArgumentException("值不能为 null 或为空。", "userId");
            try
            {
                MembershipUser currentUser = _provider.GetUser(userId, true /* userIsOnline */);
                return currentUser.ChangePassword(currentUser.GetPassword(), "123456");

            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }
        public bool ChangePassword(Guid userId, string newPassword)
        {
            if (userId == Guid.Empty) throw new ArgumentException("值不能为 null 或为空。", "userId");
            try
            {
                MembershipUser currentUser = _provider.GetUser(userId, true /* userIsOnline */);
                return currentUser.ChangePassword(currentUser.GetPassword(), newPassword);

            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }
        public bool ChangePassword(string userName,  string newPassword)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("值不能为 null 或为空。", "userName");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("值不能为 null 或为空。", "newPassword");

            // 在某些出错情况下，基础 ChangePassword() 将引发异常，
            // 而不是返回 false。
            try
            {
                MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
                return currentUser.ChangePassword(currentUser.GetPassword(), newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }
        public bool ChangePassword(string userName,string oldPassword,  string newPassword)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("值不能为 null 或为空。", "userName");
            if (String.IsNullOrEmpty(oldPassword)) throw new ArgumentException("值不能为 null 或为空。", "oldPassword");
            if (String.IsNullOrEmpty(newPassword)) throw new ArgumentException("值不能为 null 或为空。", "newPassword");

            // 在某些出错情况下，基础 ChangePassword() 将引发异常，
            // 而不是返回 false。
            try
            {
                MembershipUser currentUser = _provider.GetUser(userName, true /* userIsOnline */);
                return currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }
        public bool UnlockUser(Guid userId)
        {
            if (userId == Guid.Empty) throw new ArgumentException("值不能为 null 或为空。", "userId");
            try
            {
                MembershipUser currentUser = _provider.GetUser(userId, true /* userIsOnline */);
                return currentUser.UnlockUser();

            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }
        public DXInfo.Models.Depts GetDept(Guid deptId)
        {
            //using (var contex = new DXInfo.Models.FairiesMemberManage())
            //{
                return Uow.Depts.GetAll().FirstOrDefault<DXInfo.Models.Depts>(dept => dept.DeptId == deptId);
            //}
        }
        public bool DeleteUser(Guid userId)
        {
            if (userId == Guid.Empty) throw new ArgumentException("值不能为 null 或为空。", "userId");
            return _provider.DeleteUser(_provider.GetUser(userId, false).UserName, true);
            //_provider.DeleteUser(userId, true);
        }
        public bool DeleteUser(string userName)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentException("值不能为 null 或为空。", "userName");
            return _provider.DeleteUser(userName, true);
        }
        public void ChangeApproval(Guid userId, bool IsApproved)
        {
            MembershipUser user = GetUser(userId);
            user.IsApproved = IsApproved;
            _provider.UpdateUser(user);
        }
        
    }

    public interface IRolesService
    {
        IEnumerable<string> FindAll();
        IEnumerable<string> FindByUser(MembershipUser user);
        IEnumerable<string> FindByUserName(string userName);
        IEnumerable<string> FindUserNamesByRole(string roleName);
        void AddToRole(MembershipUser user, string roleName);
        
        void AddToRole(string userName, string roleName);
        void AddToRole(string userName, string[] roleName);
        void RemoveFromRole(MembershipUser user, string roleName);
        void RemoveFromRole(string userName, string roleName);
        bool IsInRole(MembershipUser user, string roleName);
        bool IsInRole(string userName, string roleName);
        
        void Create(string roleName);
        //void Create(string roleName,string description);
        void Delete(string roleName);
        void Delete(Guid roleId);
    }
    public class AccountRoleService : IRolesService
    {
        private readonly DXInfo.Role.SqlRoleProvider _roleProvider;
        public AccountRoleService()
            : this(null)
        {
        }
        public AccountRoleService(RoleProvider roleProvider)
        {
            _roleProvider = roleProvider as DXInfo.Role.SqlRoleProvider ?? Roles.Provider as DXInfo.Role.SqlRoleProvider;            
        }

        #region IRolesService Members

        public IEnumerable<string> FindAll()
        {
            return _roleProvider.GetAllRoles();
        }

        public IEnumerable<string> FindByUser(MembershipUser user)
        {
            return _roleProvider.GetRolesForUser(user.UserName);
        }

        public IEnumerable<string> FindByUserName(string userName)
        {
            return _roleProvider.GetRolesForUser(userName);
        }

        public IEnumerable<string> FindUserNamesByRole(string roleName)
        {
            return _roleProvider.GetUsersInRole(roleName);
        }

        public void AddToRole(MembershipUser user, string roleName)
        {
            if (!IsInRole(user, roleName))
            _roleProvider.AddUsersToRoles(new[] { user.UserName }, new[] { roleName });
        }
        public void AddToRole(string userName, string[] roleName)
        {
            _roleProvider.AddUsersToRoles(new[] { userName },roleName);
        }
        public void AddToRole(string userName, string roleName)
        {
            _roleProvider.AddUsersToRoles(new[] { userName }, new[] { roleName });
        }
        public void RemoveFromRole(MembershipUser user, string roleName)
        {
            if (IsInRole(user, roleName))
            _roleProvider.RemoveUsersFromRoles(new[] { user.UserName }, new[] { roleName });
        }
        public void RemoveFromRole(string userName, string roleName)
        {
            _roleProvider.RemoveUsersFromRoles(new[] { userName }, new[] { roleName });
        }
        public bool IsInRole(MembershipUser user, string roleName)
        {
            return _roleProvider.IsUserInRole(user.UserName, roleName);
        }
        public bool IsInRole(string  userName, string roleName)
        {
            return _roleProvider.IsUserInRole(userName, roleName);
        }
        //public void Create(string roleName)
        //{
        //    _roleProvider.CreateRole(roleName);
        //}
        public void Create(string roleName)
        {
            _roleProvider.CreateRole(roleName);
        }
        public void Delete(Guid roleId)
        {
            _roleProvider.DeleteRole(roleId, false);
        }
        public void Delete(string roleName)
        {
            _roleProvider.DeleteRole(roleName, false);
        }

        #endregion
    }
    public interface IFormsAuthenticationService
    {
        void SignIn(string userName, bool createPersistentCookie);
        void SignOut();
    }

    public class FormsAuthenticationService : IFormsAuthenticationService
    {
        public void SignIn(string userName, bool createPersistentCookie)
        {
            if (String.IsNullOrEmpty(userName)) throw new ArgumentException("值不能为 null 或为空。", "userName");

            FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
    #endregion

    #region Validation
    public static class AccountValidation
    {
        public static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // 请参见 http://go.microsoft.com/fwlink/?LinkID=177550 以查看
            // 状态代码的完整列表。
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "用户名已存在。请另输入一个用户名。";

                case MembershipCreateStatus.DuplicateEmail:
                    return "已存在与该电子邮件地址对应的用户名。请另输入一个电子邮件地址。";

                case MembershipCreateStatus.InvalidPassword:
                    return "提供的密码无效。请输入有效的密码值。";

                case MembershipCreateStatus.InvalidEmail:
                    return "提供的电子邮件地址无效。请检查该值并重试。";

                case MembershipCreateStatus.InvalidAnswer:
                    return "提供的密码取回答案无效。请检查该值并重试。";

                case MembershipCreateStatus.InvalidQuestion:
                    return "提供的密码取回问题无效。请检查该值并重试。";

                case MembershipCreateStatus.InvalidUserName:
                    return "提供的用户名无效。请检查该值并重试。";

                case MembershipCreateStatus.ProviderError:
                    return "身份验证提供程序返回了错误。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";

                case MembershipCreateStatus.UserRejected:
                    return "已取消用户创建请求。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";

                default:
                    return "发生未知错误。请验证您的输入并重试。如果问题仍然存在，请与系统管理员联系。";
            }
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidatePasswordLengthAttribute : ValidationAttribute, IClientValidatable
    {
        private const string _defaultErrorMessage = "'{0}' 必须至少包含 {1} 个字符。";
        private readonly int _minCharacters = Membership.Provider.MinRequiredPasswordLength;

        public ValidatePasswordLengthAttribute()
            : base(_defaultErrorMessage)
        {
        }

        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentCulture, ErrorMessageString,
                name, _minCharacters);
        }

        public override bool IsValid(object value)
        {
            string valueAsString = value as string;
            return (valueAsString != null && valueAsString.Length >= _minCharacters);
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new[]{
                new ModelClientValidationStringLengthRule(FormatErrorMessage(metadata.GetDisplayName()), _minCharacters, int.MaxValue)
            };
        }
    }
    #endregion

}
