
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	Material.cs
* 作者:		     郑华
* 创建日期:     2010-4-4
* 功能描述:    配方表

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
	/// **功能名称：配方表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbMaterial")]
	public class Material: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private string _cnvcMaterialCode = String.Empty;
		private string _cnvcMaterialName = String.Empty;
		private string _cnvcLeastUnit = String.Empty;
		private decimal _cnnConversion;
		private string _cnvcUnit = String.Empty;
		private string _cnvcStandardUnit = String.Empty;
		private decimal _cnnStatdardCount;
		private decimal _cnnPrice;
		private string _cnvcProductType = String.Empty;
		private string _cnvcProductClass = String.Empty;
		
		#endregion
		
		#region 构造函数




		public Material():base()
		{
		}
		
		public Material(DataRow row):base(row)
		{
		}
		
		public Material(DataTable table):base(table)
		{
		}
		
		public Material(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 原料编码
		/// </summary>
		[ColumnMapping("cnvcMaterialCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcMaterialCode
		{
			get {return _cnvcMaterialCode;}
			set {_cnvcMaterialCode = value;}
		}

		/// <summary>
		/// 原料名称
		/// </summary>
		[ColumnMapping("cnvcMaterialName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcMaterialName
		{
			get {return _cnvcMaterialName;}
			set {_cnvcMaterialName = value;}
		}

		/// <summary>
		/// 最小计量单位
		/// </summary>
		[ColumnMapping("cnvcLeastUnit",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcLeastUnit
		{
			get {return _cnvcLeastUnit;}
			set {_cnvcLeastUnit = value;}
		}

		/// <summary>
		/// 换算关系
		/// </summary>
		[ColumnMapping("cnnConversion",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnConversion
		{
			get {return _cnnConversion;}
			set {_cnnConversion = value;}
		}

		/// <summary>
		/// 单位
		/// </summary>
		[ColumnMapping("cnvcUnit",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcUnit
		{
			get {return _cnvcUnit;}
			set {_cnvcUnit = value;}
		}

		/// <summary>
		/// 规格单位
		/// </summary>
		[ColumnMapping("cnvcStandardUnit",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcStandardUnit
		{
			get {return _cnvcStandardUnit;}
			set {_cnvcStandardUnit = value;}
		}

		/// <summary>
		/// 规格数量
		/// </summary>
		[ColumnMapping("cnnStatdardCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnStatdardCount
		{
			get {return _cnnStatdardCount;}
			set {_cnnStatdardCount = value;}
		}

		/// <summary>
		/// 最小计量单位价格
		/// </summary>
		[ColumnMapping("cnnPrice",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnPrice
		{
			get {return _cnnPrice;}
			set {_cnnPrice = value;}
		}

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
		/// 
		/// </summary>
		[ColumnMapping("cnvcProductClass",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductClass
		{
			get {return _cnvcProductClass;}
			set {_cnvcProductClass = value;}
		}
		#endregion
	}	
}
