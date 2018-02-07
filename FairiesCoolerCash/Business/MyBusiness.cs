using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using DXInfo.Models;

namespace FairiesCoolerCash.Business
{
    public class MyDownloadBusiness
    {
        public string ConnectorUrlString { get; set; }
        public string ResourceType { get; set; }
        public string DestDir { get; set; }
        public WebClient client { get; set; }
        public delegate void DownloadMsgEventHandler(object sender, DownloadMsgEventArgs e);
        public event DownloadMsgEventHandler DownloadMsgEvent;
        public bool IsRun { get; set; }
        #region 文件下载

        public MyDownloadBusiness(string connectorUrlString)
        {
            this.ConnectorUrlString = connectorUrlString;
            client = new WebClient();
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
        }
        private static MyDownloadBusiness DownloadMp3Business = null;
        public static MyDownloadBusiness DownloadMp3BusinessInstance(string connectorUrlString)
        {
            if (null == DownloadMp3Business)
            {
                DownloadMp3Business = new MyDownloadBusiness(connectorUrlString);
                DownloadMp3Business.ResourceType = "mp3";
                DownloadMp3Business.DestDir = AppDomain.CurrentDomain.BaseDirectory + @"mp3\";
            }
            return DownloadMp3Business;
        }
        private static MyDownloadBusiness DownloadImageBusiness = null;
        public static MyDownloadBusiness DownloadImageBusinessInstance(string connectorUrlString)
        {
            if (null == DownloadImageBusiness)
            {
                DownloadImageBusiness = new MyDownloadBusiness(connectorUrlString);
                DownloadImageBusiness.ResourceType = "images";
                DownloadImageBusiness.DestDir = ConfigurationManager.AppSettings["imageFilePath"];
            }
            return DownloadImageBusiness;
        }
        private string getRequestString(string url)
        {
            string strHtml = string.Empty;
            using (WebClient rclient = new WebClient())
            {
                rclient.Encoding = UTF8Encoding.UTF8;
                strHtml = rclient.DownloadString(url);
            }
            return strHtml;
        }
        private List<DownloadFileInfo> getFileInfo(string folderName, string filesString)
        {
            XDocument xd = XDocument.Parse(filesString);
            List<DownloadFileInfo> q = (from x in xd.Descendants("File")
                                        select new DownloadFileInfo
                                        {
                                            FileName = x.Attribute("name").Value,
                                            Md5 = x.Attribute("md5").Value,
                                            FileSize = x.Attribute("size").Value,
                                            ModifyDate = Convert.ToDateTime(x.Attribute("date").Value),
                                            ResourceType = ResourceType,
                                            CurrentFolder = folderName,
                                        }).ToList();
            return q;
        }
        private List<string> getFolderInfo(string foldersString)
        {
            XDocument xdFolder = XDocument.Parse(foldersString);
            Dictionary<string, List<DownloadFileInfo>> dict = new Dictionary<string, List<DownloadFileInfo>>();
            List<string> folder = (from x in xdFolder.Descendants("Folder")
                                   select x.Attribute("name").Value).ToList();
            return folder;
        }
        private string getFoldersString()
        {
            return ConnectorUrlString
                + "?command=GetFolders"
                + "&type=" + ResourceType;
        }
        private string getFilesString()
        {
            return ConnectorUrlString
                + "?command=GetFiles"
                + "&type=" + ResourceType;
        }
        private List<DownloadFileInfo> procFile(FileInfo fileInfo, List<DownloadFileInfo> lFileInfo)
        {
            DownloadFileInfo dFileInfo = lFileInfo.Where(w => w.FileName == fileInfo.Name).FirstOrDefault();
            if (dFileInfo == null)
            {
                File.Delete(fileInfo.FullName);
            }
            else
            {
                FileStream fs = fileInfo.OpenRead();
                string destMd5 = DXInfo.Business.Helper.GetMd5Hash(fs);
                fs.Close();
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;
                if (0 == comparer.Compare(dFileInfo.Md5, destMd5))
                {
                    lFileInfo.Remove(dFileInfo);
                }
                else
                {
                    File.Delete(fileInfo.FullName);
                }
            }
            return lFileInfo;
        }
        private void procFile(Dictionary<string, List<DownloadFileInfo>> dict,
            FileInfo[] aFiles, string currentFolder)
        {
            foreach (FileInfo fileInfo in aFiles)
            {
                List<DownloadFileInfo> lFileInfo = new List<DownloadFileInfo>();
                if (dict.ContainsKey(currentFolder))
                {
                    lFileInfo = dict[currentFolder];
                }
                lFileInfo = procFile(fileInfo, lFileInfo);
                if (dict.ContainsKey(currentFolder))
                {
                    dict[currentFolder] = lFileInfo;
                }
            }
        }

