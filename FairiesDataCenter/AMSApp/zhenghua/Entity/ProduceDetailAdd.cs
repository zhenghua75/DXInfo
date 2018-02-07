
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	ProduceDetailAdd.cs
* ����:	     ֣��
* ��������:    2008-10-12
* ��������:    �����ƻ���Ʒϸ�ڼӵ���

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
	/// **�������ƣ������ƻ���Ʒϸ�ڼӵ���ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbProduceDetailAdd")]
	public class ProduceDetailAdd: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private decimal _cnnProduceSerialNo;
		private decimal _cnnAddSerialNo;
		private string _cnvcCode = String.Empty;
		private string _cnvcName = String.Empty;
		private string _cnvcUnit = String.Empty;
		private decimal _cnnCount;
		private string _cnvcState = String.Empty;
		
		#endregion
		
		#region ���캯��




		public ProduceDetailAdd():base()
		{
		}
		
		public ProduceDetailAdd(DataRow row):base(row)
		{
		}
		
		public ProduceDetailAdd(DataTable table):base(table)
		{
		}
		
		public ProduceDetailAdd(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
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
		/// �ӵ���ˮ
		/// </summary>
		[ColumnMapping("cnnAddSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnAddSerialNo
		{
			get {return _cnnAddSerialNo;}
			set {_cnnAddSerialNo = value;}
		}

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		[ColumnMapping("cnvcCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCode
		{
			get {return _cnvcCode;}
			set {_cnvcCode = value;}
		}

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		[ColumnMapping("cnvcName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcName
		{
			get {return _cnvcName;}
			set {_cnvcName = value;}
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
		[ColumnMapping("cnnCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnCount
		{
			get {return _cnnCount;}
			set {_cnnCount = value;}
		}

		/// <summary>
		/// ״̬
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
