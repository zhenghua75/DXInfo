using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Security;
using System.IO;
using System.Drawing;
using System.Transactions;
using System.Web;
using System.Drawing.Printing;
using DXInfo.Data.Contracts;
using AutoMapper;
using System.Web.Script.Serialization;
using System.Net;
using System.Runtime.Serialization.Json;
//using Ninject;

namespace DXInfo.WcfRestService
{
    // Start the service and browse to http://<machine_name>:<port>/Service1/help to view the service's generated help page
    // NOTE: By default, a new instance of the service is created for each call; change the InstanceContextMode to Single if you want
    // a single instance of the service to process all calls.	
    [ServiceContract]    
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    // NOTE: If the service is renamed, remember to update the global.asax.cs file
    public class Service1
    {
        private readonly IFairiesMemberManageUow uow;
        //private readonly DXInfo.Models.NameCode nc;
        private readonly Guid localDeptId;
        public Service1(IFairiesMemberManageUow uow)
        {
            this.uow = uow;
            this.localDeptId = Guid.Empty;
            DXInfo.Models.NameCode nc = uow.NameCode.GetAll().Where(w => w.Type == "LocalDept").FirstOrDefault();
            if (nc != null)
            {
                Guid.TryParse(nc.Value, out localDeptId);
            }
            Mapper.CreateMap<DXInfo.Models.OrderMenus,DXInfo.Models.OrderMenusHis>();
        }
        

        #region Sample
        //private Font printFont;
        //private void pd_PrintPage(object sender, PrintPageEventArgs ev)
        //{
        //    float linesPerPage = 0;
        //    float yPos = 0;
        //    int count = 0;
        //    float leftMargin = ev.MarginBounds.Left;
        //    float topMargin = ev.MarginBounds.Top;
        //    string line = "abcdefghijklmn";

        //    linesPerPage = ev.MarginBounds.Height /
        //       printFont.GetHeight(ev.Graphics);

        //    yPos = topMargin + (count *
        //       printFont.GetHeight(ev.Graphics));
        //    ev.Graphics.DrawString(line, printFont, Brushes.Black,
        //       leftMargin, yPos, new StringFormat());
        //    count++;
        //    if (line != null)
        //        ev.HasMorePages = true;
        //    else
        //        ev.HasMorePages = false;
        //}
        //[WebGet(UriTemplate = "")]
        //public List<SampleItem> GetCollection()
        //{
        //    List<SampleItem> ls = new List<SampleItem>();
        //    ls.Add(new SampleItem() { Id = 1, StringValue = "Hello" });
        //    ls.Add(new SampleItem() { Id = 2, StringValue = "Hello" });

        //    printFont = new Font("Arial", 10);
        //    PrintDocument pd = new PrintDocument();
        //    pd.PrintPage += new PrintPageEventHandler
        //       (this.pd_PrintPage);
        //    pd.Print();


        //    return ls;
        //}

        //[WebInvoke(UriTemplate = "", Method = "POST")]
        //public SampleItem Create(SampleItem instance)
        //{
        //    throw new NotImplementedException();
        //}

        //[WebGet(UriTemplate = "{id}")]
        //public SampleItem Get(string id)
        //{
        //    throw new NotImplementedException();
        //}

        //[WebInvoke(UriTemplate = "{id}", Method = "PUT")]
        //public SampleItem Update(string id, SampleItem instance)
        //{
        //    throw new NotImplementedException();
        //}

        //[WebInvoke(UriTemplate = "{id}", Method = "DELETE")]
        //public void Delete(string id)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion

        #region 获取
        [WebGet(UriTemplate = "GetDepts")]
        public List<DXInfo.Models.Depts> GetDepts()
        {
            var q = uow.Depts.GetAll().ToList();
            return q;
        }

        [WebGet(UriTemplate = "GetCategory")]
        public List<DXInfo.Models.InventoryCategory> GetCategory()
        {
            int CategoryType = 1;
            string type = DXInfo.Models.NameCodeType.IPadCategoryType.ToString();
            var nameCode = uow.NameCode.GetAll().Where(w => w.Type == type).FirstOrDefault();
            if (nameCode != null)
            {
                if (!string.IsNullOrEmpty(nameCode.Value))
                {
                    CategoryType = Convert.ToInt32(nameCode.Value);
                }
            }
            List<Models.InventoryCategory> lInventoryCategory =
                (from i in uow.InventoryCategory.GetAll()
                 join d in uow.CategoryDepts.GetAll() on i.Id equals d.Category
                 where d.Dept == localDeptId && i.CategoryType==CategoryType
                 select i).ToList();
            return lInventoryCategory;
        }

