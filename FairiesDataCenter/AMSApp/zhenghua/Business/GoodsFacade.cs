using System;
using System.Data;
using System.Data.SqlClient;
using AMSApp.zhenghua.DataAccess;
using AMSApp.zhenghua.Common;
using AMSApp.zhenghua.QueryArgs;
using AMSApp.zhenghua.Entity;
using System.Collections;
using System.IO;
namespace AMSApp.zhenghua.Business
{
	/// <summary>
	/// GoodsFacade 的摘要说明。
	/// </summary>
	public class GoodsFacade
	{
		public GoodsFacade()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}


		//入库
		public void ProduceCheck(DataTable dtProduce,OperLog operLog,string strProduceDeptID,string strProduceSerialNo)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					string strComments = "";
					foreach(DataRow drProduce in dtProduce.Rows)
					{
						ProduceCheckLog check = new ProduceCheckLog(drProduce);
						check.cnvcOperID = operLog.cnvcOperID;
						check.cnvcProduceDeptID = strProduceDeptID;
						check.cndOperDate = dtSysTime;

						ProduceCheckLog check2 = new ProduceCheckLog();
						check2.cnnProduceSerialNo = check.cnnProduceSerialNo;
						check2.cnvcInvCode = check.cnvcInvCode;
						check2 = EntityMapping.Get(check2,trans) as ProduceCheckLog;
						if(check2 == null)
							EntityMapping.Create(check, trans);
						else
						{
							check2.cnnCheckCount = check.cnnCheckCount;
							check2.cnvcOperID = operLog.cnvcOperID;
							check2.cndOperDate = dtSysTime;
							check2.cnnTeamID = check.cnnTeamID;
							check2.cnnProducerID = check.cnnProducerID;
							EntityMapping.Update(check2,trans);

						}
						strComments+= "["+check.cnvcInvCode+":"+check.cnnCheckCount.ToString()+"]";
					}
					ProduceLog pl = new ProduceLog();
					pl.cnnProduceSerialNo = decimal.Parse(strProduceSerialNo);
					pl = EntityMapping.Get(pl, trans) as ProduceLog;
					if(pl == null)
						throw new Exception("生产数据异常");
					pl.cnvcProduceState = "4";
					pl.cnvcOperID = operLog.cnvcOperID;
					pl.cndOperDate = dtSysTime;
					EntityMapping.Update(pl, trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产流水："+strProduceSerialNo+strComments;
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

		//更新
		public void UpdateProduceCheck(ProduceCheckLog check,OperLog operLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					ProduceCheckLog oldCheck = new ProduceCheckLog();
					oldCheck.cnnProduceSerialNo = check.cnnProduceSerialNo;
					oldCheck.cnvcInvCode = check.cnvcInvCode;
					oldCheck = EntityMapping.Get(oldCheck, trans) as ProduceCheckLog;
					if(oldCheck == null)
						throw new Exception("未找到指定产品生产库存");
					oldCheck.cnnCheckCount = check.cnnCheckCount;
					oldCheck.cnnTeamID = check.cnnTeamID;
					oldCheck.cnnProducerID = check.cnnProducerID;
					oldCheck.cnvcOperID = check.cnvcOperID;
					oldCheck.cndOperDate = dtSysTime;
					EntityMapping.Update(oldCheck, trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产流水："+check.cnnProduceSerialNo.ToString()+"["+check.cnvcInvCode+":"+check.cnnCheckCount.ToString()+"]";
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

		public void AddAssignLog(ProduceLog produceLog,OperLog operLog,string strwhhouse)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					OrderSerialNo serialNo = new OrderSerialNo();
					serialNo.cnvcFill = "0";
					serialNo.cnnSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));
					
					ProduceLog oldLog = new ProduceLog();
					oldLog.cnnProduceSerialNo = produceLog.cnnProduceSerialNo;
					oldLog = EntityMapping.Get(oldLog, trans) as ProduceLog;
					if(oldLog == null)
					{
						throw new Exception("生产计划不存在");
					}
					if(oldLog.cnvcProduceState == "6")
						throw new Exception("已分货");
					if(oldLog.cnvcProduceState == "7")
						throw new Exception("已分货出库");
					oldLog.cnvcOperID = produceLog.cnvcOperID;
					oldLog.cndOperDate = dtSysTime;
					
					AssignLog assign = new AssignLog();
					assign.cndOperDate = dtSysTime;
					assign.cnnAssignSerialNo = serialNo.cnnSerialNo;
					assign.cnnProduceSerialNo = oldLog.cnnProduceSerialNo;
					assign.cnvcShipOperID = produceLog.cnvcOperID;
					assign.cnvcShipDeptID = oldLog.cnvcProduceDeptID;
					assign.cnvcOperID = produceLog.cnvcOperID;
					assign.cndShipDate = oldLog.cndProduceDate;
					//订单数据
					string strOrderSql = @"select * from tbOrderBook where cnnOrderSerialNo in 
						(select cnnOrderSerialNo from tbProduceOrderLog where cnnProduceSerialNo="+oldLog.cnnProduceSerialNo.ToString()+")";
					DataTable dtOrder = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strOrderSql);
					string strOrderDetailSql = @"select * from tbOrderBookDetail where cnnOrderSerialNo in 
						(select cnnOrderSerialNo from tbProduceOrderLog where cnnProduceSerialNo="+oldLog.cnnProduceSerialNo.ToString()+")";
					DataTable dtOrderDetail = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strOrderDetailSql);
					DataTable dtDept = SingleTableQuery.ExcuteQuery("tbDept",trans);
					//生产盘点数据
					string strCheckSql = "select * "
						+" from tbProduceCheckLog where cnnProduceSerialNo=" +
					                     oldLog.cnnProduceSerialNo.ToString()+" and cnnCheckCount>0 and cnbInWh=1";
					DataTable dtProduce = SqlHelper.ExecuteDataTable(trans, CommandType.Text, strCheckSql);
					#region 分货不按可用量分
					string strwhsql = "SELECT cnvcinvcode,sum(cnnAvaQuantity) as cnnwhcount FROM tbCurrentStock "
