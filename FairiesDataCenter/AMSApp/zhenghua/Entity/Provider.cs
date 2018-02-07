
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:  	Provider.cs
* ����:		     ֣��
* ��������:     2010-3-7
* ��������:    ��Ӧ�̵���

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
	/// **�������ƣ���Ӧ�̵���ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbProvider")]
	public class Provider: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private string _cnvcPrvdCode = String.Empty;
		private string _cnvcPrvdName = String.Empty;
		private string _cnvcPrvdAbbName = String.Empty;
		private string _cnvcPrvdClass = String.Empty;
		private string _cnvcAddress = String.Empty;
		private string _cnvcPostCode = String.Empty;
		private string _cnvcPrvdPhone = String.Empty;
		private string _cnvcPrvdFax = String.Empty;
		private string _cnvcPrvdEmail = String.Empty;
		private string _cnvcPrvdLinkName = String.Empty;
		private DateTime _cndLastDate;
		private decimal _cnnLastMoney;
		private string _cnvcPrvdCredit = String.Empty;
		private string _cnvcPrvdQualification = String.Empty;
		private string _cnvcPrvdCreater = String.Empty;
		private DateTime _cndPrvdCreateDate;
		private string _cnvcActiveFlag = String.Empty;
		
		#endregion
		
		#region ���캯��




		public Provider():base()
		{
		}
		
		public Provider(DataRow row):base(row)
		{
		}
		
		public Provider(DataTable table):base(table)
		{
		}
		
		public Provider(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// ��Ӧ�̱���
		/// </summary>
		[ColumnMapping("cnvcPrvdCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdCode
		{
			get {return _cnvcPrvdCode;}
			set {_cnvcPrvdCode = value;}
		}

		/// <summary>
		/// ��Ӧ������
		/// </summary>
		[ColumnMapping("cnvcPrvdName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdName
		{
			get {return _cnvcPrvdName;}
			set {_cnvcPrvdName = value;}
		}

		/// <summary>
		/// ��Ӧ�̼��
		/// </summary>
		[ColumnMapping("cnvcPrvdAbbName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdAbbName
		{
			get {return _cnvcPrvdAbbName;}
			set {_cnvcPrvdAbbName = value;}
		}

		/// <summary>
		/// ��Ӧ�̷������
		/// </summary>
		[ColumnMapping("cnvcPrvdClass",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdClass
		{
			get {return _cnvcPrvdClass;}
			set {_cnvcPrvdClass = value;}
		}

		/// <summary>
		/// ��ַ
		/// </summary>
		[ColumnMapping("cnvcAddress",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcAddress
		{
			get {return _cnvcAddress;}
			set {_cnvcAddress = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
		[ColumnMapping("cnvcPostCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPostCode
		{
			get {return _cnvcPostCode;}
			set {_cnvcPostCode = value;}
		}

		/// <summary>
		/// �绰
		/// </summary>
		[ColumnMapping("cnvcPrvdPhone",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdPhone
		{
			get {return _cnvcPrvdPhone;}
			set {_cnvcPrvdPhone = value;}
		}

		/// <summary>
		/// ����
		/// </summary>
		[ColumnMapping("cnvcPrvdFax",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdFax
		{
			get {return _cnvcPrvdFax;}
			set {_cnvcPrvdFax = value;}
		}

		/// <summary>
		/// Email��ַ
		/// </summary>
		[ColumnMapping("cnvcPrvdEmail",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdEmail
		{
			get {return _cnvcPrvdEmail;}
			set {_cnvcPrvdEmail = value;}
		}

		/// <summary>
		/// ��ϵ��
		/// </summary>
		[ColumnMapping("cnvcPrvdLinkName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdLinkName
		{
			get {return _cnvcPrvdLinkName;}
			set {_cnvcPrvdLinkName = value;}
		}

		/// <summary>
		/// ���������
		/// </summary>
		[ColumnMapping("cndLastDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndLastDate
		{
			get {return _cndLastDate;}
			set {_cndLastDate = value;}
		}

		/// <summary>
		/// ����׽��
		/// </summary>
		[ColumnMapping("cnnLastMoney",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnLastMoney
		{
			get {return _cnnLastMoney;}
			set {_cnnLastMoney = value;}
		}

		/// <summary>
		/// ���õȼ�
		/// </summary>
		[ColumnMapping("cnvcPrvdCredit",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdCredit
		{
			get {return _cnvcPrvdCredit;}
			set {_cnvcPrvdCredit = value;}
		}

		/// <summary>
		/// ����֤��
		/// </summary>
		[ColumnMapping("cnvcPrvdQualification",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdQualification
		{
			get {return _cnvcPrvdQualification;}
			set {_cnvcPrvdQualification = value;}
		}

		/// <summary>
		/// ������
		/// </summary>
		[ColumnMapping("cnvcPrvdCreater",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPrvdCreater
		{
			get {return _cnvcPrvdCreater;}
			set {_cnvcPrvdCreater = value;}
		}

		/// <summary>
		/// ����ʱ��
		/// </summary>
		[ColumnMapping("cndPrvdCreateDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndPrvdCreateDate
		{
			get {return _cndPrvdCreateDate;}
			set {_cndPrvdCreateDate = value;}
		}

		/// <summary>
		/// ��Ч��־
		/// </summary>
		[ColumnMapping("cnvcActiveFlag",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcActiveFlag
		{
			get {return _cnvcActiveFlag;}
			set {_cnvcActiveFlag = value;}
		}
		#endregion
	}	
}
