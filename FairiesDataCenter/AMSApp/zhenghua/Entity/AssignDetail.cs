
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	AssignDetail.cs
* 作者:		     郑华
* 创建日期:     2010-3-18
* 功能描述:    分货流水细节表

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
	/// **功能名称：分货流水细节表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbAssignDetail")]
	public class AssignDetail: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private decimal _cnnAssignSerialNo;
		private decimal _cnnOrderSerialNo;
		private decimal _cnnProduceSerialNo;
		private string _cnvcInvCode = String.Empty;
		private decimal _cnnOrderCount;
		private decimal _cnnAssignCount;
		private DateTime _cndMdate;
		private DateTime _cndExpDate;
		
		#endregion
		
		#region 构造函数




		public AssignDetail():base()
		{
		}
		
		public AssignDetail(DataRow row):base(row)
		{
		}
		
		public AssignDetail(DataTable table):base(table)
		{
		}
		
		public AssignDetail(string  strXML):base(strXML)
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
		/// 订单流水
		/// </summary>
		[ColumnMapping("cnnOrderSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnOrderSerialNo
		{
			get {return _cnnOrderSerialNo;}
			set {_cnnOrderSerialNo = value;}
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
		/// 产品编码
		/// </summary>
		[ColumnMapping("cnvcInvCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcInvCode
		{
			get {return _cnvcInvCode;}
			set {_cnvcInvCode = value;}
		}

		/// <summary>
		/// 叫货数量
		/// </summary>
		[ColumnMapping("cnnOrderCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnOrderCount
		{
			get {return _cnnOrderCount;}
			set {_cnnOrderCount = value;}
		}

		/// <summary>
		/// 实发数量
		/// </summary>
		[ColumnMapping("cnnAssignCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnAssignCount
		{
			get {return _cnnAssignCount;}
			set {_cnnAssignCount = value;}
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
