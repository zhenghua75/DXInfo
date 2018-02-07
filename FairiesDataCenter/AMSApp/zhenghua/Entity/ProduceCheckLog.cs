
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	ProduceCheckLog.cs
* 作者:		     郑华
* 创建日期:     2010-4-17
* 功能描述:    生产判断

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
	/// **功能名称：生产判断实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbProduceCheckLog")]
	public class ProduceCheckLog: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private decimal _cnnProduceSerialNo;
		private string _cnvcProduceDeptID = String.Empty;
		private string _cnvcOperID = String.Empty;
		private DateTime _cndOperDate;
		private string _cnvcInvCode = String.Empty;
		private decimal _cnnOrderCount;
		private decimal _cnnProduceCount;
		private decimal _cnnCheckCount;
		private decimal _cnnAssignCount;
		private bool _cnbInWh;
		private DateTime _cndExpDate;
		private DateTime _cndMDate;
		private int _cnnTeamID;
		private int _cnnProducerID;
		
		#endregion
		
		#region 构造函数




		public ProduceCheckLog():base()
		{
		}
		
		public ProduceCheckLog(DataRow row):base(row)
		{
		}
		
		public ProduceCheckLog(DataTable table):base(table)
		{
		}
		
		public ProduceCheckLog(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
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
		/// 生产单位
		/// </summary>
		[ColumnMapping("cnvcProduceDeptID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProduceDeptID
		{
			get {return _cnvcProduceDeptID;}
			set {_cnvcProduceDeptID = value;}
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
		/// 产品编码
		/// </summary>
		[ColumnMapping("cnvcInvCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcInvCode
		{
			get {return _cnvcInvCode;}
			set {_cnvcInvCode = value;}
		}

		/// <summary>
		/// 订单量
		/// </summary>
		[ColumnMapping("cnnOrderCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnOrderCount
		{
			get {return _cnnOrderCount;}
			set {_cnnOrderCount = value;}
		}

		/// <summary>
		/// 生产量
		/// </summary>
		[ColumnMapping("cnnProduceCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnProduceCount
		{
			get {return _cnnProduceCount;}
			set {_cnnProduceCount = value;}
		}

		/// <summary>
		/// 盘点量
		/// </summary>
		[ColumnMapping("cnnCheckCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnCheckCount
		{
			get {return _cnnCheckCount;}
			set {_cnnCheckCount = value;}
		}

		/// <summary>
		/// 分货量
		/// </summary>
		[ColumnMapping("cnnAssignCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnAssignCount
		{
			get {return _cnnAssignCount;}
			set {_cnnAssignCount = value;}
		}

		/// <summary>
		/// 是否领用
		/// </summary>
		[ColumnMapping("cnbInWh",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public bool cnbInWh
		{
			get {return _cnbInWh;}
			set {_cnbInWh = value;}
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

		/// <summary>
		/// 生产日期
		/// </summary>
		[ColumnMapping("cndMDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndMDate
		{
			get {return _cndMDate;}
			set {_cndMDate = value;}
		}

		/// <summary>
		/// 生产员编码
		/// </summary>
		[ColumnMapping("cnnTeamID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public int cnnTeamID
		{
			get {return _cnnTeamID;}
			set {_cnnTeamID = value;}
		}

		/// <summary>
		/// 生产组编码
		/// </summary>
		[ColumnMapping("cnnProducerID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public int cnnProducerID
		{
			get {return _cnnProducerID;}
			set {_cnnProducerID = value;}
		}
		#endregion
	}	
}