+" WHERE cnvcwhcode='"+strwhhouse+"' AND cnvcStopFlag='0'  and CONVERT(char(10),isnull(cndExpDate,''),121)>=CONVERT(char(10),getdate(),121)  "
+" group by cnvcinvcode";
					DataTable dtwh = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strwhsql);
					

					Helper.DataTableConvert(dtProduce,"cnvcInvCode","cnnCheckCount",dtwh,"cnvcinvcode","cnnwhcount","");
					#endregion
					//订单按产品分类汇总
					//分配 外订订单有限分配
					Helper.DataTableConvert(dtOrderDetail,"cnnOrderSerialNo","cnvcOrderType",dtOrder,"cnnOrderSerialNo","cnvcOrderType","");
					Helper.DataTableConvert(dtOrderDetail,"cnnOrderSerialNo","cnvcOrderDeptID",dtOrder,"cnnOrderSerialNo","cnvcOrderDeptID","");
					Helper.DataTableConvert(dtOrderDetail,"cnnOrderSerialNo","cndShipDate",dtOrder,"cnnOrderSerialNo","cndShipDate","");
					Helper.DataTableConvert(dtOrderDetail,"cnvcOrderDeptID","cnnPriority",dtDept,"cnvcDeptID","cnnPriority","");
					//
					dtOrderDetail.Columns.Add("cnnAssignCount");
					foreach(DataRow dr in dtOrderDetail.Rows)
					{
						dr["cnnAssignCount"] = 0;
					}

					DataView dvOrder = new DataView(dtOrderDetail);
					dvOrder.Sort = "cnvcOrderType desc,cnnPriority asc,cnnOrderSerialNo asc";
					Hashtable hOrderSerialNo = new Hashtable();
					foreach(DataRowView dv in dvOrder)
					{
						string strOrderSerialNo = dv["cnnOrderSerialNo"].ToString();
						string strProductCode = dv["cnvcInvCode"].ToString();
						string strOrderCount = dv["cnnOrderCount"].ToString();
						string strAssignCount = dv["cnnAssignCount"].ToString();
						decimal dOrderCount = decimal.Parse(strOrderCount);
						decimal dAssignCount = Convert.ToDecimal(strAssignCount);
						string strOrderType = dv["cnvcOrderType"].ToString();
						
						DataRow[] drProduces = dtProduce.Select("cnvcInvCode='"+strProductCode+"'");
						if(drProduces.Length == 0) continue; //throw new Exception(strProductCode+"无可分货量");
							//						if(drProduces.Length > 0)
							//						{
						
							DateTime dtMDate = Convert.ToDateTime(drProduces[0]["cndMDate"].ToString());
						DateTime dtExpDate = Convert.ToDateTime(drProduces[0]["cndExpDate"].ToString());
						string strSumCount = drProduces[0]["cnnCheckCount"].ToString();
						decimal dSumCount = decimal.Parse(strSumCount);
						string strSumAssign = drProduces[0]["cnnAssignCount"].ToString();
						decimal dSumAssign = decimal.Parse(strSumAssign);
						//if(dSumCount <=0) throw new Exception(strProductCode+"无可分货量");
						//						if(dSumCount > 0)
						//						{
						AssignDetail assignDetail = new AssignDetail();
						assignDetail.cnnAssignSerialNo = assign.cnnAssignSerialNo;
						assignDetail.cnnProduceSerialNo = assign.cnnProduceSerialNo;
						assignDetail.cnnOrderSerialNo = decimal.Parse(strOrderSerialNo);
						assignDetail.cnvcInvCode = strProductCode;
						assignDetail.cndMdate = dtMDate;
						assignDetail.cndExpDate = dtExpDate;

						if(dSumCount >= dOrderCount-dAssignCount)
						{									
							assignDetail.cnnOrderCount = dOrderCount;
							assignDetail.cnnAssignCount = dOrderCount-dAssignCount;
							drProduces[0]["cnnCheckCount"] = dSumCount - dOrderCount+dAssignCount;
							drProduces[0]["cnnAssignCount"] = dSumAssign+dOrderCount-dAssignCount;
						}
						else
						{
							if(strOrderType == "WDO" || strOrderType == "WDOSELF")
							{
								throw new Exception("订单流水为"+dv["cnnOrderSerialNo"].ToString()+"的外订定单"+strProductCode+"不能满足分配");
							}
							assignDetail.cnnOrderCount = dOrderCount;
							assignDetail.cnnAssignCount = dSumCount;
							drProduces[0]["cnnCheckCount"] = 0;
							drProduces[0]["cnnAssignCount"] = dSumAssign+dSumCount;
						}
						
						EntityMapping.Create(assignDetail, trans);
						if(!hOrderSerialNo.Contains(dv["cnnOrderSerialNo"].ToString()))
						{
							hOrderSerialNo.Add(dv["cnnOrderSerialNo"].ToString(), dv["cnnOrderSerialNo"].ToString());
							assign.cnnOrderSerialNo = decimal.Parse(dv["cnnOrderSerialNo"].ToString());
							assign.cnvcReceiveDeptID = dv["cnvcOrderDeptID"].ToString();
							assign.cndShipDate = DateTime.Parse(dv["cndShipDate"].ToString());
							EntityMapping.Create(assign, trans);
						}
						
						//								string strOrderBookDetail = "update tbOrderBookDetail set cnnAssignCount="+Convert.ToString(assignDetail.cnnAssignCount+dAssignCount)+" where cnnOrderSerialNo="+strOrderSerialNo+" and cnvcProductCode='"+strProductCode+"'";
						//								SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strOrderBookDetail);
						//						}
							
							
							
						//						}
						
					}
					foreach(DataRow drProduce in dtProduce.Rows)
					{
						ProduceCheckLog check = new ProduceCheckLog(drProduce);
						string strSql = "update tbProduceCheckLog set cnnAssignCount="+check.cnnAssignCount.ToString()+" where cnnProduceSerialNo="+check.cnnProduceSerialNo.ToString()+" and cnvcInvCode='"+check.cnvcInvCode+"'";
						SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strSql);
					}
					//更新订单生产计划状态
					string strUpdateOrder = "update tbOrderBook set cnvcOrderState='2' "
					                        +
					                        " where cnnOrderSerialNo in (select cnnOrderSerialNo from tbProduceOrderLog where cnvcType='0' and cnnProduceSerialNo="+oldLog.cnnProduceSerialNo+") ";
					SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strUpdateOrder);
					//更新生产计划状态 
					
					oldLog.cnvcProduceState = "6";
					EntityMapping.Update(oldLog, trans);
					
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "分货流水："+produceLog.cnnProduceSerialNo.ToString();
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


		public void DeleteAssignLog(ProduceLog produceLog,OperLog operLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					OrderSerialNo serialNo = new OrderSerialNo();
					serialNo.cnvcFill = "0";
					serialNo.cnnSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));
					
					ProduceLog oldLog = new ProduceLog();
					oldLog.cnnProduceSerialNo = produceLog.cnnProduceSerialNo;
					oldLog = EntityMapping.Get(oldLog, trans) as ProduceLog;
					if(oldLog == null)
					{
						throw new Exception("生产计划不存在");
					}
					if(oldLog.cnvcProduceState != "6")
						throw new Exception("生产计划不在已分货状态，不能清除分货数据");
					if(oldLog.cnvcProduceState == "7")
						throw new Exception("已分货出库");
					oldLog.cnvcOperID = produceLog.cnvcOperID;
					oldLog.cndOperDate = dtSysTime;
					
					//清除分货数据
					string strsql = "delete from tbassignlog where cnnproduceserialno="+produceLog.cnnProduceSerialNo.ToString();
