
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:  	ProduceLog.cs
* ����:		     ֣��
* ��������:     2010-4-11
* ��������:    �����ƻ�����

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
	/// **�������ƣ������ƻ�����ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbProduceLog")]
	public class ProduceLog: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private decimal _cnnProduceSerialNo;
		private string _cnvcProduceDeptID = String.Empty;
		private DateTime _cndProduceDate;
		private DateTime _cndShipBeginDate;
		private DateTime _cndShipEndDate;
		private string _cnvcProduceState = String.Empty;
		private string _cnvcOperID = String.Empty;
		private DateTime _cndOperDate;
		private bool _cnbSelf;
		
		#endregion
		
		#region ���캯��




		public ProduceLog():base()
		{
		}
		
		public ProduceLog(DataRow row):base(row)
		{
		}
		
		public ProduceLog(DataTable table):base(table)
		{
		}
		
		public ProduceLog(string  strXML):base(strXML)
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
		/// ������λ
		/// </summary>
		[ColumnMapping("cnvcProduceDeptID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProduceDeptID
		{
			get {return _cnvcProduceDeptID;}
			set {_cnvcProduceDeptID = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
		[ColumnMapping("cndProduceDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndProduceDate
		{
			get {return _cndProduceDate;}
			set {_cndProduceDate = value;}
		}

		/// <summary>
		/// ������ʼ����
		/// </summary>
		[ColumnMapping("cndShipBeginDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndShipBeginDate
		{
			get {return _cndShipBeginDate;}
			set {_cndShipBeginDate = value;}
		}

		/// <summary>
		/// ������������
		/// </summary>
		[ColumnMapping("cndShipEndDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndShipEndDate
		{
			get {return _cndShipEndDate;}
			set {_cndShipEndDate = value;}
		}

		/// <summary>
		/// �����ƻ�״̬
		/// </summary>
		[ColumnMapping("cnvcProduceState",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProduceState
		{
			get {return _cnvcProduceState;}
			set {_cnvcProduceState = value;}
		}

		/// <summary>
		/// ����Ա
		/// </summary>
		[ColumnMapping("cnvcOperID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcOperID
		{
			get {return _cnvcOperID;}
			set {_cnvcOperID = value;}
		}

		/// <summary>
		/// ����ʱ��
		/// </summary>
		[ColumnMapping("cndOperDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndOperDate
		{
			get {return _cndOperDate;}
			set {_cndOperDate = value;}
		}

		/// <summary>
		/// �Ƿ�������
		/// </summary>
		[ColumnMapping("cnbSelf",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public bool cnbSelf
		{
			get {return _cnbSelf;}
			set {_cnbSelf = value;}
		}
		#endregion
	}	
}
