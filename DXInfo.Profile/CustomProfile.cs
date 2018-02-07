using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Profile;
using System.Web.Security;
using DXInfo.Models;
using DXInfo.Data.Contracts;
using Ninject;
using System.Web.Mvc;
namespace DXInfo.Profile
{
    public class CustomProfile : ProfileBase
    {
        [SettingsAllowAnonymous(false)]
        [CustomProviderData("DeptId;uniqueidentifier")]
        public Guid DeptId
        {
            get { return (Guid)this.GetPropertyValue("DeptId"); }
            set { this.SetPropertyValue("DeptId", value); }
        }

        [SettingsAllowAnonymous(false)]
        public string DeptCode
        {
            get { return (string)this.GetPropertyValue("DeptCode"); }
            set { this.SetPropertyValue("DeptCode", value); }
        }

        [SettingsAllowAnonymous(false)]
        public string DeptName
        {
            get { return (string)this.GetPropertyValue("DeptName"); }
            set { this.SetPropertyValue("DeptName", value); }
        }

        [SettingsAllowAnonymous(false)]
        [CustomProviderData("FullName;nvarchar")]
        public string FullName
        {
            get { return (string)this.GetPropertyValue("FullName"); }
            set { this.SetPropertyValue("FullName", value); }
        }

        //[SettingsAllowAnonymous(false)]
        //[CustomProviderData("Limit;int")]
        //public int Limit
        //{
        //    get { return (int)this.GetPropertyValue("Limit"); }
        //    set { this.SetPropertyValue("Limit", value); }
        //}
        public static CustomProfile GetUserProfile(string username)
        {
            CustomProfile customProfile = Create(username) as CustomProfile;
            if (customProfile.DeptId != Guid.Empty)
            {
                var Uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
                Depts dept = Uow.Depts.GetById(g => g.DeptId == customProfile.DeptId);
                if (dept != null)
                {
                    customProfile.DeptCode = dept.DeptCode;
                    customProfile.DeptName = dept.DeptName;
                }
            }
            return customProfile;
        }

        public static CustomProfile GetUserProfile()
        {
            MembershipUser u = Membership.GetUser();
            string username = "";
            if (u != null)
            {
                username = u.UserName;
            }
            CustomProfile customProfile = Create(username) as CustomProfile;
            if (customProfile.DeptId != Guid.Empty)
            {
                var Uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
                Depts dept = Uow.Depts.GetById(g => g.DeptId == customProfile.DeptId);
                if (dept != null)
                {
                    customProfile.DeptCode = dept.DeptCode;
                    customProfile.DeptName = dept.DeptName;
                }
            }
            return customProfile;
        }

    }
}
