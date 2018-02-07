using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DXInfo.Data.Contracts;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace DXInfo.Web.Models
{
    public static class MyExtensions
    {
        private static Common centerCommon()
        {
            var uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
            return new Common(uow);
        }
        //private static DXInfo.Business.Common businessCommon()
        //{
        //    var uow = DependencyResolver.Current.GetService<IFairiesMemberManageUow>();
        //    return new DXInfo.Business.Common(uow);
        //}

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
                        //ret += string.Format("<li><a href='{0}'>{1}</a>", item.Url, item.Title);
                        ret += string.Format("<h3><a href='{0}'>{1}</a></h3>", item.Url, item.Title);
                    }
                    else
                    {
                        //ret += string.Format("<li><a href='#'>{0}</a>", item.Title);
                        ret += string.Format("<h3>{0}</h3>",item.Title);
                    }
                    if (item.ChildNodes.Count > 0)
                    {
                        ret += "<div><ul class='ui-menu'>";
                        foreach (SiteMapNode item2 in item.ChildNodes)
                        {
                            ishave = false;
                            if (siteMapKeys != null) { ishave = siteMapKeys.Contains<string>(item2.Key); }
                            if ((Convert.ToBoolean(item2["IsAuthorize"]) && ishave) || !(Convert.ToBoolean(item2["IsAuthorize"])) || helper.ViewContext.HttpContext.User.Identity.Name == "admin")
                            {
                                if (item2.ChildNodes.Count == 0)
                                {
                                    //ret += string.Format("<li><a href='{0}'>{1}</a>", item2.Url, item2.Title);
                                    ret += string.Format("<li class='ui-menu-item'><a href='{0}'  onclick='addToDiv(this);return false;'>{1}</a>", item2.Url, item2.Title);
                                }
                                else
                                {
                                    //ret += string.Format("<li><a href='#'>{0}</a>", item2.Title);
                                    ret += string.Format("<li class='ui-menu-item'><a href='#'>{0}</a>", item2.Title);
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
                                            //ret += string.Format("<li><a href='{0}'>{1}</a></li>", item3.Url, item3.Title);
                                            ret += string.Format("<li class='ui-menu-item'><a href='{0}'  onclick='addToDiv(this);return false;'>{1}</a></li>", item3.Url, item3.Title);
                                        }
                                    }
                                    ret += "</ul>";
                                }
                                ret += "</li>";
                            }
                        }
                        ret += "</ul></div>";
                    }
                    else
                    {
                        ret += "<div></div>";
                    }
                    //ret += "</li>";
                }
            }
            return MvcHtmlString.Create(ret);
        }
        public static string IsAMSApp(this HtmlHelper helper)
        {
            return Helper.IsAMSApp().ToString().ToLower();
        }
        public static bool bAMSApp(this HtmlHelper helper)
        {
            return Helper.IsAMSApp();
        }
        public static bool CupTypeColumnVisible(this HtmlHelper helper)
        {
            return centerCommon().CupTypeColumnVisible();
        }
        public static bool UnitOfMeasureColumnVisibility(this HtmlHelper helper)
        {
            return centerCommon().UnitOfMeasureColumnVisibility();
        }
        public static string IsNoActiveXCheck(this HtmlHelper helper)
        {
            string userName = helper.ViewContext.HttpContext.User.Identity.Name;
            return centerCommon().IsNoActiveXCheck(userName).ToString().ToLower();
        }
        //public static string LogonCss(this HtmlHelper helper)
        //{
        //    return centerCommon().LogonCss();
        //}
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
            return centerCommon().OperatorsOnDuty();
        }
        public static List<SelectListItem> GetReglDept(this HtmlHelper helper)
        {
            return centerCommon().GetReglDept();
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
        public static List<SelectListItem> GetSupplier(this HtmlHelper helper)
        {
            return centerCommon().GetVendor((int)DXInfo.Models.VendorType.Supplier);
        }
        public static List<SelectListItem> GetReceiver(this HtmlHelper helper)
        {
            return centerCommon().GetVendor((int)DXInfo.Models.VendorType.Receiver);
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

        public static List<SelectListItem> GetBusType(this HtmlHelper helper,string vouchType)
        {
            return centerCommon().GetBusType(vouchType);
        }
        public static bool IsReceiver(this HtmlHelper helper)
        {
            return centerCommon().IsReceiver();
        }
        private static string GetPropertyPath<TEntity, TProperty>(Expression<Func<TEntity, TProperty>> property)
        {
            Match match = Regex.Match(property.ToString(), @"^[^.]+.([^()]+)$");
            return match.Groups[1].Value;
        }
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
            var name = GetPropertyPath(property);
            
            return helper.TextBox(name, value, htmlAttributes);
        }
        public static MvcHtmlString DecimalBoxFor<TEntity>(
           this HtmlHelper helper,
           TEntity model,
           Expression<Func<TEntity, Decimal?>> property,
           string formatString)
        {
            return DecimalBoxFor<TEntity>(helper, model, property, formatString, null);
        }
        //public static MvcHtmlString LinkToDiv(
        //   this System.Web.Helpers.WebGridRow row, string text)
        //{
        //    return new MvcHtmlString("<a href='" + row.GetSelectUrl() + "' onclick='addToDiv(this);'>" + text + "</a>");
        //}

        public static List<SelectListItem> GetYears(this HtmlHelper helper)
        {
            return centerCommon().GetYears();
        }
        public static List<SelectListItem> GetMonths(this HtmlHelper helper)
        {
            return centerCommon().GetMonths();
        }

        public static List<SelectListItem> GetBoolDesc(this HtmlHelper helper)
        {
            return centerCommon().GetBoolDesc();
        }
        public static List<SelectListItem> GetOrganization(this HtmlHelper helper)
        {
            return centerCommon().GetOrganization();
        }

        public static MvcForm BeginReportForm(this HtmlHelper helper, string actionName, string controllerName,ReportBaseModel rbm)
        {
            RouteValueDictionary routeValues = new RouteValueDictionary();
            routeValues.Add("InvType", rbm.InvType);
            routeValues.Add("CategoryType", rbm.CategoryType);
            routeValues.Add("DeptType", rbm.DeptType);
            return helper.BeginForm(actionName, controllerName, routeValues, FormMethod.Post, new { id = "Report", @class = "form-overflow" });
        }
        public static List<SelectListItem> GetMeasurementUnitGroup(this HtmlHelper helper)
        {
            return centerCommon().GetMeasurementUnitGroup();
        }
        public static List<SelectListItem> GetCheckCycle(this HtmlHelper helper)
        {
            return centerCommon().GetCheckCycle();
        }
        public static List<SelectListItem> GetShelfLifeType(this HtmlHelper helper)
        {
            return centerCommon().GetShelfLifeType();
        }
        public static MvcHtmlString MyValidationMessageFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression)
        {
            return helper.ValidationMessageFor<TModel, TProperty>(expression, "*必填项", new { @class="ui-state-error"});                
        }
        public static List<SelectListItem> GetAssType(this HtmlHelper helper)
        {
            return centerCommon().GetAssType();
        }
        public static List<SelectListItem> GetAssState(this HtmlHelper helper)
        {
            return centerCommon().GetAssState();
        }
        public static List<SelectListItem> GetMD(this HtmlHelper helper)
        {
            return centerCommon().GetMD();
        }
        public static List<SelectListItem> GetFlag(this HtmlHelper helper)
        {
            return centerCommon().GetFlag();
        }
        public static List<SelectListItem> GetPT(this HtmlHelper helper)
        {
            return centerCommon().GetPT();
        }
        public static List<SelectListItem> GetSpecPT(this HtmlHelper helper)
        {
            return centerCommon().GetSpecPT();
        }
        public static List<SelectListItem> GetOperName(this HtmlHelper helper)
        {
            return centerCommon().GetOperName();
        }
        public static List<SelectListItem> GetQueryType(this HtmlHelper helper)
        {
            return centerCommon().GetQueryType();
        }
        public static List<SelectListItem> GetYeanAndMonths(this HtmlHelper helper)
        {
            return centerCommon().GetYeanAndMonths();
        }
        public static List<SelectListItem> GetFillType(this HtmlHelper helper)
        {
            return centerCommon().GetFillType();
        }
        public static List<SelectListItem> GetGoodsType(this HtmlHelper helper)
        {
            return centerCommon().GetGoodsType();
        }

        public static List<SelectListItem> GetIsBalance(this HtmlHelper helper)
        {
            return centerCommon().GetIsBalance();
        }
    }
}