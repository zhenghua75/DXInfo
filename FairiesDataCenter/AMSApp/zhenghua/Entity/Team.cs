
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:  	Team.cs
* ����:		     ֣��
* ��������:     2010-4-16
* ��������:    ������

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
	/// **�������ƣ�������ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbTeam")]
	public class Team: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private int _cnnTeamID;
		private string _cnvcTeamName = String.Empty;
		
		#endregion
		
		#region ���캯��




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
		
		#region ϵͳ��������




				
		/// <summary>
		/// ���������
		/// </summary>
		[ColumnMapping("cnnTeamID",IsPrimaryKey=true,IsIdentity=true,IsVersionNumber=false)]
		public int cnnTeamID
		{
			get {return _cnnTeamID;}
			set {_cnnTeamID = value;}
		}

		/// <summary>
		/// ����������
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
