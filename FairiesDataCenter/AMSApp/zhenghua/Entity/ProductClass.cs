
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:  	ProductClass.cs
* ����:		     ֣��
* ��������:     2010-4-25
* ��������:    ��Ʒ���

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
	/// **�������ƣ���Ʒ���ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbProductClass")]
	public class ProductClass: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private string _cnvcProductType = String.Empty;
		private string _cnvcProductClassName = String.Empty;
		private string _cnvcProductClassCode = String.Empty;
		private string _cnvcComments = String.Empty;
		private int _cnnDays;
		
		#endregion
		
		#region ���캯��




		public ProductClass():base()
		{
		}
		
		public ProductClass(DataRow row):base(row)
		{
		}
		
		public ProductClass(DataTable table):base(table)
		{
		}
		
		public ProductClass(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// ��Ʒ����
		/// </summary>
		[ColumnMapping("cnvcProductType",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductType
		{
			get {return _cnvcProductType;}
			set {_cnvcProductType = value;}
		}

		/// <summary>
		/// ��Ʒ�������
		/// </summary>
		[ColumnMapping("cnvcProductClassName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductClassName
		{
			get {return _cnvcProductClassName;}
			set {_cnvcProductClassName = value;}
		}

		/// <summary>
		/// ��Ʒ������
		/// </summary>
		[ColumnMapping("cnvcProductClassCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductClassCode
		{
			get {return _cnvcProductClassCode;}
			set {_cnvcProductClassCode = value;}
		}

		/// <summary>
		/// ����
		/// </summary>
		[ColumnMapping("cnvcComments",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcComments
		{
			get {return _cnvcComments;}
			set {_cnvcComments = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnnDays",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public int cnnDays
		{
			get {return _cnnDays;}
			set {_cnnDays = value;}
		}
		#endregion
	}	
}
