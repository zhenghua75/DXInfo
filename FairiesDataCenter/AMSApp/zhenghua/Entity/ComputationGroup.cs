
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	ComputationGroup.cs
* ����:		     ֣��
* ��������:    2010-3-6
* ��������:    ������λ��

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
	/// **�������ƣ�������λ��ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbComputationGroup")]
	public class ComputationGroup: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private string _cnvcGroupCode = String.Empty;
		private string _cnvcGroupName = String.Empty;
		
		#endregion
		
		#region ���캯��




		public ComputationGroup():base()
		{
		}
		
		public ComputationGroup(DataRow row):base(row)
		{
		}
		
		public ComputationGroup(DataTable table):base(table)
		{
		}
		
		public ComputationGroup(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// ������λ�����
		/// </summary>
		[ColumnMapping("cnvcGroupCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcGroupCode
		{
			get {return _cnvcGroupCode;}
			set {_cnvcGroupCode = value;}
		}

		/// <summary>
		/// ������λ������
		/// </summary>
		[ColumnMapping("cnvcGroupName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcGroupName
		{
			get {return _cnvcGroupName;}
			set {_cnvcGroupName = value;}
		}
		#endregion
	}	
}
