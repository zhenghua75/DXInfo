
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	ProduceLogArgs.cs
* ����:	     ֣��
* ��������:    2008-10-10
* ��������:    �����ƻ���ˮ��

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ������ƻ���ˮ���ѯ������
	/// </summary>
	public class ProduceLogArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbProduceLog";
				
		/// <summary>
		/// ������ˮ
		/// </summary>
		public QueryConditionField cnnProduceSerialNo = new QueryConditionField("cnnProduceSerialNo");

		/// <summary>
		/// ������λ
		/// </summary>
		public QueryConditionField cnvcProduceDeptID = new QueryConditionField("cnvcProduceDeptID");

		/// <summary>
		/// ��������
		/// </summary>
		public QueryConditionField cndProduceDate = new QueryConditionField("cndProduceDate");

		/// <summary>
		/// ������ʼ����
		/// </summary>
		public QueryConditionField cndShipBeginDate = new QueryConditionField("cndShipBeginDate");

		/// <summary>
		/// ������������
		/// </summary>
		public QueryConditionField cndShipEndDate = new QueryConditionField("cndShipEndDate");

		/// <summary>
		/// �����ƻ�״̬
		/// </summary>
		public QueryConditionField cnvcProduceState = new QueryConditionField("cnvcProduceState");

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
