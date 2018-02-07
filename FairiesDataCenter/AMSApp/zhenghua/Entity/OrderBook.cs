
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	OrderBook.cs
* 作者:		     郑华
* 创建日期:     2010-4-11
* 功能描述:    生产订单主表

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
	/// **功能名称：生产订单主表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbOrderBook")]
	public class OrderBook: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private decimal _cnnOrderSerialNo;
		private string _cnvcOrderDeptID = String.Empty;
		private string _cnvcProduceDeptID = String.Empty;
		private string _cnvcOrderType = String.Empty;
		private string _cnvcOrderComments = String.Empty;
		private string _cnvcOperID = String.Empty;
		private DateTime _cndOperDate;
		private DateTime _cndShipDate;
		private string _cnvcCustomName = String.Empty;
		private string _cnvcShipAddress = String.Empty;
		private string _cnvcLinkPhone = String.Empty;
		private DateTime _cndArrivedDate;
		private string _cnvcOrderState = String.Empty;
		private DateTime _cndOrderDate;
		
		#endregion
		
		#region 构造函数




		public OrderBook():base()
		{
		}
		
		public OrderBook(DataRow row):base(row)
		{
		}
		
		public OrderBook(DataTable table):base(table)
		{
		}
		
		public OrderBook(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
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
		/// 订单门市
		/// </summary>
		[ColumnMapping("cnvcOrderDeptID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcOrderDeptID
		{
			get {return _cnvcOrderDeptID;}
			set {_cnvcOrderDeptID = value;}
		}

		/// <summary>
		/// 生产单位
		/// </summary>
		[ColumnMapping("cnvcProduceDeptID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProduceDeptID
		{
			get {return _cnvcProduceDeptID;}
			set {_cnvcProduceDeptID = value;}
		}

		/// <summary>
		/// 订单类型
		/// </summary>
		[ColumnMapping("cnvcOrderType",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcOrderType
		{
			get {return _cnvcOrderType;}
			set {_cnvcOrderType = value;}
		}

		/// <summary>
		/// 订单说明
		/// </summary>
		[ColumnMapping("cnvcOrderComments",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcOrderComments
		{
			get {return _cnvcOrderComments;}
			set {_cnvcOrderComments = value;}
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

		/// <summary>
		/// 发货日期
		/// </summary>
		[ColumnMapping("cndShipDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndShipDate
		{
			get {return _cndShipDate;}
			set {_cndShipDate = value;}
		}

		/// <summary>
		/// 客户姓名单位
		/// </summary>
		[ColumnMapping("cnvcCustomName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCustomName
		{
			get {return _cnvcCustomName;}
			set {_cnvcCustomName = value;}
		}

		/// <summary>
		/// 送货地址
		/// </summary>
		[ColumnMapping("cnvcShipAddress",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcShipAddress
		{
			get {return _cnvcShipAddress;}
			set {_cnvcShipAddress = value;}
		}

		/// <summary>
		/// 联系电话
		/// </summary>
		[ColumnMapping("cnvcLinkPhone",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcLinkPhone
		{
			get {return _cnvcLinkPhone;}
			set {_cnvcLinkPhone = value;}
		}

		/// <summary>
		/// 客人要求到货时间
		/// </summary>
		[ColumnMapping("cndArrivedDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndArrivedDate
		{
			get {return _cndArrivedDate;}
			set {_cndArrivedDate = value;}
		}

		/// <summary>
		/// 订单状态
		/// </summary>
		[ColumnMapping("cnvcOrderState",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcOrderState
		{
			get {return _cnvcOrderState;}
			set {_cnvcOrderState = value;}
		}

		/// <summary>
		/// 订单日期
		/// </summary>
		[ColumnMapping("cndOrderDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndOrderDate
		{
			get {return _cndOrderDate;}
			set {_cndOrderDate = value;}
		}
		#endregion
	}	
}
