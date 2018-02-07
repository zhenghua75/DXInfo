using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Permissions;

namespace ynhnTransportManage.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {

            return View();
        }
        [Authorize]
        public ActionResult About()
        {
            return View();
        }

        
    }
    public class ColdDrinkShopController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }
    }
    public class DeptManageController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }
    }
    public class ReportSystemController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }
    }
}
