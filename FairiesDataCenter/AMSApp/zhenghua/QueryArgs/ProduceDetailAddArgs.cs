
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	ProduceDetailAddArgs.cs
* ����:	     ֣��
* ��������:    2008-10-12
* ��������:    �����ƻ���Ʒϸ�ڼӵ���

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ������ƻ���Ʒϸ�ڼӵ����ѯ������
	/// </summary>
	public class ProduceDetailAddArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbProduceDetailAdd";
				
		/// <summary>
		/// ������ˮ
		/// </summary>
		public QueryConditionField cnnProduceSerialNo = new QueryConditionField("cnnProduceSerialNo");

		/// <summary>
		/// �ӵ���ˮ
		/// </summary>
		public QueryConditionField cnnAddSerialNo = new QueryConditionField("cnnAddSerialNo");

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
