
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	Dept.cs
* ����:	     ֣��
* ��������:    2008-10-30
* ��������:    ���ű�

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
	/// **�������ƣ����ű�ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbDept")]
	public class Dept: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private string _cnvcDeptName = String.Empty;
		private string _cnvcDeptID = String.Empty;
		private string _cnvcDeptType = String.Empty;
		private string _cnvcParentDeptID = String.Empty;
		private string _cnvcComments = String.Empty;
		private int _cnnPriority;
		
		#endregion
		
		#region ���캯��




		public Dept():base()
		{
		}
		
		public Dept(DataRow row):base(row)
		{
		}
		
		public Dept(DataTable table):base(table)
		{
		}
		
		public Dept(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// ��������
		/// </summary>
		[ColumnMapping("cnvcDeptName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcDeptName
		{
			get {return _cnvcDeptName;}
			set {_cnvcDeptName = value;}
		}

		/// <summary>
		/// ���ű���
		/// </summary>
		[ColumnMapping("cnvcDeptID",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcDeptID
		{
			get {return _cnvcDeptID;}
			set {_cnvcDeptID = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
		[ColumnMapping("cnvcDeptType",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcDeptType
		{
			get {return _cnvcDeptType;}
			set {_cnvcDeptType = value;}
		}

		/// <summary>
		/// �ϼ����ű���
		/// </summary>
		[ColumnMapping("cnvcParentDeptID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcParentDeptID
		{
			get {return _cnvcParentDeptID;}
			set {_cnvcParentDeptID = value;}
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

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnnPriority",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public int cnnPriority
		{
			get {return _cnnPriority;}
			set {_cnnPriority = value;}
		}
		#endregion
	}	
}
