
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	LostSerial.cs
* 作者:		     郑华
* 创建日期:     2010-3-27
* 功能描述:    报损流水表

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
	/// **功能名称：报损流水表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbLostSerial")]
	public class LostSerial: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private int _cnnLostSerialNo;
		private int _cnnProduceSerialNo;
		private string _cnvcInvCode = String.Empty;
		private decimal _cnnLostCount;
		private decimal _cnnAddCount;
		private decimal _cnnReduceCount;
		private DateTime _cndLostDate;
		private string _cnvcDeptID = String.Empty;
		private string _cnvcOperID = String.Empty;
		private DateTime _cndOperDate;
		private string _cnvcLostType = String.Empty;
		private string _cnvcComments = String.Empty;
		private string _cnvcWhCode = String.Empty;
		private string _cnvcInvalidFlag = String.Empty;
		private string _cnvcComunitCode = String.Empty;
		private DateTime _cndMdate;
		private DateTime _cndExpDate;
		
		#endregion
		
		#region 构造函数




		public LostSerial():base()
		{
		}
		
		public LostSerial(DataRow row):base(row)
		{
		}
		
		public LostSerial(DataTable table):base(table)
		{
		}
		
		public LostSerial(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 报损流水
		/// </summary>
		[ColumnMapping("cnnLostSerialNo",IsPrimaryKey=true,IsIdentity=true,IsVersionNumber=false)]
		public int cnnLostSerialNo
		{
			get {return _cnnLostSerialNo;}
			set {_cnnLostSerialNo = value;}
		}

		/// <summary>
		/// 生产流水
		/// </summary>
		[ColumnMapping("cnnProduceSerialNo",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public int cnnProduceSerialNo
		{
			get {return _cnnProduceSerialNo;}
			set {_cnnProduceSerialNo = value;}
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
		/// 报损数量
		/// </summary>
		[ColumnMapping("cnnLostCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnLostCount
		{
			get {return _cnnLostCount;}
			set {_cnnLostCount = value;}
		}

		/// <summary>
		/// 调增量
		/// </summary>
		[ColumnMapping("cnnAddCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnAddCount
		{
			get {return _cnnAddCount;}
			set {_cnnAddCount = value;}
		}

		/// <summary>
		/// 调减量
		/// </summary>
		[ColumnMapping("cnnReduceCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnReduceCount
		{
			get {return _cnnReduceCount;}
			set {_cnnReduceCount = value;}
		}

		/// <summary>
		/// 报损日期
		/// </summary>
		[ColumnMapping("cndLostDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndLostDate
		{
			get {return _cndLostDate;}
			set {_cndLostDate = value;}
		}

		/// <summary>
		/// 部门
		/// </summary>
		[ColumnMapping("cnvcDeptID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcDeptID
		{
			get {return _cnvcDeptID;}
			set {_cnvcDeptID = value;}
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
		/// 报损类型
		/// </summary>
		[ColumnMapping("cnvcLostType",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcLostType
		{
			get {return _cnvcLostType;}
			set {_cnvcLostType = value;}
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
		/// 仓库编码
		/// </summary>
		[ColumnMapping("cnvcWhCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcWhCode
		{
			get {return _cnvcWhCode;}
			set {_cnvcWhCode = value;}
		}

		/// <summary>
		/// 生产标志
		/// </summary>
		[ColumnMapping("cnvcInvalidFlag",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcInvalidFlag
		{
			get {return _cnvcInvalidFlag;}
			set {_cnvcInvalidFlag = value;}
		}

		/// <summary>
		/// 单位
		/// </summary>
		[ColumnMapping("cnvcComunitCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcComunitCode
		{
			get {return _cnvcComunitCode;}
			set {_cnvcComunitCode = value;}
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
