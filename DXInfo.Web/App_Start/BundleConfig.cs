using System.Web;
using System.Web.Optimization;
using System.Web.Mvc;
using DXInfo.Data.Contracts;

namespace DXInfo.Web
{
    public class BundleConfig
    {
        // 有关 Bundling 的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            //BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                       "~/Scripts/jquery-{version}.js",
                       "~/Scripts/jquery-ui-{version}.js"));
            
            bundles.Add(new StyleBundle("~/Content/logonCss").Include(
                        "~/Content/SiteLogon.css",
                        "~/Content/" + Models.Helper.LogonCss));
            
            bundles.Add(new ScriptBundle("~/bundles/MyJs").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive*",

                        "~/Scripts/jquery-ui-sliderAccess.js",
                        "~/Scripts/jquery-ui-timepicker-addon.js",
                        "~/Scripts/jquery.blockUI.js",
                        "~/Scripts/jquery.contextmenu.r2.js",
                        "~/Scripts/jquery.customSelect.js",
                        "~/Scripts/jquery.icheck.js",
                        "~/Scripts/ui.multiselect.js",
                        "~/Scripts/i18n/ui-multiselect-cn.js",
                        "~/Scripts/jquery.jqGrid.js",
                        "~/Scripts/i18n/grid.locale-cn.js",
                        //"~/Scripts/jquery.layout-{version}.js",
                        //"~/Scripts/jquery.rateit.js",
                        "~/Scripts/jquery.raty.js",
                        "~/Scripts/spin.js",
                        "~/Content/uploadify/jquery.uploadify.js",
                        "~/Scripts/chosen.jquery.js",
                        "~/Scripts/i18n/jquery-ui-timepicker-zh-CN.js",
                        "~/Scripts/i18n/jquery.ui.datepicker-zh-CN.js",
                        "~/Scripts/json2.js",
                        
                        "~/Scripts/MyJs.js"));



            bundles.Add(new StyleBundle("~/Content/MyCss").Include(                        
                        "~/Content/uploadify/uploadify.css",
                        "~/Content/chosen.css",
                        "~/Content/jquery-ui-timepicker-addon.css",
                        "~/Content/icheck/minimal/blue.css",
                        "~/Content/rateit.css",
                        "~/Content/ui.multiselect.css",
                        "~/Content/jquery.jqGrid/ui.jqgrid.css"//,
                        //"~/Content/jquery.layout-{version}.css",
                        //"~/Content/My.css"
                        ));


            bundles.Add(new StyleBundle("~/Content/StockManage").Include(
                        "~/Content/StockManage*"));
            bundles.Add(new ScriptBundle("~/bundles/StockManage").Include(
                        "~/Scripts/StockManage*"));
            // 使用 Modernizr 的开发版本进行开发和了解信息。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css",
                        "~/Content/themes/base/jquery.ui.menu.css"));

            bundles.Add(new StyleBundle("~/Content/themes/start/css").Include(
                "~/Content/themes/start/jquery-ui-1.10.3.custom*"));

            bundles.Add(new StyleBundle("~/Content/themes/south-street/css").Include(
                "~/Content/themes/south-street/jquery-ui-1.10.3.custom*"));

            bundles.Add(new StyleBundle("~/Content/themes/cupertino/css").Include(
                "~/Content/themes/cupertino/jquery-ui-1.10.3.custom*"));

            bundles.Add(new StyleBundle("~/Content/themes/redmond/css").Include(
                "~/Content/themes/redmond/jquery-ui-1.10.3.custom*"));
        }
    }
}