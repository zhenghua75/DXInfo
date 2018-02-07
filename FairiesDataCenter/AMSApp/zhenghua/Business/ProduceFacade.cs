using System;
using System.Data;
using System.Data.SqlClient;
using AMSApp.zhenghua.DataAccess;
using AMSApp.zhenghua.Common;
using AMSApp.zhenghua.QueryArgs;
using AMSApp.zhenghua.Entity;
using System.Collections;
namespace AMSApp.zhenghua.Business
{
	/// <summary>
	/// ProduceFacade 的摘要说明。
	/// </summary>
	public class ProduceFacade
	{
		public ProduceFacade()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 生产计划
		public void AddProduceLog(ProduceLog produceLog,OperLog operLog)//,BusiLog busiLog)
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
					
					produceLog.cndOperDate = dtSysTime;
					produceLog.cnnProduceSerialNo = serialNo.cnnSerialNo;
					EntityMapping.Create(produceLog, trans);
					
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产流水："+produceLog.cnnProduceSerialNo.ToString();

					string strOrderBookSql = "select count(*) from tbOrderBook where cnvcOrderState='0' and cnvcOrderState='0' and cndShipDate>='"+produceLog.cndShipBeginDate+"' and cndShipDate<='"+produceLog.cndShipEndDate+"' and cnvcProduceDeptID='"+produceLog.cnvcProduceDeptID+"'";
					if(produceLog.cnbSelf)
					{
						strOrderBookSql += " and cnvcOrderType IN ('SELFPRODUCE','WDOSELF')";
					}
					else
						strOrderBookSql += " and cnvcOrderType IN ('MDO','WDO')";
					object oCount = SqlHelper.ExecuteScalar(trans, CommandType.Text, strOrderBookSql);
					int iCount = int.Parse(oCount.ToString());
					if(iCount<1)
						throw new Exception("没有订单，不允许做计划！");
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

		public void UpdateProduceLog(ProduceLog produceLog,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

//					OrderSerialNo serialNo = new OrderSerialNo();
//					serialNo.cnvcFill = "0";
//					serialNo.cnnSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));
					
					ProduceLog oldLog = new ProduceLog();
					oldLog.cnnProduceSerialNo = produceLog.cnnProduceSerialNo;
					oldLog = EntityMapping.Get(oldLog, trans) as ProduceLog;
					if(oldLog == null)
					{
						throw new Exception("生产计划为空");
					}
					oldLog.cndOperDate = dtSysTime;
					oldLog.cndProduceDate = produceLog.cndProduceDate;
					oldLog.cndShipBeginDate = produceLog.cndShipBeginDate;
					oldLog.cndShipEndDate = produceLog.cndShipEndDate;
					oldLog.cnvcOperID = produceLog.cnvcOperID;
					oldLog.cnvcProduceDeptID = produceLog.cnvcProduceDeptID;
					//
					if(oldLog.cnbSelf != produceLog.cnbSelf)
					{
						string strOrderBookSql = "select count(*) from tbOrderBook where cnvcOrderState='0' and cnvcOrderState='0' and cndShipDate>='"+produceLog.cndShipBeginDate+"' and cndShipDate<='"+produceLog.cndShipEndDate+"' and cnvcProduceDeptID='"+produceLog.cnvcProduceDeptID+"'";
						if(produceLog.cnbSelf)
						{
							strOrderBookSql += " and cnvcOrderType IN ('SELFPRODUCE','WDOSELF')";
						}
						else
							strOrderBookSql += " and cnvcOrderType IN ('MDO','WDO')";
						object oCount = SqlHelper.ExecuteScalar(trans, CommandType.Text, strOrderBookSql);
						int iCount = int.Parse(oCount.ToString());
						if(iCount<1)
							throw new Exception("没有订单，不允许修改自生产属性！");
					}
					oldLog.cnbSelf = produceLog.cnbSelf;
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

		#endregion

		#region 关联订单
		public void LindOrder(ProduceLog produceLog,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);
					ProduceLog oldLog = new ProduceLog();
					oldLog.cnnProduceSerialNo = produceLog.cnnProduceSerialNo;
					oldLog = EntityMapping.Get(oldLog, trans) as ProduceLog;
					if(oldLog == null)
					{
						throw new Exception("生产计划为空");
					}
					if(!(oldLog.cnvcProduceState == "0" || oldLog.cnvcProduceState == "1"))
					{
						throw new Exception("生产计划不能关联订单");
					}

					string strProduceDetailSql1 = "";
					if(oldLog.cnbSelf)
					{
						strProduceDetailSql1 = "select '"+oldLog.cnnProduceSerialNo.ToString()+"' as cnnProduceSerialNo,a.cnvcInvCode,sum(a.cnnOrderCount) as cnnProduceCount,sum(a.cnnOrderCount) as cnnOrderCount "
							+" from tbOrderBookDetail a  where a.cnnOrderSerialNo in "
							+" (select cnnOrderSerialNo from tbOrderBook where cnvcOrderType IN ('SELFPRODUCE','WDOSELF') and cnvcOrderState='0'  and cndShipDate>='"+oldLog.cndShipBeginDate+"' and cndShipDate<='"+oldLog.cndShipEndDate+"' and cnvcProduceDeptID='"+oldLog.cnvcProduceDeptID+"')   "
							+" group by a.cnvcInvCode ";
					}
					else
					{
						strProduceDetailSql1 = "select '"+oldLog.cnnProduceSerialNo.ToString()+"' as cnnProduceSerialNo,a.cnvcInvCode,sum(a.cnnOrderCount) as cnnProduceCount,sum(a.cnnOrderCount) as cnnOrderCount "
							+" from tbOrderBookDetail a  where a.cnnOrderSerialNo in "
							+" (select cnnOrderSerialNo from tbOrderBook where cnvcOrderType IN ('MDO','WDO') and cnvcOrderState='0'  and cndShipDate>='"+oldLog.cndShipBeginDate+"' and cndShipDate<='"+oldLog.cndShipEndDate+"' and cnvcProduceDeptID='"+oldLog.cnvcProduceDeptID+"')   "
							+" group by a.cnvcInvCode ";
					}
					DataTable dtpd1= SqlHelper.ExecuteDataTable(trans,CommandType.Text,strProduceDetailSql1);
					if(dtpd1.Rows.Count == 0) throw new Exception("无新订单");

					//锁定订单表
					string strOrderBookSql = "update tbOrderBook set cnvcOrderState='0' where cnvcOrderState='0' and cndShipDate>='"+oldLog.cndShipBeginDate+"' and cndShipDate<='"+oldLog.cndShipEndDate+"' and cnvcProduceDeptID='"+oldLog.cnvcProduceDeptID+"'";
					if(oldLog.cnbSelf)
					{
						strOrderBookSql += " and cnvcOrderType IN ('SELFPRODUCE','WDOSELF')";
					}
					else
						strOrderBookSql += " and cnvcOrderType IN ('MDO','WDO')";
					SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strOrderBookSql);

					
					
					//入生产计划产品细节表  and cnvcOrderType <>'SELFPRODUCE'
					

					//存货档案
					DataTable dtInv = SingleTableQuery.ExcuteQuery("tbInventory",trans);
					//DataTable dtComputationUnit = SingleTableQuery.ExcuteQuery("tbComputationUnit",trans);

//					string strwhSql = @"select cnvcInvCode,sum(cnnAvaQuantity) as cnnwhcount from tbCurrentStock where cnvcWhCOde
//in (select cnvcWhCOde from tbwarehouse where cnvcDepCode='"+produceLog.cnvcProduceDeptID+"')"
//						+"group by cnvcInvCode";
//					DataTable dtwh = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strwhSql);

					string strProduceDetailSql2 = "select * from tbproducedetail where cnnProduceSerialNo="+oldLog.cnnProduceSerialNo.ToString();
					DataTable dtpd2= SqlHelper.ExecuteDataTable(trans,CommandType.Text,strProduceDetailSql2);
//					if(dtpd2.Rows.Count>0)
//					{
					
