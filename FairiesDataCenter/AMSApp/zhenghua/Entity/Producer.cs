
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:  	Producer.cs
* ����:		     ֣��
* ��������:     2010-4-16
* ��������:    ����Ա

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
	/// **�������ƣ�����Աʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbProducer")]
	public class Producer: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private int _cnnProducerID;
		private string _cnvcProducerName = String.Empty;
		
		#endregion
		
		#region ���캯��




		public Producer():base()
		{
		}
		
		public Producer(DataRow row):base(row)
		{
		}
		
		public Producer(DataTable table):base(table)
		{
		}
		
		public Producer(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// ����Ա����
		/// </summary>
		[ColumnMapping("cnnProducerID",IsPrimaryKey=true,IsIdentity=true,IsVersionNumber=false)]
		public int cnnProducerID
		{
			get {return _cnnProducerID;}
			set {_cnnProducerID = value;}
		}

		/// <summary>
		/// ����Ա����
		/// </summary>
		[ColumnMapping("cnvcProducerName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProducerName
		{
			get {return _cnvcProducerName;}
			set {_cnvcProducerName = value;}
		}
		#endregion
	}	
}
