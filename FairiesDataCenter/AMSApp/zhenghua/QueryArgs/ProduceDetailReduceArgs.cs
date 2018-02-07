
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	ProduceDetailReduceArgs.cs
* ����:	     ֣��
* ��������:    2008-10-12
* ��������:    �����ƻ���Ʒϸ�ڼ�����

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ������ƻ���Ʒϸ�ڼ������ѯ������
	/// </summary>
	public class ProduceDetailReduceArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbProduceDetailReduce";
				
		/// <summary>
		/// ������ˮ
		/// </summary>
		public QueryConditionField cnnProduceSerialNo = new QueryConditionField("cnnProduceSerialNo");

		/// <summary>
		/// ������ˮ
		/// </summary>
		public QueryConditionField cnnReduceSerialNo = new QueryConditionField("cnnReduceSerialNo");

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public QueryConditionField cnvcCode = new QueryConditionField("cnvcCode");

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public QueryConditionField cnvcName = new QueryConditionField("cnvcName");

		/// <summary>
		/// ��λ
		/// </summary>
		public QueryConditionField cnvcUnit = new QueryConditionField("cnvcUnit");

		/// <summary>
		/// ����
		/// </summary>
		public QueryConditionField cnnCount = new QueryConditionField("cnnCount");

		/// <summary>
		/// ״̬
		/// </summary>
		public QueryConditionField cnvcState = new QueryConditionField("cnvcState");
	}	
}
