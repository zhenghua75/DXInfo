using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using AMSApp.zhenghua.DataAccess;
using AMSApp.zhenghua.Common;
using AMSApp.zhenghua.QueryArgs;
using AMSApp.zhenghua.Entity;

namespace AMSApp.zhenghua.Business
{
	/// <summary>
	/// SalesFacade 的摘要说明。
	/// </summary>
	public class ProductFacade
	{
		public ProductFacade()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region 生产产品入库
		public void AddProductSerial(ArrayList alProductSerial,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					SerialNo serialNo = new SerialNo();
					serialNo.cnvcFill = "0";
					serialNo.cnnSerialNo = Convert.ToInt32(EntityMapping.Create(serialNo, trans));
					for(int i=0;i<alProductSerial.Count;i++)
					{
						ProductSerial productSerial = (ProductSerial)alProductSerial[i];
						productSerial.cndOperDate = dtSysTime;
						productSerial.cnnSerialNo = serialNo.cnnSerialNo;
						EntityMapping.Create(productSerial, trans);

						ProductSerialLog productSerialLog = new ProductSerialLog(productSerial.ToTable());
						//productSerialLog.cnnSerialNo = null;
						productSerialLog.cnnProductSerialNo = serialNo.cnnSerialNo;
						EntityMapping.Create(productSerialLog, trans);
					}
					
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产产品入库，生产流水："+serialNo.cnnSerialNo.ToString();

					EntityMapping.Create(operLog, trans);		

					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}

