
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ� AMSApp
* �ļ���: 	ComputationUnitAccess.cs
* ����:		     ֣��
* ��������:   2010-3-6
* ��������:   ������λ

*                                                           Copyright(C) 2010 zhenghua
*************************************************************************************/
#region ImportNameSpace
using System;
using System.Data;
using System.Data.SqlClient;

using AMSApp.zhenghua.EntityBase;
using AMSApp.zhenghua.Entity;
using AMSApp.zhenghua.QueryArgs;
using AMSApp.zhenghua.Common;
#endregion

namespace AMSApp.zhenghua.DataAccess
{
	/// <summary>
	/// **�������ƣ�������λ���ݷ�����
	/// </summary>
	public class ComputationUnitAccess
	{
		//����д��Ĵ���



		#region ���������ɴ���



		/// <summary>
		/// ȡ��ǰ��ѯ����ʵ��


		/// </summary>
		/// <returns>��ѯ����</returns>
		private static ComputationUnitArgs GetCurrentArgs()
		{
			return new ComputationUnitArgs();
		}
		
		#endregion
	}
}
