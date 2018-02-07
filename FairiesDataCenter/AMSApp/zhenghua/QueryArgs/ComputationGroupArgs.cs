
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:    	ComputationGroupArgs.cs
* ����:	     ֣��
* ��������:    2010-3-6
* ��������:    ������λ��

*                                                           Copyright(C) 2010 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ�������λ���ѯ������
	/// </summary>
	public class ComputationGroupArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbComputationGroup";
				
		/// <summary>
		/// ������λ�����
		/// </summary>
		public QueryConditionField cnvcGroupCode = new QueryConditionField("cnvcGroupCode");

		/// <summary>
		/// ������λ������
		/// </summary>
		public QueryConditionField cnvcGroupName = new QueryConditionField("cnvcGroupName");
	}	
}
