
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	OrderAddLog.cs
* ����:	     ֣��
* ��������:    2008-10-7
* ��������:    �����ӵ���ˮ��

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
	/// **�������ƣ������ӵ���ˮ��ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbOrderAddLog")]
	public class OrderAddLog: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private decimal _cnnAddSerialNo;
		private decimal _cnnOrderSerialNo;
		private string _cnvcProductCode = String.Empty;
		private string _cnvcProductName = String.Empty;
		private decimal _cnnAddCount;
		private string _cnvcUnit = String.Empty;
		private decimal _cnnPrice;
		private decimal _cnnSum;
		private string _cnvcAddType = String.Empty;
		private string _cnvcAddState = String.Empty;
		private string _cnvcAddComments = String.Empty;
		private string _cnvcOperID = String.Empty;
		private DateTime _cndOperDate;
		
		#endregion
		
		#region ���캯��




		public OrderAddLog():base()
		{
		}
		
		public OrderAddLog(DataRow row):base(row)
		{
		}
		
		public OrderAddLog(DataTable table):base(table)
		{
		}
		
		public OrderAddLog(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// �ӵ���ˮ
		/// </summary>
		[ColumnMapping("cnnAddSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnAddSerialNo
		{
			get {return _cnnAddSerialNo;}
			set {_cnnAddSerialNo = value;}
		}

		/// <summary>
		/// ������ˮ
		/// </summary>
		[ColumnMapping("cnnOrderSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnOrderSerialNo
		{
			get {return _cnnOrderSerialNo;}
			set {_cnnOrderSerialNo = value;}
		}

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		[ColumnMapping("cnvcProductCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductCode
		{
			get {return _cnvcProductCode;}
			set {_cnvcProductCode = value;}
		}

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		[ColumnMapping("cnvcProductName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductName
		{
			get {return _cnvcProductName;}
			set {_cnvcProductName = value;}
		}

		/// <summary>
		/// �ӵ�����
		/// </summary>
		[ColumnMapping("cnnAddCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnAddCount
		{
			get {return _cnnAddCount;}
			set {_cnnAddCount = value;}
		}

		/// <summary>
		/// ��λ
		/// </summary>
		[ColumnMapping("cnvcUnit",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcUnit
		{
			get {return _cnvcUnit;}
			set {_cnvcUnit = value;}
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
		/// �ϼ�
		/// </summary>
		[ColumnMapping("cnnSum",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnSum
		{
			get {return _cnnSum;}
			set {_cnnSum = value;}
		}

		/// <summary>
		/// �ӵ�����
		/// </summary>
		[ColumnMapping("cnvcAddType",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcAddType
		{
			get {return _cnvcAddType;}
			set {_cnvcAddType = value;}
		}

		/// <summary>
		/// �ӵ�״̬
		/// </summary>
		[ColumnMapping("cnvcAddState",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcAddState
		{
			get {return _cnvcAddState;}
			set {_cnvcAddState = value;}
		}

		/// <summary>
		/// �ӵ�˵��
		/// </summary>
		[ColumnMapping("cnvcAddComments",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcAddComments
		{
			get {return _cnvcAddComments;}
			set {_cnvcAddComments = value;}
		}

		/// <summary>
		/// ����Ա
		/// </summary>
		[ColumnMapping("cnvcOperID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcOperID
		{
			get {return _cnvcOperID;}
			set {_cnvcOperID = value;}
		}

		/// <summary>
		/// ����ʱ��
		/// </summary>
		[ColumnMapping("cndOperDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndOperDate
		{
			get {return _cndOperDate;}
			set {_cndOperDate = value;}
		}
		#endregion
	}	
}
