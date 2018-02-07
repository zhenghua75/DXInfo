
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	MakeLogArgs.cs
* ����:	     ֣��
* ��������:    2008-10-12
* ��������:    �����ˮ��

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ������ˮ���ѯ������
	/// </summary>
	public class MakeLogArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbMakeLog";
				
		/// <summary>
		/// ������ˮ
		/// </summary>
		public QueryConditionField cnnProduceSerialNo = new QueryConditionField("cnnProduceSerialNo");

		/// <summary>
		/// ������ˮ
		/// </summary>
		public QueryConditionField cnnMakeSerialNo = new QueryConditionField("cnnMakeSerialNo");

		/// <summary>
		/// ������
		/// </summary>
		public QueryConditionField cnvcGroup = new QueryConditionField("cnvcGroup");

		/// <summary>
		/// �������
		/// </summary>
		public QueryConditionField cnvcMakeName = new QueryConditionField("cnvcMakeName");

		/// <summary>
		/// ��������
		/// </summary>
		public QueryConditionField cnvcMakeType = new QueryConditionField("cnvcMakeType");

		/// <summary>
		/// ���
		/// </summary>
		public QueryConditionField cnvcClass = new QueryConditionField("cnvcClass");

		/// <summary>
		/// ��������
		/// </summary>
		public QueryConditionField cnvcInChargeOperID = new QueryConditionField("cnvcInChargeOperID");

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
