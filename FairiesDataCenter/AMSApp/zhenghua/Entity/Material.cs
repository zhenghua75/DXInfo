
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:  	Material.cs
* ����:		     ֣��
* ��������:     2010-4-4
* ��������:    �䷽��

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
	/// **�������ƣ��䷽��ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbMaterial")]
	public class Material: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private string _cnvcMaterialCode = String.Empty;
		private string _cnvcMaterialName = String.Empty;
		private string _cnvcLeastUnit = String.Empty;
		private decimal _cnnConversion;
		private string _cnvcUnit = String.Empty;
		private string _cnvcStandardUnit = String.Empty;
		private decimal _cnnStatdardCount;
		private decimal _cnnPrice;
		private string _cnvcProductType = String.Empty;
		private string _cnvcProductClass = String.Empty;
		
		#endregion
		
		#region ���캯��




		public Material():base()
		{
		}
		
		public Material(DataRow row):base(row)
		{
		}
		
		public Material(DataTable table):base(table)
		{
		}
		
		public Material(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// ԭ�ϱ���
		/// </summary>
		[ColumnMapping("cnvcMaterialCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcMaterialCode
		{
			get {return _cnvcMaterialCode;}
			set {_cnvcMaterialCode = value;}
		}

		/// <summary>
		/// ԭ������
		/// </summary>
		[ColumnMapping("cnvcMaterialName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcMaterialName
		{
			get {return _cnvcMaterialName;}
			set {_cnvcMaterialName = value;}
		}

		/// <summary>
		/// ��С������λ
		/// </summary>
		[ColumnMapping("cnvcLeastUnit",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcLeastUnit
		{
			get {return _cnvcLeastUnit;}
			set {_cnvcLeastUnit = value;}
		}

		/// <summary>
		/// �����ϵ
		/// </summary>
		[ColumnMapping("cnnConversion",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnConversion
		{
			get {return _cnnConversion;}
			set {_cnnConversion = value;}
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
		/// ���λ
		/// </summary>
		[ColumnMapping("cnvcStandardUnit",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcStandardUnit
		{
			get {return _cnvcStandardUnit;}
			set {_cnvcStandardUnit = value;}
		}

		/// <summary>
		/// �������
		/// </summary>
		[ColumnMapping("cnnStatdardCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnStatdardCount
		{
			get {return _cnnStatdardCount;}
			set {_cnnStatdardCount = value;}
		}

		/// <summary>
		/// ��С������λ�۸�
		/// </summary>
		[ColumnMapping("cnnPrice",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnPrice
		{
			get {return _cnnPrice;}
			set {_cnnPrice = value;}
		}

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
		/// 
		/// </summary>
		[ColumnMapping("cnvcProductClass",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductClass
		{
			get {return _cnvcProductClass;}
			set {_cnvcProductClass = value;}
		}
		#endregion
	}	
}
