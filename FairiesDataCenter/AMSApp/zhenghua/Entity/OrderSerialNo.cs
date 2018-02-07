
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	OrderSerialNo.cs
* 作者:	     郑华
* 创建日期:    2008-10-4
* 功能描述:    订单流水生成表

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
	/// **功能名称：订单流水生成表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbOrderSerialNo")]
	public class OrderSerialNo: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private decimal _cnnSerialNo;
		private string _cnvcFill = String.Empty;
		
		#endregion
		
		#region 构造函数




		public OrderSerialNo():base()
		{
		}
		
		public OrderSerialNo(DataRow row):base(row)
		{
		}
		
		public OrderSerialNo(DataTable table):base(table)
		{
		}
		
		public OrderSerialNo(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 订单流水
		/// </summary>
		[ColumnMapping("cnnSerialNo",IsPrimaryKey=true,IsIdentity=true,IsVersionNumber=false)]
		public decimal cnnSerialNo
		{
			get {return _cnnSerialNo;}
			set {_cnnSerialNo = value;}
		}

		/// <summary>
		/// 填充
		/// </summary>
		[ColumnMapping("cnvcFill",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcFill
		{
			get {return _cnvcFill;}
			set {_cnvcFill = value;}
		}
		#endregion
	}	
}
