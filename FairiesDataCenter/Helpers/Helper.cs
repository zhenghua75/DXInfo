using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DXInfo.Models;
using System.Web.Security;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Reflection;
using System.Globalization;
using System.Linq.Expressions;
using System.Web.Mvc.Html;
using System.Text.RegularExpressions;
using Ninject;
using DXInfo.Data.Contracts;
namespace ynhnTransportManage
{
    public class Helper
    {
        private static Common centerCommon;
        public Helper()
        {
            var uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
            centerCommon = new Common(uow);
        }
        public static DataTable LinqToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }
        
        
        //public static void SyncDept()
        //{
        //    var uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
        //    var amscmUow = DependencyResolver.Current.GetService<IAMSCMUow>();
        //    Common.SyncDept(uow, amscmUow);
        //}
        //public static void UpdatePwd(string userName, string newPwd)
        //{
        //    Common.UpdatePwd(userName, newPwd);
        //}
        //public static void SyncOper()
        //{
        //    var uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
        //    var amscmUow = DependencyResolver.Current.GetService<IAMSCMUow>();
        //    Common.SyncOper(uow, amscmUow);
        //}
        //public static void UpdateOper(string userName, string fullName, string deptCode)
        //{
        //    var uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
        //    Common.UpdateOper(userName, fullName, deptCode, uow);
        //}
        //public static void DeleteOper(string userName)
        //{
        //    var uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
        //    Common.DeleteOper(userName, uow);
        //}
        //public static void SyncGoods()
        //{
        //    var uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
        //    var amscmUow = DependencyResolver.Current.GetService<IAMSCMUow>();
        //    Common.SyncGoods(uow, amscmUow);
        //}
        //public static void UpdateGoods()
        //{
        //    var uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
        //    var amscmUow = DependencyResolver.Current.GetService<IAMSCMUow>();
        //    Common.UpdateGoods(uow, amscmUow);
        //}
    }

    /// <summary>
    /// 类属性/字段的值复制工具
    /// </summary>
    public class ClassValueCopier
    {
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="destination">目标</param>
        /// <param name="source">来源</param>
        /// <returns>成功复制的值个数</returns>
        public static int Copy(object destination, object source)
        {
            if (destination == null || source == null)
            {
                return 0;
            }
            return Copy(destination, source, source.GetType());
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="destination">目标</param>
        /// <param name="source">来源</param>
        /// <param name="type">复制的属性字段模板</param>
        /// <returns>成功复制的值个数</returns>
        public static int Copy(object destination, object source, Type type)
        {
            return Copy(destination, source, type, null);
        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="destination">目标</param>
        /// <param name="source">来源</param>
        /// <param name="type">复制的属性字段模板</param>
        /// <param name="excludeName">排除下列名称的属性不要复制</param>
        /// <returns>成功复制的值个数</returns>
        public static int Copy(object destination, object source, Type type, IEnumerable<string> excludeName)
        {
            if (destination == null || source == null)
            {
                return 0;
            }
            if (excludeName == null)
            {
                excludeName = new List<string>();
            }
            int i = 0;
            Type desType = destination.GetType();
            foreach (FieldInfo mi in type.GetFields())
            {
                if (excludeName.Contains(mi.Name))
                {
                    continue;
                }
                try
                {
                    FieldInfo des = desType.GetField(mi.Name);
                    if (des != null && des.FieldType == mi.FieldType)
                    {
                        des.SetValue(destination, mi.GetValue(source));
                        i++;
                    }

                }
                catch
                {
                }
            }

            foreach (PropertyInfo pi in type.GetProperties())
            {
                if (excludeName.Contains(pi.Name))
                {
                    continue;
                }
                try
                {
                    PropertyInfo des = desType.GetProperty(pi.Name);
                    if (des != null && des.PropertyType == pi.PropertyType && des.CanWrite && pi.CanRead)
                    {
                        des.SetValue(destination, pi.GetValue(source, null), null);
                        i++;
                    }

                }
                catch
                {
                    //throw ex;
                }
            }
            return i;
        }
    }


    #region 扩展方法 For .NET 3.0+
    /// <summary>
    /// 类属性/字段的值复制工具 扩展方法
    /// </summary>
    public static class ClassValueCopier2
    {
        /// <summary>
        /// 获取实体类的属性名称
        /// </summary>
        /// <param name="source">实体类</param>
        /// <returns>属性名称列表</returns>
        public static List<string> GetPropertyNames(this object source)
        {
            if (source == null)
            {
                return new List<string>();
            }
            return GetPropertyNames(source.GetType());
        }
        /// <summary>
        /// 获取类类型的属性名称（按声明顺序）
        /// </summary>
        /// <param name="source">类类型</param>
        /// <returns>属性名称列表</returns>
        public static List<string> GetPropertyNames(this Type source)
        {
            return GetPropertyNames(source, true);
        }
        /// <summary>
        /// 获取类类型的属性名称
        /// </summary>
        /// <param name="source">类类型</param>
        /// <param name="declarationOrder">是否按声明顺序排序</param>
        /// <returns>属性名称列表</returns>
        public static List<string> GetPropertyNames(this Type source, bool declarationOrder)
        {
            if (source == null)
            {
                return new List<string>();
            }
            var list = source.GetProperties().AsQueryable();
            if (declarationOrder)
            {
                list = list.OrderBy(p => p.MetadataToken);
            }
            return list.Select(o => o.Name).ToList(); ;
        }

        /// <summary>
        /// 从源对象赋值到当前对象
        /// </summary>
        /// <param name="destination">当前对象</param>
        /// <param name="source">源对象</param>
        /// <returns>成功复制的值个数</returns>
        public static int CopyValueFrom(this object destination, object source)
        {
            return CopyValueFrom(destination, source, null);
        }

        /// <summary>
        /// 从源对象赋值到当前对象
        /// </summary>
        /// <param name="destination">当前对象</param>
        /// <param name="source">源对象</param>
        /// <param name="excludeName">排除下列名称的属性不要复制</param>
        /// <returns>成功复制的值个数</returns>
        public static int CopyValueFrom(this object destination, object source, IEnumerable<string> excludeName)
        {
            if (destination == null || source == null)
            {
                return 0;
            }
            return ClassValueCopier.Copy(destination, source, source.GetType(), excludeName);
        }

        /// <summary>
        /// 从当前对象赋值到目标对象
        /// </summary>
        /// <param name="source">当前对象</param>
        /// <param name="destination">目标对象</param>
        /// <returns>成功复制的值个数</returns>
        public static int CopyValueTo(this object source, object destination)
        {
            return CopyValueTo(destination, source, null);
        }

        /// <summary>
        /// 从当前对象赋值到目标对象
        /// </summary>
        /// <param name="source">当前对象</param>
        /// <param name="destination">目标对象</param>
        /// <param name="excludeName">排除下列名称的属性不要复制</param>
        /// <returns>成功复制的值个数</returns>
        public static int CopyValueTo(this object source, object destination, IEnumerable<string> excludeName)
        {
            if (destination == null || source == null)
            {
                return 0;
            }
            return ClassValueCopier.Copy(destination, source, source.GetType(), excludeName);
        }

    }
    #endregion

    public static class ExpressionParseHelper
    {
        public static string GetPropertyPath<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            Match match = Regex.Match(property.ToString(), @"^[^.]+.([^()]+)$");
            return match.Groups[1].Value;
        }
    }
    public static class MyInputExtensions
    {
        public static MvcHtmlString DecimalBoxFor<TEntity>(
            this HtmlHelper helper,
            TEntity model,
            Expression<Func<TEntity, Decimal?>> property,
            string formatString, object htmlAttributes)
        {
            decimal? dec = property.Compile().Invoke(model);
            if (!dec.HasValue) dec = 0;
            // Here you can format value as you wish
            var value = !string.IsNullOrEmpty(formatString) ? dec.Value.ToString(formatString) : dec.Value.ToString();
            var name = ExpressionParseHelper.GetPropertyPath(property);

            return helper.TextBox(name, value, htmlAttributes);
        }
    }

    public static class MyExtensions
    {
        private static Common centerCommon()
        {
            var uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
            return new Common(uow);
        }
        private static DXInfo.Business.Common businessCommon()
        {
            var uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
            return new DXInfo.Business.Common(uow);
        }

        public static string DataCenterTitle(this HtmlHelper helper)
        {
            return centerCommon().GetDataCenterTitle();
        }
        public static MvcHtmlString Menu(this HtmlHelper helper)
        {
            string ret = "";
            IEnumerable<string> siteMapKeys = centerCommon().GetAllSitemapKeys(helper.ViewContext.HttpContext);
            foreach (SiteMapNode item in SiteMap.Provider.RootNode.ChildNodes)
            {
                bool ishave = false;
                if (siteMapKeys != null) { ishave = siteMapKeys.Contains<string>(item.Key); }

                if ((Convert.ToBoolean(item["IsAuthorize"]) && ishave) || !(Convert.ToBoolean(item["IsAuthorize"])) || helper.ViewContext.HttpContext.User.Identity.Name == "admin")
                {
                    if (item.ChildNodes.Count == 0)
                    {
                        ret += string.Format("<li><a href='{0}'>{1}</a>", item.Url, item.Title);
                    }
                    else
                    {
                        ret += string.Format("<li><a href='#'>{0}</a>", item.Title);
                    }
                    if (item.ChildNodes.Count > 0)
                    {
                        ret += "<ul>";
                        foreach (SiteMapNode item2 in item.ChildNodes)
                        {
                            ishave = false;
                            if (siteMapKeys != null) { ishave = siteMapKeys.Contains<string>(item2.Key); }
                            if ((Convert.ToBoolean(item2["IsAuthorize"]) && ishave) || !(Convert.ToBoolean(item2["IsAuthorize"])) || helper.ViewContext.HttpContext.User.Identity.Name == "admin")
                            {
                                if (item2.ChildNodes.Count == 0)
                                {
                                    ret += string.Format("<li><a href='{0}'>{1}</a>", item2.Url, item2.Title);
                                }
                                else
                                {
                                    ret += string.Format("<li><a href='#'>{0}</a>", item2.Title);
                                }
                                if (item2.ChildNodes.Count > 0)
                                {
                                    ret += "<ul>";
                                    foreach (SiteMapNode item3 in item2.ChildNodes)
                                    {
                                        ishave = false;
                                        if (siteMapKeys != null) { ishave = siteMapKeys.Contains<string>(item3.Key); }
                                        if ((Convert.ToBoolean(item3["IsAuthorize"]) && ishave) || !(Convert.ToBoolean(item3["IsAuthorize"])) || helper.ViewContext.HttpContext.User.Identity.Name == "admin")
                                        {
                                            ret += string.Format("<li><a href='{0}'>{1}</a></li>", item3.Url, item3.Title);
                                        }
                                    }
                                    ret += "</ul>";
                                }
                                ret += "</li>";
                            }
                        }
                        ret += "</ul>";
                    }
                    ret += "</li>";
                }
            }
            return MvcHtmlString.Create(ret);
        }
        public static string IsAMSApp(this HtmlHelper helper)
        {
            return centerCommon().IsAMSApp().ToString().ToLower();
        }
        public static bool bAMSApp(this HtmlHelper helper)
        {
            return centerCommon().IsAMSApp();
        }
        public static bool CupTypeColumnVisible(this HtmlHelper helper)
        {
            return businessCommon().CupTypeColumnVisible();
        }
        public static bool UnitOfMeasureColumnVisibility(this HtmlHelper helper)
        {
            return businessCommon().UnitOfMeasureColumnVisibility();
        }
        public static string IsNoActiveXCheck(this HtmlHelper helper)
        {
            string userName = helper.ViewContext.HttpContext.User.Identity.Name;
            return businessCommon().IsNoActiveXCheck(userName).ToString().ToLower();
        }
        public static string LogonCss(this HtmlHelper helper)
        {
            return centerCommon().LogonCss();
        }
        public static List<SelectListItem> GetCategory(this HtmlHelper helper)
        {
            return centerCommon().GetCategory(helper.ViewContext.RequestContext);
        }
        public static List<SelectListItem> GetInventory(this HtmlHelper helper)
        {
            return centerCommon().GetInventory(helper.ViewContext.RequestContext);
        }
        public static bool OperatorsOnDuty(this HtmlHelper helper)
        {
            return businessCommon().OperatorsOnDuty();
        }
        public static List<SelectListItem> GetDept(this HtmlHelper helper)
        {
            return centerCommon().GetDept(helper.ViewContext.RequestContext);
        }
        public static List<SelectListItem> GetOper(this HtmlHelper helper)
        {
            return centerCommon().GetOper(helper.ViewContext.RequestContext);
        }
        public static List<SelectListItem> GetSectionType(this HtmlHelper helper)
        {
            return centerCommon().GetSectionType();
        }
        public static List<SelectListItem> GetWarehouse(this HtmlHelper helper)
        {
            return centerCommon().GetWarehouse();
        }
        public static List<SelectListItem> GetWarehouseDept(this HtmlHelper helper)
        {
            return centerCommon().GetWarehouseDept();
        }
        public static List<SelectListItem> GetCardType(this HtmlHelper helper)
        {
            return centerCommon().GetCardType();
        }
        public static List<SelectListItem> GetCardLevel(this HtmlHelper helper)
        {
            return centerCommon().GetCardLevel();
        }
        public static List<SelectListItem> GetPayType(this HtmlHelper helper)
        {
            return centerCommon().GetPayType();
        }
        public static List<SelectListItem> GetCardStatus(this HtmlHelper helper)
        {
            return centerCommon().GetCardStatus();
        }
        public static List<SelectListItem> GetConsumeType(this HtmlHelper helper)
        {
            return centerCommon().GetConsumeType();
        }
        public static List<SelectListItem> GetCupType(this HtmlHelper helper)
        {
            return centerCommon().GetCupType();
        }
        public static List<SelectListItem> GetOrderDishStatus(this HtmlHelper helper)
        {
            return centerCommon().GetOrderDishStatus();
        }
        public static List<SelectListItem> GetOrderMenuStatus(this HtmlHelper helper)
        {
            return centerCommon().GetOrderMenuStatus();
        }

        public static List<SelectListItem> GetLocator(this HtmlHelper helper)
        {
            return centerCommon().GetLocator();
        }
        public static List<SelectListItem> GetVendor(this HtmlHelper helper)
        {
            return centerCommon().GetVendor(helper.ViewContext.RequestContext);
        }
        public static List<SelectListItem> GetPTType(this HtmlHelper helper)
        {
            return centerCommon().GetPTType();
        }
        public static List<SelectListItem> GetRdType(this HtmlHelper helper)
        {
            return centerCommon().GetRdType();
        }
        public static List<SelectListItem> GetRechargeType(this HtmlHelper helper)
        {
            return centerCommon().GetRechargeType();
        }
    }
}