        [WebGet(UriTemplate = "GetInventory")]
        public List<DXInfo.Models.Inventory> GetInventory()
        {
            int InvType = 1;
            string type = DXInfo.Models.NameCodeType.IPadInvType.ToString();
            var nameCode = uow.NameCode.GetAll().Where(w => w.Type == type).FirstOrDefault();
            if (nameCode != null)
            {
                if (!string.IsNullOrEmpty(nameCode.Value))
                {
                    InvType = Convert.ToInt32(nameCode.Value);
                }
            }
            List<Models.Inventory> lInventory =
                (from i in uow.Inventory.GetAll()
                 join d in uow.InvDepts.GetAll() on i.Id equals d.Inv
                 where d.Dept == localDeptId && !i.IsInvalid && i.InvType==InvType
                 orderby i.Sort
                 select i).ToList();
            return lInventory;
        }

        [WebGet(UriTemplate = "ReadImg/{imageFileName}")]
        public System.IO.Stream ReadImg(string imageFileName)
        {
            string runDir = System.Configuration.ConfigurationManager.AppSettings["imageFilePath"];
            string imgFilePath = System.IO.Path.Combine(runDir, imageFileName);

            WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
            System.IO.FileStream fs = new System.IO.FileStream(imgFilePath, System.IO.FileMode.Open, FileAccess.Read, FileShare.Read);
            return fs;
        }

        private Boolean IsImage(string path)
        {
            try
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(path);
                img.Dispose();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        [WebGet(UriTemplate = "GetImageFileNames")]
        public List<ImgInfo> GetImageFileNames()
        {
            List<ImgInfo> lf = new List<ImgInfo>();
            string runDir = System.Configuration.ConfigurationManager.AppSettings["imageFilePath"];
            if (Directory.Exists(runDir))
            {
                string[] files = Directory.GetFiles(runDir);
                foreach (string filepath in files)
                {
                    FileInfo fi = new FileInfo(filepath);
                    if (IsImage(filepath))
                    {
                        ImgInfo ii = new ImgInfo();
                        ii.FileName = fi.Name;
                        ii.ModifyDate = fi.LastWriteTime.ToString("yyyyMMddHHmmss");
                        lf.Add(ii);
                    }
                }
            }
            return lf;
        }
        #endregion

        [WebInvoke(
            UriTemplate = "LogOn", 
            Method = "POST",
            BodyStyle=WebMessageBodyStyle.WrappedRequest)]
        public bool LogOn(string userName, string passwd)
        {
            if (string.IsNullOrWhiteSpace(userName)) throw new WebFaultException<string>("用户名不能为空",HttpStatusCode.MethodNotAllowed);
            if (string.IsNullOrWhiteSpace(passwd)) throw new WebFaultException<string>("密码不能为空",HttpStatusCode.MethodNotAllowed);
            try
            {
                if (Membership.ValidateUser(userName, passwd)) return true;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message,HttpStatusCode.MethodNotAllowed);
            }
            throw new WebFaultException<string>("用户名或密码错误", HttpStatusCode.MethodNotAllowed);
        }

        //[WebGet(UriTemplate = "OpenDeskNoUser/{deskNo}/{strquantity}")]
        //public bool OpenDeskNoUser(string deskNo, string strquantity)
        //{
        //    return OpenDesk("", deskNo, strquantity);
        //}
        //{"DeskId":"b2d27fd0-a60f-e111-8331-002264899614","DeskNo":"05","OrderDeskId":"bfbefd86-65d8-e211-9fbc-005056c00008","OrderDishId":"bebefd86-65d8-e211-9fbc-005056c00008","UserId":"27aeb029-dd72-4169-be64-44a2aeba8630"

