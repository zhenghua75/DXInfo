
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:  	Warehouse.cs
* ����:		     ֣��
* ��������:     2010-3-27
* ��������:    �ֿ⵵��

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
	/// **�������ƣ��ֿ⵵��ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbWarehouse")]
	public class Warehouse: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private string _cnvcWhCode = String.Empty;
		private string _cnvcWhName = String.Empty;
		private string _cnvcDepCode = String.Empty;
		private string _cnvcWhAddress = String.Empty;
		private string _cnvcWhPhone = String.Empty;
		private string _cnvcWhPerson = String.Empty;
		private string _cnvcWhValueStyle = String.Empty;
		private string _cnvcWhMemo = String.Empty;
		private bool _cnbFreeze;
		private short _cnnFrequency;
		private string _cnvcFrequency = String.Empty;
		private DateTime _cndLastDate;
		private bool _cnbShop;
		private string _cnvcWhProperty = String.Empty;
		
		#endregion
		
		#region ���캯��




		public Warehouse():base()
		{
		}
		
		public Warehouse(DataRow row):base(row)
		{
		}
		
		public Warehouse(DataTable table):base(table)
		{
		}
		
		public Warehouse(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// �ֿ����
		/// </summary>
		[ColumnMapping("cnvcWhCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcWhCode
		{
			get {return _cnvcWhCode;}
			set {_cnvcWhCode = value;}
		}

		/// <summary>
		/// �ֿ�����
		/// </summary>
		[ColumnMapping("cnvcWhName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcWhName
		{
			get {return _cnvcWhName;}
			set {_cnvcWhName = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
		[ColumnMapping("cnvcDepCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcDepCode
		{
			get {return _cnvcDepCode;}
			set {_cnvcDepCode = value;}
		}

		/// <summary>
		/// �ֿ��ַ
		/// </summary>
		[ColumnMapping("cnvcWhAddress",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcWhAddress
		{
			get {return _cnvcWhAddress;}
			set {_cnvcWhAddress = value;}
		}

		/// <summary>
		/// �绰
		/// </summary>
		[ColumnMapping("cnvcWhPhone",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcWhPhone
		{
			get {return _cnvcWhPhone;}
			set {_cnvcWhPhone = value;}
		}

		/// <summary>
		/// ������
		/// </summary>
		[ColumnMapping("cnvcWhPerson",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcWhPerson
		{
			get {return _cnvcWhPerson;}
			set {_cnvcWhPerson = value;}
		}

		/// <summary>
		/// �Ƽ۷�ʽ
		/// </summary>
		[ColumnMapping("cnvcWhValueStyle",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcWhValueStyle
		{
			get {return _cnvcWhValueStyle;}
			set {_cnvcWhValueStyle = value;}
		}

		/// <summary>
		/// ��ע
		/// </summary>
		[ColumnMapping("cnvcWhMemo",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcWhMemo
		{
			get {return _cnvcWhMemo;}
			set {_cnvcWhMemo = value;}
		}

		/// <summary>
		/// �Ƿ񶳽�
		/// </summary>
		[ColumnMapping("cnbFreeze",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public bool cnbFreeze
		{
			get {return _cnbFreeze;}
			set {_cnbFreeze = value;}
		}

		/// <summary>
		/// �̵�����
		/// </summary>
		[ColumnMapping("cnnFrequency",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public short cnnFrequency
		{
			get {return _cnnFrequency;}
			set {_cnnFrequency = value;}
		}

		/// <summary>
		/// �̵����ڵ�λ
		/// </summary>
		[ColumnMapping("cnvcFrequency",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcFrequency
		{
			get {return _cnvcFrequency;}
			set {_cnvcFrequency = value;}
		}

		/// <summary>
		/// �ϴ��̵�����
		/// </summary>
		[ColumnMapping("cndLastDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndLastDate
		{
			get {return _cndLastDate;}
			set {_cndLastDate = value;}
		}

		/// <summary>
		/// �Ƿ��ŵ�
		/// </summary>
		[ColumnMapping("cnbShop",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public bool cnbShop
		{
			get {return _cnbShop;}
			set {_cnbShop = value;}
		}

		/// <summary>
		/// �ֿ�����
		/// </summary>
		[ColumnMapping("cnvcWhProperty",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcWhProperty
		{
			get {return _cnvcWhProperty;}
			set {_cnvcWhProperty = value;}
		}
		#endregion
	}	
}
