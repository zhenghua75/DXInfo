
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	BusiLog.cs
* ����:	     ֣��
* ��������:    2008-9-29
* ��������:    ҵ����־

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region Import NameSpace
using System;
using System.Data;
using AMSApp.zhenghua.EntityBase;
#endregion

namespace AMSApp.zhenghua.Entity
{
	/// <summary>
	/// **�������ƣ�ҵ����־ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbBusiLog")]
	public class BusiLog: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private long _iSerial;
		private long _iAssID;
		private string _vcCardID = String.Empty;
		private string _vcOperType = String.Empty;
		private string _vcOperName = String.Empty;
		private DateTime _dtOperDate;
		private string _vcComments = String.Empty;
		private string _vcDeptID = String.Empty;
		
		#endregion
		
		#region ���캯��




		public BusiLog():base()
		{
		}
		
		public BusiLog(DataRow row):base(row)
		{
		}
		
		public BusiLog(DataTable table):base(table)
		{
		}
		
		public BusiLog(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("iSerial",IsPrimaryKey=true,IsIdentity=true,IsVersionNumber=false)]
		public long iSerial
		{
			get {return _iSerial;}
			set {_iSerial = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("iAssID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public long iAssID
		{
			get {return _iAssID;}
			set {_iAssID = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("vcCardID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string vcCardID
		{
			get {return _vcCardID;}
			set {_vcCardID = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("vcOperType",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string vcOperType
		{
			get {return _vcOperType;}
			set {_vcOperType = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("vcOperName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string vcOperName
		{
			get {return _vcOperName;}
			set {_vcOperName = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("dtOperDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime dtOperDate
		{
			get {return _dtOperDate;}
			set {_dtOperDate = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("vcComments",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string vcComments
		{
			get {return _vcComments;}
			set {_vcComments = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("vcDeptID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string vcDeptID
		{
			get {return _vcDeptID;}
			set {_vcDeptID = value;}
		}
		#endregion
	}	
}
