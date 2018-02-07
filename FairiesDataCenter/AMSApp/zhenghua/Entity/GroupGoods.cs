
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	GroupGoods.cs
* 作者:	     郑华
* 创建日期:    2008-10-23
* 功能描述:    生产组产品对应表

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
	/// **功能名称：生产组产品对应表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbGroupGoods")]
	public class GroupGoods: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private string _cnvcProductType = String.Empty;
		private string _cnvcProductClass = String.Empty;
		private string _cnvcGroupCode = String.Empty;
		private string _cnvcComments = String.Empty;
		
		#endregion
		
		#region 构造函数




		public GroupGoods():base()
		{
		}
		
		public GroupGoods(DataRow row):base(row)
		{
		}
		
		public GroupGoods(DataTable table):base(table)
		{
		}
		
		public GroupGoods(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 产品类型
		/// </summary>
		[ColumnMapping("cnvcProductType",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductType
		{
			get {return _cnvcProductType;}
			set {_cnvcProductType = value;}
		}

		/// <summary>
		/// 产品类别
		/// </summary>
		[ColumnMapping("cnvcProductClass",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductClass
		{
			get {return _cnvcProductClass;}
			set {_cnvcProductClass = value;}
		}

		/// <summary>
		/// 生产组编码
		/// </summary>
		[ColumnMapping("cnvcGroupCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcGroupCode
		{
			get {return _cnvcGroupCode;}
			set {_cnvcGroupCode = value;}
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
