
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:  	AssignDetail.cs
* ����:		     ֣��
* ��������:     2010-3-18
* ��������:    �ֻ���ˮϸ�ڱ�

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
	/// **�������ƣ��ֻ���ˮϸ�ڱ�ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbAssignDetail")]
	public class AssignDetail: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private decimal _cnnAssignSerialNo;
		private decimal _cnnOrderSerialNo;
		private decimal _cnnProduceSerialNo;
		private string _cnvcInvCode = String.Empty;
		private decimal _cnnOrderCount;
		private decimal _cnnAssignCount;
		private DateTime _cndMdate;
		private DateTime _cndExpDate;
		
		#endregion
		
		#region ���캯��




		public AssignDetail():base()
		{
		}
		
		public AssignDetail(DataRow row):base(row)
		{
		}
		
		public AssignDetail(DataTable table):base(table)
		{
		}
		
		public AssignDetail(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// �ֻ���ˮ
		/// </summary>
		[ColumnMapping("cnnAssignSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnAssignSerialNo
		{
			get {return _cnnAssignSerialNo;}
			set {_cnnAssignSerialNo = value;}
		}

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
		/// �л�����
		/// </summary>
		[ColumnMapping("cnnOrderCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnOrderCount
		{
			get {return _cnnOrderCount;}
			set {_cnnOrderCount = value;}
		}

		/// <summary>
		/// ʵ������
		/// </summary>
		[ColumnMapping("cnnAssignCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnAssignCount
		{
			get {return _cnnAssignCount;}
			set {_cnnAssignCount = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
		[ColumnMapping("cndMdate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public DateTime cndMdate
		{
			get {return _cndMdate;}
			set {_cndMdate = value;}
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
		#endregion
	}	
}
