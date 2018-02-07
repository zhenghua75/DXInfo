using System;
using System.Web;
using System.IO;
using System.Net;
using System.Xml;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Text;

namespace AutoUpdate
{
    //// The RequestState class is used to pass data
    //// across async calls
    //public class RequestState
    //{
    //    public HttpWebRequest Request;

    //    public RequestState()
    //    {
    //        Request = null;
    //    }
    //}
    ///// <summary>
    ///// Class makes asynchronous HTTP Get to remote URL
    ///// </summary>
    //public class GetHttpAsync
    //{
    //    public delegate void GetCompleteHandler();
    //    public event GetCompleteHandler GetComplete;

    //    public void getPage(String url)
    //    {
    //        try
    //        {
    //            HttpWebRequest req =
    //                (HttpWebRequest)WebRequest.Create(url);
    //            // Create the state object
    //            RequestState rs = new RequestState();

    //            // Add the request into the state 
    //            // so it can be passed around
    //            rs.Request = req;

    //            // Issue the async request
    //            req.BeginGetResponse(
    //                new AsyncCallback(this.ResponseCallback), rs);
    //        }
    //        catch (Exception exp)
    //        {
    //            Console.WriteLine("\r\nRequest failed. Reason:");
    //            while (exp != null)
    //            {
    //                Console.WriteLine(exp.Message);
    //                exp = exp.InnerException;
    //            }
    //        }
    //    }

    //    private void ResponseCallback(IAsyncResult ar)
    //    {
    //        // Get the RequestState object from the async result
    //        RequestState rs = (RequestState)ar.AsyncState;

    //        // Get the HttpWebRequest from RequestState
    //        HttpWebRequest req = rs.Request;

    //        // Get the HttpWebResponse object
    //        HttpWebResponse resp =
    //            (HttpWebResponse)req.EndGetResponse(ar);

    //        // Read data from the response stream
    //        Stream responseStream = resp.GetResponseStream();
    //        StreamReader sr = new StreamReader(responseStream);//, Encoding.UTF8);
    //        string strContent = sr.ReadToEnd();

    //        // Write out the first 512 characters
    //        Console.WriteLine("Length: {0}", strContent.Length);
    //        Console.WriteLine(strContent.Substring(0,
    //            strContent.Length < 512 ? strContent.Length : 512));

    //        // Raise the completion event 
    //        GetComplete();

    //        // Close down the response stream
    //        responseStream.Close();
    //    }
    //}
    public class MyWebClient : WebClient
    {
        public int TimeOut = 0;
        /// <summary>
        /// 超时时间（秒）
        /// </summary>
        /// <param name="timeOut"></param>
        public MyWebClient(int timeOut)
        {
            this.TimeOut = timeOut;
        }
        protected override WebRequest GetWebRequest(Uri address)
        {
            //return base.GetWebRequest(address);
            HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
            request.Timeout = 1000 * TimeOut;
            request.ReadWriteTimeout = 1000 * TimeOut;
            return request;
        }
    }
	/// <summary>
	/// updater 的摘要说明。
	/// </summary>
	public class AppUpdater:IDisposable
	{
		#region 成员与字段属性
		private string _updaterUrl;
		private bool disposed = false;
		private IntPtr handle;
		private Component component = new Component();
		[System.Runtime.InteropServices.DllImport("Kernel32")]
		private extern static Boolean CloseHandle(IntPtr handle);


		public string UpdaterUrl
		{
			set{_updaterUrl = value;}
			get{return this._updaterUrl;}
		}
		#endregion

		/// <summary>
		/// AppUpdater构造函数
		/// </summary>
		public AppUpdater()
		{
			this.handle = handle;
		}
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		private void Dispose(bool disposing)
		{
			if(!this.disposed)
			{
				if(disposing)
				{
				
					component.Dispose();
				}
				CloseHandle(handle);
				handle = IntPtr.Zero;            
			}
			disposed = true;         
		}

		~AppUpdater()      
		{
			Dispose(false);
		}


