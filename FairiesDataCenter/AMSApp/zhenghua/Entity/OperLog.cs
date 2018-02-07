
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	OperLog.cs
* 作者:	     郑华
* 创建日期:    2008-10-30
* 功能描述:    操作日志

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
	/// **功能名称：操作日志实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbOperLog")]
	public class OperLog: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private decimal _cnnOperSerialNo;
		private string _cnvcOperType = String.Empty;
		private string _cnvcOperID = String.Empty;
		private string _cnvcDeptID = String.Empty;
		private DateTime _cndOperDate;
		private string _cnvcComments = String.Empty;
		
		#endregion
		
		#region 构造函数




		public OperLog():base()
		{
		}
		
		public OperLog(DataRow row):base(row)
		{
		}
		
		public OperLog(DataTable table):base(table)
		{
		}
		
		public OperLog(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 操作流水
		/// </summary>
		[ColumnMapping("cnnOperSerialNo",IsPrimaryKey=true,IsIdentity=true,IsVersionNumber=false)]
		public decimal cnnOperSerialNo
		{
			get {return _cnnOperSerialNo;}
			set {_cnnOperSerialNo = value;}
		}

		/// <summary>
		/// 操作类型
		/// </summary>
		[ColumnMapping("cnvcOperType",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcOperType
		{
			get {return _cnvcOperType;}
			set {_cnvcOperType = value;}
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
		/// 部门
		/// </summary>
		[ColumnMapping("cnvcDeptID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcDeptID
		{
			get {return _cnvcDeptID;}
			set {_cnvcDeptID = value;}
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
		/// 描述
		/// </summary>
		[ColumnMapping("cnvcComments",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcComments
		{
			get {return _cnvcComments;}
			set {_cnvcComments = value;}
		}
		#endregion
	}	
}