					//Helper.DataTableConvert(dtpd1,"cnvcinvcode","cnvccomunitcode",dtInv,"cnvcinvcode","cnvccomunitcode","");
					//Helper.DataTableConvert(dtpd1,"cnvcinvcode","cnvcstcomunitcode",dtInv,"cnvcinvcode","cnvcstcomunitcode","");
					//Helper.DataTableConvert(dtpd1,"cnvcinvcode","cnvcproduceunitcode",dtInv,"cnvcinvcode","cnvcproduceunitcode","");
					Helper.DataTableConvert(dtpd1,"cnvcinvcode","cnbself",dtInv,"cnvcinvcode","cnbself","");
					//Helper.DataTableConvert(dtpd1,"cnvccomunitcode","cnnchangrate",dtComputationUnit,"cnvccomunitcode","cnichangrate","");
					//Helper.DataTableConvert(dtpd1,"cnvcproduceunitcode","cnnchangrate_pd",dtComputationUnit,"cnvccomunitcode","cnichangrate","");
					//Helper.DataTableConvert(dtpd1,"cnvcinvcode","cnnwhcount",dtwh,"cnvcInvCode","cnnwhcount","");
					foreach(DataRow drpd1 in dtpd1.Rows)
					{
						Entity.ProduceDetail pd1 = new ProduceDetail(drpd1);
						DataRow[] drpds2 = dtpd2.Select("cnvcinvcode='"+pd1.cnvcInvCode+"'");
						//DataRow[] drinvs = dtInv.Select("cnvcinvcode='"+pd1.cnvcInvCode+"'");
						//if(drinvs.Length == 0) throw new Exception("未找到存货档案"+pd1.cnvcInvCode);
						//Entity.Inventory inv = new AMSApp.zhenghua.Entity.Inventory(drinvs[0]);
						//double dchangerate = Convert.ToDouble(drpd1["cnnchangrate"].ToString());
						//decimal dchangerate_st = Convert.ToDecimal(drpd1["cnnchangrate_st"].ToString());
						//double dchangerate_pd = Convert.ToDouble(drpd1["cnnchangrate_pd"].ToString());
						bool bself = Convert.ToBoolean(drpd1["cnbself"].ToString());
						//DataRow[] drwhs = dtwh.Select("cnvcinvcode='"+pd1.cnvcInvCode+"'");
						//string strwh = drpd1["cnnwhcount"].ToString();
						//decimal dwh = 0;
//						if( strwh != "")						
//						{														
//							dwh = Convert.ToDecimal(Math.Floor(Convert.ToDouble(drpd1["cnnwhcount"].ToString())*dchangerate/dchangerate_pd));
//						}
						if(drpds2.Length>0)
						{
							Entity.ProduceDetail pd2 = new ProduceDetail(drpds2[0]);
							if(bself)
							{
								pd1.cnnProduceCount = pd2.cnnProduceCount+pd1.cnnProduceCount;
//								if(pd2.cnnProduceCount>pd2.cnnOrderCount)
//								{
//									pd1.cnnProduceCount = pd1.cnnOrderCount>dwh?pd1.cnnOrderCount-dwh:0;
//								}
//								else
//								pd1.cnnProduceCount = pd1.cnnOrderCount>dwh-(pd2.cnnOrderCount-pd2.cnnProduceCount)?pd1.cnnOrderCount-(dwh-(pd2.cnnOrderCount-pd2.cnnProduceCount)):0;//pd1.cnnProduceCount+pd2.cnnProduceCount;
							}
							else
								pd1.cnnProduceCount = 0;
							pd1.cnnOrderCount = pd1.cnnOrderCount+pd2.cnnOrderCount;
							
							EntityMapping.Update(pd1,trans);
						}
						else
						{
							if(!bself)
								pd1.cnnProduceCount = 0;
//							else
//								pd1.cnnProduceCount = pd//pd1.cnnOrderCount>dwh?pd1.cnnOrderCount-dwh:0;
							EntityMapping.Create(pd1,trans);
						}
					}
//					}
//					else
//					{
//						string strProduceDetailSql = " insert into tbProduceDetail(cnnProduceSerialNo,cnvcInvCode,cnnProduceCount,cnnOrderCount)  "
//							+ "select '"+oldLog.cnnProduceSerialNo.ToString()+"' as cnnProduceSerialNo,a.cnvcInvCode,sum(a.cnnOrderCount) as cnnProduceCount,sum(a.cnnOrderCount) as cnnOrderCount from tbOrderBookDetail a "
//							+ " where a.cnnOrderSerialNo in (select cnnOrderSerialNo from tbOrderBook where cnvcOrderState='0'  and cndShipDate>='"+oldLog.cndShipBeginDate+"' and cndShipDate<='"+oldLog.cndShipEndDate+"' and cnvcProduceDeptID='"+oldLog.cnvcProduceDeptID+"')  "
//							+ " group by a.cnvcInvCode";
//						SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strProduceDetailSql);
//					}
					//入生产流水订单流水关联表 and cnvcOrderType <>'SELFPRODUCE'
					string strProduceOrderSql = "";
					if(oldLog.cnbSelf)
					{
						strProduceOrderSql = "insert into tbProduceOrderLog(cnnProduceSerialNo,cnnOrderSerialNo,cnvcType) "
							+ " select " + oldLog.cnnProduceSerialNo + ",cnnOrderSerialNo,'0' from tbOrderBook"
							+ " where cnnOrderSerialNo not in (select cnnOrderSerialNo from tbProduceOrderLog where cnnProduceSerialNo="+oldLog.cnnProduceSerialNo.ToString()
							+") and cnvcOrderType IN ('SELFPRODUCE','WDOSELF') and cnvcOrderState='0'  and cndShipDate>='"+oldLog.cndShipBeginDate+"' and cndShipDate<='"+oldLog.cndShipEndDate+"' and cnvcProduceDeptID='"+oldLog.cnvcProduceDeptID+"'";

					}
					else
					{
						strProduceOrderSql = "insert into tbProduceOrderLog(cnnProduceSerialNo,cnnOrderSerialNo,cnvcType) "
							+ " select " + oldLog.cnnProduceSerialNo + ",cnnOrderSerialNo,'0' from tbOrderBook"
							+ " where cnnOrderSerialNo not in (select cnnOrderSerialNo from tbProduceOrderLog where cnnProduceSerialNo="+oldLog.cnnProduceSerialNo.ToString()
							+") and cnvcOrderType IN ('MDO','WDO') and cnvcOrderState='0'  and cndShipDate>='"+oldLog.cndShipBeginDate+"' and cndShipDate<='"+oldLog.cndShipEndDate+"' and cnvcProduceDeptID='"+oldLog.cnvcProduceDeptID+"'";
					}
					SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strProduceOrderSql);

