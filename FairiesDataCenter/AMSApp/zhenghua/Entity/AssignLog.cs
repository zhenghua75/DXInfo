
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	AssignLog.cs
* 作者:	     郑华
* 创建日期:    2008-10-13
* 功能描述:    分货流水表

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
	/// **功能名称：分货流水表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbAssignLog")]
	public class AssignLog: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private decimal _cnnAssignSerialNo;
		private decimal _cnnProduceSerialNo;
		private decimal _cnnOrderSerialNo;
		private string _cnvcShipDeptID = String.Empty;
		private string _cnvcShipOperID = String.Empty;
		private string _cnvcReceiveDeptID = String.Empty;
		private string _cnvcReceiveOperID = String.Empty;
		private DateTime _cndShipDate;
		private DateTime _cndReceiveDate;
		private string _cnvcSalesroomOperID = String.Empty;
		private string _cnvcTransportOperID = String.Empty;
		private string _cnvcStorageOperID = String.Empty;
		private string _cnvcCustomerValidate = String.Empty;
		private string _cnvcCustomerIdea = String.Empty;
		private string _cnvcOperID = String.Empty;
		private DateTime _cndOperDate;
		private string _cnvcComments = String.Empty;
		private int    _cnnPrintFlag;
		
		#endregion
		
		#region 构造函数




		public AssignLog():base()
		{
		}
		
		public AssignLog(DataRow row):base(row)
		{
		}
		
		public AssignLog(DataTable table):base(table)
		{
		}
		
		public AssignLog(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 分货流水
		/// </summary>
		[ColumnMapping("cnnAssignSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnAssignSerialNo
		{
			get {return _cnnAssignSerialNo;}
			set {_cnnAssignSerialNo = value;}
		}
		/// <summary>
		/// 生产流水
		/// </summary>
		[ColumnMapping("cnnProduceSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnProduceSerialNo
		{
			get {return _cnnProduceSerialNo;}
			set {_cnnProduceSerialNo = value;}
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
		/// 发货单位
		/// </summary>
		[ColumnMapping("cnvcShipDeptID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcShipDeptID
		{
			get {return _cnvcShipDeptID;}
			set {_cnvcShipDeptID = value;}
		}

		/// <summary>
		/// 发货人
		/// </summary>
		[ColumnMapping("cnvcShipOperID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcShipOperID
		{
			get {return _cnvcShipOperID;}
			set {_cnvcShipOperID = value;}
		}

		/// <summary>
		/// 收货单位
		/// </summary>
		[ColumnMapping("cnvcReceiveDeptID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcReceiveDeptID
		{
			get {return _cnvcReceiveDeptID;}
			set {_cnvcReceiveDeptID = value;}
		}

		/// <summary>
		/// 收货人
		/// </summary>
		[ColumnMapping("cnvcReceiveOperID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcReceiveOperID
		{
			get {return _cnvcReceiveOperID;}
			set {_cnvcReceiveOperID = value;}
		}

		/// <summary>
		/// 发货时间
		/// </summary>
		[ColumnMapping("cndShipDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndShipDate
		{
			get {return _cndShipDate;}
			set {_cndShipDate = value;}
		}

		/// <summary>
		/// 收货时间
		/// </summary>
		[ColumnMapping("cndReceiveDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndReceiveDate
		{
			get {return _cndReceiveDate;}
			set {_cndReceiveDate = value;}
		}

		/// <summary>
		/// 店务
		/// </summary>
		[ColumnMapping("cnvcSalesroomOperID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcSalesroomOperID
		{
			get {return _cnvcSalesroomOperID;}
			set {_cnvcSalesroomOperID = value;}
		}

		/// <summary>
		/// 运输
		/// </summary>
		[ColumnMapping("cnvcTransportOperID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcTransportOperID
		{
			get {return _cnvcTransportOperID;}
			set {_cnvcTransportOperID = value;}
		}

		/// <summary>
		/// 成品仓
		/// </summary>
		[ColumnMapping("cnvcStorageOperID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcStorageOperID
		{
			get {return _cnvcStorageOperID;}
			set {_cnvcStorageOperID = value;}
		}

		/// <summary>
		/// 客户签收
		/// </summary>
		[ColumnMapping("cnvcCustomerValidate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCustomerValidate
		{
			get {return _cnvcCustomerValidate;}
			set {_cnvcCustomerValidate = value;}
		}

		/// <summary>
		/// 客户意见反馈
		/// </summary>
		[ColumnMapping("cnvcCustomerIdea",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCustomerIdea
		{
			get {return _cnvcCustomerIdea;}
			set {_cnvcCustomerIdea = value;}
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
		/// 打印标志
		/// </summary>
		[ColumnMapping("cnnPrintFlag",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public int cnnPrintFlag
		{
			get {return _cnnPrintFlag;}
			set {_cnnPrintFlag = value;}
		}
		#endregion
	}	
}
