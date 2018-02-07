
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	GoodsArgs.cs
* ����:	     ֣��
* ��������:    2008-10-10
* ��������:    ��Ʒ��

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ���Ʒ���ѯ������
	/// </summary>
	public class GoodsArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbGoods";
				
		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField vcGoodsID = new QueryConditionField("vcGoodsID");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField vcGoodsName = new QueryConditionField("vcGoodsName");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField vcSpell = new QueryConditionField("vcSpell");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField nPrice = new QueryConditionField("nPrice");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField nRate = new QueryConditionField("nRate");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField iIgValue = new QueryConditionField("iIgValue");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField cNewFlag = new QueryConditionField("cNewFlag");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField vcComments = new QueryConditionField("vcComments");
	}	
}
