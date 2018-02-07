
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	StorageLog.cs
* 作者:	     郑华
* 创建日期:    2008-11-3
* 功能描述:    库存日志表

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
	/// **功能名称：库存日志表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbStorageLog")]
	public class StorageLog: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private decimal _cnnSerialNo;
		private string _cnvcStorageDeptID = String.Empty;
		private string _cnvcProductCode = String.Empty;
		private string _cnvcProductName = String.Empty;
		private string _cnvcUnit = String.Empty;
		private decimal _cnnCount;
		private decimal _cnnSafeCount;
		private decimal _cnnSafeUpCount;
		private string _cnvcOperType = String.Empty;
		private string _cnvcOperID = String.Empty;
		private DateTime _cndOperDate;
		
		#endregion
		
		#region 构造函数




		public StorageLog():base()
		{
		}
		
		public StorageLog(DataRow row):base(row)
		{
		}
		
		public StorageLog(DataTable table):base(table)
		{
		}
		
		public StorageLog(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 日志流水
		/// </summary>
		[ColumnMapping("cnnSerialNo",IsPrimaryKey=true,IsIdentity=true,IsVersionNumber=false)]
		public decimal cnnSerialNo
		{
			get {return _cnnSerialNo;}
			set {_cnnSerialNo = value;}
		}

		/// <summary>
		/// 仓库
		/// </summary>
		[ColumnMapping("cnvcStorageDeptID",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcStorageDeptID
		{
			get {return _cnvcStorageDeptID;}
			set {_cnvcStorageDeptID = value;}
		}

		/// <summary>
		/// 产品编码
		/// </summary>
		[ColumnMapping("cnvcProductCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductCode
		{
			get {return _cnvcProductCode;}
			set {_cnvcProductCode = value;}
		}

		/// <summary>
		/// 产品名称
		/// </summary>
		[ColumnMapping("cnvcProductName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductName
		{
			get {return _cnvcProductName;}
			set {_cnvcProductName = value;}
		}

		/// <summary>
		/// 单位
		/// </summary>
		[ColumnMapping("cnvcUnit",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcUnit
		{
			get {return _cnvcUnit;}
			set {_cnvcUnit = value;}
		}

		/// <summary>
		/// 实际库存数量
		/// </summary>
		[ColumnMapping("cnnCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnCount
		{
			get {return _cnnCount;}
			set {_cnnCount = value;}
		}

		/// <summary>
		/// 安全库存数量
		/// </summary>
		[ColumnMapping("cnnSafeCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnSafeCount
		{
			get {return _cnnSafeCount;}
			set {_cnnSafeCount = value;}
		}

		/// <summary>
		/// 安全上限库存数量
		/// </summary>
		[ColumnMapping("cnnSafeUpCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnSafeUpCount
		{
			get {return _cnnSafeUpCount;}
			set {_cnnSafeUpCount = value;}
		}

		/// <summary>
		/// 操作类型
		/// </summary>
		[ColumnMapping("cnvcOperType",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcOperType
		{
			get {return _cnvcOperType;}
			set {_cnvcOperType = value;}
		}

		/// <summary>
		/// 操作员
		/// </summary>
		[ColumnMapping("cnvcOperID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcOperID
		{
			get {return _cnvcOperID;}
			set {_cnvcOperID = value;}
		}

		/// <summary>
		/// 操作日期
		/// </summary>
		[ColumnMapping("cndOperDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndOperDate
		{
			get {return _cndOperDate;}
			set {_cndOperDate = value;}
		}
		#endregion
	}	
}
