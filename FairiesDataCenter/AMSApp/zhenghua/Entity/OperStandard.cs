
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	OperStandard.cs
* 作者:	     郑华
* 创建日期:    2008-10-3
* 功能描述:    操作标准表

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
	/// **功能名称：操作标准表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbOperStandard")]
	public class OperStandard: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private string _cnvcProductCode = String.Empty;
		private int _cnnSort;
		private string _cnvcStandard = String.Empty;
		private string _cnvcKey = String.Empty;
		
		#endregion
		
		#region 构造函数




		public OperStandard():base()
		{
		}
		
		public OperStandard(DataRow row):base(row)
		{
		}
		
		public OperStandard(DataTable table):base(table)
		{
		}
		
		public OperStandard(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 产品编码
		/// </summary>
		[ColumnMapping("cnvcProductCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductCode
		{
			get {return _cnvcProductCode;}
			set {_cnvcProductCode = value;}
		}

		/// <summary>
		/// 操作序号
		/// </summary>
		[ColumnMapping("cnnSort",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public int cnnSort
		{
			get {return _cnnSort;}
			set {_cnnSort = value;}
		}

		/// <summary>
		/// 操作标准
		/// </summary>
		[ColumnMapping("cnvcStandard",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcStandard
		{
			get {return _cnvcStandard;}
			set {_cnvcStandard = value;}
		}

		/// <summary>
		/// 关键控制点
		/// </summary>
		[ColumnMapping("cnvcKey",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcKey
		{
			get {return _cnvcKey;}
			set {_cnvcKey = value;}
		}
		#endregion
	}	
}
