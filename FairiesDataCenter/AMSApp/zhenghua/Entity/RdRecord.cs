
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	RdRecord.cs
* 作者:		     郑华
* 创建日期:     2010-3-7
* 功能描述:    收发记录主表

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
	/// **功能名称：收发记录主表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbRdRecord")]
	public class RdRecord: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private decimal _cnnRdID;
		private string _cnvcCode = String.Empty;
		private string _cnvcRdCode = String.Empty;
		private string _cnvcRdFlag = String.Empty;
		private string _cnvcIsLsQuery = String.Empty;
		private string _cnvcWhCode = String.Empty;
		private string _cnvcDepID = String.Empty;
		private string _cnvcWhpersonName = String.Empty;
		private string _cnvcOperName = String.Empty;
		private string _cnvcCusCode = String.Empty;
//		private string _cnvcProBatch = String.Empty;
		private string _cnvcComments = String.Empty;
		private string _cnvcMaker = String.Empty;
		private DateTime _cndMakeDate;
		private string _cnvcModer = String.Empty;
		private DateTime _cndModDate;
		private string _cnvcHandler = String.Empty;
		private DateTime _cndHandDate;
//		private string _cnvcMPoCode = String.Empty;
		private decimal _cnnProorderID;
		private string _cnvcShipAddress = String.Empty;
		private DateTime _cndARVDate;
		private string _cnvcARVAddress = String.Empty;
		private string _cnvcState = String.Empty;
		
		#endregion
		
		#region 构造函数




		public RdRecord():base()
		{
		}
		
		public RdRecord(DataRow row):base(row)
		{
		}
		
		public RdRecord(DataTable table):base(table)
		{
		}
		
		public RdRecord(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 收发记录主表标识
		/// </summary>
		[ColumnMapping("cnnRdID",IsPrimaryKey=true,IsIdentity=true,IsVersionNumber=false)]
		public decimal cnnRdID
		{
			get {return _cnnRdID;}
			set {_cnnRdID = value;}
		}

		/// <summary>
		/// 收发单据号
		/// </summary>
		[ColumnMapping("cnvcCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCode
		{
			get {return _cnvcCode;}
			set {_cnvcCode = value;}
		}

		/// <summary>
		/// 收发类别编号
		/// </summary>
		[ColumnMapping("cnvcRdCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcRdCode
		{
			get {return _cnvcRdCode;}
			set {_cnvcRdCode = value;}
		}

		/// <summary>
		/// 收发标志
		/// </summary>
		[ColumnMapping("cnvcRdFlag",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcRdFlag
		{
			get {return _cnvcRdFlag;}
			set {_cnvcRdFlag = value;}
		}

		/// <summary>
		/// 是否下发零售
		/// </summary>
		[ColumnMapping("cnvcIsLsQuery",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcIsLsQuery
		{
			get {return _cnvcIsLsQuery;}
			set {_cnvcIsLsQuery = value;}
		}

		/// <summary>
		/// 仓库编码
		/// </summary>
		[ColumnMapping("cnvcWhCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcWhCode
		{
			get {return _cnvcWhCode;}
			set {_cnvcWhCode = value;}
		}

		/// <summary>
		/// 部门编码
		/// </summary>
		[ColumnMapping("cnvcDepID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcDepID
		{
			get {return _cnvcDepID;}
			set {_cnvcDepID = value;}
		}

		/// <summary>
		/// 库管员名称
		/// </summary>
		[ColumnMapping("cnvcWhpersonName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcWhpersonName
		{
			get {return _cnvcWhpersonName;}
			set {_cnvcWhpersonName = value;}
		}

		/// <summary>
		/// 业务员
		/// </summary>
		[ColumnMapping("cnvcOperName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcOperName
		{
			get {return _cnvcOperName;}
			set {_cnvcOperName = value;}
		}

		/// <summary>
		/// 客户编码
		/// </summary>
		[ColumnMapping("cnvcCusCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCusCode
		{
			get {return _cnvcCusCode;}
			set {_cnvcCusCode = value;}
		}

		/// <summary>
		/// 生产批号
		/// </summary>
//		[ColumnMapping("cnvcProBatch",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
//		public string cnvcProBatch
//		{
//			get {return _cnvcProBatch;}
//			set {_cnvcProBatch = value;}
//		}

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
		/// 制单人
		/// </summary>
		[ColumnMapping("cnvcMaker",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcMaker
		{
			get {return _cnvcMaker;}
			set {_cnvcMaker = value;}
		}

		/// <summary>
		/// 制单时间
		/// </summary>
		[ColumnMapping("cndMakeDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndMakeDate
		{
			get {return _cndMakeDate;}
			set {_cndMakeDate = value;}
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
		/// 修改时间
		/// </summary>
		[ColumnMapping("cndModDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndModDate
		{
			get {return _cndModDate;}
			set {_cndModDate = value;}
		}

		/// <summary>
		/// 审核人
		/// </summary>
		[ColumnMapping("cnvcHandler",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcHandler
		{
			get {return _cnvcHandler;}
			set {_cnvcHandler = value;}
		}

		/// <summary>
		/// 审核时间
		/// </summary>
		[ColumnMapping("cndHandDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndHandDate
		{
			get {return _cndHandDate;}
			set {_cndHandDate = value;}
		}

		/// <summary>
		/// 生产订单编号
		/// </summary>
//		[ColumnMapping("cnvcMPoCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
//		public string cnvcMPoCode
//		{
//			get {return _cnvcMPoCode;}
//			set {_cnvcMPoCode = value;}
//		}

		/// <summary>
		/// 生产订单主表标识
		/// </summary>
		[ColumnMapping("cnnProorderID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnProorderID
		{
			get {return _cnnProorderID;}
			set {_cnnProorderID = value;}
		}

		/// <summary>
		/// 发货地址
		/// </summary>
		[ColumnMapping("cnvcShipAddress",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcShipAddress
		{
			get {return _cnvcShipAddress;}
			set {_cnvcShipAddress = value;}
		}

		/// <summary>
		/// 到货日期
		/// </summary>
		[ColumnMapping("cndARVDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndARVDate
		{
			get {return _cndARVDate;}
			set {_cndARVDate = value;}
		}

		/// <summary>
		/// 到货地址
		/// </summary>
		[ColumnMapping("cnvcARVAddress",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcARVAddress
		{
			get {return _cnvcARVAddress;}
			set {_cnvcARVAddress = value;}
		}

		/// <summary>
		/// 单据状态
		/// </summary>
		[ColumnMapping("cnvcState",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcState
		{
			get {return _cnvcState;}
			set {_cnvcState = value;}
		}
		#endregion
	}	
}
