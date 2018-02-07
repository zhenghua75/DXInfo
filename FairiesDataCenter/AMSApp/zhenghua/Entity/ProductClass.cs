
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	ProductClass.cs
* 作者:		     郑华
* 创建日期:     2010-4-25
* 功能描述:    产品类别

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
	/// **功能名称：产品类别实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbProductClass")]
	public class ProductClass: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private string _cnvcProductType = String.Empty;
		private string _cnvcProductClassName = String.Empty;
		private string _cnvcProductClassCode = String.Empty;
		private string _cnvcComments = String.Empty;
		private int _cnnDays;
		
		#endregion
		
		#region 构造函数




		public ProductClass():base()
		{
		}
		
		public ProductClass(DataRow row):base(row)
		{
		}
		
		public ProductClass(DataTable table):base(table)
		{
		}
		
		public ProductClass(string  strXML):base(strXML)
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
		/// 产品类别名称
		/// </summary>
		[ColumnMapping("cnvcProductClassName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductClassName
		{
			get {return _cnvcProductClassName;}
			set {_cnvcProductClassName = value;}
		}

		/// <summary>
		/// 产品类别编码
		/// </summary>
		[ColumnMapping("cnvcProductClassCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductClassCode
		{
			get {return _cnvcProductClassCode;}
			set {_cnvcProductClassCode = value;}
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
		[ColumnMapping("cnnDays",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public int cnnDays
		{
			get {return _cnnDays;}
			set {_cnnDays = value;}
		}
		#endregion
	}	
}
