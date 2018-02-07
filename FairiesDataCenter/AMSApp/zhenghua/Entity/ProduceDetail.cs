
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	ProduceDetail.cs
* 作者:		     郑华
* 创建日期:     2010-3-19
* 功能描述:    生产计划产品细节表

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
	/// **功能名称：生产计划产品细节表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbProduceDetail")]
	public class ProduceDetail: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private decimal _cnnProduceSerialNo;
		private string _cnvcInvCode = String.Empty;
		private decimal _cnnProduceCount;
		private decimal _cnnOrderCount;
		
		#endregion
		
		#region 构造函数




		public ProduceDetail():base()
		{
		}
		
		public ProduceDetail(DataRow row):base(row)
		{
		}
		
		public ProduceDetail(DataTable table):base(table)
		{
		}
		
		public ProduceDetail(string  strXML):base(strXML)
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
		/// 产品编码
		/// </summary>
		[ColumnMapping("cnvcInvCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcInvCode
		{
			get {return _cnvcInvCode;}
			set {_cnvcInvCode = value;}
		}

		/// <summary>
		/// 计划生产数量
		/// </summary>
		[ColumnMapping("cnnProduceCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnProduceCount
		{
			get {return _cnnProduceCount;}
			set {_cnnProduceCount = value;}
		}

		/// <summary>
		/// 订单数量
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
