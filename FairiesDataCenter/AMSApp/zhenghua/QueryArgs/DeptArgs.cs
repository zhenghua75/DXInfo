
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	DeptArgs.cs
* ����:	     ֣��
* ��������:    2008-10-30
* ��������:    ���ű�

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ����ű��ѯ������
	/// </summary>
	public class DeptArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbDept";
				
		/// <summary>
		/// ��������
		/// </summary>
		public QueryConditionField cnvcDeptName = new QueryConditionField("cnvcDeptName");

		/// <summary>
		/// ���ű���
		/// </summary>
		public QueryConditionField cnvcDeptID = new QueryConditionField("cnvcDeptID");

		/// <summary>
		/// ��������
		/// </summary>
		public QueryConditionField cnvcDeptType = new QueryConditionField("cnvcDeptType");

		/// <summary>
		/// �ϼ����ű���
		/// </summary>
		public QueryConditionField cnvcParentDeptID = new QueryConditionField("cnvcParentDeptID");

		/// <summary>
		/// ����
		/// </summary>
		public QueryConditionField cnvcComments = new QueryConditionField("cnvcComments");

		/// <summary>
		/// 
		/// </summary>
		public QueryConditionField cnnPriority = new QueryConditionField("cnnPriority");
	}	
}
