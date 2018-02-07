using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Xml.Linq;
using System.Threading;

namespace DXInfo.Download
{
    public partial class RemoteDownloadFrm : Form
    {
        public string UrlString
        {
            get;
            set;
        }
        public string RequstUrlString { get; set; }
        public string BaseRequestUrlString { get; set; }
        public string DestDir
        {
            get;
            set;
        }
        public string BaseDestDir { get; set; }
        public bool IsLoadFrm { get; set; }
        public bool IsMsg { get; set; }
        public RemoteDownloadFrm(string urlString, string destDir, string requestUrlString,bool isloadfrm,bool isMsg)
        {
            InitializeComponent();
            this.UrlString = urlString;
            this.BaseDestDir = destDir;
            this.BaseRequestUrlString = requestUrlString;
            this.IsLoadFrm = isloadfrm;
            this.IsMsg = isMsg;
        }
        public void DownloadFile(BindingList<DownloadFileInfo> lf,string folderName)
        {
            //Application.DoEvents();
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
            catch (Exception)
            {
                MessageBox.Show("获取文件列表失败", "同步文件错误");
                return;
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
                string sourFilePath = this.UrlString + "/" + folderName+"/" + fileName;
                if (File.Exists(descFilePath))
                {
                    continue;
                }
                  lf.Add(new DownloadFileInfo() { FileName = f.FileName, FileSize = f.FileSize+"k", ModifyDate = dt.ToString("yyyy-MM-dd HH:mm") });
                //dataGridView1.Refresh();
                try
                {
                    
                    using (WebClient client = new WebClient())
                    {
                        byte[] fileData = client.DownloadData(sourFilePath);
                        if (!Directory.Exists(this.DestDir))
                        {
                            Directory.CreateDirectory(this.DestDir);
                        }
                        using (FileStream fs = new FileStream(descFilePath, FileMode.CreateNew))
                        {
                            fs.Write(fileData, 0, fileData.Length);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("下载失败。\n"+ex.Message, "同步文件错误");
                }
            }
            
        }

        public void DownloadFile2()
        {
            Application.DoEvents();
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
            catch (Exception)
            {
                //MessageBox.Show("获取文件列表失败", "同步文件错误");
                return;
                //throw new Exception("获取文件列表失败", ex.InnerException);
            }
            XDocument xd = XDocument.Parse(strHtml);
            var q = from x in xd.Descendants("File")
                    select new
                    {
                        FileName = x.Attribute("name").Value,
                        ModifyDate = x.Attribute("date").Value,
                        FileSize = x.Attribute("size").Value
                    };
            BindingList<DownloadFileInfo> lf = new BindingList<DownloadFileInfo>();
            BindingSource bs = new BindingSource();
            bs.DataSource = lf;
            dataGridView1.DataSource = bs;
            foreach (var f in q)
            {
                string fileName = f.FileName;
                DateTime dt = DateTime.ParseExact(f.ModifyDate, "yyyyMMddHHmm", null);
                string descFilePath = Path.Combine(this.DestDir, fileName);
                string sourFilePath = this.UrlString + "/" + fileName;
                //try
                //{
                //    WebRequest myre = WebRequest.Create(sourFilePath);
                //}
                //catch (Exception ex)
                //{
                //    throw new Exception("服务器上文件不存在", ex.InnerException);
                //}
                if (File.Exists(descFilePath))
                {
                    FileInfo fi = new System.IO.FileInfo(descFilePath);
                    DateTime dt2 = DateTime.ParseExact(fi.LastWriteTime.ToString("yyyyMMddHHmm"), "yyyyMMddHHmm", null);
                    int lgth = Convert.ToInt32(Math.Round(Convert.ToDecimal(fi.Length) / 1024));
                    if (lgth == 0) lgth = 1;
                    int size = Convert.ToInt32(f.FileSize);
                    if (dt == dt2 && lgth == size) continue;
                }
                //string[] strs = new string[] { f.FileName, f.FileSize, dt.ToString("yyyy-MM-dd HH:mm")};
                //ListViewItem lvi = new ListViewItem(strs);
                //listView1.Items.Add(lvi);
                lf.Add(new DownloadFileInfo() { FileName = f.FileName, FileSize = f.FileSize + "k", ModifyDate = dt.ToString("yyyy-MM-dd HH:mm") });
                //dataGridView1.DataSource = lf;
                dataGridView1.Refresh();
                try
                {

                    using (WebClient client = new WebClient())
                    {
                        byte[] fileData = client.DownloadData(sourFilePath);
                        if (!Directory.Exists(this.DestDir))
                        {
                            Directory.CreateDirectory(this.DestDir);
                        }
                        using (FileStream fs = new FileStream(descFilePath, FileMode.OpenOrCreate))
                        {
                            fs.Write(fileData, 0, fileData.Length);
                        }
                    }
                    File.SetLastWriteTime(descFilePath, dt);
                }
                catch (Exception)
                {
                    //throw new Exception("下载失败", ex.InnerException);
                    //MessageBox.Show("下载失败。\n" + ex.Message, "同步文件错误");
                }
            }
            //if (IsMsg)
            //{
            //    if (lf.Count == 0)
            //        MessageBox.Show("下载完成，无文件需更新");
            //    else
            //        MessageBox.Show("下载完成");
            //}
        }
        private void RemoteDownloadFrm_Load(object sender, EventArgs e)
        {
            //if (IsLoadFrm)
            //{
            //    DownloadFile();
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.Dispose();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //DownloadFile();
            
            this.backgroundWorker1.RunWorkerAsync();
            
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            this.button1.Enabled = false;
            this.button2.Enabled = false;
            BindingList<DownloadFileInfo> lf = new BindingList<DownloadFileInfo>();
            BindingSource bs = new BindingSource();
            bs.DataSource = lf;
            dataGridView1.Rows.Clear();
            //dataGridView1.DataSource = null;
            dataGridView1.DataSource = bs;

            this.RequstUrlString = string.Format(this.BaseRequestUrlString, "%E5%BF%AB%E8%8A%82%E5%A5%8F");            
            this.DestDir = this.BaseDestDir + @"快节奏\";
            DownloadFile(lf,"快节奏");

            this.RequstUrlString = string.Format(this.BaseRequestUrlString, "%E4%B8%AD%E8%8A%82%E5%A5%8F");
            this.DestDir = this.BaseDestDir + @"中节奏\";
            DownloadFile(lf, "中节奏");

            this.RequstUrlString = string.Format(this.BaseRequestUrlString, "%E6%85%A2%E8%8A%82%E5%A5%8F");
            this.DestDir = this.BaseDestDir + @"慢节奏\";
            DownloadFile(lf, "慢节奏");

            this.RequstUrlString = string.Format(this.BaseRequestUrlString, "%E6%89%93%E7%83%8A%E9%9F%B3%E4%B9%90");
            this.DestDir = this.BaseDestDir + @"打烊音乐\";
            DownloadFile(lf, "打烊音乐");

            this.RequstUrlString = string.Format(this.BaseRequestUrlString, "%E8%8A%82%E5%81%87%E6%97%A5");
            this.DestDir = this.BaseDestDir + @"节假日\";
            DownloadFile(lf, "节假日");

            this.RequstUrlString = string.Format(this.BaseRequestUrlString, "%E6%96%B0%E6%AD%8C");
            this.DestDir = this.BaseDestDir + @"新歌\";
            DownloadFile(lf, "新歌");

            this.RequstUrlString = string.Format(this.BaseRequestUrlString, "%E5%85%B6%E4%BB%96");
            this.DestDir = this.BaseDestDir + @"其他\";
            DownloadFile(lf, "其他");
            
            if (IsMsg)
            {
                if (lf.Count == 0)
                    MessageBox.Show(this,"下载完成，无文件需更新");
                else
                    MessageBox.Show(this,"下载完成");
            }
            

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.button1.Enabled = true;
            this.button2.Enabled = true;
        }
    }

    public class DownloadFileInfo
    {
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string ModifyDate { get; set; }
    }
}
