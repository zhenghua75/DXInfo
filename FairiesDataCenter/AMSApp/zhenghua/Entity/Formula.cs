
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	Formula.cs
* 作者:	     郑华
* 创建日期:    2008-9-29
* 功能描述:    配方表

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
	/// **功能名称：配方表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbFormula")]
	public class Formula: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private string _cnvcProductCode = String.Empty;
		private string _cnvcProductName = String.Empty;
		private string _cnvcProductType = String.Empty;
		private string _cnvcProductClass = String.Empty;
		private byte[] _cnbProductImage;
		private decimal _cnnMaterialCostSum;
		private decimal _cnnPackingCostSum;
		private decimal _cnnCostSum;
		private string _cnvcUnit = String.Empty;
		private decimal _cnnPortionCount;
		private string _cnvcPortionUnit = String.Empty;
		private string _cnvcFeel = String.Empty;
		private string _cnvcOrganise = String.Empty;
		private string _cnvcColor = String.Empty;
		private string _cnvcTaste = String.Empty;
		
		#endregion
		
		#region 构造函数




		public Formula():base()
		{
		}
		
		public Formula(DataRow row):base(row)
		{
		}
		
		public Formula(DataTable table):base(table)
		{
		}
		
		public Formula(string  strXML):base(strXML)
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
		/// 产品名称
		/// </summary>
		[ColumnMapping("cnvcProductName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductName
		{
			get {return _cnvcProductName;}
			set {_cnvcProductName = value;}
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
		/// 产品类别
		/// </summary>
		[ColumnMapping("cnvcProductClass",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductClass
		{
			get {return _cnvcProductClass;}
			set {_cnvcProductClass = value;}
		}

		/// <summary>
		/// 产品图片
		/// </summary>
		[ColumnMapping("cnbProductImage",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public byte[] cnbProductImage
		{
			get {return _cnbProductImage;}
			set {_cnbProductImage = value;}
		}

		/// <summary>
		/// 原料成本合计
		/// </summary>
		[ColumnMapping("cnnMaterialCostSum",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnMaterialCostSum
		{
			get {return _cnnMaterialCostSum;}
			set {_cnnMaterialCostSum = value;}
		}

		/// <summary>
		/// 材料成本合计
		/// </summary>
		[ColumnMapping("cnnPackingCostSum",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnPackingCostSum
		{
			get {return _cnnPackingCostSum;}
			set {_cnnPackingCostSum = value;}
		}

		/// <summary>
		/// 成本合计
		/// </summary>
		[ColumnMapping("cnnCostSum",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnCostSum
		{
			get {return _cnnCostSum;}
			set {_cnnCostSum = value;}
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
		/// 份产数量
		/// </summary>
		[ColumnMapping("cnnPortionCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnPortionCount
		{
			get {return _cnnPortionCount;}
			set {_cnnPortionCount = value;}
		}

		/// <summary>
		/// 份产单位
		/// </summary>
		[ColumnMapping("cnvcPortionUnit",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPortionUnit
		{
			get {return _cnvcPortionUnit;}
			set {_cnvcPortionUnit = value;}
		}

		/// <summary>
		/// 口感
		/// </summary>
		[ColumnMapping("cnvcFeel",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcFeel
		{
			get {return _cnvcFeel;}
			set {_cnvcFeel = value;}
		}

		/// <summary>
		/// 组织
		/// </summary>
		[ColumnMapping("cnvcOrganise",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcOrganise
		{
			get {return _cnvcOrganise;}
			set {_cnvcOrganise = value;}
		}

		/// <summary>
		/// 颜色
		/// </summary>
		[ColumnMapping("cnvcColor",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcColor
		{
			get {return _cnvcColor;}
			set {_cnvcColor = value;}
		}

		/// <summary>
		/// 口味
		/// </summary>
		[ColumnMapping("cnvcTaste",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcTaste
		{
			get {return _cnvcTaste;}
			set {_cnvcTaste = value;}
		}
		#endregion
	}	
}
