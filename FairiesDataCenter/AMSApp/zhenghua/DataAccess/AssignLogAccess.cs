
/******************************************************************** FR 1.20E *******
* ��Ŀ���ƣ�   AMSApp
* �ļ���:   	AssignLogAccess.cs
* ����:	     ֣��
* ��������:    2008-10-13
* ��������:    �ֻ���ˮ��

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
	/// **�������ƣ��ֻ���ˮ�����ݷ�����
	/// </summary>
	public class AssignLogAccess
	{
		// ����д��Ĵ���



		#region ���������ɴ���



		/// <summary>
		/// ȡ��ǰ��ѯ����ʵ��


		/// </summary>
		/// <returns>��ѯ����</returns>
		private static AssignLogArgs GetCurrentArgs()
		{
			return new AssignLogArgs();
		}
		
		#endregion
	}
}

