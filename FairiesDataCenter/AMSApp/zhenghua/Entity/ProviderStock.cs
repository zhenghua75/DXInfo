
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	ProviderStock.cs
* 作者:		     郑华
* 创建日期:     2010-3-7
* 功能描述:    供应商存货表

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
	/// **功能名称：供应商存货表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbProviderStock")]
	public class ProviderStock: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private string _cnvcPrvdCode = String.Empty;
		private string _cnvcGoodsCode = String.Empty;
		private string _cnvcGoodsName = String.Empty;
		private decimal _cnnGoodsPrice;
		private string _cnvcInvalidFlag = String.Empty;
		private DateTime _cndInsertDate;
		
		#endregion
		
		#region 构造函数




		public ProviderStock():base()
		{
		}
		
		public ProviderStock(DataRow row):base(row)
		{
		}
		
		public ProviderStock(DataTable table):base(table)
		{
		}
		
		public ProviderStock(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 供应商编码
		/// </summary>
		[ColumnMapping("cnvcPrvdCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdCode
		{
			get {return _cnvcPrvdCode;}
			set {_cnvcPrvdCode = value;}
		}

		/// <summary>
		/// 供应货品编码
		/// </summary>
		[ColumnMapping("cnvcGoodsCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcGoodsCode
		{
			get {return _cnvcGoodsCode;}
			set {_cnvcGoodsCode = value;}
		}

		/// <summary>
		/// 供应货品名称
		/// </summary>
		[ColumnMapping("cnvcGoodsName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcGoodsName
		{
			get {return _cnvcGoodsName;}
			set {_cnvcGoodsName = value;}
		}

		/// <summary>
		/// 供货单价
		/// </summary>
		[ColumnMapping("cnnGoodsPrice",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnGoodsPrice
		{
			get {return _cnnGoodsPrice;}
			set {_cnnGoodsPrice = value;}
		}

		/// <summary>
		/// 有效标志
		/// </summary>
		[ColumnMapping("cnvcInvalidFlag",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcInvalidFlag
		{
			get {return _cnvcInvalidFlag;}
			set {_cnvcInvalidFlag = value;}
		}

		/// <summary>
		/// 插入时间
		/// </summary>
		[ColumnMapping("cndInsertDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndInsertDate
		{
			get {return _cndInsertDate;}
			set {_cndInsertDate = value;}
		}
		#endregion
	}	
}
