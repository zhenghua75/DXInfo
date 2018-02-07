
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	ProduceLog.cs
* 作者:		     郑华
* 创建日期:     2010-4-11
* 功能描述:    生产计划主表

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
	/// **功能名称：生产计划主表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbProduceLog")]
	public class ProduceLog: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private decimal _cnnProduceSerialNo;
		private string _cnvcProduceDeptID = String.Empty;
		private DateTime _cndProduceDate;
		private DateTime _cndShipBeginDate;
		private DateTime _cndShipEndDate;
		private string _cnvcProduceState = String.Empty;
		private string _cnvcOperID = String.Empty;
		private DateTime _cndOperDate;
		private bool _cnbSelf;
		
		#endregion
		
		#region 构造函数




		public ProduceLog():base()
		{
		}
		
		public ProduceLog(DataRow row):base(row)
		{
		}
		
		public ProduceLog(DataTable table):base(table)
		{
		}
		
		public ProduceLog(string  strXML):base(strXML)
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
		/// 生产单位
		/// </summary>
		[ColumnMapping("cnvcProduceDeptID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProduceDeptID
		{
			get {return _cnvcProduceDeptID;}
			set {_cnvcProduceDeptID = value;}
		}

		/// <summary>
		/// 生产日期
		/// </summary>
		[ColumnMapping("cndProduceDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndProduceDate
		{
			get {return _cndProduceDate;}
			set {_cndProduceDate = value;}
		}

		/// <summary>
		/// 发货开始日期
		/// </summary>
		[ColumnMapping("cndShipBeginDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndShipBeginDate
		{
			get {return _cndShipBeginDate;}
			set {_cndShipBeginDate = value;}
		}

		/// <summary>
		/// 发货结束日期
		/// </summary>
		[ColumnMapping("cndShipEndDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndShipEndDate
		{
			get {return _cndShipEndDate;}
			set {_cndShipEndDate = value;}
		}

		/// <summary>
		/// 生产计划状态
		/// </summary>
		[ColumnMapping("cnvcProduceState",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProduceState
		{
			get {return _cnvcProduceState;}
			set {_cnvcProduceState = value;}
		}

		/// <summary>
		/// 操作员
		/// </summary>
		[ColumnMapping("cnvcOperID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcOperID
		{
			get {return _cnvcOperID;}
			set {_cnvcOperID = value;}
		}

		/// <summary>
		/// 操作时间
		/// </summary>
		[ColumnMapping("cndOperDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndOperDate
		{
			get {return _cndOperDate;}
			set {_cndOperDate = value;}
		}

		/// <summary>
		/// 是否自生产
		/// </summary>
		[ColumnMapping("cnbSelf",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public bool cnbSelf
		{
			get {return _cnbSelf;}
			set {_cnbSelf = value;}
		}
		#endregion
	}	
}
