
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:  	CurrentStock.cs
* ����:		     ֣��
* ��������:     2010-3-7
* ��������:    �ִ������ܱ�

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
	/// **�������ƣ��ִ������ܱ�ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbCurrentStock")]
	public class CurrentStock: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private decimal _cnnAutoID;
		private string _cnvcWhCode = String.Empty;
		private string _cnvcInvCode = String.Empty;
		private decimal _cnnQuantity;
		private decimal _cnnOutQuantity;
		private decimal _cnnInQuantity;
		private string _cnvcStopFlag = String.Empty;
		private decimal _cnnTransInQuantity;
		private DateTime _cndMdate;
		private decimal _cnnTransOutQuantity;
		private decimal _cnnPlanQuantity;
		private decimal _cnnDisableQuantity;
		private decimal _cnnAvaQuantity;
//		private byte[] _ufts;
		private int _cnnMassDate;
		private string _cnvcMassUnit = String.Empty;
		private decimal _cnnStopQuantity;
		private decimal _cnnStopNum;
		private DateTime _cndExpDate;
		
		#endregion
		
		#region ���캯��




		public CurrentStock():base()
		{
		}
		
		public CurrentStock(DataRow row):base(row)
		{
		}
		
		public CurrentStock(DataTable table):base(table)
		{
		}
		
		public CurrentStock(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// �Զ����
		/// </summary>
		[ColumnMapping("cnnAutoID",IsPrimaryKey=true,IsIdentity=true,IsVersionNumber=false)]
		public decimal cnnAutoID
		{
			get {return _cnnAutoID;}
			set {_cnnAutoID = value;}
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
		/// �������
		/// </summary>
		[ColumnMapping("cnvcInvCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcInvCode
		{
			get {return _cnvcInvCode;}
			set {_cnvcInvCode = value;}
		}

		/// <summary>
		/// �������
		/// </summary>
		[ColumnMapping("cnnQuantity",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnQuantity
		{
			get {return _cnnQuantity;}
			set {_cnnQuantity = value;}
		}

		/// <summary>
		/// ����������
		/// </summary>
		[ColumnMapping("cnnOutQuantity",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnOutQuantity
		{
			get {return _cnnOutQuantity;}
			set {_cnnOutQuantity = value;}
		}

		/// <summary>
		/// ���������
		/// </summary>
		[ColumnMapping("cnnInQuantity",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnInQuantity
		{
			get {return _cnnInQuantity;}
			set {_cnnInQuantity = value;}
		}

		/// <summary>
		/// �����??����
		/// </summary>
		[ColumnMapping("cnvcStopFlag",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcStopFlag
		{
			get {return _cnvcStopFlag;}
			set {_cnvcStopFlag = value;}
		}

		/// <summary>
		/// ������;����
		/// </summary>
		[ColumnMapping("cnnTransInQuantity",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnTransInQuantity
		{
			get {return _cnnTransInQuantity;}
			set {_cnnTransInQuantity = value;}
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
		/// ������������
		/// </summary>
		[ColumnMapping("cnnTransOutQuantity",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnTransOutQuantity
		{
			get {return _cnnTransOutQuantity;}
			set {_cnnTransOutQuantity = value;}
		}

		/// <summary>
		/// �ƻ���������
		/// </summary>
		[ColumnMapping("cnnPlanQuantity",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnPlanQuantity
		{
			get {return _cnnPlanQuantity;}
			set {_cnnPlanQuantity = value;}
		}

		/// <summary>
		/// ���ϸ�����
		/// </summary>
		[ColumnMapping("cnnDisableQuantity",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnDisableQuantity
		{
			get {return _cnnDisableQuantity;}
			set {_cnnDisableQuantity = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
		[ColumnMapping("cnnAvaQuantity",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnAvaQuantity
		{
			get {return _cnnAvaQuantity;}
			set {_cnnAvaQuantity = value;}
		}
//
//		/// <summary>
//		/// ʱ���
//		/// </summary>
//		[ColumnMapping("ufts",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
//		public byte[] ufts
//		{
//			get {return _ufts;}
//			set {_ufts = value;}
//		}

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
		[ColumnMapping("cnnStopQuantity",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnStopQuantity
		{
			get {return _cnnStopQuantity;}
			set {_cnnStopQuantity = value;}
		}

		/// <summary>
		/// �������
		/// </summary>
		[ColumnMapping("cnnStopNum",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnStopNum
		{
			get {return _cnnStopNum;}
			set {_cnnStopNum = value;}
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
