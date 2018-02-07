
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	OrderAddLog.cs
* 作者:	     郑华
* 创建日期:    2008-10-7
* 功能描述:    订单加单流水表

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
	/// **功能名称：订单加单流水表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbOrderAddLog")]
	public class OrderAddLog: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private decimal _cnnAddSerialNo;
		private decimal _cnnOrderSerialNo;
		private string _cnvcProductCode = String.Empty;
		private string _cnvcProductName = String.Empty;
		private decimal _cnnAddCount;
		private string _cnvcUnit = String.Empty;
		private decimal _cnnPrice;
		private decimal _cnnSum;
		private string _cnvcAddType = String.Empty;
		private string _cnvcAddState = String.Empty;
		private string _cnvcAddComments = String.Empty;
		private string _cnvcOperID = String.Empty;
		private DateTime _cndOperDate;
		
		#endregion
		
		#region 构造函数




		public OrderAddLog():base()
		{
		}
		
		public OrderAddLog(DataRow row):base(row)
		{
		}
		
		public OrderAddLog(DataTable table):base(table)
		{
		}
		
		public OrderAddLog(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 加单流水
		/// </summary>
		[ColumnMapping("cnnAddSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnAddSerialNo
		{
			get {return _cnnAddSerialNo;}
			set {_cnnAddSerialNo = value;}
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
		/// 加单数量
		/// </summary>
		[ColumnMapping("cnnAddCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnAddCount
		{
			get {return _cnnAddCount;}
			set {_cnnAddCount = value;}
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
		/// 加单类型
		/// </summary>
		[ColumnMapping("cnvcAddType",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcAddType
		{
			get {return _cnvcAddType;}
			set {_cnvcAddType = value;}
		}

		/// <summary>
		/// 加单状态
		/// </summary>
		[ColumnMapping("cnvcAddState",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcAddState
		{
			get {return _cnvcAddState;}
			set {_cnvcAddState = value;}
		}

		/// <summary>
		/// 加单说明
		/// </summary>
		[ColumnMapping("cnvcAddComments",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcAddComments
		{
			get {return _cnvcAddComments;}
			set {_cnvcAddComments = value;}
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
