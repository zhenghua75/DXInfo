
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	OrderBookDetailArgs.cs
* ����:	     ֣��
* ��������:    2008-10-4
* ��������:    ����ϸ�ڱ�

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ�����ϸ�ڱ��ѯ������
	/// </summary>
	public class OrderBookDetailArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbOrderBookDetail";
				
		/// <summary>
		/// ������ˮ
		/// </summary>
		public QueryConditionField cnnOrderSerialNo = new QueryConditionField("cnnOrderSerialNo");

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public QueryConditionField cnvcProductCode = new QueryConditionField("cnvcProductCode");

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public QueryConditionField cnvcProductName = new QueryConditionField("cnvcProductName");

		/// <summary>
		/// ������
		/// </summary>
		public QueryConditionField cnnOrderCount = new QueryConditionField("cnnOrderCount");

		/// <summary>
		/// ��λ
		/// </summary>
		public QueryConditionField cnvcUnit = new QueryConditionField("cnvcUnit");

		/// <summary>
		/// ����
		/// </summary>
		public QueryConditionField cnnPrice = new QueryConditionField("cnnPrice");

		/// <summary>
		/// �ϼ�
		/// </summary>
		public QueryConditionField cnnSum = new QueryConditionField("cnnSum");

		/// <summary>
		/// ����Ա
		/// </summary>
		public QueryConditionField cnvcOperID = new QueryConditionField("cnvcOperID");

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public QueryConditionField cndOperDate = new QueryConditionField("cndOperDate");
	}	
}