					//更新订单状态 and cnvcOrderType <>'SELFPRODUCE'
					string strOrderSql = "update tbOrderBook set cnvcOrderState='1' where cnvcOrderState='0'  and cndShipDate>='"+oldLog.cndShipBeginDate+"' and cndShipDate<='"+oldLog.cndShipEndDate+"' and cnvcProduceDeptID='"+oldLog.cnvcProduceDeptID+"'";
					if(oldLog.cnbSelf)
					{
						strOrderSql += " and cnvcOrderType IN ('SELFPRODUCE','WDOSELF')";
					}
					else
						strOrderSql += " and cnvcOrderType IN ('MDO','WDO')";
					SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strOrderSql);


					//更新计划状态
					if(oldLog.cnvcProduceState == "0")
					{
						oldLog.cnvcProduceState = "1";
					}
					oldLog.cnvcOperID = produceLog.cnvcOperID;
					oldLog.cndOperDate = dtSysTime;

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


		public void UpdateProduceDetail(ProduceDetail pd,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);
					
					ProduceLog oldLog = new ProduceLog();
					oldLog.cnnProduceSerialNo = pd.cnnProduceSerialNo;
					oldLog = EntityMapping.Get(oldLog, trans) as ProduceLog;
					if(oldLog == null)
					{
						throw new Exception("生产计划为空");
					}
					if(oldLog.cnvcProduceState != "1")
					{
						throw new Exception("生产计划状态不能更新计划生产数量");
					}
					ProduceDetail oldpd = new ProduceDetail();
					oldpd.cnnProduceSerialNo = pd.cnnProduceSerialNo;
					oldpd.cnvcInvCode = pd.cnvcInvCode;
					oldpd = EntityMapping.Get(oldpd,trans) as ProduceDetail;
					if(oldpd == null)
					{
						throw new Exception("生产计划无此产品");
					}
					oldpd.cnnProduceCount = pd.cnnProduceCount;
					EntityMapping.Update(oldpd,trans);

					string strComments = "["+pd.cnvcInvCode+":"+pd.cnnProduceCount.ToString()+"]";
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产流水："+pd.cnnProduceSerialNo.ToString();
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

		#region 生产预估
		/// <summary>
		/// 自生产预估
		/// </summary>
		/// <param name="produceLog"></param>
		/// <param name="operLog"></param>
		public void AddMakeLogSelf(ProduceLog produceLog,OperLog operLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);					

					ProduceLog oldLog = new ProduceLog();
					oldLog.cnnProduceSerialNo = produceLog.cnnProduceSerialNo;
					oldLog = EntityMapping.Get(oldLog, trans) as ProduceLog;
					
					if(oldLog == null)
					{
						throw new Exception("生产计划不存在");
					}
					if(oldLog.cnvcProduceState != "1" )
					{
						throw new Exception("生产计划不能预估");
					}
					//制令产品					
					string strProduceDetail = 
						strProduceDetail = "select * from tbproducedetail where cnnProduceSerialNo="+produceLog.cnnProduceSerialNo.ToString();

					DataTable dtProduceDetail = SqlHelper.ExecuteDataTable(trans, CommandType.Text, strProduceDetail);					
					//存货档案
					DataTable dtInv = SingleTableQuery.ExcuteQuery("tbInventory",trans);
					DataTable dtComputationUnit = SingleTableQuery.ExcuteQuery("tbComputationUnit",trans);
					//BOM						
					DataTable dtBOM = SingleTableQuery.ExcuteQuery("tbbillofmaterials", trans);
					//生产组与商品类型对应表
					//					DataTable dtGroupGoods = SingleTableQuery.ExcuteQuery("tbGroupGoods", trans);
					//					DataTable dtGroupMake = SingleTableQuery.ExcuteQuery("tbGroupMake", trans);
					//生产组
					//					string strGroups = "select * from tbNameCode where cnvcType='GROUP'";
					//					DataTable dtGroup = SqlHelper.ExecuteDataTable(trans, CommandType.Text, strGroups);

					Entity.MakeDetail md = new MakeDetail();
					DataTable dtMakeDetailNew = md.ToTable().Clone();
					foreach(DataRow drProduceDetail in dtProduceDetail.Rows)
					{
						string strInvCode = drProduceDetail["cnvcInvCode"].ToString();
						string strProduceCount = drProduceDetail["cnnProduceCount"].ToString();
						//						DataRow[] drs = dtMakeDetailNew.Select("cnvcInvCode='"+strInvCode+"'");
						//						if(drs.Length>0)
						//						{
						//							drs[0]["cnnMakeCount"] = decimal.Parse(drProduceDetail["cnnMakeCount"].ToString())+decimal.Parse(drs[0]["cnnMakeCount"].ToString());
						//						}
						//						else
						//						{
						//							DataRow drMakeDetail = dtMakeDetailNew.NewRow();
						//							drMakeDetail["cnvcInvCode"] = strInvCode;
						//							drMakeDetail["cnnMakeCount"] = strProduceCount;
						//							drMakeDetail["cnnCount"] = strProduceCount;
						//							dtMakeDetailNew.Rows.Add(drMakeDetail);
						//						}
						DataRow drMakeDetailNew = dtMakeDetailNew.NewRow();
						drMakeDetailNew["cnvcInvCode"] = strInvCode;
						drMakeDetailNew["cnnMakeCount"] = strProduceCount;
						drMakeDetailNew["cnnCount"] = strProduceCount;
						drMakeDetailNew["cnnAdjustCount"] = 0;
						drMakeDetailNew["cnnStCount"] = 0;
						drMakeDetailNew["cnbCollar"] = false;
						drMakeDetailNew["cnnCollarCount"] = 0;
						dtMakeDetailNew.Rows.Add(drMakeDetailNew);
						AnalyzeFormulaSelf(dtBOM,dtInv, strInvCode, strProduceCount, ref dtMakeDetailNew);					
					}					
					

					MakeLog makeLog =  new MakeLog();
					makeLog.cnnProduceSerialNo = produceLog.cnnProduceSerialNo;								
					makeLog.cndOperDate = dtSysTime;
					makeLog.cnvcOperID = produceLog.cnvcOperID;					
					//makeLog.cnvcMakeType = "0";

					OrderSerialNo serialNo = new OrderSerialNo();
					serialNo.cnvcFill = "0";
					serialNo.cnnSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));

					makeLog.cnnMakeSerialNo = serialNo.cnnSerialNo;

					Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnbSelf",dtInv,"cnvcInvCode","cnbSelf","");
					//
					//					string strwhSql = @"select cnvcInvCode,sum(cnnAvaQuantity) as cnnwhcount from tbCurrentStock where cnvcWhCOde
					//in (select cnvcWhCOde from tbwarehouse where cnvcDepCode='"+oldLog.cnvcProduceDeptID+"')"
					//						+"group by cnvcInvCode";
					//					DataTable dtwh = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strwhSql);

					//Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnnwhcount",dtwh,"cnvcInvCode","cnnwhcount","");


					Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnvcProduceUnitCode",dtInv,"cnvcInvCode","cnvcProduceUnitCode","");					
					Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnvcStComUnitCode",dtInv,"cnvcInvCode","cnvcStComUnitCode","");
					Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnvcGroupCode",dtInv,"cnvcInvCode","cnvcGroupCode","");
					//Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnvcComUnitCode_main",dtComputationUnit,"cnvcGroupCode","cnvcGroupCode","cnbMainUnit=1");

					Helper.DataTableConvert(dtMakeDetailNew,"cnvcProduceUnitCode","cniChangeRate_p",dtComputationUnit,"cnvcComUnitCode","cniChangRate","");
					Helper.DataTableConvert(dtMakeDetailNew,"cnvcStComUnitCode","cniChangeRate_st",dtComputationUnit,"cnvcComUnitCode","cniChangRate","");
					//					Helper.DataTableConvert(dtMakeDetailNew,"cnvcComUnitCode_main","cniChangeRate_main",dtComputationUnit,"cnvcComUnitCode","cniChangRate","");


					foreach(DataRow drMakeDetail in dtMakeDetailNew.Rows)
					{
						//						if( drMakeDetail["cnnwhcount"].ToString() == "")
						//							drMakeDetail["cnnwhcount"] = 0;
						//						if(drMakeDetail["cniChangeRate_main"].ToString() == "")
						//							drMakeDetail["cniChangeRate_main"] = 1;
						//						if(drMakeDetail["cniChangeRate_p"].ToString() == "")
						//							drMakeDetail["cniChangeRate_p"] =1 ;
						//						if(drMakeDetail["cniChangeRate_st"].ToString() == "")
						//							drMakeDetail["cniChangeRate_st"] = 1;
						//						if(drMakeDetail["cnbSelf"].ToString() == "")
						//							drMakeDetail["cnbSelf"] = true;
						double dChangeRate_p = Convert.ToDouble(drMakeDetail["cniChangeRate_p"].ToString());
						double dChangeRate_st = Convert.ToDouble(drMakeDetail["cniChangeRate_st"].ToString());
						//						decimal dChangeRate_main = Convert.ToDecimal(drMakeDetail["cniChangeRate_main"].ToString());
						Entity.MakeDetail mdnew = new MakeDetail(drMakeDetail);
						mdnew.cnbCollar = false;
						bool bSelf = Convert.ToBoolean(drMakeDetail["cnbSelf"].ToString());
						bool bCollar = Convert.ToBoolean(drMakeDetail["cnbCollar"].ToString());
						if(!bSelf || bCollar)
						{							
							mdnew.cnnCollarCount = mdnew.cnnMakeCount;
							//mdnew.cnnStCount = Convert.ToDecimal(Math.Ceiling((dChangeRate_p/dChangeRate_st)*Convert.ToDouble(mdnew.cnnCollarCount)));
							mdnew.cnnStCount = Convert.ToDecimal(Math.Round((dChangeRate_p/dChangeRate_st)*Convert.ToDouble(mdnew.cnnCollarCount),4));
							mdnew.cnnCount = 0;							
						}
						else
						{
							//decimal dwhCount = Convert.ToDecimal(drMakeDetail["cnnwhcount"].ToString());//库存数量
							//dwhCount = (dChangeRate_st/dChangeRate_main)/(dChangeRate_p/dChangeRate_main)*dwhCount;
							//if(dwhCount>=mdnew.cnnMakeCount)
							//{
							mdnew.cnnCollarCount = 0;
							mdnew.cnnStCount = 0;
							//mdnew.cnnMakeCount-mdnew.cnnCount;//+mdnew.cnnCollarCount;
							//mdnew.cnnCount = 0;
							//}
							//else
							//{
							//mdnew.cnnCollarCount = dwhCount;
							//mdnew.cnnCount = mdnew.cnnCount - dwhCount;
							//}
						}
						
						//oldmd.cnnAdjustCount = 0;
						//oldmd.cnnCount = md.cnnCount;
						//oldmd.cnnMakeCount = md.cnnMakeCount;
						

						mdnew.cnnMakeSerialNo = makeLog.cnnMakeSerialNo;																
						EntityMapping.Create(mdnew, trans);
					}
					EntityMapping.Create(makeLog, trans);

					oldLog.cnvcOperID = produceLog.cnvcOperID;
					oldLog.cndOperDate = dtSysTime;
					oldLog.cnvcProduceState = "2";
					EntityMapping.Update(oldLog, trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "预估流水："+makeLog.cnnMakeSerialNo.ToString();
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

		/// <summary>
		/// 生产预估
		/// </summary>
		/// <param name="produceLog"></param>
		/// <param name="operLog"></param>
		public void AddMakeLog(ProduceLog produceLog,OperLog operLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);					

					ProduceLog oldLog = new ProduceLog();
					oldLog.cnnProduceSerialNo = produceLog.cnnProduceSerialNo;
					oldLog = EntityMapping.Get(oldLog, trans) as ProduceLog;
					
					if(oldLog == null)
					{
						throw new Exception("生产计划不存在");
					}
					if(oldLog.cnvcProduceState != "1" )
					{
						throw new Exception("生产计划不能预估");
					}
					//制令产品					
					string strProduceDetail = 
					strProduceDetail = "select * from tbproducedetail where cnnProduceSerialNo="+produceLog.cnnProduceSerialNo.ToString();

					DataTable dtProduceDetail = SqlHelper.ExecuteDataTable(trans, CommandType.Text, strProduceDetail);					
					//存货档案
					DataTable dtInv = SingleTableQuery.ExcuteQuery("tbInventory",trans);
					DataTable dtComputationUnit = SingleTableQuery.ExcuteQuery("tbComputationUnit",trans);
					//BOM						
					DataTable dtBOM = SingleTableQuery.ExcuteQuery("tbbillofmaterials", trans);
					//生产组与商品类型对应表
//					DataTable dtGroupGoods = SingleTableQuery.ExcuteQuery("tbGroupGoods", trans);
//					DataTable dtGroupMake = SingleTableQuery.ExcuteQuery("tbGroupMake", trans);
					//生产组
//					string strGroups = "select * from tbNameCode where cnvcType='GROUP'";
//					DataTable dtGroup = SqlHelper.ExecuteDataTable(trans, CommandType.Text, strGroups);

					Entity.MakeDetail md = new MakeDetail();
					DataTable dtMakeDetailNew = md.ToTable().Clone();
					foreach(DataRow drProduceDetail in dtProduceDetail.Rows)
					{
						string strInvCode = drProduceDetail["cnvcInvCode"].ToString();
						string strProduceCount = drProduceDetail["cnnProduceCount"].ToString();
						string strordercount = drProduceDetail["cnnordercount"].ToString();
//						DataRow[] drs = dtMakeDetailNew.Select("cnvcInvCode='"+strInvCode+"'");
//						if(drs.Length>0)
//						{
//							drs[0]["cnnMakeCount"] = decimal.Parse(drProduceDetail["cnnMakeCount"].ToString())+decimal.Parse(drs[0]["cnnMakeCount"].ToString());
//						}
//						else
//						{
//							DataRow drMakeDetail = dtMakeDetailNew.NewRow();
//							drMakeDetail["cnvcInvCode"] = strInvCode;
//							drMakeDetail["cnnMakeCount"] = strProduceCount;
//							drMakeDetail["cnnCount"] = strProduceCount;
//							dtMakeDetailNew.Rows.Add(drMakeDetail);
//						}
						DataRow[] drs = dtMakeDetailNew.Select("cnvcInvcode='"+strInvCode+"'");
						if(drs.Length == 0)
						{
							DataRow drMakeDetailNew = dtMakeDetailNew.NewRow();
							drMakeDetailNew["cnvcInvCode"] = strInvCode;
							drMakeDetailNew["cnnMakeCount"] = strordercount;
							drMakeDetailNew["cnnCount"] = strProduceCount;
							drMakeDetailNew["cnnAdjustCount"] = 0;
							drMakeDetailNew["cnnStCount"] = 0;
							drMakeDetailNew["cnbCollar"] = false;
							drMakeDetailNew["cnnCollarCount"] = 0;
							dtMakeDetailNew.Rows.Add(drMakeDetailNew);
						}
						AnalyzeFormula(dtBOM,dtInv, strInvCode, strProduceCount, ref dtMakeDetailNew);					
					}					
					
					MakeLog makeLog =  new MakeLog();
					makeLog.cnnProduceSerialNo = produceLog.cnnProduceSerialNo;								
					makeLog.cndOperDate = dtSysTime;
					makeLog.cnvcOperID = produceLog.cnvcOperID;					
					//makeLog.cnvcMakeType = "0";

					OrderSerialNo serialNo = new OrderSerialNo();
					serialNo.cnvcFill = "0";
					serialNo.cnnSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));

					makeLog.cnnMakeSerialNo = serialNo.cnnSerialNo;

					Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnbSelf",dtInv,"cnvcInvCode","cnbSelf","");
