
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	OrderSerialNo.cs
* ����:	     ֣��
* ��������:    2008-10-4
* ��������:    ������ˮ���ɱ�

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
	/// **�������ƣ�������ˮ���ɱ�ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbOrderSerialNo")]
	public class OrderSerialNo: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private decimal _cnnSerialNo;
		private string _cnvcFill = String.Empty;
		
		#endregion
		
		#region ���캯��




		public OrderSerialNo():base()
		{
		}
		
		public OrderSerialNo(DataRow row):base(row)
		{
		}
		
		public OrderSerialNo(DataTable table):base(table)
		{
		}
		
		public OrderSerialNo(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// ������ˮ
		/// </summary>
		[ColumnMapping("cnnSerialNo",IsPrimaryKey=true,IsIdentity=true,IsVersionNumber=false)]
		public decimal cnnSerialNo
		{
			get {return _cnnSerialNo;}
			set {_cnnSerialNo = value;}
		}

		/// <summary>
		/// ���
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
