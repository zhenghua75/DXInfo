
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	GroupGoodsArgs.cs
* ����:	     ֣��
* ��������:    2008-10-23
* ��������:    �������Ʒ��Ӧ��

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ��������Ʒ��Ӧ���ѯ������
	/// </summary>
	public class GroupGoodsArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbGroupGoods";
				
		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public QueryConditionField cnvcProductType = new QueryConditionField("cnvcProductType");

		/// <summary>
		/// ��Ʒ���
		/// </summary>
		public QueryConditionField cnvcProductClass = new QueryConditionField("cnvcProductClass");

		/// <summary>
		/// ���������
		/// </summary>
		public QueryConditionField cnvcGroupCode = new QueryConditionField("cnvcGroupCode");

		/// <summary>
		/// ����
		/// </summary>
		public QueryConditionField cnvcComments = new QueryConditionField("cnvcComments");
	}	
}
