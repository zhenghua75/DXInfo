
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	GroupMake.cs
* 作者:	     郑华
* 创建日期:    2008-10-23
* 功能描述:    生产组制令对应表

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
	/// **功能名称：生产组制令对应表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbGroupMake")]
	public class GroupMake: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private string _cnvcProductType = String.Empty;
		private string _cnvcGroupCode = String.Empty;
		private string _cnvcMakeName = String.Empty;
		
		#endregion
		
		#region 构造函数




		public GroupMake():base()
		{
		}
		
		public GroupMake(DataRow row):base(row)
		{
		}
		
		public GroupMake(DataTable table):base(table)
		{
		}
		
		public GroupMake(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 产品类型
		/// </summary>
		[ColumnMapping("cnvcProductType",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductType
		{
			get {return _cnvcProductType;}
			set {_cnvcProductType = value;}
		}

		/// <summary>
		/// 生产组
		/// </summary>
		[ColumnMapping("cnvcGroupCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcGroupCode
		{
			get {return _cnvcGroupCode;}
			set {_cnvcGroupCode = value;}
		}

		/// <summary>
		/// 制令名称
		/// </summary>
		[ColumnMapping("cnvcMakeName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcMakeName
		{
			get {return _cnvcMakeName;}
			set {_cnvcMakeName = value;}
		}
		#endregion
	}	
}
