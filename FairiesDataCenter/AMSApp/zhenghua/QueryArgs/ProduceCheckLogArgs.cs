
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	ProduceCheckLogArgs.cs
* ����:	     ֣��
* ��������:    2008-10-28
* ��������:    �����̵���ˮ��

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ������̵���ˮ���ѯ������
	/// </summary>
	public class ProduceCheckLogArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbProduceCheckLog";
				
		/// <summary>
		/// ������ˮ
		/// </summary>
		public QueryConditionField cnnProduceSerialNo = new QueryConditionField("cnnProduceSerialNo");

		/// <summary>
		/// ������λ
		/// </summary>
		public QueryConditionField cnvcProduceDeptID = new QueryConditionField("cnvcProduceDeptID");

		/// <summary>
		/// ����Ա
		/// </summary>
		public QueryConditionField cnvcOperID = new QueryConditionField("cnvcOperID");

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public QueryConditionField cndOperDate = new QueryConditionField("cndOperDate");

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public QueryConditionField cnvcName = new QueryConditionField("cnvcName");

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public QueryConditionField cnvcCode = new QueryConditionField("cnvcCode");

		/// <summary>
		/// ��λ
		/// </summary>
		public QueryConditionField cnvcUnit = new QueryConditionField("cnvcUnit");

		/// <summary>
		/// ������
		/// </summary>
		public QueryConditionField cnnOrderCount = new QueryConditionField("cnnOrderCount");

		/// <summary>
		/// �ӵ���
		/// </summary>
		public QueryConditionField cnnAddCount = new QueryConditionField("cnnAddCount");

		/// <summary>
		/// ������
		/// </summary>
		public QueryConditionField cnnReduceCount = new QueryConditionField("cnnReduceCount");

		/// <summary>
		/// ������
		/// </summary>
		public QueryConditionField cnnProduceCount = new QueryConditionField("cnnProduceCount");

		/// <summary>
		/// �̵���
		/// </summary>
		public QueryConditionField cnnCheckCount = new QueryConditionField("cnnCheckCount");
	}	
}
