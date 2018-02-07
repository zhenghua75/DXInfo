using System;
using System.Data;
using System.Data.SqlClient;
using AMSApp.zhenghua.DataAccess;
using AMSApp.zhenghua.Common;
using AMSApp.zhenghua.QueryArgs;
using AMSApp.zhenghua.Entity;

namespace AMSApp.zhenghua.Business
{
	/// <summary>
	/// OrderFacade 的摘要说明。
	/// </summary>
	public class OrderFacade
	{
		public OrderFacade()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public void AddOrder(OrderBook orderBook,DataTable dtOrderBookDetail,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);


					if(orderBook.cnnOrderSerialNo > 0)
					{
						OrderBook oldOrder = new OrderBook();
						oldOrder.cnnOrderSerialNo = orderBook.cnnOrderSerialNo;
						oldOrder = EntityMapping.Get(oldOrder, trans) as OrderBook;
						if(oldOrder == null)
						{
							throw new Exception("订单未找到");
						}
						oldOrder.cnvcOperID = orderBook.cnvcOperID;
						oldOrder.cnvcOrderType = orderBook.cnvcOrderType;
						oldOrder.cnvcProduceDeptID = orderBook.cnvcProduceDeptID;
						oldOrder.cnvcOrderDeptID = orderBook.cnvcOrderDeptID;

						if(oldOrder.cnvcOrderType == "WDO" || oldOrder.cnvcOrderType == "WDOSELF")
						{
							oldOrder.cndArrivedDate = orderBook.cndArrivedDate;
							oldOrder.cnvcLinkPhone = orderBook.cnvcLinkPhone;
							oldOrder.cnvcShipAddress = orderBook.cnvcShipAddress;
							oldOrder.cnvcCustomName = orderBook.cnvcCustomName;
				
						}
						oldOrder.cndShipDate = orderBook.cndShipDate;
						oldOrder.cndOrderDate = orderBook.cndOrderDate;
						EntityMapping.Update(oldOrder, trans);

						SqlHelper.ExecuteNonQuery(trans,CommandType.Text,"delete from tborderbookdetail where cnnorderserialno="+orderBook.cnnOrderSerialNo.ToString())	;
						foreach(DataRow drOrderBookDetail in dtOrderBookDetail.Rows)
						{
							OrderBookDetail detail = new OrderBookDetail(drOrderBookDetail);
							detail.cnnOrderSerialNo = orderBook.cnnOrderSerialNo;
							EntityMapping.Create(detail, trans);
						}
					}
					else
					{
						OrderSerialNo serialNo = new OrderSerialNo();
						serialNo.cnvcFill = "0";
						serialNo.cnnSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));
					
						orderBook.cnnOrderSerialNo = serialNo.cnnSerialNo;
						orderBook.cndOperDate = dtSysTime;						
						orderBook.cnvcOperID = operLog.cnvcOperID;
						EntityMapping.Create(orderBook, trans);

