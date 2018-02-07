
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	MaterialArgs.cs
* ����:	     ֣��
* ��������:    2008-10-22
* ��������:    ԭ�ϲ��ϱ�

*                                                           Copyright(C) 2008 zhenghua
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ�ԭ�ϲ��ϱ��ѯ������
	/// </summary>
	public class MaterialArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbMaterial";
				
		/// <summary>
		/// ԭ�ϱ���
		/// </summary>
		public QueryConditionField cnvcMaterialCode = new QueryConditionField("cnvcMaterialCode");

		/// <summary>
		/// ԭ������
		/// </summary>
		public QueryConditionField cnvcMaterialName = new QueryConditionField("cnvcMaterialName");

		/// <summary>
		/// ��С������λ
		/// </summary>
		public QueryConditionField cnvcLeastUnit = new QueryConditionField("cnvcLeastUnit");

		/// <summary>
		/// �����ϵ
		/// </summary>
		public QueryConditionField cnnConversion = new QueryConditionField("cnnConversion");

		/// <summary>
		/// ��λ
		/// </summary>
		public QueryConditionField cnvcUnit = new QueryConditionField("cnvcUnit");

		/// <summary>
		/// ���λ
		/// </summary>
		public QueryConditionField cnvcStandardUnit = new QueryConditionField("cnvcStandardUnit");

		/// <summary>
		/// �������
		/// </summary>
		public QueryConditionField cnnStatdardCount = new QueryConditionField("cnnStatdardCount");

		/// <summary>
		/// ��С������λ�۸�
		/// </summary>
		public QueryConditionField cnnPrice = new QueryConditionField("cnnPrice");

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public QueryConditionField cnvcProductType = new QueryConditionField("cnvcProductType");

		/// <summary>
		/// ��Ʒ���
		/// </summary>
		public QueryConditionField cnvcProductClass = new QueryConditionField("cnvcProductClass");
	}	
}
