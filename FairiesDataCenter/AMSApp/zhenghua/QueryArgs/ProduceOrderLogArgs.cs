
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	ProduceOrderLogArgs.cs
* ����:	     ֣��
* ��������:    2008-10-10
* ��������:    ������ˮ������ˮ��Ӧ��

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ�������ˮ������ˮ��Ӧ���ѯ������
	/// </summary>
	public class ProduceOrderLogArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbProduceOrderLog";
				
		/// <summary>
		/// ������ˮ
		/// </summary>
		public QueryConditionField cnvcProduceSerialNo = new QueryConditionField("cnvcProduceSerialNo");

		/// <summary>
		/// ������ˮ
		/// </summary>
		public QueryConditionField cnnOrderSerialNo = new QueryConditionField("cnnOrderSerialNo");

		/// <summary>
		/// �µ�����
		/// </summary>
		public QueryConditionField cnvcType = new QueryConditionField("cnvcType");
	}	
}
