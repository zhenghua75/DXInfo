
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:  	ProduceCheckLog.cs
* ����:		     ֣��
* ��������:     2010-4-17
* ��������:    �����ж�

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
	/// **�������ƣ������ж�ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbProduceCheckLog")]
	public class ProduceCheckLog: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private decimal _cnnProduceSerialNo;
		private string _cnvcProduceDeptID = String.Empty;
		private string _cnvcOperID = String.Empty;
		private DateTime _cndOperDate;
		private string _cnvcInvCode = String.Empty;
		private decimal _cnnOrderCount;
		private decimal _cnnProduceCount;
		private decimal _cnnCheckCount;
		private decimal _cnnAssignCount;
		private bool _cnbInWh;
		private DateTime _cndExpDate;
		private DateTime _cndMDate;
		private int _cnnTeamID;
		private int _cnnProducerID;
		
		#endregion
		
		#region ���캯��




		public ProduceCheckLog():base()
		{
		}
		
		public ProduceCheckLog(DataRow row):base(row)
		{
		}
		
		public ProduceCheckLog(DataTable table):base(table)
		{
		}
		
		public ProduceCheckLog(string  strXML):base(strXML)
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

		/// <summary>
		/// ������
		/// </summary>
		[ColumnMapping("cnnProduceCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnProduceCount
		{
			get {return _cnnProduceCount;}
			set {_cnnProduceCount = value;}
		}

		/// <summary>
		/// �̵���
		/// </summary>
		[ColumnMapping("cnnCheckCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnCheckCount
		{
			get {return _cnnCheckCount;}
			set {_cnnCheckCount = value;}
		}

		/// <summary>
		/// �ֻ���
		/// </summary>
		[ColumnMapping("cnnAssignCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnAssignCount
		{
			get {return _cnnAssignCount;}
			set {_cnnAssignCount = value;}
		}

		/// <summary>
		/// �Ƿ�����
		/// </summary>
		[ColumnMapping("cnbInWh",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public bool cnbInWh
		{
			get {return _cnbInWh;}
			set {_cnbInWh = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
		[ColumnMapping("cndExpDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndExpDate
		{
			get {return _cndExpDate;}
			set {_cndExpDate = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
		[ColumnMapping("cndMDate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndMDate
		{
			get {return _cndMDate;}
			set {_cndMDate = value;}
		}

		/// <summary>
		/// ����Ա����
		/// </summary>
		[ColumnMapping("cnnTeamID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public int cnnTeamID
		{
			get {return _cnnTeamID;}
			set {_cnnTeamID = value;}
		}

		/// <summary>
		/// ���������
		/// </summary>
		[ColumnMapping("cnnProducerID",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public int cnnProducerID
		{
			get {return _cnnProducerID;}
			set {_cnnProducerID = value;}
		}
		#endregion
	}	
}
