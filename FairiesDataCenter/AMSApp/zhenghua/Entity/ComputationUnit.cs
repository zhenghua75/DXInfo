
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	ComputationUnit.cs
* ����:		     ֣��
* ��������:    2010-3-6
* ��������:    ������λ

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
	/// **�������ƣ�������λʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbComputationUnit")]
	public class ComputationUnit: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private string _cnvcComunitCode = String.Empty;
		private string _cnvcComUnitName = String.Empty;
		private string _cnvcGroupCode = String.Empty;
		private bool _cnbMainUnit;
		private float _cniChangRate;
		
		#endregion
		
		#region ���캯��




		public ComputationUnit():base()
		{
		}
		
		public ComputationUnit(DataRow row):base(row)
		{
		}
		
		public ComputationUnit(DataTable table):base(table)
		{
		}
		
		public ComputationUnit(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// ������λ����
		/// </summary>
		[ColumnMapping("cnvcComunitCode",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcComunitCode
		{
			get {return _cnvcComunitCode;}
			set {_cnvcComunitCode = value;}
		}

		/// <summary>
		/// ������λ����
		/// </summary>
		[ColumnMapping("cnvcComUnitName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcComUnitName
		{
			get {return _cnvcComUnitName;}
			set {_cnvcComUnitName = value;}
		}

		/// <summary>
		/// ������λ�����
		/// </summary>
		[ColumnMapping("cnvcGroupCode",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cnvcGroupCode
		{
			get {return _cnvcGroupCode;}
			set {_cnvcGroupCode = value;}
		}

		/// <summary>
		/// �Ƿ���������λ
		/// </summary>
		[ColumnMapping("cnbMainUnit",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public bool cnbMainUnit
		{
			get {return _cnbMainUnit;}
			set {_cnbMainUnit = value;}
		}

		/// <summary>
		/// ������
		/// </summary>
		[ColumnMapping("cniChangRate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public float cniChangRate
		{
			get {return _cniChangRate;}
			set {_cniChangRate = value;}
		}
		#endregion
	}	
}
