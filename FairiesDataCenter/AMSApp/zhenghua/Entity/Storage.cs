
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	Storage.cs
* 作者:	     郑华
* 创建日期:    2008-11-3
* 功能描述:    库存表

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
	/// **功能名称：库存表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbStorage")]
	public class Storage: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private string _cnvcStorageDeptID = String.Empty;
		private string _cnvcProductCode = String.Empty;
		private string _cnvcProductName = String.Empty;
		private string _cnvcUnit = String.Empty;
		private decimal _cnnCount;
		private decimal _cnnSafeCount;
		private decimal _cnnSafeUpCount;
		
		#endregion
		
		#region 构造函数




		public Storage():base()
		{
		}
		
		public Storage(DataRow row):base(row)
		{
		}
		
		public Storage(DataTable table):base(table)
		{
		}
		
		public Storage(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
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
		/// 
		/// </summary>
		[ColumnMapping("cnnSafeUpCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnSafeUpCount
		{
			get {return _cnnSafeUpCount;}
			set {_cnnSafeUpCount = value;}
		}
		#endregion
	}	
}
