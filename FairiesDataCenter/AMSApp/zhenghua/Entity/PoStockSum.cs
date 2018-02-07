
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	PoStockSum.cs
* 作者:		     郑华
* 创建日期:     2010-3-7
* 功能描述:    采购订单合计表

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
	/// **功能名称：采购订单合计表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbPoStockSum")]
	public class PoStockSum: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private string _cnvcPoID = String.Empty;
		private string _cnvcGoodsCode = String.Empty;
		private string _cnvcGroupCode = String.Empty;
		private string _cnvcStockUnit = String.Empty;
		private decimal _cnnStockPrice;
		private decimal _cnnStockCountSum;
		private decimal _cnnStockFeeSum;
		private decimal _cnnArriveCount;
		private decimal _cnnArriveFee;
		private decimal _cnnFinallyRate;
		
		#endregion
		
		#region 构造函数




		public PoStockSum():base()
		{
		}
		
		public PoStockSum(DataRow row):base(row)
		{
		}
		
		public PoStockSum(DataTable table):base(table)
		{
		}
		
		public PoStockSum(string  strXML):base(strXML)
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
		/// 累计订单数量
		/// </summary>
		[ColumnMapping("cnnStockCountSum",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnStockCountSum
		{
			get {return _cnnStockCountSum;}
			set {_cnnStockCountSum = value;}
		}

		/// <summary>
		/// 累计订单金额
		/// </summary>
		[ColumnMapping("cnnStockFeeSum",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnStockFeeSum
		{
			get {return _cnnStockFeeSum;}
			set {_cnnStockFeeSum = value;}
		}

		/// <summary>
		/// 累计到货数量
		/// </summary>
		[ColumnMapping("cnnArriveCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnArriveCount
		{
			get {return _cnnArriveCount;}
			set {_cnnArriveCount = value;}
		}

		/// <summary>
		/// 累计到货金额
		/// </summary>
		[ColumnMapping("cnnArriveFee",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnArriveFee
		{
			get {return _cnnArriveFee;}
			set {_cnnArriveFee = value;}
		}

		/// <summary>
		/// 完成比例
		/// </summary>
		[ColumnMapping("cnnFinallyRate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnFinallyRate
		{
			get {return _cnnFinallyRate;}
			set {_cnnFinallyRate = value;}
		}
		#endregion
	}	
}
