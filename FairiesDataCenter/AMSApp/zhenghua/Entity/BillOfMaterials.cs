
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	BillOfMaterials.cs
* 作者:		     郑华
* 创建日期:     2010-3-9
* 功能描述:    物料清单

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
	/// **功能名称：物料清单实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbBillOfMaterials")]
	public class BillOfMaterials: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private string _cnvcPartInvCode = String.Empty;
		private string _cnvcComponentInvCode = String.Empty;
		private decimal _cnnBaseQtyN;
		private decimal _cnnBaseQtyD;
		
		#endregion
		
		#region 构造函数




		public BillOfMaterials():base()
		{
		}
		
		public BillOfMaterials(DataRow row):base(row)
		{
		}
		
		public BillOfMaterials(DataTable table):base(table)
		{
		}
		
		public BillOfMaterials(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 母件编码
		/// </summary>
		[ColumnMapping("cnvcPartInvCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPartInvCode
		{
			get {return _cnvcPartInvCode;}
			set {_cnvcPartInvCode = value;}
		}

		/// <summary>
		/// 子件编码
		/// </summary>
		[ColumnMapping("cnvcComponentInvCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcComponentInvCode
		{
			get {return _cnvcComponentInvCode;}
			set {_cnvcComponentInvCode = value;}
		}

		/// <summary>
		/// 基本用量分子
		/// </summary>
		[ColumnMapping("cnnBaseQtyN",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnBaseQtyN
		{
			get {return _cnnBaseQtyN;}
			set {_cnnBaseQtyN = value;}
		}

		/// <summary>
		/// 基本用量分母
		/// </summary>
		[ColumnMapping("cnnBaseQtyD",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnBaseQtyD
		{
			get {return _cnnBaseQtyD;}
			set {_cnnBaseQtyD = value;}
		}
		#endregion
	}	
}