//					DataTable dtal = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strsql);
//					if(dtal.Rows.Count == 0) throw new Exception("未找到分货数据");
//					Entity.AssignLog al = new AssignLog(dtal);
//					EntityMapping.Delete(al,trans);
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql);

					strsql = "delete from tbassigndetail where cnnproduceserialno="+produceLog.cnnProduceSerialNo.ToString();
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql);
					strsql = "update tbproducechecklog set cnnassigncount=0 where cnnproduceserialno="+produceLog.cnnProduceSerialNo.ToString();
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql);
					//更新订单生产计划状态
					string strUpdateOrder = "update tbOrderBook set cnvcOrderState='1' "
						+
						" where cnnOrderSerialNo in (select cnnOrderSerialNo from tbProduceOrderLog where cnvcType='0' and cnnProduceSerialNo="+oldLog.cnnProduceSerialNo+") ";
					SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strUpdateOrder);
					//更新生产计划状态 
					
					oldLog.cnvcProduceState = "5";
					EntityMapping.Update(oldLog, trans);
					
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产流水："+produceLog.cnnProduceSerialNo.ToString();
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
		public void UpdateAssignLog(AssignDetail detail,OperLog operLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				SqlTransaction trans = conn.BeginTransaction();
				try
				{

					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);


					Entity.ProduceCheckLog pcl = new ProduceCheckLog();
					pcl.cnnProduceSerialNo = detail.cnnProduceSerialNo;
					pcl.cnvcInvCode = detail.cnvcInvCode;
					pcl = EntityMapping.Get(pcl,trans) as ProduceCheckLog;
					if(pcl == null) throw new Exception("无法定位生产");
					
					AssignDetail detailOld = EntityMapping.Get(detail, trans) as AssignDetail;


					pcl.cnnAssignCount = pcl.cnnAssignCount + detail.cnnAssignCount - detailOld.cnnAssignCount;
					EntityMapping.Update(pcl,trans);

//					if(detail.cnnAssignCount != detailOld.cnnAssignCount)
//					{
						
//						OrderSerialNo serialNo = new OrderSerialNo();
//						serialNo.cnvcFill = "0";
//						serialNo.cnnSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));
					string strComments = "";
						detailOld.cnnAssignCount = detail.cnnAssignCount;
						EntityMapping.Update(detailOld, trans);
						strComments = "["+detail.cnvcInvCode+":"+detail.cnnAssignCount+"]";
					//}

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "分货流水："+detail.cnnAssignSerialNo.ToString()+strComments;
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

		public void AssignOut(string strProduceSerialNo,OperLog operLog,string strWarehouse)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					string strsql1 = "SELECT * FROM tbProduceLog WHERE cnvcProduceState='6' and cnnProduceSerialNo="+strProduceSerialNo;
					DataTable dtProduceLog = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strsql1);
					//rr.cndMakeDate = dtSysTime;
					//EntityMapping.Create(rr,trans);
					if(dtProduceLog.Rows.Count == 0) throw new Exception("生产计划不在分货状态！");
					Entity.ProduceLog pl = new ProduceLog(dtProduceLog);
					
					string strsql2 = "select * from tbproducechecklog WHERE cnnAssignCount>0 and cnnproduceserialno="+ strProduceSerialNo;
					DataTable dtProduceCheckLog = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strsql2);
					if(dtProduceCheckLog.Rows.Count==0)
						throw new Exception("分货出库产品数量都为0");

					DataTable dtInv = SingleTableQuery.ExcuteQuery("tbInventory",trans);
					DataTable dtComputationUnit = SingleTableQuery.ExcuteQuery("tbComputationUnit",trans);

					string strsql3 = "select * from tbassignlog where cnnproduceserialno="+strProduceSerialNo;
					DataTable dtAssignLog = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strsql3);
					if(dtAssignLog.Rows.Count == 0) throw new Exception("未找到分货流水");
					Entity.AssignLog al = new AssignLog(dtAssignLog);

					Entity.RdRecord rr = new RdRecord();
					rr.cnvcRdCode = "RD010";
					rr.cnvcRdFlag = "0";
					rr.cnvcWhCode = strWarehouse;
					rr.cnvcDepID = pl.cnvcProduceDeptID;
					//rr.cnvcOperName = operLog.cnvcop
					rr.cnvcComments = "分货出库";
					rr.cnvcMaker = operLog.cnvcOperID;
					rr.cnnProorderID = pl.cnnProduceSerialNo;
					rr.cnvcState = "2";		
					rr.cndMakeDate = dtSysTime;
					long rrid = EntityMapping.Create(rr,trans);

					foreach(DataRow drProduceCheckLog in dtProduceCheckLog.Rows)
					{
						Entity.ProduceCheckLog pcl = new ProduceCheckLog(drProduceCheckLog);
						

						Entity.RdRecordDetail rrd = new RdRecordDetail();																		
						rrd.cnvcFlag = "0";
						rrd.cndExpDate = pcl.cndExpDate;//Convert.ToDateTime(this.txtProduceDate.Text).AddDays(pc.cnnDays).AddDays(Convert.ToDouble(txtDays.Text));//Convert.ToDateTime(this.txtExpDate.Text);
						rrd.cndMdate = pcl.cndMDate;//Convert.ToDateTime(this.txtProduceDate.Text);
						rrd.cnnRdID = Convert.ToDecimal(rrid);
						rrd.cnvcPOID = al.cnnAssignSerialNo.ToString();

						DataRow[] drinvs = dtInv.Select("cnvcInvCode='"+pcl.cnvcInvCode+"'");
						if(drinvs.Length == 0) throw new Exception(pcl.cnvcInvCode+"存货档案未设置");
						Entity.Inventory inv = new AMSApp.zhenghua.Entity.Inventory(drinvs[0]);


						rrd.cnvcInvCode = inv.cnvcInvCode;
						rrd.cnvcGroupCode = inv.cnvcGroupCode;
						rrd.cnvcComunitCode = inv.cnvcSTComUnitCode;

						DataRow[] drcus = dtComputationUnit.Select("cnvcGroupCode='"+inv.cnvcGroupCode+"' and cnbMainUnit=1");
						if(drcus.Length == 0) throw new Exception(inv.cnvcGroupCode+"未设置主计量单位");
						decimal dchangerate = Convert.ToDecimal(drcus[0]["cnichangrate"].ToString());
						DataRow[] drcus1 = dtComputationUnit.Select("cnvcGroupCode='"+inv.cnvcGroupCode+"' and cnvcComUnitCode='"+inv.cnvcSTComUnitCode+"'");
						decimal dchangerate_st = Convert.ToDecimal(drcus1[0]["cnichangrate"].ToString());

//						string strcssql = "SELECT * FROM tbCurrentStock WHERE cnvcWhCode='"+strWarehouse+"' AND cnvcInvCode='"+pcl.cnvcInvCode+"'";
//						DataTable dtcs = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strcssql);


						string strcssql = "SELECT * FROM tbCurrentStock WHERE cnvcWhCode='"+strWarehouse+"' AND cnvcInvCode='"+pcl.cnvcInvCode+"'"
							+ " and CONVERT(char(10),isnull(cndExpDate,''),121)>=CONVERT(char(10),getdate(),121) ";
						string strcssql2 = "SELECT isnull(sum(cnnAvaQuantity),0) FROM tbCurrentStock WHERE cnvcWhCode='"+strWarehouse+"' AND cnvcInvCode='"+pcl.cnvcInvCode+"'"
							+ " and CONVERT(char(10),isnull(cndExpDate,''),121)>=CONVERT(char(10),getdate(),121) ";
						DataTable dtcs = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strcssql);
						decimal davaquantity = Convert.ToDecimal(SqlHelper.ExecuteScalar(trans,CommandType.Text,strcssql2).ToString());

						if(dtcs.Rows.Count == 0)
						{
							throw new Exception(pcl.cnvcInvCode+"无库存");
						}
						if(davaquantity - pcl.cnnAssignCount*dchangerate_st/dchangerate<0)
							throw new Exception(pcl.cnvcInvCode+"库存不足");

