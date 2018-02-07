
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:  	ProduceDetail.cs
* ����:		     ֣��
* ��������:     2010-3-19
* ��������:    �����ƻ���Ʒϸ�ڱ�

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
	/// **�������ƣ������ƻ���Ʒϸ�ڱ�ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbProduceDetail")]
	public class ProduceDetail: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private decimal _cnnProduceSerialNo;
		private string _cnvcInvCode = String.Empty;
		private decimal _cnnProduceCount;
		private decimal _cnnOrderCount;
		
		#endregion
		
		#region ���캯��




		public ProduceDetail():base()
		{
		}
		
		public ProduceDetail(DataRow row):base(row)
		{
		}
		
		public ProduceDetail(DataTable table):base(table)
		{
		}
		
		public ProduceDetail(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// ������ˮ
		/// </summary>
		[ColumnMapping("cnnProduceSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnProduceSerialNo
		{
			get {return _cnnProduceSerialNo;}
			set {_cnnProduceSerialNo = value;}
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
		/// �ƻ���������
		/// </summary>
		[ColumnMapping("cnnProduceCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnProduceCount
		{
			get {return _cnnProduceCount;}
			set {_cnnProduceCount = value;}
		}

		/// <summary>
		/// ��������
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
