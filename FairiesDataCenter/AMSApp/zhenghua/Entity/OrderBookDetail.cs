
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	OrderBookDetail.cs
* 作者:		     郑华
* 创建日期:     2010-3-17
* 功能描述:    订单子表

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
	/// **功能名称：订单子表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbOrderBookDetail")]
	public class OrderBookDetail: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private decimal _cnnOrderSerialNo;
		private string _cnvcInvCode = String.Empty;
		private decimal _cnnOrderCount;
		
		#endregion
		
		#region 构造函数




		public OrderBookDetail():base()
		{
		}
		
		public OrderBookDetail(DataRow row):base(row)
		{
		}
		
		public OrderBookDetail(DataTable table):base(table)
		{
		}
		
		public OrderBookDetail(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 订单流水
		/// </summary>
		[ColumnMapping("cnnOrderSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnOrderSerialNo
		{
			get {return _cnnOrderSerialNo;}
			set {_cnnOrderSerialNo = value;}
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
		/// 订单量
		/// </summary>
		[ColumnMapping("cnnOrderCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnOrderCount
		{
			get {return _cnnOrderCount;}
			set {_cnnOrderCount = value;}
		}
		#endregion
	}	
}
