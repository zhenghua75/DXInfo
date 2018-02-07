
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	ProduceOrderLog.cs
* ����:	     ֣��
* ��������:    2008-10-10
* ��������:    ������ˮ������ˮ��Ӧ��

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
	/// **�������ƣ�������ˮ������ˮ��Ӧ��ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbProduceOrderLog")]
	public class ProduceOrderLog: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private decimal _cnnProduceSerialNo;
		private decimal _cnnOrderSerialNo;
		private string _cnvcType = String.Empty;
		
		#endregion
		
		#region ���캯��




		public ProduceOrderLog():base()
		{
		}
		
		public ProduceOrderLog(DataRow row):base(row)
		{
		}
		
		public ProduceOrderLog(DataTable table):base(table)
		{
		}
		
		public ProduceOrderLog(string  strXML):base(strXML)
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
		/// ������ˮ
		/// </summary>
		[ColumnMapping("cnnOrderSerialNo",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnOrderSerialNo
		{
			get {return _cnnOrderSerialNo;}
			set {_cnnOrderSerialNo = value;}
		}

		/// <summary>
		/// �µ�����
		/// </summary>
		[ColumnMapping("cnvcType",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcType
		{
			get {return _cnvcType;}
			set {_cnvcType = value;}
		}
		#endregion
	}	
}
