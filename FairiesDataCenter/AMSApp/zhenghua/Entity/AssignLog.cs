
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	AssignLog.cs
* ����:	     ֣��
* ��������:    2008-10-13
* ��������:    �ֻ���ˮ��

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
	/// **�������ƣ��ֻ���ˮ��ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbAssignLog")]
	public class AssignLog: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private decimal _cnnAssignSerialNo;
		private decimal _cnnProduceSerialNo;
		private decimal _cnnOrderSerialNo;
		private string _cnvcShipDeptID = String.Empty;
		private string _cnvcShipOperID = String.Empty;
		private string _cnvcReceiveDeptID = String.Empty;
		private string _cnvcReceiveOperID = String.Empty;
		private DateTime _cndShipDate;
		private DateTime _cndReceiveDate;
		private string _cnvcSalesroomOperID = String.Empty;
		private string _cnvcTransportOperID = String.Empty;
		private string _cnvcStorageOperID = String.Empty;
		private string _cnvcCustomerValidate = String.Empty;
		private string _cnvcCustomerIdea = String.Empty;
		private string _cnvcOperID = String.Empty;
		private DateTime _cndOperDate;
		private string _cnvcComments = String.Empty;
		private int    _cnnPrintFlag;
		
		#endregion
		
		#region ���캯��




		public AssignLog():base()
		{
		}
		
		public AssignLog(DataRow row):base(row)
		{
		}
		
		public AssignLog(DataTable table):base(table)
		{
		}
		
		public AssignLog(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// �ֻ���ˮ
		/// </summary>
		[ColumnMapping("cnnAssignSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnAssignSerialNo
		{
			get {return _cnnAssignSerialNo;}
			set {_cnnAssignSerialNo = value;}
		}
		/// <summary>
		/// ������ˮ
		/// </summary>
		[ColumnMapping("cnnProduceSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnProduceSerialNo
		{
			get {return _cnnProduceSerialNo;}
			set {_cnnProduceSerialNo = value;}
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
		/// ������λ
		/// </summary>
		[ColumnMapping("cnvcShipDeptID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcShipDeptID
		{
			get {return _cnvcShipDeptID;}
			set {_cnvcShipDeptID = value;}
		}

		/// <summary>
		/// ������
		/// </summary>
		[ColumnMapping("cnvcShipOperID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcShipOperID
		{
			get {return _cnvcShipOperID;}
			set {_cnvcShipOperID = value;}
		}

		/// <summary>
		/// �ջ���λ
		/// </summary>
		[ColumnMapping("cnvcReceiveDeptID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcReceiveDeptID
		{
			get {return _cnvcReceiveDeptID;}
			set {_cnvcReceiveDeptID = value;}
		}

		/// <summary>
		/// �ջ���
		/// </summary>
		[ColumnMapping("cnvcReceiveOperID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcReceiveOperID
		{
			get {return _cnvcReceiveOperID;}
			set {_cnvcReceiveOperID = value;}
		}

		/// <summary>
		/// ����ʱ��
		/// </summary>
		[ColumnMapping("cndShipDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndShipDate
		{
			get {return _cndShipDate;}
			set {_cndShipDate = value;}
		}

		/// <summary>
		/// �ջ�ʱ��
		/// </summary>
		[ColumnMapping("cndReceiveDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndReceiveDate
		{
			get {return _cndReceiveDate;}
			set {_cndReceiveDate = value;}
		}

		/// <summary>
		/// ����
		/// </summary>
		[ColumnMapping("cnvcSalesroomOperID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcSalesroomOperID
		{
			get {return _cnvcSalesroomOperID;}
			set {_cnvcSalesroomOperID = value;}
		}

		/// <summary>
		/// ����
		/// </summary>
		[ColumnMapping("cnvcTransportOperID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcTransportOperID
		{
			get {return _cnvcTransportOperID;}
			set {_cnvcTransportOperID = value;}
		}

		/// <summary>
		/// ��Ʒ��
		/// </summary>
		[ColumnMapping("cnvcStorageOperID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcStorageOperID
		{
			get {return _cnvcStorageOperID;}
			set {_cnvcStorageOperID = value;}
		}

		/// <summary>
		/// �ͻ�ǩ��
		/// </summary>
		[ColumnMapping("cnvcCustomerValidate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCustomerValidate
		{
			get {return _cnvcCustomerValidate;}
			set {_cnvcCustomerValidate = value;}
		}

		/// <summary>
		/// �ͻ��������
		/// </summary>
		[ColumnMapping("cnvcCustomerIdea",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCustomerIdea
		{
			get {return _cnvcCustomerIdea;}
			set {_cnvcCustomerIdea = value;}
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
		/// ��ӡ��־
		/// </summary>
		[ColumnMapping("cnnPrintFlag",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public int cnnPrintFlag
		{
			get {return _cnnPrintFlag;}
			set {_cnnPrintFlag = value;}
		}
		#endregion
	}	
}
