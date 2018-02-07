
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:  	BillOfMaterials.cs
* ����:		     ֣��
* ��������:     2010-3-9
* ��������:    �����嵥

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
	/// **�������ƣ������嵥ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbBillOfMaterials")]
	public class BillOfMaterials: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private string _cnvcPartInvCode = String.Empty;
		private string _cnvcComponentInvCode = String.Empty;
		private decimal _cnnBaseQtyN;
		private decimal _cnnBaseQtyD;
		
		#endregion
		
		#region ���캯��




		public BillOfMaterials():base()
		{
		}
		
		public BillOfMaterials(DataRow row):base(row)
		{
		}
		
		public BillOfMaterials(DataTable table):base(table)
		{
		}
		
		public BillOfMaterials(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// ĸ������
		/// </summary>
		[ColumnMapping("cnvcPartInvCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcPartInvCode
		{
			get {return _cnvcPartInvCode;}
			set {_cnvcPartInvCode = value;}
		}

		/// <summary>
		/// �Ӽ�����
		/// </summary>
		[ColumnMapping("cnvcComponentInvCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcComponentInvCode
		{
			get {return _cnvcComponentInvCode;}
			set {_cnvcComponentInvCode = value;}
		}

		/// <summary>
		/// ������������
		/// </summary>
		[ColumnMapping("cnnBaseQtyN",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnBaseQtyN
		{
			get {return _cnnBaseQtyN;}
			set {_cnnBaseQtyN = value;}
		}

		/// <summary>
		/// ����������ĸ
		/// </summary>
		[ColumnMapping("cnnBaseQtyD",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnBaseQtyD
		{
			get {return _cnnBaseQtyD;}
			set {_cnnBaseQtyD = value;}
		}
		#endregion
	}	
}
