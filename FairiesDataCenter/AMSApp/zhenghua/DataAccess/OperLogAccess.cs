
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	OperLogAccess.cs
* ����:	     ֣��
* ��������:    2008-10-10
* ��������:    ������־��

*                                                           Copyright(C) 2008 zhenghua
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
	/// **�������ƣ�������־�����ݷ�����
	/// </summary>
	public class OperLogAccess
	{
		// ����д��Ĵ���



		#region ���������ɴ���



		/// <summary>
		/// ȡ��ǰ��ѯ����ʵ��


		/// </summary>
		/// <returns>��ѯ����</returns>
		private static OperLogArgs GetCurrentArgs()
		{
			return new OperLogArgs();
		}
		
		#endregion
	}
}

