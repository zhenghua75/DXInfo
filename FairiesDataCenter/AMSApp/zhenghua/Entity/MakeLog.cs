
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:  	MakeLog.cs
* ����:		     ֣��
* ��������:     2010-3-20
* ��������:    �����ˮ��

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
	/// **�������ƣ������ˮ��ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbMakeLog")]
	public class MakeLog: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private decimal _cnnProduceSerialNo;
		private decimal _cnnMakeSerialNo;
		private string _cnvcOperID = String.Empty;
		private DateTime _cndOperDate;
		
		#endregion
		
		#region ���캯��




		public MakeLog():base()
		{
		}
		
		public MakeLog(DataRow row):base(row)
		{
		}
		
		public MakeLog(DataTable table):base(table)
		{
		}
		
		public MakeLog(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// ������ˮ
		/// </summary>
		[ColumnMapping("cnnProduceSerialNo",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnProduceSerialNo
		{
			get {return _cnnProduceSerialNo;}
			set {_cnnProduceSerialNo = value;}
		}

		/// <summary>
		/// ������ˮ
		/// </summary>
		[ColumnMapping("cnnMakeSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnMakeSerialNo
		{
			get {return _cnnMakeSerialNo;}
			set {_cnnMakeSerialNo = value;}
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
		#endregion
	}	
}
