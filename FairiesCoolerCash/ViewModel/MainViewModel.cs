using System.Linq;
using DXInfo.Data.Contracts;
using System.Collections.Generic;
using System.Windows.Controls;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using FairiesCoolerCash.Business;
using System.Diagnostics;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
//using Microsoft.Practices.ServiceLocation;
using System.Windows.Threading;
using System.Text;
using Microsoft.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections.ObjectModel;
using System.Net;
using System.ComponentModel;
using Microsoft.Synchronization;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Synchronization.Data;
using DXInfo.Models;
using System.Configuration;
using GalaSoft.MvvmLight;
using System.Windows.Media;
using CommonServiceLocator;

namespace FairiesCoolerCash.ViewModel
{
    public class MyComparer : IEqualityComparer<DXInfo.Models.aspnet_Sitemaps>
    {
        public bool Equals(DXInfo.Models.aspnet_Sitemaps x, DXInfo.Models.aspnet_Sitemaps y)
        {
            return x.Code == y.Code;
        }

        public int GetHashCode(DXInfo.Models.aspnet_Sitemaps obj)
        {
            return obj.Code.GetHashCode();
        }
    }
    public class OperEx : DXInfo.Models.aspnet_CustomProfileEx
    {
        protected override void AfterSelect()
        {
            base.AfterSelect();
        }
    }
    public class MainViewModel : BusinessViewModelBase
    {
        #region �ֶ�
        private DispatcherTimer dTimer;
        private System.Timers.Timer timer1;
        private System.Timers.Timer timer2;
        private System.Timers.Timer timer3;
        private System.Timers.Timer timer4;
        private MyDownloadBusiness DownloadImageBusiness;
        private MyDownloadBusiness DownloadMp3Business;
        private Microsoft.Windows.Controls.Ribbon.Ribbon MyRibbon;
        private DXInfo.Sync.Sync s;
        private DXInfo.Principal.MyPrincipal MyPricipal;
        private Dictionary<string, string> dImagePath;
        private DXInfo.Restaurant.DeskManageFacade DeskManageFacade;
        #endregion

        #region ����
        public List<DXInfo.Models.aspnet_CustomProfile> lCheckedOper { get; set; }
        #endregion

        #region ����
        public MainViewModel(IFairiesMemberManageUow uow) : base(uow, new List<string>())
        {
            MyPricipal = Thread.CurrentPrincipal as DXInfo.Principal.MyPrincipal;
            SetdImagePath();
            Messenger.Default.Register<RibbonMessageToken>(this, Handle_RibbonMessageToken);
            this.SetSyncMsg();
            this.SetTimer();
            Messenger.Default.Register<CloseUserControlMessageToken>(this, Handle_CloseUserControlMessageToken);
            Messenger.Default.Register<ChangeUserControlMessageToken>(this, Handle_ChangeUserControlMessageToken);
            this.Title = ClientCommon.ClientSideTitle();
            string connectorUrlString = ClientCommon.GetConnectorUrlString();
            DownloadImageBusiness = MyDownloadBusiness.DownloadImageBusinessInstance(connectorUrlString);
            DownloadImageBusiness.DownloadMsgEvent += new MyDownloadBusiness.DownloadMsgEventHandler(downloadBusiness_DownloadMsgEvent);

            DownloadMp3Business = MyDownloadBusiness.DownloadMp3BusinessInstance(connectorUrlString);
            DownloadMp3Business.DownloadMsgEvent += new MyDownloadBusiness.DownloadMsgEventHandler(downloadBusiness_DownloadMsgEvent);

            this.lCheckedOper = new List<aspnet_CustomProfile>();
            SetOperatorsOnDuty();

            this.DeskManageFacade = new DXInfo.Restaurant.DeskManageFacade(uow, Dept.DeptId, User.UserId);
        }
        private void SetdImagePath()
        {
            dImagePath = new Dictionary<string, string>();
            dImagePath.Add("AddMember", "1372966432_Add-Male-User.png");
            dImagePath.Add("MemberQuery", "1372966512_Edit-Male-User.png");

            dImagePath.Add("PutCardInMondey", "1372972668_piggy_bank.png");
            dImagePath.Add("CardInMondey", "1372972668_piggy_bank.png");
            dImagePath.Add("CardLoss", "1372967867_Male-User-Warning.png");
            dImagePath.Add("CardFound", "1372967149_Accept-Male-User.png");
            dImagePath.Add("CardAdd", "1372966432_Add-Male-User.png");

            dImagePath.Add("PointsExchange", "1372972829_doc_convert.png");
            dImagePath.Add("BillRepeat", "1306992360_print_printer.png");
            dImagePath.Add("CardConsume", "1306992086_buy.gif");

            dImagePath.Add("DeskManage", "1372969717_table.png");
            dImagePath.Add("OrderBookList", "1372970851_appointment.png");
            dImagePath.Add("OrderMenuList", "1372970007_order.png");
            dImagePath.Add("LackMenuList", "1372972161_cup_delete.png");
            dImagePath.Add("MenuNoCt", "1372969272_user_cook.png");
            dImagePath.Add("BarMenu", "1372969429_Bartender_Male_Light.png");

            dImagePath.Add("WRReport2", "1306992444_report.png");
            dImagePath.Add("WRReport3", "1306992444_report.png");
            dImagePath.Add("WRReport4", "1306992444_report.png");
            dImagePath.Add("WRReport5", "1306992444_report.png");
            dImagePath.Add("WRReport7", "1306992444_report.png");
            dImagePath.Add("WRReport8", "1306992444_report.png");
            dImagePath.Add("WRReport9", "1306992444_report.png");
            dImagePath.Add("WRReport10", "1306992444_report.png");


            dImagePath.Add("Report2", "1306992444_report.png");
            dImagePath.Add("Report3", "1306992444_report.png");
            dImagePath.Add("Report4", "1306992444_report.png");
            dImagePath.Add("Report5", "1306992444_report.png");
            dImagePath.Add("Report7", "1306992444_report.png");

            dImagePath.Add("ImgDownload", "1306992529_ark2.png");
            dImagePath.Add("Mp3DownLoad", "1306992529_ark2.png");
            dImagePath.Add("Mp3Play", "1306992604_audio-mp3.png");
            dImagePath.Add("DataSync", "1306992686_interact.png");
            dImagePath.Add("DataBaseBackup", "1324727226_Pink_Backup_B.png");
            dImagePath.Add("RecycleCard", "1306992752_recycle_bin.png");

            dImagePath.Add("Exit", "1306992845_exit.png");
            dImagePath.Add("LogOut", "1307180404_logout.png");

        }
        void downloadBusiness_DownloadMsgEvent(object sender, DownloadMsgEventArgs e)
        {
            DownloadFileInfo dfi = e.DownQueue.CurrentFile;
            if (e.Completed)
            {
                if (e.DownQueue.QueueFile.Any())
                {
                    this.CurrentSyncOperate = dfi.FileName + "--�������";
                }
                else
                {
                    this.CurrentSyncOperate = "�ļ��������";
                }
            }
            else
            {
                this.CurrentSyncOperate = "�����ļ�--" + dfi.FileName + "--" + dfi.ProgressPercentage.ToString() + "%";
            }
        }
        private void SetSyncMsg()
        {

            s = DXInfo.Sync.Sync.Instance();
            s.SyncMsgEvent += new DXInfo.Sync.Sync.SyncMsgEventHandler(s_SyncMsgEvent);
        }
        private void s_SyncMsgEvent(object sender, SyncMsgEventArgs e)
        {
            this.CurrentSyncOperate = e.msg;
        }
        private void Handle_RibbonMessageToken(RibbonMessageToken token)
        {
            this.MyRibbon = token.MyRibbon;
            this.SetMenu();
#if !DEBUG
            CheckEkey();
#endif
        }
        private void CleanupUserControl()
        {
            if (this.MyContent != null)
            {
                if (this.MyContent.DataContext != null)
                {
                    MyViewModelBase vmb = this.MyContent.DataContext as MyViewModelBase;
                    if (vmb != null)
                    {
                        vmb.Cleanup();
                    }
                    this.MyContent.DataContext = null;
                }

                this.MyContent = null;
            }
        }
        private void Handle_CloseUserControlMessageToken(CloseUserControlMessageToken token)
        {
            CleanupUserControl();
        }
        private void Handle_ChangeUserControlMessageToken(ChangeUserControlMessageToken token)
        {
            CleanupUserControl();
            this.MyContent = token.MyContent;
        }
        private bool IsProcess(string key)
        {
            if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains(key))
            {
                string value = System.Configuration.ConfigurationManager.AppSettings[key].ToLower();
                return value == "true";
            }
            return false;
        }
        private bool IsDefaultProcess(string key)
        {
            bool isDefaultProcess = true;
            if (System.Configuration.ConfigurationManager.AppSettings.AllKeys.Contains(key))
            {
                string value = System.Configuration.ConfigurationManager.AppSettings[key].ToLower();
                isDefaultProcess = !(value == "false");
            }
            return isDefaultProcess;
        }
        private void CurrentDateTimeCallback(object obj, EventArgs arg)
        {
            this.CurrentDateTime = DateTime.Now;
        }
        private void SetTimer()
        {
            this.dTimer = new DispatcherTimer();
            this.dTimer.Interval = TimeSpan.FromSeconds(1.0);
            this.dTimer.Tick += new EventHandler(this.CurrentDateTimeCallback);
            this.dTimer.Start();

            timer1 = new System.Timers.Timer();
            timer1.Interval = 60 * 60 * 1000;
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Tick);
            timer1.Start();