//
//					string strwhSql = @"select cnvcInvCode,sum(cnnAvaQuantity) as cnnwhcount from tbCurrentStock where cnvcWhCOde
//in (select cnvcWhCOde from tbwarehouse where cnvcDepCode='"+oldLog.cnvcProduceDeptID+"')"
//						+"group by cnvcInvCode";
//					DataTable dtwh = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strwhSql);

					//Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnnwhcount",dtwh,"cnvcInvCode","cnnwhcount","");


					Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnvcProduceUnitCode",dtInv,"cnvcInvCode","cnvcProduceUnitCode","");					
					Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnvcStComUnitCode",dtInv,"cnvcInvCode","cnvcStComUnitCode","");
					Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnvcGroupCode",dtInv,"cnvcInvCode","cnvcGroupCode","");
					//Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnvcComUnitCode_main",dtComputationUnit,"cnvcGroupCode","cnvcGroupCode","cnbMainUnit=1");

					Helper.DataTableConvert(dtMakeDetailNew,"cnvcProduceUnitCode","cniChangeRate_p",dtComputationUnit,"cnvcComUnitCode","cniChangRate","");
					Helper.DataTableConvert(dtMakeDetailNew,"cnvcStComUnitCode","cniChangeRate_st",dtComputationUnit,"cnvcComUnitCode","cniChangRate","");
