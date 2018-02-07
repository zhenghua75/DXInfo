
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	StorageLogArgs.cs
* ����:	     ֣��
* ��������:    2008-11-3
* ��������:    �����־��

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ������־���ѯ������
	/// </summary>
	public class StorageLogArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbStorageLog";
				
		/// <summary>
		/// ��־��ˮ
		/// </summary>
		public QueryConditionField cnnSerialNo = new QueryConditionField("cnnSerialNo");

		/// <summary>
		/// �ֿ�
		/// </summary>
		public QueryConditionField cnvcStorageDeptID = new QueryConditionField("cnvcStorageDeptID");

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public QueryConditionField cnvcProductCode = new QueryConditionField("cnvcProductCode");

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public QueryConditionField cnvcProductName = new QueryConditionField("cnvcProductName");

		/// <summary>
		/// ��λ
		/// </summary>
		public QueryConditionField cnvcUnit = new QueryConditionField("cnvcUnit");

		/// <summary>
		/// ʵ�ʿ������
		/// </summary>
		public QueryConditionField cnnCount = new QueryConditionField("cnnCount");

		/// <summary>
		/// ��ȫ�������
		/// </summary>
		public QueryConditionField cnnSafeCount = new QueryConditionField("cnnSafeCount");

		/// <summary>
		/// ��ȫ���޿������
		/// </summary>
		public QueryConditionField cnnSafeUpCount = new QueryConditionField("cnnSafeUpCount");

		/// <summary>
		/// ��������
		/// </summary>
		public QueryConditionField cnvcOperType = new QueryConditionField("cnvcOperType");

		/// <summary>
		/// ����Ա
		/// </summary>
		public QueryConditionField cnvcOperID = new QueryConditionField("cnvcOperID");

		/// <summary>
		/// ��������
		/// </summary>
		public QueryConditionField cndOperDate = new QueryConditionField("cndOperDate");
	}	
}
