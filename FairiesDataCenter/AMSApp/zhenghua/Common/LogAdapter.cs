using System;
using System.IO;

using System.Data.SqlClient;

namespace AMSApp.zhenghua.Common
{
	/// <summary>
	/// 日志文件适配器.
	/// fightop@create 2006.6.21
	/// </summary>
	public class LogAdapter
	{
        /// <summary>
        /// 数据库异常日志文件名
        /// </summary>
        private const string DATABASEEX_FILE  = "DatabaseEx";
        /// <summary>
        /// 商业规则异常日志文件名
        /// </summary>
        private const string BUSINESSEX_FILE  = "BusinessEx";
        /// <summary>
        /// 党规异常日志文件名
        /// </summary>
        private const string FEATURESEX_FILE  = "Exception";
		/// <summary>
		/// 用户界面异常日志文件名
		/// </summary>
		private const string INTERFACEEX_FILE = "InterfaceEx";
		/// <summary>
		/// 自定义日志文件名
		/// </summary>
		private const string CUSTOMLOG_FILE   = "CustomLog";
		/// <summary>
		/// windows服务日志文件名
		/// </summary>
		private const string SERVICELOG_FILE  = "ServiceLog";
        /// <summary>
        /// 日志文件扩展名
        /// </summary>
        private const string LOG_EXTEND_NAME  = ".log"; 
        /// <summary>
        /// 分隔线
        /// </summary>
        private const string LINE = "_____________________________________________________________________________________________";


        private static string strDatabaseExPath  = "";
        private static string strBusinessExPath  = "";
        private static string strFeaturesExPath  = "";
		private static string strInterfaceExPath = "";
		private static string strCustomLogPath   = "";
		private static string strServiceLogPath  = "";

        /// <summary>
        /// 静态构造器
        /// </summary>
        static LogAdapter()
        {
            // 组装路径
            strDatabaseExPath  = AppDomain.CurrentDomain.BaseDirectory + ConfigAdapter.GetConfigNote("DatabaseExLogPath");
            strBusinessExPath  = AppDomain.CurrentDomain.BaseDirectory + ConfigAdapter.GetConfigNote("BusinessExLogPath");
            strFeaturesExPath  = AppDomain.CurrentDomain.BaseDirectory + ConfigAdapter.GetConfigNote("FeaturesExLogPath");
			strInterfaceExPath = AppDomain.CurrentDomain.BaseDirectory + ConfigAdapter.GetConfigNote("InterfaceExLogPath");
			strCustomLogPath   = AppDomain.CurrentDomain.BaseDirectory + ConfigAdapter.GetConfigNote("CustomLogPath");
			strServiceLogPath  = AppDomain.CurrentDomain.BaseDirectory + ConfigAdapter.GetConfigNote("ServiceLogPath");
        }

        /// <summary>
        /// 写自定义日志
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="strErrorMessage"></param>
        public static void write(String fileName,String strErrorMessage)
        {			
            FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory+fileName,FileMode.Append,FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(System.DateTime.Now.ToString());
            sw.WriteLine(LINE);
            sw.WriteLine(strErrorMessage);
            sw.WriteLine();
            sw.Close();
            fs.Close();
        }

		/// <summary>
		/// 写自定义日志
		/// </summary>
		/// <param name="strLogMessage">日志消息</param>
		public static void WriteCustomLog(string strLogMessage)
		{
			string strLogFileName = strCustomLogPath + CUSTOMLOG_FILE + System.DateTime.Now.ToString("yyyyMMdd") + LOG_EXTEND_NAME;

			if(false == Directory.Exists(strCustomLogPath))
			{// 如果目录不存在则创建
				Directory.CreateDirectory(strCustomLogPath);
			}

			FileStream fs = new FileStream(strLogFileName,FileMode.Append,FileAccess.Write);
			StreamWriter sw = new StreamWriter(fs);
			sw.WriteLine(System.DateTime.Now.ToString());
			sw.WriteLine(LINE);
			sw.WriteLine("Message:" + strLogMessage);
			sw.WriteLine();
			sw.Close();
			fs.Close();
		}