            if (IsDefaultProcess("IsSyncData"))
            {
                timer2 = new System.Timers.Timer();
                timer2.Enabled = true;
                timer2.Interval = 3 * 60 * 1000;//
                timer2.Elapsed += new System.Timers.ElapsedEventHandler(timer2_Tick);
            }
            if (IsProcess("IsSyncMp3"))
            {
                timer3 = new System.Timers.Timer();
                timer3.Enabled = true;
                timer3.Interval = 2 * 60 * 60 * 1000;
                timer3.Elapsed += new System.Timers.ElapsedEventHandler(timer3_Tick);
            }
            if (IsProcess("IsSyncMap"))
            {
                timer4 = new System.Timers.Timer();
                timer4.Enabled = true;
                timer4.Interval = 1 * 60 * 1000 * 60;
                timer4.Elapsed += new System.Timers.ElapsedEventHandler(timer4_Tick);
            }
        }
        void OnGarbageCollection(object sender, EventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
        public override void LoadData()
        {
            base.LoadData();
            this.SetlOper();
        }
        #region ͼƬͬ��4
        object lockTimer4 = new object();
        void timer4_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (lockTimer4)
            {
                try
                {
                    SyncImages();
                }
                catch (Exception ex)
                {
                    ExceptionPolicy.HandleException(ex, "Policy");
                }
            }
        }
        private void SyncImages()
        {
            if (!(DownloadImageBusiness.client.IsBusy ||
                DownloadImageBusiness.IsRunBlock()))
            {
                DownloadImageBusiness.DownloadFile();
            }
        }
        #endregion

        #region mp3ͬ��3
        object lockTimer3 = new object();
        void timer3_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (lockTimer3)
            {
                try
                {
                    SyncMp3();
                }
                catch (Exception ex)
                {
                    ExceptionPolicy.HandleException(ex, "Policy");
                }
            }
        }
        private void SyncMp3()
        {
            if (!(DownloadMp3Business.client.IsBusy ||
                DownloadMp3Business.IsRunBlock()))
            {
                DownloadMp3Business.DownloadFile();
            }
        }
        #endregion

        #region ����ͬ��2
        private void SyncData()
        {
            DateTime dtOperDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));//DateTime.Now;
            this.DeskManageFacade.dtOperDate = dtOperDate;
            try
            {

                this.DeskManageFacade.cleanData();
            }
            catch (Exception ex)
            {
                Microsoft.Practices.EnterpriseLibrary.ExceptionHandling.ExceptionPolicy.HandleException(ex, "Policy");
                //throw ex;
            }

            if (!(s.IsRunBlock() ||
                s.IsRun1Block() ||
                s.IsRun2Block() ||
                s.IsRun3Block() ||
                s.IsRun4Block() ||
                s.IsRun5Block() ||
                s.IsRun6Block() ||
                s.IsRun7Block() ||
                s.IsRun8Block() ||
                s.IsRun9Block()))
            {
                s.ExcuteSync();
            }
        }
        object lockTimer2 = new object();
        void timer2_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (lockTimer2)
            {
                SyncData();
            }
        }
        #endregion

        #region ˢ���Ƿ�ע��1
        private bool CheckEkey()
        {
            StringBuilder sbHardWareID = new StringBuilder(64);
            StringBuilder sbCardNo = new StringBuilder(128);
            if (EkeyRef.GetFairiesHardwareID(sbHardWareID))
            {
                string strHId = sbHardWareID.ToString();
                var oldk = Uow.ekey.GetAll().Where(w => w.HardwareID == strHId).Where(w => w.IsUse).FirstOrDefault();
                if (oldk == null)
                {
                    if (EkeyRef.GetFairsKeyNo(sbCardNo))
                    {
                        DXInfo.Models.ekey k = new DXInfo.Models.ekey();
                        k.UserId = this.Oper.UserId;
                        k.IsUse = true;
                        k.HardwareID = sbHardWareID.ToString();
                        k.CardNo = sbCardNo.ToString().Substring(0, 9);
                        k.CreateDate = DateTime.Now;
                        Uow.ekey.Add(k);
                        Uow.Commit();
                    }
                }
            }
            if (!EkeyRef.FairsVerify())
            {
                foreach (ItemsControl ic in MyRibbon.Items)
                {
                    if (ic is RibbonTab)
                    {
                        RibbonTab rt = ic as RibbonTab;
                        string rtname = ic.Dispatcher.Invoke(new Func<string>(() => { return ic.Name; })).ToString();
                        //string rtname = ic.Dispatcher.Invoke(new Action(() => { return ic.Name; }));
                        if (rtname == "SysManage" || rtname == "ExitMenu")
                        {
                            rt.Dispatcher.Invoke(new Action(() => { rt.Visibility = System.Windows.Visibility.Visible; }));
                        }
                        else
                        {
                            rt.Dispatcher.Invoke(new Action(() => { rt.Visibility = System.Windows.Visibility.Hidden; }));
                        }
                    }
                }
                Helper.ShowErrorMsg("�����ekey!");
                return false;
            }
            return true;
        }
        object lockTimer1 = new object();
        private void timer1_Tick(object sender, System.Timers.ElapsedEventArgs e)
        {
            lock (lockTimer1)
            {
                DateTime dt = new DateTime(2011, 6, 30);
                if (DateTime.Now > dt)
                {
#if !DEBUG
                if (!CheckEkey())
                {
                    timer1.Stop();
                }
#endif
                }
            }
        }
        #endregion
        #endregion

        #region Cleanup
        public override void Cleanup()
        {
            base.Cleanup();
            s.SyncMsgEvent -= new DXInfo.Sync.Sync.SyncMsgEventHandler(s_SyncMsgEvent);
            DownloadImageBusiness.DownloadMsgEvent -= new MyDownloadBusiness.DownloadMsgEventHandler(downloadBusiness_DownloadMsgEvent);
            DownloadMp3Business.DownloadMsgEvent -= new MyDownloadBusiness.DownloadMsgEventHandler(downloadBusiness_DownloadMsgEvent);

            if (DownloadImageBusiness.client != null)
            {
                DownloadImageBusiness.client.Dispose();
                DownloadImageBusiness.client = null;
            }
            if (DownloadMp3Business.client != null)
            {
                DownloadMp3Business.client.Dispose();
                DownloadMp3Business.client = null;
            }
            if (s != null)
            {
                s.Dispose();
                s = null;
            }
            if (this.dTimer != null)
            {
                this.dTimer.Stop();
                this.dTimer = null;
            }
            if (timer1 != null)
            {
                timer1.Stop();
                timer1.Dispose();
                timer1 = null;
            }
            if (timer2 != null)
            {
                timer2.Stop();
                timer2.Dispose();
                timer2 = null;
            }
            if (timer3 != null)
            {
                timer3.Stop();
                timer3.Dispose();
                timer3 = null;
            }
            if (timer4 != null)
            {
                timer4.Stop();
                timer4.Dispose();
                timer4 = null;
            }
            Messenger.Default.Unregister<RibbonMessageToken>(this);
            Messenger.Default.Unregister<CloseUserControlMessageToken>(this);
            Messenger.Default.Unregister<ChangeUserControlMessageToken>(this);
        }
        #endregion

        #region ����˵��
        public string MemberManageHeader
        {
            get
            {
                string memberManageHeader = "��Ա����";
                if (this.MyPricipal.IsInRole("MemberManage"))
                {
                    memberManageHeader = GetSiteMapTitle("MemberManage");
                }
                return memberManageHeader;
            }
        }
        public string MemberArchivesHeader
        {
            get
            {
                string memberArchivesHeader = "��Ա��������";
                if (this.MyPricipal.IsInRole("MemberArchives"))
                {
                    memberArchivesHeader = GetSiteMapTitle("MemberArchives");
                }
                return memberArchivesHeader;
            }
        }
        public string AddMemberLabel
        {
            get
            {
                string addMemberLabel = "����»�Ա";
                if (this.MyPricipal.IsInRole("AddMember"))
                {
                    addMemberLabel = GetSiteMapTitle("AddMember");
                }
                return addMemberLabel;
            }
        }
        public string MemberQueryLabel
        {
            get
            {
                string memberQueryLable = "��Ա���ϲ�ѯ�޸�";
                if (this.MyPricipal.IsInRole("MemberQuery"))
                {
                    memberQueryLable = GetSiteMapTitle("MemberQuery");
                }
                return memberQueryLable;
            }
        }
        public string MemberCardHeader
        {
            get
            {
                string memberCardHeader = "��Ա������";
                if (this.MyPricipal.IsInRole("MemberCard"))
                {
                    memberCardHeader = GetSiteMapTitle("MemberCard");
                }
                return memberCardHeader;
            }
        }
        public string PutCardInMoneyLabel
        {
            get
            {
                string putCardInMoneyLabel = "������ֵ";
                if (this.MyPricipal.IsInRole("PutCardInMondey"))
                {
                    putCardInMoneyLabel = GetSiteMapTitle("PutCardInMondey");
                }
                return putCardInMoneyLabel;
            }
        }
        public string CardInMoneyLabel
        {
            get
            {
                string cardInMoneyLabel = "��Ա����ֵ";
                if (this.MyPricipal.IsInRole("CardInMondey"))
                {
                    cardInMoneyLabel = GetSiteMapTitle("CardInMondey");
                }
                return cardInMoneyLabel;
            }
        }
        public string CardLossLabel
        {
            get
            {
                string cardLossLabel = "��Ա����ʧ";
                if (this.MyPricipal.IsInRole("CardLoss"))
                {
                    cardLossLabel = GetSiteMapTitle("CardLoss");
                }
                return cardLossLabel;
            }
        }
        public string CardFoundLabel
        {
            get
            {
                string cardFoundLabel = "��Ա�����";
                if (this.MyPricipal.IsInRole("CardFound"))
                {
                    cardFoundLabel = GetSiteMapTitle("CardFound");
                }
                return cardFoundLabel;
            }
        }
        public string CardAddLabel
        {
            get
            {
                string cardAddLabel = "��Ա������";
                if (this.MyPricipal.IsInRole("CardAdd"))
                {
                    cardAddLabel = GetSiteMapTitle("CardAdd");
                }
                return cardAddLabel;
            }
        }
        public string TouchScreenConsumeHeader
        {
            get
            {
                string touchScreenConsumeHeader = "���������ѹ���";
                if (this.MyPricipal.IsInRole("TouchScreenConsume"))
                {
                    touchScreenConsumeHeader = GetSiteMapTitle("TouchScreenConsume");
                }
                return touchScreenConsumeHeader;
            }
        }
        public string GscGHeader
        {
            get
            {
                string gscGHeader = "���������ѹ���";
                if (this.MyPricipal.IsInRole("gscG"))
                {
                    gscGHeader = GetSiteMapTitle("gscG");
                }
                return gscGHeader;
            }
        }
        //public string NoMemberConsumeLabel
        //{
        //    get
        //    {
        //        string noMemberConsumeLabel = "�ǻ�Ա����";
        //        if (this.MyPricipal.IsInRole("NoMemberConsume"))
        //        {
        //            noMemberConsumeLabel = GetSiteMapTitle("NoMemberConsume");
        //        }
        //        return noMemberConsumeLabel;
        //    }
        //}
        public string PointsExchangeLabel
        {
            get
            {
                string pointsExchangeLabel = "��Ա���ֶһ�";
                if (this.MyPricipal.IsInRole("PointsExchange"))
                {
                    pointsExchangeLabel = GetSiteMapTitle("PointsExchange");
                }
                return pointsExchangeLabel;
            }
        }
        public string BillRepeatLabel
        {
            get
            {
                string billRepeatLabel = "СƱ�ش�";
                if (this.MyPricipal.IsInRole("BillRepeat"))
                {
                    billRepeatLabel = GetSiteMapTitle("BillRepeat");
                }
                return billRepeatLabel;
            }
        }
        public string CardConsumeLabel
        {
            get
            {
                string cardConsumeLabel = "����";
                if (this.MyPricipal.IsInRole("CardConsume"))
                {
                    cardConsumeLabel = GetSiteMapTitle("CardConsume");
                }
                return cardConsumeLabel;
            }
        }
        public string WesternRestaurantManageHeader
        {
            get
            {
                string westernRestaurantManageHeader = "����������";
                if (this.MyPricipal.IsInRole("WesternRestaurantManage"))
                {
                    westernRestaurantManageHeader = GetSiteMapTitle("WesternRestaurantManage");
                }
                return westernRestaurantManageHeader;
            }
        }
        public string WrmHeader
        {
            get
            {
                string wrmHeader = "������";
                if (this.MyPricipal.IsInRole("wrm"))
                {
                    wrmHeader = GetSiteMapTitle("wrm");
                }
                return wrmHeader;
            }
        }
        public string DeskManageLabel
        {
            get
            {
                string deskManageLabel = "��̨����";
                if (this.MyPricipal.IsInRole("DeskManage"))
                {
                    deskManageLabel = GetSiteMapTitle("DeskManage");
                }
                return deskManageLabel;
            }
        }
        public string OrderBookListLabel
        {
            get
            {
                string orderBookListLabel = "Ԥ���嵥";
                if (this.MyPricipal.IsInRole("OrderBookList"))
                {
                    orderBookListLabel = GetSiteMapTitle("OrderBookList");
                }
                return orderBookListLabel;
            }
        }
        public string OrderMenuListLabel
        {
            get
            {
                string orderMenuListLabel = "����嵥";
                if (this.MyPricipal.IsInRole("OrderMenuList"))
                {
                    orderMenuListLabel = GetSiteMapTitle("OrderMenuList");
                }
                return orderMenuListLabel;
            }
        }
        public string LackMenuListLabel
        {
            get
            {
                string lackMenuListLabel = "ȱ���嵥";
                if (this.MyPricipal.IsInRole("LackMenuList"))
                {
                    lackMenuListLabel = GetSiteMapTitle("LackMenuList");
                }
                return lackMenuListLabel;
            }
        }
        public string MenuNoCtLabel
        {
            get
            {
                string menuNoCtLabel = "�������";
                if (this.MyPricipal.IsInRole("MenuNoCt"))
                {
                    menuNoCtLabel = GetSiteMapTitle("MenuNoCt");
                }
                return menuNoCtLabel;
            }
        }
        public string Houchu2Label
        {
            get
            {
                string menuNoCtLabel = "���2����";
                if (this.MyPricipal.IsInRole("Houchu2"))
                {
                    menuNoCtLabel = GetSiteMapTitle("Houchu2");
                }
                return menuNoCtLabel;
            }
        }
        public string BarMenuLabel
        {
            get
            {
                string barMenuLabel = "��̨����";
                if (this.MyPricipal.IsInRole("BarMenu"))
                {
                    barMenuLabel = GetSiteMapTitle("BarMenu");
                }
                return barMenuLabel;
            }
        }
        public string CheckOutNoCardLabel
        {
            get
            {
                string checkOutNoCardLabel = "�޿�����";
                if (this.MyPricipal.IsInRole("CheckOutNoCard"))
                {
                    checkOutNoCardLabel = GetSiteMapTitle("CheckOutNoCard");
                }
                return checkOutNoCardLabel;
            }
        }
        public string WRReportHeader
        {
            get
            {
                string wRReportHeader = "�������������";
                if (this.MyPricipal.IsInRole("WRReport"))
                {
                    wRReportHeader = GetSiteMapTitle("WRReport");
                }
                return wRReportHeader;
            }
        }
        public string WRReportGHeader
        {
            get
            {
                string wRReportGHeader = "�������������";
                if (this.MyPricipal.IsInRole("WRReportG"))
                {
                    wRReportGHeader = GetSiteMapTitle("WRReportG");
                }
                return wRReportGHeader;
            }
        }
        public string WRReport2Label
        {
            get
            {
                string wRReport2Label = "������ϸ��ѯ";
                if (this.MyPricipal.IsInRole("WRReport2"))
                {
                    wRReport2Label = GetSiteMapTitle("WRReport2");
                }
                return wRReport2Label;
            }
        }
        public string WRReport3Label
        {
            get
            {
                string wRReport3Label = "��ֵ��ϸ��ѯ";
                if (this.MyPricipal.IsInRole("WRReport3"))
                {
                    wRReport3Label = GetSiteMapTitle("WRReport3");
                }
                return wRReport3Label;
            }
        }
        public string WRReport4Label
        {
            get
            {
                string wRReport4Label = "���ѷ���ͳ��";
                if (this.MyPricipal.IsInRole("WRReport4"))
                {
                    wRReport4Label = GetSiteMapTitle("WRReport4");
                }
                return wRReport4Label;
            }
        }
        public string WRReport5Label
        {
            get
            {
                string wRReport5Label = "��������ͳ��";
                if (this.MyPricipal.IsInRole("WRReport5"))
                {
                    wRReport5Label = GetSiteMapTitle("WRReport5");
                }
                return wRReport5Label;
            }
        }
        public string WRReport7Label
        {
            get
            {
                string wRReport7Label = "������ѯ";
                if (this.MyPricipal.IsInRole("WRReport7"))
                {
                    wRReport7Label = GetSiteMapTitle("WRReport7");
                }
                return wRReport7Label;
            }
        }
        public string WRReport8Label
        {
            get
            {
                string wRReport8Label = "���������ϸ��ѯ";
                if (this.MyPricipal.IsInRole("WRReport8"))
                {
                    wRReport8Label = GetSiteMapTitle("WRReport8");
                }
                return wRReport8Label;
            }
        }
        public string WRReport9Label
        {
            get
            {
                string wRReport9Label = "��̨������ϸ��ѯ";
                if (this.MyPricipal.IsInRole("WRReport9"))
                {
                    wRReport9Label = GetSiteMapTitle("WRReport9");
                }
                return wRReport9Label;
            }
        }
        public string WRReport10Label
        {
            get
            {
                string wRReport10Label = "ǰ��������ϸ��ѯ";
                if (this.MyPricipal.IsInRole("WRReport10"))
                {
                    wRReport10Label = GetSiteMapTitle("WRReport10");
                }
                return wRReport10Label;
            }
        }
        public string WRReport11Label
        {
            get
            {
                string wRReport8Label = "���2������ϸ��ѯ";
                if (this.MyPricipal.IsInRole("WRReport11"))
                {
                    wRReport8Label = GetSiteMapTitle("WRReport11");
                }
                return wRReport8Label;
            }
        }
        public string ReportHeader
        {
            get
            {
                string reportHeader = "�������";
                if (this.MyPricipal.IsInRole("Report"))
                {
                    reportHeader = GetSiteMapTitle("Report");
                }
                return reportHeader;
            }
        }
        public string ReportGHeader
        {
            get
            {
                string reportGHeader = "�������";
                if (this.MyPricipal.IsInRole("ReportG"))
                {
                    reportGHeader = GetSiteMapTitle("ReportG");
                }
                return reportGHeader;
            }
        }
        public string Report2Label
        {
            get
            {
                string report2Label = "������ϸ��ѯ";
                if (this.MyPricipal.IsInRole("Report2"))
                {
                    report2Label = GetSiteMapTitle("Report2");
                }
                return report2Label;
            }
        }
        public string Report3Label
        {
            get
            {
                string report3Label = "��ֵ��ϸ��ѯ";
                if (this.MyPricipal.IsInRole("Report3"))
                {
                    report3Label = GetSiteMapTitle("Report3");
                }
                return report3Label;
            }
        }
        public string Report4Label
        {
            get
            {
                string report4Label = "���ѷ���ͳ��";
                if (this.MyPricipal.IsInRole("Report4"))
                {
                    report4Label = GetSiteMapTitle("Report4");
                }
                return report4Label;
            }
        }
        public string Report5Label
        {
            get
            {
                string report5Label = "��������ͳ��";
                if (this.MyPricipal.IsInRole("Report5"))
                {
                    report5Label = GetSiteMapTitle("Report5");
                }
                return report5Label;
            }
        }
        public string Report7Label
        {
            get
            {
                string report7Label = "������ѯ";
                if (this.MyPricipal.IsInRole("Report7"))
                {
                    report7Label = GetSiteMapTitle("Report7");
                }
                return report7Label;
            }
        }
        public string SysManageHeader
        {
            get
            {
                string sysManageHeader = "ϵͳ����";
                if (this.MyPricipal.IsInRole("SysManage"))
                {
                    sysManageHeader = GetSiteMapTitle("SysManage");
                }
                return sysManageHeader;
            }
        }
        public string SysGHeader
        {
            get
            {
                string sysGHeader = "ϵͳ����";
                if (this.MyPricipal.IsInRole("SysG"))
                {
                    sysGHeader = GetSiteMapTitle("SysG");
                }
                return sysGHeader;
            }
        }
        public string ImgDownloadLabel
        {
            get
            {
                string imgDownloadLabel = "��ƷͼƬ����";
                if (this.MyPricipal.IsInRole("ImgDownload"))
                {
                    imgDownloadLabel = GetSiteMapTitle("ImgDownload");
                }
                return imgDownloadLabel;
            }
        }
        public string Mp3DownLoadLabel
        {
            get
            {
                string mp3DownLoadLabel = "MP3����";
                if (this.MyPricipal.IsInRole("Mp3DownLoad"))
                {
                    mp3DownLoadLabel = GetSiteMapTitle("Mp3DownLoad");
                }
                return mp3DownLoadLabel;
            }
        }
        public string Mp3PlayLabel
        {
            get
            {
                string mp3PlayLabel = "MP3����";
                if (this.MyPricipal.IsInRole("Mp3Play"))
                {
                    mp3PlayLabel = GetSiteMapTitle("Mp3Play");
                }
                return mp3PlayLabel;
            }
        }
        public string DataSyncLabel
        {
            get
            {
                string dataSyncLabel = "����ͬ��";
                if (this.MyPricipal.IsInRole("DataSync"))
                {
                    dataSyncLabel = GetSiteMapTitle("DataSync");
                }
                return dataSyncLabel;
            }
        }
        public string RecycleCardLabel
        {
            get
            {
                string recycleCardLabel = "������";
                if (this.MyPricipal.IsInRole("RecycleCard"))
                {
                    recycleCardLabel = GetSiteMapTitle("RecycleCard");
                }
                return recycleCardLabel;
            }
        }
        public string PutCardLabel
        {
            get
            {
                string recycleCardLabel = "����";
                if (this.MyPricipal.IsInRole("PutCard"))
                {
                    recycleCardLabel = GetSiteMapTitle("PutCard");
                }
                return recycleCardLabel;
            }
        }
        public string ChkIsOpenLabel
        {
            get
            {
                string chkIsOpen = "���������";
                if (this.MyPricipal.IsInRole("chkIsOpen"))
                {
                    chkIsOpen = GetSiteMapTitle("chkIsOpen");
                }
                return chkIsOpen;
            }
        }
        public string ChkIsStickerPrintLabel
        {
            get
            {
                string chkIsStickerPrintLabel = "��ӡ���ɽ�";
                if (this.MyPricipal.IsInRole("ChkIsStickerPrintLabel"))
                {
                    chkIsStickerPrintLabel = GetSiteMapTitle("ChkIsStickerPrintLabel");
                }
                return chkIsStickerPrintLabel;
            }
        }
        public string ChkIsTicket1Label
        {
            get
            {
                string chkIsTicket1Label = "������";
                if (this.MyPricipal.IsInRole("ChkIsTicket1Label"))
                {
                    chkIsTicket1Label = GetSiteMapTitle("ChkIsTicket1Label");
                }
                return chkIsTicket1Label;
            }
        }
        public string ChkIsTicket2Label
        {
            get
            {
                string chkIsTicket2Label = "��Ա�ͻ���";
                if (this.MyPricipal.IsInRole("ChkIsTicket2Label"))
                {
                    chkIsTicket2Label = GetSiteMapTitle("ChkIsTicket2Label");
                }
                return chkIsTicket2Label;
            }
        }
        public string ChkIsTicket3Label
        {
            get
            {
                string chkIsTicket3Label = "���ۿͻ���";
                if (this.MyPricipal.IsInRole("ChkIsTicket3Label"))
                {
                    chkIsTicket3Label = GetSiteMapTitle("ChkIsTicket3Label");
                }
                return chkIsTicket3Label;
            }
        }
        public string ChkIsThreeLabel
        {
            get
            {
                string chkIsThreeLabel = "��ӡ������";
                if (this.MyPricipal.IsInRole("ChkIsThreeLabel"))
                {
                    chkIsThreeLabel = GetSiteMapTitle("ChkIsThreeLabel");
                }
                return chkIsThreeLabel;
            }
        }
        public string ChkIsPrintOrderLabel
        {
            get
            {
                string chkIsPrintOrderLabel = "�µ���ӡ";
                if (this.MyPricipal.IsInRole("ChkIsPrintOrderLabel"))
                {
                    chkIsPrintOrderLabel = GetSiteMapTitle("ChkIsPrintOrderLabel");
                }
                return chkIsPrintOrderLabel;
            }
        }
        public string DataBaseBackupLabel
        {
            get
            {
                string dataBaseBackupLabel = "���ݿⱸ��";
                if (this.MyPricipal.IsInRole("DataBaseBackup"))
                {
                    dataBaseBackupLabel = GetSiteMapTitle("DataBaseBackup");
                }
                return dataBaseBackupLabel;
            }
        }
        public string ExitMenuHeader
        {
            get
            {
                string exitMenuHheader = "�˳�";
                if (this.MyPricipal.IsInRole("ExitMenu"))
                {
                    exitMenuHheader = GetSiteMapTitle("ExitMenu");
                }
                return exitMenuHheader;
            }
        }
        public string ExitGHeader
        {
            get
            {
                string exitGHeader = "�˳�";
                if (this.MyPricipal.IsInRole("ExitG"))
                {
                    exitGHeader = GetSiteMapTitle("ExitG");
                }
                return exitGHeader;
            }
        }
        public string ExitLabel
        {
            get
            {
                string exitLabel = "�˳�";
                if (this.MyPricipal.IsInRole("Exit"))
                {
                    exitLabel = GetSiteMapTitle("Exit");
                }
                return exitLabel;
            }
        }
        public string LogOutLabel
        {
            get
            {
                string logOutLabel = "ע��";
                if (this.MyPricipal.IsInRole("LogOut"))
                {
                    logOutLabel = GetSiteMapTitle("LogOut");
                }
                return logOutLabel;
            }
        }
        //private List<DXInfo.Models.aspnet_Sitemaps> getFunc()
        //{
        //    DXInfo.Principal.MyPrincipal mp = Thread.CurrentPrincipal as DXInfo.Principal.MyPrincipal;
        //    List<DXInfo.Models.aspnet_Sitemaps> func = new List<DXInfo.Models.aspnet_Sitemaps>();
        //    if (mp != null)
        //    {                
        //        foreach (DXInfo.Models.aspnet_Sitemaps sitemap in mp.Func)
        //        {
        //            func.Add(Common.CloneOf(sitemap));
        //        }
        //        mp = null;
        //    }
        //    return func;
        //}
        private string GetSiteMapTitle(string name)
        {
            DXInfo.Principal.MyPrincipal mp = Thread.CurrentPrincipal as DXInfo.Principal.MyPrincipal;
            List<DXInfo.Models.aspnet_Sitemaps> lSitemaps = mp.Func;
            var siteMap = (from d in lSitemaps
                           where d.Name == name
                           select d).FirstOrDefault();
            return siteMap.Title;
        }
        #endregion

        #region ��ť
        private void button_Click(object sender)
        {
            Microsoft.Windows.Controls.Ribbon.RibbonButton btn = sender as Microsoft.Windows.Controls.Ribbon.RibbonButton;
            switch (btn.Name)
            {
                case "ImgDownload":
                    Business.DownLoadImagesWindows dwImages = new Business.DownLoadImagesWindows();
                    dwImages.Show();
                    break;
                case "Mp3DownLoad":
                    Business.DownLoadWindows dwMp3 = new Business.DownLoadWindows();
                    dwMp3.Show();
                    break;
                case "Mp3Play":
                    Process.Start("Media Glass.exe", this.Dept.DeptId.ToString());
                    break;
                case "DataSync":
                    Business.SyncWindow sw = new Business.SyncWindow();
                    sw.Show();
                    break;
                case "RecycleCard":
                    int st = CardRef.CoolerRecycleCard();
                    if (st == 0)
                    {
                        System.Windows.MessageBox.Show("���ճɹ�");
                    }
                    else
                    {
                        System.Windows.MessageBox.Show(CardRef.GetStr(st));
                    }
                    break;
                case "PutCard":
                    putCard();
                    break;
                case "DataBaseBackup":
                    DataBaseBackupWindow bk1 = new DataBaseBackupWindow(Uow, true);
                    bk1.ShowDialog();
                    break;
                case "DataBaseRestore":
                    DataBaseBackupWindow bk2 = new DataBaseBackupWindow(Uow, false);
                    bk2.ShowDialog();
                    break;
                case "Exit":
                    App.Current.Shutdown();
                    break;
                case "LogOut":
                    var Logon = ServiceLocator.Current.GetInstance(Type.GetType("FairiesCoolerCash." + ClientCommon.LoginWin()));
                    Window win = (Window)Logon;
                    App.Current.MainWindow = win;
                    win.Show();
                    Messenger.Default.Send(0, "CloseRibbonMainWindow");
                    break;
                default:
                    CleanupUserControl();
                    Type type = Type.GetType("FairiesCoolerCash.Business." + btn.Name + "UserControl");
                    if (type != null)
                    {
                        var uc = ServiceLocator.Current.GetInstance(type);
                        if (uc != null && uc is UserControl)
                        {
                            UserControl aUc = uc as UserControl;
                            DXInfo.Models.aspnet_Sitemaps tag = btn.Tag as DXInfo.Models.aspnet_Sitemaps;
                            if (tag != null && !string.IsNullOrEmpty(tag.Action))//!string.IsNullOrEmpty(tag.Controller) && 
                            {
                                //System.Windows.Data.Binding bind = new System.Windows.Data.Binding();
                                //bind.Path = new PropertyPath(tag.Action);

                                aUc.DataContext = ServiceLocator.Current.GetInstance(Type.GetType("FairiesCoolerCash.ViewModel." + tag.Action));
                                //aUc.SetBinding(UserControl.DataContextProperty, bind);
                            }

                            this.MyContent = aUc;
                        }
                        else
                        {
                            this.MyContent = null;
                        }
                    }
                    break;
            }

            this.CurrentOperate = btn.Label;
        }
        public ICommand Button_Click
        {
            get
            {
                return new RelayCommand<object>(button_Click);
            }
        }
        private void putCard()
        {
            StringBuilder sb = new StringBuilder(33);
            sb.Append("V00995");
            int st = CardRef.CoolerPutCard(sb);

            if (st == 0)
            {
                System.Windows.MessageBox.Show("�����ɹ�");
            }
            else
            {
                System.Windows.MessageBox.Show(CardRef.GetStr(st));
            }

            int value = 158965;
            st = CardRef.CoolerRechargeCard(sb, value);
            if (st == 0)
            {
                System.Windows.MessageBox.Show("��ֵ�ɹ�");
            }
            else
            {
                Helper.ShowErrorMsg(CardRef.GetStr(st));
            }
        }
        #endregion

        #region �˵�
        private void SetMenu()
        {
            if (ClientCommon.DynamicRibbonMenu())
            {
                RibbonMenu2();
            }
            else
            {
                RibbonMenu1();
            }
        }
        //private System.Windows.Visibility _NoMemberConsumeVisibility;
        //public System.Windows.Visibility NoMemberConsumeVisibility
        //{
        //    get
        //    {
        //        return _NoMemberConsumeVisibility;
        //    }
        //    set
        //    {
        //        _NoMemberConsumeVisibility = value;
        //        this.RaisePropertyChanged("NoMemberConsumeVisibility");
        //    }
        //}
        private System.Windows.Visibility _RecycleCardVisibility;
        public System.Windows.Visibility RecycleCardVisibility
        {
            get
            {
                return _RecycleCardVisibility;
            }
            set
            {
                _RecycleCardVisibility = value;
                this.RaisePropertyChanged("RecycleCardVisibility");
            }
        }
        private System.Windows.Visibility _PutCardVisibility;
        public System.Windows.Visibility PutCardVisibility
        {
            get
            {
                return _PutCardVisibility;
            }
            set
            {
                _PutCardVisibility = value;
                this.RaisePropertyChanged("PutCardVisibility");
            }
        }
        private System.Windows.Visibility _DataSyncVisibility;
        public System.Windows.Visibility DataSyncVisibility
        {
            get
            {
                return _DataSyncVisibility;
            }
            set
            {
                _DataSyncVisibility = value;
                this.RaisePropertyChanged("DataSyncVisibility");
            }
        }
        private void RibbonMenu1()
        {
            //this.NoMemberConsumeVisibility = System.Windows.Visibility.Collapsed;
            if (this.User.UserName != "admin")
            {
                this.RecycleCardVisibility = System.Windows.Visibility.Collapsed;
                this.PutCardVisibility = System.Windows.Visibility.Collapsed;
            }
            SetVisible();
            this.DataSyncVisibility = System.Windows.Visibility.Visible;
        }
        private void RibbonMenu2()
        {
            MyRibbon.Items.Clear();

            GenerateRibbonMenu();
        }

        private void SetVisible()
        {
            if (!(this.User.UserName == "admin"))
            {
                foreach (ItemsControl ic in MyRibbon.Items)
                {
                    if (ic is RibbonTab)
                    {
                        RibbonTab rt = ic as RibbonTab;
                        if (!this.MyPricipal.IsInRole(rt.Name))
                        {
                            rt.Visibility = System.Windows.Visibility.Collapsed;
                        }
                        if (rt.HasItems)
                        {
                            foreach (ItemsControl ic1 in rt.Items)
                            {
                                if (ic1 is RibbonGroup)
                                {
                                    RibbonGroup rg = ic1 as RibbonGroup;
                                    if (!this.MyPricipal.IsInRole(rg.Name))
                                    {
                                        rg.Visibility = System.Windows.Visibility.Collapsed;
                                    }
                                    if (rg.HasItems)
                                    {
                                        for (int i = 0; i < rg.Items.Count; i++)
                                        {
                                            if (rg.Items[i] is RibbonButton)
                                            {
                                                RibbonButton rb = rg.Items[i] as RibbonButton;
                                                if (!this.MyPricipal.IsInRole(rb.Name))
                                                {
                                                    rb.Visibility = System.Windows.Visibility.Collapsed;
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void addRibbonButton(RibbonGroup rg, DXInfo.Models.aspnet_Sitemaps sitemap1)
        {
            RibbonButton rb = new RibbonButton();
            rb.Name = sitemap1.Name;
            rb.Label = sitemap1.Title;
            rb.Tag = sitemap1;

            rb.SetBinding(RibbonButton.CommandProperty, "Button_Click");

            System.Windows.Data.Binding bind = new System.Windows.Data.Binding();
            bind.RelativeSource = new RelativeSource(RelativeSourceMode.Self);
            rb.SetBinding(RibbonButton.CommandParameterProperty, bind);

            if (!string.IsNullOrEmpty(sitemap1.Url))
            {
                if (Helper.ResourceExists("images/" + sitemap1.Url.ToLower()))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri("pack://application:,,,/images/" + sitemap1.Url, UriKind.Absolute);
                    bitmap.EndInit();
                    rb.LargeImageSource = bitmap;
                }
                string strpath = System.AppDomain.CurrentDomain.BaseDirectory;
                string filepath = System.IO.Path.Combine(strpath, sitemap1.Url);
                if (System.IO.File.Exists(filepath))
                {
                    BitmapImage image = new BitmapImage(new Uri(filepath));
                    rb.LargeImageSource = image;
                }

            }
            else
            {
                if (dImagePath.ContainsKey(sitemap1.Name))
                {
                    string path = dImagePath[sitemap1.Name];
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri("pack://application:,,,/images/" + path, UriKind.Absolute);
                    bitmap.EndInit();
                    rb.LargeImageSource = bitmap;
                }
            }
            rg.Items.Add(rb);
        }

        private void GenerateRibbonMenu()
        {
            bool isOperatorsOnDuty = BusinessCommon.OperatorsOnDuty();
            DXInfo.Principal.MyPrincipal mp = Thread.CurrentPrincipal as DXInfo.Principal.MyPrincipal;
            List<DXInfo.Models.aspnet_Sitemaps> lSitemaps = mp.Func.Distinct(new MyComparer()).ToList().OrderBy(o => o.Sort).ToList();
            //һ���˵�
            List<DXInfo.Models.aspnet_Sitemaps> lSitemaps1 = lSitemaps.Where(w => w.ParentCode == "000" && w.IsMenu).ToList();
            foreach (DXInfo.Models.aspnet_Sitemaps sitemap in lSitemaps1.OrderBy(o => o.Url).ToList())
            {
                RibbonTab rt = new RibbonTab();
                rt.Name = sitemap.Name;
                rt.Header = sitemap.Title;


                List<DXInfo.Models.aspnet_Sitemaps> lSitemaps2 = lSitemaps.Where(w => w.ParentCode == sitemap.Code && w.IsMenu).ToList();

                foreach (DXInfo.Models.aspnet_Sitemaps sitemap1 in lSitemaps2)
                {

                    RibbonGroup rg = new RibbonGroup();
                    rg.Name = sitemap1.Name;
                    rg.Header = sitemap1.Title;

                    List<DXInfo.Models.aspnet_Sitemaps> lSitemaps3 = lSitemaps.Where(w => w.ParentCode == sitemap1.Code && w.IsMenu).ToList();

                    foreach (DXInfo.Models.aspnet_Sitemaps sitemap2 in lSitemaps3)
                    {
                        if (sitemap2.IsMenu)//sitemap2.Name != "DeskManage-btnCheckout")
                        {
                            addRibbonButton(rg, sitemap2);
                        }
                    }
                    if (sitemap1.Name == "SysG")
                    {
                        RibbonCheckBox cbx = new RibbonCheckBox();
                        cbx.Name = "chkIsOpen";
                        //cbx.Label = "���������";
                        cbx.SetBinding(RibbonCheckBox.LabelProperty, "ChkIsOpenLabel");
                        cbx.SetBinding(RibbonCheckBox.IsCheckedProperty, "IsOpen");
                        rg.Items.Add(cbx);

                        RibbonCheckBox cbx1 = new RibbonCheckBox();
                        cbx1.Name = "ChkIsStickerPrint";
                        //cbx1.Label = "��ӡ���ɽ�";
                        cbx1.SetBinding(RibbonCheckBox.LabelProperty, "ChkIsStickerPrintLabel");
                        cbx1.SetBinding(RibbonCheckBox.IsCheckedProperty, "IsStickerPrint");
                        rg.Items.Add(cbx1);

                        RibbonCheckBox cbx2 = new RibbonCheckBox();
                        cbx2.Name = "ChkIsTicket1";
                        //cbx2.Label = "��ӡСƱ1";
                        cbx2.SetBinding(RibbonCheckBox.LabelProperty, "ChkIsTicket1Label");
                        cbx2.SetBinding(RibbonCheckBox.IsCheckedProperty, "IsTicket1");
                        rg.Items.Add(cbx2);

                        RibbonCheckBox cbx3 = new RibbonCheckBox();
                        cbx3.Name = "ChkIsTicket2";
                        //cbx3.Label = "��ӡСƱ2";
                        cbx3.SetBinding(RibbonCheckBox.LabelProperty, "ChkIsTicket2Label");
                        cbx3.SetBinding(RibbonCheckBox.IsCheckedProperty, "IsTicket2");
                        rg.Items.Add(cbx3);

                        RibbonCheckBox cbx4 = new RibbonCheckBox();
                        cbx4.Name = "ChkIsTicket3";
                        //cbx4.Label = "��ӡСƱ3";
                        cbx4.SetBinding(RibbonCheckBox.LabelProperty, "ChkIsTicket3Label");
                        cbx4.SetBinding(RibbonCheckBox.IsCheckedProperty, "IsTicket3");
                        rg.Items.Add(cbx4);

                        RibbonCheckBox cbx5 = new RibbonCheckBox();
                        cbx5.Name = "ChkIsThree";
                        //cbx5.Label = "��ӡ������";
                        cbx5.SetBinding(RibbonCheckBox.LabelProperty, "ChkIsThreeLabel");
                        cbx5.SetBinding(RibbonCheckBox.IsCheckedProperty, "IsThree");
                        rg.Items.Add(cbx5);
                    }

                    rt.Items.Add(rg);
                }
                if (isOperatorsOnDuty)
                {
                    //SysManage ϵͳ����
                    if (sitemap.Name == "SysManage")
                    {
                        RibbonGroup rg = new RibbonGroup();
                        rg.Name = "SysO";
                        rg.Header = "�������Ա";
                        SetOperatorsOnDuty(rg);
                        rt.Items.Add(rg);
                    }
                }
                MyRibbon.Items.Add(rt);
            }
        }
        #endregion

        #region �������Ա
        private void SetOperatorsOnDuty(RibbonGroup rg)
        {
            //List<DXInfo.Models.aspnet_CustomProfileEx> lOper = (from d in Uow.aspnet_CustomProfile.GetAll()
            //                                                    where d.DeptId == this.Dept.DeptId
            //                                                    select new DXInfo.Models.aspnet_CustomProfileEx()
            //                                                    {
            //                                                        UserId = d.UserId,
            //                                                        DeptId = d.DeptId,
            //                                                        FullName = d.FullName,
            //                                                        LastUpdatedDate = d.LastUpdatedDate,
            //                                                        IsSelected = false,
            //                                                    }).ToList();
            //           <ribbon:RibbonComboBox>
            //  <ribbon:RibbonGallery MaxColumnCount="1" SelectedValue="{Binding SelectedEmployee}">
            //    <ribbon:RibbonGalleryCategory ItemsSource="{Binding Employees.View}" DisplayMemberPath="Name"/>
            //  </ribbon:RibbonGallery>
            //</ribbon:RibbonComboBox>
            int i = 0;
            foreach (DXInfo.Models.aspnet_CustomProfile oper in lOper)
            {
                RibbonCheckBox cbx = new RibbonCheckBox();
                cbx.Name = "OperatorsOnDuty" + i.ToString();
                cbx.Label = oper.FullName;
                cbx.Tag = oper;
                //cbx.SetBinding(RibbonCheckBox.IsCheckedProperty, "IsSelected");
                cbx.Unchecked += new RoutedEventHandler(cbx_Unchecked);
                cbx.Checked += new RoutedEventHandler(cbx_Checked);
                rg.Items.Add(cbx);
                i++;
            }
        }

        void cbx_Checked(object sender, RoutedEventArgs e)
        {
            RibbonCheckBox cbx = sender as RibbonCheckBox;
            DXInfo.Models.aspnet_CustomProfile oper = cbx.Tag as DXInfo.Models.aspnet_CustomProfile;
            this.lCheckedOper.Add(oper);
            SetOperatorsOnDuty();
        }

        void cbx_Unchecked(object sender, RoutedEventArgs e)
        {
            RibbonCheckBox cbx = sender as RibbonCheckBox;
            DXInfo.Models.aspnet_CustomProfile oper = cbx.Tag as DXInfo.Models.aspnet_CustomProfile;
            this.lCheckedOper.Remove(oper);
            SetOperatorsOnDuty();
        }
        private void SetOperatorsOnDuty()
        {
            this.OperatorsOnDuty = null;
            if (lCheckedOper.Count > 0)
            {
                this.OperatorsOnDuty = lCheckedOper.Select(s => s.FullName).Aggregate((pre, next) => { return pre + "��" + next; });
            }
        }
        #endregion
    }
}