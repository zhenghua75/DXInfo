
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	PoStockMain.cs
* 作者:		     郑华
* 创建日期:     2010-3-7
* 功能描述:    采购订单主表

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
	/// **功能名称：采购订单主表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbPoStockMain")]
	public class PoStockMain: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private string _cnvcPoID = String.Empty;
		private string _cnvcPrvdCode = String.Empty;
		private string _cnvcAddress = String.Empty;
		private string _cnvcComments = String.Empty;
		private string _cnvcPoState = String.Empty;
		private string _cnvcPlanCycle = String.Empty;
		private string _cnvcCreater = String.Empty;
		private string _cnvcModer = String.Empty;
		private string _cnvcChecker = String.Empty;
		private string _cnvcCloser = String.Empty;
		private DateTime _cndCreateDate;
		private DateTime _cndModDate;
		private DateTime _cndCheckDate;
		private DateTime _cndCloseDate;
		
		#endregion
		
		#region 构造函数




		public PoStockMain():base()
		{
		}
		
		public PoStockMain(DataRow row):base(row)
		{
		}
		
		public PoStockMain(DataTable table):base(table)
		{
		}
		
		public PoStockMain(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 采购订单号
		/// </summary>
		[ColumnMapping("cnvcPoID",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPoID
		{
			get {return _cnvcPoID;}
			set {_cnvcPoID = value;}
		}

		/// <summary>
		/// 供应商编码
		/// </summary>
		[ColumnMapping("cnvcPrvdCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdCode
		{
			get {return _cnvcPrvdCode;}
			set {_cnvcPrvdCode = value;}
		}

		/// <summary>
		/// 到货地址
		/// </summary>
		[ColumnMapping("cnvcAddress",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcAddress
		{
			get {return _cnvcAddress;}
			set {_cnvcAddress = value;}
		}

		/// <summary>
		/// 备注
		/// </summary>
		[ColumnMapping("cnvcComments",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcComments
		{
			get {return _cnvcComments;}
			set {_cnvcComments = value;}
		}

		/// <summary>
		/// 订单状态
		/// </summary>
		[ColumnMapping("cnvcPoState",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPoState
		{
			get {return _cnvcPoState;}
			set {_cnvcPoState = value;}
		}

		/// <summary>
		/// 物料需求计划周期
		/// </summary>
		[ColumnMapping("cnvcPlanCycle",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPlanCycle
		{
			get {return _cnvcPlanCycle;}
			set {_cnvcPlanCycle = value;}
		}

		/// <summary>
		/// 制单人
		/// </summary>
		[ColumnMapping("cnvcCreater",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCreater
		{
			get {return _cnvcCreater;}
			set {_cnvcCreater = value;}
		}

		/// <summary>
		/// 修改人
		/// </summary>
		[ColumnMapping("cnvcModer",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcModer
		{
			get {return _cnvcModer;}
			set {_cnvcModer = value;}
		}

		/// <summary>
		/// 审核人
		/// </summary>
		[ColumnMapping("cnvcChecker",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcChecker
		{
			get {return _cnvcChecker;}
			set {_cnvcChecker = value;}
		}

		/// <summary>
		/// 关闭人
		/// </summary>
		[ColumnMapping("cnvcCloser",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCloser
		{
			get {return _cnvcCloser;}
			set {_cnvcCloser = value;}
		}

		/// <summary>
		/// 制单时间
		/// </summary>
		[ColumnMapping("cndCreateDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndCreateDate
		{
			get {return _cndCreateDate;}
			set {_cndCreateDate = value;}
		}

		/// <summary>
		/// 修改时间
		/// </summary>
		[ColumnMapping("cndModDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndModDate
		{
			get {return _cndModDate;}
			set {_cndModDate = value;}
		}

		/// <summary>
		/// 审核时间
		/// </summary>
		[ColumnMapping("cndCheckDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndCheckDate
		{
			get {return _cndCheckDate;}
			set {_cndCheckDate = value;}
		}

		/// <summary>
		/// 关闭时间
		/// </summary>
		[ColumnMapping("cndCloseDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndCloseDate
		{
			get {return _cndCloseDate;}
			set {_cndCloseDate = value;}
		}
		#endregion
	}	
}
