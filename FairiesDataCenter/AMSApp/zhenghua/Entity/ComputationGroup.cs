
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:   	ComputationGroup.cs
* 作者:		     郑华
* 创建日期:    2010-3-6
* 功能描述:    计量单位组

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
	/// **功能名称：计量单位组实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbComputationGroup")]
	public class ComputationGroup: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private string _cnvcGroupCode = String.Empty;
		private string _cnvcGroupName = String.Empty;
		
		#endregion
		
		#region 构造函数




		public ComputationGroup():base()
		{
		}
		
		public ComputationGroup(DataRow row):base(row)
		{
		}
		
		public ComputationGroup(DataTable table):base(table)
		{
		}
		
		public ComputationGroup(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 计量单位组编码
		/// </summary>
		[ColumnMapping("cnvcGroupCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcGroupCode
		{
			get {return _cnvcGroupCode;}
			set {_cnvcGroupCode = value;}
		}

		/// <summary>
		/// 计量单位组名称
		/// </summary>
		[ColumnMapping("cnvcGroupName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcGroupName
		{
			get {return _cnvcGroupName;}
			set {_cnvcGroupName = value;}
		}
		#endregion
	}	
}
