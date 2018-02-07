
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	OrderBookArgs.cs
* ����:	     ֣��
* ��������:    2008-10-4
* ��������:    ������

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ��������ѯ������
	/// </summary>
	public class OrderBookArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbOrderBook";
				
		/// <summary>
		/// ������ˮ
		/// </summary>
		public QueryConditionField cnnOrderSerialNo = new QueryConditionField("cnnOrderSerialNo");

		/// <summary>
		/// ��������
		/// </summary>
		public QueryConditionField cnvcOrderDeptID = new QueryConditionField("cnvcOrderDeptID");

		/// <summary>
		/// ������λ
		/// </summary>
		public QueryConditionField cnvcProduceDeptID = new QueryConditionField("cnvcProduceDeptID");

		/// <summary>
		/// ��������
		/// </summary>
		public QueryConditionField cnvcOrderType = new QueryConditionField("cnvcOrderType");

		/// <summary>
		/// ����˵��
		/// </summary>
		public QueryConditionField cnvcOrderComments = new QueryConditionField("cnvcOrderComments");

		/// <summary>
		/// ��������Ա
		/// </summary>
		public QueryConditionField cnvcOrderOperID = new QueryConditionField("cnvcOrderOperID");

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public QueryConditionField cndOrderDate = new QueryConditionField("cndOrderDate");

		/// <summary>
		/// ��������
		/// </summary>
		public QueryConditionField cndShipDate = new QueryConditionField("cndShipDate");

		/// <summary>
		/// �ͻ�������λ
		/// </summary>
		public QueryConditionField cnvcCustomName = new QueryConditionField("cnvcCustomName");

		/// <summary>
		/// �ͻ���ַ
		/// </summary>
		public QueryConditionField cnvcShipAddress = new QueryConditionField("cnvcShipAddress");

		/// <summary>
		/// ��ϵ�绰
		/// </summary>
		public QueryConditionField cnvcLinkPhone = new QueryConditionField("cnvcLinkPhone");

		/// <summary>
		/// ����Ҫ�󵽻�ʱ��
		/// </summary>
		public QueryConditionField cndArrivedDate = new QueryConditionField("cndArrivedDate");

		/// <summary>
		/// ����״̬
		/// </summary>
		public QueryConditionField cnvcOrderState = new QueryConditionField("cnvcOrderState");
	}	
}
