
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	Goods.cs
* ����:	     ֣��
* ��������:    2008-10-10
* ��������:    ��Ʒ��

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
	/// **�������ƣ���Ʒ��ʵ����
	/// </summary>
	[Serializable]
	[TableMapping("tbGoods")]
	public class Goods: EntityObjectBase
	{
		#region ���ݱ����ɱ���



		
		
		private string _vcGoodsID = String.Empty;
		private string _vcGoodsName = String.Empty;
		private string _vcSpell = String.Empty;
		private decimal _nPrice;
		private decimal _nRate;
		private int _iIgValue;
		private string _cNewFlag = String.Empty;
		private string _vcComments = String.Empty;
		
		#endregion
		
		#region ���캯��




		public Goods():base()
		{
		}
		
		public Goods(DataRow row):base(row)
		{
		}
		
		public Goods(DataTable table):base(table)
		{
		}
		
		public Goods(string  strXML):base(strXML)
		{
		}
		#endregion
		
		#region ϵͳ��������




				
		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("vcGoodsID",IsPrimaryKey=true,IsIdentity=false,IsVersionNumber=false)]
		public string vcGoodsID
		{
			get {return _vcGoodsID;}
			set {_vcGoodsID = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("vcGoodsName",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string vcGoodsName
		{
			get {return _vcGoodsName;}
			set {_vcGoodsName = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("vcSpell",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string vcSpell
		{
			get {return _vcSpell;}
			set {_vcSpell = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("nPrice",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal nPrice
		{
			get {return _nPrice;}
			set {_nPrice = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("nRate",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public decimal nRate
		{
			get {return _nRate;}
			set {_nRate = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("iIgValue",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public int iIgValue
		{
			get {return _iIgValue;}
			set {_iIgValue = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("cNewFlag",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string cNewFlag
		{
			get {return _cNewFlag;}
			set {_cNewFlag = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		[ColumnMapping("vcComments",IsPrimaryKey=false,IsIdentity=false,IsVersionNumber=false)]
		public string vcComments
		{
			get {return _vcComments;}
			set {_vcComments = value;}
		}
		#endregion
	}	
}
