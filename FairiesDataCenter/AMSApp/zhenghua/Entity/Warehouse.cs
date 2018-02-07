
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	Warehouse.cs
* 作者:		     郑华
* 创建日期:     2010-3-27
* 功能描述:    仓库档案

*                                                           Copyright(C) 2010 zhenghua
*************************************************************************************/
#region Import NameSpace
using System;
using System.Data;
using AMSApp.zhenghua.EntityBase;
#endregion

namespace AMSApp.zhenghua.Entity
{
	/// <summary>
	/// **功能名称：仓库档案实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbWarehouse")]
	public class Warehouse: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private string _cnvcWhCode = String.Empty;
		private string _cnvcWhName = String.Empty;
		private string _cnvcDepCode = String.Empty;
		private string _cnvcWhAddress = String.Empty;
		private string _cnvcWhPhone = String.Empty;
		private string _cnvcWhPerson = String.Empty;
		private string _cnvcWhValueStyle = String.Empty;
		private string _cnvcWhMemo = String.Empty;
		private bool _cnbFreeze;
		private short _cnnFrequency;
		private string _cnvcFrequency = String.Empty;
		private DateTime _cndLastDate;
		private bool _cnbShop;
		private string _cnvcWhProperty = String.Empty;
		
		#endregion
		
		#region 构造函数




		public Warehouse():base()
		{
		}
		
		public Warehouse(DataRow row):base(row)
		{
		}
		
		public Warehouse(DataTable table):base(table)
		{
		}
		
		public Warehouse(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 仓库编码
		/// </summary>
		[ColumnMapping("cnvcWhCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcWhCode
		{
			get {return _cnvcWhCode;}
			set {_cnvcWhCode = value;}
		}

		/// <summary>
		/// 仓库名称
		/// </summary>
		[ColumnMapping("cnvcWhName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcWhName
		{
			get {return _cnvcWhName;}
			set {_cnvcWhName = value;}
		}

		/// <summary>
		/// 所属部门
		/// </summary>
		[ColumnMapping("cnvcDepCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcDepCode
		{
			get {return _cnvcDepCode;}
			set {_cnvcDepCode = value;}
		}

		/// <summary>
		/// 仓库地址
		/// </summary>
		[ColumnMapping("cnvcWhAddress",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcWhAddress
		{
			get {return _cnvcWhAddress;}
			set {_cnvcWhAddress = value;}
		}

		/// <summary>
		/// 电话
		/// </summary>
		[ColumnMapping("cnvcWhPhone",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcWhPhone
		{
			get {return _cnvcWhPhone;}
			set {_cnvcWhPhone = value;}
		}

		/// <summary>
		/// 负责人
		/// </summary>
		[ColumnMapping("cnvcWhPerson",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcWhPerson
		{
			get {return _cnvcWhPerson;}
			set {_cnvcWhPerson = value;}
		}

		/// <summary>
		/// 计价方式
		/// </summary>
		[ColumnMapping("cnvcWhValueStyle",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcWhValueStyle
		{
			get {return _cnvcWhValueStyle;}
			set {_cnvcWhValueStyle = value;}
		}

		/// <summary>
		/// 备注
		/// </summary>
		[ColumnMapping("cnvcWhMemo",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcWhMemo
		{
			get {return _cnvcWhMemo;}
			set {_cnvcWhMemo = value;}
		}

		/// <summary>
		/// 是否冻结
		/// </summary>
		[ColumnMapping("cnbFreeze",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public bool cnbFreeze
		{
			get {return _cnbFreeze;}
			set {_cnbFreeze = value;}
		}

		/// <summary>
		/// 盘点周期
		/// </summary>
		[ColumnMapping("cnnFrequency",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public short cnnFrequency
		{
			get {return _cnnFrequency;}
			set {_cnnFrequency = value;}
		}

		/// <summary>
		/// 盘点周期单位
		/// </summary>
		[ColumnMapping("cnvcFrequency",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcFrequency
		{
			get {return _cnvcFrequency;}
			set {_cnvcFrequency = value;}
		}

		/// <summary>
		/// 上次盘点日期
		/// </summary>
		[ColumnMapping("cndLastDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndLastDate
		{
			get {return _cndLastDate;}
			set {_cndLastDate = value;}
		}

		/// <summary>
		/// 是否门店
		/// </summary>
		[ColumnMapping("cnbShop",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public bool cnbShop
		{
			get {return _cnbShop;}
			set {_cnbShop = value;}
		}

		/// <summary>
		/// 仓库属性
		/// </summary>
		[ColumnMapping("cnvcWhProperty",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcWhProperty
		{
			get {return _cnvcWhProperty;}
			set {_cnvcWhProperty = value;}
		}
		#endregion
	}	
}
