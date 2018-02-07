
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	DosageArgs.cs
* ����:	     ֣��
* ��������:    2008-10-1
* ��������:    ���ϱ� 

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ����ϱ� ��ѯ������
	/// </summary>
	public class DosageArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbDosage";
				
		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public QueryConditionField cnvcProductCode = new QueryConditionField("cnvcProductCode");

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public QueryConditionField cnvcProductType = new QueryConditionField("cnvcProductType");

		/// <summary>
		/// ԭ�ϱ���
		/// </summary>
		public QueryConditionField cnvcCode = new QueryConditionField("cnvcCode");

		/// <summary>
		/// ԭ������
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
		/// �۸�
		/// </summary>
		public QueryConditionField cnnPrice = new QueryConditionField("cnnPrice");

		/// <summary>
		/// �ɱ�
		/// </summary>
		public QueryConditionField cnnSum = new QueryConditionField("cnnSum");
	}	
}
