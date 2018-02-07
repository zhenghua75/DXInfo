
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	OrderReduceLog.cs
* ����:	     ֣��
* ��������:    2008-10-7
* ��������:    ����������ˮ��

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
	/// **�������ƣ�����������ˮ��ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbOrderReduceLog")]
	public class OrderReduceLog: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private decimal _cnnReduceSerialNo;
		private decimal _cnnOrderSerialNo;
		private string _cnvcProductCode = String.Empty;
		private string _cnvcProductName = String.Empty;
		private decimal _cnnReduceCount;
		private string _cnvcUnit = String.Empty;
		private decimal _cnnPrice;
		private decimal _cnnSum;
		private string _cnvcReduceType = String.Empty;
		private string _cnvcReduceState = String.Empty;
		private string _cnvcReduceComments = String.Empty;
		private string _cnvcOperID = String.Empty;
		private DateTime _cndOperDate;
		
		#endregion
		
		#region ���캯��




		public OrderReduceLog():base()
		{
		}
		
		public OrderReduceLog(DataRow row):base(row)
		{
		}
		
		public OrderReduceLog(DataTable table):base(table)
		{
		}
		
		public OrderReduceLog(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// ������ˮ
		/// </summary>
		[ColumnMapping("cnnReduceSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnReduceSerialNo
		{
			get {return _cnnReduceSerialNo;}
			set {_cnnReduceSerialNo = value;}
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
		/// ��������
		/// </summary>
		[ColumnMapping("cnnReduceCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnReduceCount
		{
			get {return _cnnReduceCount;}
			set {_cnnReduceCount = value;}
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
		/// ��������
		/// </summary>
		[ColumnMapping("cnvcReduceType",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcReduceType
		{
			get {return _cnvcReduceType;}
			set {_cnvcReduceType = value;}
		}

		/// <summary>
		/// ����״̬
		/// </summary>
		[ColumnMapping("cnvcReduceState",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcReduceState
		{
			get {return _cnvcReduceState;}
			set {_cnvcReduceState = value;}
		}

		/// <summary>
		/// ����˵��
		/// </summary>
		[ColumnMapping("cnvcReduceComments",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcReduceComments
		{
			get {return _cnvcReduceComments;}
			set {_cnvcReduceComments = value;}
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
