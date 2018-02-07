
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	MakeDetail.cs
* 作者:		     郑华
* 创建日期:     2010-4-4
* 功能描述:    制令单生产产品细节表

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
	/// **功能名称：制令单生产产品细节表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbMakeDetail")]
	public class MakeDetail: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private decimal _cnnMakeSerialNo;
		private string _cnvcInvCode = String.Empty;
		private decimal _cnnMakeCount;
		private decimal _cnnCount;
		private decimal _cnnAdjustCount;
		private decimal _cnnStCount;
		private bool _cnbCollar;
		private decimal _cnnCollarCount;
		
		#endregion
		
		#region 构造函数




		public MakeDetail():base()
		{
		}
		
		public MakeDetail(DataRow row):base(row)
		{
		}
		
		public MakeDetail(DataTable table):base(table)
		{
		}
		
		public MakeDetail(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 制令流水
		/// </summary>
		[ColumnMapping("cnnMakeSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnMakeSerialNo
		{
			get {return _cnnMakeSerialNo;}
			set {_cnnMakeSerialNo = value;}
		}

		/// <summary>
		/// 产品编码
		/// </summary>
		[ColumnMapping("cnvcInvCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcInvCode
		{
			get {return _cnvcInvCode;}
			set {_cnvcInvCode = value;}
		}

		/// <summary>
		/// 制令数量
		/// </summary>
		[ColumnMapping("cnnMakeCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnMakeCount
		{
			get {return _cnnMakeCount;}
			set {_cnnMakeCount = value;}
		}

		/// <summary>
		/// 实际数量
		/// </summary>
		[ColumnMapping("cnnCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnCount
		{
			get {return _cnnCount;}
			set {_cnnCount = value;}
		}

		/// <summary>
		/// 调整数量
		/// </summary>
		[ColumnMapping("cnnAdjustCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnAdjustCount
		{
			get {return _cnnAdjustCount;}
			set {_cnnAdjustCount = value;}
		}

		/// <summary>
		/// 库存领用量
		/// </summary>
		[ColumnMapping("cnnStCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnStCount
		{
			get {return _cnnStCount;}
			set {_cnnStCount = value;}
		}

		/// <summary>
		/// 是否领用
		/// </summary>
		[ColumnMapping("cnbCollar",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public bool cnbCollar
		{
			get {return _cnbCollar;}
			set {_cnbCollar = value;}
		}

		/// <summary>
		/// 生产领用量
		/// </summary>
		[ColumnMapping("cnnCollarCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnCollarCount
		{
			get {return _cnnCollarCount;}
			set {_cnnCollarCount = value;}
		}
		#endregion
	}	
}