//					Helper.DataTableConvert(dtMakeDetailNew,"cnvcComUnitCode_main","cniChangeRate_main",dtComputationUnit,"cnvcComUnitCode","cniChangRate","");


					foreach(DataRow drMakeDetail in dtMakeDetailNew.Rows)
					{
//						if( drMakeDetail["cnnwhcount"].ToString() == "")
//							drMakeDetail["cnnwhcount"] = 0;
//						if(drMakeDetail["cniChangeRate_main"].ToString() == "")
//							drMakeDetail["cniChangeRate_main"] = 1;
						if(drMakeDetail["cniChangeRate_p"].ToString() == "")
							throw new Exception("无产品代码为："+drMakeDetail["cnvcInvCode"].ToString() +"的产品");
								//+drMakeDetail["cnvcProduceUnitCode"].ToString()
								//+"的换算率为："+drMakeDetail["cniChangeRate_p"].ToString());
//							drMakeDetail["cniChangeRate_p"] =1 ;
//						if(drMakeDetail["cniChangeRate_st"].ToString() == "")
//							drMakeDetail["cniChangeRate_st"] = 1;
//						if(drMakeDetail["cnbSelf"].ToString() == "")
//							drMakeDetail["cnbSelf"] = true;
						
						double dChangeRate_p = Convert.ToDouble(drMakeDetail["cniChangeRate_p"].ToString());
						double dChangeRate_st = Convert.ToDouble(drMakeDetail["cniChangeRate_st"].ToString());
//						decimal dChangeRate_main = Convert.ToDecimal(drMakeDetail["cniChangeRate_main"].ToString());
						Entity.MakeDetail mdnew = new MakeDetail(drMakeDetail);
						bool bSelf = Convert.ToBoolean(drMakeDetail["cnbSelf"].ToString());
						if(!bSelf)
						{							
							mdnew.cnnCollarCount = mdnew.cnnMakeCount;
							//mdnew.cnnStCount = Convert.ToDecimal(Math.Ceiling((dChangeRate_p/dChangeRate_st)*Convert.ToDouble(mdnew.cnnCollarCount)));
							mdnew.cnnStCount = Convert.ToDecimal(Math.Round((dChangeRate_p/dChangeRate_st)*Convert.ToDouble(mdnew.cnnCollarCount),4));
							mdnew.cnnCount = 0;							
						}
						else
						{
							//decimal dwhCount = Convert.ToDecimal(drMakeDetail["cnnwhcount"].ToString());//库存数量
							//dwhCount = (dChangeRate_st/dChangeRate_main)/(dChangeRate_p/dChangeRate_main)*dwhCount;
							//if(dwhCount>=mdnew.cnnMakeCount)
							//{
							mdnew.cnnCollarCount = 0;
							mdnew.cnnStCount = 0;
								//mdnew.cnnMakeCount-mdnew.cnnCount;//+mdnew.cnnCollarCount;
								//mdnew.cnnCount = 0;
							//}
							//else
							//{
								//mdnew.cnnCollarCount = dwhCount;
								//mdnew.cnnCount = mdnew.cnnCount - dwhCount;
							//}
						}
						
						//oldmd.cnnAdjustCount = 0;
						//oldmd.cnnCount = md.cnnCount;
						//oldmd.cnnMakeCount = md.cnnMakeCount;
						

						mdnew.cnnMakeSerialNo = makeLog.cnnMakeSerialNo;																
						EntityMapping.Create(mdnew, trans);
					}
					EntityMapping.Create(makeLog, trans);

					oldLog.cnvcOperID = produceLog.cnvcOperID;
					oldLog.cndOperDate = dtSysTime;
					oldLog.cnvcProduceState = "2";
					EntityMapping.Update(oldLog, trans);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "预估流水："+makeLog.cnnMakeSerialNo.ToString();
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

		private void AnalyzeFormulaSelf(DataTable dtBOM,DataTable dtInv,string strInvCode,string strProduceCount,ref DataTable dtMakeDetail)
		{
			DataRow[] drBOMs = dtBOM.Select("cnvcPartInvCode='" + strInvCode + "'");			
			foreach(DataRow drBOM in drBOMs)
			{
				string strComponentInvCode = drBOM["cnvcComponentInvCode"].ToString();
				string strBaseQtyN = drBOM["cnnBaseQtyN"].ToString();
				string strBaseQtyD = drBOM["cnnBaseQtyD"].ToString();

				decimal dCount = Convert.ToDecimal(strProduceCount)*Math.Round(decimal.Parse(strBaseQtyN)/decimal.Parse(strBaseQtyD),4);
				DataRow[] drMakeDetails = dtMakeDetail.Select("cnvcInvCode='" + strComponentInvCode + "'");	
				if(drMakeDetails.Length > 0)
				{
					drMakeDetails[0]["cnnMakeCount"] = dCount+decimal.Parse(drMakeDetails[0]["cnnMakeCount"].ToString());
					drMakeDetails[0]["cnnCount"] = drMakeDetails[0]["cnnMakeCount"];
				}
				else
				{
					DataRow dr = dtMakeDetail.NewRow();
					dr["cnvcInvCode"] = strComponentInvCode;
					dr["cnnMakeCount"] = dCount.ToString();		
					dr["cnnCount"] = dCount.ToString();	
					dr["cnbCollar"] = true;
					dtMakeDetail.Rows.Add(dr);
				}
				//				DataRow[] drBOMs2 = dtBOM.Select("cnvcPartInvCode='" + strComponentInvCode + "'");
				//				if(drBOMs2.Length>0)
				//				{
				//					AnalyzeFormula(dtBOM,dtInv, strComponentInvCode, dCount.ToString(), ref dtMakeDetail);
			}
		}
			

		private void AnalyzeFormula(DataTable dtBOM,DataTable dtInv,string strInvCode,string strProduceCount,ref DataTable dtMakeDetail)
		{
			DataRow[] drBOMs = dtBOM.Select("cnvcPartInvCode='" + strInvCode + "'");			
			foreach(DataRow drBOM in drBOMs)
			{
				string strComponentInvCode = drBOM["cnvcComponentInvCode"].ToString();
				string strBaseQtyN = drBOM["cnnBaseQtyN"].ToString();
				string strBaseQtyD = drBOM["cnnBaseQtyD"].ToString();
				if(decimal.Parse(strBaseQtyD)==0)throw new Exception(strInvCode+"配方错误，基础用量必须大于零，一般等于1");
				decimal dCount = Convert.ToDecimal(strProduceCount)*Math.Round(decimal.Parse(strBaseQtyN)/decimal.Parse(strBaseQtyD),4);
				DataRow[] drMakeDetails = dtMakeDetail.Select("cnvcInvCode='" + strComponentInvCode + "'");	
				if(drMakeDetails.Length > 0)
				{
					drMakeDetails[0]["cnnMakeCount"] = dCount+decimal.Parse(drMakeDetails[0]["cnnMakeCount"].ToString());
					drMakeDetails[0]["cnnCount"] = drMakeDetails[0]["cnnMakeCount"];
				}
				else
				{
					DataRow dr = dtMakeDetail.NewRow();
					dr["cnvcInvCode"] = strComponentInvCode;
					dr["cnnMakeCount"] = dCount.ToString();		
					dr["cnnCount"] = dCount.ToString();	
					dtMakeDetail.Rows.Add(dr);
				}
				DataRow[] drBOMs2 = dtBOM.Select("cnvcPartInvCode='" + strComponentInvCode + "'");
				if(drBOMs2.Length>0)
				{
					AnalyzeFormula(dtBOM,dtInv, strComponentInvCode, dCount.ToString(), ref dtMakeDetail);
				}
			}
		}		



		public void UpdateMakeDetail(string strProduceSerialNo,MakeDetail md,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);
					
					MakeLog mlog = new MakeLog();
					mlog.cnnMakeSerialNo = md.cnnMakeSerialNo;
					mlog.cnnProduceSerialNo = Convert.ToDecimal(strProduceSerialNo);
					mlog = EntityMapping.Get(mlog,trans) as MakeLog;
					if(mlog == null)
					{
						throw new Exception("生产预估为空");
					}
					ProduceLog oldLog = new ProduceLog();
					oldLog.cnnProduceSerialNo = mlog.cnnProduceSerialNo;
					oldLog = EntityMapping.Get(oldLog, trans) as ProduceLog;
					if(oldLog == null)
					{
						throw new Exception("生产计划为空");
					}
					if(oldLog.cnvcProduceState != "2")
					{
						throw new Exception("生产计划状态不能更新预估数量");
					}
					MakeDetail oldmd = new MakeDetail();
					oldmd.cnnMakeSerialNo = md.cnnMakeSerialNo;
					oldmd.cnvcInvCode = md.cnvcInvCode;
					oldmd = EntityMapping.Get(oldmd,trans) as MakeDetail;

					if(oldmd == null)
					{
						throw new Exception("生产预估无此产品");
					}
					Entity.Inventory inv = new AMSApp.zhenghua.Entity.Inventory();
					inv.cnvcInvCode = oldmd.cnvcInvCode;
					inv = EntityMapping.Get(inv,trans) as Entity.Inventory;
					if(inv == null)
					{
						throw new Exception("存货档案无此产品");
					}
					if(!inv.cnbSelf && md.cnnAdjustCount != 0)
					{
						throw new Exception("此产品非自制无法调整生产数量");
					}
					oldmd.cnnAdjustCount = md.cnnAdjustCount;
					oldmd.cnnStCount = md.cnnStCount;
					EntityMapping.Update(oldmd,trans);

					string strComments = "["+oldmd.cnvcInvCode+":"+oldmd.cnnAdjustCount.ToString()+"]";
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "预估流水："+md.cnnMakeSerialNo.ToString()+"，存货编码："+oldmd.cnvcInvCode;
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
		/// <summary>
		/// 自生产预估调整
		/// </summary>
		/// <param name="strProduceSerialNo"></param>
		/// <param name="operLog"></param>
		public void AdjustMakeDetailSelf(string strProduceSerialNo,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);
					
					string strsql = "select * from tbMakeLog where cnnProduceSerialNo="+strProduceSerialNo;
					DataTable dtMakeLog = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strsql);
					if(dtMakeLog.Rows.Count == 0)
					{
						throw new Exception("生产预估为空");
					}

					MakeLog mlog = new MakeLog(dtMakeLog);
					
					ProduceLog oldLog = new ProduceLog();
					oldLog.cnnProduceSerialNo = mlog.cnnProduceSerialNo;
					oldLog = EntityMapping.Get(oldLog, trans) as ProduceLog;
					if(oldLog == null)
					{
						throw new Exception("生产计划为空");
					}
					if(!(oldLog.cnvcProduceState == "2" || oldLog.cnvcProduceState == "3") )
					{
						throw new Exception("生产计划不能调整预估");
					}
					//cnnAdjustCount<>0 and
					strsql = "select * from tbmakedetail where  cnnMakeSerialNo="+mlog.cnnMakeSerialNo.ToString();
					DataTable dtMakeDetail = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strsql);

					DataTable dtBOM = SingleTableQuery.ExcuteQuery("tbbillofmaterials", trans);

					//存货档案
					DataTable dtInv = SingleTableQuery.ExcuteQuery("tbInventory",trans);
					DataTable dtComputationUnit = SingleTableQuery.ExcuteQuery("tbComputationUnit",trans);
					//ntity.MakeDetail md = new MakeDetail();
					DataTable dtMakeDetailNew = dtMakeDetail.Copy();

					foreach(DataRow dr in dtMakeDetail.Rows)
					{
						MakeDetail md1 = new MakeDetail(dr);
						if(md1.cnnAdjustCount != 0)
						{
							dr["cnbCollar"] = true;
							AnalyzeFormula2Self(dtBOM,dtInv, md1.cnvcInvCode, md1.cnnAdjustCount.ToString(), ref dtMakeDetailNew);
						}
					}

					Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnbSelf",dtInv,"cnvcInvCode","cnbSelf","");

					//					string strwhSql = @"select cnvcInvCode,sum(cnnAvaQuantity) as cnnwhcount from tbCurrentStock where cnvcWhCOde
					//in (select cnvcWhCOde from tbwarehouse where cnvcDepCode='"+oldLog.cnvcProduceDeptID+"')"
					//						+"group by cnvcInvCode";
					//					DataTable dtwh = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strwhSql);
					//
					//					Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnnwhcount",dtwh,"cnvcInvCode","cnnwhcount","");


					Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnvcProduceUnitCode",dtInv,"cnvcInvCode","cnvcProduceUnitCode","");					
					Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnvcStComUnitCode",dtInv,"cnvcInvCode","cnvcStComUnitCode","");
					//Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnvcGroupCode",dtInv,"cnvcInvCode","cnvcGroupCode","");
					//Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnvcComUnitCode_main",dtComputationUnit,"cnvcGroupCode","cnvcGroupCode","cnbMainUnit=1");

					Helper.DataTableConvert(dtMakeDetailNew,"cnvcProduceUnitCode","cniChangeRate_p",dtComputationUnit,"cnvcComUnitCode","cniChangRate","");
					Helper.DataTableConvert(dtMakeDetailNew,"cnvcStComUnitCode","cniChangeRate_st",dtComputationUnit,"cnvcComUnitCode","cniChangRate","");
					//Helper.DataTableConvert(dtMakeDetailNew,"cnvcComUnitCode_main","cniChangeRate_main",dtComputationUnit,"cnvcComUnitCode","cniChangRate","");

					foreach(DataRow dr in dtMakeDetailNew.Rows)
					{
						//						if(dr["cnnwhcount"].ToString() == "")
						//							dr["cnnwhcount"] = 0;
						//						if(dr["cniChangeRate_main"].ToString() == "")
						//							dr["cniChangeRate_main"] = 1;
						MakeDetail md = new MakeDetail(dr);
						MakeDetail oldmd = new MakeDetail();
						oldmd.cnnMakeSerialNo = md.cnnMakeSerialNo;
						oldmd.cnvcInvCode = md.cnvcInvCode;
						oldmd = EntityMapping.Get(oldmd,trans) as MakeDetail;

						double dChangeRate_p = Convert.ToDouble(dr["cniChangeRate_p"].ToString());
						double dChangeRate_st = Convert.ToDouble(dr["cniChangeRate_st"].ToString());
						//decimal dChangeRate_main = Convert.ToDecimal(dr["cniChangeRate_main"].ToString());

						//						DataRow[] drInvs = dtInv.Select("cnvcInvCode='"+oldmd.cnvcInvCode+"'");
						//						if(drInvs.Length==0)
						//						{
						//							throw new Exception("存货档案未找到");
						//						}
						//						Entity.Inventory inv = new AMSApp.zhenghua.Entity.Inventory(drInvs[0]);

						//oldmd.cnnCount = oldmd.cnnCount + md.cnnAdjustCount;
						bool bSelf = Convert.ToBoolean(dr["cnbSelf"].ToString());
						bool bCollar = Convert.ToBoolean(dr["cnbCollar"].ToString());
						if(!bSelf || bCollar)
						{
							oldmd.cnnCollarCount = oldmd.cnnCollarCount+md.cnnCount;
							//oldmd.cnnStCount = Convert.ToDecimal(Math.Ceiling((dChangeRate_p/dChangeRate_st)*Convert.ToDouble(oldmd.cnnCollarCount)));
							oldmd.cnnStCount = Convert.ToDecimal(Math.Round((dChangeRate_p/dChangeRate_st)*Convert.ToDouble(oldmd.cnnCollarCount),4));
							oldmd.cnnCount = 0;
						}
						else
						{
							//自制
							//decimal dwhCount = Convert.ToDecimal(dr["cnnwhcount"].ToString());//库存数量
							//dwhCount = (dChangeRate_st/dChangeRate_main)/(dChangeRate_p/dChangeRate_main)*dwhCount;
							//oldmd.cnnStCount = oldmd.cnnStCount-md.cnnCount;
							//oldmd.cnnCount = oldmd.cnnCount+md.cnnCount;
							//if(dwhCount>=oldmd.cnnStCount-md.cnnCount)
							
							//							if(dwhCount>=md.cnnCount-oldmd.cnnCollarCount)
							//							{
							//oldmd.cnnCollarCount = md.cnnCount+oldmd.cnnCollarCount;
							//oldmd.cnnCount = 0;
							//							}
							//							else
							//							{
							//oldmd.cnnCollarCount = 0;//oldmd.cnnCollarCount + dwhCount;
							//oldmd.cnnStCount = 0;
							//oldmd.cnnCollarCount = Convert.ToDecimal(Math.Ceiling((dChangeRate_st/dChangeRate_p)*Convert.ToDouble(oldmd.cnnStCount)));//0
							oldmd.cnnCollarCount = Convert.ToDecimal(Math.Round((dChangeRate_st/dChangeRate_p)*Convert.ToDouble(oldmd.cnnStCount),4));//0
							oldmd.cnnCount = md.cnnCount;// - md.cnnCollarCount;//dwhCount;
							//							}
						}
						
						oldmd.cnnAdjustCount = 0;
						//oldmd.cnnCount = md.cnnCount;
						//oldmd.cnnMakeCount = md.cnnMakeCount;
						//oldmd.cnnStCount = (dChangeRate_p/dChangeRate_main)/(dChangeRate_st/dChangeRate_main)*oldmd.cnnCollarCount;
						EntityMapping.Update(oldmd,trans);
					}
					

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "预估流水："+mlog.cnnMakeSerialNo.ToString();
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

		/// <summary>
		/// 预估调整
		/// </summary>
		/// <param name="strProduceSerialNo"></param>
		/// <param name="operLog"></param>
		public void AdjustMakeDetail(string strProduceSerialNo,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);
					
					string strsql = "select * from tbMakeLog where cnnProduceSerialNo="+strProduceSerialNo;
					DataTable dtMakeLog = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strsql);
					if(dtMakeLog.Rows.Count == 0)
					{
						throw new Exception("生产预估为空");
					}

					MakeLog mlog = new MakeLog(dtMakeLog);
					
					ProduceLog oldLog = new ProduceLog();
					oldLog.cnnProduceSerialNo = mlog.cnnProduceSerialNo;
					oldLog = EntityMapping.Get(oldLog, trans) as ProduceLog;
					if(oldLog == null)
					{
						throw new Exception("生产计划为空");
					}
					if(!(oldLog.cnvcProduceState == "2" || oldLog.cnvcProduceState == "3") )
					{
						throw new Exception("生产计划不能调整预估");
					}
					//cnnAdjustCount<>0 and
					strsql = "select * from tbmakedetail where  cnnMakeSerialNo="+mlog.cnnMakeSerialNo.ToString();
					DataTable dtMakeDetail = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strsql);

					DataTable dtBOM = SingleTableQuery.ExcuteQuery("tbbillofmaterials", trans);

					//存货档案
					DataTable dtInv = SingleTableQuery.ExcuteQuery("tbInventory",trans);
					DataTable dtComputationUnit = SingleTableQuery.ExcuteQuery("tbComputationUnit",trans);
					//ntity.MakeDetail md = new MakeDetail();
					DataTable dtMakeDetailNew = dtMakeDetail.Copy();

					foreach(DataRow dr in dtMakeDetail.Rows)
					{
						MakeDetail md1 = new MakeDetail(dr);
						if(md1.cnnAdjustCount != 0)
						AnalyzeFormula2(dtBOM,dtInv, md1.cnvcInvCode, md1.cnnAdjustCount.ToString(), ref dtMakeDetailNew);
					}

					Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnbSelf",dtInv,"cnvcInvCode","cnbSelf","");

