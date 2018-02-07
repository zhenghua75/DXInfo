
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:  	MakeDetail.cs
* ����:		     ֣��
* ��������:     2010-4-4
* ��������:    ���������Ʒϸ�ڱ�

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
	/// **�������ƣ����������Ʒϸ�ڱ�ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbMakeDetail")]
	public class MakeDetail: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private decimal _cnnMakeSerialNo;
		private string _cnvcInvCode = String.Empty;
		private decimal _cnnMakeCount;
		private decimal _cnnCount;
		private decimal _cnnAdjustCount;
		private decimal _cnnStCount;
		private bool _cnbCollar;
		private decimal _cnnCollarCount;
		
		#endregion
		
		#region ���캯��




		public MakeDetail():base()
		{
		}
		
		public MakeDetail(DataRow row):base(row)
		{
		}
		
		public MakeDetail(DataTable table):base(table)
		{
		}
		
		public MakeDetail(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
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
		/// ��Ʒ����
		/// </summary>
		[ColumnMapping("cnvcInvCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcInvCode
		{
			get {return _cnvcInvCode;}
			set {_cnvcInvCode = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
		[ColumnMapping("cnnMakeCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnMakeCount
		{
			get {return _cnnMakeCount;}
			set {_cnnMakeCount = value;}
		}

		/// <summary>
		/// ʵ������
		/// </summary>
		[ColumnMapping("cnnCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnCount
		{
			get {return _cnnCount;}
			set {_cnnCount = value;}
		}

		/// <summary>
		/// ��������
		/// </summary>
		[ColumnMapping("cnnAdjustCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnAdjustCount
		{
			get {return _cnnAdjustCount;}
			set {_cnnAdjustCount = value;}
		}

		/// <summary>
		/// ���������
		/// </summary>
		[ColumnMapping("cnnStCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnStCount
		{
			get {return _cnnStCount;}
			set {_cnnStCount = value;}
		}

		/// <summary>
		/// �Ƿ�����
		/// </summary>
		[ColumnMapping("cnbCollar",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public bool cnbCollar
		{
			get {return _cnbCollar;}
			set {_cnbCollar = value;}
		}

		/// <summary>
		/// ����������
		/// </summary>
		[ColumnMapping("cnnCollarCount",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal cnnCollarCount
		{
			get {return _cnnCollarCount;}
			set {_cnnCollarCount = value;}
		}
		#endregion
	}	
}