        private void OnDownloadMsgEvent(object sender, DownQueue dq, Exception error, bool completed)
        {
            if (dq != null && DownloadMsgEvent != null)
            {
                DownloadMsgEventArgs e = new DownloadMsgEventArgs(dq, error, completed);
                DownloadMsgEvent(sender, e);
            }
        }
        object lockIsRunObject = new object();
        public bool IsRunBlock(bool canRun = false)
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
        public void DownloadFile()
        {
            if (!IsRunBlock(true))
            {
                try
                {
                    #region 远程文件信息
                    string foldUrl = getFoldersString();
                    string fileUrl = getFilesString();
                    string foldersString = getRequestString(foldUrl);
                    List<string> lFolder = getFolderInfo(foldersString);
                    lFolder.RemoveAll(delegate(string str) { return str == "aspnet_client"; });
                    Dictionary<string, List<DownloadFileInfo>> dict = new Dictionary<string, List<DownloadFileInfo>>();
                    if (lFolder.Count > 0)
                    {
                        foreach (string folder in lFolder)
                        {
                            string filesString = getRequestString(fileUrl + "&CurrentFolder=" + WebUtility.HtmlEncode(folder));
                            List<DownloadFileInfo> lFileInfo = getFileInfo(folder, filesString);
                            if (lFileInfo.Count > 0)
                            {
                                dict.Add(folder, lFileInfo);
                            }
                        }
                    }
                    else
                    {
                        string filesString = getRequestString(fileUrl);
                        List<DownloadFileInfo> lFileInfo = getFileInfo(ResourceType, filesString);
                        if (lFileInfo.Count > 0)
                        {
                            dict.Add(ResourceType, lFileInfo);
                        }
                    }
                    #endregion

                    #region 处理本地目录及文件
                    if (Directory.Exists(DestDir) && dict.Count > 0)
                    {
                        DirectoryInfo oDir = new System.IO.DirectoryInfo(DestDir);
                        FileInfo[] aFiles = oDir.GetFiles();
                        procFile(dict, aFiles, ResourceType);

                        DirectoryInfo[] aDirectories = oDir.GetDirectories();
                        foreach (DirectoryInfo directoryInfo in aDirectories)
                        {
                            if (dict.ContainsKey(directoryInfo.Name))
                            {
                                FileInfo[] aFiles1 = directoryInfo.GetFiles();
                                procFile(dict, aFiles1, directoryInfo.Name);
                            }
                            else
                            {
                                directoryInfo.Delete(true);
                            }
                        }
                    }
                    #endregion


                    Queue<DownloadFileInfo> ll = new Queue<DownloadFileInfo>();
                    foreach (string folder in dict.Keys)
                    {
                        string downloadFileString = ConnectorUrlString
                                + "?command=DownloadFile"
                                + "&type=" + ResourceType;
                        if (!folder.Equals(ResourceType))
                        {
                            downloadFileString += "&CurrentFolder=" + WebUtility.HtmlEncode(folder);
                        }
                        foreach (DownloadFileInfo fileInfo in dict[folder])
                        {
                            string currentDownloadFileUrl = downloadFileString + "&FileName=" + WebUtility.HtmlEncode(fileInfo.FileName);
                            string descFilePath = folder == ResourceType ? Path.Combine(DestDir, fileInfo.FileName) : Path.Combine(DestDir, folder, fileInfo.FileName);

                            if (folder == ResourceType)
                            {
                                if (!Directory.Exists(DestDir))
                                {
                                    Directory.CreateDirectory(DestDir);
                                }
                            }
                            else
                            {
                                if (!Directory.Exists(Path.Combine(DestDir, folder)))
                                {
                                    Directory.CreateDirectory(Path.Combine(DestDir, folder));
                                }
                            }
                            fileInfo.DestDir = folder;
                            fileInfo.DescFilePath = descFilePath;
                            fileInfo.Url = currentDownloadFileUrl;
                            ll.Enqueue(fileInfo);
                        }
                    }
                    if (ll.Any())
                    {
                        DownloadFileInfo currentFile = ll.Dequeue();
                        DownQueue dq = new DownQueue();
                        dq.CurrentFile = currentFile;
                        dq.QueueFile = ll;
                        client.DownloadFileAsync(new Uri(currentFile.Url), currentFile.DescFilePath, dq);
                    }
                    else
                    {
                        IsRun = false;
                        client.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    IsRun = false;
                    client.Dispose();
                    throw ex;
                }
            }
        }

        private void procDownloadProgressChanged(DownloadFileInfo dfi, DownloadProgressChangedEventArgs e)
        {
            dfi.BytesReceived = Convert.ToInt64(Math.Round(Convert.ToDecimal(e.BytesReceived) / 1024));
            dfi.ProgressPercentage = (int)((dfi.BytesReceived * 100) / Convert.ToInt64(dfi.FileSize));
        }
        private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            DownQueue dq = e.UserState as DownQueue;
            procDownloadProgressChanged(dq.CurrentFile, e);
            OnDownloadMsgEvent(sender, dq, null, false);
        }
        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            DownQueue dq = e.UserState as DownQueue;
            dq.CurrentFile.Completed = true;
            if (e.Error != null)
            {
                dq.CurrentFile.ErrorMsg = e.Error.Message;
                Helper.HandelException(e.Error);
            }
            OnDownloadMsgEvent(sender, dq, e.Error, true);
            if (dq.QueueFile.Any())
            {
                DownloadFileInfo nextFile = dq.QueueFile.Dequeue();
                dq.CurrentFile = nextFile;
                client.DownloadFileAsync(new Uri(nextFile.Url), nextFile.DescFilePath, dq);
            }
            else
            {
                IsRun = false;
                client.Dispose();
            }
        }
        #endregion
    }
}
