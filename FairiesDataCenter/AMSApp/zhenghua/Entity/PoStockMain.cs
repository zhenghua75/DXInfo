
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:  	PoStockMain.cs
* ����:		     ֣��
* ��������:     2010-3-7
* ��������:    �ɹ���������

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
	/// **�������ƣ��ɹ���������ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbPoStockMain")]
	public class PoStockMain: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private string _cnvcPoID = String.Empty;
		private string _cnvcPrvdCode = String.Empty;
		private string _cnvcAddress = String.Empty;
		private string _cnvcComments = String.Empty;
		private string _cnvcPoState = String.Empty;
		private string _cnvcPlanCycle = String.Empty;
		private string _cnvcCreater = String.Empty;
		private string _cnvcModer = String.Empty;
		private string _cnvcChecker = String.Empty;
		private string _cnvcCloser = String.Empty;
		private DateTime _cndCreateDate;
		private DateTime _cndModDate;
		private DateTime _cndCheckDate;
		private DateTime _cndCloseDate;
		
		#endregion
		
		#region ���캯��




		public PoStockMain():base()
		{
		}
		
		public PoStockMain(DataRow row):base(row)
		{
		}
		
		public PoStockMain(DataTable table):base(table)
		{
		}
		
		public PoStockMain(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// �ɹ�������
		/// </summary>
		[ColumnMapping("cnvcPoID",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPoID
		{
			get {return _cnvcPoID;}
			set {_cnvcPoID = value;}
		}

		/// <summary>
		/// ��Ӧ�̱���
		/// </summary>
		[ColumnMapping("cnvcPrvdCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdCode
		{
			get {return _cnvcPrvdCode;}
			set {_cnvcPrvdCode = value;}
		}

		/// <summary>
		/// ������ַ
		/// </summary>
		[ColumnMapping("cnvcAddress",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcAddress
		{
			get {return _cnvcAddress;}
			set {_cnvcAddress = value;}
		}

		/// <summary>
		/// ��ע
		/// </summary>
		[ColumnMapping("cnvcComments",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcComments
		{
			get {return _cnvcComments;}
			set {_cnvcComments = value;}
		}

		/// <summary>
		/// ����״̬
		/// </summary>
		[ColumnMapping("cnvcPoState",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPoState
		{
			get {return _cnvcPoState;}
			set {_cnvcPoState = value;}
		}

		/// <summary>
		/// ��������ƻ�����
		/// </summary>
		[ColumnMapping("cnvcPlanCycle",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPlanCycle
		{
			get {return _cnvcPlanCycle;}
			set {_cnvcPlanCycle = value;}
		}

		/// <summary>
		/// �Ƶ���
		/// </summary>
		[ColumnMapping("cnvcCreater",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCreater
		{
			get {return _cnvcCreater;}
			set {_cnvcCreater = value;}
		}

		/// <summary>
		/// �޸���
		/// </summary>
		[ColumnMapping("cnvcModer",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcModer
		{
			get {return _cnvcModer;}
			set {_cnvcModer = value;}
		}

		/// <summary>
		/// �����
		/// </summary>
		[ColumnMapping("cnvcChecker",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcChecker
		{
			get {return _cnvcChecker;}
			set {_cnvcChecker = value;}
		}

		/// <summary>
		/// �ر���
		/// </summary>
		[ColumnMapping("cnvcCloser",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCloser
		{
			get {return _cnvcCloser;}
			set {_cnvcCloser = value;}
		}

		/// <summary>
		/// �Ƶ�ʱ��
		/// </summary>
		[ColumnMapping("cndCreateDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndCreateDate
		{
			get {return _cndCreateDate;}
			set {_cndCreateDate = value;}
		}

		/// <summary>
		/// �޸�ʱ��
		/// </summary>
		[ColumnMapping("cndModDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndModDate
		{
			get {return _cndModDate;}
			set {_cndModDate = value;}
		}

		/// <summary>
		/// ���ʱ��
		/// </summary>
		[ColumnMapping("cndCheckDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndCheckDate
		{
			get {return _cndCheckDate;}
			set {_cndCheckDate = value;}
		}

		/// <summary>
		/// �ر�ʱ��
		/// </summary>
		[ColumnMapping("cndCloseDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndCloseDate
		{
			get {return _cndCloseDate;}
			set {_cndCloseDate = value;}
		}
		#endregion
	}	
}
