
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	GroupMake.cs
* ����:	     ֣��
* ��������:    2008-10-23
* ��������:    �����������Ӧ��

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
	/// **�������ƣ������������Ӧ��ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbGroupMake")]
	public class GroupMake: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private string _cnvcProductType = String.Empty;
		private string _cnvcGroupCode = String.Empty;
		private string _cnvcMakeName = String.Empty;
		
		#endregion
		
		#region ���캯��




		public GroupMake():base()
		{
		}
		
		public GroupMake(DataRow row):base(row)
		{
		}
		
		public GroupMake(DataTable table):base(table)
		{
		}
		
		public GroupMake(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// ��Ʒ����
		/// </summary>
		[ColumnMapping("cnvcProductType",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductType
		{
			get {return _cnvcProductType;}
			set {_cnvcProductType = value;}
		}

		/// <summary>
		/// ������
		/// </summary>
		[ColumnMapping("cnvcGroupCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcGroupCode
		{
			get {return _cnvcGroupCode;}
			set {_cnvcGroupCode = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
		[ColumnMapping("cnvcMakeName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcMakeName
		{
			get {return _cnvcMakeName;}
			set {_cnvcMakeName = value;}
		}
		#endregion
	}	
}
