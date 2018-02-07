using System;
using System.Data;
using System.Data.SqlClient;
using AMSApp.zhenghua.DataAccess;
using AMSApp.zhenghua.Common;
using AMSApp.zhenghua.QueryArgs;
using AMSApp.zhenghua;
using AMSApp.zhenghua.Entity;
using System.Collections;
using System.IO;

namespace AMSApp.zhenghua.Business
{
	/// <summary>
	/// ComputationUnit ��ժҪ˵����
	/// </summary>
	public class ComputationUnit
	{
		public ComputationUnit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public int AddComputationGroup(OperLog operLog,ComputationGroup cg)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					EntityMapping.Create(cg,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "������λ����룺"+cg.cnvcGroupCode;
					EntityMapping.Create(operLog, trans);
					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					return -1;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					return -1;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
				return 1;
			}
		}

		public int UpdateComputationGroup(OperLog operLog,ComputationGroup cg)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					ComputationGroup oldcg = new ComputationGroup();
					oldcg.cnvcGroupCode = cg.cnvcGroupCode;

					oldcg = EntityMapping.Get(oldcg,trans) as ComputationGroup;
					oldcg.cnvcGroupName = cg.cnvcGroupName;
					EntityMapping.Update(cg,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "������λ����룺"+cg.cnvcGroupCode;
					EntityMapping.Create(operLog, trans);
					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					return -1;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					return -1;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
				return 1;
			}
		}

		public int DeleteComputationGroup(OperLog operLog,ComputationGroup cg)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					ComputationGroup oldcg = new ComputationGroup();
					oldcg.cnvcGroupCode = cg.cnvcGroupCode;

					oldcg = EntityMapping.Get(oldcg,trans)  as ComputationGroup;
					EntityMapping.Delete(oldcg,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "������λ����룺"+cg.cnvcGroupCode;
					EntityMapping.Create(operLog, trans);
					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					return -1;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					return -1;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
				return 1;
			}
		}




		public int AddComputationUnit(OperLog operLog,Entity.ComputationUnit cu)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					EntityMapping.Create(cu,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "������λ���룺"+cu.cnvcComunitCode;
					EntityMapping.Create(operLog, trans);
					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					return -1;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					return -1;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
				return 1;
			}
		}

		public int UpdateComputationUnit(OperLog operLog,Entity.ComputationUnit cu)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					Entity.ComputationUnit oldcu = new Entity.ComputationUnit();
					oldcu.cnvcComunitCode = cu.cnvcComunitCode;

					oldcu = EntityMapping.Get(oldcu,trans) as Entity.ComputationUnit;
					if(oldcu == null)
					{
						throw new Exception("�޷���λ������λ��"+cu.cnvcComunitCode);
					}
					oldcu.cnvcComUnitName = cu.cnvcComUnitName;
					oldcu.cnbMainUnit = cu.cnbMainUnit;
					oldcu.cniChangRate = cu.cniChangRate;
					EntityMapping.Update(oldcu,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "������λ���룺"+cu.cnvcComunitCode;
					EntityMapping.Create(operLog, trans);
					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					return -1;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					return -1;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
				return 1;
			}
		}

		public int DeleteComputationUnit(OperLog operLog,Entity.ComputationUnit cu)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					Entity.ComputationUnit oldcu = new Entity.ComputationUnit();
					oldcu.cnvcComunitCode = cu.cnvcComunitCode;

					oldcu = EntityMapping.Get(oldcu,trans)  as Entity.ComputationUnit;
					if(oldcu == null)
						throw new Exception("δ�ҵ���¼��λ");
					if(oldcu.cnbMainUnit)
						throw new Exception("��������λ����ɾ��");
					EntityMapping.Delete(oldcu,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "������λ����룺"+cu.cnvcComunitCode;
					EntityMapping.Create(operLog, trans);
					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					return -1;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					return -1;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
				return 1;
			}
		}
	}
}
