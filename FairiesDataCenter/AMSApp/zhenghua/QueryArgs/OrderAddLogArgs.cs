
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	OrderAddLogArgs.cs
* ����:	     ֣��
* ��������:    2008-10-7
* ��������:    �����ӵ���ˮ��

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ������ӵ���ˮ���ѯ������
	/// </summary>
	public class OrderAddLogArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbOrderAddLog";
				
		/// <summary>
		/// �ӵ���ˮ
		/// </summary>
		public QueryConditionField cnnAddSerialNo = new QueryConditionField("cnnAddSerialNo");

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
		/// �ӵ�����
		/// </summary>
		public QueryConditionField cnnAddCount = new QueryConditionField("cnnAddCount");

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
		/// �ӵ�����
		/// </summary>
		public QueryConditionField cnvcAddType = new QueryConditionField("cnvcAddType");

		/// <summary>
		/// �ӵ�״̬
		/// </summary>
		public QueryConditionField cnvcAddState = new QueryConditionField("cnvcAddState");

		/// <summary>
		/// �ӵ�˵��
		/// </summary>
		public QueryConditionField cnvcAddComments = new QueryConditionField("cnvcAddComments");

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
