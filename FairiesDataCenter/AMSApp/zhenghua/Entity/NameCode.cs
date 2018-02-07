
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	NameCode.cs
* 作者:	     郑华
* 创建日期:    2008-10-10
* 功能描述:    代码表

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
	/// **功能名称：代码表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbNameCode")]
	public class NameCode: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private string _cnvcType = String.Empty;
		private string _cnvcCode = String.Empty;
		private string _cnvcName = String.Empty;
		private string _cnvcComments = String.Empty;
		
		#endregion
		
		#region 构造函数




		public NameCode():base()
		{
		}
		
		public NameCode(DataRow row):base(row)
		{
		}
		
		public NameCode(DataTable table):base(table)
		{
		}
		
		public NameCode(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 类型
		/// </summary>
		[ColumnMapping("cnvcType",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcType
		{
			get {return _cnvcType;}
			set {_cnvcType = value;}
		}

		/// <summary>
		/// 代码
		/// </summary>
		[ColumnMapping("cnvcCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCode
		{
			get {return _cnvcCode;}
			set {_cnvcCode = value;}
		}

		/// <summary>
		/// 名称
		/// </summary>
		[ColumnMapping("cnvcName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcName
		{
			get {return _cnvcName;}
			set {_cnvcName = value;}
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
