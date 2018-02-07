
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	SerialNo.cs
* 作者:	     郑华
* 创建日期:    2009-7-24
* 功能描述:    产品及报损流水表

*                                                           Copyright(C) 2009 zhenghua
*************************************************************************************/
#region Import NameSpace
using System;
using System.Data;
using AMSApp.zhenghua.EntityBase;
#endregion

namespace AMSApp.zhenghua.Entity
{
	/// <summary>
	/// **功能名称：产品及报损流水表实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbSerialNo")]
	public class SerialNo: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private int _cnnSerialNo;
		private string _cnvcFill = String.Empty;
		
		#endregion
		
		#region 构造函数




		public SerialNo():base()
		{
		}
		
		public SerialNo(DataRow row):base(row)
		{
		}
		
		public SerialNo(DataTable table):base(table)
		{
		}
		
		public SerialNo(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnnSerialNo",IsPrimaryKey=true,IsIdentity=true,IsVersionNumber=false)]
		public int cnnSerialNo
		{
			get {return _cnnSerialNo;}
			set {_cnnSerialNo = value;}
		}

		/// <summary>
		/// 
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
