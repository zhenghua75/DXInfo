
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	Producer.cs
* 作者:		     郑华
* 创建日期:     2010-4-16
* 功能描述:    生产员

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
	/// **功能名称：生产员实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbProducer")]
	public class Producer: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private int _cnnProducerID;
		private string _cnvcProducerName = String.Empty;
		
		#endregion
		
		#region 构造函数




		public Producer():base()
		{
		}
		
		public Producer(DataRow row):base(row)
		{
		}
		
		public Producer(DataTable table):base(table)
		{
		}
		
		public Producer(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 生产员编码
		/// </summary>
		[ColumnMapping("cnnProducerID",IsPrimaryKey=true,IsIdentity=true,IsVersionNumber=false)]
		public int cnnProducerID
		{
			get {return _cnnProducerID;}
			set {_cnnProducerID = value;}
		}

		/// <summary>
		/// 生产员姓名
		/// </summary>
		[ColumnMapping("cnvcProducerName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProducerName
		{
			get {return _cnvcProducerName;}
			set {_cnvcProducerName = value;}
		}
		#endregion
	}	
}
