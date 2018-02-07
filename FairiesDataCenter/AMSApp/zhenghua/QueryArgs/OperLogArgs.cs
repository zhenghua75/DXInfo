
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	OperLogArgs.cs
* ����:	     ֣��
* ��������:    2008-10-10
* ��������:    ������־��

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ�������־���ѯ������
	/// </summary>
	public class OperLogArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbOperLog";
				
		/// <summary>
		/// ������ˮ
		/// </summary>
		public QueryConditionField cnnOperSerialNo = new QueryConditionField("cnnOperSerialNo");

		/// <summary>
		/// ��������
		/// </summary>
		public QueryConditionField cnvcOperType = new QueryConditionField("cnvcOperType");

		/// <summary>
		/// ����Ա
		/// </summary>
		public QueryConditionField cnvcOperID = new QueryConditionField("cnvcOperID");

		/// <summary>
		/// ����
		/// </summary>
		public QueryConditionField cnvcDeptID = new QueryConditionField("cnvcDeptID");

		/// <summary>
		/// ����ʱ��
		/// </summary>
		public QueryConditionField cndOperDate = new QueryConditionField("cndOperDate");

		/// <summary>
		/// ����
		/// </summary>
		public QueryConditionField cnvcComments = new QueryConditionField("cnvcComments");
	}	
}
