
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	NameCodeArgs.cs
* ����:	     ֣��
* ��������:    2008-10-10
* ��������:    �����

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ�������ѯ������
	/// </summary>
	public class NameCodeArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbNameCode";
				
		/// <summary>
		/// ����
		/// </summary>
		public QueryConditionField cnvcType = new QueryConditionField("cnvcType");

		/// <summary>
		/// ����
		/// </summary>
		public QueryConditionField cnvcCode = new QueryConditionField("cnvcCode");

		/// <summary>
		/// ����
		/// </summary>
		public QueryConditionField cnvcName = new QueryConditionField("cnvcName");

		/// <summary>
		/// ����
		/// </summary>
		public QueryConditionField cnvcComments = new QueryConditionField("cnvcComments");
	}	
}
