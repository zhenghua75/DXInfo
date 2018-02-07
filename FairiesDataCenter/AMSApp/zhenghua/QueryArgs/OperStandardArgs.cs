
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	OperStandardArgs.cs
* ����:	     ֣��
* ��������:    2008-10-3
* ��������:    ������׼��

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ�������׼���ѯ������
	/// </summary>
	public class OperStandardArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbOperStandard";
				
		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public QueryConditionField cnvcProductCode = new QueryConditionField("cnvcProductCode");

		/// <summary>
		/// �������
		/// </summary>
		public QueryConditionField cnnSort = new QueryConditionField("cnnSort");

		/// <summary>
		/// ������׼
		/// </summary>
		public QueryConditionField cnvcStandard = new QueryConditionField("cnvcStandard");

		/// <summary>
		/// �ؼ����Ƶ�
		/// </summary>
		public QueryConditionField cnvcKey = new QueryConditionField("cnvcKey");
	}	
}
