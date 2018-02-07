/*
  * Project: Code project article
  * Author: Veaceslav Macari
  * email: vmacari@gmail.com
  * Title: Windows Communication, Web Client Asynchronous file downloader
   */
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace DXInfo.Download
{
    class DownloaderTask
    {
        #region Private fields
        private WebClient webClient;
        private string fileName;
        private Uri uriData;
        private long fileSize;

        private System.Windows.Forms.ListView lvDetails;
        private System.Windows.Forms.ListViewGroup associatedGroup;
        private List<System.Windows.Forms.ListViewItem> descriptionItems;

        private const int DETAIL_URI = 0;
        private const int DETAIL_BYTES = 1;
        private const int DETAIL_CANCEL = 2;
        #endregion

        #region Constructors
        /// <summary>
        ///     Default constructor  
        /// </summary>
        /// <param name="uriData">the URI to the downloadable resource</param>
        public DownloaderTask (Uri uriData) 
            : this(uriData, null)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uriString"></param>
        public DownloaderTask(string uriString)
            : this(new Uri(uriString), null)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uriString"></param>
        /// <param name="lvDetails"></param>
        public DownloaderTask(string uriString, System.Windows.Forms.ListView lvDetails)
            :this (new Uri(uriString), lvDetails)
        {
        }

        /// <summary>
        ///     The intialization constructor
        /// </summary>
        /// <param name="uriData">The URI to downloadable resource</param>
        /// <param name="lvDetails">The list view wich holds status info</param>
        public DownloaderTask(Uri uriData, System.Windows.Forms.ListView lvDetails)
        {
            #region Prepare for download
            this.uriData = uriData;
            fileName = this.uriData.Segments[this.uriData.Segments.Length - 1];
            webClient = new WebClient();
            this.lvDetails = lvDetails;
            #endregion

            #region Assign call backs
            if (lvDetails != null)
            {
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webClient_DownloadProgressChanged);
                webClient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(webClient_DownloadFileCompleted);

                associatedGroup  = lvDetails.Groups.Add((new Guid()).ToString(), String.Format("{0} - 开始下载 ... ", fileName));
                descriptionItems = new List<System.Windows.Forms.ListViewItem>();
                descriptionItems.Add(new System.Windows.Forms.ListViewItem(uriData.OriginalString, associatedGroup)); // DETAIL_URI
                descriptionItems.Add(new System.Windows.Forms.ListViewItem("已下载 0/? 字节", associatedGroup)); // DETAIL_BYTES

                descriptionItems.Add(new System.Windows.Forms.ListViewItem("取消", associatedGroup)); // DETAIL_CANCEL
                descriptionItems[DETAIL_CANCEL].BackColor = System.Drawing.Color.Silver;
                descriptionItems[DETAIL_CANCEL].ForeColor = System.Drawing.Color.Blue;
                descriptionItems[DETAIL_URI].ImageIndex = 0;
                
                lvDetails.Items.AddRange(descriptionItems.ToArray());
                lvDetails.Update();
            }
            #endregion

            #region Start file download
            webClient.DownloadFileAsync(uriData, fileName, associatedGroup);
            //System.IO.File.SetLastWriteTime(fileName, DateTime.Now);
            #endregion
        }
        #endregion

        #region Downloader callback
        /// <summary>
        ///     Call back for download complete.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Download complete status</param>
        private void webClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            #region Check if additional info. is required
            if (associatedGroup == null)
            {
                return;
            }
            #endregion

            #region Process error state
            if (e.Error == null)
            {
                associatedGroup.Header = String.Format("{0} - 完成", fileName);
                descriptionItems[DETAIL_BYTES].ForeColor = System.Drawing.Color.Green;
                descriptionItems[DETAIL_BYTES].Text = String.Format("文件大小 - {0}", GetHumanReadableFileSize(fileSize));
            }
            else if (e.Cancelled == false && e.Error != null)
            {
                associatedGroup.Header = String.Format("{0} - 失败", fileName);
                descriptionItems[DETAIL_BYTES].Text = e.Error.Message;
                descriptionItems[DETAIL_BYTES].ForeColor = System.Drawing.Color.Red;
            }
            #endregion
            
            #region Process canceled download
            if (e.Cancelled == true)
            {
                associatedGroup.Header = String.Format("{0} - 取消", fileName);
                descriptionItems[DETAIL_BYTES].ForeColor = System.Drawing.Color.DarkGray;
            }
            #endregion

            descriptionItems[DETAIL_CANCEL].Text = "移除";
        }

        /// <summary>
        ///     A download progress is reported
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">the download progress information</param>
        private void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            #region Check if additional info. is required
            if (associatedGroup == null)
            {
                return;
            }
            #endregion

            #region Show progress info
            fileSize = e.TotalBytesToReceive;
            associatedGroup.Header = String.Format("{0} - {1}%", fileName, e.ProgressPercentage);
            descriptionItems[DETAIL_BYTES].Text = String.Format("Downloaded {0}/{1}", GetHumanReadableFileSize(e.BytesReceived), GetHumanReadableFileSize(fileSize));
            #endregion
        }
        #endregion

        #region Helper classes
        /// <summary>
        ///     Convert bytes to a human readable format
        /// </summary>
        /// <param name="fileSize">the number bytes</param>
        /// <returns>apropriate amount of bytes (Gb, Mb, Kb, bytes)</returns>
        private string GetHumanReadableFileSize(long fileSize)
        {
            #region Gb
            if ((fileSize / (1024 * 1024 * 1024)) > 0)
            {

                return String.Format("{0} Gb", (double)Math.Round((double)(fileSize / (1024 * 1024 * 1024)), 2));
            }
            #endregion

            #region Mb
            if ((fileSize / (1024 * 1024)) > 0)
            {
                return String.Format("{0} Mb", (double)Math.Round((double)(fileSize / (1024 * 1024)), 2));
            }
            #endregion

            #region Kb
            if ((fileSize / 1024) > 0)
            {
                return String.Format("{0} Kb", (double)Math.Round((double)(fileSize /1024), 2));
            }
            #endregion

            #region Bytes
            return String.Format("{0} b", fileSize);
            #endregion
        }
        #endregion

        #region Component event handler
        /// <summary>
        ///     Process the item click event. Cancel or remove a download. 
        /// </summary>
        /// <param name="item">The item which was clicked</param>
        /// <returns>True if the clicked item is managed by this component</returns>
        public bool ItemClicked (System.Windows.Forms.ListViewItem item)
        {

            if (associatedGroup != null && descriptionItems!= null && descriptionItems.Count > 0 && item == descriptionItems[DETAIL_CANCEL])
            {
                if (webClient.IsBusy == true)
                {
                    webClient.CancelAsync();
                } else 
                {
                    webClient.CancelAsync();
                    foreach(System.Windows.Forms.ListViewItem itemList in  descriptionItems )
                    {
                        lvDetails.Items.Remove(itemList);
                        itemList.Remove();
                    }

                    descriptionItems.Clear();
                    lvDetails.Groups.Remove(associatedGroup);
                }

                return true;
            }

            return false;
        }
        #endregion
    }
}
