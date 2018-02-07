
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	FormulaArgs.cs
* ����:	     ֣��
* ��������:    2008-9-29
* ��������:    �䷽��

*                                                           Copyright(C) 2008 fightop
*************************************************************************************/
#region ����
using System;
using AMSApp.zhenghua.Common;
#endregion
namespace AMSApp.zhenghua.QueryArgs
{
	/// <summary>
	/// **�������ƣ��䷽���ѯ������
	/// </summary>
	public class FormulaArgs
	{
		/// <summary>
		/// ����
		/// </summary>
		public string TableName = "tbFormula";
				
		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public QueryConditionField cnvcProductCode = new QueryConditionField("cnvcProductCode");

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public QueryConditionField cnvcProductName = new QueryConditionField("cnvcProductName");

		/// <summary>
		/// ��Ʒ����
		/// </summary>
		public QueryConditionField cnvcProductType = new QueryConditionField("cnvcProductType");

		/// <summary>
		/// ��Ʒ���
		/// </summary>
		public QueryConditionField cnvcProductClass = new QueryConditionField("cnvcProductClass");

		/// <summary>
		/// ��ƷͼƬ
		/// </summary>
		public QueryConditionField cnbProductImage = new QueryConditionField("cnbProductImage");

		/// <summary>
		/// ԭ�ϳɱ��ϼ�
		/// </summary>
		public QueryConditionField cnnMaterialCostSum = new QueryConditionField("cnnMaterialCostSum");

		/// <summary>
		/// ���ϳɱ��ϼ�
		/// </summary>
		public QueryConditionField cnnPackingCostSum = new QueryConditionField("cnnPackingCostSum");

		/// <summary>
		/// �ɱ��ϼ�
		/// </summary>
		public QueryConditionField cnnCostSum = new QueryConditionField("cnnCostSum");

		/// <summary>
		/// ��λ
		/// </summary>
		public QueryConditionField cnvcUnit = new QueryConditionField("cnvcUnit");

		/// <summary>
		/// �ݲ�����
		/// </summary>
		public QueryConditionField cnnPortionCount = new QueryConditionField("cnnPortionCount");

		/// <summary>
		/// �ݲ���λ
		/// </summary>
		public QueryConditionField cnvcPortionUnit = new QueryConditionField("cnvcPortionUnit");

		/// <summary>
		/// �ڸ�
		/// </summary>
		public QueryConditionField cnvcFeel = new QueryConditionField("cnvcFeel");

		/// <summary>
		/// ��֯
		/// </summary>
		public QueryConditionField cnvcOrganise = new QueryConditionField("cnvcOrganise");

		/// <summary>
		/// ��ɫ
		/// </summary>
		public QueryConditionField cnvcColor = new QueryConditionField("cnvcColor");

		/// <summary>
		/// ��ζ
		/// </summary>
		public QueryConditionField cnvcTaste = new QueryConditionField("cnvcTaste");
	}	
}
