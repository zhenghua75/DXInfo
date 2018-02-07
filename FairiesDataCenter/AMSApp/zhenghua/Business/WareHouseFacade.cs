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
	/// WareHouseFacade 的摘要说明。
	/// </summary>
	public class WareHouseFacade
	{
		public WareHouseFacade()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public int AddWareHouse(OperLog operLog,Entity.Warehouse wh)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					EntityMapping.Create(wh,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "仓库编码："+wh.cnvcWhCode;
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

		public int UpdateWareHouse(OperLog operLog,Entity.Warehouse wh)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					Entity.Warehouse oldwh = new AMSApp.zhenghua.Entity.Warehouse();
					oldwh.cnvcWhCode = wh.cnvcWhCode;

					oldwh = EntityMapping.Get(oldwh,trans) as AMSApp.zhenghua.Entity.Warehouse;
					oldwh.cnvcWhName = wh.cnvcWhName;
					oldwh.cnvcDepCode = wh.cnvcDepCode;
					oldwh.cnvcWhAddress = wh.cnvcWhAddress;
					oldwh.cnvcWhPhone = wh.cnvcWhPhone;
					oldwh.cnvcWhPerson = wh.cnvcWhPerson;
					oldwh.cnvcWhValueStyle = wh.cnvcWhValueStyle;
					oldwh.cnbFreeze = wh.cnbFreeze;
					oldwh.cnnFrequency = wh.cnnFrequency;
					oldwh.cnvcFrequency = wh.cnvcFrequency;
					oldwh.cnvcWhProperty = wh.cnvcWhProperty;
					oldwh.cnbShop = wh.cnbShop;
					EntityMapping.Update(oldwh,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "仓库编码："+oldwh.cnvcWhCode;
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



		public int AddTeam(OperLog operLog,Entity.Team team)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					long id = EntityMapping.Create(team,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产组编码："+id.ToString();
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

		public int UpdateTeam(OperLog operLog,Entity.Team team)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					Entity.Team oldteam = new AMSApp.zhenghua.Entity.Team();
					oldteam.cnnTeamID = team.cnnTeamID;

					oldteam = EntityMapping.Get(oldteam,trans) as AMSApp.zhenghua.Entity.Team;
					oldteam.cnvcTeamName = team.cnvcTeamName;
					EntityMapping.Update(oldteam,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产组编码："+oldteam.cnnTeamID.ToString();
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


		
		public int AddProducer(OperLog operLog,Entity.Producer producer)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					long id = EntityMapping.Create(producer,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产员编码："+id.ToString();
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

		public int UpdateProducer(OperLog operLog,Entity.Producer producer)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					Entity.Producer oldproducer = new AMSApp.zhenghua.Entity.Producer();
					oldproducer.cnnProducerID = producer.cnnProducerID;

					oldproducer = EntityMapping.Get(oldproducer,trans) as AMSApp.zhenghua.Entity.Producer;
					oldproducer.cnvcProducerName = producer.cnvcProducerName;
					EntityMapping.Update(oldproducer,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产员编码："+oldproducer.cnnProducerID.ToString();
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