		/// <summary>
		/// 检查更新文件
		/// </summary>
		/// <param name="serverXmlFile"></param>
		/// <param name="localXmlFile"></param>
		/// <param name="updateFileList"></param>
		/// <returns></returns>
		public int CheckForUpdate(string serverXmlFile,string localXmlFile,out Hashtable updateFileList)
		{
			updateFileList = new Hashtable();
			if(!File.Exists(localXmlFile) || !File.Exists(serverXmlFile))
			{
				return -1;
			}
			
			XmlFiles serverXmlFiles = new XmlFiles(serverXmlFile);
			XmlFiles localXmlFiles = new XmlFiles(localXmlFile);
			
			XmlNodeList newNodeList = serverXmlFiles.GetNodeList("AutoUpdater/Files");
			XmlNodeList oldNodeList = localXmlFiles.GetNodeList("AutoUpdater/Files");

			int k = 0;
			for(int i = 0;i < newNodeList.Count;i++)
			{
				string [] fileList = new string[3];

				string newFileName = newNodeList.Item(i).Attributes["Name"].Value.Trim();
				string newVer = newNodeList.Item(i).Attributes["Ver"].Value.Trim();
				
				ArrayList oldFileAl = new ArrayList();
				for(int j = 0;j < oldNodeList.Count;j++)
				{
					string oldFileName = oldNodeList.Item(j).Attributes["Name"].Value.Trim();
					string oldVer = oldNodeList.Item(j).Attributes["Ver"].Value.Trim();
					
					oldFileAl.Add(oldFileName);
					oldFileAl.Add(oldVer);

				}
				int pos = oldFileAl.IndexOf(newFileName);
				if(pos == -1)
				{
					fileList[0] = newFileName;
					fileList[1] = newVer;
					updateFileList.Add(k,fileList);
					k++;
				}
				else if(pos > -1 && newVer.CompareTo(oldFileAl[pos+1].ToString())>0 )
				{
					fileList[0] = newFileName;
					fileList[1] = newVer;
					updateFileList.Add(k,fileList);
					k++;
				}
				
			}
			return k;
		}
	
		/// <summary>
		/// 检查更新文件
		/// </summary>
		/// <param name="serverXmlFile"></param>
		/// <param name="localXmlFile"></param>
		/// <param name="updateFileList"></param>
		/// <returns></returns>
		public int CheckForUpdate()
		{
			string localXmlFile = Application.StartupPath + "\\UpdateList.xml";
			if(!File.Exists(localXmlFile))
			{
				return -1;
			}

			XmlFiles updaterXmlFiles = new XmlFiles(localXmlFile );


            string tempUpdatePath = Application.StartupPath + "\\AutoUpdateFiles" + "\\" + "_" + updaterXmlFiles.FindNode("//Application").Attributes["applicationId"].Value + "_" + "y" + "_" + "x" + "_" + "m" + "_" + "\\";
			this.UpdaterUrl = updaterXmlFiles.GetNodeValue("//Url") + "/UpdateList.xml";
			this.DownAutoUpdateFile(tempUpdatePath);

			string serverXmlFile = tempUpdatePath  +"\\UpdateList.xml";
			if(!File.Exists(serverXmlFile))
			{
				return -1;
			}
			
			XmlFiles serverXmlFiles = new XmlFiles(serverXmlFile);
			XmlFiles localXmlFiles = new XmlFiles(localXmlFile);
			
			XmlNodeList newNodeList = serverXmlFiles.GetNodeList("AutoUpdater/Files");
			XmlNodeList oldNodeList = localXmlFiles.GetNodeList("AutoUpdater/Files");

			int k = 0;
			for(int i = 0;i < newNodeList.Count;i++)
			{
				string [] fileList = new string[3];

				string newFileName = newNodeList.Item(i).Attributes["Name"].Value.Trim();
				string newVer = newNodeList.Item(i).Attributes["Ver"].Value.Trim();
				
				ArrayList oldFileAl = new ArrayList();
				for(int j = 0;j < oldNodeList.Count;j++)
				{
					string oldFileName = oldNodeList.Item(j).Attributes["Name"].Value.Trim();
					string oldVer = oldNodeList.Item(j).Attributes["Ver"].Value.Trim();
					
					oldFileAl.Add(oldFileName);
					oldFileAl.Add(oldVer);

				}
				int pos = oldFileAl.IndexOf(newFileName);
				if(pos == -1)
				{
					fileList[0] = newFileName;
					fileList[1] = newVer;
					k++;
				}
				else if(pos > -1 && newVer.CompareTo(oldFileAl[pos+1].ToString())>0 )
				{
					fileList[0] = newFileName;
					fileList[1] = newVer;
					k++;
				}
				
			}
			return k;
		}
		/// <summary>
		/// 返回下载更新文件的临时目录
		/// </summary>
		/// <returns></returns>
		public void DownAutoUpdateFile(string downpath)
		{
			if(!System.IO.Directory.Exists(downpath))
				System.IO.Directory.CreateDirectory(downpath);
			string serverXmlFile = downpath+@"/UpdateList.xml";

			try
			{
				WebRequest req = WebRequest.Create(this.UpdaterUrl);
                //req.Timeout = 1000 * 3;
				WebResponse res = req.GetResponse();
				if(res.ContentLength >0)
				{
					try
					{
						WebClient wClient = new WebClient();
                        //MyWebClient wClient = new MyWebClient(3);
						wClient.DownloadFile(this.UpdaterUrl,serverXmlFile);
					}
					catch(Exception ex)
					{
						throw ex;
					}
				}
			}
			catch(Exception ex)
			{
				throw ex;
			}
			//return tempPath;
		}
	}
}
