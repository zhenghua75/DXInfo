
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	GroupMakeArgs.cs
* ����:	     ֣��
* ��������:    2008-10-23
* ��������:    �����������Ӧ��

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ������������Ӧ���ѯ������
	/// </summary>
	public class GroupMakeArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbGroupMake";
				
		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public QueryConditionField cnvcProductType = new QueryConditionField("cnvcProductType");

		/// <summary>
		/// ������
		/// </summary>
		public QueryConditionField cnvcGroupCode = new QueryConditionField("cnvcGroupCode");

		/// <summary>
		/// ��������
		/// </summary>
		public QueryConditionField cnvcMakeName = new QueryConditionField("cnvcMakeName");
	}	
}
