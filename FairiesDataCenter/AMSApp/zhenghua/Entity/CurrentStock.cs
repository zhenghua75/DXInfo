
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	CurrentStock.cs
* 作者:		     郑华
* 创建日期:     2010-3-7
* 功能描述:    现存量汇总表

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
	/// **功能名称：现存量汇总表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbCurrentStock")]
	public class CurrentStock: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private decimal _cnnAutoID;
		private string _cnvcWhCode = String.Empty;
		private string _cnvcInvCode = String.Empty;
		private decimal _cnnQuantity;
		private decimal _cnnOutQuantity;
		private decimal _cnnInQuantity;
		private string _cnvcStopFlag = String.Empty;
		private decimal _cnnTransInQuantity;
		private DateTime _cndMdate;
		private decimal _cnnTransOutQuantity;
		private decimal _cnnPlanQuantity;
		private decimal _cnnDisableQuantity;
		private decimal _cnnAvaQuantity;
//		private byte[] _ufts;
		private int _cnnMassDate;
		private string _cnvcMassUnit = String.Empty;
		private decimal _cnnStopQuantity;
		private decimal _cnnStopNum;
		private DateTime _cndExpDate;
		
		#endregion
		
		#region 构造函数




		public CurrentStock():base()
		{
		}
		
		public CurrentStock(DataRow row):base(row)
		{
		}
		
		public CurrentStock(DataTable table):base(table)
		{
		}
		
		public CurrentStock(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 自动编号
		/// </summary>
		[ColumnMapping("cnnAutoID",IsPrimaryKey=true,IsIdentity=true,IsVersionNumber=false)]
		public decimal cnnAutoID
		{
			get {return _cnnAutoID;}
			set {_cnnAutoID = value;}
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
		/// 存货编码
		/// </summary>
		[ColumnMapping("cnvcInvCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcInvCode
		{
			get {return _cnvcInvCode;}
			set {_cnvcInvCode = value;}
		}

		/// <summary>
		/// 结存数量
		/// </summary>
		[ColumnMapping("cnnQuantity",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnQuantity
		{
			get {return _cnnQuantity;}
			set {_cnnQuantity = value;}
		}

		/// <summary>
		/// 待发货数量
		/// </summary>
		[ColumnMapping("cnnOutQuantity",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnOutQuantity
		{
			get {return _cnnOutQuantity;}
			set {_cnnOutQuantity = value;}
		}

		/// <summary>
		/// 待入库数量
		/// </summary>
		[ColumnMapping("cnnInQuantity",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnInQuantity
		{
			get {return _cnnInQuantity;}
			set {_cnnInQuantity = value;}
		}

		/// <summary>
		/// 库存是??冻结
		/// </summary>
		[ColumnMapping("cnvcStopFlag",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcStopFlag
		{
			get {return _cnvcStopFlag;}
			set {_cnvcStopFlag = value;}
		}

		/// <summary>
		/// 调拨在途数量
		/// </summary>
		[ColumnMapping("cnnTransInQuantity",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnTransInQuantity
		{
			get {return _cnnTransInQuantity;}
			set {_cnnTransInQuantity = value;}
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
		/// 调拨待发数量
		/// </summary>
		[ColumnMapping("cnnTransOutQuantity",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnTransOutQuantity
		{
			get {return _cnnTransOutQuantity;}
			set {_cnnTransOutQuantity = value;}
		}

		/// <summary>
		/// 计划备料数量
		/// </summary>
		[ColumnMapping("cnnPlanQuantity",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnPlanQuantity
		{
			get {return _cnnPlanQuantity;}
			set {_cnnPlanQuantity = value;}
		}

		/// <summary>
		/// 不合格数量
		/// </summary>
		[ColumnMapping("cnnDisableQuantity",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnDisableQuantity
		{
			get {return _cnnDisableQuantity;}
			set {_cnnDisableQuantity = value;}
		}

		/// <summary>
		/// 可用数量
		/// </summary>
		[ColumnMapping("cnnAvaQuantity",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnAvaQuantity
		{
			get {return _cnnAvaQuantity;}
			set {_cnnAvaQuantity = value;}
		}
//
//		/// <summary>
//		/// 时间戳
//		/// </summary>
//		[ColumnMapping("ufts",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
//		public byte[] ufts
//		{
//			get {return _ufts;}
//			set {_ufts = value;}
//		}

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
		/// 冻结数量
		/// </summary>
		[ColumnMapping("cnnStopQuantity",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnStopQuantity
		{
			get {return _cnnStopQuantity;}
			set {_cnnStopQuantity = value;}
		}

		/// <summary>
		/// 冻结件数
		/// </summary>
		[ColumnMapping("cnnStopNum",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnStopNum
		{
			get {return _cnnStopNum;}
			set {_cnnStopNum = value;}
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
