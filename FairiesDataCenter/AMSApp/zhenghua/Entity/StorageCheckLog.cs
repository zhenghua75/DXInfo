
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	StorageCheckLog.cs
* 作者:		     李贤
* 创建日期:     2010-3-18
* 功能描述:    仓库库存盘点流水表

*                                                           Copyright(C) 2010 lixian
*************************************************************************************/
#region Import NameSpace
using System;
using System.Data;
using AMSApp.zhenghua.EntityBase;
#endregion

namespace AMSApp.zhenghua.Entity
{
	/// <summary>
	/// **功能名称：仓库库存盘点流水表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbStorageCheckLog")]
	public class StorageCheckLog: EntityObjectBase
	{
		#region 数据表生成变量


		private decimal _cnnSerialNo;
		private string _cnvcCheckNo = String.Empty;
		private string _cnvcDeptID = String.Empty;
		private string _cnvcWhCode = String.Empty;
		private string _cnvcInvCode = String.Empty;
		private decimal _cnnSysCount;
		private decimal _cnnCheckCount;
		private string _cnvcUnitCode= String.Empty;
		private string _cnvcOperName = String.Empty;
		private DateTime _cndOperDate;
		private string _cnvcFlag = String.Empty;
		private DateTime _cndMdate;
		private DateTime _cndExpDate;
		
		#endregion
		
		#region 构造函数




		public StorageCheckLog():base()
		{
		}
		
		public StorageCheckLog(DataRow row):base(row)
		{
		}
		
		public StorageCheckLog(DataTable table):base(table)
		{
		}
		
		public StorageCheckLog(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnnSerialNo",IsPrimaryKey=true,IsIdentity=true,IsVersionNumber=false)]
		public decimal cnnSerialNo
		{
			get {return _cnnSerialNo;}
			set {_cnnSerialNo = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnvcCheckNo",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCheckNo
		{
			get {return _cnvcCheckNo;}
			set {_cnvcCheckNo = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnvcDeptID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcDeptID
		{
			get {return _cnvcDeptID;}
			set {_cnvcDeptID = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnvcWhCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcWhCode
		{
			get {return _cnvcWhCode;}
			set {_cnvcWhCode = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnvcInvCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcInvCode
		{
			get {return _cnvcInvCode;}
			set {_cnvcInvCode = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnnSysCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnSysCount
		{
			get {return _cnnSysCount;}
			set {_cnnSysCount = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnnCheckCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnCheckCount
		{
			get {return _cnnCheckCount;}
			set {_cnnCheckCount = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnvcUnitCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcUnitCode
		{
			get {return _cnvcUnitCode;}
			set {_cnvcUnitCode = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnvcOperName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcOperName
		{
			get {return _cnvcOperName;}
			set {_cnvcOperName = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cndOperDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndOperDate
		{
			get {return _cndOperDate;}
			set {_cndOperDate = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnvcFlag",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcFlag
		{
			get {return _cnvcFlag;}
			set {_cnvcFlag = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cndMdate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndMdate
		{
			get {return _cndMdate;}
			set {_cndMdate = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cndExpDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndExpDate
		{
			get {return _cndExpDate;}
			set {_cndExpDate = value;}
		}
		#endregion
	}	
}
