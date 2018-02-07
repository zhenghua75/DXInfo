
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:  	PoStockDetail.cs
* ����:		     ֣��
* ��������:     2010-3-7
* ��������:    �ɹ������ӱ�

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
	/// **�������ƣ��ɹ������ӱ�ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbPoStockDetail")]
	public class PoStockDetail: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private string _cnvcPoID = String.Empty;
		private string _cnvcDeptID = String.Empty;
		private string _cnvcGoodsCode = String.Empty;
		private string _cnvcGroupCode = String.Empty;
		private string _cnvcStockUnit = String.Empty;
		private decimal _cnnStockPrice;
		private decimal _cnnStockCount;
		private decimal _cnnStockFee;
		private DateTime _cndArriveDate;
		private string _cnvcRowState = String.Empty;
		private string _cnvcCreater = String.Empty;
		private string _cnvcModer = String.Empty;
		private string _cnvcChecker = String.Empty;
		private DateTime _cndCreateDate;
		private DateTime _cndModDate;
		private DateTime _cndCheckDate;
		
		#endregion
		
		#region ���캯��




		public PoStockDetail():base()
		{
		}
		
		public PoStockDetail(DataRow row):base(row)
		{
		}
		
		public PoStockDetail(DataTable table):base(table)
		{
		}
		
		public PoStockDetail(string  strXML):base(strXML)
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
		/// �¶�����
		/// </summary>
		[ColumnMapping("cnvcDeptID",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcDeptID
		{
			get {return _cnvcDeptID;}
			set {_cnvcDeptID = value;}
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
		/// ��������
		/// </summary>
		[ColumnMapping("cnnStockCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnStockCount
		{
			get {return _cnnStockCount;}
			set {_cnnStockCount = value;}
		}

		/// <summary>
		/// �������
		/// </summary>
		[ColumnMapping("cnnStockFee",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnStockFee
		{
			get {return _cnnStockFee;}
			set {_cnnStockFee = value;}
		}

		/// <summary>
		/// �ƻ���������
		/// </summary>
		[ColumnMapping("cndArriveDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndArriveDate
		{
			get {return _cndArriveDate;}
			set {_cndArriveDate = value;}
		}

		/// <summary>
		/// �Ӷ�����״̬
		/// </summary>
		[ColumnMapping("cnvcRowState",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcRowState
		{
			get {return _cnvcRowState;}
			set {_cnvcRowState = value;}
		}

		/// <summary>
		/// �Ƶ���
		/// </summary>
		[ColumnMapping("cnvcCreater",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcCreater
		{
			get {return _cnvcCreater;}
			set {_cnvcCreater = value;}
		}

		/// <summary>
		/// �޸���
		/// </summary>
		[ColumnMapping("cnvcModer",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcModer
		{
			get {return _cnvcModer;}
			set {_cnvcModer = value;}
		}

		/// <summary>
		/// �����
		/// </summary>
		[ColumnMapping("cnvcChecker",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcChecker
		{
			get {return _cnvcChecker;}
			set {_cnvcChecker = value;}
		}

		/// <summary>
		/// �Ƶ�ʱ��
		/// </summary>
		[ColumnMapping("cndCreateDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndCreateDate
		{
			get {return _cndCreateDate;}
			set {_cndCreateDate = value;}
		}

		/// <summary>
		/// �޸�ʱ��
		/// </summary>
		[ColumnMapping("cndModDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndModDate
		{
			get {return _cndModDate;}
			set {_cndModDate = value;}
		}

		/// <summary>
		/// ���ʱ��
		/// </summary>
		[ColumnMapping("cndCheckDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndCheckDate
		{
			get {return _cndCheckDate;}
			set {_cndCheckDate = value;}
		}
		#endregion
	}	
}
