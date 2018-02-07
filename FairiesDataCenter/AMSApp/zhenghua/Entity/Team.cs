
/******************************************************************** FR 1.20E *******
* 项目名称：   AMSApp
* 文件名:  	Team.cs
* 作者:		     郑华
* 创建日期:     2010-4-16
* 功能描述:    生产组

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
	/// **功能名称：生产组实体类
	/// </summary>
	[Serializable]
	[TableMapping("tbTeam")]
	public class Team: EntityObjectBase
	{
		#region 数据表生成变量



		
		
		private int _cnnTeamID;
		private string _cnvcTeamName = String.Empty;
		
		#endregion
		
		#region 构造函数




		public Team():base()
		{
		}
		
		public Team(DataRow row):base(row)
		{
		}
		
		public Team(DataTable table):base(table)
		{
		}
		
		public Team(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region 系统生成属性




				
		/// <summary>
		/// 生产组编码
		/// </summary>
		[ColumnMapping("cnnTeamID",IsPrimaryKey=true,IsIdentity=true,IsVersionNumber=false)]
		public int cnnTeamID
		{
			get {return _cnnTeamID;}
			set {_cnnTeamID = value;}
		}

		/// <summary>
		/// 生产组名称
		/// </summary>
		[ColumnMapping("cnvcTeamName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcTeamName
		{
			get {return _cnvcTeamName;}
			set {_cnvcTeamName = value;}
		}
		#endregion
	}	
}
