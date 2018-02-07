
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	PoStockDetail.cs
* 作者:		     郑华
* 创建日期:     2010-3-7
* 功能描述:    采购订单子表

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
	/// **功能名称：采购订单子表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbPoStockDetail")]
	public class PoStockDetail: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private string _cnvcPoID = String.Empty;
		private string _cnvcDeptID = String.Empty;
		private string _cnvcGoodsCode = String.Empty;
		private string _cnvcGroupCode = String.Empty;
		private string _cnvcStockUnit = String.Empty;
		private decimal _cnnStockPrice;
		private decimal _cnnStockCount;
		private decimal _cnnStockFee;
		private DateTime _cndArriveDate;
		private string _cnvcRowState = String.Empty;
		private string _cnvcCreater = String.Empty;
		private string _cnvcModer = String.Empty;
		private string _cnvcChecker = String.Empty;
		private DateTime _cndCreateDate;
		private DateTime _cndModDate;
		private DateTime _cndCheckDate;
		
		#endregion
		
		#region 构造函数




		public PoStockDetail():base()
		{
		}
		
		public PoStockDetail(DataRow row):base(row)
		{
		}
		
		public PoStockDetail(DataTable table):base(table)
		{
		}
		
		public PoStockDetail(string  strXML):base(strXML)
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
		/// 下订部门
		/// </summary>
		[ColumnMapping("cnvcDeptID",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcDeptID
		{
			get {return _cnvcDeptID;}
			set {_cnvcDeptID = value;}
		}

		/// <summary>
		/// 供应货品编码
		/// </summary>
		[ColumnMapping("cnvcGoodsCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcGoodsCode
		{
			get {return _cnvcGoodsCode;}
			set {_cnvcGoodsCode = value;}
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
		/// 单位
		/// </summary>
		[ColumnMapping("cnvcStockUnit",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcStockUnit
		{
			get {return _cnvcStockUnit;}
			set {_cnvcStockUnit = value;}
		}

		/// <summary>
		/// 单价
		/// </summary>
		[ColumnMapping("cnnStockPrice",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnStockPrice
		{
			get {return _cnnStockPrice;}
			set {_cnnStockPrice = value;}
		}

		/// <summary>
		/// 订单数量
		/// </summary>
		[ColumnMapping("cnnStockCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnStockCount
		{
			get {return _cnnStockCount;}
			set {_cnnStockCount = value;}
		}

		/// <summary>
		/// 订单金额
		/// </summary>
		[ColumnMapping("cnnStockFee",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnStockFee
		{
			get {return _cnnStockFee;}
			set {_cnnStockFee = value;}
		}

		/// <summary>
		/// 计划到货日期
		/// </summary>
		[ColumnMapping("cndArriveDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndArriveDate
		{
			get {return _cndArriveDate;}
			set {_cndArriveDate = value;}
		}

		/// <summary>
		/// 子订单行状态
		/// </summary>
		[ColumnMapping("cnvcRowState",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcRowState
		{
			get {return _cnvcRowState;}
			set {_cnvcRowState = value;}
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
		#endregion
	}	
}
