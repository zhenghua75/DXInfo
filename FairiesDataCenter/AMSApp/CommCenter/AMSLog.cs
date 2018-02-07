using System;
using System.IO;

namespace CommCenter
{
	/// <summary>
	/// Summary description for AMSLog.
	/// </summary>
	public class AMSLog
	{
		string strFileName;
		StreamWriter writer;
		string strPathName;

		public AMSLog()
		{
			//
			// TODO: Add constructor logic here
			//
			strPathName=@"c:\AMSLog\";
			if(!Directory.Exists(strPathName))
				Directory.CreateDirectory(strPathName);

			strFileName = GetCurrentTimeFileName();
		}
		
		string GetCurrentTimeFileName()
		{
			return strPathName+"\\error"+DateTime.Now.ToString("yyyy-MM-dd")+".txt";
		}

		public void WriteLine(Exception e)
		{
			lock(this)
			{
				if(strFileName ==null) return;
				string strNewFileName = GetCurrentTimeFileName();
				if(!strNewFileName.Equals(strFileName))
				{
					writer.Close();
					writer = new StreamWriter(strNewFileName,true);
					strFileName =  strNewFileName;
				}
				else
				{
					writer = new StreamWriter(strFileName,true);
				}
				string strLog = "\n" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss\t") + e.ToString();
				strLog = strLog.Replace("\r\n","|");
				writer.WriteLine(strLog);
				writer.Flush();
			}
			writer.Close();
		}

		public void WriteLine(string strerr)
		{
			lock(this)
			{
				if(strFileName ==null) return;
				string strNewFileName = GetCurrentTimeFileName();
				if(!strNewFileName.Equals(strFileName))
				{
					writer.Close();
					writer = new StreamWriter(strNewFileName,true);
					strFileName =  strNewFileName;
				}
				else
				{
					writer = new StreamWriter(strFileName,true);
				}
				string strLog = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss\t") + strerr;
				strLog = strLog.Replace("\r\n","|");
				writer.WriteLine(strLog);
				writer.Flush();
			}
			writer.Close();
		}

		public void Close()
		{
			lock(this)
			{
				strFileName = null;
				if(writer != null)
					writer.Close();
				writer = null;
			}
		}

		public void Dispose()
		{
			try
			{
				if(writer != null)
					writer.Close();
				writer =null;
			}
			catch
			{}
		}
	}
}
