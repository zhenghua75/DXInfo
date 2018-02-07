
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	AssignLogArgs.cs
* ����:	     ֣��
* ��������:    2008-10-13
* ��������:    �ֻ���ˮ��

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ��ֻ���ˮ���ѯ������
	/// </summary>
	public class AssignLogArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbAssignLog";
				
		/// <summary>
		/// �ֻ���ˮ
		/// </summary>
		public QueryConditionField cnnAssignSerialNo = new QueryConditionField("cnnAssignSerialNo");

		/// <summary>
		/// ������ˮ
		/// </summary>
		public QueryConditionField cnnOrderSerialNo = new QueryConditionField("cnnOrderSerialNo");

		/// <summary>
		/// ������λ
		/// </summary>
		public QueryConditionField cnvcShipDeptID = new QueryConditionField("cnvcShipDeptID");

		/// <summary>
		/// ������
		/// </summary>
		public QueryConditionField cnvcShipOperID = new QueryConditionField("cnvcShipOperID");

		/// <summary>
		/// �ջ���λ
		/// </summary>
		public QueryConditionField cnvcReceiveDeptID = new QueryConditionField("cnvcReceiveDeptID");

		/// <summary>
		/// �ջ���
		/// </summary>
		public QueryConditionField cnvcReceiveOperID = new QueryConditionField("cnvcReceiveOperID");

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public QueryConditionField cndShipDate = new QueryConditionField("cndShipDate");

		/// <summary>
		/// �ջ�ʱ��
		/// </summary>
		public QueryConditionField cndReceiveDate = new QueryConditionField("cndReceiveDate");

		/// <summary>
		/// ����
		/// </summary>
		public QueryConditionField cnvcSalesroomOperID = new QueryConditionField("cnvcSalesroomOperID");

		/// <summary>
		/// ����
		/// </summary>
		public QueryConditionField cnvcTransportOperID = new QueryConditionField("cnvcTransportOperID");

		/// <summary>
		/// ��Ʒ��
		/// </summary>
		public QueryConditionField cnvcStorageOperID = new QueryConditionField("cnvcStorageOperID");

		/// <summary>
		/// �ͻ�ǩ��
		/// </summary>
		public QueryConditionField cnvcCustomerValidate = new QueryConditionField("cnvcCustomerValidate");

		/// <summary>
		/// �ͻ��������
		/// </summary>
		public QueryConditionField cnvcCustomerIdea = new QueryConditionField("cnvcCustomerIdea");

		/// <summary>
		/// ����Ա
		/// </summary>
		public QueryConditionField cnvcOperID = new QueryConditionField("cnvcOperID");

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public QueryConditionField cndOperDate = new QueryConditionField("cndOperDate");

		/// <summary>
		/// ��ע
		/// </summary>
		public QueryConditionField cnvcComments = new QueryConditionField("cnvcComments");
	}	
}
