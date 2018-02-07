
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	GroupGoods.cs
* ����:	     ֣��
* ��������:    2008-10-23
* ��������:    �������Ʒ��Ӧ��

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
	/// **�������ƣ��������Ʒ��Ӧ��ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbGroupGoods")]
	public class GroupGoods: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private string _cnvcProductType = String.Empty;
		private string _cnvcProductClass = String.Empty;
		private string _cnvcGroupCode = String.Empty;
		private string _cnvcComments = String.Empty;
		
		#endregion
		
		#region ���캯��




		public GroupGoods():base()
		{
		}
		
		public GroupGoods(DataRow row):base(row)
		{
		}
		
		public GroupGoods(DataTable table):base(table)
		{
		}
		
		public GroupGoods(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// ��Ʒ����
		/// </summary>
		[ColumnMapping("cnvcProductType",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductType
		{
			get {return _cnvcProductType;}
			set {_cnvcProductType = value;}
		}

		/// <summary>
		/// ��Ʒ���
		/// </summary>
		[ColumnMapping("cnvcProductClass",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcProductClass
		{
			get {return _cnvcProductClass;}
			set {_cnvcProductClass = value;}
		}

		/// <summary>
		/// ���������
		/// </summary>
		[ColumnMapping("cnvcGroupCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcGroupCode
		{
			get {return _cnvcGroupCode;}
			set {_cnvcGroupCode = value;}
		}

		/// <summary>
		/// ����
		/// </summary>
		[ColumnMapping("cnvcComments",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcComments
		{
			get {return _cnvcComments;}
			set {_cnvcComments = value;}
		}
		#endregion
	}	
}
