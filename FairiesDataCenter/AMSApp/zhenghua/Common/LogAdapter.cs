using System;
using System.IO;

using System.Data.SqlClient;

namespace AMSApp.zhenghua.Common
{
	/// <summary>
	/// ��־�ļ�������.
	/// fightop@create 2006.6.21
	/// </summary>
	public class LogAdapter
	{
        /// <summary>
        /// ���ݿ��쳣��־�ļ���
        /// </summary>
        private const string DATABASEEX_FILE  = "DatabaseEx";
        /// <summary>
        /// ��ҵ�����쳣��־�ļ���
        /// </summary>
        private const string BUSINESSEX_FILE  = "BusinessEx";
        /// <summary>
        /// �����쳣��־�ļ���
        /// </summary>
        private const string FEATURESEX_FILE  = "Exception";
		/// <summary>
		/// �û������쳣��־�ļ���
		/// </summary>
		private const string INTERFACEEX_FILE = "InterfaceEx";
		/// <summary>
		/// �Զ�����־�ļ���
		/// </summary>
		private const string CUSTOMLOG_FILE   = "CustomLog";
		/// <summary>
		/// windows������־�ļ���
		/// </summary>
		private const string SERVICELOG_FILE  = "ServiceLog";
        /// <summary>
        /// ��־�ļ���չ��
        /// </summary>
        private const string LOG_EXTEND_NAME  = ".log"; 
        /// <summary>
        /// �ָ���
        /// </summary>
        private const string LINE = "_____________________________________________________________________________________________";


        private static string strDatabaseExPath  = "";
        private static string strBusinessExPath  = "";
        private static string strFeaturesExPath  = "";
		private static string strInterfaceExPath = "";
		private static string strCustomLogPath   = "";
		private static string strServiceLogPath  = "";

        /// <summary>
        /// ��̬������
        /// </summary>
        static LogAdapter()
        {
            // ��װ·��
            strDatabaseExPath  = AppDomain.CurrentDomain.BaseDirectory + ConfigAdapter.GetConfigNote("DatabaseExLogPath");
            strBusinessExPath  = AppDomain.CurrentDomain.BaseDirectory + ConfigAdapter.GetConfigNote("BusinessExLogPath");
            strFeaturesExPath  = AppDomain.CurrentDomain.BaseDirectory + ConfigAdapter.GetConfigNote("FeaturesExLogPath");
			strInterfaceExPath = AppDomain.CurrentDomain.BaseDirectory + ConfigAdapter.GetConfigNote("InterfaceExLogPath");
			strCustomLogPath   = AppDomain.CurrentDomain.BaseDirectory + ConfigAdapter.GetConfigNote("CustomLogPath");
			strServiceLogPath  = AppDomain.CurrentDomain.BaseDirectory + ConfigAdapter.GetConfigNote("ServiceLogPath");
        }

        /// <summary>
        /// д�Զ�����־
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
		/// д�Զ�����־
		/// </summary>
		/// <param name="strLogMessage">��־��Ϣ</param>
		public static void WriteCustomLog(string strLogMessage)
		{
			string strLogFileName = strCustomLogPath + CUSTOMLOG_FILE + System.DateTime.Now.ToString("yyyyMMdd") + LOG_EXTEND_NAME;

			if(false == Directory.Exists(strCustomLogPath))
			{// ���Ŀ¼�������򴴽�
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
        /// д�����쳣
        /// </summary>
        /// <param name="e">�쳣����</param>
        public static void WriteFeaturesException(Exception e)
        {
            string strLogFileName = strFeaturesExPath + FEATURESEX_FILE + System.DateTime.Now.ToString("yyyyMMdd") + LOG_EXTEND_NAME;

            if(false == Directory.Exists(strFeaturesExPath))
            {// ���Ŀ¼�������򴴽�
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
		/// д�����쳣
		/// </summary>
		/// <param name="e">�쳣����</param>
		public static void WriteInterfaceException(Exception e)
		{
			string strLogFileName = strInterfaceExPath + INTERFACEEX_FILE + System.DateTime.Now.ToString("yyyyMMdd") + LOG_EXTEND_NAME;

			if(false == Directory.Exists(strInterfaceExPath))
			{// ���Ŀ¼�������򴴽�
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
        /// дҵ������쳣
        /// </summary>
        /// <param name="e">�쳣����</param>
        public static void WriteBusinessException(BusinessException be)
        {
            string strLogFileName = strBusinessExPath + BUSINESSEX_FILE + System.DateTime.Now.ToString("yyyyMMdd") + LOG_EXTEND_NAME;

            if(false == Directory.Exists(strBusinessExPath))
            {// ���Ŀ¼�������򴴽�
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
        /// д���ݷ����쳣
        /// </summary>
        /// <param name="se">�쳣����</param>
        public static void WriteDatabaseException(SqlException se)
        {
            string strLogFileName = strDatabaseExPath + DATABASEEX_FILE + System.DateTime.Now.ToString("yyyyMMdd") + LOG_EXTEND_NAME;

            if(false == Directory.Exists(strDatabaseExPath))
            {// ���Ŀ¼�������򴴽�
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
		/// дwindows������־
		/// </summary>
		/// <param name="strLogMessage">��־��Ϣ</param>
		public static void WriteServiceLog(string strLogMessage)
		{
			string strLogFileName = strServiceLogPath + SERVICELOG_FILE + System.DateTime.Now.ToString("yyyyMMdd") + LOG_EXTEND_NAME;

			if(false == Directory.Exists(strServiceLogPath))
			{// ���Ŀ¼�������򴴽�
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
