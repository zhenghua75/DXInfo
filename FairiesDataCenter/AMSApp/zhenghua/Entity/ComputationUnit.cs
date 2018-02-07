
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	ComputationUnit.cs
* 作者:		     郑华
* 创建日期:    2010-3-6
* 功能描述:    计量单位

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
	/// **功能名称：计量单位实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbComputationUnit")]
	public class ComputationUnit: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private string _cnvcComunitCode = String.Empty;
		private string _cnvcComUnitName = String.Empty;
		private string _cnvcGroupCode = String.Empty;
		private bool _cnbMainUnit;
		private float _cniChangRate;
		
		#endregion
		
		#region 构造函数




		public ComputationUnit():base()
		{
		}
		
		public ComputationUnit(DataRow row):base(row)
		{
		}
		
		public ComputationUnit(DataTable table):base(table)
		{
		}
		
		public ComputationUnit(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 计量单位编码
		/// </summary>
		[ColumnMapping("cnvcComunitCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcComunitCode
		{
			get {return _cnvcComunitCode;}
			set {_cnvcComunitCode = value;}
		}

		/// <summary>
		/// 计量单位名称
		/// </summary>
		[ColumnMapping("cnvcComUnitName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcComUnitName
		{
			get {return _cnvcComUnitName;}
			set {_cnvcComUnitName = value;}
		}

		/// <summary>
		/// 计量单位组编码
		/// </summary>
		[ColumnMapping("cnvcGroupCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcGroupCode
		{
			get {return _cnvcGroupCode;}
			set {_cnvcGroupCode = value;}
		}

		/// <summary>
		/// 是否主计量单位
		/// </summary>
		[ColumnMapping("cnbMainUnit",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public bool cnbMainUnit
		{
			get {return _cnbMainUnit;}
			set {_cnbMainUnit = value;}
		}

		/// <summary>
		/// 换算率
		/// </summary>
		[ColumnMapping("cniChangRate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public float cniChangRate
		{
			get {return _cniChangRate;}
			set {_cniChangRate = value;}
		}
		#endregion
	}	
}
