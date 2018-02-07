
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	Inventory.cs
* 作者:		     郑华
* 创建日期:     2010-4-10
* 功能描述:    存货档案

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
	/// **功能名称：存货档案实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbInventory")]
	public class Inventory: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private bool _cnbProductBill;
		private string _cnvcInvCode = String.Empty;
		private string _cnvcInvName = String.Empty;
		private string _cnvcInvStd = String.Empty;
		private string _cnvcInvCCode = String.Empty;
		private bool _cnbSale;
		private bool _cnbPurchase;
		private bool _cnbSelf;
		private bool _cnbComsume;
		private decimal _cniInvCCost;
		private decimal _cniInvNCost;
		private decimal _cniSafeNum;
		private decimal _cniLowSum;
		private DateTime _cndSDate;
		private DateTime _cndEDate;
		private string _cnvcCreatePerson = String.Empty;
		private string _cnvcModifyPerson = String.Empty;
		private DateTime _cndModifyDate;
		private string _cnvcValueType = String.Empty;
		private string _cnvcGroupCode = String.Empty;
		private string _cnvcComUnitCode = String.Empty;
		private string _cnvcSAComUnitCode = String.Empty;
		private string _cnvcPUComUnitCode = String.Empty;
		private string _cnvcSTComUnitCode = String.Empty;
		private string _cnvcProduceUnitCode = String.Empty;
		private decimal _cnfRetailPrice;
		private string _cnvcShopUnitCode = String.Empty;
		private string _cnvcFeel = String.Empty;
		private string _cnvcOrganise = String.Empty;
		private string _cnvcColor = String.Empty;
		private string _cnvcTaste = String.Empty;
		private int _cnnExpire;
		private int _cnnDue;
		
		#endregion
		
		#region 构造函数




		public Inventory():base()
		{
		}
		
		public Inventory(DataRow row):base(row)
		{
		}
		
		public Inventory(DataTable table):base(table)
		{
		}
		
		public Inventory(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 允许生产订单
		/// </summary>
		[ColumnMapping("cnbProductBill",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public bool cnbProductBill
		{
			get {return _cnbProductBill;}
			set {_cnbProductBill = value;}
		}

		/// <summary>
		/// 存货编码
		/// </summary>
		[ColumnMapping("cnvcInvCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcInvCode
		{
			get {return _cnvcInvCode;}
			set {_cnvcInvCode = value;}
		}

		/// <summary>
		/// 存货名称
		/// </summary>
		[ColumnMapping("cnvcInvName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcInvName
		{
			get {return _cnvcInvName;}
			set {_cnvcInvName = value;}
		}

		/// <summary>
		/// 规格型号
		/// </summary>
		[ColumnMapping("cnvcInvStd",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcInvStd
		{
			get {return _cnvcInvStd;}
			set {_cnvcInvStd = value;}
		}

		/// <summary>
		/// 存货大类编码
		/// </summary>
		[ColumnMapping("cnvcInvCCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcInvCCode
		{
			get {return _cnvcInvCCode;}
			set {_cnvcInvCCode = value;}
		}

		/// <summary>
		/// 是否销售
		/// </summary>
		[ColumnMapping("cnbSale",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public bool cnbSale
		{
			get {return _cnbSale;}
			set {_cnbSale = value;}
		}

		/// <summary>
		/// 是否外购
		/// </summary>
		[ColumnMapping("cnbPurchase",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public bool cnbPurchase
		{
			get {return _cnbPurchase;}
			set {_cnbPurchase = value;}
		}

		/// <summary>
		/// 是否自制
		/// </summary>
		[ColumnMapping("cnbSelf",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public bool cnbSelf
		{
			get {return _cnbSelf;}
			set {_cnbSelf = value;}
		}

		/// <summary>
		/// 是否生产耗用
		/// </summary>
		[ColumnMapping("cnbComsume",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public bool cnbComsume
		{
			get {return _cnbComsume;}
			set {_cnbComsume = value;}
		}

		/// <summary>
		/// 参考成本
		/// </summary>
		[ColumnMapping("cniInvCCost",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cniInvCCost
		{
			get {return _cniInvCCost;}
			set {_cniInvCCost = value;}
		}

		/// <summary>
		/// 最新成本
		/// </summary>
		[ColumnMapping("cniInvNCost",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cniInvNCost
		{
			get {return _cniInvNCost;}
			set {_cniInvNCost = value;}
		}

		/// <summary>
		/// 安全库存量
		/// </summary>
		[ColumnMapping("cniSafeNum",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cniSafeNum
		{
			get {return _cniSafeNum;}
			set {_cniSafeNum = value;}
		}

		/// <summary>
		/// 最低库存
		/// </summary>
		[ColumnMapping("cniLowSum",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cniLowSum
		{
			get {return _cniLowSum;}
			set {_cniLowSum = value;}
		}

		/// <summary>
		/// 启用日期
		/// </summary>
		[ColumnMapping("cndSDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndSDate
		{
			get {return _cndSDate;}
			set {_cndSDate = value;}
		}

		/// <summary>
		/// 停用日期
		/// </summary>
		[ColumnMapping("cndEDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndEDate
		{
			get {return _cndEDate;}
			set {_cndEDate = value;}
		}

		/// <summary>
		/// 建档人
		/// </summary>
		[ColumnMapping("cnvcCreatePerson",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCreatePerson
		{
			get {return _cnvcCreatePerson;}
			set {_cnvcCreatePerson = value;}
		}

		/// <summary>
		/// 变更人
		/// </summary>
		[ColumnMapping("cnvcModifyPerson",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcModifyPerson
		{
			get {return _cnvcModifyPerson;}
			set {_cnvcModifyPerson = value;}
		}

		/// <summary>
		/// 变更日期
		/// </summary>
		[ColumnMapping("cndModifyDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndModifyDate
		{
			get {return _cndModifyDate;}
			set {_cndModifyDate = value;}
		}

		/// <summary>
		/// 计价方式
		/// </summary>
		[ColumnMapping("cnvcValueType",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcValueType
		{
			get {return _cnvcValueType;}
			set {_cnvcValueType = value;}
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
		/// 主计量单位编码
		/// </summary>
		[ColumnMapping("cnvcComUnitCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcComUnitCode
		{
			get {return _cnvcComUnitCode;}
			set {_cnvcComUnitCode = value;}
		}

		/// <summary>
		/// 销售默认计量单位
		/// </summary>
		[ColumnMapping("cnvcSAComUnitCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcSAComUnitCode
		{
			get {return _cnvcSAComUnitCode;}
			set {_cnvcSAComUnitCode = value;}
		}

		/// <summary>
		/// 采购默认计量单位
		/// </summary>
		[ColumnMapping("cnvcPUComUnitCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPUComUnitCode
		{
			get {return _cnvcPUComUnitCode;}
			set {_cnvcPUComUnitCode = value;}
		}

		/// <summary>
		/// 库存默认计量单位
		/// </summary>
		[ColumnMapping("cnvcSTComUnitCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcSTComUnitCode
		{
			get {return _cnvcSTComUnitCode;}
			set {_cnvcSTComUnitCode = value;}
		}

		/// <summary>
		/// 生产计量单位
		/// </summary>
		[ColumnMapping("cnvcProduceUnitCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProduceUnitCode
		{
			get {return _cnvcProduceUnitCode;}
			set {_cnvcProduceUnitCode = value;}
		}

		/// <summary>
		/// 零售价格
		/// </summary>
		[ColumnMapping("cnfRetailPrice",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnfRetailPrice
		{
			get {return _cnfRetailPrice;}
			set {_cnfRetailPrice = value;}
		}

		/// <summary>
		/// 零售计量单位
		/// </summary>
		[ColumnMapping("cnvcShopUnitCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcShopUnitCode
		{
			get {return _cnvcShopUnitCode;}
			set {_cnvcShopUnitCode = value;}
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

		/// <summary>
		/// 过期限制默认为0
		/// </summary>
		[ColumnMapping("cnnExpire",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public int cnnExpire
		{
			get {return _cnnExpire;}
			set {_cnnExpire = value;}
		}

		/// <summary>
		/// 到期提示默认为5
		/// </summary>
		[ColumnMapping("cnnDue",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public int cnnDue
		{
			get {return _cnnDue;}
			set {_cnnDue = value;}
		}
		#endregion
	}	
}