						//SqlHelper.ExecuteNonQuery(trans,CommandType.Text,"delete from tborderbookdetail where cnnorderserialno="+orderBook.cnnOrderSerialNo.ToString())	;
						foreach(DataRow drOrderBookDetail in dtOrderBookDetail.Rows)
						{
							OrderBookDetail detail = new OrderBookDetail(drOrderBookDetail);
							detail.cnnOrderSerialNo = serialNo.cnnSerialNo;
							EntityMapping.Create(detail, trans);
						}
						
					}
					
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "订单流水："+orderBook.cnnOrderSerialNo;
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

		public void UpdateOrder(OrderBook order,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					OrderBook oldOrder = new OrderBook();
					oldOrder.cnnOrderSerialNo = order.cnnOrderSerialNo;
					oldOrder = EntityMapping.Get(oldOrder, trans) as OrderBook;
					if(oldOrder == null)
					{
						throw new Exception("订单未找到");
					}
					oldOrder.cnvcOperID = order.cnvcOperID;
					oldOrder.cnvcOrderType = order.cnvcOrderType;
					oldOrder.cnvcProduceDeptID = order.cnvcProduceDeptID;
					oldOrder.cnvcOrderDeptID = order.cnvcOrderDeptID;

					if(oldOrder.cnvcOrderType == "WDO" || oldOrder.cnvcOrderType == "WDOSELF")
					{
						oldOrder.cndArrivedDate = order.cndArrivedDate;
						oldOrder.cnvcLinkPhone = order.cnvcLinkPhone;
						oldOrder.cnvcShipAddress = order.cnvcShipAddress;
						oldOrder.cnvcCustomName = order.cnvcCustomName;
				
					}
					oldOrder.cndShipDate = order.cndShipDate;
					EntityMapping.Update(oldOrder, trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "订单流水："+oldOrder.cnnOrderSerialNo;
					EntityMapping.Create(operLog, trans);	

					//EntityMapping.Create(busiLog, trans);					
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

		//加单
		public void OrderAdd(string strOrderSerialNo,string strAddType,string strAddComments,DataTable dtOrderAdd,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					OrderSerialNo serialNo = new OrderSerialNo();
					serialNo.cnvcFill = "0";
					serialNo.cnnSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));
					
					foreach(DataRow drOrderAdd in dtOrderAdd.Rows)
					{
						OrderAddLog orderAdd = new OrderAddLog(drOrderAdd);
						orderAdd.cnnOrderSerialNo = decimal.Parse(strOrderSerialNo);
						orderAdd.cnnAddSerialNo = serialNo.cnnSerialNo;
						orderAdd.cnvcAddType = strAddType;
						orderAdd.cnvcAddComments = strAddComments;
						orderAdd.cndOperDate = dtSysTime;
						orderAdd.cnvcOperID = operLog.cnvcOperID;
						orderAdd.cnvcAddState = "0";
						orderAdd.cnnAddCount = decimal.Parse(drOrderAdd["cnnOrderCount"].ToString());
						EntityMapping.Create(orderAdd, trans);
					}
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "订单流水："+strOrderSerialNo;
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

		//减单
		public void OrderReduce(string strOrderSerialNo,string strReduceType,string strAddComments,DataTable dtOrderReduce,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					OrderSerialNo serialNo = new OrderSerialNo();
					serialNo.cnvcFill = "0";
					serialNo.cnnSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));
					
					foreach(DataRow drOrderReduce in dtOrderReduce.Rows)
					{
						OrderReduceLog orderReduce = new OrderReduceLog(drOrderReduce);
						orderReduce.cnnOrderSerialNo = decimal.Parse(strOrderSerialNo);
						orderReduce.cnnReduceSerialNo = serialNo.cnnSerialNo;
						orderReduce.cnvcReduceType = strReduceType;
						orderReduce.cnvcReduceComments = strAddComments;
						orderReduce.cndOperDate = dtSysTime;
						orderReduce.cnvcOperID = operLog.cnvcOperID;
						orderReduce.cnvcReduceState = "0";
						orderReduce.cnnReduceCount = decimal.Parse(drOrderReduce["cnnOrderCount"].ToString());
						EntityMapping.Create(orderReduce, trans);
					}
					
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "订单流水："+strOrderSerialNo;
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

		//订单添加产品
		public void AddDetail(string strOrderSerialNo,OperLog operLog,DataTable dtDetail)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);
					foreach(DataRow drDetail in dtDetail.Rows)
					{
						OrderBookDetail detail = new OrderBookDetail(drDetail);
						detail.cnnOrderSerialNo = decimal.Parse(strOrderSerialNo);
						//detail.cnvcOperID = operLog.cnvcOperID;
						//detail.cndOperDate = dtSysTime;
						EntityMapping.Create(detail, trans);
					}
					
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "订单流水："+strOrderSerialNo;
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

		//订单更新产品
		public void UpdateDetail(OrderBookDetail detail,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					OrderBookDetail oldDetail = new OrderBookDetail();
					oldDetail.cnnOrderSerialNo = detail.cnnOrderSerialNo;
					oldDetail.cnvcInvCode = detail.cnvcInvCode;
					oldDetail = EntityMapping.Get(oldDetail, trans) as OrderBookDetail;

					oldDetail.cnnOrderCount = detail.cnnOrderCount;
					EntityMapping.Update(oldDetail, trans);
					
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "订单流水："+detail.cnnOrderSerialNo.ToString();
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

		//订单删除产品
		public void DeleteDetail(OrderBookDetail detail,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					
					EntityMapping.Delete(detail, trans);

					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "订单流水："+detail.cnnOrderSerialNo.ToString()+"，产品编码："+detail.cnvcInvCode;
					EntityMapping.Create(operLog, trans);		
					//EntityMapping.Create(busiLog, trans);					
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
	}
}
