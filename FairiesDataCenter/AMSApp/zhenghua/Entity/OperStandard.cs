
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	OperStandard.cs
* ����:	     ֣��
* ��������:    2008-10-3
* ��������:    ������׼��

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
	/// **�������ƣ�������׼��ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbOperStandard")]
	public class OperStandard: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private string _cnvcProductCode = String.Empty;
		private int _cnnSort;
		private string _cnvcStandard = String.Empty;
		private string _cnvcKey = String.Empty;
		
		#endregion
		
		#region ���캯��




		public OperStandard():base()
		{
		}
		
		public OperStandard(DataRow row):base(row)
		{
		}
		
		public OperStandard(DataTable table):base(table)
		{
		}
		
		public OperStandard(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// ��Ʒ����
		/// </summary>
		[ColumnMapping("cnvcProductCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductCode
		{
			get {return _cnvcProductCode;}
			set {_cnvcProductCode = value;}
		}

		/// <summary>
		/// �������
		/// </summary>
		[ColumnMapping("cnnSort",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public int cnnSort
		{
			get {return _cnnSort;}
			set {_cnnSort = value;}
		}

		/// <summary>
		/// ������׼
		/// </summary>
		[ColumnMapping("cnvcStandard",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcStandard
		{
			get {return _cnvcStandard;}
			set {_cnvcStandard = value;}
		}

		/// <summary>
		/// �ؼ����Ƶ�
		/// </summary>
		[ColumnMapping("cnvcKey",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcKey
		{
			get {return _cnvcKey;}
			set {_cnvcKey = value;}
		}
		#endregion
	}	
}
