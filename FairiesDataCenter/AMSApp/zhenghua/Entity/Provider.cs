
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	Provider.cs
* 作者:		     郑华
* 创建日期:     2010-3-7
* 功能描述:    供应商档案

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
	/// **功能名称：供应商档案实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbProvider")]
	public class Provider: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private string _cnvcPrvdCode = String.Empty;
		private string _cnvcPrvdName = String.Empty;
		private string _cnvcPrvdAbbName = String.Empty;
		private string _cnvcPrvdClass = String.Empty;
		private string _cnvcAddress = String.Empty;
		private string _cnvcPostCode = String.Empty;
		private string _cnvcPrvdPhone = String.Empty;
		private string _cnvcPrvdFax = String.Empty;
		private string _cnvcPrvdEmail = String.Empty;
		private string _cnvcPrvdLinkName = String.Empty;
		private DateTime _cndLastDate;
		private decimal _cnnLastMoney;
		private string _cnvcPrvdCredit = String.Empty;
		private string _cnvcPrvdQualification = String.Empty;
		private string _cnvcPrvdCreater = String.Empty;
		private DateTime _cndPrvdCreateDate;
		private string _cnvcActiveFlag = String.Empty;
		
		#endregion
		
		#region 构造函数




		public Provider():base()
		{
		}
		
		public Provider(DataRow row):base(row)
		{
		}
		
		public Provider(DataTable table):base(table)
		{
		}
		
		public Provider(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 供应商编码
		/// </summary>
		[ColumnMapping("cnvcPrvdCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdCode
		{
			get {return _cnvcPrvdCode;}
			set {_cnvcPrvdCode = value;}
		}

		/// <summary>
		/// 供应商名称
		/// </summary>
		[ColumnMapping("cnvcPrvdName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdName
		{
			get {return _cnvcPrvdName;}
			set {_cnvcPrvdName = value;}
		}

		/// <summary>
		/// 供应商简称
		/// </summary>
		[ColumnMapping("cnvcPrvdAbbName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdAbbName
		{
			get {return _cnvcPrvdAbbName;}
			set {_cnvcPrvdAbbName = value;}
		}

		/// <summary>
		/// 供应商分类编码
		/// </summary>
		[ColumnMapping("cnvcPrvdClass",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdClass
		{
			get {return _cnvcPrvdClass;}
			set {_cnvcPrvdClass = value;}
		}

		/// <summary>
		/// 地址
		/// </summary>
		[ColumnMapping("cnvcAddress",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcAddress
		{
			get {return _cnvcAddress;}
			set {_cnvcAddress = value;}
		}

		/// <summary>
		/// 邮政编码
		/// </summary>
		[ColumnMapping("cnvcPostCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPostCode
		{
			get {return _cnvcPostCode;}
			set {_cnvcPostCode = value;}
		}

		/// <summary>
		/// 电话
		/// </summary>
		[ColumnMapping("cnvcPrvdPhone",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdPhone
		{
			get {return _cnvcPrvdPhone;}
			set {_cnvcPrvdPhone = value;}
		}

		/// <summary>
		/// 传真
		/// </summary>
		[ColumnMapping("cnvcPrvdFax",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdFax
		{
			get {return _cnvcPrvdFax;}
			set {_cnvcPrvdFax = value;}
		}

		/// <summary>
		/// Email地址
		/// </summary>
		[ColumnMapping("cnvcPrvdEmail",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdEmail
		{
			get {return _cnvcPrvdEmail;}
			set {_cnvcPrvdEmail = value;}
		}

		/// <summary>
		/// 联系人
		/// </summary>
		[ColumnMapping("cnvcPrvdLinkName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdLinkName
		{
			get {return _cnvcPrvdLinkName;}
			set {_cnvcPrvdLinkName = value;}
		}

		/// <summary>
		/// 最后交易日期
		/// </summary>
		[ColumnMapping("cndLastDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndLastDate
		{
			get {return _cndLastDate;}
			set {_cndLastDate = value;}
		}

		/// <summary>
		/// 最后交易金额
		/// </summary>
		[ColumnMapping("cnnLastMoney",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnLastMoney
		{
			get {return _cnnLastMoney;}
			set {_cnnLastMoney = value;}
		}

		/// <summary>
		/// 信用等级
		/// </summary>
		[ColumnMapping("cnvcPrvdCredit",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdCredit
		{
			get {return _cnvcPrvdCredit;}
			set {_cnvcPrvdCredit = value;}
		}

		/// <summary>
		/// 资质证明
		/// </summary>
		[ColumnMapping("cnvcPrvdQualification",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdQualification
		{
			get {return _cnvcPrvdQualification;}
			set {_cnvcPrvdQualification = value;}
		}

		/// <summary>
		/// 建档人
		/// </summary>
		[ColumnMapping("cnvcPrvdCreater",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdCreater
		{
			get {return _cnvcPrvdCreater;}
			set {_cnvcPrvdCreater = value;}
		}

		/// <summary>
		/// 建档时间
		/// </summary>
		[ColumnMapping("cndPrvdCreateDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndPrvdCreateDate
		{
			get {return _cndPrvdCreateDate;}
			set {_cndPrvdCreateDate = value;}
		}

		/// <summary>
		/// 有效标志
		/// </summary>
		[ColumnMapping("cnvcActiveFlag",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcActiveFlag
		{
			get {return _cnvcActiveFlag;}
			set {_cnvcActiveFlag = value;}
		}
		#endregion
	}	
}
