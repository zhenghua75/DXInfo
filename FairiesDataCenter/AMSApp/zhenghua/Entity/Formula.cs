
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	Formula.cs
* ����:	     ֣��
* ��������:    2008-9-29
* ��������:    �䷽��

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
	/// **�������ƣ��䷽��ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbFormula")]
	public class Formula: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private string _cnvcProductCode = String.Empty;
		private string _cnvcProductName = String.Empty;
		private string _cnvcProductType = String.Empty;
		private string _cnvcProductClass = String.Empty;
		private byte[] _cnbProductImage;
		private decimal _cnnMaterialCostSum;
		private decimal _cnnPackingCostSum;
		private decimal _cnnCostSum;
		private string _cnvcUnit = String.Empty;
		private decimal _cnnPortionCount;
		private string _cnvcPortionUnit = String.Empty;
		private string _cnvcFeel = String.Empty;
		private string _cnvcOrganise = String.Empty;
		private string _cnvcColor = String.Empty;
		private string _cnvcTaste = String.Empty;
		
		#endregion
		
		#region ���캯��




		public Formula():base()
		{
		}
		
		public Formula(DataRow row):base(row)
		{
		}
		
		public Formula(DataTable table):base(table)
		{
		}
		
		public Formula(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
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
		/// ��Ʒ����
		/// </summary>
		[ColumnMapping("cnvcProductType",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductType
		{
			get {return _cnvcProductType;}
			set {_cnvcProductType = value;}
		}

		/// <summary>
		/// ��Ʒ���
		/// </summary>
		[ColumnMapping("cnvcProductClass",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductClass
		{
			get {return _cnvcProductClass;}
			set {_cnvcProductClass = value;}
		}

		/// <summary>
		/// ��ƷͼƬ
		/// </summary>
		[ColumnMapping("cnbProductImage",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public byte[] cnbProductImage
		{
			get {return _cnbProductImage;}
			set {_cnbProductImage = value;}
		}

		/// <summary>
		/// ԭ�ϳɱ��ϼ�
		/// </summary>
		[ColumnMapping("cnnMaterialCostSum",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnMaterialCostSum
		{
			get {return _cnnMaterialCostSum;}
			set {_cnnMaterialCostSum = value;}
		}

		/// <summary>
		/// ���ϳɱ��ϼ�
		/// </summary>
		[ColumnMapping("cnnPackingCostSum",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnPackingCostSum
		{
			get {return _cnnPackingCostSum;}
			set {_cnnPackingCostSum = value;}
		}

		/// <summary>
		/// �ɱ��ϼ�
		/// </summary>
		[ColumnMapping("cnnCostSum",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnCostSum
		{
			get {return _cnnCostSum;}
			set {_cnnCostSum = value;}
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
		/// �ݲ�����
		/// </summary>
		[ColumnMapping("cnnPortionCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnPortionCount
		{
			get {return _cnnPortionCount;}
			set {_cnnPortionCount = value;}
		}

		/// <summary>
		/// �ݲ���λ
		/// </summary>
		[ColumnMapping("cnvcPortionUnit",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPortionUnit
		{
			get {return _cnvcPortionUnit;}
			set {_cnvcPortionUnit = value;}
		}

		/// <summary>
		/// �ڸ�
		/// </summary>
		[ColumnMapping("cnvcFeel",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcFeel
		{
			get {return _cnvcFeel;}
			set {_cnvcFeel = value;}
		}

		/// <summary>
		/// ��֯
		/// </summary>
		[ColumnMapping("cnvcOrganise",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcOrganise
		{
			get {return _cnvcOrganise;}
			set {_cnvcOrganise = value;}
		}

		/// <summary>
		/// ��ɫ
		/// </summary>
		[ColumnMapping("cnvcColor",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcColor
		{
			get {return _cnvcColor;}
			set {_cnvcColor = value;}
		}

		/// <summary>
		/// ��ζ
		/// </summary>
		[ColumnMapping("cnvcTaste",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcTaste
		{
			get {return _cnvcTaste;}
			set {_cnvcTaste = value;}
		}
		#endregion
	}	
}
