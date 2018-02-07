
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:  	PoStockSum.cs
* ����:		     ֣��
* ��������:     2010-3-7
* ��������:    �ɹ������ϼƱ�

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
	/// **�������ƣ��ɹ������ϼƱ�ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbPoStockSum")]
	public class PoStockSum: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private string _cnvcPoID = String.Empty;
		private string _cnvcGoodsCode = String.Empty;
		private string _cnvcGroupCode = String.Empty;
		private string _cnvcStockUnit = String.Empty;
		private decimal _cnnStockPrice;
		private decimal _cnnStockCountSum;
		private decimal _cnnStockFeeSum;
		private decimal _cnnArriveCount;
		private decimal _cnnArriveFee;
		private decimal _cnnFinallyRate;
		
		#endregion
		
		#region ���캯��




		public PoStockSum():base()
		{
		}
		
		public PoStockSum(DataRow row):base(row)
		{
		}
		
		public PoStockSum(DataTable table):base(table)
		{
		}
		
		public PoStockSum(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// �ɹ�������
		/// </summary>
		[ColumnMapping("cnvcPoID",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPoID
		{
			get {return _cnvcPoID;}
			set {_cnvcPoID = value;}
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
		/// ������λ�����
		/// </summary>
		[ColumnMapping("cnvcGroupCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcGroupCode
		{
			get {return _cnvcGroupCode;}
			set {_cnvcGroupCode = value;}
		}

		/// <summary>
		/// ��λ
		/// </summary>
		[ColumnMapping("cnvcStockUnit",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcStockUnit
		{
			get {return _cnvcStockUnit;}
			set {_cnvcStockUnit = value;}
		}

		/// <summary>
		/// ����
		/// </summary>
		[ColumnMapping("cnnStockPrice",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnStockPrice
		{
			get {return _cnnStockPrice;}
			set {_cnnStockPrice = value;}
		}

		/// <summary>
		/// �ۼƶ�������
		/// </summary>
		[ColumnMapping("cnnStockCountSum",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnStockCountSum
		{
			get {return _cnnStockCountSum;}
			set {_cnnStockCountSum = value;}
		}

		/// <summary>
		/// �ۼƶ������
		/// </summary>
		[ColumnMapping("cnnStockFeeSum",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnStockFeeSum
		{
			get {return _cnnStockFeeSum;}
			set {_cnnStockFeeSum = value;}
		}

		/// <summary>
		/// �ۼƵ�������
		/// </summary>
		[ColumnMapping("cnnArriveCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnArriveCount
		{
			get {return _cnnArriveCount;}
			set {_cnnArriveCount = value;}
		}

		/// <summary>
		/// �ۼƵ������
		/// </summary>
		[ColumnMapping("cnnArriveFee",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnArriveFee
		{
			get {return _cnnArriveFee;}
			set {_cnnArriveFee = value;}
		}

		/// <summary>
		/// ��ɱ���
		/// </summary>
		[ColumnMapping("cnnFinallyRate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnFinallyRate
		{
			get {return _cnnFinallyRate;}
			set {_cnnFinallyRate = value;}
		}
		#endregion
	}	
}
