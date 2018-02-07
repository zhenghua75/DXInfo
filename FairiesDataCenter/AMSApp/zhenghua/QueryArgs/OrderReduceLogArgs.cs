
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	OrderReduceLogArgs.cs
* ����:	     ֣��
* ��������:    2008-10-7
* ��������:    ����������ˮ��

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ�����������ˮ���ѯ������
	/// </summary>
	public class OrderReduceLogArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbOrderReduceLog";
				
		/// <summary>
		/// ������ˮ
		/// </summary>
		public QueryConditionField cnnReduceSerialNo = new QueryConditionField("cnnReduceSerialNo");

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
		/// ��������
		/// </summary>
		public QueryConditionField cnnReduceCount = new QueryConditionField("cnnReduceCount");

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
		/// ��������
		/// </summary>
		public QueryConditionField cnvcReduceType = new QueryConditionField("cnvcReduceType");

		/// <summary>
		/// ����״̬
		/// </summary>
		public QueryConditionField cnvcReduceState = new QueryConditionField("cnvcReduceState");

		/// <summary>
		/// ����˵��
		/// </summary>
		public QueryConditionField cnvcReduceComments = new QueryConditionField("cnvcReduceComments");

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