//					string strwhSql = @"select cnvcInvCode,sum(cnnAvaQuantity) as cnnwhcount from tbCurrentStock where cnvcWhCOde
//in (select cnvcWhCOde from tbwarehouse where cnvcDepCode='"+oldLog.cnvcProduceDeptID+"')"
//						+"group by cnvcInvCode";
//					DataTable dtwh = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strwhSql);
//
//					Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnnwhcount",dtwh,"cnvcInvCode","cnnwhcount","");


					Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnvcProduceUnitCode",dtInv,"cnvcInvCode","cnvcProduceUnitCode","");					
					Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnvcStComUnitCode",dtInv,"cnvcInvCode","cnvcStComUnitCode","");
					//Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnvcGroupCode",dtInv,"cnvcInvCode","cnvcGroupCode","");
					//Helper.DataTableConvert(dtMakeDetailNew,"cnvcInvCode","cnvcComUnitCode_main",dtComputationUnit,"cnvcGroupCode","cnvcGroupCode","cnbMainUnit=1");

					Helper.DataTableConvert(dtMakeDetailNew,"cnvcProduceUnitCode","cniChangeRate_p",dtComputationUnit,"cnvcComUnitCode","cniChangRate","");
					Helper.DataTableConvert(dtMakeDetailNew,"cnvcStComUnitCode","cniChangeRate_st",dtComputationUnit,"cnvcComUnitCode","cniChangRate","");
					//Helper.DataTableConvert(dtMakeDetailNew,"cnvcComUnitCode_main","cniChangeRate_main",dtComputationUnit,"cnvcComUnitCode","cniChangRate","");

					foreach(DataRow dr in dtMakeDetailNew.Rows)
					{
//						if(dr["cnnwhcount"].ToString() == "")
//							dr["cnnwhcount"] = 0;
//						if(dr["cniChangeRate_main"].ToString() == "")
//							dr["cniChangeRate_main"] = 1;
						MakeDetail md = new MakeDetail(dr);
						MakeDetail oldmd = new MakeDetail();
						oldmd.cnnMakeSerialNo = md.cnnMakeSerialNo;
						oldmd.cnvcInvCode = md.cnvcInvCode;
						oldmd = EntityMapping.Get(oldmd,trans) as MakeDetail;

						double dChangeRate_p = Convert.ToDouble(dr["cniChangeRate_p"].ToString());
						double dChangeRate_st = Convert.ToDouble(dr["cniChangeRate_st"].ToString());
						//decimal dChangeRate_main = Convert.ToDecimal(dr["cniChangeRate_main"].ToString());

//						DataRow[] drInvs = dtInv.Select("cnvcInvCode='"+oldmd.cnvcInvCode+"'");
//						if(drInvs.Length==0)
//						{
//							throw new Exception("存货档案未找到");
//						}
//						Entity.Inventory inv = new AMSApp.zhenghua.Entity.Inventory(drInvs[0]);

						//oldmd.cnnCount = oldmd.cnnCount + md.cnnAdjustCount;
						bool bSelf = Convert.ToBoolean(dr["cnbSelf"].ToString());
						if(!bSelf)
						{
							oldmd.cnnCollarCount = oldmd.cnnCollarCount+md.cnnCount;
							//oldmd.cnnStCount = Convert.ToDecimal(Math.Ceiling((dChangeRate_p/dChangeRate_st)*Convert.ToDouble(oldmd.cnnCollarCount)));
							oldmd.cnnStCount = Convert.ToDecimal(Math.Round((dChangeRate_p/dChangeRate_st)*Convert.ToDouble(oldmd.cnnCollarCount),4));
							oldmd.cnnCount = 0;
						}
						else
						{
							//自制
							//decimal dwhCount = Convert.ToDecimal(dr["cnnwhcount"].ToString());//库存数量
							//dwhCount = (dChangeRate_st/dChangeRate_main)/(dChangeRate_p/dChangeRate_main)*dwhCount;
							//oldmd.cnnStCount = oldmd.cnnStCount-md.cnnCount;
							//oldmd.cnnCount = oldmd.cnnCount+md.cnnCount;
							//if(dwhCount>=oldmd.cnnStCount-md.cnnCount)
							
//							if(dwhCount>=md.cnnCount-oldmd.cnnCollarCount)
//							{
								//oldmd.cnnCollarCount = md.cnnCount+oldmd.cnnCollarCount;
								//oldmd.cnnCount = 0;
//							}
//							else
//							{
								//oldmd.cnnCollarCount = 0;//oldmd.cnnCollarCount + dwhCount;
								//oldmd.cnnStCount = 0;
								//oldmd.cnnCollarCount = Convert.ToDecimal(Math.Ceiling((dChangeRate_st/dChangeRate_p)*Convert.ToDouble(oldmd.cnnStCount)));//0;
							oldmd.cnnCollarCount = Convert.ToDecimal(Math.Round((dChangeRate_st/dChangeRate_p)*Convert.ToDouble(oldmd.cnnStCount),4));//0;
								oldmd.cnnCount = md.cnnCount;// - md.cnnCollarCount;//dwhCount;
//							}
						}
						
						oldmd.cnnAdjustCount = 0;
						//oldmd.cnnCount = md.cnnCount;
						//oldmd.cnnMakeCount = md.cnnMakeCount;
						//oldmd.cnnStCount = (dChangeRate_p/dChangeRate_main)/(dChangeRate_st/dChangeRate_main)*oldmd.cnnCollarCount;
						EntityMapping.Update(oldmd,trans);
					}
					

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "预估流水："+mlog.cnnMakeSerialNo.ToString();
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

		private void AnalyzeFormula2(DataTable dtBOM,DataTable dtInv,string strInvCode,string strProduceCount,ref DataTable dtMakeDetail)
		{
			DataRow[] drMakeDetail = dtMakeDetail.Select("cnvcInvCode='"+strInvCode+"'");
			if(drMakeDetail.Length>0)
			{
				drMakeDetail[0]["cnnCount"] = Convert.ToDecimal(drMakeDetail[0]["cnnAdjustCount"].ToString()) +
					Convert.ToDecimal(drMakeDetail[0]["cnnCount"].ToString());
			}
			DataRow[] drBOMs = dtBOM.Select("cnvcPartInvCode='" + strInvCode + "'");			
			foreach(DataRow drBOM in drBOMs)
			{
				string strComponentInvCode = drBOM["cnvcComponentInvCode"].ToString();
				string strBaseQtyN = drBOM["cnnBaseQtyN"].ToString();
				string strBaseQtyD = drBOM["cnnBaseQtyD"].ToString();

				if(decimal.Parse(strBaseQtyD)==0)throw new Exception(strInvCode+"配方错误，基础用量必须大于零，一般等于1");
				decimal dCount = Convert.ToDecimal(strProduceCount)*Math.Round(decimal.Parse(strBaseQtyN)/decimal.Parse(strBaseQtyD),4);
				DataRow[] drMakeDetails = dtMakeDetail.Select("cnvcInvCode='" + strComponentInvCode + "'");	
				if(drMakeDetails.Length > 0)
				{
					//drMakeDetails[0]["cnnMakeCount"] = dCount+decimal.Parse(drMakeDetails[0]["cnnMakeCount"].ToString());
					drMakeDetails[0]["cnnCount"] = dCount+decimal.Parse(drMakeDetails[0]["cnnCount"].ToString());
				}
				else
				{
					DataRow dr = dtMakeDetail.NewRow();
					dr["cnvcInvCode"] = strComponentInvCode;
					//dr["cnnMakeCount"] = dCount.ToString();		
					dr["cnnCount"] = dCount.ToString();	
					dtMakeDetail.Rows.Add(dr);
				}
				DataRow[] drBOMs2 = dtBOM.Select("cnvcPartInvCode='" + strComponentInvCode + "'");
				if(drBOMs2.Length>0)
				{
					AnalyzeFormula2(dtBOM,dtInv, strComponentInvCode, dCount.ToString(), ref dtMakeDetail);
				}
			}
		}		


		private void AnalyzeFormula2Self(DataTable dtBOM,DataTable dtInv,string strInvCode,string strProduceCount,ref DataTable dtMakeDetail)
		{
			DataRow[] drMakeDetail = dtMakeDetail.Select("cnvcInvCode='"+strInvCode+"'");
			if(drMakeDetail.Length>0)
			{
				drMakeDetail[0]["cnnCount"] = Convert.ToDecimal(drMakeDetail[0]["cnnAdjustCount"].ToString()) +
					Convert.ToDecimal(drMakeDetail[0]["cnnCount"].ToString());
			}
			DataRow[] drBOMs = dtBOM.Select("cnvcPartInvCode='" + strInvCode + "'");			
			foreach(DataRow drBOM in drBOMs)
			{
				string strComponentInvCode = drBOM["cnvcComponentInvCode"].ToString();
				string strBaseQtyN = drBOM["cnnBaseQtyN"].ToString();
				string strBaseQtyD = drBOM["cnnBaseQtyD"].ToString();

				decimal dCount = Convert.ToDecimal(strProduceCount)*Math.Round(decimal.Parse(strBaseQtyN)/decimal.Parse(strBaseQtyD),4);
				DataRow[] drMakeDetails = dtMakeDetail.Select("cnvcInvCode='" + strComponentInvCode + "'");	
				if(drMakeDetails.Length > 0)
				{
					//drMakeDetails[0]["cnnMakeCount"] = dCount+decimal.Parse(drMakeDetails[0]["cnnMakeCount"].ToString());
					drMakeDetails[0]["cnnCount"] = dCount+decimal.Parse(drMakeDetails[0]["cnnCount"].ToString());
				}
				else
				{
					DataRow dr = dtMakeDetail.NewRow();
					dr["cnvcInvCode"] = strComponentInvCode;
					//dr["cnnMakeCount"] = dCount.ToString();		
					dr["cnnCount"] = dCount.ToString();	
					dr["cnbCollar"] = true;
					dtMakeDetail.Rows.Add(dr);
				}
//				DataRow[] drBOMs2 = dtBOM.Select("cnvcPartInvCode='" + strComponentInvCode + "'");
//				if(drBOMs2.Length>0)
//				{
//					AnalyzeFormula2(dtBOM,dtInv, strComponentInvCode, dCount.ToString(), ref dtMakeDetail);
//				}
			}
		}		


		#endregion

		#region 清除预估数据
		
		public void ClearMake(string strProduceSerialNo,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);
					
					Entity.ProduceLog pl = new ProduceLog();
					pl.cnnProduceSerialNo = Convert.ToDecimal(strProduceSerialNo);

					pl = EntityMapping.Get(pl,trans) as Entity.ProduceLog;
					if(null == pl)
						throw new Exception("不能定位生产计划！");
					if(pl.cnvcProduceState != "2")
						throw new Exception("不能清除预估数据，生产计划未曾预估，或者已经领料！");
					pl.cnvcOperID = operLog.cnvcOperID;
					pl.cndOperDate = dtSysTime;
					pl.cnvcProduceState = "1";
					EntityMapping.Update(pl,trans);

					string strmakelog = "select * from tbmakelog where cnnproduceserialno="+strProduceSerialNo;
					DataTable dtMakeLog = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strmakelog);
					if(dtMakeLog.Rows.Count == 0) throw new Exception("不能定位原有预估数据");

					Entity.MakeLog ml = new MakeLog(dtMakeLog);
