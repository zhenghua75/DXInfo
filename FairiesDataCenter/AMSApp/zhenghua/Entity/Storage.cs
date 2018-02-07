
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	Storage.cs
* ����:	     ֣��
* ��������:    2008-11-3
* ��������:    ����

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
	/// **�������ƣ�����ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbStorage")]
	public class Storage: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private string _cnvcStorageDeptID = String.Empty;
		private string _cnvcProductCode = String.Empty;
		private string _cnvcProductName = String.Empty;
		private string _cnvcUnit = String.Empty;
		private decimal _cnnCount;
		private decimal _cnnSafeCount;
		private decimal _cnnSafeUpCount;
		
		#endregion
		
		#region ���캯��




		public Storage():base()
		{
		}
		
		public Storage(DataRow row):base(row)
		{
		}
		
		public Storage(DataTable table):base(table)
		{
		}
		
		public Storage(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// �ֿ�
		/// </summary>
		[ColumnMapping("cnvcStorageDeptID",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcStorageDeptID
		{
			get {return _cnvcStorageDeptID;}
			set {_cnvcStorageDeptID = value;}
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
		/// ��λ
		/// </summary>
		[ColumnMapping("cnvcUnit",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcUnit
		{
			get {return _cnvcUnit;}
			set {_cnvcUnit = value;}
		}

		/// <summary>
		/// ʵ�ʿ������
		/// </summary>
		[ColumnMapping("cnnCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnCount
		{
			get {return _cnnCount;}
			set {_cnnCount = value;}
		}

		/// <summary>
		/// ��ȫ�������
		/// </summary>
		[ColumnMapping("cnnSafeCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnSafeCount
		{
			get {return _cnnSafeCount;}
			set {_cnnSafeCount = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnnSafeUpCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnSafeUpCount
		{
			get {return _cnnSafeUpCount;}
			set {_cnnSafeUpCount = value;}
		}
		#endregion
	}	
}
