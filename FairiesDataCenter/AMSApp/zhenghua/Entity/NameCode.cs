
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	NameCode.cs
* ����:	     ֣��
* ��������:    2008-10-10
* ��������:    �����

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
	/// **�������ƣ������ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbNameCode")]
	public class NameCode: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private string _cnvcType = String.Empty;
		private string _cnvcCode = String.Empty;
		private string _cnvcName = String.Empty;
		private string _cnvcComments = String.Empty;
		
		#endregion
		
		#region ���캯��




		public NameCode():base()
		{
		}
		
		public NameCode(DataRow row):base(row)
		{
		}
		
		public NameCode(DataTable table):base(table)
		{
		}
		
		public NameCode(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// ����
		/// </summary>
		[ColumnMapping("cnvcType",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcType
		{
			get {return _cnvcType;}
			set {_cnvcType = value;}
		}

		/// <summary>
		/// ����
		/// </summary>
		[ColumnMapping("cnvcCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCode
		{
			get {return _cnvcCode;}
			set {_cnvcCode = value;}
		}

		/// <summary>
		/// ����
		/// </summary>
		[ColumnMapping("cnvcName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcName
		{
			get {return _cnvcName;}
			set {_cnvcName = value;}
		}

		/// <summary>
		/// ����
		/// </summary>
		[ColumnMapping("cnvcComments",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcComments
		{
			get {return _cnvcComments;}
			set {_cnvcComments = value;}
		}
		#endregion
	}	
}