//					ml.cnnProduceSerialNo = pl.cnnProduceSerialNo;
//					ml = EntityMapping.Get(ml,trans) as MakeLog;
					
					EntityMapping.Delete(ml,trans);
					

					string strdelmakedetail = "delete FROM tbMakeDetail WHERE cnnMakeSerialNo="+ml.cnnMakeSerialNo.ToString();
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strdelmakedetail);

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "预估流水"+ml.cnnMakeSerialNo.ToString();
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

		public void Collar(string strMakeSerialNo,Entity.RdRecord rr,ArrayList alrrd,OperLog operLog,string strWarehouse)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					rr.cndMakeDate = dtSysTime;
					long rrid = EntityMapping.Create(rr,trans);
					

					DataTable dtInv = SingleTableQuery.ExcuteQuery("tbInventory",trans);
					DataTable dtComputationUnit = SingleTableQuery.ExcuteQuery("tbComputationUnit",trans);

					for(int i = 0;i<alrrd.Count;i++)
					{
											
						Entity.RdRecordDetail rrd = alrrd[i] as Entity.RdRecordDetail;
						rrd.cnnRdID = Convert.ToDecimal(rrid);//rr.cnnRdID;

						DataRow[] drcus = dtComputationUnit.Select("cnvcGroupCode='"+rrd.cnvcGroupCode+"' and cnbMainUnit=1");
						if(drcus.Length == 0) throw new Exception(rrd.cnvcGroupCode+"未设置主计量单位");
						decimal dchangerate = Convert.ToDecimal(drcus[0]["cnichangrate"].ToString());
						DataRow[] drcus1 = dtComputationUnit.Select("cnvcGroupCode='"+rrd.cnvcGroupCode+"' and cnvcComUnitCode='"+rrd.cnvcComunitCode+"'");
						decimal dchangerate_st = Convert.ToDecimal(drcus1[0]["cnichangrate"].ToString());

						string strcssql = "SELECT * FROM tbCurrentStock WHERE cnvcWhCode='"+strWarehouse+"' AND cnvcInvCode='"+rrd.cnvcInvCode+"'"
							+ " and CONVERT(char(10),isnull(cndExpDate,''),121)>=CONVERT(char(10),getdate(),121)  order by cndExpDate";
						string strcssql2 = "SELECT isnull(sum(cnnAvaQuantity),0) FROM tbCurrentStock WHERE cnvcWhCode='"+strWarehouse+"' AND cnvcInvCode='"+rrd.cnvcInvCode+"'"
							+ " and CONVERT(char(10),isnull(cndExpDate,''),121)>=CONVERT(char(10),getdate(),121) ";
						DataTable dtcs = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strcssql);
						decimal davaquantity = Convert.ToDecimal(SqlHelper.ExecuteScalar(trans,CommandType.Text,strcssql2).ToString());
						if(dtcs.Rows.Count == 0)
						{
							throw new Exception(rrd.cnvcInvCode+"无库存");
						}
						if(davaquantity - rrd.cnnQuantity*dchangerate_st/dchangerate<0)
							throw new Exception(rrd.cnvcInvCode+"库存不足");
						decimal dhave=0;
						foreach(DataRow drcs in dtcs.Rows)
						{
							
							Entity.RdRecordDetail rrd1 = rrd.Copy() as Entity.RdRecordDetail;
							Entity.CurrentStock cs = new CurrentStock(drcs);
							//if(cs.cnnAvaQuantity - rrd.cnnQuantity*dchangerate_st/dchangerate<0)
							if(cs.cnnAvaQuantity>rrd.cnnQuantity*dchangerate_st/dchangerate-dhave)
							{
								cs.cnnAvaQuantity = cs.cnnAvaQuantity - rrd.cnnQuantity*dchangerate_st/dchangerate;
								cs.cnnQuantity = cs.cnnQuantity - rrd.cnnQuantity*dchangerate_st/dchangerate;

								rrd1.cnnQuantity =  rrd.cnnQuantity;//*dchangerate_st/dchangerate;
								
								EntityMapping.Update(cs,trans);

								rrd1.cndMdate = cs.cndMdate;
								rrd1.cndExpDate = cs.cndExpDate;
								EntityMapping.Create(rrd1,trans);	
								break;
							}
							else if (cs.cnnAvaQuantity>0)
							{

								rrd1.cnnQuantity = cs.cnnAvaQuantity * (dchangerate / dchangerate_st);

								cs.cnnAvaQuantity = 0;
								cs.cnnQuantity = 0;

								rrd1.cndMdate = cs.cndMdate;
								rrd1.cndExpDate = cs.cndExpDate;
								EntityMapping.Create(rrd1,trans);	
								
								EntityMapping.Update(cs,trans);
								dhave += cs.cnnAvaQuantity ;//* (dchangerate / dchangerate_st);
							}
							
							
						}

					}
					
					string strsql = "update tbMakeDetail set cnbCollar=1 where cnnMakeSerialNo="+strMakeSerialNo;
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql);

					string strsql1 = "update tbproducelog set cnvcproducestate='3' where cnnproduceserialno="+rr.cnnProorderID.ToString();
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql1);
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "预估流水："+strMakeSerialNo;
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

		public void CheckInWh(string strMakeSerialNo,Entity.RdRecord rr,ArrayList alrrd,OperLog operLog,string strWarehouse)//,BusiLog busiLog)
		{
			
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					//4
//					string strpl = "SELECT * FROM dbo.tbProduceLog WHERE cnnProduceSerialNo="+rr.cnnProorderID.ToString();
//					DataTable dtpl = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strpl);
//					if(dtpl.Rows.Count == 0)
//						throw new Exception("不能找到生产计划");
//					Entity.ProduceLog pl = new ProduceLog(dtpl);
//					if(pl.cnvcProduceState != "4")
//						throw new Exception("只有盘点后的生产计划才能做入库");
					rr.cndMakeDate = dtSysTime;
					long rrid = EntityMapping.Create(rr,trans);
					//trans.Commit();
					DataTable dtInv = SingleTableQuery.ExcuteQuery("tbInventory",trans);
					DataTable dtComputationUnit = SingleTableQuery.ExcuteQuery("tbComputationUnit",trans);

					for(int i = 0;i<alrrd.Count;i++)
					{
						Entity.RdRecordDetail rrd = alrrd[i] as Entity.RdRecordDetail;
						rrd.cnnRdID = Convert.ToDecimal(rrid);
						//rrd.cndMdate = dtSysTime;
						if(rrd.cnnQuantity>0)
						EntityMapping.Create(rrd,trans);

						//rrd.cnvcComunitCode
						//rrd.cnvcGroupCode
						DataRow[] drcus = dtComputationUnit.Select("cnvcGroupCode='"+rrd.cnvcGroupCode+"' and cnbMainUnit=1");
						if(drcus.Length == 0) throw new Exception(rrd.cnvcGroupCode+"未设置主计量单位");
						decimal dchangerate = Convert.ToDecimal(drcus[0]["cnichangrate"].ToString());
						DataRow[] drcus1 = dtComputationUnit.Select("cnvcGroupCode='"+rrd.cnvcGroupCode+"' and cnvcComUnitCode='"+rrd.cnvcComunitCode+"'");
						decimal dchangerate_st = Convert.ToDecimal(drcus1[0]["cnichangrate"].ToString());

						string strcssql = "SELECT * FROM tbCurrentStock WHERE cnvcWhCode='"+strWarehouse+"' AND cnvcInvCode='"+rrd.cnvcInvCode+"' and CONVERT(CHAR(10),cndExpDate,121)='"+rrd.cndExpDate.ToString("yyyy-MM-dd")+"'";
						DataTable dtcs = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strcssql);
						if(rrd.cnnQuantity>0)
						{
							if(dtcs.Rows.Count == 0)
							{
								//throw new Exception(rrd.cnvcInvCode+"无库存");
								Entity.CurrentStock cs = new CurrentStock();
								//cs.cndMdate = rrd.cndMdate;//dtSysTime;
								cs.cnnAvaQuantity = rrd.cnnQuantity*dchangerate_st/dchangerate;
								cs.cnnQuantity = rrd.cnnQuantity*dchangerate_st/dchangerate;
								cs.cnvcInvCode = rrd.cnvcInvCode;
								cs.cnvcWhCode = strWarehouse;
								cs.cnvcStopFlag = "0";
								cs.cndMdate = rrd.cndMdate;//dtSysTime;
								cs.cndExpDate = rrd.cndExpDate;
								EntityMapping.Create(cs,trans);	
							}
							else
							{
								Entity.CurrentStock cs = new CurrentStock(dtcs);
								//if(cs.cnnAvaQuantity - rrd.cnnQuantity<0)
								//	throw new Exception(rrd.cnvcInvCode+"库存不足");
								cs.cnnAvaQuantity = cs.cnnAvaQuantity + rrd.cnnQuantity*dchangerate_st/dchangerate;
								cs.cnnQuantity = cs.cnnQuantity + rrd.cnnQuantity*dchangerate_st/dchangerate;
								EntityMapping.Update(cs,trans);
							}
						}
						string strpcsql = "select * from tbProduceCheckLog where cnnproduceserialno="+rr.cnnProorderID+" and cnvcinvcode='"+rrd.cnvcInvCode+"'";
						DataTable dtpc = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strpcsql);
						if(dtpc.Rows.Count>0)
						{
							Entity.ProduceCheckLog pc = new ProduceCheckLog(dtpc);
							pc.cndExpDate = rrd.cndExpDate;
							pc.cndMDate = rrd.cndMdate;
							EntityMapping.Update(pc,trans);
						}
					}
					
					string strsql = "update tbproducechecklog set cnbInWh=1 where cnnproduceserialno="+rr.cnnProorderID.ToString();
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql);

					string strsql1 = "update tbproducelog set cnvcproducestate='5' where cnnproduceserialno="+rr.cnnProorderID.ToString();
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql1);
					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "生产流水："+rr.cnnProorderID.ToString();
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

		public void ProduceCLose(string strProduceSerialNo,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);

					string strsql = "select * from tbproducelog where cnnproduceserialno="+strProduceSerialNo;
					DataTable dtpd = SqlHelper.ExecuteDataTable(trans,CommandType.Text,strsql);
					if(dtpd.Rows.Count == 0) throw new Exception("没有找到生产计划");
					Entity.ProduceLog pl = new ProduceLog(dtpd);
					if(!(pl.cnvcProduceState == "5" || pl.cnvcProduceState == "7"))
						throw new Exception("没有生产盘点入库或者分货出库的生产计划不能竣工！");
					

					pl.cnvcProduceState = "8";
					pl.cnvcOperID = operLog.cnvcOperID;
					pl.cndOperDate = dtSysTime;
					EntityMapping.Update(pl,trans);

					strsql = @"UPDATE dbo.tbOrderBook SET cnvcOrderState='3' WHERE cnvcOrderState='2' and cnnOrderSerialNo IN(
