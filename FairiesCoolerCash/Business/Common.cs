using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using AutoMapper;
using DXInfo.Data.Contracts;
using DXInfo.Models;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace FairiesCoolerCash.Business
{
    public class Helper
    {
        public static bool CardConsume(string cardNo, decimal dAmount)
        {
            StringBuilder sb = new StringBuilder(33);
            sb.Append(cardNo);
            int value = Convert.ToInt32(dAmount * 100);
            int st = -1;
//#if DEBUG
//            st = 0;
//#else
            try
            {
                st = CardRef.CoolerConsumeCard(sb, value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
//#endif
            if (st != 0)
            {
                MessageBox.Show(CardRef.GetStr(st));
                return false;
            }
            return true;
        }
        public static bool CardCancelConsume(string cardNo, decimal dAmount)
        {
            StringBuilder sb = new StringBuilder(33);
            sb.Append(cardNo);
            int value = Convert.ToInt32(dAmount * 100);
            int st = -1;
//#if DEBUG
//            st = 0;
//#else
            try
            {
                st = CardRef.CancelCoolerConsumeCard(sb, value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
//#endif
            if (st != 0)
            {
                MessageBox.Show(CardRef.GetStr(st));
                return false;
            }
            return true;
        }
        public static void HandelException(Exception ex)
        {
            ExceptionPolicy.HandleException(ex, "Policy");
        }
        public static void ShowErrorMsg(string msg)
        {
            MessageBox.Show(msg, "错误提示", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static void ShowSuccMsg(string msg)
        {
            MessageBox.Show(msg, "操作成功提示", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
        public static string ClientSideTitle(SqlConnection conn)
        {
            string title = "寻仙记冷饮收银系统";
            string type = DXInfo.Models.NameCodeType.ClientSideTitle.ToString();
            string nameCode = string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select value from NameCode where Type='{0}'", type);
            using (SqlCommand comm = new SqlCommand(sb.ToString(), conn))
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                object o = comm.ExecuteScalar();
                if (o != null)
                {
                    nameCode = o.ToString();
                }
            }
            if (!string.IsNullOrEmpty(nameCode))
            {
                title = nameCode;
            }
            else
            {
                if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("Title"))
                {
                    title = System.Configuration.ConfigurationManager.AppSettings["Title"];
                }
            }
            return title;
        }
        public static string LoginWin(SqlConnection conn)
        {
            SqlCommand comm;
            string loginWin = "LogOn";
            string type = DXInfo.Models.NameCodeType.ClientSideLoginWin.ToString();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select value from NameCode where Type='{0}'", type);
            string nameCode = string.Empty;
            using (comm = new SqlCommand(sb.ToString(), conn))
            {
                if (conn.State != System.Data.ConnectionState.Open)
                    conn.Open();
                object o = comm.ExecuteScalar();
                if (o != null)
                {
                    nameCode = o.ToString();
                }
            }
            if (!string.IsNullOrEmpty(nameCode))
            {
                loginWin = nameCode;
            }
            else
            {
                if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("LoginWin"))
                {
                    loginWin = System.Configuration.ConfigurationManager.AppSettings["LoginWin"];
                }
            }
            return loginWin;
        }
        public static string SplashScreenImgPath(SqlConnection conn)
        {
            SqlCommand comm;
            string splashScreenImgPath = "splashscreen1.png";
            string type = DXInfo.Models.NameCodeType.ClientSideSplashScreenImgPath.ToString();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select value from NameCode where Type='{0}'", type);
            string nameCode = string.Empty;
            using (comm = new SqlCommand(sb.ToString(), conn))
            {
                object o = comm.ExecuteScalar();
                if (o != null)
                {
                    nameCode = o.ToString();
                }
            }
            if (!string.IsNullOrEmpty(nameCode))
            {
                splashScreenImgPath = nameCode;
            }
            else
            {
                if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("SplashScreenImgPath"))
                {
                    splashScreenImgPath = System.Configuration.ConfigurationManager.AppSettings["SplashScreenImgPath"];
                }
            }
            return splashScreenImgPath;
        }
        public static int GetLocalDeptCount(SqlConnection conn)
        {
            string script = "select count(1) from NameCode where Type='LocalDept'";
            int count = 0;
            using (SqlCommand comm = new SqlCommand(script, conn))
            {
                object o = comm.ExecuteScalar();
                if (o != null)
                {
                    count = Convert.ToInt32(o);
                }
            }
            return count;
        }
        public static int GetUsersCount(SqlConnection conn)
        {
            string script = "select count(1) from aspnet_Users";
            int count = 0;
            using (SqlCommand comm = new SqlCommand(script, conn))
            {
                object o = comm.ExecuteScalar();
                if (o != null)
                {
                    count = Convert.ToInt32(o);
                }
            }
            return count;
        }
        public static int GetAdminUserNameCount(SqlConnection conn)
        {
            string script = "select count(1) from aspnet_Users where UserName = 'admin'";
            int count = 0;
            using (SqlCommand comm = new SqlCommand(script, conn))
            {
                object o = comm.ExecuteScalar();
                if (o != null)
                {
                    count = Convert.ToInt32(o);
                }
            }
            return count;
        }
        public static int GetAdminFullNameCount(SqlConnection conn)
        {
            string script = "select count(1) from aspnet_CustomProfile where FullName = '系统管理员'";
            int count = 0;
            using (SqlCommand comm = new SqlCommand(script, conn))
            {
                object o = comm.ExecuteScalar();
                if (o != null)
                {
                    count = Convert.ToInt32(o);
                }
            }
            return count;
        }
        public static void DecimalInput(object sender, TextCompositionEventArgs e)
        {
            var dud = (sender as Xceed.Wpf.Toolkit.DecimalUpDown);
            var decsep = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if (dud.Text != null && e.Text == decsep && dud.Text.Contains(decsep))
            {
                e.Handled = true;
                return;
            }
            int tmp;
            if (e.Text != decsep && !int.TryParse(e.Text, out tmp))
                e.Handled = true;
        }
        public static bool ResourceExists(string resourcePath)
        {
            var assembly = Assembly.GetExecutingAssembly();

            return ResourceExists(assembly, resourcePath);
        }
        public static bool ResourceExists(Assembly assembly, string resourcePath)
        {
             IEnumerable<object> resources = GetResourcePaths(assembly);
             return resources.Contains(resourcePath);
        }
        public static IEnumerable<object> GetResourcePaths(Assembly assembly)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var resourceName = assembly.GetName().Name + ".g";
            var resourceManager = new ResourceManager(resourceName, assembly);

            try
            {
                var resourceSet = resourceManager.GetResourceSet(culture, true, true);

                foreach (System.Collections.DictionaryEntry resource in resourceSet)
                {
                    yield return resource.Key;
                }
            }
            finally
            {
                resourceManager.ReleaseAllResources();
            }
        }
    }
    public class Common
    {
        private IFairiesMemberManageUow uow;
        public Common(IFairiesMemberManageUow uow)
        {
            this.uow = uow;
        }
        public string PrintTicketTitle(DXInfo.Models.NameCodeType type)
        {
            string title = "";
            switch (type)
            {
                case DXInfo.Models.NameCodeType.PrintTicketTitleOfMember:
                    title = "寻仙记,里约";
                    break;
                case DXInfo.Models.NameCodeType.PrintTicketTitle1OfCold:
                    title = "寻仙记";
                    break;
                case DXInfo.Models.NameCodeType.PrintTicketTitle1OfWR:
                    title = "小伦家傣味店";
                    break;
            }
            string strtype = type.ToString();
            DXInfo.Models.NameCode nameCode = uow.NameCode.GetAll().Where(w => w.Type == strtype).FirstOrDefault();
            if (nameCode != null)
            {
                title = nameCode.Value;
            }
            return title;
        }
        public string PrintTicketButtomTitle(DXInfo.Models.NameCodeType type)
        {
            string title = "";
            switch (type)
            {
                case DXInfo.Models.NameCodeType.PrintTicketButtomTitle:
                    title = "大理沱之源茶文化有限责任公司";
                    break;
                case DXInfo.Models.NameCodeType.PrintTicketButtomTitleOfWR:
                    title = "大理沱之源茶文化有限责任公司";
                    break;
                case DXInfo.Models.NameCodeType.ThreeButtomTitleInMoney:
                    title = "大理沱之源茶文化有限责任公司";
                    break;
            }
            string strtype = type.ToString();
            DXInfo.Models.NameCode nameCode = uow.NameCode.GetAll().Where(w => w.Type == strtype).FirstOrDefault();
            if (nameCode != null)
            {
                title = nameCode.Value;
            }
            return title;
        }
        public string ThreePrintFile(DXInfo.Models.NameCodeType type)
        {
            string fileName = "";
            switch (type)
            {
                case DXInfo.Models.NameCodeType.ThreePrintNoMemmber:
                    fileName = "Report1.rdlc";
                    break;
                case DXInfo.Models.NameCodeType.ThreePrintMemmberNoMoney:
                    fileName = "Report2.rdlc";
                    break;
                case DXInfo.Models.NameCodeType.ThreePrintMemmber:
                    fileName = "Report3.rdlc";
                    break;
                case DXInfo.Models.NameCodeType.SaleThreePrintNoMemmber:
                    fileName = "Report4.rdlc";
                    break;
                case DXInfo.Models.NameCodeType.SaleThreePrintMemmberNoMoney:
                    fileName = "Report5.rdlc";
                    break;
                case DXInfo.Models.NameCodeType.SaleThreePrintMemmber:
                    fileName = "Report6.rdlc";
                    break;
                case DXInfo.Models.NameCodeType.ThreePrintInMoney:
                    fileName = "Report7.rdlc";
                    break;
            }
            string strtype = type.ToString();
            DXInfo.Models.NameCode nameCode = uow.NameCode.GetAll().Where(w => w.Type == strtype).FirstOrDefault();
            if (nameCode != null)
            {
                fileName = nameCode.Value;
            }
            return fileName;
        }
        //
        public bool Erasing()
        {
            DXInfo.Models.NameCode nameCode = uow.NameCode.GetAll().Where(w => w.Type == "Erasing").FirstOrDefault();
            if (nameCode != null && nameCode.Value == "true")
            {
                return true;
            }
            return false;
        }
        public string ClientSideTitle()
        {
            string title = "寻仙记冷饮收银系统";
            string type = DXInfo.Models.NameCodeType.ClientSideTitle.ToString();
            var nameCode = uow.NameCode.GetAll().Where(w => w.Type == type).FirstOrDefault();
            if (nameCode != null)
            {
                title = nameCode.Value;
            }
            else
            {
                if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("Title"))
                {
                    title = System.Configuration.ConfigurationManager.AppSettings["Title"];
                }
            }
            return title;
        }
        public string BackgroundImgPath()
        {
            string backgroudImgPath = @"images\background.jpg";
            string type = DXInfo.Models.NameCodeType.ClientSideBackgroundImgPath.ToString();
            var nameCode = uow.NameCode.GetAll().Where(w => w.Type == type).FirstOrDefault();
            if (nameCode != null)
            {
                backgroudImgPath = nameCode.Value;
            }
            else
            {
                if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("BackgroundImgPath"))
                {
                    backgroudImgPath = System.Configuration.ConfigurationManager.AppSettings["BackgroundImgPath"];
                }
            }
            return backgroudImgPath;
        }
        public BitmapImage BackgroundImg()
        {
            BitmapImage image = new BitmapImage();
            if (Helper.ResourceExists("images/background.jpg"))
            {
                image.BeginInit();
                image.UriSource = new Uri("pack://application:,,,/images/background.jpg", UriKind.Absolute);
                image.EndInit();
            }
            string backgroudImgPath = @"background.jpg";
            string type = DXInfo.Models.NameCodeType.ClientSideBackgroundImgPath.ToString();
            var nameCode = uow.NameCode.GetAll().Where(w => w.Type == type).FirstOrDefault();
            if (nameCode != null)
            {
                backgroudImgPath = nameCode.Value;
            }

            string strpath = System.AppDomain.CurrentDomain.BaseDirectory;
            string filepath = System.IO.Path.Combine(strpath, backgroudImgPath);
            if (System.IO.File.Exists(filepath))
            {
                image = new BitmapImage(new Uri(filepath));
            }
            return image;
        }
        public bool DynamicRibbonMenu()
        {
            bool isDynamicRibbonMenu = false;
            string type = DXInfo.Models.NameCodeType.ClientSideRibbonMenu.ToString();
            var nameCode = uow.NameCode.GetAll().Where(w => w.Type == type).FirstOrDefault();
            if (nameCode != null)
            {
                isDynamicRibbonMenu = nameCode.Value.ToLower() == "true";
            }
            else
            {
                if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("RibbonMenu"))
                {
                    string value = System.Configuration.ConfigurationManager.AppSettings["RibbonMenu"].ToLower();
                    isDynamicRibbonMenu = value == "true";
                }
            }
            return isDynamicRibbonMenu;
        }
        public string SplashScreenImgPath()
        {
            string splashScreenImgPath = "SplashScreen1.png";
            string type = DXInfo.Models.NameCodeType.ClientSideSplashScreenImgPath.ToString();
            var nameCode = uow.NameCode.GetAll().Where(w => w.Type == type).FirstOrDefault();
            if (nameCode != null)
            {
                splashScreenImgPath = nameCode.Value;
            }
            else
            {
                if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("SplashScreenImgPath"))
                {
                    splashScreenImgPath = System.Configuration.ConfigurationManager.AppSettings["SplashScreenImgPath"];
                }
            }
            return splashScreenImgPath;
        }
        public string LoginWin()
        {
            string loginWin = "LogOn";
            string type = DXInfo.Models.NameCodeType.ClientSideLoginWin.ToString();
            var nameCode = uow.NameCode.GetAll().Where(w => w.Type == type).FirstOrDefault();
            if (nameCode != null)
            {
                loginWin = nameCode.Value;
            }
            else
            {
                if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("LoginWin"))
                {
                    loginWin = System.Configuration.ConfigurationManager.AppSettings["LoginWin"];
                }
            }
            return loginWin;
        }
        public string RemoteDownloadServer()
        {
            string remoteDownloadServer = @"http://192.168.1.5:8200";
            string type = DXInfo.Models.NameCodeType.RemoteDownloadServer.ToString();
            var nameCode = uow.NameCode.GetAll().Where(w => w.Type == type).FirstOrDefault();
            if (nameCode != null)
            {
                remoteDownloadServer = nameCode.Value;
            }
            else
            {
                if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("RemoteDownloadServer"))
                {
                    remoteDownloadServer = System.Configuration.ConfigurationManager.AppSettings["RemoteDownloadServer"];
                }
            }
            return remoteDownloadServer;
        }
        public string CardNoRule(DXInfo.Models.CardTypes cardType, out string comment)
        {
            string cardNoRule = @"^\w{1}\d{5}$";
            comment = "卡号必须以字母开头加5个数字";
            if (cardType != null && !string.IsNullOrEmpty(cardType.CardNoRule))
            {
                cardNoRule = cardType.CardNoRule;
                comment = cardType.Comment;
            }
            else
            {
                string type = DXInfo.Models.NameCodeType.CardNoRule.ToString();
                var nameCode = uow.NameCode.GetAll().Where(w => w.Type == type).FirstOrDefault();
                if (nameCode != null)
                {
                    cardNoRule = nameCode.Value;
                    comment = nameCode.Comment;
                }
            }
            return cardNoRule;
        }        
        public bool CheckUser(DXInfo.Models.aspnet_CustomProfile user)
        {
            if (user == null)
            {
                Helper.ShowErrorMsg("操作员信息错误");
                return false;
            }
            if (!user.DeptId.HasValue || user.DeptId == Guid.Empty)
            {
                Helper.ShowErrorMsg("部门信息错误");
                return false;
            }
            return true;
        }
        public bool CheckCard(DXInfo.Models.Cards card)
        {
            if (card == null)
            {
                Helper.ShowErrorMsg("未找到对应会员卡信息");
                return false;
            }
            return true;
        }
        public bool ReadCard(out string cardNo, out decimal cardBalance)
        {
            StringBuilder sb = new StringBuilder(33);
            int value = 0;
            cardNo = "";
            cardBalance = 0;
            int st = CardRef.CoolerReadCard(sb, ref value);
            if (st != 0)
            {
                MessageBox.Show(CardRef.GetStr(st));
                return false;
            }
            cardBalance = Convert.ToDecimal(value) / 100;
            cardNo = sb.ToString();
            return true;
        }
        public bool GetLocalDept(out DXInfo.Models.Depts dept)
        {
            dept = new DXInfo.Models.Depts();
            var nc = uow.NameCode.GetAll().Where(w => w.Type == "LocalDept").FirstOrDefault();
            if (nc == null)
            {
                Helper.ShowErrorMsg("请首先设置本地门店");
                return false;
            }
            if (string.IsNullOrEmpty(nc.Value))
            {
                Helper.ShowErrorMsg("本地门店信息错误");
                return false;
            }
            dept = uow.Depts.GetById(g => g.DeptId == Guid.Parse(nc.Value));
            if (dept == null) return false;
            return true;
        }
        public List<CardDonateInventoryEx> GetCardDonateInventoryEx(Guid cardId)
        {
            DateTime dtn = DateTime.Now.AddDays(-1);
            var di1 = (from d1 in uow.CardDonateInventory.GetAll().Where(w => 
                w.CardId == cardId && 
                w.IsValidate &&
                w.InvalideDate > dtn)
                       join i in uow.Inventory.GetAll() on d1.Inventory equals i.Id into d1i
                       from d1is in d1i.DefaultIfEmpty()
                       select new CardDonateInventoryEx
                       {
                           Id = d1.Id,
                           Inventory = d1.Inventory,
                           Name = d1is.Name,
                           SalePrice = d1is.SalePrice,
                           Quantity = 1,
                           Amount = d1is.SalePrice,
                           IsCheck = false
                       }).ToList();
            return di1;
        }
        public void CardDonateInventoryExDeptPrice(DXInfo.Models.Depts dept, List<CardDonateInventoryEx> di1)
        {
            if (dept != null && dept.IsDeptPrice && di1.Count > 0)
            {
                foreach (CardDonateInventoryEx qry in di1)
                {
                    var invDeptPrice = uow.InventoryDeptPrice.GetById(g => g.DeptId == dept.DeptId && g.InvId == qry.Id);
                    if (invDeptPrice != null)
                    {
                        if (invDeptPrice.SalePrice > 0)
                        {
                            qry.SalePrice = invDeptPrice.SalePrice;
                            qry.Amount = qry.SalePrice;
                        }
                    }
                }
            }
        }
        public void InventoryDeptPrice(DXInfo.Models.Depts dept, List<DXInfo.Models.Inventory> lInv)
        {
            if (dept != null && dept.IsDeptPrice)
            {
                var invDeptPrices = uow.InventoryDeptPrice.GetAll().Where(w => w.DeptId == dept.DeptId).ToList();
                foreach (DXInfo.Models.Inventory inv in lInv)
                {
                    var invDeptPrice = invDeptPrices.Find(f => f.InvId == inv.Id);
                    if (invDeptPrice != null)
                    {
                        if (invDeptPrice.SalePoint > 0) inv.SalePoint = invDeptPrice.SalePoint;
                        if (invDeptPrice.SalePoint0 > 0) inv.SalePoint0 = invDeptPrice.SalePoint0;
                        if (invDeptPrice.SalePoint1 > 0) inv.SalePoint1 = invDeptPrice.SalePoint1;
                        if (invDeptPrice.SalePoint2 > 0) inv.SalePoint2 = invDeptPrice.SalePoint2;
                        if (invDeptPrice.SalePrice > 0) inv.SalePrice = invDeptPrice.SalePrice;
                        if (invDeptPrice.SalePrice0 > 0) inv.SalePrice0 = invDeptPrice.SalePrice0;
                        if (invDeptPrice.SalePrice1 > 0) inv.SalePrice1 = invDeptPrice.SalePrice1;
                        if (invDeptPrice.SalePrice2 > 0) inv.SalePrice2 = invDeptPrice.SalePrice2;
                    }
                }
            }
        }
        public string StockVouchLocalCode()
        {
            string stockVouchLocalCode = "001";
            if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains("StockVouchLocalCode"))
            {
                stockVouchLocalCode = System.Configuration.ConfigurationManager.AppSettings["StockVouchLocalCode"];
            }
            return stockVouchLocalCode;
        }
        public bool IsNetworkAvailable()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }
        public string GetConnectorUrlString()
        {
            string remoteDownloadServer = this.RemoteDownloadServer();
            Uri baseUri = new Uri(remoteDownloadServer);
            Uri uri = new Uri(baseUri, "/Content/ckfinder/core/connector/aspx/connector.aspx");
            string connectorUrlString = uri.AbsoluteUri;
            return connectorUrlString;
        }
        public void PrintSticker(ObservableCollection<DXInfo.Models.InventoryEx> ocInventoryEx,
            string deskNo, DateTime dtOperDate, string deptName)
        {
            List<string> lPrinter = (from d in ocInventoryEx
                                     where !string.IsNullOrEmpty(d.Printer)
                                     select d.Printer
                    ).Distinct().ToList();
            foreach (string printer in lPrinter)
            {
                DXInfo.Print.PrintBatchStickerEngine stickerPrint = new DXInfo.Print.PrintBatchStickerEngine(printer, 10, 10);

                System.Drawing.Printing.PaperSize ps = stickerPrint.DefaultPageSettings.PaperSize;

                //int height = 12;
                int yPos = 0;
                ObservableCollection<DXInfo.Models.InventoryEx> oiex =
                    ocInventoryEx.Where(w => w.Printer == printer).ToObservableCollection();
                int count = Convert.ToInt32(oiex.Sum(s => s.Quantity));
                int idx = 0;
                foreach (DXInfo.Models.InventoryEx iex in oiex)
                {
                    if (iex != null && !string.IsNullOrEmpty(iex.Printer))
                    {
                        int quantity = Convert.ToInt32(iex.Quantity);
                        for (int i = 0; i < quantity; i++)
                        {
                            idx++;
                            StickerBatchPrintObject sbp = new StickerBatchPrintObject(printer, iex, deskNo,
                                                deptName, dtOperDate, idx, count);
                            stickerPrint.AddPrintObject(sbp);
                            yPos = ps.Height;// +6;
                            //yPos += height;
                            //if (idx % 20 == 0 && idx > 0)
                            //{
                                //yPos += 6;
                                //int yPos = ps.Height+6;
                                stickerPrint.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(ps.PaperName, ps.Width, yPos);
                                stickerPrint.Print();
                                stickerPrint.ClearPrintObject();
                                //yPos = 0;
                            //}
                        }
                    }
                }
                int iIdx = idx % 20;
                //if (iIdx == 1)
                //{
                //    yPos -= height;
                //}
                //else
                //{
                //    float fYpos = yPos;
                //    for (int i = 0; i < iIdx; i++)
                //    {
                //        fYpos -= height / idx;
                //    }
                //    yPos = Convert.ToInt32(Math.Round(fYpos, 0));
                //}
                //if (idx > 0)
                //{

                //    stickerPrint.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(ps.PaperName, ps.Width, ps.Height);
                //    stickerPrint.Print();
                //}

            }
        }

        #region 修改预定
        public bool OrderBook_ModifyBook(Guid orderBookId)
        {
            DXInfo.Models.OrderBooks orderBook = uow.OrderBooks.GetById(g => g.Id == orderBookId);
            if (orderBook == null)
            {
                Helper.ShowErrorMsg("未找到预订信息，不能修改");
                return false;
            }
            if (orderBook.Status != (int)DXInfo.Models.OrderBookStatus.Booked)
            {
                Helper.ShowErrorMsg("此桌台不在预定状态，不能修改");
                return false;
            }
            DeskBookWindow dqw = new DeskBookWindow(uow, true, orderBook);
            if (dqw.ShowDialog().GetValueOrDefault())
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 删除菜品
        public bool OrderDish_DeleteMenu(Guid orderMenuId)
        {
            DXInfo.Models.OrderMenus orderMenu = uow.OrderMenus.GetById(g => g.Id == orderMenuId);
            if (orderMenu == null)
            {
                Helper.ShowErrorMsg("菜品空，已被删除");
                return false;
            }
            if (orderMenu.Status != (int)DXInfo.Models.OrderMenuStatus.Normal)
            {
                Helper.ShowErrorMsg("正常状态才能删除");
                return false;
            }
            if (orderMenu.IsPackage)
            {
                List<DXInfo.Models.OrderMenus> lpkg = this.uow.OrderMenus.GetAll().Where(
                    w => w.IsPackage &&
                        w.PackageId == orderMenu.PackageId &&
                        w.PackageSn == orderMenu.PackageSn &&
                        w.OrderId == orderMenu.OrderId &&
                        w.Id != orderMenu.Id).ToList();
                foreach (DXInfo.Models.OrderMenus pkg in lpkg)
                {
                    if (pkg.Status != 0)
                    {
                        Helper.ShowErrorMsg("正常状态才能删除");
                        return false;
                    }
                    uow.OrderMenus.Delete(pkg);
                }
                DXInfo.Models.OrderPackages orderPackage = uow.OrderPackages.GetById(g => g.Id == orderMenu.PackageId);
                if (orderPackage != null)
                {
                    uow.OrderPackages.Delete(orderPackage);
                }
            }
            uow.OrderMenus.Delete(orderMenu);
            uow.Commit();
            return true;
        }
        #endregion
    }
}
