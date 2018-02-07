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
	/// StorageFacade 的摘要说明。
	/// </summary>
	public class StorageFacade
	{
		public StorageFacade()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		public int AddRdRecordCom(string strBillType,OperLog operLog,RdRecord rd)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);
					
					string strmaxid=SqlHelper.ExecuteScalar(trans,CommandType.Text,"select isnull(max(cast(substring(cnvcCode,10,4) as int)),0) from tbRdRecord where cnvcRdCode='"+strBillType+"' and cnvcCode like '"+rd.cnvcCode.Substring(0,9)+"%'").ToString();
					int maxid=int.Parse(strmaxid);
					maxid++;
					if(maxid<10)
						rd.cnvcCode=rd.cnvcCode.Substring(0,9)+"000"+maxid.ToString();
					else if(maxid>=10&&maxid<100)
						rd.cnvcCode=rd.cnvcCode.Substring(0,9)+"00"+maxid.ToString();
					else if(maxid>=100&&maxid<1000)
						rd.cnvcCode=rd.cnvcCode.Substring(0,9)+"0"+maxid.ToString();
					else if(maxid>=1000&&maxid<9999)
						rd.cnvcCode=rd.cnvcCode.Substring(0,9)+maxid.ToString();
					else
						return 0;

					rd.cndMakeDate=dtSysTime;
					EntityMapping.Create(rd,trans);

					operLog.cndOperDate = dtSysTime;
					switch(strBillType)
					{
						case "RD001":
							operLog.cnvcComments = "添加采购入库单内容："+rd.cnvcCode;
							break;
						case "RD002":
							operLog.cnvcComments = "添加采购退货单内容："+rd.cnvcCode;
							break;
						case "RD007":
							operLog.cnvcComments = "添加分货入库单内容："+rd.cnvcCode;
							break;
					}
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

		public int UpdateRdRecordCom(string strBillType,OperLog operLog,RdRecord rd)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					RdRecord oldrd =new RdRecord();
					oldrd.cnnRdID = rd.cnnRdID;

					oldrd = EntityMapping.Get(oldrd,trans) as RdRecord;
					oldrd.cnvcIsLsQuery = rd.cnvcIsLsQuery;
					oldrd.cndARVDate=rd.cndARVDate;
					oldrd.cnvcShipAddress=rd.cnvcShipAddress;
					oldrd.cnvcARVAddress=rd.cnvcARVAddress;
					oldrd.cnvcComments=rd.cnvcComments;
					oldrd.cnvcModer=rd.cnvcModer;
					oldrd.cndModDate=dtSysTime;
					EntityMapping.Update(oldrd,trans);

					operLog.cndOperDate = dtSysTime;
					switch(strBillType)
					{
						case "RD001":
							operLog.cnvcComments = "采购入库单内容修改："+oldrd.cnvcCode;
							break;
						case "RD002":
							operLog.cnvcComments = "采购退货单内容修改："+oldrd.cnvcCode;
							break;
						case "RD005":
							operLog.cnvcComments = "调拨出库单内容修改："+oldrd.cnvcCode;
							break;
						case "RD006":
							operLog.cnvcComments = "调拨入库单内容修改："+oldrd.cnvcCode;
							break;
					}
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

		public int AddPoStockEnterDetail(OperLog operLog,string strRdid,string strPoid,string strPrvdCode)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					string strsql="insert into tbRdRecordDetail select "+strRdid+",'"+strPoid+"',0,'"+strPrvdCode+"',cnvcGoodsCode,cnnStockCountSum,cnnStockPrice,cnnStockFeeSum,0,cnvcGroupCode,cnvcStockUnit,'','0','',null,null,null,null";
					strsql+=" from tbPoStockSum where cnvcPoID='"+strPoid+"'";
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql);
			
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "添加关联采购订单："+strRdid+"|"+strPoid;
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

		public int UpdatePoStockEnterDetail(OperLog operLog,RdRecordDetail rrd)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					RdRecordDetail oldrrd =new RdRecordDetail();
					oldrrd.cnnAutoID = rrd.cnnAutoID;

					oldrrd = EntityMapping.Get(oldrrd,trans) as RdRecordDetail;
					oldrrd.cnnAutoID=rrd.cnnAutoID;
					oldrrd.cnnPrice=rrd.cnnPrice;
					oldrrd.cnnQuantity=rrd.cnnQuantity;
					oldrrd.cnnCost=Math.Round(oldrrd.cnnPrice*rrd.cnnQuantity,2);
					oldrrd.cnnExtraCost=rrd.cnnExtraCost;
					oldrrd.cndMdate=rrd.cndMdate;
					oldrrd.cnnMassDate=rrd.cnnMassDate;
					oldrrd.cnvcMassUnit=rrd.cnvcMassUnit;
					oldrrd.cndExpDate=rrd.cndExpDate;
					oldrrd.cnvcCommens=rrd.cnvcCommens;
					EntityMapping.Update(oldrrd,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "采购入库单子表修改："+oldrrd.cnnAutoID;
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

		public int PoStockEnterExecEntering(OperLog operLog,string strWhcode,RdRecordDetail rrd)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					string maxautoid=SqlHelper.ExecuteScalar(trans,CommandType.Text,"select isnull(max(cnnAutoID),0) from tbCurrentStock where cnvcWhCode='"+strWhcode+"' and cnvcInvCode='"+rrd.cnvcInvCode+"' and cndMDate='"+rrd.cndMdate.ToShortDateString()+"' and cndExpDate='"+rrd.cndExpDate.ToShortDateString()+"'").ToString();
					string unitrate=SqlHelper.ExecuteScalar(trans,CommandType.Text,"select cniChangRate from tbComputationUnit where cnvcComunitCode=(select cnvcComunitCode from tbRdRecordDetail where cnnAutoID="+rrd.cnnAutoID.ToString()+")").ToString();
					if(maxautoid=="0")
					{
						string strsql10="insert into tbCurrentStock select '"+strWhcode+"',a.cnvcInvCode,a.cnnQuantity*b.cniChangRate,0,0,'0',0,a.cndMDate,0,0,0,a.cnnQuantity*b.cniChangRate,0,'0',0,0,a.cndExpDate from tbRdRecordDetail a,tbComputationUnit b where a.cnnAutoID="+rrd.cnnAutoID.ToString()+" and a.cnvcComunitCode=b.cnvcComunitCode";
						SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql10);

						operLog.cndOperDate = dtSysTime;
						operLog.cnvcComments = "采购入库添加库存："+strWhcode+"|"+rrd.cnvcInvCode;
						EntityMapping.Create(operLog, trans);
					}
					else
					{
						string strsql11="update tbCurrentStock set cnnQuantity=a.cnnQuantity+b.cnnQuantity*c.cniChangRate,cnnAvaQuantity=a.cnnAvaQuantity+b.cnnQuantity*c.cniChangRate from tbCurrentStock a,tbRdRecordDetail b,tbComputationUnit c";
						strsql11+=" where a.cnvcWhCode='"+strWhcode+"' and a.cnvcInvCode=b.cnvcInvCode and a.cndMDate=b.cndMDate and a.cndExpDate=b.cndExpDate and b.cnnAutoID="+rrd.cnnAutoID.ToString()+" and b.cnvcComunitCode=c.cnvcComunitCode";
						SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql11);

						operLog.cndOperDate = dtSysTime;
						operLog.cnvcComments = "采购入库更新库存："+strWhcode+"|"+rrd.cnvcInvCode;
						EntityMapping.Create(operLog, trans);
					}

					string strsql="update tbPoStockSum set cnnArriveCount=a.cnnArriveCount+b.cnnQuantity,cnnArriveFee=a.cnnArriveFee+b.cnnCost+b.cnnExtraCost from tbPoStockSum a,tbRdRecordDetail b";
					strsql+=" where b.cnnRdID="+rrd.cnnRdID.ToString()+" and a.cnvcPOID=b.cnvcPOID and a.cnvcGoodsCode=b.cnvcInvCode and b.cnvcInvCode='"+rrd.cnvcInvCode+"'";
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql);

					string strsql4="update tbPoStockSum set cnnFinallyRate=a.cnnArriveCount/a.cnnStockCountSum from tbPoStockSum a,tbRdRecordDetail b where b.cnnRdID="+rrd.cnnRdID.ToString()+" and a.cnvcPOID=b.cnvcPOID and a.cnvcGoodsCode=b.cnvcInvCode";
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql4);

					string strsql6="update tbPoStockDetail set cnvcRowState='2' where cnvcPoID+cnvcGoodsCode in(select cnvcPoID+cnvcInvCode from tbRdRecordDetail where cnnAutoID="+rrd.cnnAutoID.ToString()+")";
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql6);

					string strsql5="update tbRdRecordDetail set cnvcFlag='1' where cnnAutoID="+rrd.cnnAutoID.ToString();
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql5);

					string existunfin=SqlHelper.ExecuteScalar(trans,CommandType.Text,"select count(*) from tbRdRecordDetail where cnnRdID="+rrd.cnnRdID.ToString()+" and cnvcFlag='0'").ToString();
					if(existunfin=="0")
					{
						string strsql3="update tbRdRecord set cnvcState='1' where cnnRdID="+rrd.cnnRdID.ToString();
						SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql3);
					}
					string existPounfin=SqlHelper.ExecuteScalar(trans,CommandType.Text,"select count(*) from tbPoStockDetail where cnvcPoID='"+rrd.cnvcPOID.ToString()+"' and cnvcRowState<>'2'").ToString();
					if(existPounfin=="0")
					{
						string strsql7="update tbPoStockMain set cnvcPoState='2' where cnvcPoID='"+rrd.cnvcPOID+"'";
						SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql7);
					}

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

		public int AddPoStockReturnDetail(OperLog operLog,string strselect)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					string strsql="insert into tbRdRecordDetail "+strselect;
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql);
			
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "添加采购退货品："+strselect;
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

		public int UpdatePoStockReturnDetail(OperLog operLog,RdRecordDetail rrd)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					RdRecordDetail oldrrd =new RdRecordDetail();
					oldrrd.cnnAutoID = rrd.cnnAutoID;

					oldrrd = EntityMapping.Get(oldrrd,trans) as RdRecordDetail;
					oldrrd.cnnAutoID=rrd.cnnAutoID;
					oldrrd.cnnQuantity=rrd.cnnQuantity;
					oldrrd.cnnCost=oldrrd.cnnPrice*rrd.cnnQuantity;
					oldrrd.cnvcCommens=rrd.cnvcCommens;
					EntityMapping.Update(oldrrd,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "采购退货单子表修改："+oldrrd.cnnAutoID;
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

		public int PoStockReturnExecReturning(OperLog operLog,string strWhcode,DataTable dtsum,string strRdid)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					for(int i=0;i<dtsum.Rows.Count;i++)
					{
						DataTable dttmpwh=SqlHelper.ExecuteDataTable(trans,CommandType.Text,"select cnnAutoID,cnnQuantity from tbCurrentStock where cnvcWhCode='"+strWhcode+"' and cnvcInvCode='"+dtsum.Rows[i]["存货编号"].ToString()+"' and cndMdate='"+dtsum.Rows[i]["生产日期"].ToString()+"' and cndExpDate='"+dtsum.Rows[i]["过期日期"].ToString()+"'");
						string unitrate=SqlHelper.ExecuteScalar(trans,CommandType.Text,"select cniChangRate from tbComputationUnit where cnvcComunitCode='"+dtsum.Rows[i]["单位编码"].ToString()+"'").ToString();
						if(dttmpwh==null||dttmpwh.Rows.Count==0)
						{
							throw new Exception("仓库中无此货品信息！"+strWhcode+"|"+dtsum.Rows[i]["存货编号"].ToString());
						}
						else if(decimal.Parse(dttmpwh.Rows[0]["cnnQuantity"].ToString())<Math.Round(decimal.Parse(dtsum.Rows[i]["数量"].ToString())*decimal.Parse(unitrate),2))
						{
							throw new Exception("仓库中当前结存数量小于退货数量！"+strWhcode+"|"+dtsum.Rows[i]["存货编号"].ToString());
						}
						else
						{
							Entity.CurrentStock cs=new CurrentStock();
							cs.cnnAutoID=int.Parse(dttmpwh.Rows[0]["cnnAutoID"].ToString());
							cs=EntityMapping.Get(cs,trans) as CurrentStock;
							cs.cnnQuantity=cs.cnnQuantity-Math.Round(decimal.Parse(dtsum.Rows[i]["数量"].ToString())*decimal.Parse(unitrate),2);
							cs.cnnAvaQuantity=cs.cnnAvaQuantity-Math.Round(decimal.Parse(dtsum.Rows[i]["数量"].ToString())*decimal.Parse(unitrate),2);
							EntityMapping.Update(cs,trans);

							string strsql1="update tbRdRecordDetail set cnvcFlag='1' where cnnRdID="+strRdid;
							SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql1);

							operLog.cndOperDate = dtSysTime;
							operLog.cnvcComments = "采购退货更新库存："+cs.cnvcWhCode+"|"+cs.cnvcInvCode;
							EntityMapping.Create(operLog, trans);
						}
					}
					
					string strsql3="update tbRdRecord set cnvcState='1' where cnnRdID="+strRdid;
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql3);

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

		public int InventoryCostAccount(OperLog operLog,string strtype)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					string strsql="";
					if(strtype=="mate")
					{
						operLog.cnvcComments = "原材料成本核算成功";

						strsql="exec sp_MaterialCostAccount";
						SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql);
					}

					if(strtype=="prod")
					{
						operLog.cnvcComments = "成品成本核算成功";

						strsql="exec sp_ProductCostAccount";
						SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql);
					}

					operLog.cndOperDate = dtSysTime;
					EntityMapping.Create(operLog, trans);
					trans.Commit();
				}
				catch(SqlException sex)
				{
					trans.Rollback();
					LogAdapter.WriteDatabaseException(sex);

					if(strtype=="mate")
					{
						operLog.cnvcComments = "原材料成本核算失败";
					}

					if(strtype=="prod")
					{
						operLog.cnvcComments = "成品成本核算失败";
					}
					operLog.cndOperDate = DateTime.Now;
					EntityMapping.Create(operLog,conn);
					return -1;
				}
				catch(Exception ex)
				{
					trans.Rollback();
					LogAdapter.WriteFeaturesException(ex);

					if(strtype=="mate")
					{
						operLog.cnvcComments = "原材料成本核算失败";
					}

					if(strtype=="prod")
					{
						operLog.cnvcComments = "成品成本核算失败";
					}
					operLog.cndOperDate = DateTime.Now;
					EntityMapping.Create(operLog,conn);
					return -1;
				}
				finally
				{
					ConnectionPool.ReturnConnection(conn);
				}
				return 1;
			}
		}

		public int AddStorageCheckLog(StorageCheckLog scl,string strCheckNobe,out string strcheckno)
		{
			strcheckno="";
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);
					
					string strtmp = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select count(cnvcCheckNo) from tbStorageCheckLog where cnvcDeptID='"+scl.cnvcDeptID+"' and cnvcWhCode='"+scl.cnvcWhCode+"' and cnvcCheckNo like '"+strCheckNobe+"%' and cnvcFlag='0'").ToString();
					if(strtmp=="0")
					{
						string strmaxid=SqlHelper.ExecuteScalar(trans,CommandType.Text,"select isnull(max(cast(substring(cnvcCheckNo,9,5) as int)),0) from tbStorageCheckLog where cnvcDeptID='"+scl.cnvcDeptID+"' and cnvcWhCode='"+scl.cnvcWhCode+"' and cnvcCheckNo like '"+strCheckNobe+"%'").ToString();
						int maxid=int.Parse(strmaxid);
						maxid++;
						if(maxid<10)
							scl.cnvcCheckNo=strCheckNobe+"0000"+maxid.ToString();
						else if(maxid>=10&&maxid<100)
							scl.cnvcCheckNo=strCheckNobe+"000"+maxid.ToString();
						else if(maxid>=100&&maxid<1000)
							scl.cnvcCheckNo=strCheckNobe+"00"+maxid.ToString();
						else if(maxid>=1000&&maxid<9999)
							scl.cnvcCheckNo=strCheckNobe+"0"+maxid.ToString();
						else if(maxid>=10000&&maxid<99999)
							scl.cnvcCheckNo=strCheckNobe+maxid.ToString();
					}
					else
					{
						scl.cnvcCheckNo=SqlHelper.ExecuteScalar(trans, CommandType.Text, "select top 1 cnvcCheckNo from tbStorageCheckLog where cnvcDeptID='"+scl.cnvcDeptID+"' and cnvcWhCode='"+scl.cnvcWhCode+"' and cnvcCheckNo like '"+strCheckNobe+"%' and cnvcFlag='0'").ToString();
					}

					if(scl.cnvcCheckNo.Length==13)
					{
						string strisInvExsit = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select count(*) from tbStorageCheckLog where cnvcDeptID='"+scl.cnvcDeptID+"' and cnvcWhCode='"+scl.cnvcWhCode+"' and cnvcCheckNo='"+scl.cnvcCheckNo+"' and cnvcInvCode='"+scl.cnvcInvCode+"'").ToString();
						if(strisInvExsit!="0")
						{
							strcheckno=scl.cnvcCheckNo;
							trans.Commit();
							return -1;
						}
						else
						{
							strcheckno=scl.cnvcCheckNo;
							scl.cndOperDate=dtSysTime;
							EntityMapping.Create(scl,trans);

							trans.Commit();
							return 1;
						}
					}
					else
					{
						trans.Commit();
						return 0;
					}
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
			}
		}

		public int UpdateStorageCheckLog(StorageCheckLog scl,string strflag)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);
					
					StorageCheckLog oldscl =new StorageCheckLog();
					oldscl.cnnSerialNo = scl.cnnSerialNo;

					oldscl = EntityMapping.Get(oldscl,trans) as StorageCheckLog;
					oldscl.cnnCheckCount=scl.cnnCheckCount;
					oldscl.cndOperDate=dtSysTime;
					oldscl.cnvcOperName=scl.cnvcOperName;
					if(strflag=="up_date")
					{
						oldscl.cndMdate=scl.cndMdate;
						oldscl.cndExpDate=scl.cndExpDate;
					}
					EntityMapping.Update(oldscl,trans);

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

		public int StorageCheckLogConfirm(OperLog operLog,string strCheckNo,string strWhcode,string strDeptID)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);
					
					string sql1="update tbCurrentStock set cnnQuantity=b.cnnCheckCount*c.cniChangRate,cnnAvaQuantity=b.cnnCheckCount*c.cniChangRate from tbCurrentStock a,tbStorageCheckLog b,tbComputationUnit c";
					sql1+=" where b.cnvcCheckNo='"+strCheckNo+"' and b.cnvcWhCode='"+strWhcode+"' and b.cnvcDeptID='"+strDeptID+"' and a.cnvcWhCode=b.cnvcWhCode and a.cnvcInvCode=b.cnvcInvCode and convert(char(8),a.cndMdate,112)=convert(char(8),b.cndMdate,112) and convert(char(8),a.cndExpDate,112)=convert(char(8),b.cndExpDate,112) and b.cnvcUnitCode=c.cnvcComunitCode";
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,sql1);

					string sql2="insert into tbCurrentStock select distinct cnvcWhCode,cnvcInvCode,cnnCheckCount*b.cniChangRate,0,0,'0',0,a.cndMdate,0,0,0,cnnCheckCount*b.cniChangRate,0,'0',0,0,a.cndExpDate from tbStorageCheckLog a,tbComputationUnit b";
					sql2+=" where cnvcCheckNo='"+strCheckNo+"' and a.cnvcWhCode='"+strWhcode+"' and a.cnvcDeptID='"+strDeptID+"' and a.cnvcUnitCode=b.cnvcComunitCode and cnvcInvCode+convert(char(8),a.cndMdate,112)+convert(char(8),a.cndExpDate,112) not in(select cnvcInvCode+convert(char(8),cndMdate,112)+convert(char(8),cndExpDate,112) from tbCurrentStock where cnvcWhCode='"+strWhcode+"')";
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,sql2);

					string sql3="update tbStorageCheckLog set cnvcFlag='1' where cnvcCheckNo='"+strCheckNo+"' and cnvcWhCode='"+strWhcode+"' and cnvcDeptID='"+strDeptID+"'";
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,sql3);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "仓库库存盘点："+strWhcode+"|"+strCheckNo;
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

		public int StorageCheckClearConfirm(OperLog operLog,string strCheckNobe,string strWhcode,string strDeptID,string strOperName)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strCheckNo="";
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);
					
					string strmaxid=SqlHelper.ExecuteScalar(trans,CommandType.Text,"select isnull(max(cast(substring(cnvcCheckNo,9,5) as int)),0) from tbStorageCheckLog where cnvcDeptID='"+strDeptID+"' and cnvcWhCode='"+strWhcode+"' and cnvcCheckNo like '"+strCheckNobe+"%'").ToString();
					int maxid=int.Parse(strmaxid);
					maxid++;
					if(maxid<10)
						strCheckNo=strCheckNobe+"0000"+maxid.ToString();
					else if(maxid>=10&&maxid<100)
						strCheckNo=strCheckNobe+"000"+maxid.ToString();
					else if(maxid>=100&&maxid<1000)
						strCheckNo=strCheckNobe+"00"+maxid.ToString();
					else if(maxid>=1000&&maxid<9999)
						strCheckNo=strCheckNobe+"0"+maxid.ToString();
					else if(maxid>=10000&&maxid<99999)
						strCheckNo=strCheckNobe+maxid.ToString();

					if(strCheckNo.Length==13)
					{
						string sql1="update tbCurrentStock set cnnQuantity=0,cnnAvaQuantity=0 where cnvcWhCode='"+strWhcode+"' and cnvcInvCode+convert(char(8),cndMdate,112)+convert(char(8),cndExpDate,112)";
						sql1+=" not in (select cnvcInvCode+convert(char(8),cndMdate,112)+convert(char(8),cndExpDate,112) from tbStorageCheckLog where cnvcDeptID='"+strDeptID+"' and cnvcWhCode='"+strWhcode+"'";
						sql1+=" and cnvcCheckNo like '"+strCheckNobe+"%') and cnvcInvCode in(select cnvcInvCode from tbInventory where cnvcInvCCode in(select cnvcProductClassCode from tbProductClass where cnvcProductType='FINALPRODUCT'))";
						sql1+=" and cnnQuantity>0";
						SqlHelper.ExecuteNonQuery(trans,CommandType.Text,sql1);

						string sql2="insert into tbStorageCheckLog select '"+strCheckNo+"','"+strDeptID+"',a.cnvcWhCode,a.cnvcInvCode,cast(a.cnnQuantity/c.cniChangRate as numeric(18,2)) as cnnSysCount,0,b.cnvcSTComunitCode,";
						sql2+="'"+strOperName+"',getdate(),'1',convert(char(10),a.cndMdate,120) as cndMdate,convert(char(10),a.cndExpDate,120) as cndExpDate from tbCurrentStock a,tbInventory b,tbComputationUnit c";
						sql2+=" where a.cnnQuantity>0 and cnvcWhCode='"+strWhcode+"' and a.cnvcInvCode+convert(char(8),cndMdate,112)+convert(char(8),cndExpDate,112) not in (select cnvcInvCode+convert(char(8),cndMdate,112)+convert(char(8),cndExpDate,112)";
						sql2+=" from tbStorageCheckLog where cnvcDeptID='"+strDeptID+"' and cnvcWhCode='"+strWhcode+"' and cnvcCheckNo like '"+strCheckNobe+"%') and b.cnvcInvCCode in(select cnvcProductClassCode ";
						sql2+=" from tbProductClass where cnvcProductType='FINALPRODUCT') and a.cnvcInvCode=b.cnvcInvCode and b.cnvcSTComunitCode=c.cnvcComunitCode";
						SqlHelper.ExecuteNonQuery(trans,CommandType.Text,sql2);
					}

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "仓库库存清零盘点："+strWhcode+"|"+strCheckNo;
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

		public int AddRdRecordMove(OperLog operLog,RdRecord rdout,RdRecord rdin)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);
					
					string strmaxid=SqlHelper.ExecuteScalar(trans,CommandType.Text,"select isnull(max(cast(substring(cnvcCode,10,4) as int)),0) from tbRdRecord where cnvcCode like '"+rdout.cnvcCode.Substring(0,9)+"%'").ToString();
					int maxid=int.Parse(strmaxid);
					maxid++;
					if(maxid<10)
						rdout.cnvcCode=rdout.cnvcCode.Substring(0,9)+"000"+maxid.ToString();
					else if(maxid>=10&&maxid<100)
						rdout.cnvcCode=rdout.cnvcCode.Substring(0,9)+"00"+maxid.ToString();
					else if(maxid>=100&&maxid<1000)
						rdout.cnvcCode=rdout.cnvcCode.Substring(0,9)+"0"+maxid.ToString();
					else if(maxid>=1000&&maxid<9999)
						rdout.cnvcCode=rdout.cnvcCode.Substring(0,9)+maxid.ToString();
					else
						return 0;

					rdout.cndMakeDate=dtSysTime;
					EntityMapping.Create(rdout,trans);
					rdin.cndMakeDate=dtSysTime;
					rdin.cnvcCode=rdout.cnvcCode;
					EntityMapping.Create(rdin,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "添加调拨出库单内容："+rdout.cnnRdID+"；添加调拨入库单内容："+rdin.cnnRdID;
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

		public int AddRdRecordMoveDetail(OperLog operLog,ArrayList alrdout,ArrayList alrdin)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					for(int i=0;i<alrdout.Count;i++)
					{
						EntityMapping.Create((RdRecordDetail)alrdout[i],trans);
						operLog.cndOperDate = dtSysTime;
						operLog.cnvcComments = "添加调拨存货："+((RdRecordDetail)alrdout[i]).cnvcPOID.ToString()+"|"+((RdRecordDetail)alrdout[i]).cnvcInvCode;
						EntityMapping.Create(operLog, trans);
					}

					for(int k=0;k<alrdin.Count;k++)
					{
						EntityMapping.Create((RdRecordDetail)alrdin[k],trans);
					}
			
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

		public int UpdateRdRecordMoveDetail(string strRdCode,OperLog operLog,RdRecordDetail rdrd,string strlostcount)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					if(strRdCode=="RD005")
					{
						//调拨出
//						RdRecordDetail oldrrd =new RdRecordDetail();
//						oldrrd.cnnAutoID = rdrd.cnnAutoID;
//						oldrrd = EntityMapping.Get(oldrrd,trans) as RdRecordDetail;
//						oldrrd.cnnQuantity=rdrd.cnnQuantity;
//						oldrrd.cnnCost=Math.Round(oldrrd.cnnQuantity*oldrrd.cnnPrice,2);
//						EntityMapping.Update(oldrrd,trans);
//			
//						string strAutoID = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select cnnAutoID from tbRdRecordDetail where cnvcPOID='"+oldrrd.cnvcPOID+"' and cnnRdID<>"+oldrrd.cnnRdID+" and cnvcInvCode='"+oldrrd.cnvcInvCode+"'").ToString();
//						RdRecordDetail oldrrdin =new RdRecordDetail();
//						oldrrdin.cnnAutoID = int.Parse(strAutoID);
//						oldrrdin = EntityMapping.Get(oldrrdin,trans) as RdRecordDetail;
//						oldrrdin.cnnQuantity=rdrd.cnnQuantity;
//						oldrrdin.cnnCost=Math.Round(oldrrdin.cnnQuantity*oldrrdin.cnnPrice,2);
//						EntityMapping.Update(oldrrdin,trans);
					}
					else
					{
						
						//调拨入
						DataTable dtoutrd=SqlHelper.ExecuteDataTable(trans, CommandType.Text, "select cnnAutoID,cnnRdID,cnvcInvCode,cnvcComunitCode,cnnQuantity,convert(char(10),cndMdate,120) as cndMdate,convert(char(10),cndExpDate,120) as cndExpDate from tbRdRecordDetail where cnnRdID=(select cnnRdID from tbRdRecord where cnvcCode=(select cnvcCode from tbRdRecord where cnnRdID="+rdrd.cnnRdID+") and cnvcRdCode='RD005') and cnvcInvCode='"+rdrd.cnvcInvCode+"'");
						decimal dlostcount=Math.Round(decimal.Parse(strlostcount),2);
						string strsqlout="";

						SqlHelper.ExecuteNonQuery(trans,CommandType.Text,"delete from tbLostSerial where cnnProduceSerialNo="+dtoutrd.Rows[0]["cnnRdID"].ToString()+" and cnvcInvCode='"+rdrd.cnvcInvCode+"'");

						for(int i=0;i<dtoutrd.Rows.Count;i++)
						{
							if(Math.Round(decimal.Parse(dtoutrd.Rows[i]["cnnQuantity"].ToString()),2)>=dlostcount)
							{
								strsqlout="select cnnAutoID from tbRdRecordDetail where cnnRdID=(select cnnRdID from tbRdRecord where cnvcCode=(select cnvcCode from tbRdRecord where cnnRdID="+rdrd.cnnRdID+") and cnvcRdCode='RD006')";
								strsqlout+=" and cnvcInvCode='"+rdrd.cnvcInvCode+"' and convert(char(10),cndMdate,120)='"+dtoutrd.Rows[i]["cndMdate"].ToString()+"' and convert(char(10),cndExpDate,120)='"+dtoutrd.Rows[i]["cndExpDate"].ToString()+"'";
								string strAutoID = SqlHelper.ExecuteScalar(trans, CommandType.Text, strsqlout).ToString();
								RdRecordDetail oldrrd =new RdRecordDetail();
								oldrrd.cnnAutoID = int.Parse(strAutoID);
								oldrrd = EntityMapping.Get(oldrrd,trans) as RdRecordDetail;
								oldrrd.cnnQuantity=Math.Round(decimal.Parse(dtoutrd.Rows[i]["cnnQuantity"].ToString()),2)-dlostcount;
								oldrrd.cnnCost=Math.Round(oldrrd.cnnQuantity*oldrrd.cnnPrice,2);
								EntityMapping.Update(oldrrd,trans);

								DataTable dtrd = SqlHelper.ExecuteDataTable(trans, CommandType.Text, "select cnnRdID,cnvcDepID,cnvcWhCode from tbRdRecord where cnnRdID="+dtoutrd.Rows[i]["cnnRdID"].ToString());
								LostSerial lost=new LostSerial();
								lost.cnnProduceSerialNo=int.Parse(dtrd.Rows[0]["cnnRdID"].ToString());
								lost.cnvcInvCode=rdrd.cnvcInvCode;
								lost.cnnLostCount=dlostcount;
								lost.cnnAddCount=0;
								lost.cnnReduceCount=0;
								lost.cndLostDate=dtSysTime;
								lost.cnvcDeptID=dtrd.Rows[0]["cnvcDepID"].ToString();
								lost.cnvcOperID=operLog.cnvcOperID;
								lost.cndOperDate=dtSysTime;
								lost.cnvcLostType="2";
								lost.cnvcComments="调拨出库运输损耗";
								lost.cnvcWhCode=dtrd.Rows[0]["cnvcWhCode"].ToString();
								lost.cnvcInvalidFlag="0";
								lost.cnvcComunitCode=dtoutrd.Rows[i]["cnvcComunitCode"].ToString();
								lost.cndMdate=DateTime.Parse(dtoutrd.Rows[i]["cndMdate"].ToString());
								lost.cndExpDate=DateTime.Parse(dtoutrd.Rows[i]["cndExpDate"].ToString());
								EntityMapping.Create(lost, trans);
								break;
							}
							else
							{
								strsqlout="select cnnAutoID from tbRdRecordDetail where cnnRdID=(select cnnRdID from tbRdRecord where cnvcCode=(select cnvcCode from tbRdRecord where cnnRdID="+rdrd.cnnRdID+") and cnvcRdCode='RD006')";
								strsqlout+=" and cnvcInvCode='"+rdrd.cnvcInvCode+"' and convert(char(10),cndMdate,120)='"+dtoutrd.Rows[i]["cndMdate"].ToString()+"' and convert(char(10),cndExpDate,120)='"+dtoutrd.Rows[i]["cndExpDate"].ToString()+"'";
								string strAutoID = SqlHelper.ExecuteScalar(trans, CommandType.Text, strsqlout).ToString();
								RdRecordDetail oldrrd =new RdRecordDetail();
								oldrrd.cnnAutoID = int.Parse(strAutoID);
								oldrrd = EntityMapping.Get(oldrrd,trans) as RdRecordDetail;
								oldrrd.cnnQuantity=0;
								oldrrd.cnnCost=0;
								EntityMapping.Update(oldrrd,trans);

								DataTable dtrd = SqlHelper.ExecuteDataTable(trans, CommandType.Text, "select cnnRdID,cnvcDepID,cnvcWhCode from tbRdRecord where cnnRdID="+dtoutrd.Rows[i]["cnnRdID"].ToString());
								LostSerial lost=new LostSerial();
								lost.cnnProduceSerialNo=int.Parse(dtrd.Rows[0]["cnnRdID"].ToString());
								lost.cnvcInvCode=rdrd.cnvcInvCode;
								lost.cnnLostCount=Math.Round(decimal.Parse(dtoutrd.Rows[i]["cnnQuantity"].ToString()),2);
								lost.cnnAddCount=0;
								lost.cnnReduceCount=0;
								lost.cndLostDate=dtSysTime;
								lost.cnvcDeptID=dtrd.Rows[0]["cnvcDepID"].ToString();
								lost.cnvcOperID=operLog.cnvcOperID;
								lost.cndOperDate=dtSysTime;
								lost.cnvcLostType="2";
								lost.cnvcComments="调拨出库运输损耗";
								lost.cnvcWhCode=dtrd.Rows[0]["cnvcWhCode"].ToString();
								lost.cnvcInvalidFlag="0";
								lost.cnvcComunitCode=dtoutrd.Rows[i]["cnvcComunitCode"].ToString();
								lost.cndMdate=DateTime.Parse(dtoutrd.Rows[i]["cndMdate"].ToString());
								lost.cndExpDate=DateTime.Parse(dtoutrd.Rows[i]["cndExpDate"].ToString());
								EntityMapping.Create(lost, trans);

								dlostcount=dlostcount-lost.cnnLostCount;
							}
						}
					}

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "修改调拨存货："+rdrd.cnnRdID.ToString()+"|"+rdrd.cnvcInvCode;
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

		public int DeleteRdRecordMoveDetail(OperLog operLog,string strRdID,string strInvCode)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					string sql="delete from tbRdRecordDetail where cnvcInvCode='"+strInvCode+"' and cnvcPOID=(select cnvcCode from tbRdRecord where cnnRdID="+strRdID+")";
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,sql);
					
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "删除调拨存货："+strRdID+"|"+strInvCode;
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

		public int RdRecordMoveDetailExecMoving(OperLog operLog,string strWhcode,string strRdid,string strRdCode)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					if(strRdCode=="RD005")
					{
						string strsql1="update tbCurrentStock set cnnQuantity=a.cnnQuantity-(b.cnnQuantity*c.cniChangRate),cnnAvaQuantity=a.cnnAvaQuantity-(b.cnnQuantity*c.cniChangRate)";
						strsql1+=" from tbCurrentStock a,tbRdRecordDetail b,tbComputationUnit c where a.cnvcWhCode='"+strWhcode+"' and b.cnnRdId="+strRdid+" and a.cnvcInvCode=b.cnvcInvCode and b.cnvcComUnitCode=c.cnvcComUnitCode";
						strsql1+=" and a.cndMdate=b.cndMdate and a.cndExpDate=b.cndExpDate";
						SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql1);
					
						string strsql3="update tbRdRecord set cnvcState='2' where cnnRdID="+strRdid;
						SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql3);

						operLog.cndOperDate = dtSysTime;
						operLog.cnvcComments = "调拨单出库："+strRdid;
						EntityMapping.Create(operLog, trans);
					}
					else
					{
						string strsql1="update tbCurrentStock set cnnQuantity=a.cnnQuantity+(b.cnnQuantity*c.cniChangRate),cnnAvaQuantity=a.cnnAvaQuantity+(b.cnnQuantity*c.cniChangRate)";
						strsql1+=" from tbCurrentStock a,tbRdRecordDetail b,tbComputationUnit c where a.cnvcWhCode='"+strWhcode+"' and b.cnnRdId="+strRdid+" and a.cnvcInvCode=b.cnvcInvCode and b.cnvcComUnitCode=c.cnvcComUnitCode";
						strsql1+=" and a.cndMdate=b.cndMdate and a.cndExpDate=b.cndExpDate";
						SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql1);

						string strsql2="insert into tbCurrentStock select a.cnvcWhCode,b.cnvcInvCode,b.cnnQuantity*c.cniChangRate,0,0,'0',0,b.cndMdate,0,0,0,b.cnnQuantity*c.cniChangRate,0,'0',0,0,b.cndExpDate from tbRdRecord a,tbRdRecordDetail b,tbComputationUnit c";
						strsql2+=" where a.cnnRdID="+strRdid+" and a.cnnRdID=b.cnnRdID and b.cnvcComUnitCode=c.cnvcComUnitCode and b.cnvcInvCode+convert(char(10),b.cndMdate,120)+convert(char(10),b.cndExpDate,120) not in(select cnvcInvCode+convert(char(10),cndMdate,120)+convert(char(10),cndExpDate,120) from tbCurrentStock where cnvcWhCode='"+strWhcode+"')";
						SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql2);
					
						string strsql3="update tbRdRecord set cnvcState='1' where cnnRdID="+strRdid;
						SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql3);

						string stroutRdID = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select cnnRdID from tbRdRecord where cnvcCode=(select cnvcCode from tbRdRecord where cnnRdID="+strRdid+") and cnvcRdCode='RD005'").ToString();
						string strsql4="update tbLostSerial set cnvcInvalidFlag='1' where cnnProduceSerialNo="+stroutRdID;
						SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql4);

						operLog.cndOperDate = dtSysTime;
						operLog.cnvcComments = "调拨单入库："+strRdid;
						EntityMapping.Create(operLog, trans);
					}

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

		public int AddDeptStorageEnterDetail(OperLog operLog,string strRdid,string strAssID,string strDeptID)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					string strsql="insert into tbRdRecordDetail select "+strRdid+",a.cnnAssignSerialNo,0,'',a.cnvcInvCode,a.cnnAssignCount,0,0,0,b.cnvcGroupCode,b.cnvcProduceUnitCode,null,'0','',";
					strsql+="convert(char(10),cndMdate,120),0,'0',convert(char(10),cndExpDate,120) from tbAssignDetail a,tbInventory b,tbAssignLog c where a.cnvcInvCode=b.cnvcInvCode and a.cnnAssignSerialNo="+strAssID;
					strsql+=" and c.cnvcReceiveDeptID='"+strDeptID+"' and c.cnnAssignSerialNo="+strAssID+" and a.cnnAssignSerialNo=c.cnnAssignSerialNo and a.cnnOrderSerialNo=c.cnnOrderSerialNo and a.cnnProduceSerialNo=c.cnnProduceSerialNo";
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql);
			
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "添加关联分货单："+strRdid+"|"+strAssID;
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

		public int UpdateDeptStorageEnterDetail(OperLog operLog,RdRecordDetail rrd,decimal dlostcount)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					RdRecordDetail oldrrd =new RdRecordDetail();
					oldrrd.cnnAutoID = rrd.cnnAutoID;

					oldrrd = EntityMapping.Get(oldrrd,trans) as RdRecordDetail;
					oldrrd.cnnAutoID=rrd.cnnAutoID;
					oldrrd.cnnQuantity=rrd.cnnQuantity;
					oldrrd.cnnCost=oldrrd.cnnPrice*rrd.cnnQuantity;
					EntityMapping.Update(oldrrd,trans);

					DataTable dtrd=SqlHelper.ExecuteDataTable(trans,CommandType.Text,"select * from tbRdRecord where cnnRdID="+oldrrd.cnnRdID);
					LostSerial lost=new LostSerial();
					lost.cnnProduceSerialNo=int.Parse(oldrrd.cnnRdID.ToString());
					lost.cnvcInvCode=oldrrd.cnvcInvCode;
					lost.cnnLostCount=dlostcount;
					lost.cnnAddCount=0;
					lost.cnnReduceCount=0;
					lost.cndLostDate=dtSysTime;
					lost.cnvcDeptID=dtrd.Rows[0]["cnvcDepID"].ToString();
					lost.cnvcOperID=operLog.cnvcOperID;
					lost.cndOperDate=dtSysTime;
					lost.cnvcLostType="2";
					lost.cnvcComments="分货入库运输损耗";
					lost.cnvcWhCode=dtrd.Rows[0]["cnvcWhCode"].ToString();
					lost.cnvcInvalidFlag="0";
					lost.cnvcComunitCode=oldrrd.cnvcComunitCode;
					lost.cndMdate=oldrrd.cndMdate;
					lost.cndExpDate=oldrrd.cndExpDate;
					EntityMapping.Create(lost, trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "采购分货单子表修改："+oldrrd.cnnAutoID;
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

		public int DeptStorageEnterExecEntering(OperLog operLog,string strWhcode,DataTable dtdetial,string strRdid,string strDeptID)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					for(int i=0;i<dtdetial.Rows.Count;i++)
					{
						string maxautoid=SqlHelper.ExecuteScalar(trans,CommandType.Text,"select isnull(max(cnnAutoID),0) from tbCurrentStock where cnvcWhCode='"+strWhcode+"' and cnvcInvCode='"+dtdetial.Rows[i]["cnvcInvCode"].ToString()+"' and cndMdate='"+dtdetial.Rows[i]["cndMdate"].ToString()+"' and cndExpDate='"+dtdetial.Rows[i]["cndExpDate"].ToString()+"'").ToString();
						DataTable dtlost=SqlHelper.ExecuteDataTable(trans,CommandType.Text,"select cnnLostSerialNo,count(*) from tbLostSerial where cnvcWhCode='"+strWhcode+"' and cnvcInvCode='"+dtdetial.Rows[i]["cnvcInvCode"].ToString()+"' and cndMdate='"+dtdetial.Rows[i]["cndMdate"].ToString()+"' and cndExpDate='"+dtdetial.Rows[i]["cndExpDate"].ToString()+"' group by cnnLostSerialNo");
						string changrate=SqlHelper.ExecuteScalar(trans,CommandType.Text,"select cniChangRate from tbComputationUnit where cnvcComunitCode=(select cnvcComunitCode from tbRdRecordDetail where cnnAutoID="+dtdetial.Rows[i]["cnnAutoID"].ToString()+")").ToString();
						string orderserial=SqlHelper.ExecuteScalar(trans,CommandType.Text,"select distinct cnnOrderSerialNo from tbAssignLog where cnnAssignSerialNo="+dtdetial.Rows[i]["cnvcPOID"].ToString()+" and cnvcReceiveDeptID='"+strDeptID+"'").ToString();
						if(maxautoid=="0")
						{
							Entity.CurrentStock cs=new CurrentStock();
							cs.cnvcWhCode=strWhcode;
							cs.cnvcInvCode=dtdetial.Rows[i]["cnvcInvCode"].ToString();
							cs.cnnQuantity=Math.Round(decimal.Parse(dtdetial.Rows[i]["cnnQuantity"].ToString())*decimal.Parse(changrate),2);
							cs.cnvcStopFlag="0";
							cs.cnnAvaQuantity=cs.cnnQuantity;
							cs.cndMdate=DateTime.Parse(dtdetial.Rows[i]["cndMdate"].ToString());
							cs.cndExpDate=DateTime.Parse(dtdetial.Rows[i]["cndExpDate"].ToString());
							EntityMapping.Create(cs,trans);

							operLog.cndOperDate = dtSysTime;
							operLog.cnvcComments = "分货入库添加库存："+cs.cnvcWhCode+"|"+cs.cnvcInvCode;
							EntityMapping.Create(operLog, trans);
						}
						else
						{
							Entity.CurrentStock cs=new CurrentStock();
							cs.cnnAutoID=int.Parse(maxautoid);
							cs=EntityMapping.Get(cs,trans) as CurrentStock;
							cs.cnnQuantity=cs.cnnQuantity+Math.Round(decimal.Parse(dtdetial.Rows[i]["cnnQuantity"].ToString())*decimal.Parse(changrate),2);
							cs.cnnAvaQuantity=cs.cnnAvaQuantity+Math.Round(decimal.Parse(dtdetial.Rows[i]["cnnQuantity"].ToString())*decimal.Parse(changrate),2);
							EntityMapping.Update(cs,trans);

							operLog.cndOperDate = dtSysTime;
							operLog.cnvcComments = "分货入库更新库存："+cs.cnvcWhCode+"|"+cs.cnvcInvCode;
							EntityMapping.Create(operLog, trans);
						}

						if(dtlost.Rows.Count>0)
						{
							string strsql1="update tbLostSerial set cnvcInvalidFlag='1' where cnnLostSerialNo="+dtlost.Rows[0]["cnnLostSerialNo"].ToString();
							SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql1);
						}

						string strsql5="update tbOrderBook set cnvcOrderState='3' where cnnOrderSerialNo="+orderserial;
						SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql5);
					}

					string strsql3="update tbRdRecordDetail set cnvcFlag='1' where cnnRdID="+strRdid;
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql3);

					string strsql4="update tbRdRecord set cnvcState='1' where cnnRdID="+strRdid;
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql4);

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

		public int AddLostSerial(OperLog operLog,LostSerial ls)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					ls.cndOperDate=dtSysTime;
					EntityMapping.Create(ls,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "添加销售损耗："+ls.cnnLostSerialNo.ToString();
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

		public int UpdateLostSerial(OperLog operLog,LostSerial ls)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					LostSerial oldls =new LostSerial();
					oldls.cnnLostSerialNo=ls.cnnLostSerialNo;

					oldls = EntityMapping.Get(oldls,trans) as LostSerial;
					oldls.cnnAddCount=ls.cnnAddCount;
					oldls.cnnReduceCount=ls.cnnReduceCount;
					oldls.cndLostDate=ls.cndLostDate;
					oldls.cnvcComments=ls.cnvcComments;
					oldls.cnvcOperID=ls.cnvcOperID;
					oldls.cndOperDate=dtSysTime;
					EntityMapping.Update(oldls,trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "销售损耗修改："+oldls.cnnLostSerialNo.ToString();

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

		public int ConfirmLostSerial(OperLog operLog,string strserialno)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					string strsql1="update tbCurrentStock set cnnQuantity=a.cnnQuantity-(b.cnnLostCount+b.cnnAddCount-b.cnnReduceCount)*c.cniChangRate,cnnAvaQuantity=a.cnnAvaQuantity-(b.cnnLostCount+b.cnnAddCount-b.cnnReduceCount)*c.cniChangRate";
					strsql1+=" from tbCurrentStock a,tbLostSerial b,tbComputationUnit c where b.cnnLostSerialNo="+strserialno+" and a.cnvcWhCode=b.cnvcWhCode and a.cnvcInvCode=b.cnvcInvCode and b.cnvcComunitCode=c.cnvcComunitCode";
					strsql1+=" and cnvcStopFlag='0' and convert(char(8),a.cndMdate,112)=convert(char(8),b.cndMdate,112) and convert(char(8),a.cndExpDate,112)=convert(char(8),b.cndExpDate,112)";
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql1);

					string strsql2="update tbCurrentStock set cnnQuantity=a.cnnQuantity-(b.cnnLostCount+b.cnnAddCount-b.cnnReduceCount)*c.cniChangRate,cnnStopQuantity=a.cnnStopQuantity-(b.cnnLostCount+b.cnnAddCount-b.cnnReduceCount)*c.cniChangRate";
					strsql2+=" from tbCurrentStock a,tbLostSerial b,tbComputationUnit c where b.cnnLostSerialNo="+strserialno+" and a.cnvcWhCode=b.cnvcWhCode and a.cnvcInvCode=b.cnvcInvCode and b.cnvcComunitCode=c.cnvcComunitCode";
					strsql2+=" and cnvcStopFlag='1' and convert(char(8),a.cndMdate,112)=convert(char(8),b.cndMdate,112) and convert(char(8),a.cndExpDate,112)=convert(char(8),b.cndExpDate,112)";
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql2);

					string strsql3="update tbLostSerial set cnvcInvalidFlag='1' where cnnLostSerialNo="+strserialno;
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql3);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "确认过期损耗："+strserialno;

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

		public int CreateStorageBalance()
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				try
				{
//					string strSysTime = SqlHelper.ExecuteScalar( CommandType.Text, "select getdate()").ToString();
//					DateTime dtSysTime = DateTime.Parse(strSysTime);

					SqlCommand cmd=new SqlCommand("exec sp_CreateStorageBalance",conn);
					cmd.CommandTimeout=600;
					cmd.ExecuteNonQuery();

					cmd=new SqlCommand("exec sp_CreateStorageBalanceDetail",conn);
					cmd.CommandTimeout=600;
					cmd.ExecuteNonQuery();

				}
				catch(SqlException sex)
				{
					LogAdapter.WriteDatabaseException(sex);
					return -1;
				}
				catch(Exception ex)
				{
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

		public int UpdateCurrentStockReStop(OperLog operLog,string strAutoID)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					string strsql="update tbCurrentStock set cnnAvaQuantity=cnnStopQuantity,cnnStopQuantity=0,cnvcStopFlag='0' where cnnAutoID="+strAutoID;
					SqlCommand cmd=new SqlCommand(strsql,conn,trans);
					cmd.ExecuteNonQuery();

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "库存解冻：库存标识|"+strAutoID;
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
