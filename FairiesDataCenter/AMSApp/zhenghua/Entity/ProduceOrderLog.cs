
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	ProduceOrderLog.cs
* 作者:	     郑华
* 创建日期:    2008-10-10
* 功能描述:    生产流水订单流水对应表

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
	/// **功能名称：生产流水订单流水对应表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbProduceOrderLog")]
	public class ProduceOrderLog: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private decimal _cnnProduceSerialNo;
		private decimal _cnnOrderSerialNo;
		private string _cnvcType = String.Empty;
		
		#endregion
		
		#region 构造函数




		public ProduceOrderLog():base()
		{
		}
		
		public ProduceOrderLog(DataRow row):base(row)
		{
		}
		
		public ProduceOrderLog(DataTable table):base(table)
		{
		}
		
		public ProduceOrderLog(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 生产流水
		/// </summary>
		[ColumnMapping("cnnProduceSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnProduceSerialNo
		{
			get {return _cnnProduceSerialNo;}
			set {_cnnProduceSerialNo = value;}
		}

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
		/// 下单类型
		/// </summary>
		[ColumnMapping("cnvcType",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcType
		{
			get {return _cnvcType;}
			set {_cnvcType = value;}
		}
		#endregion
	}	
}