		public void AdjustProductSerial_Add(ProductSerial productSerial,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					ProductSerial oldProductSerial = EntityMapping.Get(productSerial,trans) as ProductSerial;
					if(oldProductSerial == null)
						throw new Exception("未找到对应生产流水的产品！");

					oldProductSerial.cnnAddCount = productSerial.cnnAddCount;
					oldProductSerial.cnvcOperID = operLog.cnvcOperID;
					oldProductSerial.cndOperDate = dtSysTime;
					EntityMapping.Update(oldProductSerial, trans);

					ProductSerialLog productSerialLog = new ProductSerialLog(oldProductSerial.ToTable());
					productSerialLog.cnnProductSerialNo = oldProductSerial.cnnSerialNo;
					EntityMapping.Create(productSerialLog, trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产产品调增，生产流水："+oldProductSerial.cnnSerialNo.ToString()+"，产品编码："+productSerial.cnvcCode;

					EntityMapping.Create(operLog, trans);		

					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}

		public void AdjustProductSerial_Reduce(ProductSerial productSerial,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					ProductSerial oldProductSerial = EntityMapping.Get(productSerial,trans) as ProductSerial;
					if(oldProductSerial == null)
						throw new Exception("未找到对应生产流水的产品！");

					oldProductSerial.cnnReduceCount = productSerial.cnnReduceCount;
					oldProductSerial.cnvcOperID = operLog.cnvcOperID;
					oldProductSerial.cndOperDate = dtSysTime;
					EntityMapping.Update(oldProductSerial, trans);

					ProductSerialLog productSerialLog = new ProductSerialLog(oldProductSerial.ToTable());
					productSerialLog.cnnProductSerialNo = oldProductSerial.cnnSerialNo;
					EntityMapping.Create(productSerialLog, trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产产品调减，生产流水："+oldProductSerial.cnnSerialNo.ToString()+"，产品编码："+productSerial.cnvcCode;

					EntityMapping.Create(operLog, trans);		

					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}

		public void AdjustProductSerial_Delete(ProductSerial productSerial,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					ProductSerial oldProductSerial = EntityMapping.Get(productSerial,trans) as ProductSerial;
					if(oldProductSerial == null)
						throw new Exception("未找到对应生产流水的产品！");

					oldProductSerial.cnnReduceCount = productSerial.cnnReduceCount;
					oldProductSerial.cnvcOperID = operLog.cnvcOperID;
					oldProductSerial.cndOperDate = dtSysTime;
					EntityMapping.Delete(oldProductSerial, trans);

					ProductSerialLog productSerialLog = new ProductSerialLog(oldProductSerial.ToTable());
					productSerialLog.cnnProductSerialNo = oldProductSerial.cnnSerialNo;
					EntityMapping.Create(productSerialLog, trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产产品删除，生产流水："+oldProductSerial.cnnSerialNo.ToString()+"，产品编码："+productSerial.cnvcCode;

					EntityMapping.Create(operLog, trans);		

					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}
		#endregion

		#region 生产产品报损
		public void AddProductLostSerial(ArrayList alLostSerial,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					SerialNo serialNo = new SerialNo();
					serialNo.cnvcFill = "0";
					serialNo.cnnSerialNo = Convert.ToInt32(EntityMapping.Create(serialNo, trans));
					for(int i=0;i<alLostSerial.Count;i++)
					{
						LostSerial ls = (LostSerial)alLostSerial[i];
						ls.cnvcLostType = "0";
						ls.cndOperDate = dtSysTime;
						ls.cnnLostSerialNo = serialNo.cnnSerialNo;
						EntityMapping.Create(ls, trans);

//						ProductLostSerialLog productLostSerialLog = new ProductLostSerialLog(productLostSerial.ToTable());
//						//productSerialLog.cnnSerialNo = null;
//						productLostSerialLog.cnnProductLostSerialNo = serialNo.cnnSerialNo;
//						EntityMapping.Create(productLostSerialLog, trans);
					}
					
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产产品报损，报损流水："+serialNo.cnnSerialNo.ToString();

					EntityMapping.Create(operLog, trans);		

					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}

		public void AdjustProductLostSerial_Add(LostSerial ls,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					LostSerial oldls = EntityMapping.Get(ls,trans) as LostSerial;
					if(oldls == null)
						throw new Exception("未找到对应报损流水的产品！");

					oldls.cnnAddCount = ls.cnnAddCount;
					oldls.cnnReduceCount = ls.cnnReduceCount;
					oldls.cnvcOperID = operLog.cnvcOperID;
					oldls.cndOperDate = dtSysTime;
					EntityMapping.Update(oldls, trans);

//					ProductLostSerialLog productLostSerialLog = new ProductLostSerialLog(oldProductLostSerial.ToTable());
//					productLostSerialLog.cnnProductLostSerialNo = oldProductLostSerial.cnnSerialNo;
//					EntityMapping.Create(productLostSerialLog, trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产产品报损调整，报损流水："+oldls.cnnLostSerialNo.ToString()+"，产品编码："+ls.cnvcInvCode;

					EntityMapping.Create(operLog, trans);		

					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}

		public void AdjustProductLostSerial_Reduce(LostSerial ls,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					LostSerial oldls = EntityMapping.Get(ls,trans) as LostSerial;
					if(oldls == null)
						throw new Exception("未找到对应报损流水的产品！");

					oldls.cnnReduceCount = ls.cnnReduceCount;
					oldls.cnvcOperID = operLog.cnvcOperID;
					oldls.cndOperDate = dtSysTime;
					EntityMapping.Update(oldls, trans);

//					ProductLostSerialLog productLostSerialLog = new ProductLostSerialLog(oldProductLostSerial.ToTable());
//					productLostSerialLog.cnnProductLostSerialNo = oldProductLostSerial.cnnSerialNo;
//					EntityMapping.Create(productLostSerialLog, trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产产品报损调减，报损流水："+oldls.cnnLostSerialNo.ToString()+"，产品编码："+ls.cnvcInvCode;

					EntityMapping.Create(operLog, trans);		

					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}

		public void AdjustProductLostSerial_Delete(LostSerial ls,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					LostSerial oldls = EntityMapping.Get(ls,trans) as LostSerial;
					if(oldls == null)
						throw new Exception("未找到对应报损流水的产品！");

					oldls.cnnReduceCount = ls.cnnReduceCount;
					oldls.cnvcOperID = operLog.cnvcOperID;
					oldls.cndOperDate = dtSysTime;
					EntityMapping.Delete(oldls, trans);

//					ProductLostSerialLog productLostSerialLog = new ProductLostSerialLog(oldProductLostSerial.ToTable());
//					productLostSerialLog.cnnProductLostSerialNo = oldProductLostSerial.cnnSerialNo;
//					EntityMapping.Create(productLostSerialLog, trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产产品报损删除，报损流水："+oldls.cnnLostSerialNo.ToString()+"，产品编码："+ls.cnvcInvCode;

					EntityMapping.Create(operLog, trans);		

					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}
		#endregion

		#region 销售入库
		public void AddSalesSerial(ArrayList alSalesSerial,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					SerialNo serialNo = new SerialNo();
					serialNo.cnvcFill = "0";
					serialNo.cnnSerialNo = Convert.ToInt32(EntityMapping.Create(serialNo, trans));
					for(int i=0;i<alSalesSerial.Count;i++)
					{
						SalesSerial salesSerial = (SalesSerial)alSalesSerial[i];
						salesSerial.cndOperDate = dtSysTime;
						salesSerial.cnnSerialNo = serialNo.cnnSerialNo;
						EntityMapping.Create(salesSerial, trans);

						SalesSerialLog salesSerialLog = new SalesSerialLog(salesSerial.ToTable());
						//salesSerialLog.cnnSerialNo = null;
						salesSerialLog.cnnSalesSerialNo = serialNo.cnnSerialNo;
						EntityMapping.Create(salesSerialLog, trans);
					}
					
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "销售入库，生产流水："+serialNo.cnnSerialNo.ToString();

					EntityMapping.Create(operLog, trans);		

					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}

		public void AdjustSalesSerial_Add(SalesSerial salesSerial,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					SalesSerial oldSalesSerial = EntityMapping.Get(salesSerial,trans) as SalesSerial;
					if(oldSalesSerial == null)
						throw new Exception("未找到对应销售流水的产品！");

					oldSalesSerial.cnnAddCount = salesSerial.cnnAddCount;
					oldSalesSerial.cnvcOperID = operLog.cnvcOperID;
					oldSalesSerial.cndOperDate = dtSysTime;
					EntityMapping.Update(oldSalesSerial, trans);

					SalesSerialLog salesSerialLog = new SalesSerialLog(oldSalesSerial.ToTable());
					salesSerialLog.cnnSalesSerialNo = oldSalesSerial.cnnSerialNo;
					EntityMapping.Create(salesSerialLog, trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "销售调增，生产流水："+oldSalesSerial.cnnSerialNo.ToString()+"，产品编码："+salesSerial.cnvcCode;

					EntityMapping.Create(operLog, trans);		

					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}

		public void AdjustSalesSerial_Reduce(SalesSerial salesSerial,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					SalesSerial oldSalesSerial = EntityMapping.Get(salesSerial,trans) as SalesSerial;
					if(oldSalesSerial == null)
						throw new Exception("未找到对应销售流水的产品！");

					oldSalesSerial.cnnReduceCount = salesSerial.cnnReduceCount;
					oldSalesSerial.cnvcOperID = operLog.cnvcOperID;
					oldSalesSerial.cndOperDate = dtSysTime;
					EntityMapping.Update(oldSalesSerial, trans);

					SalesSerialLog salesSerialLog = new SalesSerialLog(oldSalesSerial.ToTable());
					salesSerialLog.cnnSalesSerialNo = oldSalesSerial.cnnSerialNo;
					EntityMapping.Create(salesSerialLog, trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "销售调减，生产流水："+oldSalesSerial.cnnSerialNo.ToString()+"，产品编码："+salesSerial.cnvcCode;

					EntityMapping.Create(operLog, trans);		

					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}

		public void AdjustSalesSerial_Delete(SalesSerial salesSerial,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					SalesSerial oldSalesSerial = EntityMapping.Get(salesSerial,trans) as SalesSerial;
					if(oldSalesSerial == null)
						throw new Exception("未找到对应销售流水的产品！");

					oldSalesSerial.cnnReduceCount = salesSerial.cnnReduceCount;
					oldSalesSerial.cnvcOperID = operLog.cnvcOperID;
					oldSalesSerial.cndOperDate = dtSysTime;
					EntityMapping.Delete(oldSalesSerial, trans);

					SalesSerialLog salesSerialLog = new SalesSerialLog(oldSalesSerial.ToTable());
					salesSerialLog.cnnSalesSerialNo = oldSalesSerial.cnnSerialNo;
					EntityMapping.Create(salesSerialLog, trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "销售删除，生产流水："+oldSalesSerial.cnnSerialNo.ToString()+"，产品编码："+salesSerial.cnvcCode;

					EntityMapping.Create(operLog, trans);		

					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}
		#endregion

		#region 盘点入库
		public void AddCheckSerial(ArrayList alCheckSerial,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					SerialNo serialNo = new SerialNo();
					serialNo.cnvcFill = "0";
					serialNo.cnnSerialNo = Convert.ToInt32(EntityMapping.Create(serialNo, trans));
					for(int i=0;i<alCheckSerial.Count;i++)
					{
						CheckSerial checkSerial = (CheckSerial)alCheckSerial[i];
						checkSerial.cndOperDate = dtSysTime;
						checkSerial.cnnSerialNo = serialNo.cnnSerialNo;
						EntityMapping.Create(checkSerial, trans);

						CheckSerialLog checkSerialLog = new CheckSerialLog(checkSerial.ToTable());
						//CheckSerialLog.cnnSerialNo = null;
						checkSerialLog.cnnCheckSerialNo = serialNo.cnnSerialNo;
						EntityMapping.Create(checkSerialLog, trans);
					}
					
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "盘点入库，盘点流水："+serialNo.cnnSerialNo.ToString();

					EntityMapping.Create(operLog, trans);		

					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}

		public void AdjustCheckSerial_Add(CheckSerial CheckSerial,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					CheckSerial oldCheckSerial = EntityMapping.Get(CheckSerial,trans) as CheckSerial;
					if(oldCheckSerial == null)
						throw new Exception("未找到对应盘点流水的产品！");

					oldCheckSerial.cnnAddCount = CheckSerial.cnnAddCount;
					oldCheckSerial.cnvcOperID = operLog.cnvcOperID;
					oldCheckSerial.cndOperDate = dtSysTime;
					EntityMapping.Update(oldCheckSerial, trans);

					CheckSerialLog checkSerialLog = new CheckSerialLog(oldCheckSerial.ToTable());
					checkSerialLog.cnnCheckSerialNo = oldCheckSerial.cnnSerialNo;
					EntityMapping.Create(checkSerialLog, trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "盘点调增，盘点流水："+oldCheckSerial.cnnSerialNo.ToString()+"，产品编码："+CheckSerial.cnvcCode;

					EntityMapping.Create(operLog, trans);		

					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}

		public void AdjustCheckSerial_Reduce(CheckSerial CheckSerial,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					CheckSerial oldCheckSerial = EntityMapping.Get(CheckSerial,trans) as CheckSerial;
					if(oldCheckSerial == null)
						throw new Exception("未找到对应盘点流水的产品！");

					oldCheckSerial.cnnReduceCount = CheckSerial.cnnReduceCount;
					oldCheckSerial.cnvcOperID = operLog.cnvcOperID;
					oldCheckSerial.cndOperDate = dtSysTime;
					EntityMapping.Update(oldCheckSerial, trans);

					CheckSerialLog checkSerialLog = new CheckSerialLog(oldCheckSerial.ToTable());
					checkSerialLog.cnnCheckSerialNo = oldCheckSerial.cnnSerialNo;
					EntityMapping.Create(checkSerialLog, trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "盘点调减，盘点流水："+oldCheckSerial.cnnSerialNo.ToString()+"，产品编码："+CheckSerial.cnvcCode;

					EntityMapping.Create(operLog, trans);		

					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}

		public void AdjustCheckSerial_Delete(CheckSerial CheckSerial,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					CheckSerial oldCheckSerial = EntityMapping.Get(CheckSerial,trans) as CheckSerial;
					if(oldCheckSerial == null)
						throw new Exception("未找到对应盘点流水的产品！");

					oldCheckSerial.cnnReduceCount = CheckSerial.cnnReduceCount;
					oldCheckSerial.cnvcOperID = operLog.cnvcOperID;
					oldCheckSerial.cndOperDate = dtSysTime;
					EntityMapping.Delete(oldCheckSerial, trans);

					CheckSerialLog checkSerialLog = new CheckSerialLog(oldCheckSerial.ToTable());
					checkSerialLog.cnnCheckSerialNo = oldCheckSerial.cnnSerialNo;
					EntityMapping.Create(checkSerialLog, trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "盘点删除，盘点流水："+oldCheckSerial.cnnSerialNo.ToString()+"，产品编码："+CheckSerial.cnvcCode;

					EntityMapping.Create(operLog, trans);		

					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);
					throw sex;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);
					throw ex;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
			}
		}
		#endregion

	}
}