SELECT cnnOrderSerialNo FROM dbo.tbProduceOrderLog WHERE cnnProduceSerialNo="+strProduceSerialNo+")";
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strsql);
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

		//门市生产
		public void SelfProduce(string strOrderSerialNo,string strDeptID,OperLog operLog)//,BusiLog busiLog)
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
					
					OrderBook ob = new OrderBook();
					ob.cnnOrderSerialNo = decimal.Parse(strOrderSerialNo);
					ob = EntityMapping.Get(ob, trans) as OrderBook;
					if(ob == null)
					{
						throw new Exception("未找到订单");
					}
					if(ob.cnvcOrderState != "0")
					{
						throw new Exception("订单已作处理");
					}
					DataTable dtOrderBookDetail =
						SqlHelper.ExecuteDataTable(trans, CommandType.Text,
						                           "select * from tbOrderBookDetail where cnnOrderSerialNo=" + strOrderSerialNo);
					DataTable dtDosage = SqlHelper.ExecuteDataTable(trans, CommandType.Text, "select * from tbDosage");
					//配方表	
					string strFormula = "select cnvcProductCode,cnvcProductType,cnvcUnit,cnvcPortionUnit,cnnPortionCount from tbFormula";
					DataTable dtFormula = SqlHelper.ExecuteDataTable(trans, CommandType.Text, strFormula);
					string strMaterial = "select * from tbMaterial";
					DataTable dtMaterial = SqlHelper.ExecuteDataTable(trans, CommandType.Text, strMaterial);
					DataTable dtStorage =
						SqlHelper.ExecuteDataTable(trans, CommandType.Text,
						                           "select * from tbStorage where cnvcStorageDeptID='" + strDeptID + "'");
					DataTable dtOrder = dtStorage.Clone();
					foreach(DataRow dr in dtOrderBookDetail.Rows)
					{
						OrderBookDetail obd = new OrderBookDetail(dr);
						DataRow[] drFormulas = dtFormula.Select("cnvcProductCode='" + obd.cnvcInvCode + "'");
						if(drFormulas.Length > 0)
						{
							Entity.Formula formula = new AMSApp.zhenghua.Entity.Formula(drFormulas[0]);
							decimal dPortionCount = 1;
							if(formula.cnvcProductType == "SEMIPRODUCT")
							{
								dPortionCount = formula.cnnPortionCount;
							}
							DataRow[] drDosages = dtDosage.Select("cnvcProductCode='" + obd.cnvcInvCode + "'");
							if(drDosages.Length > 0)
							{
								foreach(DataRow drDosage in drDosages)
								{
									Dosage dosage = new Dosage(drDosage);
									DataRow drOrder = dtOrder.NewRow();
									drOrder["cnvcStorageDeptID"] = strDeptID;
									drOrder["cnvcProductCode"] = dosage.cnvcCode;
									drOrder["cnvcProductName"] = dosage.cnvcName;
									if(dosage.cnvcProductType == "Raw" || dosage.cnvcProductType == "Pack")
									{
										DataRow[] drMaterials = dtMaterial.Select("cnvcMaterialCode='" + dosage.cnvcCode + "'");
										if(drMaterials.Length == 0)
										{
											throw new Exception("未找到相应原材料，材料编码为："+dosage.cnvcCode);
										}
										Material material = new Material(drMaterials[0]);
										drOrder["cnvcUnit"] = material.cnvcUnit;
										drOrder["cnnCount"] = Math.Round(dosage.cnnCount*obd.cnnOrderCount/material.cnnConversion, 4);
									}
									else
									{
										drOrder["cnvcUnit"] = dosage.cnvcUnit;
										drOrder["cnnCount"] = Math.Round(dosage.cnnCount*obd.cnnOrderCount/dPortionCount, 4);
									}
									dtOrder.Rows.Add(drOrder);
								}
								
							}
						}
					}
					//判断库存是否支持生产
					foreach(DataRow drOrder in dtOrder.Rows)
					{
						Entity.Storage storage = new AMSApp.zhenghua.Entity.Storage(drOrder);
						DataRow[] drStorages = dtStorage.Select("cnvcProductCode='" + storage.cnvcProductCode + "'");
						if(drStorages.Length == 0)
						{
							throw new Exception(storage.cnvcProductName+"无库存");
						}
						Entity.Storage oldStorage = new AMSApp.zhenghua.Entity.Storage(drStorages[0]);
						if(storage.cnnCount > oldStorage.cnnCount)
						{
							throw new Exception(storage.cnvcProductName + "库存量不足");
						}
						drOrder["cnnSafeCount"] = oldStorage.cnnSafeCount;
						drOrder["cnnSafeUpCount"] = oldStorage.cnnSafeUpCount;
					}
					//生产
					//1去料
					foreach(DataRow drOrder in dtOrder.Rows)
					{
						Entity.Storage storage = new AMSApp.zhenghua.Entity.Storage(drOrder);
						StorageLog storageLog = new StorageLog(drOrder);
						storageLog.cnvcOperID = operLog.cnvcOperID;
						storageLog.cndOperDate = dtSysTime;
						storageLog.cnvcStorageDeptID = strDeptID;
						DataRow[] drFormula = dtFormula.Select("cnvcProductCode='" + storage.cnvcProductCode + "'");
						if(drFormula.Length == 0)
						{
							storageLog.cnvcOperType = "DA03";//原材料出
						}
						else
						{
							storageLog.cnvcOperType = "DB04";//半成品出
						}
						DataRow[] drStorages = dtStorage.Select("cnvcProductCode='" + storage.cnvcProductCode + "'");
						if(drStorages.Length == 0)
						{
							throw new Exception(storage.cnvcProductName+"无库存");
						}
						Entity.Storage oldStorage = new AMSApp.zhenghua.Entity.Storage(drStorages[0]);
						if(storage.cnnCount > oldStorage.cnnCount)
						{
							throw new Exception(storage.cnvcProductName + "库存量不足");
						}
						oldStorage.cnnCount = oldStorage.cnnCount - storage.cnnCount;
						EntityMapping.Update(oldStorage, trans);
						EntityMapping.Create(storageLog, trans);
						//写日志
					}
					foreach(DataRow dr in dtOrderBookDetail.Rows)
					{
						OrderBookDetail obd = new OrderBookDetail(dr);
						StorageLog storageLog = new StorageLog(dr);
						storageLog.cnvcOperID = operLog.cnvcOperID;
						storageLog.cnvcStorageDeptID = strDeptID;
						storageLog.cndOperDate = dtSysTime;
						storageLog.cnvcOperType = "DC03";//成品入
						
						DataRow[] drStorages = dtStorage.Select("cnvcProductCode='" + obd.cnvcInvCode + "'");
						if(drStorages.Length == 0)
						{
							//添加
							Entity.Storage storage = new AMSApp.zhenghua.Entity.Storage(dr);
							storage.cnnCount = obd.cnnOrderCount;
							storage.cnvcStorageDeptID = strDeptID;
							EntityMapping.Create(storage, trans);
						}
						else
						{
							//更新量
							Entity.Storage storage = new AMSApp.zhenghua.Entity.Storage(drStorages[0]);
							storage.cnnCount = storage.cnnCount + obd.cnnOrderCount;
							EntityMapping.Update(storage, trans);
							storageLog.cnnSafeCount = storage.cnnSafeCount;
							storageLog.cnnSafeUpCount = storage.cnnSafeUpCount;
						}
						storageLog.cnnCount = obd.cnnOrderCount;
						EntityMapping.Create(storageLog, trans);
						
					}
					//更新订单状态
					ob.cnvcOrderState = "3";
					EntityMapping.Update(ob, trans);

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


		public void AssignPrint(string strAssignSerialNo,OperLog operLog)//,BusiLog busiLog)
		{
			using (SqlConnection	conn  =  ConnectionPool.BorrowConnection())
			{
				//conn.Open();	
				
				SqlTransaction trans = conn.BeginTransaction();
				try
				{
					string strSysTime = SqlHelper.ExecuteScalar(trans, CommandType.Text, "select getdate()").ToString();
					DateTime dtSysTime = DateTime.Parse(strSysTime);
										
					string strUpdateSql = "update tbAssignLog set cnnPrintFlag=cnnPrintFlag+1 where cnnAssignSerialNo="+strAssignSerialNo;
					SqlHelper.ExecuteNonQuery(trans,CommandType.Text,strUpdateSql);

					OrderSerialNo serialNo = new OrderSerialNo();
					serialNo.cnvcFill = "0";
					serialNo.cnnSerialNo = Convert.ToDecimal(EntityMapping.Create(serialNo, trans));

					operLog.cndOperDate = dtSysTime;
					operLog.cnvcComments = "分货流水："+strAssignSerialNo;
					EntityMapping.Create(operLog,trans);

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
