using System;
using System.Collections;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Xml.Linq;
using AutoMapper;
using AutoUpdate;
using DXInfo.Data;
using DXInfo.Data.Contracts;
using FairiesCoolerCash.Business;
using FairiesCoolerCash.ViewModel;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using Unity.ServiceLocation;
//using Microsoft.Practices.Unity;
using Unity;
using CommonServiceLocator;

namespace FairiesCoolerCash
{
    public partial class App : Application
    {
        #region 静态变量
        public static bool IsOpen = false;
        public static bool IsStickerPrint = false;
        public static bool IsTicket1 = true;
        public static bool IsTicket2 = true;
        public static bool IsTicket3 = false;
        public static bool IsThree = false;
        public static string OperatorsOnDuty = "";
        public static bool IsPrintOrder = true;
        #endregion

        #region 字段
        private Mutex mutex;
        private bool ownsMutex;
        private SplashScreenWindow s;
        #endregion

        
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string msg = e.Exception.Message;
            if (e.Exception.InnerException != null)
            {
                msg += "\n" + e.Exception.InnerException.Message;
                if (e.Exception.InnerException.InnerException != null)
                {
                    msg += "\n" + e.Exception.InnerException.InnerException.Message;
                    if (e.Exception.InnerException.InnerException.InnerException != null)
                    {
                        msg += "\n" + e.Exception.InnerException.InnerException.InnerException.Message;
                    }
                }
            }
            MessageBox.Show(msg, "错误信息", MessageBoxButton.OK, MessageBoxImage.Error);
            HandleException(e.Exception);
            e.Handled = true;
        }
        private void HandleException(Exception e)
        {
            try
            {
                ExceptionPolicy.HandleException(e, "Policy");                
                if (App.Current != null)
                {
                    var mainWindow = App.Current.MainWindow;
                    if (mainWindow == null)
                    {
                        DXInfo.Models.DXInfoTracer.Verbose("mainWindow == null");
                        App.Current.Shutdown();
                    }
                    else if (mainWindow != null && !mainWindow.IsVisible)
                    {
                        DXInfo.Models.DXInfoTracer.Verbose("!mainWindow.IsVisible");
                        App.Current.Shutdown();
                    }
                }
            }
            catch
            {
                MessageBox.Show("应用程序异常", "应用程序错误", MessageBoxButton.OK, MessageBoxImage.Stop);
                if (Application.Current != null)
                {
                    Application.Current.Shutdown();
                }
            }
        }
        public bool IsUpdate()
        {
            string updateUrl = string.Empty;
            string tempUpdatePath = string.Empty;
            XmlFiles updaterXmlFiles = null;
            int availableUpdate = 0;

            string localXmlFile = AppDomain.CurrentDomain.BaseDirectory + "UpdateList.xml";
            string serverXmlFile = string.Empty;

            if (!File.Exists(localXmlFile))
            {
                MessageBox.Show("配置文件不存在!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            try
            {
                //从本地读取更新配置文件信息
                updaterXmlFiles = new XmlFiles(localXmlFile);
            }
            catch
            {
                MessageBox.Show("配置文件出错!", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                //this.Close();
                return false;
            }
            //获取服务器地址
            updateUrl = updaterXmlFiles.GetNodeValue("//Url");

            AppUpdater appUpdater = new AppUpdater();
            appUpdater.UpdaterUrl = updateUrl + "/UpdateList.xml";

            //与服务器连接,下载更新配置文件
            try
            {
                tempUpdatePath = Environment.GetEnvironmentVariable("Temp") + "\\" + "_" + updaterXmlFiles.FindNode("//Application").Attributes["applicationId"].Value + "_" + "y" + "_" + "x" + "_" + "m" + "_" + "\\";
                appUpdater.DownAutoUpdateFile(tempUpdatePath);
            }
            catch
            {
                MessageBox.Show("与更新服务器连接失败,操作超时!", "程序更新提示", MessageBoxButton.OK, MessageBoxImage.Information);
                //this.Close();
                return false;

            }

            //获取更新文件列表
            Hashtable htUpdateFile = new Hashtable();

            serverXmlFile = tempUpdatePath + "\\UpdateList.xml";
            if (!File.Exists(serverXmlFile))
            {
                return false;
            }
            try
            {
                availableUpdate = appUpdater.CheckForUpdate(serverXmlFile, localXmlFile, out htUpdateFile);
            }
            catch
            {
                MessageBox.Show("服务器配置文件格式错误", "程序更新提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (availableUpdate > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void JudgeUpdate()
        {
            try
            {
                Process.Start("AutoUpdate.exe");
                Environment.Exit(1);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private DXInfo.Sync.Sync sync;
        private void UpdateConfig()
        {
            XNamespace xs = "urn:schemas-microsoft-com:asm.v1";
            string CONFIG_FILE = "FairiesCoolerCash.exe.config";
            string strConfigPath = "";
            strConfigPath = AppDomain.CurrentDomain.BaseDirectory + CONFIG_FILE;
            XElement xe1 = new XElement(xs + "assemblyIdentity", new XAttribute("name", "System.Windows.Interactivity")
                                                 , new XAttribute("publicKeyToken", "31bf3856ad364e35")
                                                 , new XAttribute("culture", "neutral"));
            XElement xe2 = new XElement(xs + "bindingRedirect", new XAttribute("oldVersion", "0.0.0.0-4.0.0.0")
                                                 , new XAttribute("newVersion", "4.0.0.0"));
            XElement xe3 = new XElement(xs + "assemblyIdentity", new XAttribute("name", "Microsoft.Practices.Unity")
                                                 , new XAttribute("publicKeyToken", "31bf3856ad364e35")
                                                 , new XAttribute("culture", "neutral"));
            XElement xe4 = new XElement(xs + "bindingRedirect", new XAttribute("oldVersion", "0.0.0.0-2.1.505.0")
                                                 , new XAttribute("newVersion", "2.1.505.0"));

            XElement xe5 = new XElement(xs + "dependentAssembly", xe1, xe2);
            XElement xe6 = new XElement(xs + "dependentAssembly", xe3, xe4);
            XElement xe7 = new XElement(xs + "assemblyBinding", xe5, xe6);

            XDocument xmldoc = XDocument.Load(strConfigPath);
            XElement element1 = xmldoc.Element("configuration");//.Element("runtime")
            int count1 = element1.Descendants("runtime").Count();
            if (count1 > 0)
            {
                XElement element2 = element1.Element("runtime");
                int count2 = element2.Descendants(xs + "assemblyBinding").Count();
                if (count2 > 0)
                {
                    XElement element3 = element2.Element(xs + "assemblyBinding");
                    int count3 = element3.Descendants(xs + "dependentAssembly").Count();
                    if (count3 > 0)
                    {

                        bool bAdd1 = true;
                        bool bAdd2 = true;
                        foreach (XElement element4 in element3.Descendants(xs + "dependentAssembly"))
                        {
                            if (element4.Descendants().Count() > 0)
                            {
                                foreach (XElement element5 in element4.Descendants())
                                {
                                    if (element5.Attributes().Where(w => w.Name == "name").Count() > 0)
                                    {
                                        if (element5.Attribute("name").Value == "System.Windows.Interactivity")
                                        {
                                            bAdd1 = false;
                                        }
                                        if (element5.Attribute("name").Value == "Microsoft.Practices.Unity")
                                        {
                                            bAdd2 = false;
                                        }
                                    }
                                }
                            }
                        }
                        if (bAdd1)
                        {
                            element3.Add(xe5);
                        }
                        if (bAdd2)
                        {
                            element3.Add(xe6);
                        }
                        if (bAdd1 || bAdd2)
                        {
                            element1.Save(strConfigPath);
                        }
                    }
                    else
                    {
                        element3.Add(xe5, xe6);
                        element1.Save(strConfigPath);
                    }
                }
                else
                {
                    element2.Add(xe7);
                    element1.Save(strConfigPath);
                }

            }
            else
            {
                XElement xe8 = new XElement("runtime", xe7);
                element1.Add(xe8);
                element1.Save(strConfigPath);
            }

            //int count4 = element1.Descendants("startup").Count();
            //XElement xe9 = new XElement("supportedRuntime", new XAttribute("version", "v4.0")
            //                                     , new XAttribute("sku", ".NETFramework,Version=v4.0"));
            //if (count4 > 0)
            //{
            //    XElement element2 = element1.Element("startup");
            //    if (element2.Attributes().Where(w => w.Name == "useLegacyV2RuntimeActivationPolicy").Count() > 0)
            //    {
            //        if (element2.Attribute("useLegacyV2RuntimeActivationPolicy").Value.ToLower() == "false")
            //        {
            //            element2.Attribute("useLegacyV2RuntimeActivationPolicy").Value = "true";
            //            element1.Save(strConfigPath);
            //        }
            //    }
            //    else
            //    {
            //        element2.SetAttributeValue("useLegacyV2RuntimeActivationPolicy", "true");
            //        element1.Save(strConfigPath);
            //    }
            //    int count5 = element2.Descendants("supportedRuntime").Count();
            //    bool bAdd1 = true;
            //    foreach (XElement element4 in element2.Descendants("supportedRuntime"))
            //    {
            //        if (element4.Attributes().Where(w => w.Name == "version").Count() > 0)
            //        {
            //            if (element4.Attribute("version").Value == "v4.0")
            //            {
            //                bAdd1 = false;
            //            }
            //        }
            //    }
            //    if (bAdd1)
            //    {
            //        element2.Add(xe9);                    
            //    }
            //    if (bAdd1 )
            //    {
            //        element1.Save(strConfigPath);
            //    }
            //}
            //else
            //{
            //    XElement xe11 = new XElement("startup", new XAttribute("useLegacyV2RuntimeActivationPolicy", "true"), xe9);//,xe10);
            //    element1.Add(xe11);
            //    element1.Save(strConfigPath);
            //}
        }
        private bool IsNullDatabase(SqlConnection conn)
        {
            bool isNull = false;
            string script = "SELECT name FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[NameCode]') AND type in (N'U')";
            using (SqlCommand cmd = new SqlCommand(script, conn))
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();
                    object obj = cmd.ExecuteScalar();
                    if (obj == null)
                    {
                        isNull = true;
                    }
                    else
                    {
                        string strNameCode = obj.ToString();
                        if (strNameCode.ToLower() != "namecode")
                        {
                            isNull = true;
                        }
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
            return isNull;
        }
        private void UpdateSqlVersionConfig()
        {
            System.Configuration.Configuration config =
              ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings.AllKeys.Contains("SqlVersion"))
            {
                config.AppSettings.Settings["SqlVersion"].Value = DXInfo.Business.Helper.SqlVersion;
            }
            else
            {
                config.AppSettings.Settings.Add("SqlVersion", DXInfo.Business.Helper.SqlVersion);
            }
            config.Save(ConfigurationSaveMode.Modified, false);
            ConfigurationManager.RefreshSection("appSettings");
        }
        private bool JudgeSqlUpdate()
        {
            bool update = true;
            if (ConfigurationManager.AppSettings.AllKeys.Contains("SqlVersion"))
            {
                string sqlversion = ConfigurationManager.AppSettings["SqlVersion"];
                if (sqlversion == DXInfo.Business.Helper.SqlVersion)
                {
                    update = false;
                }
            }
            return update;
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            JudgeOnly();
            if (!ownsMutex)
            {
                return;
            }
            UpdateConfig();
#if !DEBUG
                if (IsUpdate())
                {
                    JudgeUpdate();
                }
#endif
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FairiesMemberManage"].ConnectionString))
            {
                try
                {
                    conn.Open();
                    if (JudgeSqlUpdate())
                    {
                        DXInfo.Models.SyncTableStruct.Update(conn);
                        DisplayScreen(conn);
                        sync = DXInfo.Sync.Sync.Instance();
                        sync.ExecuteSyncSync();
                        UpdateSqlVersionConfig();
                    }
                    else
                    {
                        DisplayScreen(conn);
                    }
                    this.SetIoc();
                    //SetMapper();
                    JudgeSync(conn);
                    DisplayLogin(conn);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "错误信息", MessageBoxButton.OK, MessageBoxImage.Error);
                    HandleException(ex);
                }
                finally
                {
                    conn.Close();
                    if (s != null)
                    {
                        s.Close();
                    }
                }
            }
        }
        private void JudgeOnly()
        {
            mutex = new Mutex(true, "FairiesCoolerCash", out ownsMutex);
            if (!ownsMutex)
            {
                Application.Current.Shutdown();
                MessageBox.Show("程序已经启动了");
            }
        }
        private void DisplayScreen(SqlConnection conn)
        {
            string splashScreenImgPath = Helper.SplashScreenImgPath(conn);
            s = new SplashScreenWindow(splashScreenImgPath);
            s.Show();
        }
        private void JudgeSync(SqlConnection conn)
        {
            int localDeptCount = 0;
            int userCount = 0;
            int adminCount = 0;
            int adminNameCount = 0;
            localDeptCount = Helper.GetLocalDeptCount(conn);
            if (localDeptCount == 0)
            {
                if (MessageBox.Show(@"在Sql Server从备份还原之后，更新该数据库中的同步元数据。
若不执行此操作这个客户端数据库将不能同步，
执行了此操作后这个客户端数据库将全部重新同步。
请确保这个备份出来的客户端数据库从未执行过此操作，并已经清理过数据。
使用工具SyncOper.exe输入数字3也可达到同样效果。
是否进行这个操作？",
                    "还原的数据库的处理", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    sync = DXInfo.Sync.Sync.Instance();
                    sync.RestoreDatabase(sync.ClientConn);
                }
            }
            else
            {
                userCount = Helper.GetUsersCount(conn);
                if (userCount > 0)
                {
                    adminCount = Helper.GetAdminUserNameCount(conn);
                    if (adminCount > 0)
                    {
                        adminNameCount = Helper.GetAdminFullNameCount(conn);
                    }
                }
            }
            if (localDeptCount == 0 || userCount == 0 || adminCount == 0 || adminNameCount == 0)
            {
                sync = DXInfo.Sync.Sync.Instance();
                sync.ExecuteSyncSync();
            }
            if (localDeptCount == 0)
            {
                LocalDeptWindow ld = ServiceLocator.Current.GetInstance<LocalDeptWindow>();
                ld.ShowDialog();
            }
        }

        private IMapper SetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DXInfo.Models.Members, DXInfo.Models.MembersLog>();
                cfg.CreateMap<DXInfo.Models.Cards, DXInfo.Models.CardsLog>();
                cfg.CreateMap<DXInfo.Models.Inventory, DXInfo.Models.InventoryEx>();

                cfg.CreateMap<DXInfo.Models.OrderDeskes, DXInfo.Models.OrderDeskesHis>();
                cfg.CreateMap<DXInfo.Models.OrderMenus, DXInfo.Models.OrderMenusHis>();

                cfg.CreateMap<DXInfo.Models.OrderMenus, DXInfo.Models.Inventory>();
                //.Include<DXInfo.Models.OrderMenus, DXInfo.Models.InventoryEx>();
                cfg.CreateMap<DXInfo.Models.OrderMenus, DXInfo.Models.InventoryEx>();
                cfg.CreateMap<DXInfo.Models.BillInvLists, DXInfo.Models.InventoryEx>()
                    .ForMember(m => m.CupType, o => o.Ignore());
                cfg.CreateMap<DXInfo.Models.BillDonateInvLists, DXInfo.Models.CardDonateInventoryEx>();
                cfg.CreateMap<DXInfo.Models.InventoryEx, DXInfo.Models.OrderMenus>();

                cfg.CreateMap<DXInfo.Models.OrderBooks, DXInfo.Models.OrderBookEx>();
                cfg.CreateMap<DXInfo.Models.OrderPackages, DXInfo.Models.OrderPackagesHis>();
                cfg.CreateMap<DXInfo.Models.Desks, DXInfo.Models.DeskEx>();
                cfg.CreateMap<DXInfo.Models.InvPrice, DXInfo.Models.ConsumeInvPrice>();
                cfg.CreateMap<DXInfo.Models.InvPrice, DXInfo.Models.OrderInvPrice>();
                cfg.CreateMap<DXInfo.Models.OrderInvPrice, DXInfo.Models.InvPrice>();
                cfg.CreateMap<DXInfo.Models.Receipts, DXInfo.Models.ReceiptHis>();

                cfg.CreateMap<DXInfo.Models.OrderDishes, DXInfo.Models.OrderDishesHis>();
                cfg.CreateMap<DXInfo.Models.OrderBooks, DXInfo.Models.OrderBooksHis>();
                cfg.CreateMap<DXInfo.Models.OrderBookDeskes, DXInfo.Models.OrderBookDeskesHis>();
                cfg.CreateMap<DXInfo.Models.OrderDeskes, DXInfo.Models.OrderDeskesHis>();
                cfg.CreateMap<DXInfo.Models.OrderMenus, DXInfo.Models.OrderMenusHis>();
                cfg.CreateMap<DXInfo.Models.OrderMenusHis, DXInfo.Models.OrderMenus>();
                cfg.CreateMap<DXInfo.Models.OrderPackages, DXInfo.Models.OrderPackagesHis>();

            });

            return config.CreateMapper();

        }

        private IUnityContainer ConfigureUnityContainer()
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterInstance(SetMapper());

            container.RegisterType<DbContext, FairiesMemberManageDbContext>();
            container.RegisterType<IFairiesMemberManageUow, FairiesMemberManageUow>();

            container.RegisterType<LogOn>();
            container.RegisterType<Login>();
            container.RegisterType<LocalDeptWindow>();

            container.RegisterType<RibbonMainWindow>();
            container.RegisterType<AddMemberUserControl>();
            container.RegisterType<MemberQueryUserControl>();

            container.RegisterType<PutCardInMoneyViewModel>();
            container.RegisterType<PutCardInMondeyUserControl>();
            container.RegisterType<CardInMondeyUserControl>();
            
            container.RegisterType<CardLossUserControl>();
            container.RegisterType<CardFoundUserControl>();
            container.RegisterType<CardAddUserControl>();
            container.RegisterType<PointsExchangeUserControl>();
            container.RegisterType<BillRepeatUserControl>();
            container.RegisterType<CardConsumeUserControl>();
            container.RegisterType<DeskManageUserControl>();
            container.RegisterType<OrderBookListUserControl>();
            container.RegisterType<OrderMenuListUserControl>();
            container.RegisterType<LackMenuListUserControl>();
            container.RegisterType<MenuNoCtUserControl>();
            container.RegisterType<BarMenuUserControl>();
            container.RegisterType<Houchu2UserControl>();
            container.RegisterType<CodeDishUserControl>();
            container.RegisterType<DeskBookPayWindow>();
            container.RegisterType<WRReport2UserControl>();
            container.RegisterType<WRReport3UserControl>();
            container.RegisterType<WRReport4UserControl>();
            container.RegisterType<WRReport5UserControl>();
            container.RegisterType<WRReport7UserControl>();
            container.RegisterType<WRReport8UserControl>();
            container.RegisterType<WRReport9UserControl>();
            container.RegisterType<WRReport10UserControl>();
            container.RegisterType<WRReport11UserControl>();
            container.RegisterType<WRReport12UserControl>();
            
            container.RegisterType<Report2UserControl>();
            container.RegisterType<Report3UserControl>();
            container.RegisterType<Report4UserControl>();
            container.RegisterType<Report5UserControl>();
            container.RegisterType<Report7UserControl>();

            container.RegisterType<ImgDownloadUserControl>();
            container.RegisterType<Mp3DownLoadUserControl>();

            container.RegisterType<StockConsumeViewModel>();
            container.RegisterType<StockDeskManageViewModel>();

            container.RegisterType<OrderAddViewModel>();
            container.RegisterType<OrderQueryViewModel>();
            container.RegisterType<ReworkAddViewModel>();
            container.RegisterType<ReworkQueryViewModel>();

            //container.RegisterType<LoginViewModel>();
            return container;
        }

        private void SetIoc()
        {
            UnityServiceLocator locator = new UnityServiceLocator(ConfigureUnityContainer());
            ServiceLocator.SetLocatorProvider(() => locator);
        }
        private void DisplayLogin(SqlConnection conn)
        {
            string loginWin = Helper.LoginWin(conn);
            Type type = Type.GetType("FairiesCoolerCash." + loginWin);
            var Logon = ServiceLocator.Current.GetInstance(type);
            Window win = (Window)Logon;
            win.Title = Helper.ClientSideTitle(conn);
            App.Current.MainWindow = win;
            win.Show();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            if (sync != null)
            {
                sync.Dispose();
                sync = null;
            }

            try
            {
                FairiesMemberManageUow uow = ServiceLocator.Current.GetInstance<FairiesMemberManageUow>();
                if (uow != null)
                {
                    uow.Dispose();
                    uow = null;
                }
            }
            catch(Exception)
            {
            }
            if (mutex != null && ownsMutex)
            {
                mutex.ReleaseMutex();
                mutex.Dispose();
                mutex = null;
            }
            base.OnExit(e);

        }
    }

}
