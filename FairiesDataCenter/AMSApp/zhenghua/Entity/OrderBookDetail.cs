
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:  	OrderBookDetail.cs
* ����:		     ֣��
* ��������:     2010-3-17
* ��������:    �����ӱ�

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
	/// **�������ƣ������ӱ�ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbOrderBookDetail")]
	public class OrderBookDetail: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private decimal _cnnOrderSerialNo;
		private string _cnvcInvCode = String.Empty;
		private decimal _cnnOrderCount;
		
		#endregion
		
		#region ���캯��




		public OrderBookDetail():base()
		{
		}
		
		public OrderBookDetail(DataRow row):base(row)
		{
		}
		
		public OrderBookDetail(DataTable table):base(table)
		{
		}
		
		public OrderBookDetail(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// ������ˮ
		/// </summary>
		[ColumnMapping("cnnOrderSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnOrderSerialNo
		{
			get {return _cnnOrderSerialNo;}
			set {_cnnOrderSerialNo = value;}
		}

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		[ColumnMapping("cnvcInvCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcInvCode
		{
			get {return _cnvcInvCode;}
			set {_cnvcInvCode = value;}
		}

		/// <summary>
		/// ������
		/// </summary>
		[ColumnMapping("cnnOrderCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnOrderCount
		{
			get {return _cnnOrderCount;}
			set {_cnnOrderCount = value;}
		}
		#endregion
	}	
}
