
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:  	ProviderStock.cs
* ����:		     ֣��
* ��������:     2010-3-7
* ��������:    ��Ӧ�̴����

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
	/// **�������ƣ���Ӧ�̴����ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbProviderStock")]
	public class ProviderStock: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private string _cnvcPrvdCode = String.Empty;
		private string _cnvcGoodsCode = String.Empty;
		private string _cnvcGoodsName = String.Empty;
		private decimal _cnnGoodsPrice;
		private string _cnvcInvalidFlag = String.Empty;
		private DateTime _cndInsertDate;
		
		#endregion
		
		#region ���캯��




		public ProviderStock():base()
		{
		}
		
		public ProviderStock(DataRow row):base(row)
		{
		}
		
		public ProviderStock(DataTable table):base(table)
		{
		}
		
		public ProviderStock(string  strXML):base(strXML)
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
		/// ��Ӧ��Ʒ����
		/// </summary>
		[ColumnMapping("cnvcGoodsCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcGoodsCode
		{
			get {return _cnvcGoodsCode;}
			set {_cnvcGoodsCode = value;}
		}

		/// <summary>
		/// ��Ӧ��Ʒ����
		/// </summary>
		[ColumnMapping("cnvcGoodsName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcGoodsName
		{
			get {return _cnvcGoodsName;}
			set {_cnvcGoodsName = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
		[ColumnMapping("cnnGoodsPrice",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnGoodsPrice
		{
			get {return _cnnGoodsPrice;}
			set {_cnnGoodsPrice = value;}
		}

		/// <summary>
		/// ��Ч��־
		/// </summary>
		[ColumnMapping("cnvcInvalidFlag",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcInvalidFlag
		{
			get {return _cnvcInvalidFlag;}
			set {_cnvcInvalidFlag = value;}
		}

		/// <summary>
		/// ����ʱ��
		/// </summary>
		[ColumnMapping("cndInsertDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndInsertDate
		{
			get {return _cndInsertDate;}
			set {_cndInsertDate = value;}
		}
		#endregion
	}	
}
