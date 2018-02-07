
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	SerialNo.cs
* ����:	     ֣��
* ��������:    2009-7-24
* ��������:    ��Ʒ��������ˮ��

*                                                           Copyright(C) 2009 zhenghua
*************************************************************************************/
#region Import NameSpace
using System;
using System.Data;
using AMSApp.zhenghua.EntityBase;
#endregion

namespace AMSApp.zhenghua.Entity
{
	/// <summary>
	/// **�������ƣ���Ʒ��������ˮ��ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbSerialNo")]
	public class SerialNo: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private int _cnnSerialNo;
		private string _cnvcFill = String.Empty;
		
		#endregion
		
		#region ���캯��




		public SerialNo():base()
		{
		}
		
		public SerialNo(DataRow row):base(row)
		{
		}
		
		public SerialNo(DataTable table):base(table)
		{
		}
		
		public SerialNo(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnnSerialNo",IsPrimaryKey=true,IsIdentity=true,IsVersionNumber=false)]
		public int cnnSerialNo
		{
			get {return _cnnSerialNo;}
			set {_cnnSerialNo = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cnvcFill",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcFill
		{
			get {return _cnvcFill;}
			set {_cnvcFill = value;}
		}
		#endregion
	}	
}