        /// <summary>
        /// 写常规异常
        /// </summary>
        /// <param name="e">异常对像</param>
        public static void WriteFeaturesException(Exception e)
        {
            string strLogFileName = strFeaturesExPath + FEATURESEX_FILE + System.DateTime.Now.ToString("yyyyMMdd") + LOG_EXTEND_NAME;

            if(false == Directory.Exists(strFeaturesExPath))
            {// 如果目录不存在则创建
                Directory.CreateDirectory(strFeaturesExPath);
            }

            FileStream fs = new FileStream(strLogFileName,FileMode.Append,FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(System.DateTime.Now.ToString());
            sw.WriteLine(LINE);
            sw.WriteLine("Message:" + e.Message);
            sw.WriteLine("Source :" + e.Source);
            sw.WriteLine();
            sw.WriteLine("Detail :");
            sw.WriteLine(e.StackTrace);
            sw.WriteLine();
            sw.Close();
            fs.Close();
        }

		/// <summary>
		/// 写界面异常
		/// </summary>
		/// <param name="e">异常对像</param>
		public static void WriteInterfaceException(Exception e)
		{
			string strLogFileName = strInterfaceExPath + INTERFACEEX_FILE + System.DateTime.Now.ToString("yyyyMMdd") + LOG_EXTEND_NAME;

			if(false == Directory.Exists(strInterfaceExPath))
			{// 如果目录不存在则创建
				Directory.CreateDirectory(strInterfaceExPath);
			}

			FileStream fs = new FileStream(strLogFileName,FileMode.Append,FileAccess.Write);
			StreamWriter sw = new StreamWriter(fs);
			sw.WriteLine(System.DateTime.Now.ToString());
			sw.WriteLine(LINE);
			sw.WriteLine("Message:" + e.Message);
			sw.WriteLine("Source :" + e.Source);
			sw.WriteLine();
			sw.WriteLine("Detail :");
			sw.WriteLine(e.StackTrace);
			sw.WriteLine();
			sw.Close();
			fs.Close();
		}

        /// <summary>
        /// 写业务规则异常
        /// </summary>
        /// <param name="e">异常对像</param>
        public static void WriteBusinessException(BusinessException be)
        {
            string strLogFileName = strBusinessExPath + BUSINESSEX_FILE + System.DateTime.Now.ToString("yyyyMMdd") + LOG_EXTEND_NAME;

            if(false == Directory.Exists(strBusinessExPath))
            {// 如果目录不存在则创建
                Directory.CreateDirectory(strBusinessExPath);
            }

            FileStream fs = new FileStream(strLogFileName,FileMode.Append,FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(System.DateTime.Now.ToString());
            sw.WriteLine(LINE);
			sw.WriteLine("Type   :" + be.Type);
            sw.WriteLine("Message:" + be.Message);
            sw.WriteLine("Source :" + be.Source);
            sw.WriteLine();
            sw.WriteLine("Detail :");
            sw.WriteLine(be.StackTrace);
            sw.WriteLine();
            sw.Close();
            fs.Close();
        }

        /// <summary>
        /// 写数据访问异常
        /// </summary>
        /// <param name="se">异常对像</param>
        public static void WriteDatabaseException(SqlException se)
        {
            string strLogFileName = strDatabaseExPath + DATABASEEX_FILE + System.DateTime.Now.ToString("yyyyMMdd") + LOG_EXTEND_NAME;

            if(false == Directory.Exists(strDatabaseExPath))
            {// 如果目录不存在则创建
                Directory.CreateDirectory(strDatabaseExPath);
            }

            FileStream fs = new FileStream(strLogFileName,FileMode.Append,FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(System.DateTime.Now.ToString());
            sw.WriteLine(LINE);
            sw.WriteLine("Message    :" + se.Message);
            sw.WriteLine("Source     :" + se.Source);
            sw.WriteLine();
            sw.WriteLine("LineNumber :" + se.LineNumber);
			sw.WriteLine("Procedure  :");
			sw.WriteLine(se.Procedure);
			sw.WriteLine();
            sw.WriteLine("HelpLink   :");
            sw.WriteLine(se.HelpLink);
            sw.WriteLine();
            sw.WriteLine("Detail     :");
            sw.WriteLine(se.StackTrace);
            sw.WriteLine();
            sw.Close();
            fs.Close();
        }


		/// <summary>
		/// 写windows服务日志
		/// </summary>
		/// <param name="strLogMessage">日志消息</param>
		public static void WriteServiceLog(string strLogMessage)
		{
			string strLogFileName = strServiceLogPath + SERVICELOG_FILE + System.DateTime.Now.ToString("yyyyMMdd") + LOG_EXTEND_NAME;

			if(false == Directory.Exists(strServiceLogPath))
			{// 如果目录不存在则创建
				Directory.CreateDirectory(strServiceLogPath);
			}

			FileStream fs = new FileStream(strLogFileName,FileMode.Append,FileAccess.Write);
			StreamWriter sw = new StreamWriter(fs);
			sw.WriteLine(System.DateTime.Now.ToString());
			sw.WriteLine(LINE);
			sw.WriteLine("Message:\n" + strLogMessage);
			sw.WriteLine();
			sw.Close();
			fs.Close();
		}

	}
}
