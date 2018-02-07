
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:  	RdRecordDetail.cs
* ����:		     ֣��
* ��������:     2010-4-4
* ��������:    �շ���¼�ӱ�

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
	/// **�������ƣ��շ���¼�ӱ�ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbRdRecordDetail")]
	public class RdRecordDetail: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private decimal _cnnAutoID;
		private decimal _cnnRdID;
		private string _cnvcPOID = String.Empty;
		private decimal _cnnMPoID;
		private string _cnvcProviderID = String.Empty;
		private string _cnvcInvCode = String.Empty;
		private decimal _cnnQuantity;
		private decimal _cnnPrice;
		private decimal _cnnCost;
		private decimal _cnnExtraCost;
		private string _cnvcGroupCode = String.Empty;
		private string _cnvcComunitCode = String.Empty;
		private string _cnvcBatch = String.Empty;
		private string _cnvcFlag = String.Empty;
		private string _cnvcCommens = String.Empty;
		private DateTime _cndMdate;
		private int _cnnMassDate;
		private string _cnvcMassUnit = String.Empty;
		private DateTime _cndExpDate;
		
		#endregion
		
		#region ���캯��




		public RdRecordDetail():base()
		{
		}
		
		public RdRecordDetail(DataRow row):base(row)
		{
		}
		
		public RdRecordDetail(DataTable table):base(table)
		{
		}
		
		public RdRecordDetail(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// �շ���¼�ӱ��ʶ
		/// </summary>
		[ColumnMapping("cnnAutoID",IsPrimaryKey=true,IsIdentity=true,IsVersionNumber=false)]
		public decimal cnnAutoID
		{
			get {return _cnnAutoID;}
			set {_cnnAutoID = value;}
		}

		/// <summary>
		/// �շ���¼�����ʶ
		/// </summary>
		[ColumnMapping("cnnRdID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnRdID
		{
			get {return _cnnRdID;}
			set {_cnnRdID = value;}
		}

		/// <summary>
		/// ��Ӧ����������
		/// </summary>
		[ColumnMapping("cnvcPOID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPOID
		{
			get {return _cnvcPOID;}
			set {_cnvcPOID = value;}
		}

		/// <summary>
		/// ��Ӧ�����ӱ��ʶ
		/// </summary>
		[ColumnMapping("cnnMPoID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnMPoID
		{
			get {return _cnnMPoID;}
			set {_cnnMPoID = value;}
		}

		/// <summary>
		/// ��Ӧ�̱���
		/// </summary>
		[ColumnMapping("cnvcProviderID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProviderID
		{
			get {return _cnvcProviderID;}
			set {_cnvcProviderID = value;}
		}

		/// <summary>
		/// �������
		/// </summary>
		[ColumnMapping("cnvcInvCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcInvCode
		{
			get {return _cnvcInvCode;}
			set {_cnvcInvCode = value;}
		}

		/// <summary>
		/// ����
		/// </summary>
		[ColumnMapping("cnnQuantity",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnQuantity
		{
			get {return _cnnQuantity;}
			set {_cnnQuantity = value;}
		}

		/// <summary>
		/// ����
		/// </summary>
		[ColumnMapping("cnnPrice",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnPrice
		{
			get {return _cnnPrice;}
			set {_cnnPrice = value;}
		}

		/// <summary>
		/// ���
		/// </summary>
		[ColumnMapping("cnnCost",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnCost
		{
			get {return _cnnCost;}
			set {_cnnCost = value;}
		}

		/// <summary>
		/// �������
		/// </summary>
		[ColumnMapping("cnnExtraCost",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnExtraCost
		{
			get {return _cnnExtraCost;}
			set {_cnnExtraCost = value;}
		}

		/// <summary>
		/// ������λ�����
		/// </summary>
		[ColumnMapping("cnvcGroupCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcGroupCode
		{
			get {return _cnvcGroupCode;}
			set {_cnvcGroupCode = value;}
		}

		/// <summary>
		/// ������λ����
		/// </summary>
		[ColumnMapping("cnvcComunitCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcComunitCode
		{
			get {return _cnvcComunitCode;}
			set {_cnvcComunitCode = value;}
		}

		/// <summary>
		/// ����
		/// </summary>
		[ColumnMapping("cnvcBatch",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcBatch
		{
			get {return _cnvcBatch;}
			set {_cnvcBatch = value;}
		}

		/// <summary>
		/// ��־
		/// </summary>
		[ColumnMapping("cnvcFlag",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcFlag
		{
			get {return _cnvcFlag;}
			set {_cnvcFlag = value;}
		}

		/// <summary>
		/// ��ע
		/// </summary>
		[ColumnMapping("cnvcCommens",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCommens
		{
			get {return _cnvcCommens;}
			set {_cnvcCommens = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
		[ColumnMapping("cndMdate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndMdate
		{
			get {return _cndMdate;}
			set {_cndMdate = value;}
		}

		/// <summary>
		/// ����������
		/// </summary>
		[ColumnMapping("cnnMassDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public int cnnMassDate
		{
			get {return _cnnMassDate;}
			set {_cnnMassDate = value;}
		}

		/// <summary>
		/// �����ڵ�λ
		/// </summary>
		[ColumnMapping("cnvcMassUnit",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcMassUnit
		{
			get {return _cnvcMassUnit;}
			set {_cnvcMassUnit = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
		[ColumnMapping("cndExpDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndExpDate
		{
			get {return _cndExpDate;}
			set {_cndExpDate = value;}
		}
		#endregion
	}	
}
