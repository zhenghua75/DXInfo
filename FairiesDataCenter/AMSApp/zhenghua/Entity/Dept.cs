
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	Dept.cs
* 作者:	     郑华
* 创建日期:    2008-10-30
* 功能描述:    部门表

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
	/// **功能名称：部门表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbDept")]
	public class Dept: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private string _cnvcDeptName = String.Empty;
		private string _cnvcDeptID = String.Empty;
		private string _cnvcDeptType = String.Empty;
		private string _cnvcParentDeptID = String.Empty;
		private string _cnvcComments = String.Empty;
		private int _cnnPriority;
		
		#endregion
		
		#region 构造函数




		public Dept():base()
		{
		}
		
		public Dept(DataRow row):base(row)
		{
		}
		
		public Dept(DataTable table):base(table)
		{
		}
		
		public Dept(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 部门名称
		/// </summary>
		[ColumnMapping("cnvcDeptName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcDeptName
		{
			get {return _cnvcDeptName;}
			set {_cnvcDeptName = value;}
		}

		/// <summary>
		/// 部门编码
		/// </summary>
		[ColumnMapping("cnvcDeptID",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcDeptID
		{
			get {return _cnvcDeptID;}
			set {_cnvcDeptID = value;}
		}

		/// <summary>
		/// 部门类型
		/// </summary>
		[ColumnMapping("cnvcDeptType",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcDeptType
		{
			get {return _cnvcDeptType;}
			set {_cnvcDeptType = value;}
		}

		/// <summary>
		/// 上级部门编码
		/// </summary>
		[ColumnMapping("cnvcParentDeptID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcParentDeptID
		{
			get {return _cnvcParentDeptID;}
			set {_cnvcParentDeptID = value;}
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

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnnPriority",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public int cnnPriority
		{
			get {return _cnnPriority;}
			set {_cnnPriority = value;}
		}
		#endregion
	}	
}
