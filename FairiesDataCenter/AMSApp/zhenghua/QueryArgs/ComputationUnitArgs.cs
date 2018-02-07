
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:    	ComputationUnitArgs.cs
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
	public class ComputationUnitArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbComputationUnit";
				
		/// <summary>
		/// ������λ����
		/// </summary>
		public QueryConditionField cnvcComunitCode = new QueryConditionField("cnvcComunitCode");

		/// <summary>
		/// ������λ����
		/// </summary>
		public QueryConditionField cnvcComUnitName = new QueryConditionField("cnvcComUnitName");

		/// <summary>
		/// ������λ�����
		/// </summary>
		public QueryConditionField cnvcGroupCode = new QueryConditionField("cnvcGroupCode");

		/// <summary>
		/// �Ƿ���������λ
		/// </summary>
		public QueryConditionField cnbMainUnit = new QueryConditionField("cnbMainUnit");

		/// <summary>
		/// ������
		/// </summary>
		public QueryConditionField cniChangRate = new QueryConditionField("cniChangRate");
	}	
}
