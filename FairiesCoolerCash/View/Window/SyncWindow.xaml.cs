using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Synchronization;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;
using DXInfo.Sync;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System.Collections.ObjectModel;
namespace FairiesCoolerCash.Business
{
    /// <summary>
    /// 同步
    /// </summary>
    public partial class SyncWindow : Window
    {
        
        public SyncWindow()
        {
            InitializeComponent();
            //backgroundWorker1 = new BackgroundWorker();
            //backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            //backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            //backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }

        //void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    //throw new NotImplementedException();
            
            
        //}

        //void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    //throw new NotImplementedException();

        //    btnsync.IsEnabled = false;
        //    btnclose.IsEnabled = false;
        //    lstStatistics.Items.Add("数据库同步开始");
        //    List<SyncOperationStatistics> lsyncOperationStatistics = e.Result as List<SyncOperationStatistics>;
        //    if (lsyncOperationStatistics != null && lsyncOperationStatistics.Count > 0)
        //    {
        //        int iDownloadChangesApplied = 0;
        //        int iDownloadChangesFailed = 0;
        //        int iDownloadChangesTotal = 0;
        //        int iUploadChangesApplied = 0;
        //        int iUploadChangesFailed = 0;
        //        int iUploadChangesTotal = 0;
        //        lstStatistics.Items.Add("开始时间: " + lsyncOperationStatistics[0].SyncStartTime);
        //        foreach (SyncOperationStatistics syncOperationStatistics in lsyncOperationStatistics)
        //        {
        //            //lstStatistics.Items.Add("开始时间: " + syncOperationStatistics.SyncStartTime);
                    
        //            //lstStatistics.Items.Add("下载: " + syncOperationStatistics.DownloadChangesApplied.ToString());
        //            iDownloadChangesApplied +=syncOperationStatistics.DownloadChangesApplied;
        //            //lstStatistics.Items.Add("下载失败: " + syncOperationStatistics.DownloadChangesFailed.ToString());
        //            iDownloadChangesFailed += syncOperationStatistics.DownloadChangesFailed;
        //            //lstStatistics.Items.Add("下载总数: " + syncOperationStatistics.DownloadChangesTotal.ToString());
        //            iDownloadChangesTotal += syncOperationStatistics.DownloadChangesTotal;
        //            //lstStatistics.Items.Add("上传: " + syncOperationStatistics.UploadChangesApplied.ToString());
        //            iUploadChangesApplied += syncOperationStatistics.UploadChangesApplied;
        //            //lstStatistics.Items.Add("上传失败: " + syncOperationStatistics.UploadChangesFailed.ToString());
        //            iUploadChangesFailed += syncOperationStatistics.UploadChangesFailed;
        //            //lstStatistics.Items.Add("上传总数: " + syncOperationStatistics.UploadChangesTotal.ToString());
        //            iUploadChangesTotal += syncOperationStatistics.UploadChangesTotal;
        //            //lstStatistics.Items.Add("完成时间: " + syncOperationStatistics.SyncEndTime);

        //        }
        //        lstStatistics.Items.Add("下载成功: " + iDownloadChangesApplied.ToString());
        //        lstStatistics.Items.Add("下载失败: " + iDownloadChangesFailed.ToString());
        //        lstStatistics.Items.Add("下载总数: " + iDownloadChangesTotal.ToString());
        //        lstStatistics.Items.Add("上传成功: " + iUploadChangesApplied.ToString());
        //        lstStatistics.Items.Add("上传失败: " + iUploadChangesFailed.ToString());
        //        lstStatistics.Items.Add("上传总数: " + iUploadChangesTotal.ToString());
        //        lstStatistics.Items.Add("完成时间: " + lsyncOperationStatistics[lsyncOperationStatistics.Count - 1].SyncEndTime);
        //    }
        //    lstStatistics.Items.Add("数据库同步完成");
        //    btnsync.IsEnabled = true;
        //    btnclose.IsEnabled = true;
        //}

        //void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    try
        //    {
        //        Sync s = Sync.Instance();
        //        this.lsos = s.lsos;
        //        if (s.IsRun)
        //        {
        //            MessageBox.Show("同步已在后台运行，请等待！", "同步"); 
        //        }
        //        s.ExcuteSync();
        //        e.Result = s.lsos;
        //    }
        //    catch (Exception ex)
        //    {
        //        ExceptionPolicy.HandleException(ex, "Policy");
        //        MessageBox.Show(ex.Message, "同步异常");
        //    }
        //}

        //private BackgroundWorker backgroundWorker1;
        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    btnclose.IsEnabled = false;
        //    btnsync.IsEnabled = false;
        //    backgroundWorker1.RunWorkerAsync();
        //    //sync();
        //    //NormalDelegate d = new NormalDelegate(sync);
        //    //this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, d);    
        //}
    }
}
