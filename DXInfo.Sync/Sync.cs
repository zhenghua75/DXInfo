#region 引用
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.SqlServer;
using Microsoft.Synchronization;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Data;
using Microsoft.Synchronization.MetadataStorage;
using System.Configuration;
using System.Reflection;
using System.IO;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DXInfo.Models;
#endregion

namespace DXInfo.Sync
{
    public class Sync:IDisposable
    {
        #region 字段
        private bool IsRun = false;
        private bool IsRun1 = false;
        private bool IsRun2 = false;
        private bool IsRun3 = false;
        private bool IsRun4 = false;
        private bool IsRun5 = false;
        private bool IsRun6 = false;
        private bool IsRun7 = false;
        private bool IsRun8 = false;
        private bool IsRun9 = false;
        //private bool IsRun10 = false;
        //private bool IsRun11 = false;
        //private bool IsRun12 = false;
        private bool IsFirstRun=true;

        public SqlConnection ClientConn { get; private set; }
        public SqlConnection ServerConn { get; private set; }
        public Dictionary<int, string> ScopeNames { get; private set; }
        public Dictionary<string, string> ScopeTables { get; private set; }
        private string GetServerConnStr()
        {
            string _connectionStringSet = ConfigAdapter.GetConfigNote("SetConnectionString").Trim();
            string _connectionString = _connectionStringSet;
            if (String.Empty != _connectionStringSet)
            {
                _connectionStringSet = DataSecurity.Encrypt(_connectionStringSet);
                ConfigAdapter.SetConfigNote("ConnectionString", _connectionStringSet);
                ConfigAdapter.SetConfigNote("SetConnectionString", String.Empty);
            }
            else
            {
                _connectionString = ConfigAdapter.GetConfigNote("ConnectionString");
                _connectionString = DataSecurity.Decrypt(_connectionString);
            }
            return _connectionString;
        }
        private string batchingDirectory=string.Empty;
        private uint batchSize = 1024;
        private long transactionSize = 1536;//1024;
        private SyncOrchestrator downloadSyncOrchestrator;
        private SyncOrchestrator uploadSyncOrchestrator;
        private SyncOrchestrator uploadAndDownloadSyncOrchestrator;
        private SyncOrchestrator WRDownloadSyncOrchestrator;
        private SyncOrchestrator WRuploadSyncOrchestrator;
        private SyncOrchestrator KitchenuploadSyncOrchestrator;
        private SyncOrchestrator PackageSyncOrchestrator;
        private SyncOrchestrator PackageuploadSyncOrchestrator;
        private SyncOrchestrator InvDeptPriceSyncOrchestrator;
        //库存同步
        //private SyncOrchestrator downloadStockSyncOrchestrator;
        //private SyncOrchestrator uploadStockSyncOrchestrator;
        //private SyncOrchestrator uploadAndDownloadStockSyncOrchestrator;
        #endregion

        #region 构造函数
        private void InitScopeData()
        {
            this.ScopeNames = new Dictionary<int, string>();
            ScopeNames.Add(0, "DownloadScope");
            ScopeNames.Add(1, "UploadScope");
            ScopeNames.Add(2, "UploadAndDownloadScope");
            ScopeNames.Add(3, "WRDownloadScope");
            ScopeNames.Add(4, "WRUploadScope");
            ScopeNames.Add(5, "KitchenUploadScope");
            ScopeNames.Add(6, "DownloadPackageScope");
            ScopeNames.Add(7, "UploadPackageScope");
            ScopeNames.Add(8, "DownloadInvDeptPriceScope");
            //库存数据同步
            //ScopeNames.Add(9, "DownloadStockScope");
            //ScopeNames.Add(10, "UploadStockScope");
            //ScopeNames.Add(11, "UploadAndDownloadStockScope");

            this.ScopeTables = new Dictionary<string, string>();

            ScopeTables.Add("aspnet_Applications", "DownloadScope");
            ScopeTables.Add("aspnet_AuthorizationRules", "DownloadScope");
            ScopeTables.Add("aspnet_CustomProfile", "DownloadScope");
            ScopeTables.Add("aspnet_Roles", "DownloadScope");
            ScopeTables.Add("aspnet_SchemaVersions", "DownloadScope");
            ScopeTables.Add("aspnet_Sitemaps", "DownloadScope");
            ScopeTables.Add("aspnet_Users", "DownloadScope");
            ScopeTables.Add("aspnet_UsersInRoles", "DownloadScope");
            ScopeTables.Add("aspnet_Membership", "DownloadScope");
            ScopeTables.Add("CardLevels", "DownloadScope");
            ScopeTables.Add("ConsumePoints", "DownloadScope");
            ScopeTables.Add("Depts", "DownloadScope");
            ScopeTables.Add("Inventory", "DownloadScope");
            ScopeTables.Add("InventoryCategory", "DownloadScope");
            ScopeTables.Add("NameCode", "DownloadScope");
            ScopeTables.Add("PlayLists", "DownloadScope");
            ScopeTables.Add("RechargeDonations", "DownloadScope");
            ScopeTables.Add("Tastes", "DownloadScope");
            ScopeTables.Add("UnitOfMeasures", "DownloadScope");
            ScopeTables.Add("CardTypes", "DownloadScope");
            ScopeTables.Add("PayTypes", "DownloadScope");
            ScopeTables.Add("ConsumeDonateInv", "DownloadScope");
            ScopeTables.Add("InvDepts", "DownloadScope");

            ScopeTables.Add("Bills", "UploadScope");
            ScopeTables.Add("BillInvLists", "UploadScope");
            ScopeTables.Add("BillDonateInvLists", "UploadScope");
            ScopeTables.Add("Consume", "UploadScope");
            ScopeTables.Add("ConsumeInvPrice", "UploadScope");
            ScopeTables.Add("ConsumeList", "UploadScope");
            ScopeTables.Add("ConsumeTastes", "UploadScope");
            ScopeTables.Add("Recharges", "UploadScope");
            ScopeTables.Add("ReceiptHis", "UploadScope");

            ScopeTables.Add("CardDonateInventory", "UploadAndDownloadScope");
            ScopeTables.Add("CardPoints", "UploadAndDownloadScope");
            ScopeTables.Add("Cards", "UploadAndDownloadScope");
            ScopeTables.Add("CardsLog", "UploadAndDownloadScope");
            ScopeTables.Add("ekey", "UploadAndDownloadScope");
            ScopeTables.Add("Members", "UploadAndDownloadScope");
            ScopeTables.Add("MembersLog", "UploadAndDownloadScope");
            ScopeTables.Add("Receipts", "UploadAndDownloadScope");

            ScopeTables.Add("Rooms", "WRDownloadScope");
            ScopeTables.Add("Desks", "WRDownloadScope");
            ScopeTables.Add("IPads", "WRDownloadScope");
            ScopeTables.Add("WRCardLevels", "WRDownloadScope");
            ScopeTables.Add("CategoryDepts", "WRDownloadScope");
            ScopeTables.Add("Organizations", "WRDownloadScope");

            ScopeTables.Add("OrderDishes", "WRUploadScope");
            ScopeTables.Add("OrderDishesHis", "WRUploadScope");
            ScopeTables.Add("OrderBooks", "WRUploadScope");
            ScopeTables.Add("OrderBooksHis", "WRUploadScope");
            ScopeTables.Add("OrderBookDeskes", "WRUploadScope");
            ScopeTables.Add("OrderBookDeskesHis", "WRUploadScope");
            ScopeTables.Add("OrderIpads", "WRUploadScope");
            ScopeTables.Add("OrderDeskes", "WRUploadScope");
            ScopeTables.Add("OrderDeskesHis", "WRUploadScope");
            ScopeTables.Add("OrderMenus", "WRUploadScope");
            ScopeTables.Add("OrderMenusHis", "WRUploadScope");
            ScopeTables.Add("OrderHurry", "WRUploadScope");
            ScopeTables.Add("OrderInvPrice", "WRUploadScope");//商品单价

            ScopeTables.Add("KitchenMenuDesk", "KitchenUploadScope");
            ScopeTables.Add("KitchenBill", "KitchenUploadScope");
            ScopeTables.Add("KitchenMiss", "KitchenUploadScope");

            ScopeTables.Add("Packages", "DownloadPackageScope");

            ScopeTables.Add("ConsumePackages", "UploadPackageScope");
            ScopeTables.Add("OrderPackages", "UploadPackageScope");
            ScopeTables.Add("OrderPackagesHis", "UploadPackageScope");

            ScopeTables.Add("InventoryDeptPrice", "DownloadInvDeptPriceScope");
            ScopeTables.Add("InvPrice", "DownloadInvDeptPriceScope");

            //库存同步
            //ScopeTables.Add("CurrentStock", "DownloadStockScope");
            //ScopeTables.Add("MeasurementUnitGroup", "DownloadStockScope");
            //ScopeTables.Add("Warehouse", "DownloadStockScope");
            //ScopeTables.Add("VouchCodeRule", "DownloadStockScope");

            //ScopeTables.Add("RdRecord", "UploadStockScope");
            //ScopeTables.Add("RdRecords", "UploadStockScope");
            //ScopeTables.Add("RdRecord", "UploadAndDownloadStockScope");
            //ScopeTables.Add("RdRecords", "UploadAndDownloadStockScope");
            
        }
        public Sync(string connectionString)
        {
            this.ServerConn = new SqlConnection(connectionString);
            this.InitScopeData();
        }
        private static Sync theSync = null;
        public static Sync Instance()
        {
            if (null == theSync)
            {
                theSync = new Sync();
            }
            return theSync;
        }
        private Sync()
        {
            this.ClientConn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["FairiesMemberManage"].ConnectionString);
            this.ServerConn = new SqlConnection(GetServerConnStr());
            this.InitScopeData();
            this.SetBatchSpoolLocation(Path.Combine(Directory.GetCurrentDirectory(), "Sync_BatchFiles"));