        [WebInvoke(UriTemplate = "OpenDesk",
            Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public OrderInfo OpenDesk(string userName, string passwd, string deskNo, int quantity)
        {

            if (string.IsNullOrEmpty(deskNo))
            {
                throw new WebFaultException<string>("请输入桌台号", HttpStatusCode.MethodNotAllowed);
            }
            if (quantity == 0)
            {
                throw new WebFaultException<string>("请输入人数", HttpStatusCode.MethodNotAllowed);
            }
            if (!LogOn(userName, passwd)) return null;

            DXInfo.Models.aspnet_Users user = uow.aspnet_Users.GetAll().Where(w => w.UserName == userName).FirstOrDefault();
            if (user == null)
            {
                throw new WebFaultException<string>("无“" + userName + "”操作员信息", HttpStatusCode.MethodNotAllowed);
            }
            Guid userId = user.UserId;
            DXInfo.Models.aspnet_CustomProfile userProfile = uow.aspnet_CustomProfile.GetById(g=>g.UserId==userId);
            if (userProfile == null)
            {
                throw new WebFaultException<string>("无“" + userName + "”操作员信息", HttpStatusCode.MethodNotAllowed);
            }
            //判断桌台是否可用
            DXInfo.Models.Desks curdesk = uow.Desks.GetAll().Where(w => w.Code == deskNo &&
                w.Status == (int)DXInfo.Models.DeskStatus.InUse).FirstOrDefault();
            if (curdesk == null)
            {
                throw new WebFaultException<string>("桌台不可用", HttpStatusCode.MethodNotAllowed);
            }
            if (localDeptId == Guid.Empty)
            {
                throw new WebFaultException<string>("未获得本地部门信息", HttpStatusCode.MethodNotAllowed);
            }

            DXInfo.Restaurant.DeskManageFacade dmf = new DXInfo.Restaurant.DeskManageFacade(uow, localDeptId, userId);
            DateTime dtOperDate = DateTime.Now;
            dmf.dtOperDate = dtOperDate;
            DXInfo.Models.OrderDishes orderDish = new Models.OrderDishes();
            DXInfo.Models.OrderDeskes orderDesk = new Models.OrderDeskes();
            OrderInfo oi = new OrderInfo();
            //OrderDeskInfo odi = new OrderDeskInfo();
            try
            {
                dmf.Open(quantity, curdesk.Id, true, ref orderDish, ref orderDesk);
                oi.OrderDesk.OrderDishId = orderDish.Id;
                oi.OrderDesk.OrderDeskId = orderDesk.Id;
                oi.OrderDesk.DeskNo = deskNo;
                oi.OrderDesk.DeskId = curdesk.Id;
                oi.OrderDesk.UserId = userId;
                oi.OrderDesk.UserName = userName;
                oi.OrderDesk.FullName = userProfile.FullName;

                List<DXInfo.Models.MenuStatus> lLackMenu = (from d in uow.MenuStatus.GetAll()
                                                            where d.Dept == localDeptId &&
                                                            d.Status == (int)DXInfo.Models.OrderMenuStatus.Lack
                                                            select d).ToList();
                oi.lLackMenu = lLackMenu;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.MethodNotAllowed);
            }
            return oi;
        }

        [WebInvoke(UriTemplate = "OpenBookDesk",
            Method = "POST",
            BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public OrderInfo OpenBookDesk(string userName, string passwd,int quantity, 
            OrderBookDeskInfo orderBookDesk)
        {
            if (quantity == 0)
            {
                throw new WebFaultException<string>("请输入人数", HttpStatusCode.MethodNotAllowed);
            }
            if (!LogOn(userName, passwd)) return null;

            DXInfo.Models.aspnet_Users user = uow.aspnet_Users.GetAll().Where(w => w.UserName == userName).FirstOrDefault();
            if (user == null)
            {
                throw new WebFaultException<string>("无“" + userName + "”操作员信息", HttpStatusCode.MethodNotAllowed);
            }
            Guid userId = user.UserId;
            DXInfo.Models.aspnet_CustomProfile userProfile = uow.aspnet_CustomProfile.GetById(g=>g.UserId==userId);
            if (userProfile == null)
            {
                throw new WebFaultException<string>("无“" + userName + "”操作员信息", HttpStatusCode.MethodNotAllowed);
            }
            //判断桌台是否可用
            DXInfo.Models.Desks curdesk = uow.Desks.GetAll().Where(w => w.Id == orderBookDesk.DeskId &&
                w.Status == (int)DXInfo.Models.DeskStatus.InUse).FirstOrDefault();
            if (curdesk == null)
            {
                throw new WebFaultException<string>("桌台不可用", HttpStatusCode.MethodNotAllowed);
            }
            if (localDeptId == Guid.Empty)
            {
                throw new WebFaultException<string>("未获得本地部门信息", HttpStatusCode.MethodNotAllowed);
            }

            DXInfo.Restaurant.DeskManageFacade dmf = new DXInfo.Restaurant.DeskManageFacade(uow, localDeptId, userId);
            DateTime dtOperDate = DateTime.Now;
            dmf.dtOperDate = dtOperDate;
            DXInfo.Models.OrderDishes orderDish = new Models.OrderDishes();
            DXInfo.Models.OrderDeskes orderDesk = new Models.OrderDeskes();
            OrderInfo oi = new OrderInfo();
            try
            {
                dmf.OpenBook(orderBookDesk.OrderBookId, orderBookDesk.DeskId, quantity, true, ref orderDish, ref orderDesk);
                oi.OrderDesk.OrderDishId = orderDish.Id;
                oi.OrderDesk.OrderDeskId = orderDesk.Id;
                oi.OrderDesk.DeskNo = orderBookDesk.DeskNo;
                oi.OrderDesk.DeskId = orderBookDesk.DeskId;
                oi.OrderDesk.UserId = userId;
                oi.OrderDesk.UserName = userName;
                oi.OrderDesk.FullName = userProfile.FullName;
                List<DXInfo.Models.MenuStatus> lLackMenu = (from d in uow.MenuStatus.GetAll()
                                                            where d.Dept == localDeptId &&
                                                            d.Status == (int)DXInfo.Models.OrderMenuStatus.Lack
                                                            select d).ToList();
                oi.lLackMenu = lLackMenu;
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.MethodNotAllowed);
            }
            return oi;
        }

        [WebInvoke(UriTemplate = "Order", BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public bool Order(OrderDeskInfo orderDesk, List<OrderMenuInfo> lOrderMenu)
        {
            //if (!LogOn(userName, passwd)) return false;
            DateTime dtOperDate = DateTime.Now;
            Guid userId = Guid.Empty;
            try
            {

                using (TransactionScope transaction = new TransactionScope())
                {
                    //DXInfo.Models.aspnet_Users user = (from d in uow.aspnet_Users.GetAll() where d.UserName == userName select d).FirstOrDefault();
                    if (!orderDesk.UserId.HasValue)
                    {
                        throw new WebFaultException<string>("无“" + orderDesk.UserName + "”操作员信息", HttpStatusCode.MethodNotAllowed);
                    }
                    userId = orderDesk.UserId.Value;

                    DXInfo.Models.OrderDishes orderDish = uow.OrderDishes.GetById(g=>g.Id==orderDesk.OrderDishId);

                    if (orderDish == null)
                    {
                        throw new WebFaultException<string>("无此订单", HttpStatusCode.MethodNotAllowed);
                    }
                    if (orderDish.Status == 1 || orderDish.Status == 2)
                    {
                        throw new WebFaultException<string>("已结账或撤销的不能提交", HttpStatusCode.MethodNotAllowed);
                    }
                    foreach (OrderMenuInfo omi in lOrderMenu)
                    {
                        int lackCount = (from d in uow.MenuStatus.GetAll()
                                         where d.Dept == localDeptId && d.Inventory == omi.InvId
                                         && d.Status == (int)DXInfo.Models.OrderMenuStatus.Lack
                                         select d).Count();
                        if (lackCount > 0)
                        {
                            throw new WebFaultException<string>(omi.InvName + "缺菜不能提交", HttpStatusCode.MethodNotAllowed);
                        }

                        //string strcomment = omi.Comment;
                        //if (strcomment.Contains("null")) strcomment = "";
                        if (omi.IsAdd&&!omi.OrderMenuId.HasValue)
                        {
                            DXInfo.Models.OrderMenus orderMenu = new DXInfo.Models.OrderMenus();
                            orderMenu.UserId = userId;
                            orderMenu.OrderId = orderDesk.OrderDishId.Value;
                            orderMenu.InventoryId = omi.InvId;
                            orderMenu.Price = omi.SalePrice;
                            orderMenu.Quantity = omi.Quantity;
                            orderMenu.Amount = omi.Amount;
                            orderMenu.Comment = omi.Comment;
                            orderMenu.CreateDate = dtOperDate;
                            orderMenu.IsPackage = omi.IsPackage;
                            orderMenu.PackageId = omi.PackageId;
                            orderMenu.PackageSn = omi.PackageSn;
                            orderMenu.Status = (int)DXInfo.Models.OrderMenuStatus.Normal;
                            uow.OrderMenus.Add(orderMenu);
                            uow.Commit();

                            DXInfo.Models.OrderMenusHis omHis = Mapper.Map<DXInfo.Models.OrderMenusHis>(orderMenu);
                            omHis.LinkId = orderMenu.Id;
                            uow.OrderMenusHis.Add(omHis);

                            //orderDish.Status = 0;
                            //uow.OrderDishes.Update(orderDish);
                        }
                        else
                        {
                            if (omi.OrderMenuId.HasValue)
                            {
                                DXInfo.Models.OrderMenus orderMenu = 
                                    (from d in uow.OrderMenus.GetAll() 
                                     where d.Id == omi.OrderMenuId select d)
                                     .FirstOrDefault();

                                if (orderMenu != null && orderMenu.Status == (int)DXInfo.Models.OrderMenuStatus.Normal)
                                {
                                    if (omi.IsDelete)
                                    {
                                        uow.OrderMenus.Delete(orderMenu);
                                    }
                                    else
                                    {
                                        orderMenu.Quantity = omi.Quantity;
                                        orderMenu.Price = omi.SalePrice;
                                        orderMenu.Amount = omi.Amount;
                                        orderMenu.Comment = omi.Comment;
                                        uow.OrderMenus.Update(orderMenu);
                                    }
                                    DXInfo.Models.OrderMenusHis omHis = Mapper.Map<DXInfo.Models.OrderMenusHis>(orderMenu);
                                    omHis.LinkId = orderMenu.Id;
                                    omHis.UserId = userId;
                                    omHis.CreateDate = dtOperDate;
                                    uow.OrderMenusHis.Add(omHis);
                                }
                            }
                        }
                    }
                    uow.Commit();
                    transaction.Complete();
                }
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.MethodNotAllowed);
            }
            return true;
        }

        [WebGet(UriTemplate = "GetOrder/{userName}/{passwd}/{deskNo}")]
        public OrderInfo GetOrder(string userName, string passwd, string deskNo)
        {
            OrderInfo orderInfo = new OrderInfo();

            if (!LogOn(userName, passwd)) return null;
            DXInfo.Models.aspnet_Users user = (from d in uow.aspnet_Users.GetAll() where d.UserName == userName select d).FirstOrDefault();
            if (user == null)
            {
                throw new WebFaultException<string>("无“" + userName + "”操作员信息", HttpStatusCode.MethodNotAllowed);
            }
            orderInfo.OrderDesk.UserId = user.UserId;
            DXInfo.Models.aspnet_CustomProfile userProfile = uow.aspnet_CustomProfile.GetById(g=>g.UserId==user.UserId);
            if (userProfile == null)
            {
                throw new WebFaultException<string>("无“" + userName + "”操作员信息", HttpStatusCode.MethodNotAllowed);
            }
            orderInfo.OrderDesk.UserName = user.UserName;
            orderInfo.OrderDesk.FullName = userProfile.FullName;
            //判断桌台是否可用
            DXInfo.Models.Desks curdesk = uow.Desks.GetAll().Where(w => w.Code == deskNo).FirstOrDefault();
            if (curdesk == null)
            {
                //throw new Exception("桌台错误");
                throw new WebFaultException<string>(deskNo+"无此桌台", HttpStatusCode.MethodNotAllowed);
            }
            if (curdesk.Status != 1)
            {
                //throw new Exception("桌台不在用");
                throw new WebFaultException<string>(deskNo+"不在用", HttpStatusCode.MethodNotAllowed);
            }
            orderInfo.OrderDesk.DeskId = curdesk.Id;
            orderInfo.OrderDesk.DeskNo = deskNo;

            //判断桌台是否使用
            DXInfo.Models.OrderDeskes od = (from d in uow.OrderDeskes.GetAll() where d.DeskId == curdesk.Id && d.Status == 0 select d).FirstOrDefault();
            if (od == null)
            {
                //throw new Exception("订单错误");
                throw new WebFaultException<string>(deskNo+"未开台", HttpStatusCode.MethodNotAllowed);
            }
            orderInfo.OrderDesk.OrderDeskId = od.Id;

            DXInfo.Models.OrderDishes orderDish = (from d in uow.OrderDishes.GetAll() where d.Id == od.OrderId select d).FirstOrDefault();
            if (orderDish == null)
            {
                throw new WebFaultException<string>("无此订单", HttpStatusCode.MethodNotAllowed);
            }
            if (!(orderDish.Status == 0 || orderDish.Status == 3))
            {
                throw new WebFaultException<string>("已结账或已撤销", HttpStatusCode.MethodNotAllowed);
            }
            orderInfo.OrderDesk.OrderDishId = orderDish.Id;

            var q = from d in uow.OrderMenus.GetAll()
                    where d.OrderId == od.OrderId && !(d.Status == 1 || d.Status == 7)
                    join i in uow.Inventory.GetAll() on d.InventoryId equals i.Id into di
                    from dis in di.DefaultIfEmpty()

                    join p in uow.OrderPackages.GetAll() on d.PackageId equals p.Id into dp
                    from dps in dp.DefaultIfEmpty()
                    orderby d.Id
                    select new OrderMenuInfo()
                    {
                        OrderMenuId=d.Id,
                        InvId = d.InventoryId,
                        InvCode = dis.Code,
                        InvName = dis.Name,
                        Amount = d.Amount,
                        Quantity = d.Quantity,
                        SalePrice = d.Price,
                        //IsConfirmed=d.Status!=0?true:false,
                        //EnglishName = dis.EnglishName == null ? "" : dis.EnglishName,
                        //Comment = d.Comment==null?"":d.Comment,
                        EnglishName=dis.EnglishName,
                        Comment = d.Comment,
                        IsAdd=false,
                        IsDelete=false,
                        IsPackage = d.IsPackage,                        
                        PackageId = d.PackageId,//dps.InventoryId,//d.PackageId,    
                        PackageSn=d.PackageSn,
                        Status = d.Status,
                        //StatusName = d.Status == 0 ? "正常" :  d.Status == 2 ? "下单" : d.Status == 3 ? "缺菜" : d.Status == 4 ? "催菜" : d.Status == 5 ? "制作" : d.Status == 6 ? "出菜" : "其它",
                    };
            orderInfo.lOrderMenu = q.ToList();

            List<DXInfo.Models.MenuStatus> lLackMenu = (from d in uow.MenuStatus.GetAll()
                                                        where d.Dept == localDeptId &&
                                                        d.Status == (int)DXInfo.Models.OrderMenuStatus.Lack
                                                        select d).ToList();
                orderInfo.lLackMenu = lLackMenu;

            return orderInfo;
        }

        [WebGet(UriTemplate = "GetOrderDeskInfo")]
        public List<OrderDeskInfo> GetOrderDeskInfo()
        {
            try
            {
                var q = from d1 in uow.Desks.GetAll().Where(w => w.Status == 1)

                        join d2 in uow.OrderDeskes.GetAll().Where(w => 
                            w.Status == (int)DXInfo.Models.OrderDeskStatus.InUse) on d1.Id equals d2.DeskId into dd2
                        from dd2s in dd2.DefaultIfEmpty()

                        join d3 in uow.OrderDishes.GetAll().Where(w => 
                            w.Status == (int)DXInfo.Models.OrderDishStatus.Opened || 
                            w.Status == (int)DXInfo.Models.OrderDishStatus.Ordered) on dd2s.OrderId equals d3.Id into dd3
                        from dd3s in dd3.DefaultIfEmpty()

                        join d4 in uow.aspnet_Users.GetAll() on dd2s.UserId equals d4.UserId into dd4
                        from dd4s in dd4.DefaultIfEmpty()

                        join d5 in uow.aspnet_CustomProfile.GetAll() on dd2s.UserId equals d5.UserId into dd5
                        from dd5s in dd5.DefaultIfEmpty()

                        orderby d1.Code
                        select new OrderDeskInfo()
                        {
                            OrderDishId = dd3s.Id,
                            OrderDeskId = dd2s.Id,
                            DeskId = d1.Id,
                            DeskNo = d1.Code,
                            Status=dd3s.Status,
                            //StatusName = dd3s.Status == null ? @"空" : dd3s.Status == 3 ? @"已确认提交" : @"已开台",
                            //ImageFileName = dd3s.Status == null ? @"desk-white.png" : dd3s.Status == 3 ? @"desk-green.png" : @"desk-red.png",
                            UserId = dd2s.UserId,
                            UserName = dd4s.UserName,
                            FullName = dd5s.FullName,
                            CreateDate = dd2s.CreateDate,
                        };
                return q.ToList();
            }
            catch (Exception ex)
            {
                throw new WebFaultException<string>(ex.Message, HttpStatusCode.MethodNotAllowed);
            }
        }

        //预定信息
        [WebGet(UriTemplate = "GetOrderBookDeskInfo")]
        public List<OrderBookDeskInfo> GetOrderBookDeskInfo()
        {
            var q =
                from d1 in uow.OrderBookDeskes.GetAll()

                join d2 in uow.OrderBooks.GetAll() on d1.OrderBookId equals d2.Id into dd2
                from dd2s in dd2.DefaultIfEmpty()

                join d3 in uow.Desks.GetAll() on d1.DeskId equals d3.Id into dd3
                from dd3s in dd3.DefaultIfEmpty()

                join d4 in uow.aspnet_CustomProfile.GetAll() on d1.UserId equals d4.UserId into dd4
                from dd4s in dd4.DefaultIfEmpty()

                join d5 in uow.aspnet_Users.GetAll() on dd2s.UserId equals d5.UserId into dd5
                from dd5s in dd5.DefaultIfEmpty()

                where dd2s.Status == 0
                orderby dd2s.CreateDate
                select new OrderBookDeskInfo
                     {
                         OrderBookId = d1.OrderBookId,
                         OrderBookDeskId = d1.Id,
                         BookBeginDate = dd2s.BookBeginDate,
                         BookEndDate = dd2s.BookEndDate,
                         Comment = dd2s.Comment,
                         CreateDate = dd2s.CreateDate,
                         Customer = dd2s.Customer,
                         LinkPhone = dd2s.LinkPhone,
                         Quantity = dd2s.Quantity,
                         FullName = dd4s.FullName,
                         //DeptName = dd5s.DeptName,
                         DeskId = d1.DeskId,
                         DeskNo = dd3s.Code,
                         UserId = dd2s.UserId,
                         UserName = dd5s.UserName,
                         //Status=dd2s.Status,
                     };
            return q.ToList();
        }

        [WebGet(UriTemplate = "GetPackage/{PackageId}")]
        public List<PackageInfo> GetPackage(string PackageId)
        {
            Guid packageId;
            if (!Guid.TryParse(PackageId, out packageId))
            {
                throw new WebFaultException<string>("套餐ID错误", HttpStatusCode.MethodNotAllowed);
            }
            List<PackageInfo> lPackageInfo = (from d1 in uow.Packages.GetAll()
                                              join d2 in uow.Inventory.GetAll() on d1.InventoryId equals d2.Id into dd1
                                              from dd1s in dd1.DefaultIfEmpty()
                                              where d1.PackageId == packageId
                                              select new PackageInfo()
                                              {
                                                  Id=d1.Id,
                                                  PackageId=d1.PackageId,
                                                  InventoryId = d1.InventoryId,
                                                  Code = dd1s.Code,
                                                  Name = dd1s.Name,
                                                  SalePrice = d1.Price,
                                                  OptionalGroup = d1.OptionalGroup,
                                                  IsOptional = d1.IsOptional,
                                                  Comment=d1.Comment,
                                              }).ToList();
            return lPackageInfo;
        }

        [WebGet(UriTemplate = "GetPackages")]
        public List<PackageInfo> GetPackages()
        {
            List<PackageInfo> lPackageInfo = new List<PackageInfo>();
            if (localDeptId == Guid.Empty)
            {
                return lPackageInfo;
            }
            lPackageInfo = (from d1 in uow.Packages.GetAll()
                            join d2 in uow.Inventory.GetAll() on d1.InventoryId equals d2.Id into dd1
                            from dd1s in dd1.DefaultIfEmpty()

                            join d in uow.InvDepts.GetAll() on dd1s.Id equals d.Inv
                            where d.Dept == localDeptId

                            select new PackageInfo()
                            {
                                Id = d1.Id,
                                PackageId = d1.PackageId,
                                InventoryId = d1.InventoryId,
                                Code = dd1s.Code,
                                Name = dd1s.Name,
                                SalePrice = d1.Price,
                                OptionalGroup = d1.OptionalGroup,
                                IsOptional = d1.IsOptional,
                                Comment=d1.Comment,
                            }).ToList();
            return lPackageInfo;
        }

        
    }
}
