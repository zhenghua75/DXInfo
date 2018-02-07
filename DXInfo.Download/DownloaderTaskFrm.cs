/*
  * Project: Code project article
  * Author: Veaceslav Macari
  * email: vmacari@gmail.com
  * Title: Windows Communication, Web Client Asynchronous file downloader
   */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Linq;
namespace DXInfo.Download
{
    public partial class DownloaderTaskFrm : Form
    {
        public string UrlString
        {
            get;
            set;
        }
        public string RequstUrlString { get; set; }
        public string DestDir
        {
            get;
            set;
        }
        #region Private fields
        // private WebClient webClient = null;
        /// <summary>
        ///     A list of all active downloads
        /// </summary>
        private List<DownloaderTask> dwnTasks = new List<DownloaderTask>();

        #endregion

        #region Constructors
        public DownloaderTaskFrm(string urlString, string destDir, string requestUrlString)
        {
            InitializeComponent();
            this.UrlString = urlString;
            this.DestDir = destDir;
            this.RequstUrlString = requestUrlString;
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //dwnTasks.Add(new DownloaderTask(tbUri.Text, lvDownloads));

            string strHtml = "";
            try
            {
                StreamReader sr = null;
                System.Text.Encoding code = System.Text.Encoding.UTF8;
                WebRequest wr = WebRequest.Create(this.RequstUrlString);
                WebResponse rp = null;
                rp = wr.GetResponse();
                sr = new StreamReader(rp.GetResponseStream(), code);
                strHtml = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
                rp.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("获取文件列表失败", ex.InnerException);
            }

            XDocument xd = XDocument.Parse(strHtml);
            var q = from x in xd.Descendants("File")
                    select new
                    {
                        FileName = x.Attribute("name").Value,
                        ModifyDate = x.Attribute("date").Value,
                        FileSize = x.Attribute("size").Value
                    };
            foreach (var f in q)
            {
                string fileName = f.FileName;
                DateTime dt = DateTime.ParseExact(f.ModifyDate, "yyyyMMddHHmm", null);
                string descFilePath = Path.Combine(this.DestDir, fileName);
                string sourFilePath = this.UrlString + "/" + fileName;
            }
            lvDownloads.Items.Clear();
            dwnTasks.Add(new DownloaderTask("http://localhost:1692/ckfinder/userfiles/images/1304241998_credit_card_add.png", lvDownloads));
            dwnTasks.Add(new DownloaderTask("http://localhost:1692/ckfinder/userfiles/images/test.jpg", lvDownloads));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvDownloads_ItemActivate(object sender, EventArgs e)
        {
            if (lvDownloads.SelectedItems.Count <= 0)
            {
                return;
            }

            foreach(DownloaderTask dwnTask in dwnTasks)
            {
                if(dwnTask.ItemClicked(lvDownloads.SelectedItems[0]))
                {
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Form events handlers
/*
        private void btnDownload_Click(object sender, EventArgs e)
        {
            #region Start new download 
            if (webClient == null)
            {
                Uri fileUri = new Uri(tbUri.Text);

                #region Accept only files
                if (fileUri.IsFile == false)
                {
                    MessageBox.Show("Not a file!", "URI Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                #endregion

                #region Prepare for download 
                webClient = new WebClient();
                btnDownload.Text = "Cancel";
                string[] segments = fileUri.Segments;
                #endregion

                #region Assign call backs
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webClient_DownloadProgressChanged);
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(webClient_DownloadFileCompleted);
                #endregion

                #region Start file download
                webClient.DownloadFileAsync(fileUri, segments[segments.Length - 1], segments[segments.Length - 1]);
                rtbMessages.Text += String.Format("Download started for {0}\n", segments[segments.Length - 1]);
                #endregion
            }
            #endregion

            #region Cancel download
            else if (webClient != null)
            {
                webClient.CancelAsync();
                btnDownload.Text = "Download";
            }
            #endregion
        }
        #endregion

        #region Asycnronous download events
        /// <summary>
        ///     Download complete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            rtbMessages.Text += String.Format("Download complete for {0}. Error state: {1}, Canceled - {2} \n", e.UserState, e.Error, e.Cancelled);
        }

        /// <summary>
        ///     Download progress
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            rtbMessages.Text += String.Format("[{0}] - {1}/{2} bytes. Completed - {3}\n",
                e.UserState, e.BytesReceived, e.TotalBytesToReceive, e.ProgressPercentage);
        }
 */ 
        #endregion
    }
}
