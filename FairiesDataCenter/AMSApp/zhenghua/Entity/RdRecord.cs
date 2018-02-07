
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:  	RdRecord.cs
* ����:		     ֣��
* ��������:     2010-3-7
* ��������:    �շ���¼����

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
	/// **�������ƣ��շ���¼����ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbRdRecord")]
	public class RdRecord: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private decimal _cnnRdID;
		private string _cnvcCode = String.Empty;
		private string _cnvcRdCode = String.Empty;
		private string _cnvcRdFlag = String.Empty;
		private string _cnvcIsLsQuery = String.Empty;
		private string _cnvcWhCode = String.Empty;
		private string _cnvcDepID = String.Empty;
		private string _cnvcWhpersonName = String.Empty;
		private string _cnvcOperName = String.Empty;
		private string _cnvcCusCode = String.Empty;
//		private string _cnvcProBatch = String.Empty;
		private string _cnvcComments = String.Empty;
		private string _cnvcMaker = String.Empty;
		private DateTime _cndMakeDate;
		private string _cnvcModer = String.Empty;
		private DateTime _cndModDate;
		private string _cnvcHandler = String.Empty;
		private DateTime _cndHandDate;
//		private string _cnvcMPoCode = String.Empty;
		private decimal _cnnProorderID;
		private string _cnvcShipAddress = String.Empty;
		private DateTime _cndARVDate;
		private string _cnvcARVAddress = String.Empty;
		private string _cnvcState = String.Empty;
		
		#endregion
		
		#region ���캯��




		public RdRecord():base()
		{
		}
		
		public RdRecord(DataRow row):base(row)
		{
		}
		
		public RdRecord(DataTable table):base(table)
		{
		}
		
		public RdRecord(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// �շ���¼�����ʶ
		/// </summary>
		[ColumnMapping("cnnRdID",IsPrimaryKey=true,IsIdentity=true,IsVersionNumber=false)]
		public decimal cnnRdID
		{
			get {return _cnnRdID;}
			set {_cnnRdID = value;}
		}

		/// <summary>
		/// �շ����ݺ�
		/// </summary>
		[ColumnMapping("cnvcCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCode
		{
			get {return _cnvcCode;}
			set {_cnvcCode = value;}
		}

		/// <summary>
		/// �շ������
		/// </summary>
		[ColumnMapping("cnvcRdCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcRdCode
		{
			get {return _cnvcRdCode;}
			set {_cnvcRdCode = value;}
		}

		/// <summary>
		/// �շ���־
		/// </summary>
		[ColumnMapping("cnvcRdFlag",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcRdFlag
		{
			get {return _cnvcRdFlag;}
			set {_cnvcRdFlag = value;}
		}

		/// <summary>
		/// �Ƿ��·�����
		/// </summary>
		[ColumnMapping("cnvcIsLsQuery",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcIsLsQuery
		{
			get {return _cnvcIsLsQuery;}
			set {_cnvcIsLsQuery = value;}
		}

		/// <summary>
		/// �ֿ����
		/// </summary>
		[ColumnMapping("cnvcWhCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcWhCode
		{
			get {return _cnvcWhCode;}
			set {_cnvcWhCode = value;}
		}

		/// <summary>
		/// ���ű���
		/// </summary>
		[ColumnMapping("cnvcDepID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcDepID
		{
			get {return _cnvcDepID;}
			set {_cnvcDepID = value;}
		}

		/// <summary>
		/// ���Ա����
		/// </summary>
		[ColumnMapping("cnvcWhpersonName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcWhpersonName
		{
			get {return _cnvcWhpersonName;}
			set {_cnvcWhpersonName = value;}
		}

		/// <summary>
		/// ҵ��Ա
		/// </summary>
		[ColumnMapping("cnvcOperName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcOperName
		{
			get {return _cnvcOperName;}
			set {_cnvcOperName = value;}
		}

		/// <summary>
		/// �ͻ�����
		/// </summary>
		[ColumnMapping("cnvcCusCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCusCode
		{
			get {return _cnvcCusCode;}
			set {_cnvcCusCode = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
//		[ColumnMapping("cnvcProBatch",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
//		public string cnvcProBatch
//		{
//			get {return _cnvcProBatch;}
//			set {_cnvcProBatch = value;}
//		}

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
		/// �Ƶ���
		/// </summary>
		[ColumnMapping("cnvcMaker",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcMaker
		{
			get {return _cnvcMaker;}
			set {_cnvcMaker = value;}
		}

		/// <summary>
		/// �Ƶ�ʱ��
		/// </summary>
		[ColumnMapping("cndMakeDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndMakeDate
		{
			get {return _cndMakeDate;}
			set {_cndMakeDate = value;}
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
		/// �޸�ʱ��
		/// </summary>
		[ColumnMapping("cndModDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndModDate
		{
			get {return _cndModDate;}
			set {_cndModDate = value;}
		}

		/// <summary>
		/// �����
		/// </summary>
		[ColumnMapping("cnvcHandler",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcHandler
		{
			get {return _cnvcHandler;}
			set {_cnvcHandler = value;}
		}

		/// <summary>
		/// ���ʱ��
		/// </summary>
		[ColumnMapping("cndHandDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndHandDate
		{
			get {return _cndHandDate;}
			set {_cndHandDate = value;}
		}

		/// <summary>
		/// �����������
		/// </summary>
//		[ColumnMapping("cnvcMPoCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
//		public string cnvcMPoCode
//		{
//			get {return _cnvcMPoCode;}
//			set {_cnvcMPoCode = value;}
//		}

		/// <summary>
		/// �������������ʶ
		/// </summary>
		[ColumnMapping("cnnProorderID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnProorderID
		{
			get {return _cnnProorderID;}
			set {_cnnProorderID = value;}
		}

		/// <summary>
		/// ������ַ
		/// </summary>
		[ColumnMapping("cnvcShipAddress",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcShipAddress
		{
			get {return _cnvcShipAddress;}
			set {_cnvcShipAddress = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
		[ColumnMapping("cndARVDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndARVDate
		{
			get {return _cndARVDate;}
			set {_cndARVDate = value;}
		}

		/// <summary>
		/// ������ַ
		/// </summary>
		[ColumnMapping("cnvcARVAddress",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcARVAddress
		{
			get {return _cnvcARVAddress;}
			set {_cnvcARVAddress = value;}
		}

		/// <summary>
		/// ����״̬
		/// </summary>
		[ColumnMapping("cnvcState",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcState
		{
			get {return _cnvcState;}
			set {_cnvcState = value;}
		}
		#endregion
	}	
}