            //RestoreDatabase(this.ClientConn);

            downloadSyncOrchestrator = new SyncOrchestrator();
            downloadSyncOrchestrator.LocalProvider = SetSqlSyncProvider("DownloadScope", ClientConn);
            downloadSyncOrchestrator.RemoteProvider = SetSqlSyncProvider("DownloadScope", ServerConn);
            downloadSyncOrchestrator.Direction = SyncDirectionOrder.Download;
            ((SqlSyncProvider)downloadSyncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(syncClientSyncProvider_Download_ApplyChangeFailed);
            ((SqlSyncProvider)downloadSyncOrchestrator.RemoteProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(syncServerSyncProvider_Download_ApplyChangeFailed);
            downloadSyncOrchestrator.SessionProgress += new EventHandler<SyncStagedProgressEventArgs>(syncOrchestrator_SessionProgress);

            uploadSyncOrchestrator = new SyncOrchestrator();
            uploadSyncOrchestrator.LocalProvider = SetSqlSyncProvider("UploadScope", ClientConn);
            uploadSyncOrchestrator.RemoteProvider = SetSqlSyncProvider("UploadScope", ServerConn);
            uploadSyncOrchestrator.Direction = SyncDirectionOrder.Upload;
            ((SqlSyncProvider)uploadSyncOrchestrator.RemoteProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(syncSeverSyncProvider_Upload_ApplyChangeFailed);
            uploadSyncOrchestrator.SessionProgress += new EventHandler<SyncStagedProgressEventArgs>(syncOrchestrator_SessionProgress);

            uploadAndDownloadSyncOrchestrator = new SyncOrchestrator();
            uploadAndDownloadSyncOrchestrator.LocalProvider = SetSqlSyncProvider("UploadAndDownloadScope", ClientConn);
            uploadAndDownloadSyncOrchestrator.RemoteProvider = SetSqlSyncProvider("UploadAndDownloadScope", ServerConn, true);
            uploadAndDownloadSyncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;
            ((SqlSyncProvider)uploadAndDownloadSyncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(syncClientSyncProvider_UploadDownload_ApplyChangeFailed);
            ((SqlSyncProvider)uploadAndDownloadSyncOrchestrator.RemoteProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(syncSeverSyncProvider_UploadDownload_ApplyChangeFailed);
            uploadAndDownloadSyncOrchestrator.SessionProgress += new EventHandler<SyncStagedProgressEventArgs>(syncOrchestrator_SessionProgress);

            WRDownloadSyncOrchestrator = new SyncOrchestrator();
            WRDownloadSyncOrchestrator.LocalProvider = SetSqlSyncProvider("WRDownloadScope", ClientConn);
            WRDownloadSyncOrchestrator.RemoteProvider = SetSqlSyncProvider("WRDownloadScope", ServerConn);
            WRDownloadSyncOrchestrator.Direction = SyncDirectionOrder.Download;
            ((SqlSyncProvider)WRDownloadSyncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(syncClientSyncProvider_Download_ApplyChangeFailed);
            ((SqlSyncProvider)WRDownloadSyncOrchestrator.RemoteProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(syncServerSyncProvider_Download_ApplyChangeFailed);
            WRDownloadSyncOrchestrator.SessionProgress += new EventHandler<SyncStagedProgressEventArgs>(syncOrchestrator_SessionProgress);

            WRuploadSyncOrchestrator = new SyncOrchestrator();
            WRuploadSyncOrchestrator.LocalProvider = SetSqlSyncProvider("WRUploadScope", ClientConn,true);
            WRuploadSyncOrchestrator.RemoteProvider = SetSqlSyncProvider("WRUploadScope", ServerConn);
            WRuploadSyncOrchestrator.Direction = SyncDirectionOrder.Upload;
            ((SqlSyncProvider)WRuploadSyncOrchestrator.RemoteProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(syncSeverSyncProvider_Upload_ApplyChangeFailed);
            WRuploadSyncOrchestrator.SessionProgress += new EventHandler<SyncStagedProgressEventArgs>(syncOrchestrator_SessionProgress);

            KitchenuploadSyncOrchestrator = new SyncOrchestrator();
            KitchenuploadSyncOrchestrator.LocalProvider = SetSqlSyncProvider("KitchenUploadScope", ClientConn);
            KitchenuploadSyncOrchestrator.RemoteProvider = SetSqlSyncProvider("KitchenUploadScope", ServerConn);
            KitchenuploadSyncOrchestrator.Direction = SyncDirectionOrder.Upload;
            ((SqlSyncProvider)KitchenuploadSyncOrchestrator.RemoteProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(syncSeverSyncProvider_Upload_ApplyChangeFailed);
            KitchenuploadSyncOrchestrator.SessionProgress += new EventHandler<SyncStagedProgressEventArgs>(syncOrchestrator_SessionProgress);

            PackageSyncOrchestrator = new SyncOrchestrator();
            PackageSyncOrchestrator.LocalProvider = SetSqlSyncProvider("DownloadPackageScope", ClientConn);
            PackageSyncOrchestrator.RemoteProvider = SetSqlSyncProvider("DownloadPackageScope", ServerConn);
            PackageSyncOrchestrator.Direction = SyncDirectionOrder.Download;
            ((SqlSyncProvider)PackageSyncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(syncClientSyncProvider_Download_ApplyChangeFailed);
            ((SqlSyncProvider)PackageSyncOrchestrator.RemoteProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(syncServerSyncProvider_Download_ApplyChangeFailed);
            PackageSyncOrchestrator.SessionProgress += new EventHandler<SyncStagedProgressEventArgs>(syncOrchestrator_SessionProgress);

            PackageuploadSyncOrchestrator = new SyncOrchestrator();
            PackageuploadSyncOrchestrator.LocalProvider = SetSqlSyncProvider("UploadPackageScope", ClientConn);
            PackageuploadSyncOrchestrator.RemoteProvider = SetSqlSyncProvider("UploadPackageScope", ServerConn);
            PackageuploadSyncOrchestrator.Direction = SyncDirectionOrder.Upload;
            ((SqlSyncProvider)PackageuploadSyncOrchestrator.RemoteProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(syncSeverSyncProvider_Upload_ApplyChangeFailed);
            PackageuploadSyncOrchestrator.SessionProgress += new EventHandler<SyncStagedProgressEventArgs>(syncOrchestrator_SessionProgress);

            InvDeptPriceSyncOrchestrator = new SyncOrchestrator();
            InvDeptPriceSyncOrchestrator.LocalProvider = SetSqlSyncProvider("DownloadInvDeptPriceScope", ClientConn);
            InvDeptPriceSyncOrchestrator.RemoteProvider = SetSqlSyncProvider("DownloadInvDeptPriceScope", ServerConn);
            InvDeptPriceSyncOrchestrator.Direction = SyncDirectionOrder.Download;
            ((SqlSyncProvider)InvDeptPriceSyncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(syncClientSyncProvider_Download_ApplyChangeFailed);
            ((SqlSyncProvider)InvDeptPriceSyncOrchestrator.RemoteProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(syncServerSyncProvider_Download_ApplyChangeFailed);
            InvDeptPriceSyncOrchestrator.SessionProgress += new EventHandler<SyncStagedProgressEventArgs>(syncOrchestrator_SessionProgress);

            //库存
            //downloadStockSyncOrchestrator = new SyncOrchestrator();
            //downloadStockSyncOrchestrator.LocalProvider = SetSqlSyncProvider("DownloadStockScope", ClientConn);
            //downloadStockSyncOrchestrator.RemoteProvider = SetSqlSyncProvider("DownloadStockScope", ServerConn);
            //downloadStockSyncOrchestrator.Direction = SyncDirectionOrder.Download;
            //((SqlSyncProvider)downloadStockSyncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(syncClientSyncProvider_Download_ApplyChangeFailed);
            //((SqlSyncProvider)downloadStockSyncOrchestrator.RemoteProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(syncServerSyncProvider_Download_ApplyChangeFailed);
            //downloadStockSyncOrchestrator.SessionProgress += new EventHandler<SyncStagedProgressEventArgs>(syncOrchestrator_SessionProgress);

            //uploadStockSyncOrchestrator = new SyncOrchestrator();
            //uploadStockSyncOrchestrator.LocalProvider = SetSqlSyncProvider("UploadStockScope", ClientConn);
            //uploadStockSyncOrchestrator.RemoteProvider = SetSqlSyncProvider("UploadStockScope", ServerConn);
            //uploadStockSyncOrchestrator.Direction = SyncDirectionOrder.Upload;
            //((SqlSyncProvider)uploadStockSyncOrchestrator.RemoteProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(syncSeverSyncProvider_Upload_ApplyChangeFailed);
            //uploadStockSyncOrchestrator.SessionProgress += new EventHandler<SyncStagedProgressEventArgs>(syncOrchestrator_SessionProgress);

            //uploadAndDownloadStockSyncOrchestrator = new SyncOrchestrator();
            //uploadAndDownloadStockSyncOrchestrator.LocalProvider = SetSqlSyncProvider("UploadAndDownloadStockScope", ClientConn);
            //uploadAndDownloadStockSyncOrchestrator.RemoteProvider = SetSqlSyncProvider("UploadAndDownloadStockScope", ServerConn, true);
            //uploadAndDownloadStockSyncOrchestrator.Direction = SyncDirectionOrder.UploadAndDownload;
            //((SqlSyncProvider)uploadAndDownloadStockSyncOrchestrator.LocalProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(syncClientSyncProvider_UploadDownload_ApplyChangeFailed);
            //((SqlSyncProvider)uploadAndDownloadStockSyncOrchestrator.RemoteProvider).ApplyChangeFailed += new EventHandler<DbApplyChangeFailedEventArgs>(syncSeverSyncProvider_UploadDownload_ApplyChangeFailed);
            //uploadAndDownloadStockSyncOrchestrator.SessionProgress += new EventHandler<SyncStagedProgressEventArgs>(syncOrchestrator_SessionProgress);

            TaskScheduler.UnobservedTaskException += new EventHandler<UnobservedTaskExceptionEventArgs>(TaskScheduler_UnobservedTaskException);
        }

        void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            // += (s, e) =>
            //{
            //    //设置所有未觉察异常被觉察
            //    e.SetObserved();
            //};
            //throw new NotImplementedException();
            e.SetObserved();
        }    

        private bool MetadataCleanup(SqlConnection conn)
        {
            bool cleanupSuccessful = false;
            try
            {
                SqlSyncStoreMetadataCleanup metadataCleanup = new SqlSyncStoreMetadataCleanup(conn);
                metadataCleanup.RetentionInDays = 7;
                cleanupSuccessful = metadataCleanup.PerformCleanup();
            }
            catch (Exception ex)
            {
                ExceptionPolicy.HandleException(ex, "Policy");
            }
            return cleanupSuccessful;
        }
        private bool ClientMetadataCleanup()
        {
            return MetadataCleanup(ClientConn);
        }
        private bool ServerMetadataCleanup()
        {
            return MetadataCleanup(ServerConn);
        }
        public void RestoreDatabase(SqlConnection conn)
        {
            SqlSyncStoreRestore databaseRestore = new SqlSyncStoreRestore(conn);
            databaseRestore.PerformPostRestoreFixup();
        }
        public delegate void SyncMsgEventHandler(object sender, SyncMsgEventArgs e);
        public event SyncMsgEventHandler SyncMsgEvent;
        private void OnSyncMsgEvent(object sender, string msg)
        {
            if (!string.IsNullOrEmpty(msg) && SyncMsgEvent != null)
            {
                SyncMsgEventArgs e = new SyncMsgEventArgs(msg);
                SyncMsgEvent(sender, e);
            }
        }
        private void syncOrchestrator_SessionProgress(object sender, SyncStagedProgressEventArgs e)
        {
            SyncOrchestrator orchestrator = sender as SyncOrchestrator;

            string str = orchestrator.Direction.ToString() + "--" +
                orchestrator.State.ToString() + "--" +
                "位置：" + e.ReportingProvider.ToString() + "--" +
                "阶段：" + e.Stage.ToString() + "--" +
                "完成量：" + (e.CompletedWork * 100 / e.TotalWork).ToString();
            UpdateSyncProgressMsg(sender, str);
        }
        private void provider_SyncProgress(object sender, DbSyncProgressEventArgs e)
        {            
            SqlSyncProvider provider = sender as SqlSyncProvider;
            string str = provider.ScopeName + "--" +
                e.TableProgress.TableName + "--" +
                e.Stage.ToString() + "--" +
                "总变更：" + e.TableProgress.TotalChanges.ToString() + "--其中--" +
                "插入：" + e.TableProgress.Inserts.ToString() + "--" +
                "更新：" + e.TableProgress.Updates.ToString() + "--" +
                "删除" + e.TableProgress.Deletes.ToString();
            UpdateSyncProgressMsg(sender, str);
        }
        private void UpdateSyncProgressMsg(object sender, string msg)
        {
            OnSyncMsgEvent(sender, msg);
        }
        private void UpdateSyncProgressMsg(SyncOrchestrator sender, SyncOperationStatistics statistics)
        {
            if (statistics != null)
            {
                UpdateSyncProgressMsg(sender, "开始时间: " + statistics.SyncStartTime);
                UpdateSyncProgressMsg(sender, "下载成功: " + statistics.DownloadChangesApplied.ToString());
                UpdateSyncProgressMsg(sender, "下载失败: " + statistics.DownloadChangesFailed.ToString());
                UpdateSyncProgressMsg(sender, "下载总数: " + statistics.DownloadChangesTotal.ToString());
                UpdateSyncProgressMsg(sender, "上传成功: " + statistics.UploadChangesApplied.ToString());
                UpdateSyncProgressMsg(sender, "上传失败: " + statistics.UploadChangesFailed.ToString());
                UpdateSyncProgressMsg(sender, "上传总数: " + statistics.UploadChangesTotal.ToString());
                UpdateSyncProgressMsg(sender, "完成时间: " + statistics.SyncEndTime);
            }
        }
        private void SetBatchSpoolLocation(string directoryName)
        {
            this.batchingDirectory = directoryName;
            DirectoryInfo locationInfo = null;
            try
            {
                locationInfo = new DirectoryInfo(directoryName);
                if (!locationInfo.Exists)
                {
                    Directory.CreateDirectory(locationInfo.FullName);
                }
                this.batchingDirectory = locationInfo.FullName;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private SqlSyncProvider SetSqlSyncProvider(string scopeName,
            SqlConnection conn,
            bool timeout = false)
        {
            SqlSyncProvider provider = new SqlSyncProvider(scopeName, conn);  
            
            if (timeout)
            {
                provider.CommandTimeout = 300;
            }
            if (!string.IsNullOrEmpty(this.batchingDirectory) && this.batchSize > 0)
            {
                provider.BatchingDirectory = this.batchingDirectory;
                provider.MemoryDataCacheSize = this.batchSize;
                provider.ApplicationTransactionSize = this.transactionSize;
            }

            provider.SyncProgress += new EventHandler<DbSyncProgressEventArgs>(provider_SyncProgress);
            return provider;
        }
        #endregion

        #region 建同步框架
        private void PopulateTrackingTable(SqlConnection conn)
        {
            List<string> lScopeName = (from d in this.ScopeNames select d.Value).ToList();
            foreach (string scopeName in lScopeName)
            {
                List<string> lTableName = (from d in this.ScopeTables
                                           where d.Value == scopeName
                                           select d.Key).ToList();
                foreach (string tableName in lTableName)
                {
                    
                    PopulateTrackingTable(scopeName, tableName, conn);
                }
                                          
            }
        }
        private void PopulateTrackingTable(string scopeName, string tableName,SqlConnection conn)
        {
            if (tableName == "aspnet_UsersInRoles"||
                tableName=="InventoryDeptPrice") return;
            
            if (!(scopeName.Contains("UploadScope") ||
                        scopeName.Contains("UploadPackageScope")))
            {
                string idCol = "Id";
                switch (tableName)
                {
                    case "Depts":
                        idCol = "DeptId";
                        break;
                    case "aspnet_Applications":
                        idCol = "ApplicationId";
                        break;
                    case "aspnet_AuthorizationRules":
                        idCol = "RuleId";
                        break;
                    case "aspnet_CustomProfile":
                        idCol = "UserId";
                        break;
                    case "aspnet_Roles":
                        idCol = "RoleId";
                        break;
                    case "aspnet_SchemaVersions":
                        idCol = "Feature";
                        break;
                    case "aspnet_Sitemaps":
                        idCol = "Code";
                        break;
                    case "aspnet_Users":
                        idCol = "UserId";
                        break;
                    case "aspnet_Membership":
                        idCol = "UserId";
                        break;
                    case "ekey":
                        idCol = "HardwareID";
                        break;
                    case "InventoryDeptPrice":
                        break;
                }
                string sql = "INSERT INTO [" + tableName + "_tracking] ([i].["+idCol
                    +"], [create_scope_local_id], [local_create_peer_key], [local_create_peer_timestamp],"
    + " [update_scope_local_id], [local_update_peer_key], [sync_row_is_tombstone], "
    + " [last_change_datetime], [restore_timestamp]) "
    + "SELECT [i].["+idCol+"], NULL, 0, @@DBTS+1, NULL, 0, 0, GETDATE() , NULL "
    + "FROM " + tableName 
    + " AS [i] "
    + "LEFT JOIN [" + tableName 
    + "_tracking] [side] ON [side].["+idCol
    +"] = [i].["+idCol
    +"] WHERE [side].["+idCol
    +"] IS NULL";
                using (SqlCommand comm = new SqlCommand(sql, conn))
                {
                    try
                    {
                        if (conn.State != System.Data.ConnectionState.Open)
                            conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(tableName+ex.Message);
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }
        }
        public void ProvisionClient()
        {
            SqlSyncScopeProvisioning clientProvision = new SqlSyncScopeProvisioning(ClientConn);
            clientProvision.SetCreateTableDefault(DbSyncCreationOption.CreateOrUseExisting);
            //clientProvision.CommandTimeout = 600;
            DbSyncScopeDescription scopeDesc;

            var scopeNames = (from d in this.ScopeNames
                              select d.Value).ToList();
            foreach (string scopeName in scopeNames)
            {
                if (!clientProvision.ScopeExists(scopeName))
                {
                    scopeDesc = SqlSyncDescriptionBuilder.GetDescriptionForScope(scopeName, ServerConn);
                    clientProvision.PopulateFromScopeDescription(scopeDesc);
                    clientProvision.Apply();
                }
            }            
        }
        public void ProvisionServer()
        {
            DbSyncTableDescription tableDesc;
            DbSyncScopeDescription scopeDesc;
            SqlSyncScopeProvisioning serverProvision = new SqlSyncScopeProvisioning(ServerConn);
            
            serverProvision.SetCreateTableDefault(DbSyncCreationOption.Skip);            
            //serverProvision.CommandTimeout = 600;  
            
            var scopeNames = (from d in this.ScopeNames
                              select d.Value).ToList();
            foreach (string scopeName in scopeNames)
            {
                if (!serverProvision.ScopeExists(scopeName))
                {
                    var scopeTables = (from d in this.ScopeTables
                             where d.Value == scopeName
                             select d.Key).ToList();
                    scopeDesc = new DbSyncScopeDescription(scopeName);
                    foreach (string tableName in scopeTables)
                    {
                        tableDesc = SqlSyncDescriptionBuilder.GetDescriptionForTable(tableName, ServerConn);
                        scopeDesc.Tables.Add(tableDesc);
                    }
                    serverProvision.PopulateFromScopeDescription(scopeDesc);
                    serverProvision.Apply();
                }
            }
        }

        public void Provision()
        {
            ProvisionServer();
            ProvisionClient();
        }
        #endregion

        #region 删同步框架
        public void DeProvision(SqlConnection conn,List<string> scopNames)
        {
            SqlSyncScopeProvisioning scopeProvision = new SqlSyncScopeProvisioning(conn);
            SqlSyncScopeDeprovisioning scopeDeprovision = new SqlSyncScopeDeprovisioning(conn);
            foreach (string scopName in scopNames)
            {
                if (scopeProvision.ScopeExists(scopName))
                {
                    scopeDeprovision.DeprovisionScope(scopName);
                }
            }
        }
        public void DeProvisionServer()
        {
            List<string> scopeNames = (from d in ScopeNames
                     select d.Value).ToList();
            this.DeProvision(this.ServerConn, scopeNames);
        }
        public void DeProvisionClient()
        {
            List<string> scopeNames = (from d in ScopeNames
                                       select d.Value).ToList();
            this.DeProvision(this.ClientConn, scopeNames);
        }
        public void DeProvison()
        {
            this.DeProvisionServer();
            this.DeProvisionClient();
        }
        #endregion

        #region 重建框架
        private void CheckProvision(SqlConnection conn,Dictionary<string,string> tablesThatChanged,bool isDropTrackingTable)
        {
            //PopulateTrackingTable(conn);
            if (tablesThatChanged.Count > 0)
            {
                var scopeNames = (from d in tablesThatChanged select d.Value).ToList().Distinct();
                foreach (string scopeName in scopeNames)
                {
                    var scopeTables = (from d in tablesThatChanged
                                       where d.Value == scopeName
                                       select d.Key).ToList();
                    if (scopeTables.Count > 0)
                    {
                        ReProvision(conn,scopeName, scopeTables, isDropTrackingTable);
                    }
                }
            }
        }
        public void CheckProvisionServer()
        {
            Dictionary<string, string> tablesThatChangedServer = CheckProvision(this.ServerConn);
            CheckProvision(this.ServerConn, tablesThatChangedServer, true);

            //Dictionary<string, string> tablesThatChangedServerTracking = CheckProvisionOfTracking(this.ServerConn);
            //CheckProvision(this.ServerConn, tablesThatChangedServerTracking, true);
        }
        public void CheckProvisionClient()
        {
            Dictionary<string, string> tablesThatChangedClient = CheckProvision(this.ClientConn);
            CheckProvision(this.ClientConn, tablesThatChangedClient, true);

            //Dictionary<string, string> tablesThatChangedClientTracking = CheckProvisionOfTracking(this.ClientConn);
            //CheckProvision(this.ClientConn, tablesThatChangedClientTracking, true);
        }
        public string SerializeObject(object obj)
        {
            if (null == obj)
                return string.Empty;
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            StringBuilder objString = new StringBuilder();
            foreach (FieldInfo field in fields)
            {
                objString.Append(field.Name + ":");
                object value = field.GetValue(obj);     //取得字段的值
                if (null != value)
                {
                    Type fieldType = value.GetType();
                    //判断该字段类型是否为类，且不是string类型
                    if (fieldType.IsClass && "String" != fieldType.Name)
                        objString.Append(SerializeObject(value));

                    objString.Append(value);
                }
                objString.Append(";");
            }
            return objString.ToString();
        }
        public bool CompareObject(object obj1, object obj2)
        {
            if (null == obj1 || null == obj2)
                return false;
            if (obj1.GetType() != obj2.GetType())
                return false;
            string str1 = SerializeObject(obj1).Replace("decimal", "numeric");
            string str2 = SerializeObject(obj2).Replace("decimal","numeric");
            return str1.Equals(str2);
        }
        private Dictionary<string, string> CheckProvision(SqlConnection conn)
        {
            List<string> scopeNames = (from d in this.ScopeNames select d.Value).ToList();
            Dictionary<string, string> tablesThatChanged = new Dictionary<string, string>();
            foreach (string scopeName in scopeNames)
            {
                SqlSyncScopeProvisioning scopeProvison = new SqlSyncScopeProvisioning(conn);
                DbSyncScopeDescription scopeDesc = SqlSyncDescriptionBuilder.GetDescriptionForScope(scopeName, conn);
                foreach (DbSyncTableDescription tableDesc in scopeDesc.Tables)
                {
                    string tableName = tableDesc.UnquotedLocalName;
                    DbSyncTableDescription tableDescFromDb = SqlSyncDescriptionBuilder.GetDescriptionForTable(tableName, conn);
                    bool eq = true;
                    if (!CompareObject(tableDesc, tableDescFromDb)) eq = false;
                    if (tableDesc.Columns.Count != tableDescFromDb.Columns.Count) eq = false;
                    for (int i = 0; i < tableDesc.Columns.Count; i++)
                    {
                        if (!CompareObject(tableDesc.Columns[i], tableDescFromDb.Columns[i]))
                        {
                            eq = false;
                        }
                    }
                    if (!eq)
                    {
                        tablesThatChanged.Add(tableName,scopeName);
                    }
                }
                var q = (from d in this.ScopeTables where d.Value == scopeName select d.Key).ToList();
                var q2 = (from d in scopeDesc.Tables select d.UnquotedLocalName).ToList();
                var q3 = (from d in q where !q2.Contains(d) select d).ToList();
                foreach (string tbl in q3)
                {
                    tablesThatChanged.Add(tbl, scopeName);
                }
            }
            return tablesThatChanged;
        }
        private Dictionary<string, string> CheckProvisionOfTracking(SqlConnection conn)
        {
            Dictionary<string, string> tablesThatChanged = new Dictionary<string, string>();
            if (!this.CheckTableFieldExist(conn, "aspnet_Roles_tracking", "RoleId"))
            {
                tablesThatChanged.Add("aspnet_Roles", "DownloadScope");
            }
            if (!this.CheckTableFieldExist(conn, "aspnet_Users_tracking", "UserId"))
            {
                tablesThatChanged.Add("aspnet_Users", "DownloadScope");
            }
            return tablesThatChanged;
        }

        private bool CheckTableFieldExist(SqlConnection conn,string tableName,string fieldName)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select COUNT(1) from syscolumns where name='{1}' and id=OBJECT_ID(N'[dbo].[{0}]')", tableName, fieldName);
            SqlCommand comm;
            int count=0;
            using (comm = new SqlCommand(sb.ToString(), conn))
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();
                    object o = comm.ExecuteScalar();
                    if (o != null)
                    {
                        count = Convert.ToInt32(o);
                    }
                }
                finally
                {
                    conn.Close();
                }
            }
            return count > 0;
        }
        public void ReProvision(string scopeName, string tableName, bool isDropTrackingTable)
        {
            List<string> tableNames = new List<string>();
            tableNames.Add(tableName);
            this.ReProvision(scopeName, tableNames,isDropTrackingTable);
        }
        public void ReProvision(string scopeName, IEnumerable<string> tablesThatChanged, bool isDropTrackingTable)
        {
            ReProvisionServer(scopeName, tablesThatChanged,isDropTrackingTable);
            ReProvisionClient(scopeName, tablesThatChanged,isDropTrackingTable);
        }
        public void ReProvisionServer(string scopeName, IEnumerable<string> tablesThatChanged, bool isDropTrackingTable)
        {
            this.ReProvision(this.ServerConn, scopeName, tablesThatChanged,isDropTrackingTable);
        }
        public void ReProvisionClient(string scopeName, IEnumerable<string> tablesThatChanged, bool isDropTrackingTable)
        {
            this.ReProvision(this.ClientConn, scopeName, tablesThatChanged,isDropTrackingTable);
        }
        public void ReProvision(SqlConnection conn, string scopeName, IEnumerable<string> tablesThatChanged, bool isDropTrackingTable)
        {
            var serverConfig = new SqlSyncScopeProvisioning(conn);
            var scopeDesc = SqlSyncDescriptionBuilder.GetDescriptionForScope(scopeName, conn);
            scopeDesc.ScopeName += "_temp";
            List<string> lDropTrackingTables = new List<string>();
            foreach (var tableName in tablesThatChanged)
            {                
                //var bracketedName = string.Format("[{0}]", tableName);
                var count = (from d in scopeDesc.Tables where d.UnquotedLocalName == tableName select d).Count();
                if (count == 0)
                {
                    //isDropTrackingTable = true;
                    lDropTrackingTables.Add(tableName);
                    DropTrackingForTable(conn, tableName, true);
                }
                else
                {
                    scopeDesc.Tables.Remove(scopeDesc.Tables[tableName]);
                    DropTrackingForTable(conn, tableName, isDropTrackingTable);
                }
                DbSyncTableDescription tableDescription = SqlSyncDescriptionBuilder.GetDescriptionForTable(tableName, conn);                
                scopeDesc.Tables.Add(tableDescription);
            }

            serverConfig.PopulateFromScopeDescription(scopeDesc);
            serverConfig.SetCreateProceduresDefault(DbSyncCreationOption.Skip);
            serverConfig.SetCreateTableDefault(DbSyncCreationOption.Skip);
            serverConfig.SetCreateTrackingTableDefault(DbSyncCreationOption.Skip);
            serverConfig.SetCreateTriggersDefault(DbSyncCreationOption.Skip);
            serverConfig.SetPopulateTrackingTableDefault(DbSyncCreationOption.Skip);
            
            foreach (var tableName in tablesThatChanged)
            {
                //var bracketedName = string.Format("[{0}]", tableName);

                serverConfig.Tables[tableName].CreateProcedures = DbSyncCreationOption.Create;
                if (isDropTrackingTable || lDropTrackingTables.Contains(tableName))
                {
                    serverConfig.Tables[tableName].CreateTrackingTable = DbSyncCreationOption.Create;
                    if (scopeName.Contains("UploadScope")||
                        scopeName.Contains("UploadPackageScope"))
                    {
                        serverConfig.Tables[tableName].PopulateTrackingTable = DbSyncCreationOption.Skip;//20131212表结构变化不引发同步
                    }
                    else
                    {
                        serverConfig.Tables[tableName].PopulateTrackingTable = DbSyncCreationOption.Create;
                    }
                }
                serverConfig.Tables[tableName].CreateTriggers = DbSyncCreationOption.Create;
            }
            serverConfig.Apply();

            using (SqlCommand comm1 = new SqlCommand(@"  
                     declare @config_id uniqueidentifier, @config_data xml  
                     SELECT @config_id=sc.config_id, @config_data=sc.config_data  
                     From scope_config sc Join [scope_info] si on si.scope_config_id=sc.config_id  
                     WHERE si.sync_scope_name = @scope_name + '_temp'  
               
                     Update [scope_config] Set config_data=@config_data  
                     From scope_config sc Join [scope_info] si on si.scope_config_id=sc.config_id  
                     WHERE si.sync_scope_name = @scope_name  
               
                     Delete [scope_config] WHERE config_id=@config_id;  
                     Delete [scope_info] WHERE scope_config_id=@config_id;  
                   ", conn))
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();
                    comm1.Parameters.AddWithValue("@scope_name", scopeName);
                    comm1.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        public void DropTrackingForTable(SqlConnection conn, string tableName, bool isDropTrackingTable)
        {
            SqlCommand comm;
            StringBuilder sb = new StringBuilder();
            //Drop tracking table & triggers
            if (isDropTrackingTable)
            {
                sb.AppendFormat(@"
                IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{0}_tracking]') AND type in (N'U'))
                    DROP TABLE [dbo].[{0}_tracking]
                ", tableName);
            }
            sb.AppendFormat(@"
                
                IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[{0}_insert_trigger]'))
                    DROP TRIGGER [dbo].[{0}_insert_trigger]
                IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[{0}_delete_trigger]'))
                    DROP TRIGGER [dbo].[{0}_delete_trigger]
                IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[{0}_update_trigger]'))
                    DROP TRIGGER [dbo].[{0}_update_trigger]
                ", tableName);
            //Drop Procedures
            foreach (string procName in new string[] { "delete", "deletemetadata", "insert", "insertmetadata", "update", "updatemetadata", "selectrow", "selectchanges", "bulkinsert", "bulkdelete", "bulkupdate" })
            {
                sb.AppendFormat(@"
                  IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{0}_{1}]') AND type in (N'P', N'PC'))
                    DROP PROCEDURE [dbo].[{0}_{1}]
                  ", tableName, procName);
            }
            //Drop Type
            sb.AppendFormat(@"
            IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'{0}_BulkType' AND ss.name = N'dbo')
                DROP TYPE [dbo].[{0}_BulkType]
            ", tableName);
            using (comm = new SqlCommand(sb.ToString(), conn))
            {
                try
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                        conn.Open();
                    comm.ExecuteNonQuery();
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        #endregion

        #region 同步
        
        object lockObject = new object();
        object lockFirstRunObject = new object();
        private void FirstRunExecute()
        {
            lock (lockFirstRunObject)
            {
                if (IsFirstRun)
                {
                    try
                    {
                        ProvisionClient();
                        CheckProvisionClient();
                    }
                    catch (Exception ex)
                    {
                        ExceptionPolicy.HandleException(ex, "Policy");
                    }
                    finally
                    {
                        IsFirstRun = false;
                    }
                }
            }
        }

        object lockIsRunObject = new object();
        public bool IsRunBlock(bool canRun=false)
        {
            lock (lockIsRunObject)
            {
                if (!IsRun)
                {
                    if (canRun)
                    {
                        IsRun = true;
                    }
                    return false;
                }
                return true;
            }
        }
        object lockIsRun1Object = new object();
        public bool IsRun1Block(bool canRun = false)
        {
            lock (lockIsRun1Object)
            {
                if (!IsRun1)
                {
                    if (canRun)
                    {
                        IsRun1 = true;
                    }
                    return false;
                }
                return true;
            }
        }
        object lockIsRun2Object = new object();
        public bool IsRun2Block(bool canRun = false)
        {
            lock (lockIsRun2Object)
            {
                if (!IsRun2)
                {
                    if (canRun)
                    {
                        IsRun2 = true;
                    }
                    return false;
                }
                return true;
            }
        }
        object lockIsRun3Object = new object();
        public bool IsRun3Block(bool canRun = false)
        {
            lock (lockIsRun3Object)
            {
                if (!IsRun3)
                {
                    if (canRun)
                    {
                        IsRun3 = true;
                    }
                    return false;
                }
                return true;
            }
        }
        object lockIsRun4Object = new object();
        public bool IsRun4Block(bool canRun = false)
        {
            lock (lockIsRun4Object)
            {
                if (!IsRun4)
                {
                    if (canRun)
                    {
                        IsRun4 = true;
                    }
                    return false;
                }
                return true;
            }
        }
        object lockIsRun5Object = new object();
        public bool IsRun5Block(bool canRun = false)
        {
            lock (lockIsRun5Object)
            {
                if (!IsRun5)
                {
                    if (canRun)
                    {
                        IsRun5 = true;
                    }
                    return false;
                }
                return true;
            }
        }
        object lockIsRun6Object = new object();
        public bool IsRun6Block(bool canRun = false)
        {
            lock (lockIsRun6Object)
            {
                if (!IsRun6)
                {
                    if (canRun)
                    {
                        IsRun6 = true;
                    }
                    return false;
                }
                return true;
            }
        }
        object lockIsRun7Object = new object();
        public bool IsRun7Block(bool canRun = false)
        {
            lock (lockIsRun7Object)
            {
                if (!IsRun7)
                {
                    if (canRun)
                    {
                        IsRun7 = true;
                    }
                    return false;
                }
                return true;
            }
        }
        object lockIsRun8Object = new object();
        public bool IsRun8Block(bool canRun = false)
        {
            lock (lockIsRun8Object)
            {
                if (!IsRun8)
                {
                    if (canRun)
                    {
                        IsRun8 = true;
                    }
                    return false;
                }
                return true;
            }
        }
        object lockIsRun9Object = new object();
        public bool IsRun9Block(bool canRun = false)
        {
            lock (lockIsRun9Object)
            {
                if (!IsRun9)
                {
                    if (canRun)
                    {
                        IsRun9 = true;
                    }
                    return false;
                }
                return true;
            }
        }

        //object lockIsRun10Object = new object();
        //public bool IsRun10Block(bool canRun = false)
        //{
        //    lock (lockIsRun10Object)
        //    {
        //        if (!IsRun10)
        //        {
        //            if (canRun)
        //            {
        //                IsRun10 = true;
        //            }
        //            return false;
        //        }
        //        return true;
        //    }
        //}

        //object lockIsRun11Object = new object();
        //public bool IsRun11Block(bool canRun = false)
        //{
        //    lock (lockIsRun11Object)
        //    {
        //        if (!IsRun11)
        //        {
        //            if (canRun)
        //            {
        //                IsRun11 = true;
        //            }
        //            return false;
        //        }
        //        return true;
        //    }
        //}
        //object lockIsRun12Object = new object();
        //public bool IsRun12Block(bool canRun = false)
        //{
        //    lock (lockIsRun12Object)
        //    {
        //        if (!IsRun12)
        //        {
        //            if (canRun)
        //            {
        //                IsRun12 = true;
        //            }
        //            return false;
        //        }
        //        return true;
        //    }
        //}
        private bool SyncOrchestratorIsRunning(SyncOrchestrator syncOrchestrator)
        {
            if (syncOrchestrator != null)
            {
                if (syncOrchestrator.State == SyncOrchestratorState.Downloading ||
                    syncOrchestrator.State == SyncOrchestratorState.Uploading ||
                    syncOrchestrator.State == SyncOrchestratorState.UploadingAndDownloading)
                {
                    return true;
                }
            }
            return false;
        }
        public void ExcuteSync()
        {            
            FirstRunExecute();
            //try
            //{
            //    if (ServerConn.State != ConnectionState.Open)
            //        ServerConn.Open();
            //}
            //catch (Exception ex)
            //{
            //    //ExceptionPolicy.HandleException(ex, "Policy");
            //    throw ex;
            //}
            //finally
            //{
            //    ServerConn.Close();
            //}
            if (!IsRunBlock(true))
            {
                try
                {                         
                    if (!IsRun1Block(true) && !SyncOrchestratorIsRunning(downloadSyncOrchestrator))
                    {
                        Task.Factory.StartNew(() =>
                        {
                            try
                            {                                
                                SyncOperationStatistics statistics = downloadSyncOrchestrator.Synchronize();
                                UpdateSyncProgressMsg(downloadSyncOrchestrator, statistics);
                            }
                            catch (Exception ex)
                            {
                                ExceptionPolicy.HandleException(ex, "Policy");
                            }
                            finally
                            {
                                IsRun1 = false;
                            }
                        });
                    }

                    if (!IsRun2Block(true) && !SyncOrchestratorIsRunning(uploadAndDownloadSyncOrchestrator))
                    {
                        Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                SyncOperationStatistics statistics = uploadAndDownloadSyncOrchestrator.Synchronize();
                                UpdateSyncProgressMsg(uploadAndDownloadSyncOrchestrator, statistics);
                            }
                            catch (Exception ex)
                            {
                                ExceptionPolicy.HandleException(ex, "Policy");
                            }
                            finally
                            {
                                IsRun2 = false;
                            }
                        });
                    }
                    if (!IsRun3Block(true) && !SyncOrchestratorIsRunning(uploadSyncOrchestrator))
                    {
                        Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                SyncOperationStatistics statistics = uploadSyncOrchestrator.Synchronize();
                                UpdateSyncProgressMsg(uploadSyncOrchestrator, statistics);
                            }
                            catch (Exception ex)
                            {
                                ExceptionPolicy.HandleException(ex, "Policy");
                            }
                            finally
                            {
                                IsRun3 = false;
                            }
                        });
                    }

                    if (!IsRun4Block(true) && !SyncOrchestratorIsRunning(WRDownloadSyncOrchestrator))
                    {
                        Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                SyncOperationStatistics statistics = WRDownloadSyncOrchestrator.Synchronize();
                                UpdateSyncProgressMsg(WRDownloadSyncOrchestrator, statistics);
                            }
                            catch (Exception ex)
                            {
                                ExceptionPolicy.HandleException(ex, "Policy");
                            }
                            finally
                            {
                                IsRun4 = false;
                            }
                        });
                    }
                    if (!IsRun5Block(true) && !SyncOrchestratorIsRunning(WRuploadSyncOrchestrator))
                    {
                        Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                SyncOperationStatistics statistics = WRuploadSyncOrchestrator.Synchronize();
                                UpdateSyncProgressMsg(WRuploadSyncOrchestrator, statistics);
                            }
                            catch (Exception ex)
                            {
                                ExceptionPolicy.HandleException(ex, "Policy");
                            }
                            finally
                            {
                                IsRun5 = false;
                            }
                        });
                    }
                    if (!IsRun6Block(true) && !SyncOrchestratorIsRunning(KitchenuploadSyncOrchestrator))
                    {
                        Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                SyncOperationStatistics statistics = KitchenuploadSyncOrchestrator.Synchronize();
                                UpdateSyncProgressMsg(KitchenuploadSyncOrchestrator, statistics);
                            }
                            catch (Exception ex)
                            {
                                ExceptionPolicy.HandleException(ex, "Policy");
                            }
                            finally
                            {
                                IsRun6 = false;
                            }
                        });
                    }
                    if (!IsRun7Block(true) && !SyncOrchestratorIsRunning(PackageSyncOrchestrator))
                    {
                        Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                SyncOperationStatistics statistics = PackageSyncOrchestrator.Synchronize();
                                UpdateSyncProgressMsg(PackageSyncOrchestrator, statistics);
                            }
                            catch (Exception ex)
                            {
                                ExceptionPolicy.HandleException(ex, "Policy");
                            }
                            finally
                            {
                                IsRun7 = false;
                            }
                        });
                    }
                    if (!IsRun8Block(true) && !SyncOrchestratorIsRunning(PackageuploadSyncOrchestrator))
                    {
                        Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                SyncOperationStatistics statistics = PackageuploadSyncOrchestrator.Synchronize();
                                UpdateSyncProgressMsg(PackageuploadSyncOrchestrator, statistics);
                            }
                            catch (Exception ex)
                            {
                                ExceptionPolicy.HandleException(ex, "Policy");
                            }
                            finally
                            {
                                IsRun8 = false;
                            }
                        });
                    }
                    if (!IsRun9Block(true) && !SyncOrchestratorIsRunning(InvDeptPriceSyncOrchestrator))
                    {
                        Task.Factory.StartNew(() =>
                        {
                            try
                            {
                                SyncOperationStatistics statistics = InvDeptPriceSyncOrchestrator.Synchronize();
                                UpdateSyncProgressMsg(InvDeptPriceSyncOrchestrator, statistics);
                            }
                            catch (Exception ex)
                            {
                                ExceptionPolicy.HandleException(ex, "Policy");
                            }
                            finally
                            {
                                IsRun9 = false;
                            }
                        });
                    }

                    //if (!IsRun10Block(true) && !SyncOrchestratorIsRunning(downloadStockSyncOrchestrator))
                    //{
                    //    Task.Factory.StartNew(() =>
                    //    {
                    //        try
                    //        {
                    //            SyncOperationStatistics statistics = downloadStockSyncOrchestrator.Synchronize();
                    //            UpdateSyncProgressMsg(downloadStockSyncOrchestrator, statistics);
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            ExceptionPolicy.HandleException(ex, "Policy");
                    //        }
                    //        finally
                    //        {
                    //            IsRun10 = false;
                    //        }
                    //    });
                    //}

                    //if (!IsRun11Block(true) && !SyncOrchestratorIsRunning(uploadStockSyncOrchestrator))
                    //{
                    //    Task.Factory.StartNew(() =>
                    //    {
                    //        try
                    //        {
                    //            SyncOperationStatistics statistics = uploadStockSyncOrchestrator.Synchronize();
                    //            UpdateSyncProgressMsg(uploadStockSyncOrchestrator, statistics);
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            ExceptionPolicy.HandleException(ex, "Policy");
                    //        }
                    //        finally
                    //        {
                    //            IsRun11 = false;
                    //        }
                    //    });
                    //}
                    //if (!IsRun12Block(true) && !SyncOrchestratorIsRunning(uploadAndDownloadStockSyncOrchestrator))
                    //{
                    //    Task.Factory.StartNew(() =>
                    //    {
                    //        try
                    //        {
                    //            SyncOperationStatistics statistics = uploadAndDownloadStockSyncOrchestrator.Synchronize();
                    //            UpdateSyncProgressMsg(uploadAndDownloadStockSyncOrchestrator, statistics);
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            ExceptionPolicy.HandleException(ex, "Policy");
                    //        }
                    //        finally
                    //        {
                    //            IsRun12 = false;
                    //        }
                    //    });
                    //}
                }
                catch (Exception ex)
                {
                    ExceptionPolicy.HandleException(ex, "Policy");
                }
                finally
                {
                    IsRun = false;
                }
            }
        }
        public void ExecuteSyncSync()
        {
            //ClientMetadataCleanup();

            ProvisionClient();
            CheckProvisionClient();


            downloadSyncOrchestrator.Synchronize();
            uploadAndDownloadSyncOrchestrator.Synchronize();
            uploadSyncOrchestrator.Synchronize();
            WRDownloadSyncOrchestrator.Synchronize();
            WRuploadSyncOrchestrator.Synchronize();
            KitchenuploadSyncOrchestrator.Synchronize();
            PackageSyncOrchestrator.Synchronize();
            PackageuploadSyncOrchestrator.Synchronize();
            InvDeptPriceSyncOrchestrator.Synchronize();
            //downloadStockSyncOrchestrator.Synchronize();
            //uploadStockSyncOrchestrator.Synchronize();
            //uploadAndDownloadStockSyncOrchestrator.Synchronize();
        }
        #endregion

        #region 错误处理
        #region 下载
        void syncClientSyncProvider_Download_ApplyChangeFailed(object sender, DbApplyChangeFailedEventArgs e)
        {            
            switch (e.Conflict.Type)
            {
                case DbConflictType.LocalDeleteRemoteUpdate:
                    e.Action = ApplyAction.RetryWithForceWrite;
                    break;
                case DbConflictType.LocalDeleteRemoteDelete:
                    e.Action = ApplyAction.Continue;
                    break;
                case DbConflictType.LocalInsertRemoteInsert:
                    e.Action = ApplyAction.Continue;
                    break;
                case DbConflictType.LocalUpdateRemoteUpdate:
                    //仅仅主键表
                    if (e.Conflict.LocalChange.TableName.ToLower() == "aspnet_usersinroles")
                    {
                        e.Action = ApplyAction.Continue;
                    }
                    else
                    {
                        e.Action = ApplyAction.RetryWithForceWrite;//Continue;//    
                    }
                    break;
                case DbConflictType.LocalUpdateRemoteDelete:
                    e.Action = ApplyAction.RetryWithForceWrite;//Continue;//.
                    break;
                case DbConflictType.LocalCleanedupDeleteRemoteUpdate:
                    e.Action = ApplyAction.RetryWithForceWrite;
                    break; 
                default:
                    if (e.Error != null)
                    {
                        ExceptionPolicy.HandleException(e.Error, "Policy");
                    }
                    e.Action = ApplyAction.RetryNextSync;
                    break;
            }
            
        }
        void syncServerSyncProvider_Download_ApplyChangeFailed(object sender, DbApplyChangeFailedEventArgs e)
        {
            switch (e.Conflict.Type)
            {
                case DbConflictType.LocalDeleteRemoteUpdate:
                    e.Action = ApplyAction.RetryWithForceWrite;
                    break;
                case DbConflictType.LocalDeleteRemoteDelete:
                    e.Action = ApplyAction.Continue;
                    break;
                case DbConflictType.LocalInsertRemoteInsert:
                    e.Action = ApplyAction.Continue;
                    break;
                case DbConflictType.LocalUpdateRemoteUpdate:
                    e.Action = ApplyAction.Continue;
                    break;
                case DbConflictType.LocalUpdateRemoteDelete:
                    e.Action = ApplyAction.RetryWithForceWrite;
                    break;
                case DbConflictType.LocalCleanedupDeleteRemoteUpdate:
                    e.Action = ApplyAction.RetryWithForceWrite;
                    break;
                default:
                    if (e.Error != null)
                    {
                        ExceptionPolicy.HandleException(e.Error, "Policy");
                    }
                    e.Action = ApplyAction.RetryNextSync;
                    break;
            }

        }
        #endregion
        #region 上传
        void syncSeverSyncProvider_Upload_ApplyChangeFailed(object sender, DbApplyChangeFailedEventArgs e)
        {
            switch (e.Conflict.Type)
            {
                case DbConflictType.LocalDeleteRemoteUpdate:
                    e.Action = ApplyAction.RetryWithForceWrite;
                    break;
                case DbConflictType.LocalDeleteRemoteDelete:
                    e.Action = ApplyAction.Continue;
                    break;
                case DbConflictType.LocalUpdateRemoteUpdate:
                    e.Action = ApplyAction.RetryWithForceWrite;
                    break;
                case DbConflictType.LocalUpdateRemoteDelete:
                    e.Action = ApplyAction.RetryWithForceWrite;
                    break;
                case DbConflictType.LocalInsertRemoteInsert:
                    e.Action = ApplyAction.Continue;
                    break;
                case DbConflictType.LocalCleanedupDeleteRemoteUpdate:
                    e.Action = ApplyAction.RetryWithForceWrite;
                    break;
                default:
                    if (e.Error != null)
                    {
                        ExceptionPolicy.HandleException(e.Error, "Policy");
                    }
                    break;
            }
            //if (e.Error != null)
            //{
            //    ExceptionPolicy.HandleException(e.Error, "Policy");
            //}
        }
        #endregion
        #region 上传下载
        void syncClientSyncProvider_UploadDownload_ApplyChangeFailed(object sender, DbApplyChangeFailedEventArgs e)
        {
            switch (e.Conflict.Type)
            {
                case DbConflictType.LocalDeleteRemoteUpdate:
                    e.Action = ApplyAction.RetryWithForceWrite;
                    break;
                case DbConflictType.LocalDeleteRemoteDelete:
                    e.Action = ApplyAction.Continue;
                    break;
                case DbConflictType.LocalInsertRemoteInsert:
                    e.Action = ApplyAction.Continue;
                    break;
                case DbConflictType.LocalUpdateRemoteUpdate:
                    e.Action = ApplyAction.RetryWithForceWrite;
                    break;
                case DbConflictType.LocalUpdateRemoteDelete:
                    e.Action = ApplyAction.Continue;
                    break;
                case DbConflictType.LocalCleanedupDeleteRemoteUpdate:
                    e.Action = ApplyAction.RetryWithForceWrite;
                    break;
                default:
                    if (e.Error != null)
                    {
                        ExceptionPolicy.HandleException(e.Error, "Policy");
                    }
                    break;
            }
            //if (e.Error != null)
            //{
            //    ExceptionPolicy.HandleException(e.Error, "Policy");
            //}
        }
        void syncSeverSyncProvider_UploadDownload_ApplyChangeFailed(object sender, DbApplyChangeFailedEventArgs e)
        {
            switch (e.Conflict.Type)
            {
                case DbConflictType.LocalDeleteRemoteUpdate:
                    e.Action = ApplyAction.RetryWithForceWrite;                    
                    break;
                case DbConflictType.LocalDeleteRemoteDelete:
                    e.Action = ApplyAction.Continue;
                    break;
                case DbConflictType.LocalUpdateRemoteUpdate:
                    e.Action = ApplyAction.RetryWithForceWrite;                    
                    break;
                case DbConflictType.LocalUpdateRemoteDelete:
                    e.Action = ApplyAction.Continue;
                    break;
                case DbConflictType.LocalInsertRemoteInsert:
                    e.Action = ApplyAction.Continue;
                    break;
                case DbConflictType.LocalCleanedupDeleteRemoteUpdate:
                    e.Action = ApplyAction.RetryWithForceWrite;
                    break;
                default:
                    if (e.Error != null)
                    {
                        ExceptionPolicy.HandleException(e.Error, "Policy");
                    }
                    break;
            }
            //if (e.Error != null)
            //{
            //    ExceptionPolicy.HandleException(e.Error, "Policy");
            //}
        }
        #endregion
        static void Program_ApplyChangeFailed(object sender, DbApplyChangeFailedEventArgs e)
        {
            switch (e.Conflict.Type)
            {
                case DbConflictType.LocalUpdateRemoteUpdate:
                    e.Action = ApplyAction.RetryWithForceWrite;
                    break;
                case DbConflictType.LocalInsertRemoteInsert:
                    e.Action = ApplyAction.RetryWithForceWrite;
                    break;
                case DbConflictType.LocalDeleteRemoteUpdate:
                    e.Action = ApplyAction.Continue;
                    break;
            }
            if (e.Error != null)
            {
                ExceptionPolicy.HandleException(e.Error, "Policy");
            }

        }
        #endregion

        #region IDisposable
        private void CancelSyncOrchestrator(SyncOrchestrator syncOrchestrator)
        {
            if (syncOrchestrator != null)
            {
                if (syncOrchestrator.State == SyncOrchestratorState.Downloading || syncOrchestrator.State == SyncOrchestratorState.Uploading ||
                        syncOrchestrator.State == SyncOrchestratorState.UploadingAndDownloading)
                {
                    syncOrchestrator.Cancel();
                }
                syncOrchestrator = null;
            }
        }
        private void cancelConn(SqlConnection conn)
        {
            if (conn != null)
            {
                conn.Close();
                conn = null;
            }
        }
        private bool disposed = false;
        ~Sync()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);   
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    //释放托管对象
                    ScopeNames = null;
                    ScopeTables = null;
                    TaskScheduler.UnobservedTaskException-=
                        new EventHandler<UnobservedTaskExceptionEventArgs>(TaskScheduler_UnobservedTaskException);
                }         
                //释放非托管对象
                CancelSyncOrchestrator(downloadSyncOrchestrator);
                CancelSyncOrchestrator(uploadSyncOrchestrator);
                CancelSyncOrchestrator(uploadAndDownloadSyncOrchestrator);
                CancelSyncOrchestrator(WRDownloadSyncOrchestrator);
                CancelSyncOrchestrator(WRuploadSyncOrchestrator);
                CancelSyncOrchestrator(KitchenuploadSyncOrchestrator);
                CancelSyncOrchestrator(PackageSyncOrchestrator);
                CancelSyncOrchestrator(PackageuploadSyncOrchestrator);
                CancelSyncOrchestrator(InvDeptPriceSyncOrchestrator);
                //CancelSyncOrchestrator(downloadStockSyncOrchestrator);
                //CancelSyncOrchestrator(uploadStockSyncOrchestrator);
                //CancelSyncOrchestrator(uploadAndDownloadStockSyncOrchestrator);
                cancelConn(ClientConn);
                cancelConn(ServerConn);

                disposed = true;
            }
        }
        #endregion
    }
}
