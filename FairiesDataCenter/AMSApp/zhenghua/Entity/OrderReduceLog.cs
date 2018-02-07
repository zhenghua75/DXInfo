
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	OrderReduceLog.cs
* 作者:	     郑华
* 创建日期:    2008-10-7
* 功能描述:    订单减单流水表

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
	/// **功能名称：订单减单流水表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbOrderReduceLog")]
	public class OrderReduceLog: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private decimal _cnnReduceSerialNo;
		private decimal _cnnOrderSerialNo;
		private string _cnvcProductCode = String.Empty;
		private string _cnvcProductName = String.Empty;
		private decimal _cnnReduceCount;
		private string _cnvcUnit = String.Empty;
		private decimal _cnnPrice;
		private decimal _cnnSum;
		private string _cnvcReduceType = String.Empty;
		private string _cnvcReduceState = String.Empty;
		private string _cnvcReduceComments = String.Empty;
		private string _cnvcOperID = String.Empty;
		private DateTime _cndOperDate;
		
		#endregion
		
		#region 构造函数




		public OrderReduceLog():base()
		{
		}
		
		public OrderReduceLog(DataRow row):base(row)
		{
		}
		
		public OrderReduceLog(DataTable table):base(table)
		{
		}
		
		public OrderReduceLog(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 减单流水
		/// </summary>
		[ColumnMapping("cnnReduceSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnReduceSerialNo
		{
			get {return _cnnReduceSerialNo;}
			set {_cnnReduceSerialNo = value;}
		}

		/// <summary>
		/// 订单流水
		/// </summary>
		[ColumnMapping("cnnOrderSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnOrderSerialNo
		{
			get {return _cnnOrderSerialNo;}
			set {_cnnOrderSerialNo = value;}
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
		/// 减单数量
		/// </summary>
		[ColumnMapping("cnnReduceCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnReduceCount
		{
			get {return _cnnReduceCount;}
			set {_cnnReduceCount = value;}
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
		/// 单价
		/// </summary>
		[ColumnMapping("cnnPrice",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnPrice
		{
			get {return _cnnPrice;}
			set {_cnnPrice = value;}
		}

		/// <summary>
		/// 合计
		/// </summary>
		[ColumnMapping("cnnSum",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnSum
		{
			get {return _cnnSum;}
			set {_cnnSum = value;}
		}

		/// <summary>
		/// 减单类型
		/// </summary>
		[ColumnMapping("cnvcReduceType",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcReduceType
		{
			get {return _cnvcReduceType;}
			set {_cnvcReduceType = value;}
		}

		/// <summary>
		/// 减单状态
		/// </summary>
		[ColumnMapping("cnvcReduceState",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcReduceState
		{
			get {return _cnvcReduceState;}
			set {_cnvcReduceState = value;}
		}

		/// <summary>
		/// 减单说明
		/// </summary>
		[ColumnMapping("cnvcReduceComments",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcReduceComments
		{
			get {return _cnvcReduceComments;}
			set {_cnvcReduceComments = value;}
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
		/// 操作时间
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
