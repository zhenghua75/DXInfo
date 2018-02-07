
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	StorageArgs.cs
* ����:	     ֣��
* ��������:    2008-11-3
* ��������:    ����

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ������ѯ������
	/// </summary>
	public class StorageArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbStorage";
				
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
		/// 
		/// </summary>
		public QueryConditionField cnnSafeUpCount = new QueryConditionField("cnnSafeUpCount");
	}	
}