//						if(cs.cnnAvaQuantity - pcl.cnnAssignCount<0)
//							throw new Exception(pcl.cnvcInvCode+"库存不足");
						decimal dhave=0;
						foreach(DataRow drcs in dtcs.Rows)
						{
							Entity.CurrentStock cs = new CurrentStock(drcs);
//							if(cs.cnnAvaQuantity - pcl.cnnAssignCount<0)
//								throw new Exception(pcl.cnvcInvCode+"库存不足");
//							cs.cnnAvaQuantity = cs.cnnAvaQuantity - pcl.cnnAssignCount;
//							cs.cnnQuantity = cs.cnnQuantity - pcl.cnnAssignCount;
//							EntityMapping.Update(cs,trans);


							if(cs.cnnAvaQuantity>pcl.cnnAssignCount*dchangerate_st/dchangerate-dhave)
							{
								cs.cnnAvaQuantity = cs.cnnAvaQuantity - pcl.cnnAssignCount*dchangerate_st/dchangerate;
								cs.cnnQuantity = cs.cnnQuantity - pcl.cnnAssignCount*dchangerate_st/dchangerate;
								EntityMapping.Update(cs,trans);								
								break;
							}
							else
							{
								cs.cnnAvaQuantity = 0;
								cs.cnnQuantity = 0;
								EntityMapping.Update(cs,trans);
								dhave += cs.cnnAvaQuantity;
							}							
						}
						rrd.cnnQuantity = pcl.cnnAssignCount*dchangerate_st/dchangerate;
						EntityMapping.Create(rrd,trans);
					}
					
					
					//string strsql = "update tbMakeDetail set cnbCollar=1 where cnnMakeSerialNo="+strMakeSerialNo;
					//SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql);

					string strsql4 = "update tbproducelog set cnvcproducestate='7' where cnnproduceserialno="+strProduceSerialNo;
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql4);
					
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产流水："+strProduceSerialNo;
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
		public void UpdatePriority(Dept dept,OperLog operLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					Dept oldDept = new Dept();
					oldDept.cnvcDeptID = dept.cnvcDeptID;
					oldDept = EntityMapping.Get(oldDept, trans) as Dept;
					if(oldDept == null)
						throw new Exception("未找到相应的部门");
					oldDept.cnnPriority = dept.cnnPriority;
					EntityMapping.Update(oldDept, trans);

					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "部门ID："+dept.cnvcDeptID;
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
	}
}
