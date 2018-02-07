using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace ynhnTransportManage.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.Add(new LegacyUrlRoute());
            // routes.MapRoute("NoAction", "{controller}.mvc",
            //     new { controller = "Account", action = "Logon", id = "" });//无

            // routes.MapRoute(
            //     "Default", // 路由名称
            //     "{controller}.aspx/{action}/{id}", // 带有参数的 URL
            //     new { action = "LogOn", id = UrlParameter.Optional } // 参数默认值
            // );

            // routes.MapRoute(
            //  "Root",
            //  "",
            //  new { controller = "Account", action = "LogOn", id = UrlParameter.Optional }
            //);
            //默认匹配 
            //routes.MapRoute("Root", "", new { controller = "Account", action = "LogOn", id = "" });//根目录匹配 
            routes.MapRoute("Root", "", new { controller = "Home", action = "Index", id = "" });
            routes.MapRoute("NoID", "{controller}/{action}.aspx", new { controller = "Account", action = "Logon", id = "" });//无ID的匹配 
            routes.MapRoute("NoAction", "{controller}.aspx", new { controller = "Account", action = "Logon", id = "" });//无Action的匹配 

            routes.MapRoute("Default", "{controller}/{action}/{id}.aspx", new { controller = "Account", action = "Logon", id = "" });//默认匹配 
            //routes.MapRoute("Help", "StockManage/{action}.mht", new { Controller = "StockManage", action = "Help", id = "" });


            //routes.MapRoute(
            //    "Root", // 路由名称
            //    "", // 带有参数的 URL
            //    new { controller = "Account", action = "LogOn", id = UrlParameter.Optional } // 参数默认值
            //);
            //routes.MapRoute(
            //    "Default", // 路由名称
            //    "{controller}.aspx/{action}/{id}", // 带有参数的 URL
            //    new { controller = "Account", action = "LogOn", id = UrlParameter.Optional } // 参数默认值
            //);
            //routes.MapRoute(
            //    "AddToRole", // 路由名称
            //    "{controller}/{action}/{userName}/{roleName}", // 带有参数的 URL
            //    new { controller = "Account", action = "AddToRole",userName= "",roleName="" } // 参数默认值
            //);
            //routes.MapRoute(
            //  "Root",
            //  "",
            //  new { controller = "Account", action = "LogOn", id = UrlParameter.Optional }
            //);
        }
    }
}