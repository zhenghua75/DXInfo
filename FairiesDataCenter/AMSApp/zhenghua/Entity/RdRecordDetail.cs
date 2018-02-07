
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	RdRecordDetail.cs
* 作者:		     郑华
* 创建日期:     2010-4-4
* 功能描述:    收发记录子表

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
	/// **功能名称：收发记录子表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbRdRecordDetail")]
	public class RdRecordDetail: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private decimal _cnnAutoID;
		private decimal _cnnRdID;
		private string _cnvcPOID = String.Empty;
		private decimal _cnnMPoID;
		private string _cnvcProviderID = String.Empty;
		private string _cnvcInvCode = String.Empty;
		private decimal _cnnQuantity;
		private decimal _cnnPrice;
		private decimal _cnnCost;
		private decimal _cnnExtraCost;
		private string _cnvcGroupCode = String.Empty;
		private string _cnvcComunitCode = String.Empty;
		private string _cnvcBatch = String.Empty;
		private string _cnvcFlag = String.Empty;
		private string _cnvcCommens = String.Empty;
		private DateTime _cndMdate;
		private int _cnnMassDate;
		private string _cnvcMassUnit = String.Empty;
		private DateTime _cndExpDate;
		
		#endregion
		
		#region 构造函数




		public RdRecordDetail():base()
		{
		}
		
		public RdRecordDetail(DataRow row):base(row)
		{
		}
		
		public RdRecordDetail(DataTable table):base(table)
		{
		}
		
		public RdRecordDetail(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 收发记录子表标识
		/// </summary>
		[ColumnMapping("cnnAutoID",IsPrimaryKey=true,IsIdentity=true,IsVersionNumber=false)]
		public decimal cnnAutoID
		{
			get {return _cnnAutoID;}
			set {_cnnAutoID = value;}
		}

		/// <summary>
		/// 收发记录主表标识
		/// </summary>
		[ColumnMapping("cnnRdID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnRdID
		{
			get {return _cnnRdID;}
			set {_cnnRdID = value;}
		}

		/// <summary>
		/// 对应订单主表单号
		/// </summary>
		[ColumnMapping("cnvcPOID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPOID
		{
			get {return _cnvcPOID;}
			set {_cnvcPOID = value;}
		}

		/// <summary>
		/// 对应订单子表标识
		/// </summary>
		[ColumnMapping("cnnMPoID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnMPoID
		{
			get {return _cnnMPoID;}
			set {_cnnMPoID = value;}
		}

		/// <summary>
		/// 供应商编码
		/// </summary>
		[ColumnMapping("cnvcProviderID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProviderID
		{
			get {return _cnvcProviderID;}
			set {_cnvcProviderID = value;}
		}

		/// <summary>
		/// 存货编码
		/// </summary>
		[ColumnMapping("cnvcInvCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcInvCode
		{
			get {return _cnvcInvCode;}
			set {_cnvcInvCode = value;}
		}

		/// <summary>
		/// 数量
		/// </summary>
		[ColumnMapping("cnnQuantity",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnQuantity
		{
			get {return _cnnQuantity;}
			set {_cnnQuantity = value;}
		}

		/// <summary>
		/// 单价
		/// </summary>
		[ColumnMapping("cnnPrice",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnPrice
		{
			get {return _cnnPrice;}
			set {_cnnPrice = value;}
		}

		/// <summary>
		/// 金额
		/// </summary>
		[ColumnMapping("cnnCost",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnCost
		{
			get {return _cnnCost;}
			set {_cnnCost = value;}
		}

		/// <summary>
		/// 其它金额
		/// </summary>
		[ColumnMapping("cnnExtraCost",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnExtraCost
		{
			get {return _cnnExtraCost;}
			set {_cnnExtraCost = value;}
		}

		/// <summary>
		/// 计量单位组编码
		/// </summary>
		[ColumnMapping("cnvcGroupCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcGroupCode
		{
			get {return _cnvcGroupCode;}
			set {_cnvcGroupCode = value;}
		}

		/// <summary>
		/// 计量单位编码
		/// </summary>
		[ColumnMapping("cnvcComunitCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcComunitCode
		{
			get {return _cnvcComunitCode;}
			set {_cnvcComunitCode = value;}
		}

		/// <summary>
		/// 批号
		/// </summary>
		[ColumnMapping("cnvcBatch",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcBatch
		{
			get {return _cnvcBatch;}
			set {_cnvcBatch = value;}
		}

		/// <summary>
		/// 标志
		/// </summary>
		[ColumnMapping("cnvcFlag",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcFlag
		{
			get {return _cnvcFlag;}
			set {_cnvcFlag = value;}
		}

		/// <summary>
		/// 备注
		/// </summary>
		[ColumnMapping("cnvcCommens",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCommens
		{
			get {return _cnvcCommens;}
			set {_cnvcCommens = value;}
		}

		/// <summary>
		/// 生产日期
		/// </summary>
		[ColumnMapping("cndMdate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndMdate
		{
			get {return _cndMdate;}
			set {_cndMdate = value;}
		}

		/// <summary>
		/// 保质期天数
		/// </summary>
		[ColumnMapping("cnnMassDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public int cnnMassDate
		{
			get {return _cnnMassDate;}
			set {_cnnMassDate = value;}
		}

		/// <summary>
		/// 保质期单位
		/// </summary>
		[ColumnMapping("cnvcMassUnit",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcMassUnit
		{
			get {return _cnvcMassUnit;}
			set {_cnvcMassUnit = value;}
		}

		/// <summary>
		/// 过期日期
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
